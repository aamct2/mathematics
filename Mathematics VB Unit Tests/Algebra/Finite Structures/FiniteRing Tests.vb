
Imports Mathematics
Imports Mathematics.Algebra
Imports NUnit.Framework

Namespace Algebra

    Namespace FiniteStructures

        <TestFixture()> Public Class FiniteRing_Tests
            Private Zmod2Ring As FiniteRing(Of IntegerNumber)

            Public Sub New()
                MyBase.New()

                Dim Zmod2Set As New FiniteSet(Of IntegerNumber)
                Zmod2Set.AddElement(New IntegerNumber(0))
                Zmod2Set.AddElement(New IntegerNumber(1))
                Dim Zmod2Addition As New FiniteBinaryOperation(Of IntegerNumber)(Zmod2Set, New ZmodNAdditionMap(New IntegerNumber(2)))
                Dim Zmod2Multiplication As New FiniteBinaryOperation(Of IntegerNumber)(Zmod2Set, New ZmodNMultiplicationMap(New IntegerNumber(2)))
                Zmod2Ring = New FiniteRing(Of IntegerNumber)(Zmod2Set, Zmod2Addition, Zmod2Multiplication)
            End Sub

            <SetUp()> Public Sub Init()

            End Sub

#Region "  Method Tests  "

            <Test()> Public Sub MethodCenter()
                Assert.AreEqual(True, Zmod2Ring.Center.Equals(Zmod2Ring.theSet))
            End Sub

            <Test()> Public Sub MethodCenterRing()
                Assert.AreEqual(True, Zmod2Ring.CenterRing.Equals(Zmod2Ring))
            End Sub

            <Test()> Public Sub MethodDirectProduct()
                Dim Z2xZ2 As FiniteRing(Of Tuple) = Zmod2Ring.DirectProduct(Of IntegerNumber)(Zmod2Ring)
            End Sub

            <Test()> Public Sub MethodIsBoolean()
                Dim Z2xZ2 As FiniteRing(Of Tuple) = Zmod2Ring.DirectProduct(Of IntegerNumber)(Zmod2Ring)

                Assert.AreEqual(True, Z2xZ2.IsBoolean)
            End Sub

            <Test()> Public Sub MethodIsLeftIdeal()
                Assert.AreEqual(True, Zmod2Ring.IsLeftIdeal(Zmod2Ring.theSet), "whole ring is left ideal test")
                Assert.AreEqual(True, Zmod2Ring.IsLeftIdeal(Zmod2Ring.TrivialSubring.theSet), "{0} is left ideal test")
            End Sub

            <Test()> Public Sub MethodIsSimple()
                Assert.AreEqual(True, Zmod2Ring.IsSimple())
            End Sub

            <Test()> Public Sub MethodIsSubringOf()
                Assert.AreEqual(True, Zmod2Ring.TrivialSubring.IsSubringOf(Zmod2Ring))
            End Sub

            <Test()> Public Sub MethodIsTwoSidedIdeal()
                Assert.AreEqual(True, Zmod2Ring.IsLeftIdeal(Zmod2Ring.theSet), "Zmod2 is two-sided ideal of Zmod2 test")
                Assert.AreEqual(True, Zmod2Ring.IsLeftIdeal(Zmod2Ring.TrivialSubring.theSet), "{0} is two-sided ideal Zmod2 test")
            End Sub

            <Test()> Public Sub MethodTrivialSubring()
                Dim trivRing As FiniteRing(Of IntegerNumber) = Zmod2Ring.TrivialSubring

            End Sub

#End Region

        End Class

    End Namespace

End Namespace
