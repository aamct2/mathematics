
Imports Mathematics
Imports Mathematics.Algebra
Imports NUnit.Framework

Namespace Algebra

    Namespace FiniteStructures

        <TestFixture()> Public Class FiniteSemigroup_Tests
            Private Zmod2Set As New FiniteSet(Of IntegerNumber)
            Private Zmod2Addition As FiniteBinaryOperation(Of IntegerNumber)

            Public Sub New()
                MyBase.New()
            End Sub

            <SetUp()> Public Sub Init()
                Zmod2Set.AddElement(New IntegerNumber(0))
                Zmod2Set.AddElement(New IntegerNumber(1))
                Zmod2Addition = New FiniteBinaryOperation(Of IntegerNumber)(Zmod2Set, New ZmodNAdditionMap(New IntegerNumber(2)))
            End Sub

            <Test()> Public Sub ConstructorNew()
                Dim Zmod2Semigroup As New FiniteSemigroup(Of IntegerNumber)(Zmod2Set, Zmod2Addition)
            End Sub
        End Class

    End Namespace

End Namespace

