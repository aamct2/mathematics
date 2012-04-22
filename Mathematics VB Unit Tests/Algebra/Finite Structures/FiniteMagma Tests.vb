
Imports Mathematics
Imports Mathematics.Algebra
Imports NUnit.Framework

Namespace Algebra

    Namespace FiniteStructures

        <TestFixture()> Public Class FiniteMagma_Tests
            Private Zmod2Set As New FiniteSet(Of IntegerNumber)
            Private Zmod2Addition As FiniteBinaryOperation(Of IntegerNumber)
            Private Zmod2Magma As FiniteMagma(Of IntegerNumber)

            Public Sub New()
                MyBase.New()
            End Sub

            <SetUp()> Public Sub Init()
                Zmod2Set.AddElement(New IntegerNumber(0))
                Zmod2Set.AddElement(New IntegerNumber(1))
                Zmod2Addition = New FiniteBinaryOperation(Of IntegerNumber)(Zmod2Set, New ZmodNAdditionMap(New IntegerNumber(2)))
                Zmod2Magma = New FiniteMagma(Of IntegerNumber)(Zmod2Set, Zmod2Addition)
            End Sub

#Region "  Constructor Tests  "

            <Test()> Public Sub ConstructorNew()
                Zmod2Magma = New FiniteMagma(Of IntegerNumber)(Zmod2Set, Zmod2Addition)
            End Sub

#End Region

#Region "  Property Tests  "

            <Test()> Public Sub PropertyTheSet()
                Assert.AreEqual(True, Zmod2Magma.theSet.Equals(Zmod2Set))
            End Sub

            <Test()> Public Sub PropertyOperation()
                Assert.AreEqual(True, Zmod2Magma.Operation.Equals(Zmod2Addition))
            End Sub

#End Region

#Region "  Method Tests  "

            <Test()> Public Sub MethodApplyOperation()
                Dim curTup As New Tuple(2)

                curTup.Element(0) = New IntegerNumber(1)
                curTup.Element(1) = New IntegerNumber(1)

                Assert.AreEqual(True, Zmod2Magma.ApplyOperation(curTup).Equals(New IntegerNumber(0)))
            End Sub

            <Test()> Public Sub MethodSetOfSquareElements()
                Dim testSet As New FiniteSet(Of IntegerNumber)

                testSet.AddElement(New IntegerNumber(0))

                Assert.AreEqual(True, Zmod2Magma.SetOfSquareElements.Equals(testSet))
            End Sub

#End Region

        End Class

    End Namespace

End Namespace

