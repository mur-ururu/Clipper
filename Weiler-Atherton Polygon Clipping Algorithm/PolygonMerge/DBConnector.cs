using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Linq;

namespace PolygonMerge
{
    class DBConnector
    {
        string DataContextConnection;
        public DBConnector()
        {  
            DataContextConnection = "Data Source = " + Application.StartupPath + @"\DBase.sdf; Password =''";                
        } 

        public List<string> loadPolygonsList()
        {
            List<string> list = new List<string>();

            DBase db = new DBase(DataContextConnection);

            IQueryable<Polygons> DBpolys = from c in db.Polygons
                                              where c.PARENT_ID == null
                                              select c;

            foreach (Polygons DBpoly in DBpolys)
            {
                list.Add(DBpoly.NAME);
            }

            return list;
        }

        public void SavePolygon(Polygon polyg, string name)
        { 
            DBase db = new DBase(DataContextConnection);
            // insert polygon
            Polygons DBpoly = new Polygons
            {                
                NAME = name, 
            };

            for (int i = 0; i < polyg.VertexCount; i++)
            {
                Points p = new Points
                {
                    X = polyg.GetVertex(i).X,
	
	                Y = polyg.GetVertex(i).Y,

                    POLYGON_ID = DBpoly.ID,
                };
                DBpoly.Points.Add(p);
            }
            db.Polygons.InsertOnSubmit(DBpoly);
            db.SubmitChanges();

            // insert subPolygons
            for (int i = 0; i < polyg.SubPolygs.Count; i++)
            {
                Polygons DBsubpoly = new Polygons
                {
                    NAME = name,
                    PARENT_ID = DBpoly.ID,
                };

                for (int j = 0; j < polyg.SubPolygs[i].VertexCount; j++)
                {
                    Points sp = new Points
                    {
                        X = polyg.SubPolygs[i].GetVertex(j).X,

                        Y = polyg.SubPolygs[i].GetVertex(j).Y,

                        POLYGON_ID = DBsubpoly.ID,
                    };
                    DBsubpoly.Points.Add(sp);
                }
                db.Polygons.InsertOnSubmit(DBsubpoly);    
            }

            db.SubmitChanges();
        }

        public Polygon GetPolygonByName(string s)
        {
            DBase db = new DBase(DataContextConnection);
            
            Polygon poly = new Polygon();

            // construct polygon
            Polygons DBpoly = (from c in db.Polygons
                                         where c.NAME==s && c.PARENT_ID==null                                         
                                         select c).SingleOrDefault<Polygons>();
                        
            foreach (Points pnt in DBpoly.Points)
            {
                Point p = new Point(pnt.X,pnt.Y);
                poly.AddVertex(p);
            }
            // construct subPolygons
            IQueryable <Polygons> DBsubpolys = from c in db.Polygons
                              where c.PARENT_ID == DBpoly.ID
                              select c;

            foreach (Polygons DBsubpoly in DBsubpolys)
            {
                Polygon subpoly = new Polygon();

                foreach (Points pnt in DBsubpoly.Points)
                {
                    Point p = new Point(pnt.X, pnt.Y);
                    subpoly.AddVertex(p);
                }
                poly.AddSubPolygon(subpoly);
            }

            return poly;
        }

        public void DeletePolygonByName(string name)
        {
            DBase db = new DBase(DataContextConnection);

            Polygons DBpoly = (from c in db.Polygons
                             where c.PARENT_ID == null && c.NAME==name
                             select c).SingleOrDefault<Polygons>();
            // delete subpolygons
            IQueryable<Polygons> DBsubpolys = from c in db.Polygons
                                              where c.PARENT_ID == DBpoly.ID
                                              select c;
            foreach (Polygons DBsubpoly in DBsubpolys)
            {
                db.Points.DeleteAllOnSubmit(DBsubpoly.Points);
                db.Polygons.DeleteOnSubmit(DBsubpoly);
            }
            // delete polygon
            db.Points.DeleteAllOnSubmit(DBpoly.Points);
            db.Polygons.DeleteOnSubmit(DBpoly);

            db.SubmitChanges();

        }

        public bool PolygonExists(string name)
        {
            DBase db = new DBase(DataContextConnection);            

            Polygons DBpoly = (from c in db.Polygons
                               where c.NAME == name && c.PARENT_ID == null
                               select c).SingleOrDefault<Polygons>();
            if (DBpoly == null) return false;

            return true;
        }
    }
}
