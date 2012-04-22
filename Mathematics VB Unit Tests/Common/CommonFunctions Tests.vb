Imports Mathematics
Imports NUnit.Framework

Namespace Common

    Namespace CommonFunctions_Tests

        <TestFixture()> Public Class RealNumberFunction_Tests
            Private num1 As RealNumber
            Private num2 As RealNumber
            Private num3 As RealNumber
            Private result As RealNumber

            Public Sub New()
                MyBase.New()
            End Sub

            <SetUp()> Public Sub Init()
                num1 = New RealNumber(1)
                num2 = New RealNumber(2)
            End Sub

            <Test()> Public Sub MethodCeiling()
                num3 = New RealNumber(3.5)
                result = CommonFunctions.Ceiling(num3)
                Assert.AreEqual(4, result.Value)
            End Sub

            <Test()> Public Sub MethodFloor()
                num3 = New RealNumber(3.5)
                result = CommonFunctions.Floor(num3)
                Assert.AreEqual(3, result.Value)
            End Sub

            <Test()> Public Sub MethodMax()
                result = CommonFunctions.Max(num1, num2)
                Assert.AreEqual(2, result.Value)
            End Sub

            <Test()> Public Sub MethodMin()
                result = CommonFunctions.Min(num1, num2)
                Assert.AreEqual(1, result.Value)
            End Sub

            <Test()> Public Sub MethodPow()
                result = CommonFunctions.Pow(num2, num2)
                Assert.AreEqual(4, result.Value)
            End Sub

            <Test()> Public Sub MethodRound()
                num3 = New RealNumber(3.5)
                result = CommonFunctions.Round(num3)
                Assert.AreEqual(4, result.Value)

                num3 = New RealNumber(3.2)
                result = CommonFunctions.Round(num3)
                Assert.AreEqual(3, result.Value)

                num3 = New RealNumber(3.7)
                result = CommonFunctions.Round(num3)
                Assert.AreEqual(4, result.Value)
            End Sub

            <Test()> Public Sub MethodSign()
                num3 = New RealNumber(4)
                result = CommonFunctions.Sign(num3)
                Assert.AreEqual(1, result.Value)

                num3 = New RealNumber(-4)
                result = CommonFunctions.Sign(num3)
                Assert.AreEqual(-1, result.Value)
            End Sub

            <Test()> Public Sub MethodSqrt()
                num3 = New RealNumber(4)
                result = CommonFunctions.Sqrt(num3)
                Assert.AreEqual(2, result.Value)
            End Sub

            <Test()> Public Sub MethodTruncate()
                num3 = New RealNumber(3.5)
                result = CommonFunctions.Truncate(num3)
                Assert.AreEqual(3, result.Value)
            End Sub

        End Class

    End Namespace

End Namespace
