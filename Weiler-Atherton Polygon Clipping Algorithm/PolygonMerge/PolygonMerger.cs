using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

// реализация алгоритма Уайлера-Атертона для 2-х контуров, принадлежащих разным полигонам;
// внешний+внешний
// внешний+внутренний
// внутренний+внутренний

namespace PolygonMerge
{
    enum MergedContoursType { OUTER_WITH_OUTER, OUTER_WITH_INNER, INNER_WITH_INNER };
    enum CurrentContour     { POLYGON, WINDOW };
    enum VertType           { INTERSECT_OUT = -1, NO_INTERSECT = 0, INTERSECT_IN = 1 };
    

    class PolygonMerger
    {
        public PolygonMerger(Polygon First_wnd, Polygon Second_pl, MergedContoursType type) 
        {
            wind = First_wnd;
            poly = Second_pl;
            window = new List <Vertex>();
            polygon = new List<Vertex>();
       
            intersections = new List<IntersecPoint>();
            polyForSort = new List<List<int>>();
            windForSort = new List<List<int>>();
            ContoursTYPE = type;
        }
        public Polygon wind;
        public Polygon poly;
       
        private MergedContoursType ContoursTYPE;
        public bool NoIntersect = true; 
       
        public List<Polygon> Result;        
        public Polygon curResult;

        public List<Vertex> window; 
        public List<Vertex> polygon;
        public static List<IntersecPoint> intersections;
        public List<List<int>> polyForSort;
        public List<List<int>> windForSort;


        public class IntersecPointComparerPoly : IComparer<int>
        {
            public int Compare(int l, int r)
            {
                
                int ldist = (intersections[l].start_Poly.X - intersections[l].P.X) * (intersections[l].start_Poly.X - intersections[l].P.X)
                    + (intersections[l].start_Poly.Y - intersections[l].P.Y) * (intersections[l].start_Poly.Y - intersections[l].P.Y);
                int rdist = (intersections[r].start_Poly.X - intersections[r].P.X) * (intersections[r].start_Poly.X - intersections[r].P.X)
                    + (intersections[r].start_Poly.Y - intersections[r].P.Y) * (intersections[r].start_Poly.Y - intersections[r].P.Y);
                
                return ldist.CompareTo(rdist);
                
            }
        }
        public class IntersecPointComparerWnd : IComparer<int>
        {
            public int Compare(int l, int r)
            {
                int ldist = (intersections[l].start_Wind.X - intersections[l].P.X) * (intersections[l].start_Wind.X - intersections[l].P.X)
                    + (intersections[l].start_Wind.Y - intersections[l].P.Y) * (intersections[l].start_Wind.Y - intersections[l].P.Y);
                int rdist = (intersections[r].start_Wind.X - intersections[r].P.X) * (intersections[r].start_Wind.X - intersections[r].P.X)
                    + (intersections[r].start_Wind.Y - intersections[r].P.Y) * (intersections[r].start_Wind.Y - intersections[r].P.Y);

                return ldist.CompareTo(rdist);
            }
        }
        public class Vertex
        {
            public Vertex(Polygon polyg, int index, Point _p)
            {
                Polyg = polyg;
                Index = index;
                P = _p;
            }
            
            public           int Index; // индекс в исходном массиве: wind or poly
            public       Polygon Polyg; // полигон           
            public   VertType VertType;
            public        bool visited;
            public             Point P; 
        }
        public class IntersecPoint
        {
            public IntersecPoint(Point point, int _startPPoly, Point _start_Poly, int _startPWind, Point _start_Wind)
            {
                startPPoly = _startPPoly;
                startPWind = _startPWind;
                start_Poly = _start_Poly;
                start_Wind = _start_Wind;
                P = point;
            }
            public Point P;
            public int startPPoly;
            public Point start_Poly;
            public int startPWind;
            public Point start_Wind;
            public int entryPoly;
            public int entryWind;
        }
        
               
        public void MakePolygonLists() 
        {
            for (int i = 0; i < wind.VertexCount; i++)
            {
                Vertex v = new Vertex(wind, i,wind.GetVertex(i));                   
                window.Add(v); 
            }
            for (int i = 0; i < poly.VertexCount; i++)
            {
                Vertex v = new Vertex(poly, i, poly.GetVertex(i));
                polygon.Add(v);
            }
        }
        public void FindPolygonsIntersectionPoints()
        {
                 
            for (int i = 0; i < poly.VertexCount; i++)
            {
                int j = i + 1; 
                if (i == poly.VertexCount - 1) j = 0;

                Point Seg1Start = poly.GetVertex(i);
                Point Seg1End   = poly.GetVertex(j);

                
                for (int i2 = 0; i2 < wind.VertexCount; i2++)
                {
                    int j2 = i2 + 1;
                    if (i2 == wind.VertexCount - 1) j2 = 0;

                    Point Seg2Start = wind.GetVertex(i2);
                    Point Seg2End   = wind.GetVertex(j2);

                    Point PointIntersec = new Point();
                    if (!Utility.SegmentIntersectionPoint(Seg1Start, Seg1End, Seg2Start, Seg2End, out PointIntersec)) continue;

                    NoIntersect = false;
                    IntersecPoint p = new IntersecPoint(PointIntersec, i, poly.GetVertex(i), i2, wind.GetVertex(i2));
                    intersections.Add(p);
                }
            }
            
        }
        public void SortIntersections()
        {
            for (int i = 0; i < wind.VertexCount; i++)
            {
                List<int> l = new List<int>();
                windForSort.Add(l);
            }
            for (int i = 0; i < poly.VertexCount; i++)
            {
                List<int> l = new List<int>();
                polyForSort.Add(l);
            }// пробегаем по доп массиву и создаем списки для сортировки
            for (int i = 0; i < intersections.Count; i++)
            { 
                polyForSort[intersections[i].startPPoly].Add(i);    
                windForSort[intersections[i].startPWind].Add(i);            
            }
            // сортируем все списки в обоих списках
            for (int i = 0; i < windForSort.Count; i++)
            {
                if (windForSort[i].Count < 2) continue;
                IntersecPointComparerWnd comp = new IntersecPointComparerWnd();
                windForSort[i].Sort(0, windForSort[i].Count, comp);          
            }
            for (int i = 0; i < polyForSort.Count; i++)
            {
                if (polyForSort[i].Count < 2) continue;
                IntersecPointComparerPoly comp = new IntersecPointComparerPoly();
                polyForSort[i].Sort(0, polyForSort[i].Count, comp);   
            }

        }

        public void InsertIntersectPoints()
        {
            int AddedCount = 0;
            for (int i = 0; i < windForSort.Count; i++)
            {
                for (int j = 0; j < windForSort[i].Count; j++)
                {
                    Point p = intersections[windForSort[i][j]].P;
                    Vertex inter = new Vertex(null, -1, p); 
                    inter.Index = windForSort[i].ElementAt(j); 
                    window.Insert(i + 1 + AddedCount, inter);
                    intersections[windForSort[i][j]].entryWind = i + 1 + AddedCount;
                    AddedCount++;
                }
            }
            AddedCount = 0;
            for (int i = 0; i < polyForSort.Count; i++)
            {
                for (int j = 0; j < polyForSort[i].Count; j++)
                {
                    Point p = intersections[polyForSort[i][j]].P;
                    Vertex inter = new Vertex(null, -1,p); 
                    inter.Index = polyForSort[i].ElementAt(j); 
                    polygon.Insert(i + 1 + AddedCount, inter);
                    intersections[polyForSort[i][j]].entryPoly = i + 1 + AddedCount;
                    AddedCount++;
                }
            }
        }
        public void SetInnerAndOuterVertexes()
        { 
            VertType vtype = VertType.INTERSECT_IN;
            // объединение внешних контуров: стартовой точкой является выходная точка пересечения
            if (ContoursTYPE == MergedContoursType.OUTER_WITH_OUTER)
            {
                if (Utility.PointInPolygon(wind, poly.GetVertex(0)) == Utility.Inside)
                    vtype = VertType.INTERSECT_OUT; 
            }
            // объединение внешнего контура с внутренним или 2х внутренних:
            //стартовой точкой является входная точка пересечения
            else if (ContoursTYPE == MergedContoursType.OUTER_WITH_INNER
                    || ContoursTYPE == MergedContoursType.INNER_WITH_INNER)
            {
                if (Utility.PointInPolygon(wind, poly.GetVertex(0)) == Utility.Outside)
                    vtype = VertType.INTERSECT_OUT;
            }
            for (int i = 0; i < polygon.Count; i++)
            {
                if (polygon[i].Polyg != null) continue; 
                polygon[i].VertType = vtype;
                // изменить тип следующей вершины на противоположный
                vtype = (VertType)((-1) * (sbyte)vtype);
            }
        }
        public void MakeResultPolygon()
        {
            bool NewCycle = false;
            for (int i = 0; i < polygon.Count; i++)
            {
                if (polygon[i].VertType != VertType.INTERSECT_OUT || polygon[i].visited) continue;
                NewCycle = true;                
                Polygon res = new Polygon();
                Result.Add(res);
                curResult = res;
                FindOuterCycle(CurrentContour.POLYGON, i, polygon[i], NewCycle);                
            }
        }
        
        public List<Polygon> MakeUnion() 
        {
            MakePolygonLists();            
            FindPolygonsIntersectionPoints();
            Result = new List<Polygon>();
            // если пересечений не найдено, вернуть пустое множество
            if (NoIntersect) return Result; 
            
            SortIntersections(); 
            InsertIntersectPoints();
            SetInnerAndOuterVertexes();

            
            MakeResultPolygon();
            //очищаем контейнер точек пересечения
            intersections.Clear(); 
            return Result;
        }      

        private void FindOuterCycle(CurrentContour curContour, int IndexToStart, Vertex start, bool NewCycle)
        {
            // поиск цикла из выходящей вершины
            List<Vertex> p = polygon;
            if (curContour == CurrentContour.WINDOW) p = window;

            for (int i = IndexToStart; ; i++)
            {
                if (i == p.Count) i = 0; 
                
                if (p[i].P == start.P && p[i].visited) return;
                
                if (p[i].Polyg == null)
                {
                    p[i].visited = true;
                    if (curContour == CurrentContour.WINDOW)
                        polygon[intersections[p[i].Index].entryPoly].visited = true;
                    else window[intersections[p[i].Index].entryWind].visited = true;
                }

                curResult.AddVertex(p[i].P);

                if (p[i].Polyg == null && !NewCycle)
                {

                    if (curContour == CurrentContour.POLYGON)
                        FindOuterCycle(CurrentContour.WINDOW, intersections[p[i].Index].entryWind + 1, start, NewCycle);
                    else
                        FindOuterCycle(CurrentContour.POLYGON, intersections[p[i].Index].entryPoly + 1, start, NewCycle);
                    break; 
                }
                NewCycle = false;
            }
        }
    }
}
