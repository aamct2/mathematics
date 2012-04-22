
Imports Mathematics.Geometry
Imports NUnit.Framework

Namespace Geometry

    <TestFixture()> Public Class Line_Tests
        Private line1 As Line
        Private line2 As Line
        Private pt1 As PointF3
        Private pt2 As PointF3

        Public Sub New()
            MyBase.New()
        End Sub

        <SetUp()> Public Sub Init()
            pt2 = New PointF3(0.0, 0.0, 0.0)
            line2 = New Line(pt2, New Vector3(1.0, 0.0, 0.0))
        End Sub

#Region "  Method Tests  "

        <Test()> Public Sub MethodClosestPointOnLine()
            Dim pt3 As PointF3

            pt1 = New PointF3(0.0, 0.0, 0.0)
            pt3 = New PointF3(0.0, 0.0, 0.0)
            Assert.IsTrue(line2.ClosestPointOnLine(pt1).Equals(pt3))

            pt1 = New PointF3(1.0, 0.0, 0.0)
            pt3 = New PointF3(1.0, 0.0, 0.0)
            Assert.IsTrue(line2.ClosestPointOnLine(pt1).Equals(pt3))

            pt1 = New PointF3(0.0, 1.0, 0.0)
            pt3 = New PointF3(0.0, 0.0, 0.0)
            Assert.IsTrue(line2.ClosestPointOnLine(pt1).Equals(pt3))

            pt1 = New PointF3(0.0, 0.0, 1.0)
            pt3 = New PointF3(0.0, 0.0, 0.0)
            Assert.IsTrue(line2.ClosestPointOnLine(pt1).Equals(pt3))

            pt1 = New PointF3(1.0, 0.0, 1.0)
            pt3 = New PointF3(1.0, 0.0, 0.0)
            Assert.IsTrue(line2.ClosestPointOnLine(pt1).Equals(pt3))
        End Sub

#End Region

    End Class

End Namespace
