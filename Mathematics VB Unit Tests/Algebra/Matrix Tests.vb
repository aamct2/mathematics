
Imports Mathematics
Imports Mathematics.Algebra
Imports NUnit.Framework

Namespace Algebra

    <TestFixture()> Public Class Matrix_Tests
        Private matrix1 As Matrix(Of RealNumber)
        Private matrix2 As Matrix(Of RealNumber)
        Private num1 As RealNumber
        Private A As Integer
        Private B As Integer

        Public Sub New()
            MyBase.New()
        End Sub

        <SetUp()> Public Sub Init()
            matrix2 = New Matrix(Of RealNumber)()

            For A = 0 To 2
                For B = 0 To 2
                    matrix2.Item(A, B) = New RealNumber(A)
                Next B
            Next A
        End Sub

#Region "  Constructor Tests  "

        <Test()> Public Sub ConstructorNew()
            matrix1 = New Matrix(Of RealNumber)()
        End Sub

        <Test()> Public Sub ConstructorNewInteger()
            matrix1 = New Matrix(Of RealNumber)(5, 5)
        End Sub

#End Region

#Region "  Property Tests  "

        <Test()> Public Sub PropertyCount()
            Assert.AreEqual(9, matrix2.Count)
        End Sub

        <Test()> Public Sub PropertyWidth()
            Assert.AreEqual(3, matrix2.Width)
        End Sub

        <Test()> Public Sub PropertyHieght()
            Assert.AreEqual(3, matrix2.Height)
        End Sub

        <Test()> <ExpectedException("System.ArgumentException")> Public Sub PropertyItemGetRowGreaterThanHeight()
            num1 = matrix2.Item(5, 0)
        End Sub

        <Test()> <ExpectedException("System.ArgumentException")> Public Sub PropertyItemGetColumnGreaterThanWidth()
            num1 = matrix2.Item(0, 5)
        End Sub

        <Test()> Public Sub PropertyItemGet()
            Assert.AreEqual(1, matrix2.Item(1, 1).Value)
        End Sub

        <Test()> <ExpectedException("System.ArgumentNullException")> Public Sub PropertyItemSetArgNull()
            matrix2.Item(1, 1) = num1
        End Sub

        <Test()> Public Sub PropertyItemSet()
            Dim num2 As RealNumber

            num2 = New RealNumber(5.5)
            matrix2.Item(1, 1) = num2
            Assert.AreEqual(5.5, matrix2.Item(1, 1).Value)
        End Sub

#End Region

#Region "  Method Tests  "

        <Test()> <ExpectedException("System.ArgumentNullException")> Public Sub MethodAddArgNull()
            Dim matrix3 As Matrix(Of RealNumber)

            matrix2.Add(matrix3)
        End Sub

        <Test()> <ExpectedException("System.ArgumentException")> Public Sub MethodAddWidthsNotEqual()
            Dim matrix3 As New Matrix(Of RealNumber)(3, 5)

            matrix2.Add(matrix3)
        End Sub

        <Test()> <ExpectedException("System.ArgumentException")> Public Sub MethodAddHeightsNotEqual()
            Dim matrix3 As New Matrix(Of RealNumber)(5, 3)

            matrix2.Add(matrix3)
        End Sub

        <Test()> Public Sub MethodAdd()
            Dim matrix3 As New Matrix(Of RealNumber)()
            Dim returnValue As Matrix(Of RealNumber)

            returnValue = matrix2.Add(matrix3)

            For A = 0 To 2
                For B = 0 To 2
                    Assert.AreEqual(matrix2.Item(A, B).Value, returnValue.Item(A, B).Value)
                Next B
            Next A
        End Sub

        <Test()> Public Sub MethodClone()
            Dim matrix3 As Matrix(Of RealNumber) = matrix2.Clone

            For A = 0 To 2
                For B = 0 To 2
                    Assert.AreEqual(matrix2.Item(A, B).Value, matrix3.Item(A, B).Value)
                Next B
            Next A
        End Sub

        <Test()> <ExpectedException("System.ArgumentNullException")> Public Sub MethodHadamardProductArgNull()
            Dim matrix3 As Matrix(Of RealNumber)

            matrix2.HadamardProduct(matrix3)
        End Sub

        <Test()> <ExpectedException("System.ArgumentException")> Public Sub MethodHadamardProductWidthsDontMatch()
            Dim matrix3 As New Matrix(Of RealNumber)(3, 4)

            matrix2.HadamardProduct(matrix3)
        End Sub

        <Test()> <ExpectedException("System.ArgumentException")> Public Sub MethodHadamardProductHeightsDontMatch()
            Dim matrix3 As New Matrix(Of RealNumber)(4, 3)

            matrix2.HadamardProduct(matrix3)
        End Sub

        <Test()> Public Sub MethodHadamardProduct()
            Dim matrix3 As New Matrix(Of RealNumber)(3, 3)
            Dim matrix4 As New Matrix(Of RealNumber)(3, 3)
            Dim matrix5 As New Matrix(Of RealNumber)(3, 3)
            Dim resultMatrix As Matrix(Of RealNumber)

            With matrix3
                .Item(0, 0) = New RealNumber(1)
                .Item(0, 1) = New RealNumber(3)
                .Item(0, 2) = New RealNumber(2)
                .Item(1, 0) = New RealNumber(1)
                .Item(1, 1) = New RealNumber(0)
                .Item(1, 2) = New RealNumber(0)
                .Item(2, 0) = New RealNumber(1)
                .Item(2, 1) = New RealNumber(2)
                .Item(2, 2) = New RealNumber(2)
            End With

            With matrix4
                .Item(0, 0) = New RealNumber(0)
                .Item(0, 1) = New RealNumber(0)
                .Item(0, 2) = New RealNumber(2)
                .Item(1, 0) = New RealNumber(7)
                .Item(1, 1) = New RealNumber(5)
                .Item(1, 2) = New RealNumber(0)
                .Item(2, 0) = New RealNumber(2)
                .Item(2, 1) = New RealNumber(1)
                .Item(2, 2) = New RealNumber(1)
            End With

            With matrix5
                .Item(0, 0) = New RealNumber(0)
                .Item(0, 1) = New RealNumber(0)
                .Item(0, 2) = New RealNumber(4)
                .Item(1, 0) = New RealNumber(7)
                .Item(1, 1) = New RealNumber(0)
                .Item(1, 2) = New RealNumber(0)
                .Item(2, 0) = New RealNumber(2)
                .Item(2, 1) = New RealNumber(2)
                .Item(2, 2) = New RealNumber(2)
            End With

            resultMatrix = matrix3.HadamardProduct(matrix4)

            For A = 0 To 1
                For B = 0 To 1
                    'Console.WriteLine("(" & A & "," & B & ") = " & resultMatrix.Item(A, B).Value)
                    Assert.AreEqual(matrix5.Item(A, B).Value, resultMatrix.Item(A, B).Value)
                Next B
            Next A
        End Sub

        <Test()> Public Sub MethodIsSquare()
            Dim matrix4 As New Matrix(Of RealNumber)(3, 4)

            Assert.IsFalse(matrix4.IsSquare)
            Assert.IsTrue(matrix2.IsSquare)
        End Sub

        <Test()> <ExpectedException("System.ArgumentNullException")> Public Sub MethodMatrixMultiplyArgNull()
            Dim matrix3 As Matrix(Of RealNumber)

            matrix2.HadamardProduct(matrix3)
        End Sub

        <Test()> <ExpectedException("System.ArgumentException")> Public Sub MethodMatrixMultiplyWidthHeightDontMatch()
            Dim matrix3 As New Matrix(Of RealNumber)(4, 3)

            matrix2.HadamardProduct(matrix3)
        End Sub

        <Test()> Public Sub MethodMultiply()
            Dim matrix3 As New Matrix(Of RealNumber)(2, 3)
            Dim matrix4 As New Matrix(Of RealNumber)(3, 2)
            Dim matrix5 As New Matrix(Of RealNumber)(2, 2)
            Dim resultMatrix As New Matrix(Of RealNumber)

            With matrix3
                .Item(0, 0) = New RealNumber(1)
                .Item(0, 1) = New RealNumber(0)
                .Item(0, 2) = New RealNumber(2)
                .Item(1, 0) = New RealNumber(-1)
                .Item(1, 1) = New RealNumber(3)
                .Item(1, 2) = New RealNumber(1)
            End With

            With matrix4
                .Item(0, 0) = New RealNumber(3)
                .Item(0, 1) = New RealNumber(1)
                .Item(1, 0) = New RealNumber(2)
                .Item(1, 1) = New RealNumber(1)
                .Item(2, 0) = New RealNumber(1)
                .Item(2, 1) = New RealNumber(0)
            End With

            With matrix5
                .Item(0, 0) = New RealNumber(5)
                .Item(0, 1) = New RealNumber(1)
                .Item(1, 0) = New RealNumber(4)
                .Item(1, 1) = New RealNumber(2)
            End With

            resultMatrix = matrix3.Multiply(matrix4)

            For A = 0 To 1
                For B = 0 To 1
                    'Console.WriteLine("(" & A & "," & B & ") = " & resultMatrix.Item(A, B).Value)
                    Assert.AreEqual(matrix5.Item(A, B).Value, resultMatrix.Item(A, B).Value)
                Next B
            Next A
        End Sub

        <Test()> Public Sub MethodMinor()
            Dim matrix3 As New Matrix(Of RealNumber)(3, 3)
            Dim matrix4 As Matrix(Of RealNumber)
            Dim matrix5 As New Matrix(Of RealNumber)(2, 2)

            With matrix3
                .Item(0, 0) = New RealNumber(-2)
                .Item(0, 1) = New RealNumber(2)
                .Item(0, 2) = New RealNumber(-3)
                .Item(1, 0) = New RealNumber(-1)
                .Item(1, 1) = New RealNumber(1)
                .Item(1, 2) = New RealNumber(3)
                .Item(2, 0) = New RealNumber(2)
                .Item(2, 1) = New RealNumber(0)
                .Item(2, 2) = New RealNumber(-1)
            End With

            With matrix5
                .Item(0, 0) = New RealNumber(-1)
                .Item(0, 1) = New RealNumber(3)
                .Item(1, 0) = New RealNumber(2)
                .Item(1, 1) = New RealNumber(-1)
            End With

            matrix4 = matrix3.Minor(0, 1)

            For A = 0 To 1
                For B = 0 To 1
                    'Console.WriteLine("(" & A & "," & B & ") = " & matrix4.Item(A, B).Value)
                    Assert.AreEqual(matrix5.Item(A, B).Value, matrix4.Item(A, B).Value)
                Next B
            Next A
        End Sub

        <Test()> <ExpectedException("System.ArgumentNullException")> Public Sub MethodScalarMultiplyArgNull()
            Dim matrix3 As New Matrix(Of RealNumber)
            Dim scalar As RealNumber
            Dim resultMatrix As Matrix(Of RealNumber)

            resultMatrix = matrix3.ScalarMultiply(scalar)
        End Sub

        <Test()> Public Sub MethodScalarMultiply()
            Dim matrix3 As New Matrix(Of RealNumber)(3, 3)
            Dim matrix4 As New Matrix(Of RealNumber)(3, 3)
            Dim scalar As New RealNumber(2)
            Dim resultMatrix As New Matrix(Of RealNumber)

            With matrix3
                .Item(0, 0) = New RealNumber(1)
                .Item(0, 1) = New RealNumber(3)
                .Item(0, 2) = New RealNumber(2)
                .Item(1, 0) = New RealNumber(1)
                .Item(1, 1) = New RealNumber(0)
                .Item(1, 2) = New RealNumber(0)
                .Item(2, 0) = New RealNumber(1)
                .Item(2, 1) = New RealNumber(2)
                .Item(2, 2) = New RealNumber(2)
            End With

            With matrix4
                .Item(0, 0) = New RealNumber(2)
                .Item(0, 1) = New RealNumber(6)
                .Item(0, 2) = New RealNumber(4)
                .Item(1, 0) = New RealNumber(2)
                .Item(1, 1) = New RealNumber(0)
                .Item(1, 2) = New RealNumber(0)
                .Item(2, 0) = New RealNumber(2)
                .Item(2, 1) = New RealNumber(4)
                .Item(2, 2) = New RealNumber(4)
            End With

            resultMatrix = matrix3.ScalarMultiply(scalar)

            For A = 0 To 2
                For B = 0 To 2
                    'Console.WriteLine("(" & A & "," & B & ") = " & resultMatrix.Item(A, B).Value)
                    Assert.AreEqual(matrix4.Item(A, B).Value, resultMatrix.Item(A, B).Value)
                Next B
            Next A
        End Sub

        <Test()> <ExpectedException("System.ArgumentNullException")> Public Sub MethodSubtractArgNull()
            Dim matrix3 As Matrix(Of RealNumber)

            matrix2.Subtract(matrix3)
        End Sub

        <Test()> <ExpectedException("System.ArgumentException")> Public Sub MethodSubtractWidthsNotEqual()
            Dim matrix3 As New Matrix(Of RealNumber)(3, 5)

            matrix2.Subtract(matrix3)
        End Sub

        <Test()> <ExpectedException("System.ArgumentException")> Public Sub MethodSubtractHeightsNotEqual()
            Dim matrix3 As New Matrix(Of RealNumber)(5, 3)

            matrix2.Subtract(matrix3)
        End Sub

        <Test()> Public Sub MethodSubtract()
            Dim matrix3 As New Matrix(Of RealNumber)(3, 3)
            Dim matrix4 As New Matrix(Of RealNumber)(3, 3)
            Dim matrix5 As New Matrix(Of RealNumber)(3, 3)
            Dim resultMatrix As Matrix(Of RealNumber)

            With matrix3
                .Item(0, 0) = New RealNumber(1)
                .Item(0, 1) = New RealNumber(3)
                .Item(0, 2) = New RealNumber(2)
                .Item(1, 0) = New RealNumber(1)
                .Item(1, 1) = New RealNumber(0)
                .Item(1, 2) = New RealNumber(0)
                .Item(2, 0) = New RealNumber(1)
                .Item(2, 1) = New RealNumber(2)
                .Item(2, 2) = New RealNumber(2)
            End With

            With matrix4
                .Item(0, 0) = New RealNumber(2)
                .Item(0, 1) = New RealNumber(6)
                .Item(0, 2) = New RealNumber(4)
                .Item(1, 0) = New RealNumber(2)
                .Item(1, 1) = New RealNumber(0)
                .Item(1, 2) = New RealNumber(0)
                .Item(2, 0) = New RealNumber(2)
                .Item(2, 1) = New RealNumber(4)
                .Item(2, 2) = New RealNumber(4)
            End With

            With matrix5
                .Item(0, 0) = New RealNumber(-1)
                .Item(0, 1) = New RealNumber(-3)
                .Item(0, 2) = New RealNumber(-2)
                .Item(1, 0) = New RealNumber(-1)
                .Item(1, 1) = New RealNumber(0)
                .Item(1, 2) = New RealNumber(0)
                .Item(2, 0) = New RealNumber(-1)
                .Item(2, 1) = New RealNumber(-2)
                .Item(2, 2) = New RealNumber(-2)
            End With

            resultMatrix = matrix3.Subtract(matrix4)

            For A = 0 To 2
                For B = 0 To 2
                    'Console.WriteLine("(" & A & "," & B & ") = " & resultMatrix.Item(A, B).Value)
                    Assert.AreEqual(matrix5.Item(A, B).Value, resultMatrix.Item(A, B).Value)
                Next B
            Next A
        End Sub

#End Region

#Region "  Operator Tests  "

        <Test()> Public Sub OperatorAddition()
            Dim matrix3 As New Matrix(Of RealNumber)(3, 3)
            Dim matrix4 As New Matrix(Of RealNumber)(3, 3)
            Dim matrix5 As New Matrix(Of RealNumber)(3, 3)
            Dim resultMatrix As Matrix(Of RealNumber)

            With matrix3
                .Item(0, 0) = New RealNumber(1)
                .Item(0, 1) = New RealNumber(3)
                .Item(0, 2) = New RealNumber(2)
                .Item(1, 0) = New RealNumber(1)
                .Item(1, 1) = New RealNumber(0)
                .Item(1, 2) = New RealNumber(0)
                .Item(2, 0) = New RealNumber(1)
                .Item(2, 1) = New RealNumber(2)
                .Item(2, 2) = New RealNumber(2)
            End With

            With matrix4
                .Item(0, 0) = New RealNumber(2)
                .Item(0, 1) = New RealNumber(6)
                .Item(0, 2) = New RealNumber(4)
                .Item(1, 0) = New RealNumber(2)
                .Item(1, 1) = New RealNumber(0)
                .Item(1, 2) = New RealNumber(0)
                .Item(2, 0) = New RealNumber(2)
                .Item(2, 1) = New RealNumber(4)
                .Item(2, 2) = New RealNumber(4)
            End With

            With matrix5
                .Item(0, 0) = New RealNumber(3)
                .Item(0, 1) = New RealNumber(9)
                .Item(0, 2) = New RealNumber(6)
                .Item(1, 0) = New RealNumber(3)
                .Item(1, 1) = New RealNumber(0)
                .Item(1, 2) = New RealNumber(0)
                .Item(2, 0) = New RealNumber(3)
                .Item(2, 1) = New RealNumber(6)
                .Item(2, 2) = New RealNumber(6)
            End With

            resultMatrix = matrix3 + matrix4

            For A = 0 To 2
                For B = 0 To 2
                    'Console.WriteLine("(" & A & "," & B & ") = " & resultMatrix.Item(A, B).Value)
                    Assert.AreEqual(matrix5.Item(A, B).Value, resultMatrix.Item(A, B).Value)
                Next B
            Next A
        End Sub

        <Test()> Public Sub OperatorSubtraction()
            Dim matrix3 As New Matrix(Of RealNumber)(3, 3)
            Dim matrix4 As New Matrix(Of RealNumber)(3, 3)
            Dim matrix5 As New Matrix(Of RealNumber)(3, 3)
            Dim resultMatrix As Matrix(Of RealNumber)

            With matrix3
                .Item(0, 0) = New RealNumber(1)
                .Item(0, 1) = New RealNumber(3)
                .Item(0, 2) = New RealNumber(2)
                .Item(1, 0) = New RealNumber(4)
                .Item(1, 1) = New RealNumber(5)
                .Item(1, 2) = New RealNumber(6)
                .Item(2, 0) = New RealNumber(1)
                .Item(2, 1) = New RealNumber(2)
                .Item(2, 2) = New RealNumber(2)
            End With

            With matrix4
                .Item(0, 0) = New RealNumber(2)
                .Item(0, 1) = New RealNumber(6)
                .Item(0, 2) = New RealNumber(4)
                .Item(1, 0) = New RealNumber(7)
                .Item(1, 1) = New RealNumber(8)
                .Item(1, 2) = New RealNumber(9)
                .Item(2, 0) = New RealNumber(2)
                .Item(2, 1) = New RealNumber(1)
                .Item(2, 2) = New RealNumber(4)
            End With

            With matrix5
                .Item(0, 0) = New RealNumber(-1)
                .Item(0, 1) = New RealNumber(-3)
                .Item(0, 2) = New RealNumber(-2)
                .Item(1, 0) = New RealNumber(-3)
                .Item(1, 1) = New RealNumber(-3)
                .Item(1, 2) = New RealNumber(-3)
                .Item(2, 0) = New RealNumber(-1)
                .Item(2, 1) = New RealNumber(1)
                .Item(2, 2) = New RealNumber(-2)
            End With

            resultMatrix = matrix3 - matrix4

            For A = 0 To 2
                For B = 0 To 2
                    'Console.WriteLine("(" & A & "," & B & ") = " & resultMatrix.Item(A, B).Value)
                    Assert.AreEqual(matrix5.Item(A, B).Value, resultMatrix.Item(A, B).Value)
                Next B
            Next A
        End Sub

        <Test()> Public Sub OperatorScalarMultiply()
            Dim matrix3 As New Matrix(Of RealNumber)(3, 3)
            Dim matrix4 As New Matrix(Of RealNumber)(3, 3)
            Dim resultMatrix As Matrix(Of RealNumber)
            Dim scalar As New RealNumber(2)

            With matrix3
                .Item(0, 0) = New RealNumber(1)
                .Item(0, 1) = New RealNumber(3)
                .Item(0, 2) = New RealNumber(2)
                .Item(1, 0) = New RealNumber(4)
                .Item(1, 1) = New RealNumber(5)
                .Item(1, 2) = New RealNumber(6)
                .Item(2, 0) = New RealNumber(1)
                .Item(2, 1) = New RealNumber(2)
                .Item(2, 2) = New RealNumber(2)
            End With

            With matrix4
                .Item(0, 0) = New RealNumber(2)
                .Item(0, 1) = New RealNumber(6)
                .Item(0, 2) = New RealNumber(4)
                .Item(1, 0) = New RealNumber(8)
                .Item(1, 1) = New RealNumber(10)
                .Item(1, 2) = New RealNumber(12)
                .Item(2, 0) = New RealNumber(2)
                .Item(2, 1) = New RealNumber(4)
                .Item(2, 2) = New RealNumber(4)
            End With

            resultMatrix = scalar * matrix3

            For A = 0 To 2
                For B = 0 To 2
                    'Console.WriteLine("(" & A & "," & B & ") = " & resultMatrix.Item(A, B).Value)
                    Assert.AreEqual(matrix4.Item(A, B).Value, resultMatrix.Item(A, B).Value)
                Next B
            Next A
        End Sub

        <Test()> Public Sub OperatorMatrixMultiply()
            Dim matrix3 As New Matrix(Of RealNumber)(3, 3)
            Dim matrix4 As New Matrix(Of RealNumber)(3, 3)
            Dim matrix5 As New Matrix(Of RealNumber)(3, 3)
            Dim resultMatrix As Matrix(Of RealNumber)

            With matrix3
                .Item(0, 0) = New RealNumber(1)
                .Item(0, 1) = New RealNumber(3)
                .Item(0, 2) = New RealNumber(2)
                .Item(1, 0) = New RealNumber(4)
                .Item(1, 1) = New RealNumber(5)
                .Item(1, 2) = New RealNumber(6)
                .Item(2, 0) = New RealNumber(1)
                .Item(2, 1) = New RealNumber(2)
                .Item(2, 2) = New RealNumber(2)
            End With

            With matrix4
                .Item(0, 0) = New RealNumber(1)
                .Item(0, 1) = New RealNumber(0)
                .Item(0, 2) = New RealNumber(0)
                .Item(1, 0) = New RealNumber(0)
                .Item(1, 1) = New RealNumber(1)
                .Item(1, 2) = New RealNumber(0)
                .Item(2, 0) = New RealNumber(0)
                .Item(2, 1) = New RealNumber(0)
                .Item(2, 2) = New RealNumber(1)
            End With

            resultMatrix = matrix3 * matrix4

            For A = 0 To 2
                For B = 0 To 2
                    'Console.WriteLine("(" & A & "," & B & ") = " & resultMatrix.Item(A, B).Value)
                    Assert.AreEqual(matrix3.Item(A, B).Value, resultMatrix.Item(A, B).Value)
                Next B
            Next A
        End Sub

#End Region

    End Class

End Namespace
