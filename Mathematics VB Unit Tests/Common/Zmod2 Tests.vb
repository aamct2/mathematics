Imports Mathematics
Imports NUnit.Framework

Namespace Common

    <TestFixture()> Public Class Zmod2_Tests
        Private num1 As Zmod2
        Private num2 As Zmod2

        Public Sub New()
            MyBase.New()
        End Sub

        <SetUp()> Public Sub Init()
            num2 = New Zmod2()
        End Sub

#Region "  Constructor Tests  "

        <Test()> Public Sub ConstructorNew()
            num1 = New Zmod2()
        End Sub

        <Test()> Public Sub ConstructorNewInteger()
            num1 = New Zmod2(New IntegerNumber(1))
            Assert.AreEqual(1, num1.Value.Value)

            num1 = New Zmod2(New IntegerNumber(2))
            Assert.AreEqual(0, num1.Value.Value)
        End Sub

#End Region

#Region "  Property Tests  "

        <Test()> Public Sub PopertyValueGet()
            Assert.AreEqual(0, num2.Value.Value)
        End Sub

        <Test()> Public Sub PopertyValueSet()
            num2.Value = New IntegerNumber(1)
            Assert.AreEqual(1, num2.Value.Value)

            num2.Value = New IntegerNumber(2)
            Assert.AreEqual(0, num2.Value.Value)
        End Sub

        <Test()> Public Sub PropertyAdditiveIdentity()
            Assert.AreEqual(0, num1.AdditiveIdentity.Value.Value)
        End Sub

        <Test()> Public Sub PropertyMultiplicativeIdentity()
            Assert.AreEqual(1, num1.MultiplicativeIdentity.Value.Value)
        End Sub

#End Region

#Region "  Method Tests  "

        <Test()> Public Sub MethodAbsoluteValue()
            num1 = New Zmod2(New IntegerNumber(1))
            Assert.AreEqual(1, num1.AbsoluteValue.Value.Value)
        End Sub

        <Test()> Public Sub MethodAdd()
            num1 = New Zmod2(New IntegerNumber(1))
            Assert.AreEqual(1, num1.Add(num2).Value.Value)

            Assert.AreEqual(0, num1.Add(num1).Value.Value)
        End Sub

        <Test()> Public Sub MethodCompareTo()
            'TODO: FINISH
        End Sub

        <Test()> Public Sub MethodSubtract()
            num1 = New Zmod2(New IntegerNumber(1))
            num2 = New Zmod2(New IntegerNumber(0))
            Assert.AreEqual(1, num2.Subtract(num1).Value.Value)

            Assert.AreEqual(0, num1.Subtract(num1).Value.Value)
        End Sub

#End Region

#Region "  Operator Tests  "


#End Region

    End Class

End Namespace
