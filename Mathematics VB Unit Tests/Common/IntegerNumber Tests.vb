
Imports Mathematics
Imports NUnit.Framework

Namespace Common

    <TestFixture()> Public Class IntegerNumber_Tests
        Private num1 As IntegerNumber
        Private num2 As IntegerNumber

        Public Sub New()
            MyBase.New()
        End Sub

        <SetUp()> Public Sub Init()
            num2 = New IntegerNumber()
        End Sub

#Region "  Constructor Tests  "

        <Test()> Public Sub ConstructorNew()
            num1 = New IntegerNumber()
        End Sub

        <Test()> Public Sub ConstructorNewInteger()
            num1 = New IntegerNumber(5)
            Assert.AreEqual(5, num1.Value)

            num1 = New IntegerNumber(-5)
            Assert.AreEqual(-5, num1.Value)
        End Sub

#End Region

#Region "  Property Tests  "

        <Test()> Public Sub PopertyValueGet()
            Assert.AreEqual(0, num2.Value)
        End Sub

        <Test()> Public Sub PopertyValueSet()
            num2.Value = 1
            Assert.AreEqual(1, num2.Value)

            num2.Value = -1
            Assert.AreEqual(-1, num2.Value)
        End Sub

        <Test()> Public Sub PropertyAdditiveIdentity()
            Assert.AreEqual(0, num1.AdditiveIdentity.Value)
        End Sub

        <Test()> Public Sub PropertyMultiplicativeIdentity()
            Assert.AreEqual(1, num1.MultiplicativeIdentity.Value)
        End Sub

#End Region

#Region "  Method Tests  "

        <Test()> Public Sub MethodAbsoluteValue()
            num1 = New IntegerNumber(5)
            Assert.AreEqual(5, num1.AbsoluteValue.Value)

            num1 = New IntegerNumber(-5)
            Assert.AreEqual(5, num1.AbsoluteValue.Value)
        End Sub

        <Test()> Public Sub MethodAdd()
            num1 = New IntegerNumber(5)
            Assert.AreEqual(5, num1.Add(num2).Value)

            num1 = New IntegerNumber(-5)
            Assert.AreEqual(-5, num1.Add(num2).Value)
        End Sub

        <Test()> Public Sub MethodCompareTo()
            num1 = New IntegerNumber(-5)
            Assert.Less(num1.CompareTo(num2), 0)

            num1 = New IntegerNumber(0)
            Assert.AreEqual(0, num1.CompareTo(num2))

            num1 = New IntegerNumber(5)
            Assert.Greater(num1.CompareTo(num2), 0)
        End Sub

        <Test()> Public Sub MethodEquals()
            num1 = New IntegerNumber(-5)
            Assert.AreEqual(False, num1.Equals(num2))

            num1 = New IntegerNumber(0)
            Assert.AreEqual(True, num1.Equals(num2))

            num1 = New IntegerNumber(5)
            Assert.AreEqual(False, num1.Equals(num2))
        End Sub

        <Test()> Public Sub MethodSubtract()
            num1 = New IntegerNumber(5)
            Assert.AreEqual(-5, num2.Subtract(num1).Value)

            num1 = New IntegerNumber(-5)
            Assert.AreEqual(5, num2.Subtract(num1).Value)
        End Sub

#End Region

#Region "  Operator Tests  "

        <Test()> Public Sub OperatorAddition()
            Dim returnValue As IntegerNumber

            num1 = New IntegerNumber(6)
            returnValue = num1 + num2
            Assert.AreEqual(6, returnValue.Value)

            num1 = New IntegerNumber(6)
            num2 = New IntegerNumber(-2)
            returnValue = num1 + num2
            Assert.AreEqual(4, returnValue.Value)
        End Sub

        <Test()> Public Sub OperatorSubtraction()
            Dim returnValue As IntegerNumber

            num1 = New IntegerNumber(6)
            returnValue = num1 - num2
            Assert.AreEqual(6, returnValue.Value)

            num1 = New IntegerNumber(6)
            num2 = New IntegerNumber(-2)
            returnValue = num1 - num2
            Assert.AreEqual(8, returnValue.Value)
        End Sub

        <Test()> Public Sub OperatorMultiplication()
            Dim returnValue As IntegerNumber

            num1 = New IntegerNumber(6)
            returnValue = num1 * num2
            Assert.AreEqual(0, returnValue.Value)

            num1 = New IntegerNumber(6)
            num2 = New IntegerNumber(-2)
            returnValue = num1 * num2
            Assert.AreEqual(-12, returnValue.Value)
        End Sub

        <Test()> Public Sub OperatorEquals()
            num1 = New IntegerNumber(6)
            Assert.IsFalse(num1 = num2)

            num1 = New IntegerNumber(6)
            num2 = New IntegerNumber(6)
            Assert.IsTrue(num1 = num2)
        End Sub

        <Test()> Public Sub OperatorNotEquals()
            num1 = New IntegerNumber(6)
            Assert.IsTrue(num1 <> num2)

            num1 = New IntegerNumber(6)
            num2 = New IntegerNumber(6)
            Assert.IsFalse(num1 <> num2)
        End Sub

        <Test()> Public Sub OperatorGreaterThan()
            num1 = New IntegerNumber(6)
            Assert.IsTrue(num1 > num2)

            num1 = New IntegerNumber(6)
            num2 = New IntegerNumber(6)
            Assert.IsFalse(num1 > num2)

            num1 = New IntegerNumber(4)
            num2 = New IntegerNumber(6)
            Assert.IsFalse(num1 > num2)
        End Sub

        <Test()> Public Sub OperatorLessThan()
            num1 = New IntegerNumber(6)
            Assert.IsFalse(num1 < num2)

            num1 = New IntegerNumber(6)
            num2 = New IntegerNumber(6)
            Assert.IsFalse(num1 < num2)

            num1 = New IntegerNumber(4)
            num2 = New IntegerNumber(6)
            Assert.IsTrue(num1 < num2)
        End Sub

        <Test()> Public Sub OperatorGreaterThanOrEqual()
            num1 = New IntegerNumber(6)
            Assert.IsTrue(num1 >= num2)

            num1 = New IntegerNumber(6)
            num2 = New IntegerNumber(6)
            Assert.IsTrue(num1 >= num2)

            num1 = New IntegerNumber(4)
            num2 = New IntegerNumber(6)
            Assert.IsFalse(num1 >= num2)
        End Sub

        <Test()> Public Sub OperatorLessThanOrEqual()
            num1 = New IntegerNumber(6)
            Assert.IsFalse(num1 <= num2)

            num1 = New IntegerNumber(6)
            num2 = New IntegerNumber(6)
            Assert.IsTrue(num1 <= num2)

            num1 = New IntegerNumber(4)
            num2 = New IntegerNumber(6)
            Assert.IsTrue(num1 <= num2)
        End Sub

#End Region

    End Class

End Namespace

