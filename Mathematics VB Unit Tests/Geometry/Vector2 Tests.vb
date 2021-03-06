
Imports Mathematics.Geometry
Imports NUnit.Framework

Namespace Geometry

    <TestFixture()> Public Class Vector2_Tests
        Private vect1 As Vector2
        Private vect2 As Vector2

        Public Sub New()
            MyBase.New()
        End Sub

        <SetUp()> Public Sub Init()
            vect2 = New Vector2(1.0, 1.0)
        End Sub

#Region "  Constructor Tests  "

        <Test()> Public Sub ConstructorNew()
            vect1 = New Vector2()
        End Sub

        <Test()> Public Sub ConstructorNewDoubleDouble()
            vect1 = New Vector2(1.0, 1.0)
        End Sub

        <Test()> Public Sub ConstructorNewPointF2PointF2()
            Dim startPt As New PointF2(0.0, 0.0)
            Dim endPt As New PointF2(1.0, 1.0)
            vect1 = New Vector2(startPt, endPt)
        End Sub

        <Test()> Public Sub ConstructorNewPointF2PointF2Double()
            Dim startPt As New PointF2(0.0, 0.0)
            Dim directionPt As New PointF2(0.0, 1.0)
            Dim magnitude As Double = 2

            vect1 = New Vector2(startPt, directionPt, magnitude)
            Assert.AreEqual(2.0, vect1.Length())
        End Sub

        <Test()> Public Sub ConstructorNewVector2Double()
            Dim vect3 As Vector2
            Dim magnitude As Double = 2

            vect1 = New Vector2(0.0, 1.0)

            vect3 = New Vector2(vect1, magnitude)
            Assert.AreEqual(2.0, vect3.Length())
        End Sub

#End Region

#Region "  Property Tests  "

        <Test()> Public Sub PropertyIGet()
            Dim returnValue As Double

            returnValue = vect2.I
            Assert.AreEqual(1, returnValue)
        End Sub

        <Test()> Public Sub PropertyISet()
            Dim returnValue As Double

            vect2.I = 2.0
            returnValue = vect2.I
            Assert.AreEqual(2, returnValue)
        End Sub

        <Test()> Public Sub PropertyJGet()
            Dim returnValue As Double

            returnValue = vect2.J
            Assert.AreEqual(1.0, returnValue)
        End Sub

        <Test()> Public Sub PropertyJSet()
            Dim returnValue As Double

            vect2.J = 2.0
            returnValue = vect2.J
            Assert.AreEqual(2, returnValue)
        End Sub

#End Region

#Region "  Method Tests  "

        <Test()> Public Sub MethodAdd()
            Dim resultVect As Vector2
            vect1 = New Vector2(2.0, -1.0)

            resultVect = vect1.Add(vect2)
            Assert.AreEqual(3.0, resultVect.I)
            Assert.AreEqual(0.0, resultVect.J)
        End Sub

        <Test()> Public Sub MethodCrossProduct()
            Dim resultVect As Vector3
            vect1 = New Vector2(0.0, -5.0)

            resultVect = vect1.CrossProduct(vect2)
            Assert.AreEqual(0.0, resultVect.I)
            Assert.AreEqual(0.0, resultVect.J)
            Assert.AreEqual(5.0, resultVect.K)
        End Sub

        <Test()> Public Sub MethodDotProduct()
            Dim result As Double
            vect1 = New Vector2(2.0, -1.0)

            result = vect1.DotProduct(vect2)
            Assert.AreEqual(1.0, result)
        End Sub

        <Test()> Public Sub MethodNormalizedVector()
            Dim resultVect As Vector2

            vect1 = New Vector2(2.0, 0)
            resultVect = vect1.NormalizedVector()
            Assert.AreEqual(1.0, resultVect.I)
            Assert.AreEqual(0.0, resultVect.J)

            vect1 = New Vector2(0, 2.0)
            resultVect = vect1.NormalizedVector()
            Assert.AreEqual(0.0, resultVect.I)
            Assert.AreEqual(1.0, resultVect.J)
        End Sub

#End Region

    End Class

End Namespace
