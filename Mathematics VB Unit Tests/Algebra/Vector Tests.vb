
Imports Mathematics
Imports Mathematics.Algebra
Imports NUnit.Framework

Namespace Algebra

    <TestFixture()> Public Class Vector_Tests
        Private vect1 As Vector(Of RealNumber)
        Private vect2 As Vector(Of RealNumber)
        Private num1 As New RealNumber(5.5)

        Public Sub New()
            MyBase.New()
        End Sub

        <SetUp()> Public Sub Init()
            vect2 = New Vector(Of RealNumber)()
            vect2.Item(2) = num1
        End Sub

#Region "  Constructor Tests  "

        <Test()> Public Sub ConstructorNew()
            vect1 = New Vector(Of RealNumber)()
        End Sub

        <Test()> Public Sub ConstructorNewInteger()
            vect1 = New Vector(Of RealNumber)(5)
        End Sub

#End Region

#Region "  Property Tests  "

        <Test()> Public Sub PropertyCount()
            Assert.AreEqual(3, vect2.Count)
        End Sub

        <Test()> <ExpectedException("System.IndexOutOfRangeException")> Public Sub PropertyItemGetIndexOutOfRangeToBig()
            Dim num2 As RealNumber

            num2 = vect2.Item(4)
        End Sub

        <Test()> <ExpectedException("System.IndexOutOfRangeException")> Public Sub PropertyItemGetIndexOutOfRangeToSmall()
            Dim num2 As RealNumber

            num2 = vect2.Item(-1)
        End Sub

        <Test()> Public Sub PropertyItemGet()
            Assert.AreEqual(0.0, vect2.Item(1).Value)

            Assert.AreEqual(5.5, vect2.Item(2).Value)
        End Sub

        <Test()> <ExpectedException("System.ArgumentNullException")> Public Sub PropertyItemSetArgNull()
            Dim num2 As RealNumber
            vect2.Item(2) = num2
        End Sub

        <Test()> <ExpectedException("System.IndexOutOfRangeException")> Public Sub PropertyItemSetIndexOutOfRangeToBig()
            Dim num2 As New RealNumber(5.5)

            vect2.Item(4) = num2
        End Sub

        <Test()> <ExpectedException("System.IndexOutOfRangeException")> Public Sub PropertyItemSetIndexOutOfRangeToSmall()
            Dim num2 As New RealNumber(5.5)

            num2 = vect2.Item(-1)
        End Sub

        <Test()> Public Sub PropertyItemSet()
            Dim num2 As New RealNumber(-5.5)

            vect2.Item(2) = num2
            Assert.AreEqual(-5.5, vect2.Item(2).Value)
        End Sub

#End Region

    End Class

End Namespace
