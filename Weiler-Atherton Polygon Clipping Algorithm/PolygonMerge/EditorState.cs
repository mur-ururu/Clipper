using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace PolygonMerge
{
    public abstract class EditorState
    {
        public abstract void MouseDown(MouseEventArgs e, ImageEditor imgEd);
        public abstract void KeyPress(KeyPressEventArgs e, ImageEditor imgEd);
        protected bool WhereIsThePoint(MouseEventArgs e, ImageEditor imgEd, out Polygon p, out int index)
        {
            
            int mouseSensitivity = imgEd.mouseSensitivity;
            p = null;
            index = -1;
            for (int i = 0; i < imgEd.CurPolygon.VertexCount; i++)
            {
                if (Math.Abs(e.X - imgEd.CurPolygon.GetVertex(i).X) <= mouseSensitivity
                    && Math.Abs(e.Y - imgEd.CurPolygon.GetVertex(i).Y) <= mouseSensitivity)
                {
                    p = imgEd.CurPolygon;
                    index = i;
                    return true;                      
                }
            }
            for (int i = 0; i < imgEd.CurPolygon.SubPolygs.Count; i++)
            {
                for (int j = 0; j < imgEd.CurPolygon.SubPolygs[i].VertexCount; j++)
                {
                    if (Math.Abs(e.X - imgEd.CurPolygon.SubPolygs[i].GetVertex(j).X) <= mouseSensitivity
                        && Math.Abs(e.Y - imgEd.CurPolygon.SubPolygs[i].GetVertex(j).Y) <= mouseSensitivity)
                    {
                        p = imgEd.CurPolygon.SubPolygs[i];
                        index = j;
                        return true;
                    }
                }
            }
            MessageBox.Show("Указанная точка не совпадает ни с одной из вершин редактируемого полигона! Можно снизить чувствительность курсора"); 
            return false;
        }
        protected void Clean(ImageEditor imgEd)
        {
            imgEd.CurContour = null;
            imgEd.CurPoints.Clear();
            MessageBox.Show("Пожалуйста, отмечайте смежные точки. Выделение удалено");
        }
    }
    public class NewPolygon : EditorState
    {
        public override void MouseDown(MouseEventArgs e, ImageEditor imgEd)
        {
            Point E = imgEd.ShiftPointIfNotEmpty(new Point(e.X, e.Y));
            Graphics g = imgEd.GetGraphics();
            imgEd.CurPolygon.AddVertex(E);

            if (imgEd.CurPolygon.VertexCount > 1)
                g.DrawLine(new Pen(Brushes.Black, 2), E,
                    new Point(imgEd.CurPolygon.GetVertex(imgEd.CurPolygon.VertexCount - 2).X,
                        imgEd.CurPolygon.GetVertex(imgEd.CurPolygon.VertexCount - 2).Y));
            else
                g.DrawRectangle(new Pen(Brushes.Black, 2), E.X, E.Y, 1, 1);
            imgEd.ReDraw();          

        }
        public override void KeyPress(KeyPressEventArgs e, ImageEditor imgEd)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                if (imgEd.CurPolygon.VertexCount >= 3)
                {
                    Graphics g = imgEd.GetGraphics();
                    g.DrawLine(new Pen(Brushes.Black, 2), new Point(imgEd.CurPolygon.GetVertex(0).X,
                        imgEd.CurPolygon.GetVertex(0).Y), new Point(imgEd.CurPolygon.GetVertex(imgEd.CurPolygon.VertexCount - 1).X,
                            imgEd.CurPolygon.GetVertex(imgEd.CurPolygon.VertexCount - 1).Y));
                    imgEd.ReDraw();

                    MessageBox.Show("Внешний контур создан");
                    imgEd.EditingOff();
                    imgEd.fm.SetbtInnerContour(true);
                    
                }
                else MessageBox.Show("Контур не может содержать менее 3 точек!");
            }
        }
    }
    public class DeletingPoints : EditorState
    {
        public override void MouseDown(MouseEventArgs e, ImageEditor imgEd)
        {
            if (imgEd.CurPolygon == null) return;
            Polygon p;
            int ind = -1;

            if (WhereIsThePoint(e, imgEd, out p, out ind) == false) return;
            if (p.VertexCount <= 3) { MessageBox.Show("Количество точек контура минимально. Удаление невозможно"); return; }
            
            Point point = new Point(p.GetVertex(ind).X, p.GetVertex(ind).Y); // todo подсветка точки

            Graphics g = imgEd.GetGraphics();
            p.Paint(g, new Pen(Brushes.White, 2));
            imgEd.ReDraw();
                        
            p.RemoveVertexAt(ind);

            g = imgEd.GetGraphics();
            p.Paint(imgEd.g);
            imgEd.ReDraw();
        } 
        public override void KeyPress(KeyPressEventArgs e, ImageEditor imgEd) { }
    }
    public class AddingPoints : EditorState
    {
        private void AddNewPoint(ImageEditor imgEd, MouseEventArgs e, Polygon polyg)
        {
            int pos = Math.Max(imgEd.CurPoints[0], imgEd.CurPoints[1]);
            if (pos == polyg.VertexCount-1) pos++;

            Point E = imgEd.ShiftPointIfNotEmpty(new Point(e.X, e.Y));           
            Graphics g = imgEd.GetGraphics();
            polyg.Paint(g, new Pen(Brushes.White, 2));
            polyg.AddVertexAt(pos, E);
            imgEd.ReDraw();
            g = imgEd.GetGraphics();
            polyg.Paint(g);
            imgEd.ReDraw();

            imgEd.CurPoints.Clear();
        }
        public override void MouseDown(MouseEventArgs e, ImageEditor imgEd)
        {
            if (imgEd.CurPolygon == null) return;
            Polygon p;
            int ind = -1;
            if (imgEd.CurPoints.Count < 2)
            {
                if (WhereIsThePoint(e, imgEd, out p, out ind) == false) return;
                if (p == imgEd.CurPolygon)
                {
                    if (imgEd.CurPoints.Count == 1 && imgEd.CurContour != null)
                    {
                        Clean(imgEd);
                        return;
                    }
                    imgEd.CurPoints.Add(ind);
                }
                else  // попали в контур
                {
                    if (imgEd.CurContour != null && p != imgEd.CurContour) {    Clean(imgEd); return;}
                    imgEd.CurContour = p;
                    imgEd.CurPoints.Add(ind);
                }
            }
            else
            {
                int dif = Math.Abs(imgEd.CurPoints[0] - imgEd.CurPoints[1]);
                
                if (imgEd.CurContour==null) {
                    if ( dif!= 1 && (dif!=imgEd.CurPolygon.VertexCount-1)) { Clean(imgEd);return;}
                    AddNewPoint(imgEd, e, imgEd.CurPolygon);
                }
                else 
                {
                    if ( dif!= 1 && (dif!=imgEd.CurContour.VertexCount-1)) { Clean(imgEd);return;}
                    AddNewPoint(imgEd, e, imgEd.CurContour);    
                }
            }
        }
        public override void KeyPress(KeyPressEventArgs e, ImageEditor imgEd) { }
    }
    
    public class NoEditing : EditorState
    {
        public override void MouseDown(MouseEventArgs e, ImageEditor imgEd) { }
        public override void KeyPress(KeyPressEventArgs e, ImageEditor imgEd) { }
    }
    public class NewContour : EditorState 
    {
        // todo //объединить с классом NewPolygon! изначально предполагалось, что реализации этих классов будут значительно отличаться
        public override void MouseDown(MouseEventArgs e, ImageEditor imgEd)
        {
            Point E = imgEd.ShiftPointIfNotEmpty(new Point(e.X, e.Y));
            imgEd.CurContour.AddVertex(E);
            Graphics g = imgEd.GetGraphics();
            if (imgEd.CurContour.VertexCount > 1)
                imgEd.g.DrawLine(new Pen(Brushes.Black, 2), E, 
                    new Point(imgEd.CurContour.GetVertex(imgEd.CurContour.VertexCount - 2).X,
                        imgEd.CurContour.GetVertex(imgEd.CurContour.VertexCount - 2).Y));
            else
                imgEd.g.DrawRectangle(new Pen(Brushes.Black, 2), E.X, E.Y, 1, 1);
            imgEd.ReDraw();
        }
        public override void KeyPress(KeyPressEventArgs e, ImageEditor imgEd)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                if (imgEd.CurContour.VertexCount >= 3)
                {
                    Graphics g = imgEd.GetGraphics();
                    imgEd.g.DrawLine(new Pen(Brushes.Black, 2), new Point(imgEd.CurContour.GetVertex(0).X,
                        imgEd.CurContour.GetVertex(0).Y), new Point(imgEd.CurContour.GetVertex(imgEd.CurContour.VertexCount - 1).X,
                            imgEd.CurContour.GetVertex(imgEd.CurContour.VertexCount - 1).Y));
                    imgEd.ReDraw();
                    MessageBox.Show("Внутренний контур добавлен");
                    imgEd.EditingOff();
                }
                else MessageBox.Show("Контур не может содержать менее 3 точек!");
            }
        }
    }



      
}
