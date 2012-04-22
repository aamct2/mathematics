Imports Mathematics
Imports Mathematics.Algebra
Imports NUnit.Framework

Namespace Algebra

    Namespace FiniteStructures

        <TestFixture()> Public Class FiniteLeftModule_Tests
            Private Zmod2Field As FiniteField(Of IntegerNumber)
            Private Zmod2ScalarMult As FiniteLeftExternalBinaryOperation(Of IntegerNumber, IntegerNumber)
            Private Zmod2Module As FiniteLeftModule(Of IntegerNumber, IntegerNumber)

            Public Sub New()
                MyBase.New()

                Dim Zmod2Set As New FiniteSet(Of IntegerNumber)
                Zmod2Set.AddElement(New IntegerNumber(0))
                Zmod2Set.AddElement(New IntegerNumber(1))
                Dim Zmod2Addition As New FiniteBinaryOperation(Of IntegerNumber)(Zmod2Set, New ZmodNAdditionMap(New IntegerNumber(2)))
                Dim Zmod2Multiplication As New FiniteBinaryOperation(Of IntegerNumber)(Zmod2Set, New ZmodNMultiplicationMap(New IntegerNumber(2)))
                Zmod2Field = New FiniteField(Of IntegerNumber)(Zmod2Set, Zmod2Addition, Zmod2Multiplication)


                Zmod2ScalarMult = New FiniteLeftExternalBinaryOperation(Of IntegerNumber, IntegerNumber)(Zmod2Field.theSet, Zmod2Field.theSet, Zmod2Field.MultiplicationOperation.theRelation)
                Zmod2Module = New FiniteLeftModule(Of IntegerNumber, IntegerNumber)(Zmod2Field.theSet, Zmod2Field.AdditionOperation, Zmod2ScalarMult, Zmod2Field)
            End Sub

            <SetUp()> Public Sub Init()

            End Sub

#Region "  Constructor Tests  "

            <Test()> Public Sub ConstructorNew()
                Zmod2Module = New FiniteLeftModule(Of IntegerNumber, IntegerNumber)(Zmod2Field.theSet, Zmod2Field.AdditionOperation, Zmod2ScalarMult, Zmod2Field)
            End Sub

#End Region

#Region "  Property Tests  "

            <Test()> Public Sub PropertyAdditiveIdentity()
                Assert.AreEqual(True, Zmod2Module.AdditiveIdentity.Equals(New IntegerNumber(0)))
            End Sub

            <Test()> Public Sub PropertyTheSet()
                Assert.AreEqual(True, Zmod2Module.theSet.Equals(Zmod2Field.theSet))
            End Sub

#End Region

#Region "  Method Tests  "



#End Region

        End Class

    End Namespace

End Namespace
