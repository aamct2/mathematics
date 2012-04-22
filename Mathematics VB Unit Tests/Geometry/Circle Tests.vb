
Imports Mathematics.Geometry
Imports NUnit.Framework

Namespace Geometry

    <TestFixture()> Public Class Circle_Tests
        Private circ1 As Circle
        Private circ2 As Circle
        Private pt1 As PointF3
        Private pt2 As PointF3

        Public Sub New()
            MyBase.New()
        End Sub

        <SetUp()> Public Sub Init()
            pt2 = New PointF3(0.0, 0.0, 0.0)
            circ2 = New Circle(pt2, New Vector3(1.0, 0.0, 0.0), New Vector3(0.0, 0.0, 1.0))
        End Sub

#Region "  Method Tests  "

        <Test()> Public Sub MethodArea()
            Assert.AreEqual(circ2.Area, Math.PI, "Method Area Test")
        End Sub

        <Test()> Public Sub MethodCircumference()
            Assert.AreEqual(circ2.Circumference, 2 * Math.PI, "Method Circumference Test")
        End Sub

        <Test()> Public Sub MethodContainsPoint()
            Assert.IsTrue(circ2.Contains(pt2), "Method ContainsPoint Test 1")

            pt1 = New PointF3(0.0, 0.0, 1.0)
            Assert.IsFalse(circ2.Contains(pt1), "Method ContainsPoint Test 2")
        End Sub

        <Test()> Public Sub MethodDistancePoint()
            circ1 = New Circle(New PointF3(0.0, 0.0, 1.0), New Vector3(1.0, 0.0, 0.0), New Vector3(0.0, 0.0, 1.0))

            Assert.AreEqual(1.0, circ1.Distance(pt2), "Method Distance Test 2")

            pt1 = New PointF3(1.0, 0.0, 0.0)
            Assert.AreEqual(1.0, circ1.Distance(pt1), "Method Distance Test 2")

            Assert.AreEqual(0.0, circ2.Distance(pt2), "Method Distance Test 3")

            pt1 = New PointF3(2.0, 0.0, 0.0)
            Assert.AreEqual(1.0, circ2.Distance(pt1), "Method Distance Test 4")

            circ1 = New Circle(pt2, New Vector3(0.0, 0.0, 2.0), New Vector3(2.0, 0.0, 0.0))

            Assert.AreEqual(0.0, circ1.Distance(pt2), "Method Distance Test 5")

            pt1 = New PointF3(0.0, 0.0, 2.0)
            Assert.AreEqual(0.0, circ1.Distance(pt1), "Method Distance Test 6")

            pt1 = New PointF3(2.0, 0.0, 0.0)
            Assert.AreEqual(2.0, circ1.Distance(pt1), "Method Distance Test 7")

            pt1 = New PointF3(1.0, 0.0, 1.0)
            Assert.AreEqual(1.0, circ1.Distance(pt1), "Method Distance Test 8")
        End Sub

#End Region

    End Class

End Namespace
