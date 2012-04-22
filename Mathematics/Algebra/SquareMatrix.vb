
Namespace Algebra

    Public Class SquareMatrix(Of T As {Class, New, ISubtractable(Of T), IDivideable(Of T), IAbsoluteable(Of T), IComparable(Of T)})
        Inherits Matrix(Of T)
        Implements ISubtractable(Of SquareMatrix(Of T)), IMultipliable(Of SquareMatrix(Of T)), IMultiplicativeIdentity(Of SquareMatrix(Of T)), IEquatable(Of SquareMatrix(Of T))

#Region "  Constructors  "

        ''' <summary>
        ''' Initializes a new instance of the SquareMatrix class of size 3 x 3 with elements of type T.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            MyBase.New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the SquareMatrix class of size newWidth with elements of type T.
        ''' </summary>
        ''' <param name="newWidth">The width and height of the new SquareMatrix.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal newWidth As Integer)
            MyBase.New(newWidth, newWidth)
        End Sub

#End Region

#Region "  Properties  "

        Public Shadows ReadOnly Property AdditiveIdentity() As SquareMatrix(Of T) Implements IAdditiveIdentity(Of SquareMatrix(Of T)).AdditiveIdentity
            Get
                Return New SquareMatrix(Of T)(Me.Width)
            End Get
        End Property

        Public ReadOnly Property MultiplicativeIdentity() As SquareMatrix(Of T) Implements IMultiplicativeIdentity(Of SquareMatrix(Of T)).MultiplicativeIdentity
            Get
                Dim rowIndex As Integer
                Dim colIndex As Integer
                Dim resultValue As New SquareMatrix(Of T)(Me.Width)
                Dim exampleT As New T

                For rowIndex = 0 To Me.Width - 1
                    For colIndex = 0 To Me.Width - 1
                        If rowIndex = colIndex Then
                            resultValue.Item(rowIndex, colIndex) = exampleT.MultiplicativeIdentity
                        Else
                            resultValue.Item(rowIndex, colIndex) = exampleT.AdditiveIdentity
                        End If
                    Next colIndex
                Next rowIndex

                Return resultValue
            End Get
        End Property

#End Region

#Region "  Methods  "

        Public Shadows Function Add(ByVal matrix2 As SquareMatrix(Of T)) As SquareMatrix(Of T) Implements IAddable(Of SquareMatrix(Of T)).Add
            If IsNothing(matrix2) Then
                Throw New ArgumentNullException("matrix2 cannot be null.")
            ElseIf Me.Width <> matrix2.Width Then
                Throw New ArgumentException("matrix2.Width must be equal to this Matrix's Width")
            ElseIf Me.Height <> matrix2.Height Then
                Throw New ArgumentException("matrix2.Height must be equal to this Matrix's Height")
            Else
                Dim resultValue As New SquareMatrix(Of T)(Me.Width)
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

        Public Shadows Function Clone() As SquareMatrix(Of T)
            Dim newClone As New SquareMatrix(Of T)(Me.Width)
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
        ''' Returns the determinant of the matrix by using Laplacian expansion
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Determinant() As T
            Return Me.DeterminantRecursion()
        End Function

        Private Function DeterminantRecursion() As T
            If Me.Width = 1 Then
                'Base Case
                Return Me.Item(0, 0)
            Else
                Dim topColIndex As Integer
                Dim exampleT As New T
                Dim returnValue As T = exampleT.AdditiveIdentity

                For topColIndex = 0 To Me.Width - 1
                    Dim minorDet As T

                    minorDet = Me.Minor(0, topColIndex).DeterminantRecursion
                    If Math.Pow(-1, topColIndex) = -1 Then
                        minorDet = exampleT.AdditiveIdentity.Subtract(minorDet)
                    End If
                    returnValue = returnValue.Add(Me.Item(0, topColIndex).Multiply(minorDet))
                Next topColIndex

                Return returnValue
            End If
        End Function

        Public Shadows Function Equals(ByVal other As SquareMatrix(Of T)) As Boolean Implements System.IEquatable(Of SquareMatrix(Of T)).Equals
            Return MyBase.Equals(other)
        End Function

        Public Function Inverse() As SquareMatrix(Of T)
            Dim matrixWidth As Integer = Me.Width
            Dim cloneMatrix As SquareMatrix(Of T) = Me.Clone
            Dim returnMatrix As New SquareMatrix(Of T)(matrixWidth)
            Dim indx As Vector(Of IntegerNumber)
            Dim col As New Vector(Of T)(matrixWidth)
            Dim exampleT As New T
            Dim i As Integer
            Dim j As Integer

            indx = cloneMatrix.LUDecomposition()
            For j = 0 To matrixWidth - 1
                For i = 0 To matrixWidth - 1
                    col.Item(i) = exampleT.AdditiveIdentity
                Next i
                col.Item(j) = exampleT.MultiplicativeIdentity
                cloneMatrix.LUBackSubstitution(indx, col)
                For i = 0 To matrixWidth - 1
                    returnMatrix.Item(i, j) = col.Item(i)
                Next i
            Next j

            Return returnMatrix
        End Function

        Public Function IsSymmetric() As Boolean
            Dim rowIndex As Integer
            Dim columnIndex As Integer

            For rowIndex = 0 To Me.Height - 1
                For columnIndex = 0 To Me.Width - 1
                    If Not (Me.Item(rowIndex, columnIndex).CompareTo(Me.Item(columnIndex, rowIndex))) = 0 Then
                        Return False
                    End If
                Next columnIndex
            Next rowIndex

            Return True
        End Function

        ''' <summary>
        ''' Performs a back substitution on a LU Decomposed matrix.
        ''' </summary>
        ''' <param name="LUDecompPermutation">The Vector the recorded the row permutation effected by the partial pivoting during the LUDecomposition.</param>
        ''' <param name="rhsMatrix"></param>
        ''' <remarks></remarks>
        Public Sub LUBackSubstitution(ByVal LUDecompPermutation As Vector(Of IntegerNumber), ByVal rhsMatrix As Vector(Of T))
            Dim matrixWidth As Integer = Me.Width
            Dim exampleT As New T

            Dim i As Integer
            Dim ii As Integer = -1
            Dim ip As Integer
            Dim j As Integer
            Dim sum As T

            'When ii is set to a positive value, it will become the
            '   index of the first nonvanishing element of rhsMatrix. We now
            '   do the forward substitution, equation (2.3.6). The
            '   only new wrinkle is to unscramble the permutation
            '   as we go.
            For i = 0 To matrixWidth - 1
                ip = CType(LUDecompPermutation.Item(i).Value, Integer)
                sum = rhsMatrix.Item(ip)
                rhsMatrix.Item(ip) = rhsMatrix.Item(i)
                If ii <> -1 Then
                    For j = ii To i - 1
                        sum = sum.Subtract(Me.Item(i, j).Multiply(rhsMatrix.Item(j)))
                    Next j
                ElseIf sum.CompareTo(exampleT.AdditiveIdentity) <> 0 Then
                    'A nonzero element was encountered, so from now one we
                    '   will have to do the sums in the loop above
                    ii = i
                End If
                rhsMatrix.Item(i) = sum
            Next i

            'Now we do the backsubstitution
            For i = matrixWidth - 1 To 0 Step -1
                sum = rhsMatrix.Item(i)
                For j = i + 1 To matrixWidth - 1
                    sum = sum.Subtract(Me.Item(i, j).Multiply(rhsMatrix.Item(j)))
                Next j
                rhsMatrix.Item(i) = sum.Divide(Me.Item(i, i))    'Store a component of the solution vector X.
            Next i

            'All done!
        End Sub

        ''' <summary>
        ''' Performs a Lower/Upper Decomposition on the matrix.
        ''' </summary>
        ''' <returns>A Vector that records the row permutation effected by the partial pivoting.</returns>
        ''' <remarks></remarks>
        Public Function LUDecomposition() As Vector(Of IntegerNumber)
            Dim matrixWidth As Integer = Me.Width
            Dim indx As New Vector(Of IntegerNumber)(matrixWidth)
            Dim d As T
            Dim exampleT As New T

            Dim i As Integer
            Dim iMax As Integer
            Dim j As Integer
            Dim k As Integer
            Dim big As T
            Dim dum As T
            Dim sum As T
            Dim temp As T
            Dim vv As New Vector(Of T)(matrixWidth)

            d = exampleT.MultiplicativeIdentity 'No row interchanges yet
            For i = 0 To matrixWidth - 1        'Loop over rows to get the implicit scaling information
                big = exampleT.AdditiveIdentity
                For j = 0 To matrixWidth - 1
                    temp = Me.Item(i, j).AbsoluteValue
                    If temp.CompareTo(big) > 0 Then
                        big = temp
                    End If
                Next j

                If (big.CompareTo(exampleT.AdditiveIdentity) = 0) Then
                    Throw New MatrixSingularException()
                End If
                'No nonzero largest element
                vv.Item(i) = exampleT.MultiplicativeIdentity.Divide(big)
            Next i

            'This is the loop over columns of Crout's method.
            For j = 0 To matrixWidth - 1
                For i = 0 To j - 1
                    sum = Me.Item(i, j)
                    For k = 0 To i - 1
                        sum = sum.Subtract(Me.Item(i, k).Multiply(Me.Item(k, j)))
                    Next k
                    Me.Item(i, j) = sum
                Next i
                big = exampleT.AdditiveIdentity 'Initialize for the search for largest pivot element
                For i = j To matrixWidth - 1
                    sum = Me.Item(i, j)
                    For k = 0 To j - 1
                        sum = sum.Subtract(Me.Item(i, k).Multiply(Me.Item(k, j)))
                    Next k
                    Me.Item(i, j) = sum
                    dum = vv.Item(i).Multiply(sum.AbsoluteValue)
                    'Is the figure of merit for the pivot better than the best so far?
                    If dum.CompareTo(big) >= 0 Then
                        big = dum
                        iMax = i
                    End If
                Next i

                'Do we need to interchange rows?
                If (j <> iMax) Then
                    'Yes, do so...
                    For k = 0 To matrixWidth - 1
                        dum = Me.Item(iMax, k)
                        Me.Item(iMax, k) = Me.Item(j, k)
                        Me.Item(j, k) = dum
                    Next k
                    d = exampleT.AdditiveIdentity.Subtract(d)   '...and change the parity of d.
                    vv.Item(iMax) = vv.Item(j)  'Also interchange the scale factor.
                End If

                indx.Item(j) = New IntegerNumber(iMax)

                'If the pivot element is zero the matrix is singular (at least to the precision of the
                'algorithm). For some aplications on singular matrices, it is desirable to substitute
                'TINY for zero.
                'If resultMatrix.Item(j,j).CompareTo(exampleT.AdditiveIdentity) = 0 Then
                '   resultMatrix.Item(j,j) = TINY
                'End If
                If Me.Item(j, j).CompareTo(exampleT.AdditiveIdentity) = 0 Then
                    Throw New MatrixSingularException()
                End If

                If (j <> matrixWidth - 1) Then
                    'Now, finally, divide by the pivot element
                    dum = exampleT.MultiplicativeIdentity.Divide(Me.Item(j, j))
                    For i = j + 1 To matrixWidth - 1
                        Me.Item(i, j) = Me.Item(i, j).Multiply(dum)
                    Next i
                End If
            Next j  'Got back for the next column in the reduction

            Return indx
        End Function

        Public Shadows Function Minor(ByVal rowIndex As Integer, ByVal columnIndex As Integer) As SquareMatrix(Of T)
            Dim minorMatrix As New SquareMatrix(Of T)(Me.Width - 1)
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

        Public Shadows Function Multiply(ByVal matrix2 As SquareMatrix(Of T)) As SquareMatrix(Of T) Implements IMultipliable(Of SquareMatrix(Of T)).Multiply
            If IsNothing(matrix2) Then
                Throw New ArgumentNullException("matrix2 cannot be null.")
            ElseIf Me.Width <> matrix2.Height Then
                Throw New ArgumentException("matrix2.Height must equal this Matrix's Width")
            Else
                Dim resultValue As New SquareMatrix(Of T)(Me.Width)
                Dim rowIndex As Integer
                Dim columnIndex As Integer
                Dim commonIndex As Integer

                For rowIndex = 0 To Me.Height - 1
                    For columnIndex = 0 To matrix2.Width - 1
                        For commonIndex = 0 To Me.Width - 1
                            resultValue.Item(rowIndex, columnIndex) = resultValue.Item(rowIndex, columnIndex).Add( _
                                Me.Item(rowIndex, commonIndex).Multiply(matrix2.Item(commonIndex, columnIndex)))
                        Next commonIndex
                    Next columnIndex
                Next rowIndex

                Return resultValue
            End If
        End Function

        Public Shadows Function ScalarMultiply(ByVal scalar As T) As SquareMatrix(Of T)
            If IsNothing(scalar) Then
                Throw New ArgumentNullException("scalar cannot be null.")
            Else
                Dim resultValue As New SquareMatrix(Of T)(Me.Width)
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

        Public Shadows Function Subtract(ByVal matrix2 As SquareMatrix(Of T)) As SquareMatrix(Of T) Implements ISubtractable(Of SquareMatrix(Of T)).Subtract
            If IsNothing(matrix2) Then
                Throw New ArgumentNullException("matrix2 cannot be null.")
            ElseIf Me.Width <> matrix2.Width Then
                Throw New ArgumentException("matrix2.Width must be equal to this Matrix's Width")
            ElseIf Me.Height <> matrix2.Height Then
                Throw New ArgumentException("matrix2.Height must be equal to this Matrix's Height")
            Else
                Dim resultValue As New SquareMatrix(Of T)(Me.Width)
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

        Public Function Trace() As T
            Dim index As Integer
            Dim sum As New T

            For index = 0 To Me.Width - 1
                sum = sum.Add(Me.Item(index, index))
            Next index

            Return sum
        End Function

#End Region

#Region "  Operators  "

        Public Shared Shadows Operator +(ByVal lhs As SquareMatrix(Of T), ByVal rhs As SquareMatrix(Of T)) As SquareMatrix(Of T)
            If IsNothing(lhs) Then
                Throw New ArgumentNullException("Left-hand side of + cannot be null.")
            ElseIf IsNothing(rhs) Then
                Throw New ArgumentNullException("Right-hand side of + cannot be null.")
            Else
                Return lhs.Add(rhs)
            End If
        End Operator

        Public Shared Shadows Operator -(ByVal lhs As SquareMatrix(Of T), ByVal rhs As SquareMatrix(Of T)) As SquareMatrix(Of T)
            If IsNothing(lhs) Then
                Throw New ArgumentNullException("Left-hand side of + cannot be null.")
            ElseIf IsNothing(rhs) Then
                Throw New ArgumentNullException("Right-hand side of + cannot be null.")
            Else
                Return lhs.Subtract(rhs)
            End If
        End Operator

        Public Shared Shadows Operator *(ByVal scalar As T, ByVal matrx As SquareMatrix(Of T)) As SquareMatrix(Of T)
            If IsNothing(scalar) Then
                Throw New ArgumentNullException("scalar cannot be null.")
            ElseIf IsNothing(matrx) Then
                Throw New ArgumentNullException("matrx cannot be null.")
            Else
                Return matrx.ScalarMultiply(scalar)
            End If
        End Operator

        Public Shared Shadows Operator *(ByVal lhs As SquareMatrix(Of T), ByVal rhs As SquareMatrix(Of T)) As SquareMatrix(Of T)
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

End Namespace
