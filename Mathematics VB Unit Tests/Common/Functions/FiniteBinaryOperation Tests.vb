
Imports Mathematics
Imports Mathematics.Algebra
Imports NUnit.Framework
Imports System.Collections.Generic

Namespace Common

    Namespace Functions_Tests

        <TestFixture()> Public Class FiniteBinaryOperation_Tests
            Private Zmod2Set As New FiniteSet(Of IntegerNumber)
            Private Zmod2Addition As FiniteBinaryOperation(Of IntegerNumber)

            Private Zmod4Set As FiniteSet(Of IntegerNumber)
            Private Zmod4Multiplication As FiniteBinaryOperation(Of IntegerNumber)

            Private Dih8Group As FiniteGroup(Of SquareMatrix(Of RealNumber))

            Public Sub New()
                MyBase.New()
            End Sub

            <SetUp()> Public Sub Init()
                Zmod2Set = New FiniteSet(Of IntegerNumber)
                Zmod2Set.AddElement(New IntegerNumber(0))
                Zmod2Set.AddElement(New IntegerNumber(1))
                Zmod2Addition = New FiniteBinaryOperation(Of IntegerNumber)(Zmod2Set, New ZmodNAdditionMap(New IntegerNumber(2)))

                Zmod4Set = New FiniteSet(Of IntegerNumber)
                Zmod4Set.AddElement(New IntegerNumber(0))
                Zmod4Set.AddElement(New IntegerNumber(1))
                Zmod4Set.AddElement(New IntegerNumber(2))
                Zmod4Set.AddElement(New IntegerNumber(3))
                Zmod4Multiplication = New FiniteBinaryOperation(Of IntegerNumber)(Zmod2Set, New ZmodNMultiplicationMap(New IntegerNumber(4)))
            End Sub

            <Test()> Public Sub MethodIsAssociative()
                Assert.AreEqual(True, Zmod2Addition.IsAssociative)
            End Sub

            <Test()> Public Sub MethodIsCommutative()
                Dih8Group = Dihedral8Group()

                Assert.AreEqual(True, Zmod2Addition.IsCommutative, "Zmod2Addition.IsCommutative Test")
                Assert.AreEqual(False, Dih8Group.Operation.IsCommutative, "Dih4Group.Operation.IsCommutative Test")
            End Sub

            <Test()> Public Sub MethodHasIdentity()
                Dim int0 As New IntegerNumber(0)

                Assert.AreEqual(True, Zmod2Addition.HasIdentity)
                Assert.AreEqual(True, Zmod2Addition.Identity.Equals(int0))
            End Sub

            <Test()> Public Sub MethodHasInverses()
                Assert.AreEqual(True, Zmod2Addition.HasInverses)
                Assert.AreEqual(False, Zmod4Multiplication.HasInverses)
            End Sub
        End Class

    End Namespace

End Namespace
