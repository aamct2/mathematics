
Imports Mathematics
Imports NUnit.Framework

Namespace Common

    <TestFixture()> Public Class ComplexNumber_Tests
        Private num1 As ComplexNumber
        Private num2 As ComplexNumber
        Private real0 As New RealNumber(0)
        Private real1 As New RealNumber(1)
        Private real2 As New RealNumber(2)

        Public Sub New()
            MyBase.New()
        End Sub

        <SetUp()> Public Sub Init()
            num2 = New ComplexNumber()
        End Sub

#Region "  Constructor Tests  "

        <Test()> Public Sub ConstructorNew()
            num1 = New ComplexNumber()
        End Sub

        <Test()> Public Sub ConstructorNewRealReal()
            num1 = New ComplexNumber(real1, real2)
            Assert.AreEqual(1, num1.RealPart.Value)
            Assert.AreEqual(2, num1.ImaginaryPart.Value)
        End Sub

#End Region

#Region "  Property Tests  "

        <Test()> Public Sub PopertyRealPartGet()
            Assert.AreEqual(0, num2.RealPart.Value)
        End Sub

        <Test()> Public Sub PopertyRealPartSet()
            num2.RealPart = real2
            Assert.AreEqual(2, num2.RealPart.Value)
        End Sub

        <Test()> Public Sub PopertyImaginaryPartGet()
            num2.ImaginaryPart = real1
            Assert.AreEqual(1, num2.ImaginaryPart.Value)
        End Sub

        <Test()> Public Sub PopertyImaginaryPartSet()
            num2.ImaginaryPart = real2
            Assert.AreEqual(2, num2.ImaginaryPart.Value)
        End Sub

        <Test()> Public Sub PropertyAdditiveIdentity()
            Assert.AreEqual(0.0, num1.AdditiveIdentity.RealPart.Value)
            Assert.AreEqual(0.0, num1.AdditiveIdentity.ImaginaryPart.Value)
        End Sub

        <Test()> Public Sub PropertyMultiplicativeIdentity()
            Assert.AreEqual(1.0, num1.MultiplicativeIdentity.RealPart.Value)
            Assert.AreEqual(1.0, num1.MultiplicativeIdentity.ImaginaryPart.Value)
        End Sub

#End Region

#Region "  Method Tests  "

        <Test()> Public Sub MethodAbsoluteValue()
            num1 = New ComplexNumber(real0, real2)
            Assert.AreEqual(2, num1.AbsoluteValue.RealPart.Value)
        End Sub

        <Test()> Public Sub MethodAdd()
            num1 = New ComplexNumber(real1, real2)
            Dim num3 As New ComplexNumber(real2, real0)
            Dim result As ComplexNumber

            result = num1.Add(num3)

            Assert.AreEqual(3, result.RealPart.Value)
            Assert.AreEqual(2, result.ImaginaryPart.Value)
        End Sub

        <Test()> Public Sub MethodCompareTo()
            num1 = New ComplexNumber(New RealNumber(-1), real0)
            Assert.Less(num2.CompareTo(num1), 0)

            num1 = New ComplexNumber(real0, real0)
            Assert.AreEqual(0, num1.CompareTo(num2))

            num1 = New ComplexNumber(real1, real2)
            Assert.Greater(num1.CompareTo(num2), 0)
        End Sub

        <Test()> Public Sub MethodConjugate()
            num1 = New ComplexNumber(real1, real2)
            num2 = num1.Conjugate
            Assert.AreEqual(1, num2.RealPart.Value)
            Assert.AreEqual(-2, num2.ImaginaryPart.Value)
        End Sub

        <Test()> <ExpectedException("System.DivideByZeroException")> Public Sub MethodDivideByZero()
            num1 = New ComplexNumber(real0, real0)
            num1.Divide(num2)
        End Sub

        <Test()> Public Sub MethodDivide()
            num1 = New ComplexNumber(real2, real2)
            num2 = New ComplexNumber(real1, real0)
            Assert.AreEqual(2, num1.Divide(num2).RealPart.Value)
            Assert.AreEqual(2, num1.Divide(num2).ImaginaryPart.Value)
        End Sub

        <Test()> Public Sub MethodSubtract()
            num1 = New ComplexNumber(real2, real2)
            num2 = New ComplexNumber(real1, real0)
            Assert.AreEqual(-1, num2.Subtract(num1).RealPart.Value)
            Assert.AreEqual(-2, num2.Subtract(num1).ImaginaryPart.Value)
        End Sub

        <Test()> Public Sub MethodToString()
            num1 = New ComplexNumber(real2, real2)
            Assert.AreEqual("2 + 2i", num1.ToString)
            num1 = num1.Conjugate
            Assert.AreEqual("2 - 2i", num1.ToString)
        End Sub

#End Region

#Region "  Operator Tests  "

        <Test()> Public Sub OperatorAddition()
            Dim returnValue As ComplexNumber

            num1 = New ComplexNumber(real2, real2)
            returnValue = num1 + num2
            Assert.AreEqual(2, returnValue.RealPart.Value)
            Assert.AreEqual(2, returnValue.ImaginaryPart.Value)
        End Sub

        <Test()> Public Sub OperatorSubtraction()
            Dim returnValue As ComplexNumber

            num1 = New ComplexNumber(real2, real2)
            returnValue = num2 - num1
            Assert.AreEqual(-2, returnValue.RealPart.Value)
            Assert.AreEqual(-2, returnValue.ImaginaryPart.Value)
        End Sub

        <Test()> Public Sub OperatorMultiplication()
            Dim returnValue As ComplexNumber

            num1 = New ComplexNumber(real2, real2)
            returnValue = num1 * num2
            Assert.AreEqual(0.0, returnValue.RealPart.Value)
            Assert.AreEqual(0.0, returnValue.ImaginaryPart.Value)
        End Sub

        <Test()> <ExpectedException("System.DivideByZeroException")> Public Sub OperatorDivisionByZero()
            Dim returnValue As ComplexNumber

            num1 = New ComplexNumber(real2, real2)
            returnValue = num1 / num2
        End Sub

        <Test()> Public Sub OperatorDivision()
            Dim returnValue As ComplexNumber

            num1 = New ComplexNumber(real2, real2)
            num2 = New ComplexNumber(real1, real0)
            returnValue = num1 / num2
            Assert.AreEqual(2, returnValue.RealPart.Value)
            Assert.AreEqual(2, returnValue.ImaginaryPart.Value)
        End Sub

        <Test()> Public Sub OperatorEquals()
            num1 = New ComplexNumber(real2, real2)
            Assert.IsFalse(num1 = num2)

            num1 = New ComplexNumber(real2, real2)
            num2 = New ComplexNumber(real2, real2)
            Assert.IsTrue(num1 = num2)
        End Sub

        <Test()> Public Sub OperatorNotEquals()
            num1 = New ComplexNumber(real2, real2)
            Assert.IsTrue(num1 <> num2)

            num1 = New ComplexNumber(real2, real2)
            num2 = New ComplexNumber(real2, real2)
            Assert.IsFalse(num1 <> num2)
        End Sub

#End Region

    End Class

End Namespace
