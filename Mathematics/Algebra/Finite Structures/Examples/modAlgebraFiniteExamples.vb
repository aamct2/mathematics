
Imports System.Collections.Generic

Namespace Algebra

    Public Module modAlgebraFiniteExamples

        Private myZmod2Field As FiniteField(Of IntegerNumber)

        Private values As New List(Of Integer)
        Private level As Integer
        Private size As Integer
        Private matSet As FiniteSet(Of SquareMatrix(Of RealNumber))

        Public Function Zmod2Field() As FiniteField(Of IntegerNumber)
            If IsNothing(myZmod2Field) Then
                'Generate the field
                Dim Zmod2Set As New FiniteSet(Of IntegerNumber)
                Dim Zmod2Addition As FiniteBinaryOperation(Of IntegerNumber)
                Dim Zmod2Multiplication As FiniteBinaryOperation(Of IntegerNumber)

                Zmod2Set.AddElement(New IntegerNumber(0))
                Zmod2Set.AddElement(New IntegerNumber(1))

                Zmod2Addition = New FiniteBinaryOperation(Of IntegerNumber)(Zmod2Set, New ZmodNAdditionMap(New IntegerNumber(2)))
                Zmod2Multiplication = New FiniteBinaryOperation(Of IntegerNumber)(Zmod2Set, New ZmodNMultiplicationMap(New IntegerNumber(2)))

                myZmod2Field = New FiniteField(Of IntegerNumber)(Zmod2Set, Zmod2Addition, Zmod2Multiplication)
            End If

            Return myZmod2Field
        End Function

        Public Class ZmodNAdditionMap
            Inherits Map(Of Tuple, IntegerNumber)

            Private myN As IntegerNumber

            Public Sub New(ByVal N As IntegerNumber)
                Me.myN = N
            End Sub

            Public ReadOnly Property N() As IntegerNumber
                Get
                    Return Me.myN
                End Get
            End Property

            Public Overrides Function ApplyMap(ByVal input As Tuple) As IntegerNumber
                Dim dummyIntNum As New IntegerNumber

                If input.Element(0).GetType.Equals(dummyIntNum.GetType) = True And _
                    input.Element(1).GetType.Equals(dummyIntNum.GetType) = True Then
                    Return (CType(input.Element(0), IntegerNumber) + CType(input.Element(1), IntegerNumber)) Mod Me.N
                Else
                    Throw New Exception("The input for the ZmodNAdditionMap contained an element that was not an IntegerNumber.")
                End If
            End Function
        End Class

        Public Class ZmodNMultiplicationMap
            Inherits Map(Of Tuple, IntegerNumber)

            Private myN As IntegerNumber

            Public Sub New(ByVal N As IntegerNumber)
                Me.myN = N
            End Sub

            Public ReadOnly Property N() As IntegerNumber
                Get
                    Return Me.myN
                End Get
            End Property

            Public Overrides Function ApplyMap(ByVal input As Tuple) As IntegerNumber
                Dim dummyIntNum As New IntegerNumber

                If input.Element(0).GetType.Equals(dummyIntNum.GetType) = True And _
                    input.Element(1).GetType.Equals(dummyIntNum.GetType) = True Then
                    Return (CType(input.Element(0), IntegerNumber) * CType(input.Element(1), IntegerNumber)) Mod Me.N
                Else
                    Throw New Exception("The input for the ZmodNMultiplicationMap contained an element that was not an IntegerNumber.")
                End If
            End Function
        End Class

        Public Function Dihedral2NGroup(ByVal N As Integer) As FiniteGroup(Of SquareMatrix(Of RealNumber))
            'Generate the group's set
            Dim newSet As New FiniteSet(Of SquareMatrix(Of RealNumber))

            Dim K As Integer
            Dim curMat As SquareMatrix(Of RealNumber)

            For K = 0 To N
                'Add the rotation matrix
                curMat = New SquareMatrix(Of RealNumber)(2)
                curMat.Item(0, 0) = New RealNumber(Math.Cos((2 * Math.PI * K) / N))
                curMat.Item(1, 0) = New RealNumber(Math.Sin((2 * Math.PI * K) / N))
                curMat.Item(0, 1) = New RealNumber(-1 * Math.Sin((2 * Math.PI * K) / N))
                curMat.Item(1, 1) = New RealNumber(Math.Cos((2 * Math.PI * K) / N))
                newSet.AddElement(curMat)

                'Add the reflection matrix
                curMat = New SquareMatrix(Of RealNumber)(2)
                curMat.Item(0, 0) = New RealNumber(Math.Cos((2 * Math.PI * K) / N))
                curMat.Item(1, 0) = New RealNumber(Math.Sin((2 * Math.PI * K) / N))
                curMat.Item(0, 1) = New RealNumber(Math.Sin((2 * Math.PI * K) / N))
                curMat.Item(1, 1) = New RealNumber(-1 * Math.Cos((2 * Math.PI * K) / N))
                newSet.AddElement(curMat)
            Next K

            Dim index As Integer
            Console.Out.WriteLine("------")
            For index = 0 To newSet.Cardinality - 1
                Console.Out.WriteLine(newSet.Element(index).ToString)
            Next index
            Console.Out.Flush()

            'Create the operation
            Dim newOperation As New FiniteBinaryOperation(Of SquareMatrix(Of RealNumber))(newSet, New SquareMatrixNMultiplicationMap(Of RealNumber)(New IntegerNumber(2)))

            'Return the group
            Return New FiniteGroup(Of SquareMatrix(Of RealNumber))(newSet, newOperation)
        End Function

        ''' <summary>
        ''' Returns the dihedral group of order 8, as represented by matrices.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Dihedral8Group() As FiniteGroup(Of SquareMatrix(Of RealNumber))
            'Generate the group's set
            Dim newSet As New FiniteSet(Of SquareMatrix(Of RealNumber))

            Dim K As Integer
            Dim N As Integer = 4
            Dim curMat As SquareMatrix(Of RealNumber)

            For K = 0 To N
                'Add the rotation matrix
                curMat = New SquareMatrix(Of RealNumber)(2)
                curMat.Item(0, 0) = New RealNumber(Math.Round(Math.Cos((2 * Math.PI * K) / N)))
                curMat.Item(1, 0) = New RealNumber(Math.Round(Math.Sin((2 * Math.PI * K) / N)))
                curMat.Item(0, 1) = New RealNumber(Math.Round(-1 * Math.Sin((2 * Math.PI * K) / N)))
                curMat.Item(1, 1) = New RealNumber(Math.Round(Math.Cos((2 * Math.PI * K) / N)))
                newSet.AddElement(curMat)

                'Add the reflection matrix
                curMat = New SquareMatrix(Of RealNumber)(2)
                curMat.Item(0, 0) = New RealNumber(Math.Round(Math.Cos((2 * Math.PI * K) / N)))
                curMat.Item(1, 0) = New RealNumber(Math.Round(Math.Sin((2 * Math.PI * K) / N)))
                curMat.Item(0, 1) = New RealNumber(Math.Round(Math.Sin((2 * Math.PI * K) / N)))
                curMat.Item(1, 1) = New RealNumber(Math.Round(-1 * Math.Cos((2 * Math.PI * K) / N)))
                newSet.AddElement(curMat)
            Next K

            'Create the operation
            Dim newOperation As New FiniteBinaryOperation(Of SquareMatrix(Of RealNumber))(newSet, New SquareMatrixNMultiplicationMap(Of RealNumber)(New IntegerNumber(2)))

            'Return the group
            Return New FiniteGroup(Of SquareMatrix(Of RealNumber))(newSet, newOperation)
        End Function

        ''' <summary>
        ''' Returns the quaternion group of order 8, as represented by matrices.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function QuaternionGroup() As FiniteGroup(Of SquareMatrix(Of ComplexNumber))
            Dim newSet As New FiniteSet(Of SquareMatrix(Of ComplexNumber))
            Dim id As New SquareMatrix(Of ComplexNumber)(2)
            Dim i As New SquareMatrix(Of ComplexNumber)(2)
            Dim j As New SquareMatrix(Of ComplexNumber)(2)
            Dim k As New SquareMatrix(Of ComplexNumber)(2)
            Dim neg1 As New ComplexNumber(New RealNumber(-1), New RealNumber(0))

            With id
                .Item(0, 0) = New ComplexNumber(New RealNumber(1), New RealNumber(0))
                .Item(1, 0) = New ComplexNumber(New RealNumber(0), New RealNumber(0))
                .Item(0, 1) = New ComplexNumber(New RealNumber(0), New RealNumber(0))
                .Item(1, 1) = New ComplexNumber(New RealNumber(1), New RealNumber(0))
            End With

            With i
                .Item(0, 0) = New ComplexNumber(New RealNumber(0), New RealNumber(1))
                .Item(1, 0) = New ComplexNumber(New RealNumber(0), New RealNumber(0))
                .Item(0, 1) = New ComplexNumber(New RealNumber(0), New RealNumber(0))
                .Item(1, 1) = New ComplexNumber(New RealNumber(0), New RealNumber(-1))
            End With

            With j
                .Item(0, 0) = New ComplexNumber(New RealNumber(0), New RealNumber(0))
                .Item(1, 0) = New ComplexNumber(New RealNumber(1), New RealNumber(0))
                .Item(0, 1) = New ComplexNumber(New RealNumber(-1), New RealNumber(0))
                .Item(1, 1) = New ComplexNumber(New RealNumber(0), New RealNumber(0))
            End With

            With k
                .Item(0, 0) = New ComplexNumber(New RealNumber(0), New RealNumber(0))
                .Item(1, 0) = New ComplexNumber(New RealNumber(0), New RealNumber(1))
                .Item(0, 1) = New ComplexNumber(New RealNumber(0), New RealNumber(1))
                .Item(1, 1) = New ComplexNumber(New RealNumber(0), New RealNumber(0))
            End With

            With newSet
                .AddElement(id)
                .AddElement(neg1 * id)
                .AddElement(i)
                .AddElement(neg1 * i)
                .AddElement(j)
                .AddElement(neg1 * j)
                .AddElement(k)
                .AddElement(neg1 * k)
            End With

            Dim newMap As New SquareMatrixNMultiplicationMap(Of ComplexNumber)(New IntegerNumber(2))
            Dim newOp As New FiniteBinaryOperation(Of SquareMatrix(Of ComplexNumber))(newSet, newMap)

            Return New FiniteGroup(Of SquareMatrix(Of ComplexNumber))(newSet, newOp)
        End Function

        ''' <summary>
        ''' Returns the symmetric group of order n!, as represented by permutation matrices.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SymmetricGroup(ByVal n As Integer) As FiniteGroup(Of SquareMatrix(Of RealNumber))
            Dim newSet As New FiniteSet(Of SquareMatrix(Of RealNumber))

            matSet = New FiniteSet(Of SquareMatrix(Of RealNumber))
            level = -1
            values.Clear()
            size = n

            Dim index As Integer

            For index = 0 To n - 1
                values.Add(0)
            Next index

            Visit(0)

            newSet = matSet

            Dim newMap As New SquareMatrixNMultiplicationMap(Of RealNumber)(New IntegerNumber(n))
            Dim newOp As New FiniteBinaryOperation(Of SquareMatrix(Of RealNumber))(newSet, newMap)

            'Cheat here since we know it is associative and it has inverses for all elements
            newOp.functionProperties.Add("associativity", True)
            newOp.functionProperties.Add("inverses", True)
            newOp.identityElement = newSet.Element(0).MultiplicativeIdentity()
            newOp.functionProperties.Add("identity", True)
            If n > 2 Then
                newOp.functionProperties.Add("commutative", False)
            Else
                newOp.functionProperties.Add("commutative", True)
            End If

            Return New FiniteGroup(Of SquareMatrix(Of RealNumber))(newSet, newOp)
        End Function

        ''' <summary>
        ''' Returns the alternating group of order n!/2, as represented by permutation matrices.
        ''' </summary>
        ''' <param name="n"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function AlternatingGroup(ByVal n As Integer) As FiniteGroup(Of SquareMatrix(Of RealNumber))
            'First get the symmetric group of order n!
            Dim SymNGroup As FiniteGroup(Of SquareMatrix(Of RealNumber)) = SymmetricGroup(n)

            'Then pick out the even permutations (the ones with determinant -1)
            Dim newSet As New FiniteSet(Of SquareMatrix(Of RealNumber))
            Dim index As Integer
            Dim int1 As New RealNumber(1)

            For index = 0 To SymNGroup.Order - 1
                If SymNGroup.theSet.Element(index).Determinant = int1 Then
                    newSet.AddElement(SymNGroup.theSet.Element(index))
                End If
            Next index

            Dim newMap As New SquareMatrixNMultiplicationMap(Of RealNumber)(New IntegerNumber(n))
            Dim newOp As FiniteBinaryOperation(Of SquareMatrix(Of RealNumber)) = SymNGroup.Operation.Restriction(newSet)

            'Cheat here since we know it has inverses for all elements
            newOp.functionProperties.Add("inverses", True)
            If n = 3 Then
                newOp.functionProperties.Add("commutative", True)
            End If

            Return New FiniteGroup(Of SquareMatrix(Of RealNumber))(newSet, newOp)
        End Function

        ''' <summary>
        ''' Returns the Heisenberg group of order 27, as represented by 3x3 matrices with entries from Zmod3.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Heisenberg3Group() As FiniteGroup(Of SquareMatrix(Of Zmod3))
            Dim Heisenberg3Set As New FiniteSet(Of SquareMatrix(Of Zmod3))
            Dim HeisenbergGrp3 As FiniteGroup(Of SquareMatrix(Of Zmod3))
            Dim Heisenberg3Op As FiniteBinaryOperation(Of SquareMatrix(Of Zmod3))

            Dim a As Integer
            Dim b As Integer
            Dim c As Integer
            Dim Z1 As New Zmod3(New IntegerNumber(1))
            Dim curMatrix As SquareMatrix(Of Zmod3)

            For a = 0 To 2
                For b = 0 To 2
                    For c = 0 To 2
                        curMatrix = New SquareMatrix(Of Zmod3)(3)
                        curMatrix.Item(0, 0) = Z1
                        curMatrix.Item(1, 1) = Z1
                        curMatrix.Item(2, 2) = Z1
                        curMatrix.Item(0, 1) = New Zmod3(New IntegerNumber(a))
                        curMatrix.Item(0, 2) = New Zmod3(New IntegerNumber(b))
                        curMatrix.Item(1, 2) = New Zmod3(New IntegerNumber(c))

                        Heisenberg3Set.AddElement(curMatrix)
                    Next c
                Next b
            Next a

            Heisenberg3Op = New FiniteBinaryOperation(Of SquareMatrix(Of Zmod3))(Heisenberg3Set, New SquareMatrixNMultiplicationMap(Of Zmod3)(New IntegerNumber(3)))
            HeisenbergGrp3 = New FiniteGroup(Of SquareMatrix(Of Zmod3))(Heisenberg3Set, Heisenberg3Op)

            Return HeisenbergGrp3
        End Function

        ''' <summary>
        ''' The map corresoding to matrix addition of square matrices of size N.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <remarks></remarks>
        Public Class SquareMatrixNAdditionMap(Of T As {Class, New, ISubtractable(Of T), IDivideable(Of T), IAbsoluteable(Of T), IComparable(Of T)})
            Inherits Map(Of Tuple, SquareMatrix(Of T))

            Private myN As IntegerNumber

            Public Sub New(ByVal size As IntegerNumber)
                Me.myN = size
            End Sub

            Public ReadOnly Property N() As IntegerNumber
                Get
                    Return Me.myN
                End Get
            End Property

            Public Overrides Function ApplyMap(ByVal input As Tuple) As SquareMatrix(Of T)
                Dim dummyMat As New SquareMatrix(Of T)
                Dim mat1 As SquareMatrix(Of T)
                Dim mat2 As SquareMatrix(Of T)

                If input.Element(0).GetType.Equals(dummyMat.GetType) = False Or _
                    input.Element(1).GetType.Equals(dummyMat.GetType) = False Then
                    Throw New Exception("The input for the SquareMatrixNAdditionMap contained an element that was not a SquareMatrix.")
                End If

                mat1 = CType(input.Element(0), SquareMatrix(Of T))
                mat2 = CType(input.Element(1), SquareMatrix(Of T))
                If mat1.Height <> Me.N.Value Or mat2.Height <> Me.N.Value Then
                    Throw New Exception("The input for the SquareMatrixNAdditionMap contained an element that did not have a height of " & Me.N.Value & ".")
                End If

                Return mat1 + mat2
            End Function
        End Class

        ''' <summary>
        ''' The map corresoding to matrix multiplication of square matrices of size N.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <remarks></remarks>
        Public Class SquareMatrixNMultiplicationMap(Of T As {Class, New, ISubtractable(Of T), IDivideable(Of T), IAbsoluteable(Of T), IComparable(Of T)})
            Inherits Map(Of Tuple, SquareMatrix(Of T))

            Private myN As IntegerNumber

            Public Sub New(ByVal size As IntegerNumber)
                Me.myN = size
            End Sub

            Public ReadOnly Property N() As IntegerNumber
                Get
                    Return Me.myN
                End Get
            End Property

            Public Overrides Function ApplyMap(ByVal input As Tuple) As SquareMatrix(Of T)
                Dim dummyMat As New SquareMatrix(Of T)
                Dim mat1 As SquareMatrix(Of T)
                Dim mat2 As SquareMatrix(Of T)

                If input.Element(0).GetType.Equals(dummyMat.GetType) = False Or _
                    input.Element(1).GetType.Equals(dummyMat.GetType) = False Then
                    Throw New Exception("The input for the SquareMatrixNAdditionMap contained an element that was not a SquareMatrix.")
                End If

                mat1 = CType(input.Element(0), SquareMatrix(Of T))
                mat2 = CType(input.Element(1), SquareMatrix(Of T))
                If mat1.Height <> Me.N.Value Or mat2.Height <> Me.N.Value Then
                    Throw New Exception("The input for the SquareMatrixNAdditionMap contained an element that did not have a height of " & Me.N.Value & ".")
                End If

                Return mat1 * mat2
            End Function
        End Class

        Private Sub Visit(ByVal k As Integer)
            level = level + 1
            values.Item(k) = level

            If level = size Then
                AddItem()
            Else
                Dim i As Integer

                For i = 0 To size - 1
                    If values.Item(i) = 0 Then
                        Visit(i)
                    End If
                Next i
            End If

            level = level - 1
            values.Item(k) = 0
        End Sub

        Private Sub AddItem()
            Dim rowIndex As Integer
            Dim colIndex As Integer
            Dim newMat As New SquareMatrix(Of RealNumber)(size)

            For rowIndex = 0 To size - 1
                For colIndex = 0 To size - 1
                    If values.Item(rowIndex) - 1 = colIndex Then
                        newMat.Item(rowIndex, colIndex) = New RealNumber(1)
                    Else
                        newMat.Item(rowIndex, colIndex) = New RealNumber(0)
                    End If
                Next colIndex
            Next rowIndex

            matSet.AddElement(newMat)
        End Sub

    End Module

End Namespace