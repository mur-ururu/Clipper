using PolygonMerge;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;

namespace unitTest
{  
    
    /// <summary>
    ///This is a test class for UtilityTest and is intended
    ///to contain all UtilityTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UtilityTest
    {

        private Polygon Polygon_ForGeneralCases;
        private Polygon Polygon_ForSpecialCases;
        private TestContext testContextInstance;
        public UtilityTest() // constructor
        {
            // create all polygons we need
            Polygon_ForGeneralCases = InitializePolygon_ForGeneralCases();
            Polygon_ForSpecialCases = InitializePolygon_ForSpecialCases();
        }
        private Polygon InitializePolygon_ForGeneralCases()
        {
            Polygon Polyg = new Polygon();
            Polyg.AddVertex(new Point(235, 47));
            Polyg.AddVertex(new Point(408, 47));
            Polyg.AddVertex(new Point(370, 195));
            Polyg.AddVertex(new Point(238, 113));
            Polyg.AddVertex(new Point(112, 208));
            Polyg.AddVertex(new Point(112, 158));
            Polyg.AddVertex(new Point(152, 115));
            Polyg.AddVertex(new Point(112, 105));
            Polyg.AddVertex(new Point(112, 76));    
            
            return Polyg;
        }
        private Polygon InitializePolygon_ForSpecialCases()
        {
            Polygon Polyg = new Polygon();            
            
            Polyg.AddVertex(new Point(327, 233));
            Polyg.AddVertex(new Point(311, 280));
            Polyg.AddVertex(new Point(232, 252));
            Polyg.AddVertex(new Point(197, 185));
            Polyg.AddVertex(new Point(126, 147));
            Polyg.AddVertex(new Point(197, 128));
            Polyg.AddVertex(new Point(232, 145));
            Polyg.AddVertex(new Point(232, 125));
            Polyg.AddVertex(new Point(327, 125));

            return Polyg;
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for PointInPolygon
        ///</summary>
        [TestMethod()]
        public void PointInPolygonTest_GeneralCase_Inside()
        {  
            Point a = new Point(254, 102); 
            int expected = Utility.Inside; 
            int actual;
            actual = Utility.PointInPolygon(Polygon_ForGeneralCases, a);
            Assert.AreEqual(expected, actual);            
        }

        [TestMethod()]// самый общий случай
        public void PointInPolygonTest_GeneralCase1_Outside()
        {            
            Point a = new Point(174, 38);
            int expected = Utility.Outside;
            int actual;
            actual = Utility.PointInPolygon(Polygon_ForGeneralCases, a);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]// луч пересекает первый отрезок полигона (вершины 0 и 1)
        public void PointInPolygonTest_GeneralCase2_Outside()
        {
            Point a = new Point(309, 22);            
            int expected = Utility.Outside;
            int actual;
            actual = Utility.PointInPolygon(Polygon_ForGeneralCases, a);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()] // точка в "заливе" полигона
        public void PointInPolygonTest_GeneralCase3_Outside()
        {
            Point a = new Point(131, 120);
            int expected = Utility.Outside;
            int actual;
            actual = Utility.PointInPolygon(Polygon_ForGeneralCases, a);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void PointInPolygonTest_GeneralCase_OnBorder_Edge()
        {
            Point a = new Point(300, 47);
            int expected = Utility.OnBorder;
            int actual;
            actual = Utility.PointInPolygon(Polygon_ForGeneralCases, a);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void PointInPolygonTest_GeneralCase_OnBorder_Vertex()
        {
            Point a = new Point(235, 47);
            int expected = Utility.OnBorder;
            int actual;
            actual = Utility.PointInPolygon(Polygon_ForGeneralCases, a);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        // точка снаружи: луч, спускаясь, касается полигона по двум сторонам, она за другой
        public void PointInPolygonTest_SpecialCase1_Outside()
        {
            Point a = new Point(112, 20);
            int expected = Utility.Outside;
            int actual;
            actual = Utility.PointInPolygon(Polygon_ForGeneralCases, a);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        // точка снаружи: луч, спускаясь, касается полигона по одной стороне
        public void PointInPolygonTest_SpecialCase2_Outside()
        {
            Point a = new Point(112, 120);
            int expected = Utility.Outside;
            int actual;
            actual = Utility.PointInPolygon(Polygon_ForGeneralCases, a);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        // точка снаружи: луч, спускаясь, касается полигона в вершине
        public void PointInPolygonTest_TouchInVertex_Outside()
        {
            Point a = new Point(126, 100);
            int expected = Utility.Outside;
            int actual;
            actual = Utility.PointInPolygon(Polygon_ForSpecialCases, a);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        // точка внутри: луч, спускаясь, пересекает границу полигона в вершине
        public void PointInPolygonTest_CrossInVertex_Inside()
        {
            Point a = new Point(311, 250);
            int expected = Utility.Inside;
            int actual;
            actual = Utility.PointInPolygon(Polygon_ForSpecialCases, a);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        // точка снаружи: луч, спускаясь, пересекает границу полигона в вершине 2 раза
        public void PointInPolygonTest_DoubleCrossInVertex_Outside()
        {
            Point a = new Point(197, 100);
            int expected = Utility.Outside;
            int actual;
            actual = Utility.PointInPolygon(Polygon_ForSpecialCases, a);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        // точка снаружи: луч, спускаясь, пересекает границу полигона, так что площадь полигона по обеим сторонам от луча
        public void PointInPolygonTest_TouchEdgeWhichSeparates2PartsOfPoly_Outside()
        {
            Point a = new Point(232, 100);
            int expected = Utility.Outside;
            int actual;
            actual = Utility.PointInPolygon(Polygon_ForSpecialCases, a);
            Assert.AreEqual(expected, actual);
        }
    }
}
