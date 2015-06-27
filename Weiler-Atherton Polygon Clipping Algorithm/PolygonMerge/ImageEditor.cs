using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PolygonMerge
{
    public class ImageEditor 
    {
        private int PolygonMaxNumber;
        public EditorState State;
        private PictureBox PB;
        private Bitmap bmp;

        public fmMain fm;
        public Graphics g; 

        public Polygon CurPolygon;
        public Polygon CurContour;

        public List<Polygon> Polygs;
        public List<int> CurPoints;
        public int mouseSensitivity;
        public bool NotValidated = false;
        public bool CurOuterContourCompleted = false;


        public ImageEditor(PictureBox picturebox, fmMain _fm)
        {
            fm = _fm;
            PB = picturebox;            
            //g = PB.CreateGraphics();
            g = Graphics.FromImage(PB.Image);
            PB.Invalidate(); // вызов полной перерисовки контрола
            
            Polygs = new List<Polygon>();
            CurPoints = new List<int>();
            PolygonMaxNumber = 2;
        }
        public void AddOuterContour()
        {
            Polygon polyg = new Polygon();
            Polygs.Add(polyg);
            CurPolygon = polyg;

            EditorState edState = new NewPolygon();
            State = edState;
           
        }
        public void AddInnerContour()
        {
            if (CurPolygon == null)
            {
                MessageBox.Show("Внешний контур не задан!");
                return;
            }
            Polygon polyg = new Polygon();
            CurContour = polyg;
           // Polygs.Add(polyg);
            CurPolygon.AddSubPolygon(polyg); 

            EditorState edState = new NewContour();
            State = edState;
        
        }
        public void MouseDown(MouseEventArgs e)
        {                      
            State.MouseDown(e, this);           
        }
        public void KeyPress(KeyPressEventArgs e)
        {            
            State.KeyPress(e, this);            
        }
        public void DeletePoint(EventArgs e)
        {
            EditorState edState = new DeletingPoints();
            State = edState;
            CurContour = null;
        }
        public void AddPoint(EventArgs e)
        {
            EditorState edState = new AddingPoints();
            State = edState;
            CurPoints.Clear();
            CurContour = null;
        }
        public void EditingOff()
        {
            EditorState edState = new NoEditing();
            State = edState;
        }
        public void ImageClean()
        {
            CurPolygon = null;
            Polygs.Clear();
            CurPoints.Clear();
            EditingOff();
            
            g = GetGraphics();
            g.Clear(System.Drawing.Color.White);
            ReDraw();

            NotValidated = false;
        }
        public void Merge()
        {
            if (Polygs.Count < PolygonMaxNumber)
            { MessageBox.Show("Для объединения необходимо создать " + PolygonMaxNumber.ToString() + " полигона"); return; }

            WholeMerger WholeMerg = new WholeMerger(Polygs[0], Polygs[1]);
            WholeMerg.Merge();
            if (WholeMerg.noSharedPoints) {ImageClean(); return; }
            Polygon United = WholeMerg.GetResult(); 
            CurPolygon = United;

            Polygs.Clear();
            Polygs.Add(United);

            g = GetGraphics();
            g.Clear(System.Drawing.Color.White);
            ReDraw();

            g = GetGraphics();
            United.Paint(g);
            ReDraw();
        }
        public void ValidateCur()
        {
            if (CurPolygon == null) return;
            
            PolygonValidater Validater = new PolygonValidater(CurPolygon);
            if (!Validater.Validate()) NotValidated = true;
            else NotValidated = false;
        }

        public Graphics GetGraphics()
        {
            return Graphics.FromImage(PB.Image);
        }
        public void ReDraw()
        {
            PB.Invalidate();
        }

        public Point ShiftPointIfNotEmpty(Point P) 
        {
            Point res = new Point(P.X, P.Y);
            //System.Drawing.Color col = System.Drawing.Color.White;
            bmp = (Bitmap)PB.Image;
            System.Drawing.Color coll = bmp.GetPixel(P.X, P.Y);
            if (bmp.GetPixel(P.X, P.Y).Name == "ffffffff") return res;

            if ((P.X) - 1 > 0 && (P.Y) - 1 > 0 && bmp.GetPixel((P.X) - 1, (P.Y) - 1).Name == "ffffffff")
                return new Point((P.X) - 1, (P.Y) - 1);
            if ((P.Y) - 1 > 0 && bmp.GetPixel(P.X, (P.Y) - 1).Name == "ffffffff")
                return new Point(P.X, (P.Y) - 1);
            if ((P.Y) - 1 > 0 && bmp.GetPixel((P.X) + 1, (P.Y) - 1).Name == "ffffffff")
                return new Point((P.X) + 1, (P.Y) - 1);
            if ((P.X) - 1 > 0 && bmp.GetPixel((P.X) - 1, P.Y).Name == "ffffffff")
                return new Point((P.X) - 1, P.Y);
            if (bmp.GetPixel((P.X) + 1, P.Y).Name == "ffffffff")
                return new Point((P.X) + 1, P.Y);
            if ((P.X) - 1 > 0 && bmp.GetPixel((P.X) - 1, (P.Y) + 1).Name == "ffffffff")
                return new Point((P.X) - 1, (P.Y) + 1);
            if (bmp.GetPixel(P.X, (P.Y) + 1).Name == "ffffffff")
                return new Point(P.X, (P.Y) + 1);
            if (bmp.GetPixel((P.X) + 1, (P.Y) + 1).Name == "ffffffff")
                return new Point((P.X) + 1, (P.Y) + 1);

            return res;
        }
    }
}
