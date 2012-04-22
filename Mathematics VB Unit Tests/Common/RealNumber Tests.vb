
Imports Mathematics
Imports NUnit.Framework

Namespace Common

    <TestFixture()> Public Class RealNumber_Tests
        Private num1 As RealNumber
        Private num2 As RealNumber

        Public Sub New()
            MyBase.New()
        End Sub

        <SetUp()> Public Sub Init()
            num2 = New RealNumber()
        End Sub

#Region "  Constructor Tests  "

        <Test()> Public Sub ConstructorNew()
            num1 = New RealNumber()
        End Sub

        <Test()> Public Sub ConstructorNewDouble()
            num1 = New RealNumber(5.5)
            Assert.AreEqual(5.5, num1.Value)

            num1 = New RealNumber(-5.5)
            Assert.AreEqual(-5.5, num1.Value)
        End Sub

#End Region

#Region "  Property Tests  "

        <Test()> Public Sub PopertyValueGet()
            Assert.AreEqual(0, num2.Value)
        End Sub

        <Test()> Public Sub PopertyValueSet()
            num2.Value = 1.5
            Assert.AreEqual(1.5, num2.Value)

            num2.Value = -1.5
            Assert.AreEqual(-1.5, num2.Value)
        End Sub

        <Test()> Public Sub PropertyAdditiveIdentity()
            Assert.AreEqual(0.0, num1.AdditiveIdentity.Value)
        End Sub

        <Test()> Public Sub PropertyMultiplicativeIdentity()
            Assert.AreEqual(1.0, num1.MultiplicativeIdentity.Value)
        End Sub

#End Region

#Region "  Method Tests  "

        <Test()> Public Sub MethodAbsoluteValue()
            num1 = New RealNumber(5.5)
            Assert.AreEqual(5.5, num1.AbsoluteValue.Value)

            num1 = New RealNumber(-5.5)
            Assert.AreEqual(5.5, num1.AbsoluteValue.Value)
        End Sub

        <Test()> Public Sub MethodAdd()
            num1 = New RealNumber(5.5)
            Assert.AreEqual(5.5, num1.Add(num2).Value)

            num1 = New RealNumber(-5.5)
            Assert.AreEqual(-5.5, num1.Add(num2).Value)
        End Sub

        <Test()> Public Sub MethodCompareTo()
            num1 = New RealNumber(-5.5)
            Assert.Less(num1.CompareTo(num2), 0)

            num1 = New RealNumber(0.0)
            Assert.AreEqual(0, num1.CompareTo(num2))

            num1 = New RealNumber(5.5)
            Assert.Greater(num1.CompareTo(num2), 0)
        End Sub

        <Test()> <ExpectedException("System.DivideByZeroException")> Public Sub MethodDivideByZero()
            num1 = New RealNumber(5.5)
            num1.Divide(num2)
        End Sub

        <Test()> Public Sub MethodDivide()
            num1 = New RealNumber(7.0)
            num2 = New RealNumber(2.0)
            Assert.AreEqual(3.5, num1.Divide(num2).Value)
        End Sub

        <Test()> Public Sub MethodSubtract()
            num1 = New RealNumber(5.5)
            Assert.AreEqual(-5.5, num2.Subtract(num1).Value)

            num1 = New RealNumber(-5.5)
            Assert.AreEqual(5.5, num2.Subtract(num1).Value)
        End Sub

        <Test()> Public Sub MethodToString()
            num1 = New RealNumber(8.5)
            Assert.AreEqual("8.5", num1.ToString)
        End Sub

#End Region

#Region "  Operator Tests  "

        <Test()> Public Sub OperatorAddition()
            Dim returnValue As RealNumber

            num1 = New RealNumber(6.5)
            returnValue = num1 + num2
            Assert.AreEqual(6.5, returnValue.Value)

            num1 = New RealNumber(6.5)
            num2 = New RealNumber(-2.5)
            returnValue = num1 + num2
            Assert.AreEqual(4.0, returnValue.Value)
        End Sub

        <Test()> Public Sub OperatorSubtraction()
            Dim returnValue As RealNumber

            num1 = New RealNumber(6.5)
            returnValue = num1 - num2
            Assert.AreEqual(6.5, returnValue.Value)

            num1 = New RealNumber(6.5)
            num2 = New RealNumber(-2.5)
            returnValue = num1 - num2
            Assert.AreEqual(9.0, returnValue.Value)
        End Sub

        <Test()> Public Sub OperatorMultiplication()
            Dim returnValue As RealNumber

            num1 = New RealNumber(6.5)
            returnValue = num1 * num2
            Assert.AreEqual(0.0, returnValue.Value)

            num1 = New RealNumber(6.5)
            num2 = New RealNumber(-2.5)
            returnValue = num1 * num2
            Assert.AreEqual(-16.25, returnValue.Value)
        End Sub

        <Test()> <ExpectedException("System.DivideByZeroException")> Public Sub OperatorDivisionByZero()
            Dim returnValue As RealNumber

            num1 = New RealNumber(6.5)
            returnValue = num1 / num2
        End Sub

        <Test()> Public Sub OperatorDivision()
            Dim returnValue As RealNumber

            num1 = New RealNumber(6.5)
            num2 = New RealNumber(-2.5)
            returnValue = num1 / num2
            Assert.AreEqual(-2.6, returnValue.Value)
        End Sub

        <Test()> Public Sub OperatorEquals()
            num1 = New RealNumber(6.5)
            Assert.IsFalse(num1 = num2)

            num1 = New RealNumber(6.5)
            num2 = New RealNumber(6.5)
            Assert.IsTrue(num1 = num2)
        End Sub

        <Test()> Public Sub OperatorNotEquals()
            num1 = New RealNumber(6.5)
            Assert.IsTrue(num1 <> num2)

            num1 = New RealNumber(6.5)
            num2 = New RealNumber(6.5)
            Assert.IsFalse(num1 <> num2)
        End Sub

        <Test()> Public Sub OperatorGreaterThan()
            num1 = New RealNumber(6.5)
            Assert.IsTrue(num1 > num2)

            num1 = New RealNumber(6.5)
            num2 = New RealNumber(6.5)
            Assert.IsFalse(num1 > num2)

            num1 = New RealNumber(4.5)
            num2 = New RealNumber(6.5)
            Assert.IsFalse(num1 > num2)
        End Sub

        <Test()> Public Sub OperatorLessThan()
            num1 = New RealNumber(6.5)
            Assert.IsFalse(num1 < num2)

            num1 = New RealNumber(6.5)
            num2 = New RealNumber(6.5)
            Assert.IsFalse(num1 < num2)

            num1 = New RealNumber(4.5)
            num2 = New RealNumber(6.5)
            Assert.IsTrue(num1 < num2)
        End Sub

        <Test()> Public Sub OperatorGreaterThanOrEqual()
            num1 = New RealNumber(6.5)
            Assert.IsTrue(num1 >= num2)

            num1 = New RealNumber(6.5)
            num2 = New RealNumber(6.5)
            Assert.IsTrue(num1 >= num2)

            num1 = New RealNumber(4.5)
            num2 = New RealNumber(6.5)
            Assert.IsFalse(num1 >= num2)
        End Sub

        <Test()> Public Sub OperatorLessThanOrEqual()
            num1 = New RealNumber(6.5)
            Assert.IsFalse(num1 <= num2)

            num1 = New RealNumber(6.5)
            num2 = New RealNumber(6.5)
            Assert.IsTrue(num1 <= num2)

            num1 = New RealNumber(4.5)
            num2 = New RealNumber(6.5)
            Assert.IsTrue(num1 <= num2)
        End Sub

#End Region

    End Class

End Namespace
