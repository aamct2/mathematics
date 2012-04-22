

Imports Mathematics
Imports Mathematics.Algebra
Imports NUnit.Framework

Namespace Algebra

    <TestFixture()> Public Class SquareMatrix_Tests
        Private matrix1 As SquareMatrix(Of RealNumber)
        Private matrix2 As SquareMatrix(Of RealNumber)
        Private num1 As RealNumber
        Private A As Integer
        Private B As Integer

        Public Sub New()
            MyBase.New()
        End Sub

        <SetUp()> Public Sub Init()
            matrix2 = New SquareMatrix(Of RealNumber)()

            For A = 0 To 2
                For B = 0 To 2
                    matrix2.Item(A, B) = New RealNumber(A)
                Next B
            Next A
        End Sub

#Region "  Method Tests  "

        <Test()> Public Sub MethodDeterminant()
            Dim det As RealNumber
            Dim matrix3 As New SquareMatrix(Of RealNumber)(3)

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

            det = matrix3.Determinant()
            Assert.AreEqual(18, det.Value, "3x3 Matrix Determinant Test")

            Dim mat2 As New SquareMatrix(Of RealNumber)(2)
            Dim idt2 As SquareMatrix(Of RealNumber) = mat2.MultiplicativeIdentity
            Console.Out.WriteLine(idt2.ToString)
            Assert.AreEqual(1, idt2.Determinant.Value, "2x2 Identity Matrix Determinant Test")

            Dim mat4 As New SquareMatrix(Of RealNumber)(4)
            Dim idt4 As SquareMatrix(Of RealNumber) = mat4.MultiplicativeIdentity
            Console.Out.WriteLine(idt4.ToString)
            Assert.AreEqual(1, idt4.Determinant.Value, "4x4 Identity Matrix Determinant Test")
        End Sub

        <Test()> Public Sub MethodIdentityMatrix()
            Dim matrix3 As New SquareMatrix(Of RealNumber)
            Dim ident As SquareMatrix(Of RealNumber)


            With matrix3
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

            ident = matrix2.MultiplicativeIdentity()

            For A = 0 To 2
                For B = 0 To 2
                    'Console.WriteLine("(" & A & "," & B & ") = " & ident.Item(A, B).Value)
                    Assert.AreEqual(matrix3.Item(A, B).Value, ident.Item(A, B).Value)
                Next B
            Next A
        End Sub

        <Test()> Public Sub MethodInverse2x2()
            Dim matrix3 As New SquareMatrix(Of RealNumber)(2)
            Dim matrix4 As New SquareMatrix(Of RealNumber)(2)
            Dim resultMatrix As SquareMatrix(Of RealNumber)

            With matrix3
                .Item(0, 0) = New RealNumber(1)
                .Item(0, 1) = New RealNumber(-1)
                .Item(1, 0) = New RealNumber(-1)
                .Item(1, 1) = New RealNumber(-1)
            End With

            With matrix4
                .Item(0, 0) = New RealNumber(0.5)
                .Item(0, 1) = New RealNumber(-0.5)
                .Item(1, 0) = New RealNumber(-0.5)
                .Item(1, 1) = New RealNumber(-0.5)
            End With

            resultMatrix = matrix3.Inverse()

            For A = 0 To 1
                For B = 0 To 1
                    'Console.WriteLine("(" & A & "," & B & ") = " & resultMatrix.Item(A, B).Value)
                    Assert.AreEqual(matrix4.Item(A, B).Value, resultMatrix.Item(A, B).Value)
                Next B
            Next A
        End Sub

        <Test()> Public Sub MethodInverse3x3()
            Dim matrix3 As New SquareMatrix(Of RealNumber)(3)
            Dim matrix4 As New SquareMatrix(Of RealNumber)(3)
            Dim resultMatrix As SquareMatrix(Of RealNumber)

            With matrix3
                .Item(0, 0) = New RealNumber(1)
                .Item(0, 1) = New RealNumber(5)
                .Item(0, 2) = New RealNumber(2)
                .Item(1, 0) = New RealNumber(1)
                .Item(1, 1) = New RealNumber(1)
                .Item(1, 2) = New RealNumber(7)
                .Item(2, 0) = New RealNumber(0)
                .Item(2, 1) = New RealNumber(-3)
                .Item(2, 2) = New RealNumber(4)
            End With

            With matrix4
                .Item(0, 0) = New RealNumber(-25)
                .Item(0, 1) = New RealNumber(26)
                .Item(0, 2) = New RealNumber(-33)
                .Item(1, 0) = New RealNumber(4)
                .Item(1, 1) = New RealNumber(-4)
                .Item(1, 2) = New RealNumber(5)
                .Item(2, 0) = New RealNumber(3)
                .Item(2, 1) = New RealNumber(-3)
                .Item(2, 2) = New RealNumber(4)
            End With

            resultMatrix = matrix3.Inverse()

            For A = 0 To 2
                For B = 0 To 2
                    'Console.WriteLine("(" & A & "," & B & ") = " & resultMatrix.Item(A, B).Value)
                    Assert.Less(Math.Abs(matrix4.Item(A, B).Value - resultMatrix.Item(A, B).Value), 0.00000001)
                Next B
            Next A
        End Sub

        <Test()> Public Sub MethodIsSymmetric()
            Dim matrix3 As New SquareMatrix(Of RealNumber)(3)
            Dim matrix4 As New SquareMatrix(Of RealNumber)(3)

            With matrix3
                .Item(0, 0) = New RealNumber(1)
                .Item(0, 1) = New RealNumber(2)
                .Item(0, 2) = New RealNumber(3)
                .Item(1, 0) = New RealNumber(2)
                .Item(1, 1) = New RealNumber(-4)
                .Item(1, 2) = New RealNumber(5)
                .Item(2, 0) = New RealNumber(3)
                .Item(2, 1) = New RealNumber(5)
                .Item(2, 2) = New RealNumber(6)
            End With

            With matrix4
                .Item(0, 0) = New RealNumber(1)
                .Item(0, 1) = New RealNumber(2)
                .Item(0, 2) = New RealNumber(3)
                .Item(1, 0) = New RealNumber(1)
                .Item(1, 1) = New RealNumber(-4)
                .Item(1, 2) = New RealNumber(5)
                .Item(2, 0) = New RealNumber(3)
                .Item(2, 1) = New RealNumber(5)
                .Item(2, 2) = New RealNumber(6)
            End With

            Assert.IsTrue(matrix3.IsSymmetric)
            Assert.IsFalse(matrix4.IsSymmetric)
        End Sub

        <Test()> Public Sub MethodMinor()
            Dim mat As New SquareMatrix(Of RealNumber)(5)
            mat = mat.MultiplicativeIdentity
            Dim min As SquareMatrix(Of RealNumber)

            min = mat.Minor(2, 2)
            Assert.AreEqual(True, min.Equals(min.MultiplicativeIdentity))
        End Sub

#End Region

    End Class

End Namespace
