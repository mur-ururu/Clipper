using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PolygonMerge
{
    class WholeMerger
    {
        private Polygon WINDOW, POLYGON, RESULT;
        public Polygon GetResult() { return RESULT; }
        public bool noSharedPoints = false;
        public WholeMerger(Polygon _WINDOW, Polygon _POLYGON)
        { 
            WINDOW = _WINDOW;
            POLYGON = _POLYGON;
            RESULT = new Polygon();
        }

        public void Merge()
        {
            MergeOuterContours();
            if (noSharedPoints) return;
            for (int i = 0; i < WINDOW.SubPolygs.Count; i++)
            {                
                MergeOuterWithInners(WINDOW.SubPolygs[i], POLYGON);
            }
            for (int i = 0; i < POLYGON.SubPolygs.Count; i++)
            {
                MergeOuterWithInners(POLYGON.SubPolygs[i], WINDOW);
            }

            for (int i = 0; i < POLYGON.SubPolygs.Count; i++)
            {
                for (int j = 0; j < WINDOW.SubPolygs.Count; j++)
                {
                    MergeInnerWithInner(WINDOW.SubPolygs[j], POLYGON.SubPolygs[i]);
                }
            }
        }
        private void MergeOuterContours()
        {
            PolygonMerger PM = new PolygonMerger(WINDOW, POLYGON, MergedContoursType.OUTER_WITH_OUTER);

            List<Polygon> contoursToAdd = PM.MakeUnion(); 
            if (PM.NoIntersect)
            {
                if (Utility.PointInPolygon(WINDOW, POLYGON.GetVertex(0)) == Utility.Inside) 
                {
                    for (int i = 0; i < WINDOW.VertexCount; i++)
                    {
                        RESULT.AddVertex(WINDOW.GetVertex(i));
                    }
                }
                else if (Utility.PointInPolygon(POLYGON, WINDOW.GetVertex(0)) == Utility.Inside)
                {
                    for (int i = 0; i < POLYGON.VertexCount; i++)
                    {
                        RESULT.AddVertex(POLYGON.GetVertex(i));
                    }
                }
                else { MessageBox.Show("Полигоны не имеют общих точек"); noSharedPoints = true; }
            }
            else
            {
                for (int i = 0; i < contoursToAdd.Count; i++)
                {
                    if (Utility.PolygonSquare(contoursToAdd[i]) > 0)
                    {
                        // добавить внешyнюю границу к ответу
                        for (int j = 0; j < contoursToAdd[i].VertexCount; j++)
                        {
                            RESULT.AddVertex(contoursToAdd[i].GetVertex(j));
                        }
                    }
                    else // добавить внутреннюю границу к ответу
                    {
                        RESULT.AddSubPolygon(contoursToAdd[i]);
                    }
                }
            }
                    
        
        }        
        private void MergeOuterWithInners(Polygon WindowHole, Polygon Polygon)
        {
            PolygonMerger PM = new PolygonMerger(WindowHole, Polygon, MergedContoursType.OUTER_WITH_INNER/* 2*/);

            List<Polygon> contoursToAdd = PM.MakeUnion();  
            if (PM.NoIntersect)
            {
                if (Utility.PointInPolygon(Polygon, WindowHole.GetVertex(0)) == Utility.Outside)
                    RESULT.AddSubPolygon(WindowHole);
            }
            else
            {
                for (int i = 0; i < contoursToAdd.Count; i++)
                {
                    RESULT.AddSubPolygon(contoursToAdd[i]);
                }
            }

        }
        private void MergeInnerWithInner(Polygon WindowHole, Polygon PolygonHole)
        {
            PolygonMerger PM = new PolygonMerger(WindowHole, PolygonHole, MergedContoursType.INNER_WITH_INNER/* 3*/);

            List<Polygon> contoursToAdd = PM.MakeUnion();
            if (PM.NoIntersect)
            {
                if (Utility.PointInPolygon(WindowHole, PolygonHole.GetVertex(0)) == Utility.Inside)
                {
                    RESULT.AddSubPolygon(PolygonHole);
                }
                else if (Utility.PointInPolygon(PolygonHole, WindowHole.GetVertex(0)) == Utility.Inside)
                {
                    RESULT.AddSubPolygon(WindowHole);
                }
            }
            else
            {
                for (int i = 0; i < contoursToAdd.Count; i++)
                {
                    RESULT.AddSubPolygon(contoursToAdd[i]);
                }
            
            }
        }
    }
}
