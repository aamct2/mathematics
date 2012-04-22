
Imports Mathematics.Geometry
Imports NUnit.Framework

Namespace Geometry

    <TestFixture()> Public Class PointF3_Tests
        Private pt1 As PointF3
        Private pt2 As PointF3

        Public Sub New()
            MyBase.New()
        End Sub

        <SetUp()> Public Sub Init()
            pt2 = New PointF3(1, 1, 1)
        End Sub

#Region "  Constructor Tests  "

        <Test()> Public Sub ConstructorNew()
            pt1 = New PointF3()
        End Sub

        <Test()> Public Sub ConstructorNewDoubleDoubleDouble()
            pt1 = New PointF3(1.0, 1.0, 1.0)
        End Sub

#End Region

#Region "  Property Tests  "

        <Test()> Public Sub PropertyXGet()
            Dim returnValue As Double

            returnValue = pt2.X
            Assert.AreEqual(1, returnValue)
        End Sub

        <Test()> Public Sub PropertyXSet()
            Dim returnValue As Double

            pt2.X = 2.0
            returnValue = pt2.X
            Assert.AreEqual(2, returnValue)
        End Sub

        <Test()> Public Sub PropertyYGet()
            Dim returnValue As Double

            returnValue = pt2.Y
            Assert.AreEqual(1.0, returnValue)
        End Sub

        <Test()> Public Sub PropertyYSet()
            Dim returnValue As Double

            pt2.Y = 2.0
            returnValue = pt2.Y
            Assert.AreEqual(2, returnValue)
        End Sub

#End Region

#Region "  Method Tests  "

        <Test()> Public Sub MethodEquals()
            pt1 = New PointF3(1, 1, 1)
            Assert.IsTrue(pt1.Equals(pt2))

            pt1 = New PointF3(1, 2, 1)
            Assert.IsFalse(pt1.Equals(pt2))

            pt1 = New PointF3(1, 1, 2)
            Assert.IsFalse(pt1.Equals(pt2))
        End Sub

        <Test()> Public Sub MethodDistance()
            pt1 = New PointF3(1, 1, 1)
            Assert.AreEqual(0.0, pt1.Distance(pt2))

            pt1 = New PointF3(1, 2, 1)
            Assert.AreEqual(1.0, pt1.Distance(pt2))

            pt1 = New PointF3(2, 1, 1)
            Assert.AreEqual(1.0, pt1.Distance(pt2))

            pt1 = New PointF3(1, 1, 2)
            Assert.AreEqual(1.0, pt1.Distance(pt2))
        End Sub

        <Test()> Public Sub MethodToString()
            Assert.AreEqual("(1,1,1)", pt2.ToString)
        End Sub

#End Region

#Region "  Operator Tests  "

        <Test()> Public Sub OperatorEquals()
            pt1 = New PointF3(1, 1, 1)
            Assert.IsTrue(pt1 = pt2)

            pt1 = New PointF3(1, 2, 1)
            Assert.IsFalse(pt1 = pt2)

            pt1 = New PointF3(1, 1, 2)
            Assert.IsFalse(pt1 = pt2)
        End Sub

        <Test()> Public Sub OperatorNotEquals()
            pt1 = New PointF3(1, 1, 1)
            Assert.IsFalse(pt1 <> pt2)

            pt1 = New PointF3(1, 2, 1)
            Assert.IsTrue(pt1 <> pt2)
        End Sub

        <Test()> Public Sub OperatorAddition()
            Dim vect As Vector3
            Dim returnValue As PointF3

            vect = New Vector3(0, 1, 1)
            pt1 = New PointF3(1, 0, 0)
            returnValue = pt1 + vect
            Assert.IsTrue(pt2 = returnValue)

            vect = New Vector3(1, 0, 1)
            pt1 = New PointF3(0, 1, 0)
            returnValue = pt1 + vect
            Assert.IsTrue(pt2 = returnValue)

            vect = New Vector3(1, 1, 0)
            pt1 = New PointF3(0, 0, 1)
            returnValue = pt1 + vect
            Assert.IsTrue(pt2 = returnValue)

            vect = New Vector3(0, 2, 0)
            pt1 = New PointF3(1, 0, 0)
            returnValue = pt1 + vect
            Assert.IsFalse(pt2 = returnValue)
        End Sub

#End Region

    End Class

End Namespace
