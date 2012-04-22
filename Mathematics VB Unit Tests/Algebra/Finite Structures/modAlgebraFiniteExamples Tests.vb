
Imports Mathematics
Imports Mathematics.Algebra
Imports NUnit.Framework

Namespace Algebra

    Namespace FiniteStructures

        <TestFixture()> Public Class modAlgebraFiniteExamples_Tests
            Public Sub New()
                MyBase.New()
            End Sub

            <SetUp()> Public Sub Init()

            End Sub

            <Test()> Public Sub Zmod2Field()
                Dim testField As FiniteField(Of IntegerNumber)

                testField = Mathematics.Algebra.Zmod2Field
            End Sub
        End Class

    End Namespace

End Namespace