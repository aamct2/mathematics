
Imports Mathematics.Geometry
Imports NUnit.Framework

Namespace Geometry

    <TestFixture()> Public Class PointF2_Tests
        Private pt1 As PointF2
        Private pt2 As PointF2

        Public Sub New()
            MyBase.New()
        End Sub

        <SetUp()> Public Sub Init()
            pt2 = New PointF2(1, 1)
        End Sub

#Region "  Constructor Tests  "

        <Test()> Public Sub ConstructorNew()
            pt1 = New PointF2()
        End Sub

        <Test()> Public Sub ConstructorNewDoubleDouble()
            pt1 = New PointF2(1.0, 1.0)
        End Sub

#End Region

#Region "  Property Tests  "

        <Test()> Public Sub PropertyXGet()
            Dim returnValue As Double

            returnValue = pt2.X
        End Sub

        <Test()> Public Sub PropertyXSet()
            pt2.X = 2.0
        End Sub

        <Test()> Public Sub PropertyYGet()
            Dim returnValue As Double

            returnValue = pt2.Y
            Assert.AreEqual(1.0, returnValue)
        End Sub

        <Test()> Public Sub PropertyYSet()
            pt2.Y = 2.0
        End Sub

#End Region

#Region "  Method Tests  "

        <Test()> Public Sub MethodEquals()
            pt1 = New PointF2(1, 1)
            Assert.IsTrue(pt1.Equals(pt2))

            pt1 = New PointF2(1, 2)
            Assert.IsFalse(pt1.Equals(pt2))
        End Sub

        <Test()> Public Sub MethodDistance()
            pt1 = New PointF2(1, 1)
            Assert.AreEqual(0.0, pt1.Distance(pt2))

            pt1 = New PointF2(1, 2)
            Assert.AreEqual(1.0, pt1.Distance(pt2))

            pt1 = New PointF2(2, 1)
            Assert.AreEqual(1.0, pt1.Distance(pt2))
        End Sub

        <Test()> Public Sub MethodToString()
            Assert.AreEqual("(1,1)", pt2.ToString)
        End Sub

#End Region

#Region "  Operator Tests  "

        <Test()> Public Sub OperatorEquals()
            pt1 = New PointF2(1, 1)
            Assert.IsTrue(pt1 = pt2)

            pt1 = New PointF2(1, 2)
            Assert.IsFalse(pt1 = pt2)
        End Sub

        <Test()> Public Sub OperatorNotEquals()
            pt1 = New PointF2(1, 1)
            Assert.IsFalse(pt1 <> pt2)

            pt1 = New PointF2(1, 2)
            Assert.IsTrue(pt1 <> pt2)
        End Sub

        <Test()> Public Sub OperatorAddition()
            Dim vect As Vector2
            Dim returnValue As PointF2

            vect = New Vector2(0, 1)
            pt1 = New PointF2(1, 0)
            returnValue = pt1 + vect
            Assert.IsTrue(pt2 = returnValue)

            vect = New Vector2(1, 0)
            pt1 = New PointF2(0, 1)
            returnValue = pt1 + vect
            Assert.IsTrue(pt2 = returnValue)

            vect = New Vector2(0, 2)
            pt1 = New PointF2(1, 0)
            returnValue = pt1 + vect
            Assert.IsFalse(pt2 = returnValue)
        End Sub

        <Test()> Public Sub OperatorCTypeToPointF2FromPointF()
            Dim pt3 As System.Drawing.PointF

            pt3.X = 1
            pt3.Y = 1
            pt1 = CType(pt3, PointF2)
            Assert.IsTrue(pt1 = pt2)

            pt3.X = 1
            pt3.Y = 2
            pt1 = CType(pt3, PointF2)
            Assert.IsFalse(pt1 = pt2)
        End Sub

        <Test()> Public Sub OperatorCTypeToPointFFromPointF2()
            Dim pt3 As System.Drawing.PointF

            pt3 = CType(pt2, System.Drawing.PointF)
            Assert.IsTrue(pt3.X = pt2.X)
            Assert.IsTrue(pt3.Y = pt2.Y)
        End Sub

        <Test()> Public Sub OperatorCTypeToPointF2FromPoint()
            Dim pt3 As System.Drawing.Point

            pt3.X = 1
            pt3.Y = 1
            pt1 = CType(pt3, PointF2)
            Assert.IsTrue(pt1 = pt2)

            pt3.X = 1
            pt3.Y = 2
            pt1 = CType(pt3, PointF2)
            Assert.IsFalse(pt1 = pt2)
        End Sub

        <Test()> Public Sub OperatorCTypeToPointFromPointF2()
            Dim pt3 As System.Drawing.Point

            pt3 = CType(pt2, System.Drawing.Point)
            Assert.IsTrue(pt3.X = pt2.X)
            Assert.IsTrue(pt3.Y = pt2.Y)
        End Sub

#End Region

    End Class

End Namespace
