using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
//using System.Windows;

namespace PolygonMerge
{
    public class Polygon
    {
        private List<Point> VertexList; // todo написать итератор для перебора вершин и компоновщик, чтобы легко было перебирать вершины вложенных контуров
        public List<Polygon> SubPolygs;

        public Polygon() 
        {
            VertexList = new List<Point>();
            SubPolygs = new List<Polygon>();
        }

        public void AddVertex (Point P) { VertexList.Add(P); }
        public void RemoveVertexAt(int Index)
        {            
            VertexList.RemoveAt(Index);
        } 
        public void AddVertexAt(int Index, Point p) { VertexList.Insert(Index, p); } 
        public Point GetVertex(int Index) 
        {
            if (Index < 0 || Index >= VertexCount)
            {
                MessageBox.Show("Недопустимое значение индекса. Результат может быть неверным");
                return new Point();
            }
            return VertexList[Index];
        }  
        public int VertexCount
        {
            get { return VertexList.Count; }
        }
        public void AddSubPolygon(Polygon subpolyg)
        {
            SubPolygs.Add(subpolyg);
        }
        public void ReversePoints()
        {
            VertexList.Reverse(0, VertexList.Count);    
        }

        public void Paint(Graphics g)
        {
            Paint(g, new Pen(Brushes.Black, 2));    
        }
        public void Paint(Graphics g, Pen pen)
        {
            PaintOne(this,g,pen);
            for (int i = 0; i < this.SubPolygs.Count; i++)
                this.SubPolygs[i].PaintOne(this.SubPolygs[i],g,pen);
        }
        public void PaintOne(Polygon p, Graphics g, Pen pen)
        {
            for (int i = 0; i < p.VertexCount; i++)
            {
                int prev = 0;
                if (i != 0) prev = i - 1;
                else prev = p.VertexCount - 1;

                g.DrawLine(pen, new Point(p.GetVertex(prev).X, p.GetVertex(prev).Y), new Point(p.GetVertex(i).X, p.GetVertex(i).Y));
            }    
        }

        public string[] ToStringArray(string name)
        {
            int linesQuan = VertexCount + 3; // 3: имя, кол-во вершин, кол-во вложенных

            int SubVertQuan = 0;
            for (int i=0;i<SubPolygs.Count;i++)
            {
                SubVertQuan += SubPolygs[i].VertexCount + 2; // 2: номер полигона, кол-во вершин в нем
            }
            if (SubVertQuan != 0) linesQuan += SubVertQuan;


            string[] lines = new string[linesQuan];
            
            lines[0] = name;
            lines[1] = VertexCount.ToString();
            lines[2] = SubPolygs.Count.ToString();
            int curIndex = 3;

            for (int i = 0; i < VertexList.Count; i++)
            {
                string s = VertexList[i].X + "," + VertexList[i].Y;// заменить на  stringbuilder
                 lines[curIndex] = s;
                 curIndex++;
            }
            // рефакторинг последобавления компоновщика в класс Polygon
            for (int i = 0; i < SubPolygs.Count; i++)
            {
                lines[curIndex++] = i.ToString();
                lines[curIndex++] = SubPolygs[i].VertexCount.ToString();
                for (int j = 0; j < SubPolygs[i].VertexList.Count; j++)
                {
                    string s = SubPolygs[i].GetVertex(j).X + "," + SubPolygs[i].GetVertex(j).Y;
                    lines[curIndex] = s;
                    curIndex++;
                }
            }
            return lines;        
        }
        public Polygon(string[] lines)
        {
            VertexList = new List<Point>();
            SubPolygs = new List<Polygon>();
            if (lines.Length <= 3) return;
            
            int vertCount = 0, subCount = 0;
            Int32.TryParse(lines[1], out vertCount);
            Int32.TryParse(lines[2], out subCount);
            int shift = 3, i = 0;
            for (i = 0; i < vertCount; i++)
            {
                System.Windows.Point pw = System.Windows.Point.Parse(lines[i + shift]);
                Point p = new Point((int)pw.X,(int) pw.Y);
                VertexList.Add(p); 
            }
            shift += i;
            for (int k = 0; k < subCount; k++)
            {
                Polygon sub = new Polygon();
                SubPolygs.Add(sub);
                shift ++;
                
                int vertSubCount = 0;
                Int32.TryParse(lines[shift], out vertSubCount);
                shift++;

                int j = 0;
                for (j = 0; j < vertSubCount; j++)
                {
                    System.Windows.Point pw = System.Windows.Point.Parse(lines[j + shift]);
                    Point p = new Point((int)pw.X, (int)pw.Y);
                    sub.VertexList.Add(p);                    
                }
                shift += j;
            }
            
        }
    }
}
