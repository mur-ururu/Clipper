using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PolygonMerge
{
    class PolygonValidater
    {
        private Polygon polygon;
        private List<Segment> segments;
        public PolygonValidater(Polygon p)
        {
            polygon = p;
            segments = new List<Segment>();
        }
        public bool Validate()
        {
            if (!CheckAllSegments()) return false;
            if (!AllInnersInsideOuter()) return false;
            CheckDirections();
            return true;
        }

        class Segment
        {
            public Segment(Point _a, Point _b)
            {
                a = new Point(_a.X,_a.Y);
                b = new Point(_b.X, _b.Y);
            }
            public Point a;
            public Point b;
        }

        private bool CheckAllSegments()
        {
            for (int i = 0; i < polygon.VertexCount; i++)
            {
                Point a = polygon.GetVertex(i);
                int j = i+1;
                if (j==polygon.VertexCount) j = 0;
                Point b = polygon.GetVertex(j);
                Segment seg = new Segment(a, b);
                segments.Add(seg);
            }
            for (int i = 0; i < polygon.SubPolygs.Count; i++)
            {
                for (int m = 0; m < polygon.SubPolygs[i].VertexCount; m++)
                {
                    Point a = polygon.SubPolygs[i].GetVertex(m);
                    int  j = m + 1;
                    if (j == polygon.SubPolygs[i].VertexCount) j = 0;
                    Point b = polygon.SubPolygs[i].GetVertex(j);
                    Segment seg = new Segment(a, b);
                    segments.Add(seg);
                }
            }
            for (int i = 0; i < segments.Count - 1; i++)
            {
                for (int j = i+1/*0*/; j < segments.Count; j++)
                {
                    Point PointIntersec = new Point();
                    if (segments[i].b.X == segments[j].a.X && segments[i].b.Y == segments[j].a.Y) continue;
                    if (Utility.SegmentIntersectionPoint(segments[i].a, segments[i].b, segments[j].a, segments[j].b, out PointIntersec))
                    {
                        MessageBox.Show("Контуры не могут пересекаться!"); return false;
                    }
                }
            }
            return true;
        }
        private bool AllInnersInsideOuter()
        { 
            for (int i = 0; i < polygon.SubPolygs.Count; i++)
            {
                if (Utility.PointInPolygon(polygon, polygon.SubPolygs[i].GetVertex(0)) != Utility.Inside/*1*/)
                { MessageBox.Show("Внутренние контуры должны лежать внутри внешного!"); return false; }               
                for (int j = i + 1; j < polygon.SubPolygs.Count; j++)
                {
                    if (Utility.PointInPolygon(polygon.SubPolygs[i], polygon.SubPolygs[j].GetVertex(0)) != Utility.Outside /*- 1*/)
                    { MessageBox.Show("Внутренние контуры не могут быть расположены один внутри другого!"); return false; } 
                }
            }
            return true;
        }
        private void CheckDirections()
        {
            if (Utility.PolygonSquare(polygon) < 0) polygon.ReversePoints();
            for (int i = 0; i < polygon.SubPolygs.Count; i++)
            {
                if (Utility.PolygonSquare(polygon.SubPolygs[i]) > 0) polygon.SubPolygs[i].ReversePoints();            
            }
        }
    }
}
