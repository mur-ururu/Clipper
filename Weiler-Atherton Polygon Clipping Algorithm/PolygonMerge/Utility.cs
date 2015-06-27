using System;
using System.Drawing;

namespace PolygonMerge
{

    class Utility
    {      
        public const int Outside  = -1;
        public const int Inside   = 1;
        public const int OnBorder = 0;


        public static bool SegmentIntersectionPoint(Point start1, Point end1, Point start2, Point end2, out Point pIntersection)
        {
            pIntersection = new Point(-1, -1);

            Point dir1 = new Point(end1.X - start1.X, end1.Y - start1.Y);
            Point dir2 = new Point(end2.X - start2.X, end2.Y - start2.Y);

            //считаем уравнения прямых, проходящих через отрезки
            double a1 = -dir1.Y;
            double b1 = +dir1.X;
            double d1 = -(a1 * start1.X + b1 * start1.Y);

            double a2 = -dir2.Y;
            double b2 = +dir2.X;
            double d2 = -(a2 * start2.X + b2 * start2.Y);

            //подставляем концы отрезков для выяснения в каких полуплоскоcтях они
            //подставляем координаты концов  первого отрезка в уравнение прямой второго отрезка 
            //либо начало, либо конец должен принадлежать второй линии
            double seg1_line2_start = a2 * start1.X + b2 * start1.Y + d2;
            double seg1_line2_end = a2 * end1.X + b2 * end1.Y + d2;

            double seg2_line1_start = a1 * start2.X + b1 * start2.Y + d1;
            double seg2_line1_end = a1 * end2.X + b1 * end2.Y + d1;

            //если концы одного отрезка имеют один знак, значит он в одной полуплоскости и пересечения нет.
            if (seg1_line2_start * seg1_line2_end >= 0 || seg2_line1_start * seg2_line1_end >= 0)
                return false;

            double u = seg1_line2_start / (seg1_line2_start - seg1_line2_end);

            pIntersection.X = (int)(start1.X + u * (double)dir1.X);
            pIntersection.Y = (int)(start1.Y + u * (double)dir1.Y);            

            return true;
        }   
        public static double PolygonSquare(Polygon Polyg)
        {
            double sum = 0;
            for (int i = 0; i < Polyg.VertexCount; i++)
            {
                int j = i + 1;
                if (i == Polyg.VertexCount - 1) j = 0;
                sum += Polyg.GetVertex(i).X * Polyg.GetVertex(j).Y - Polyg.GetVertex(j).X * Polyg.GetVertex(i).Y;
            }
            return sum/2;
        }
        public static int PointInPolygon(Polygon Polyg, Point a) 
        {
            // из точки проводим луч вертикально вниз и считаем пересечения со сторонами полигона
            int intersectNum = 0;
            bool EndIntersect = false, SecondLoop = false;
            int SegStartX = 0;

            for (int i = 0; i < Polyg.VertexCount; i++)
            {
                int j = i + 1;
                if (i == Polyg.VertexCount - 1) j = 0;
                // точка совпала с одним из концов отрезка
                if (a == Polyg.GetVertex(i) || a == Polyg.GetVertex(j)) return OnBorder; 
                // начало и конец отрезка лежат по одну сторону от луча
                int Min_X = Math.Min(Polyg.GetVertex(i).X, Polyg.GetVertex(j).X);
                int Max_X = Math.Max(Polyg.GetVertex(i).X, Polyg.GetVertex(j).X);
                if (a.X < Min_X || a.X > Max_X) continue;

                if (a.Y == Polyg.GetVertex(i).Y && a.Y == Polyg.GetVertex(j).Y) return OnBorder;
                
                double X_dif = (double)(Polyg.GetVertex(j).X - Polyg.GetVertex(i).X);
                if (X_dif !=0 ) 
                {
                    double _y = Polyg.GetVertex(i).Y + 
                        (a.X - Polyg.GetVertex(i).X) * (Polyg.GetVertex(j).Y - Polyg.GetVertex(i).Y) / X_dif;
                    
                    if (a.Y <= _y)
                    {
                        // точки начала и конца лежат на луче
                        if (Polyg.GetVertex(i).X == a.X && Polyg.GetVertex(j).X == a.X) continue;
                        // конец отрезка лежит на луче
                        if (Polyg.GetVertex(j).X == a.X) 
                        {
                            SegStartX = Polyg.GetVertex(i).X;
                            EndIntersect = true;
                        }
                        else
                        {
                            if (EndIntersect)
                            {
                                if (a.X > Math.Min(SegStartX, Polyg.GetVertex(j).X)
                                    && a.X < Math.Max(SegStartX, Polyg.GetVertex(j).X))
                                {
                                    intersectNum++;
                                    if (SecondLoop) { intersectNum--; break; } 
                                }
                                else if (SecondLoop) {  intersectNum--; break;} 
                                EndIntersect = false;
                            }
                            else intersectNum++;
                        }
                    }
                }

                if (j == Polyg.VertexCount - 1 && EndIntersect)
                {
                    i = -1;
                    SecondLoop = true;
                }
            }

            if (intersectNum % 2 == 1) return Inside;
            else return Outside;
        }
    }
}
