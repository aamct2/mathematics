
Imports Mathematics.Geometry
Imports NUnit.Framework

Namespace Geometry

    <TestFixture()> Public Class Plane_Tests
        Private plane1 As Plane
        Private plane2 As Plane
        Private pt1 As PointF3
        Private pt2 As PointF3

        Public Sub New()
            MyBase.New()
        End Sub

        <SetUp()> Public Sub Init()
            pt2 = New PointF3(0.0, 0.0, 0.0)
            plane2 = New Plane(pt2, New Vector3(1.0, 0.0, 0.0), New Vector3(0.0, 1.0, 0.0))
        End Sub

#Region "  Property Tests  "

        <Test()> Public Sub PropertyNormalVector()
            Dim testVect = New Vector3(0.0, 0.0, 1.0)
            Assert.IsTrue(plane2.NormalVector.Parallel(testVect), "PropertyNormalVector Test 1")

            pt1 = New PointF3(1.0, 0.0, 0.0)
            plane1 = New Plane(pt1, New Vector3(1.0, 0.0, 0.0), New Vector3(0.0, 0.0, 1.0))
            testVect = New Vector3(0.0, 1.0, 0.0)
            Assert.IsTrue(plane1.NormalVector.Parallel(testVect), "PropertyNormalVector Test 2")
        End Sub

#End Region

#Region "  Method Tests   "

        <Test()> Public Sub MethodClosestPointOnPlane()
            Dim pt3 As PointF3

            pt1 = New PointF3(0.0, 0.0, 1.0)
            pt3 = New PointF3(0.0, 0.0, 0.0)
            Assert.IsTrue(plane2.ClosestPointOnPlane(pt1).Equals(pt3), "MethodClosestPointOnPlane Test 1")

            pt1 = New PointF3(0.0, 0.0, -1.0)
            pt3 = New PointF3(0.0, 0.0, 0.0)
            Assert.IsTrue(plane2.ClosestPointOnPlane(pt1).Equals(pt3), "MethodClosestPointOnPlane Test 2")

            pt1 = New PointF3(1.0, 1.0, 1.0)
            pt3 = New PointF3(1.0, 1.0, 0.0)
            Assert.IsTrue(plane2.ClosestPointOnPlane(pt1).Equals(pt3), "MethodClosestPointOnPlane Test 3")

            pt1 = New PointF3(1.0, 1.0, -1.0)
            pt3 = New PointF3(1.0, 1.0, 0.0)
            Assert.IsTrue(plane2.ClosestPointOnPlane(pt1).Equals(pt3), "MethodClosestPointOnPlane Test 4")

            pt1 = New PointF3(-1.0, -1.0, 1.0)
            pt3 = New PointF3(-1.0, -1.0, 0.0)
            Assert.IsTrue(plane2.ClosestPointOnPlane(pt1).Equals(pt3), "MethodClosestPointOnPlane Test 5")

            pt1 = New PointF3(-1.0, -1.0, -1.0)
            pt3 = New PointF3(-1.0, -1.0, 0.0)
            Assert.IsTrue(plane2.ClosestPointOnPlane(pt1).Equals(pt3), "MethodClosestPointOnPlane Test 6")
        End Sub

        <Test()> Public Sub MethodDistancePoint()
            pt1 = New PointF3(0.0, 0.0, 1.0)
            Assert.IsTrue(plane2.Distance(pt1) = 1.0, "MethodDistancePoint Test 1")

            pt1 = New PointF3(0.0, 0.0, -2.0)
            Assert.IsTrue(plane2.Distance(pt1) = 2.0, "MethodDistancePoint Test 2")

            pt1 = New PointF3(1.0, 1.0, 1.0)
            Assert.IsTrue(plane2.Distance(pt1) = 1.0, "MethodDistancePoint Test 3")

            pt1 = New PointF3(1.0, 1.0, -1.0)
            Assert.IsTrue(plane2.Distance(pt1) = 1.0, "MethodDistancePoint Test 4")

            pt1 = New PointF3(-1.0, -1.0, 1.0)
            Assert.IsTrue(plane2.Distance(pt1) = 1.0, "MethodDistancePoint Test 5")

            pt1 = New PointF3(-1.0, -1.0, -1.0)
            Assert.IsTrue(plane2.Distance(pt1) = 1.0, "MethodDistancePoint Test 6")
        End Sub

        <Test()> Public Sub MethodParallel()
            pt1 = New PointF3(1.0, 0.0, 0.0)
            plane1 = New Plane(pt1, New Vector3(1.0, 0.0, 0.0), New Vector3(0.0, 1.0, 0.0))
            Assert.IsTrue(plane1.Parallel(plane2), "MethodParallel Test 1")

            plane1 = New Plane(pt1, New Vector3(1.0, 0.0, 0.0), New Vector3(0.0, 0.0, 1.0))
            Assert.IsFalse(plane1.Parallel(plane2), "MethodParallel Test 2")
        End Sub

#End Region

    End Class

End Namespace
