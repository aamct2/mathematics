
Imports System.Text
Imports System.Collections.Generic
Imports System.Threading.Tasks

Namespace Algebra

    ''' <summary>
    ''' Represents a matrix with elements of type T.
    ''' </summary>
    ''' <typeparam name="T">The Type of elements used for entries in the matrix.</typeparam>
    ''' <remarks></remarks>
    Public Class Matrix(Of T As {Class, New, ISubtractable(Of T), IDivideable(Of T), IAbsoluteable(Of T), IComparable(Of T)})
        Implements IAddable(Of Matrix(Of T)), IEquatable(Of Matrix(Of T)), ISubtractable(Of Matrix(Of T))

        Private data(,) As T

#Region "  Constructors  "

        ''' <summary>
        ''' Initializes a new instance of the Matrix class of size 3 x 3 with elements of type T.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            MyClass.New(3, 3)
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the Matrix class of size newHeight and newWidth with elements of type T.
        ''' </summary>
        ''' <param name="newHeight">Number of rows in the new matrix.</param>
        ''' <param name="newWidth">Number of columns in the new matrix.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal newHeight As Integer, ByVal newWidth As Integer)
            If newWidth < 1 Then
                Throw New ArgumentException("newWidth cannot be < 1")
            ElseIf newHeight < 1 Then
                Throw New ArgumentException("newHeight cannot be < 1")
            Else
                ReDim data(newHeight, newWidth)

                Dim rowIndex As Integer
                Dim columnIndex As Integer
                Dim exampleT As New T

                For rowIndex = 0 To Me.Height - 1
                    For columnIndex = 0 To Me.Width - 1
                        data(rowIndex, columnIndex) = exampleT.AdditiveIdentity
                    Next columnIndex
                Next rowIndex
            End If
        End Sub

#End Region

#Region "  Properties  "

        ''' <summary>
        ''' Gets the number of components in the matrix (Width * Height).
        ''' </summary>
        ''' <value>Returns an <c>Integer</c> representing the number of componenets in the matrix.</value>
        ''' <returns>Returns the number of components in the matrix.</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Count() As Integer
            Get
                Return Me.Width * Me.Height
            End Get
        End Property

        ''' <summary>
        ''' Gets the width of the matrix (number of columns).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Width() As Integer
            Get
                Return UBound(data, 2)
            End Get
        End Property

        ''' <summary>
        ''' Gets the height of the matrix (number of rows).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Height() As Integer
            Get
                Return UBound(data, 1)
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the component at (rowIndex, columnIndex).
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <param name="columnIndex"></param>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Item(ByVal rowIndex As Integer, ByVal columnIndex As Integer) As T
            Get
                If rowIndex > Me.Height - 1 Then
                    Throw New ArgumentException("rowIndex must be less than or equal to the Height - 1.")
                ElseIf columnIndex > Me.Width - 1 Then
                    Throw New ArgumentException("columnIndex must be less than or equal to the Width - 1.")
                Else
                    Return data(rowIndex, columnIndex)
                End If
            End Get
            Set(ByVal value As T)
                If Not IsNothing(value) Then
                    data(rowIndex, columnIndex) = value
                ElseIf rowIndex > Me.Height - 1 Then
                    Throw New ArgumentException("rowIndex must be less than or equal to the Height - 1.")
                ElseIf columnIndex > Me.Width - 1 Then
                    Throw New ArgumentException("columnIndex must be less than or equal to the Width - 1.")
                Else
                    Throw New ArgumentNullException("Item cannot be null.")
                End If
            End Set
        End Property

        ''' <summary>
        ''' Returns the additive identity matrix of appropriate size.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property AdditiveIdentity() As Matrix(Of T) Implements IAdditiveIdentity(Of Matrix(Of T)).AdditiveIdentity
            Get
                Return New Matrix(Of T)(Me.Height, Me.Width)
            End Get
        End Property

#End Region

#Region "  Methods  "

        ''' <summary>
        ''' Returns whether this matrix and matrix2 are identical.
        ''' </summary>
        ''' <param name="matrix2"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shadows Function Equals(ByVal matrix2 As Matrix(Of T)) As Boolean Implements System.IEquatable(Of Matrix(Of T)).Equals
            Return Matrix(Of T).Equals(Me, matrix2)
        End Function

        ''' <summary>
        ''' Returns whether matrix1 and matrix2 are identical.
        ''' </summary>
        ''' <param name="matrix1"></param>
        ''' <param name="matrix2"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Shadows Function Equals(ByVal matrix1 As Matrix(Of T), ByVal matrix2 As Matrix(Of T)) As Boolean
            Dim rowIndex As Integer
            Dim colIndex As Integer

            If matrix1.Height <> matrix2.Height Or matrix1.Width <> matrix2.Width Then
                Return False
            End If

            For rowIndex = 0 To matrix1.Height - 1
                For colIndex = 0 To matrix1.Width - 1
                    If matrix1.Item(rowIndex, colIndex).CompareTo(matrix2.Item(rowIndex, colIndex)) <> 0 Then Return False
                Next colIndex
            Next rowIndex

            Return True
        End Function

        ''' <summary>
        ''' Returns the sum of this matrix and matrix2.
        ''' </summary>
        ''' <param name="matrix2"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Add(ByVal matrix2 As Matrix(Of T)) As Matrix(Of T) Implements IAddable(Of Matrix(Of T)).Add
            If IsNothing(matrix2) Then
                Throw New ArgumentNullException("matrix2 cannot be null.")
            ElseIf Me.Width <> matrix2.Width Then
                Throw New ArgumentException("matrix2.Width must be equal to this Matrix's Width")
            ElseIf Me.Height <> matrix2.Height Then
                Throw New ArgumentException("matrix2.Height must be equal to this Matrix's Height")
            Else
                Dim resultValue As New Matrix(Of T)(Me.Height, Me.Width)
                Dim rowIndex As Integer
                Dim columnIndex As Integer

                For rowIndex = 0 To Me.Height - 1
                    For columnIndex = 0 To Me.Width - 1
                        resultValue.Item(rowIndex, columnIndex) = _
                            Me.Item(rowIndex, columnIndex).Add(matrix2.Item(rowIndex, columnIndex))
                    Next columnIndex
                Next rowIndex

                Return resultValue
            End If
        End Function

        ''' <summary>
        ''' Returns an identical copy of this matrix.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Clone() As Matrix(Of T)
            Dim newClone As New Matrix(Of T)(Me.Height, Me.Width)
            Dim rowIndex As Integer
            Dim columnIndex As Integer

            For rowIndex = 0 To Me.Height - 1
                For columnIndex = 0 To Me.Width - 1
                    newClone.Item(rowIndex, columnIndex) = Me.Item(rowIndex, columnIndex)
                Next columnIndex
            Next rowIndex

            Return newClone
        End Function

        ''' <summary>
        ''' Returns the Hadamard Product of this matrix and matrix2.
        ''' </summary>
        ''' <param name="matrix2"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function HadamardProduct(ByVal matrix2 As Matrix(Of T)) As Matrix(Of T)
            If IsNothing(matrix2) Then
                Throw New ArgumentNullException("matrix2 cannot be null.")
            ElseIf Me.Width <> matrix2.Width Then
                Throw New ArgumentException("matrix2.Width must equal this Matrix's Width")
            ElseIf Me.Height <> matrix2.Height Then
                Throw New ArgumentException("matrix2.Height must equal this Matrix's Height")
            Else
                Dim resultValue As New Matrix(Of T)(Me.Height, Me.Width)
                Dim rowIndex As Integer
                Dim columnIndex As Integer

                For rowIndex = 0 To Me.Height - 1
                    For columnIndex = 0 To matrix2.Width - 1
                        resultValue.Item(rowIndex, columnIndex) = Me.Item(rowIndex, columnIndex).Multiply(matrix2.Item(rowIndex, columnIndex))
                    Next columnIndex
                Next rowIndex

                Return resultValue
            End If

            Return Nothing
        End Function

        ''' <summary>
        ''' Returns whether or not this matrix is a square matrix.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsSquare() As Boolean
            If Me.Width = Me.Height Then
                Return True
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' Returns the product of this matrix and matrix2.
        ''' </summary>
        ''' <param name="matrix2"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Multiply(ByVal matrix2 As Matrix(Of T)) As Matrix(Of T)
            If IsNothing(matrix2) Then
                Throw New ArgumentNullException("matrix2 cannot be null.")
            ElseIf Me.Width <> matrix2.Height Then
                Throw New ArgumentException("matrix2.Height must equal this Matrix's Width")
            Else
                Dim resultValue As New Matrix(Of T)(Me.Height, matrix2.Width)
                'Dim rowIndex As Integer
                'Dim columnIndex As Integer
                'Dim commonIndex As Integer

                Parallel.For(0, Me.Height, Sub(rowIndex)
                                               For columnIndex As Integer = 0 To matrix2.Width - 1
                                                   Dim temp As New T
                                                   For commonIndex As Integer = 0 To Me.Width - 1
                                                       temp = temp.Add(Me.Item(rowIndex, commonIndex).Multiply(matrix2.Item(commonIndex, columnIndex)))
                                                   Next commonIndex
                                                   resultValue.Item(rowIndex, columnIndex) = resultValue.Item(rowIndex, columnIndex).Add(temp)
                                               Next columnIndex
                                           End Sub)

                'For rowIndex = 0 To Me.Height - 1
                '    For columnIndex = 0 To matrix2.Width - 1
                '        For commonIndex = 0 To Me.Width - 1
                '            resultValue.Item(rowIndex, columnIndex) = resultValue.Item(rowIndex, columnIndex).Add( _
                '                Me.Item(rowIndex, commonIndex).Multiply(matrix2.Item(commonIndex, columnIndex)))
                '        Next commonIndex
                '    Next columnIndex
                'Next rowIndex

                Return resultValue
            End If
        End Function

        ''' <summary>
        ''' Returns the minor of this matrix by removing the row at rowIndex and column at columnIndex.
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <param name="columnIndex"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Minor(ByVal rowIndex As Integer, ByVal columnIndex As Integer) As Matrix(Of T)
            Dim minorMatrix As New Matrix(Of T)(Me.Height - 1, Me.Width - 1)
            Dim row As Integer
            Dim column As Integer
            Dim minorRow As Integer = 0
            Dim minorColumn As Integer = 0

            For row = 0 To Me.Height - 1
                For column = 0 To Me.Width - 1
                    If row <> rowIndex And column <> columnIndex Then
                        minorMatrix.Item(minorRow, minorColumn) = Me.Item(row, column)
                        minorColumn += 1
                        If minorColumn > Me.Width - 2 Then
                            minorColumn = 0
                            minorRow += 1
                        End If
                    End If
                Next column
            Next row

            Return minorMatrix
        End Function

        ''' <summary>
        ''' Returns the scalar product of this matrix with scalar.
        ''' </summary>
        ''' <param name="scalar"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ScalarMultiply(ByVal scalar As T) As Matrix(Of T)
            If IsNothing(scalar) Then
                Throw New ArgumentNullException("scalar cannot be null.")
            Else
                Dim resultValue As New Matrix(Of T)(Me.Height, Me.Width)
                Dim rowIndex As Integer
                Dim columnIndex As Integer

                For rowIndex = 0 To Me.Height - 1
                    For columnIndex = 0 To Me.Width - 1
                        resultValue.Item(rowIndex, columnIndex) = Me.Item(rowIndex, columnIndex).Multiply(scalar)
                    Next columnIndex
                Next rowIndex

                Return resultValue
            End If
        End Function

        ''' <summary>
        ''' Returns the difference of this matrix by matrix2.
        ''' </summary>
        ''' <param name="matrix2"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Subtract(ByVal matrix2 As Matrix(Of T)) As Matrix(Of T) Implements ISubtractable(Of Matrix(Of T)).Subtract
            If IsNothing(matrix2) Then
                Throw New ArgumentNullException("matrix2 cannot be null.")
            ElseIf Me.Width <> matrix2.Width Then
                Throw New ArgumentException("matrix2.Width must be equal to this Matrix's Width")
            ElseIf Me.Height <> matrix2.Height Then
                Throw New ArgumentException("matrix2.Height must be equal to this Matrix's Height")
            Else
                Dim resultValue As New Matrix(Of T)(Me.Height, Me.Width)
                Dim rowIndex As Integer
                Dim columnIndex As Integer

                For rowIndex = 0 To Me.Height - 1
                    For columnIndex = 0 To Me.Width - 1
                        resultValue.Item(rowIndex, columnIndex) = _
                            Me.Item(rowIndex, columnIndex).Subtract(matrix2.Item(rowIndex, columnIndex))
                    Next columnIndex
                Next rowIndex

                Return resultValue
            End If
        End Function

        ''' <summary>
        ''' Returns a string representation of this matrix.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overrides Function ToString() As String
            Dim strBuilder As New StringBuilder()

            Dim rowIndex As Integer
            Dim colIndex As Integer

            For rowIndex = 0 To Me.Height - 1
                strBuilder.Append("|" & Me.Item(rowIndex, 0).ToString)

                For colIndex = 1 To Me.Width - 1
                    strBuilder.Append(" " & Me.Item(rowIndex, colIndex).ToString)
                Next colIndex

                strBuilder.Append("|" & vbCrLf)
            Next rowIndex

            Return strBuilder.ToString
        End Function

#End Region

#Region "  Operators  "

        Public Shared Operator +(ByVal lhs As Matrix(Of T), ByVal rhs As Matrix(Of T)) As Matrix(Of T)
            If IsNothing(lhs) Then
                Throw New ArgumentNullException("Left-hand side of + cannot be null.")
            ElseIf IsNothing(rhs) Then
                Throw New ArgumentNullException("Right-hand side of + cannot be null.")
            Else
                Return lhs.Add(rhs)
            End If
        End Operator

        Public Shared Operator -(ByVal lhs As Matrix(Of T), ByVal rhs As Matrix(Of T)) As Matrix(Of T)
            If IsNothing(lhs) Then
                Throw New ArgumentNullException("Left-hand side of + cannot be null.")
            ElseIf IsNothing(rhs) Then
                Throw New ArgumentNullException("Right-hand side of + cannot be null.")
            Else
                Return lhs.Subtract(rhs)
            End If
        End Operator

        Public Shared Operator *(ByVal scalar As T, ByVal matrx As Matrix(Of T)) As Matrix(Of T)
            If IsNothing(scalar) Then
                Throw New ArgumentNullException("scalar cannot be null.")
            ElseIf IsNothing(matrx) Then
                Throw New ArgumentNullException("matrx cannot be null.")
            Else
                Return matrx.ScalarMultiply(scalar)
            End If
        End Operator

        Public Shared Operator *(ByVal lhs As Matrix(Of T), ByVal rhs As Matrix(Of T)) As Matrix(Of T)
            If IsNothing(lhs) Then
                Throw New ArgumentNullException("Left-hand side of + cannot be null.")
            ElseIf IsNothing(rhs) Then
                Throw New ArgumentNullException("Right-hand side of + cannot be null.")
            Else
                Return lhs.Multiply(rhs)
            End If
        End Operator

#End Region

    End Class

    Public Class MatrixNotSquareException
        Inherits Exception

#Region "  Constructors  "

        Public Sub New()
            MyBase.New("The matrix must be a square matrix.")
        End Sub

        Public Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub

#End Region

    End Class

    Public Class MatrixSingularException
        Inherits Exception

#Region "  Constructors  "

        Public Sub New()
            MyBase.New("The matrix cannot be singular.")
        End Sub

        Public Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub

#End Region

    End Class

End Namespace