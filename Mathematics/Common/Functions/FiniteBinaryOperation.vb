

Imports System.Collections.Generic
Imports System.Data
Imports System.Threading.Tasks
Imports Mathematics.Algebra

Public Class FiniteLeftExternalBinaryOperation(Of LeftOperand As {Class, New, IEquatable(Of LeftOperand)}, RightOperand As {Class, New, IEquatable(Of RightOperand)})
    Inherits FiniteFunction(Of Tuple, RightOperand)

    Private myLeftIdentity As LeftOperand

    Private LeftOperandDomain As FiniteSet(Of LeftOperand)
    Private RightOperandDomain As FiniteSet(Of RightOperand)

#Region "  Constructors  "

    Public Sub New(ByVal newScalarSet As FiniteSet(Of LeftOperand), ByVal newCodomain As FiniteSet(Of RightOperand), ByVal newRelation As Map(Of Tuple, RightOperand))
        MyBase.New(newScalarSet.DirectProduct(newCodomain), newCodomain, newRelation)

        Me.LeftOperandDomain = newScalarSet
        Me.RightOperandDomain = newCodomain
    End Sub

#End Region

#Region "  Properties  "

    Public ReadOnly Property LeftIdentity() As LeftOperand
        Get
            If Me.functionProperties.ContainsKey("left identity") = False Then
                'First check to see if there is one and generate it along the way
                Me.HasLeftIdentity()
            End If

            If Me.functionProperties.Item("left identity") = False Then
                Throw New Exception("There is no left identity element for this operation.")
            Else
                Return Me.myLeftIdentity
            End If
        End Get
    End Property

#End Region

#Region "  Methods  "

    Public Function HasLeftIdentity() As Boolean
        If Me.functionProperties.ContainsKey("left identity") = False Then
            Dim index1 As Integer
            Dim index2 As Integer
            Dim curLeft As LeftOperand
            Dim curRight As RightOperand
            Dim curTup As New Tuple(2)
            Dim same As Boolean

            For index1 = 0 To Me.LeftOperandDomain.Cardinality - 1
                curLeft = Me.LeftOperandDomain.Element(index1)
                same = True

                For index2 = 0 To Me.RightOperandDomain.Cardinality - 1
                    curRight = Me.RightOperandDomain.Element(index2)

                    curTup.Element(0) = curLeft
                    curTup.Element(1) = curRight
                    If same = True And curRight.Equals(Me.ApplyMap(curTup)) = False Then
                        same = False
                        Exit For
                    End If
                Next index2

                If same = True Then
                    'We found the identity element!

                    Me.functionProperties.Add("left identity", True)
                    Me.myLeftIdentity = curLeft
                    Return True
                End If
            Next index1

            'There is no identity element for this operation
            Me.functionProperties.Add("left identity", False)
            Return False
        Else
            Return Me.functionProperties.Item("left identity")
        End If
    End Function

#End Region

End Class

Public Class FiniteBinaryOperation(Of T As {Class, New, IEquatable(Of T)})
    Inherits FiniteFunction(Of Tuple, T)

    Friend identityElement As T
    Private myCayleyTable As DataTable

#Region "  Constructors  "

    ''' <summary>
    ''' Creates a new binary operation.
    ''' </summary>
    ''' <param name="newCodomain"></param>
    ''' <param name="newRelation"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal newCodomain As FiniteSet(Of T), ByVal newRelation As Map(Of Tuple, T))
        MyBase.New(newCodomain.DirectProduct(newCodomain), newCodomain, newRelation)
    End Sub

    ''' <summary>
    ''' Creates a new binary operation and stores any initially known properties of the operation.
    ''' </summary>
    ''' <param name="newCodomain"></param>
    ''' <param name="newRelation"></param>
    ''' <param name="knownProperties"></param>
    ''' <remarks></remarks>
    Protected Friend Sub New(ByVal newCodomain As FiniteSet(Of T), ByVal newRelation As Map(Of Tuple, T), ByVal knownProperties As Dictionary(Of String, Boolean))
        MyBase.New(newCodomain.DirectProduct(newCodomain), newCodomain, newRelation)

        Dim enumer As Dictionary(Of String, Boolean).Enumerator = knownProperties.GetEnumerator

        Do While enumer.MoveNext = True
            If Me.functionProperties.ContainsKey(enumer.Current.Key) = False Then
                Me.functionProperties.Add(enumer.Current.Key, enumer.Current.Value)
            End If
        Loop
    End Sub

#End Region

#Region "  Properties  "

    Public ReadOnly Property Identity() As T
        Get
            If Me.functionProperties.ContainsKey("identity") = False Then
                'First check to see if there is one and generate it along the way
                Me.HasIdentity()
            End If

            If Me.functionProperties.Item("identity") = False Then
                Throw New Exception("There is no identity element for this operation.")
            Else
                Return Me.identityElement
            End If
        End Get
    End Property

    Public ReadOnly Property InverseOperation() As FiniteBinaryOperation(Of T)
        Get
            If Me.functionProperties.ContainsKey("inverses") = False Then
                Me.HasInverses()
            End If

            If Me.functionProperties.Item("inverses") = False Then
                Throw New Exception("This operation does not have an inverse.")
            Else
                'TODO: Finish!

                Return Nothing
            End If
        End Get
    End Property

#End Region

#Region "  Methods  "

    ''' <summary>
    ''' Determines if the operation is associative. In other words, (a + b) + c = a + (b + c) for all a, b, and c.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Uses Light's algorithm for testing associativity.</remarks>
    Public Function IsAssociative() As Boolean
        If Me.functionProperties.ContainsKey("associativity") = False Then
            Dim domainSize As Integer = Me.Codomain.Cardinality

            Dim keyElementIndex As Integer
            Dim lightTable As New SquareMatrix(Of RealNumber)(domainSize + 1)
            Dim originalTable As DataTable

            Dim colIndex As Integer
            Dim rowIndex As Integer
            Dim cayleyIndex As Integer

            originalTable = Me.CayleyTableGeneric

            Dim cayleyMatrix As New SquareMatrix(Of RealNumber)(domainSize + 1)

            'Turn the Cayley table into a SquareMatrix for easier processing
            For colIndex = 0 To domainSize
                cayleyMatrix.Item(0, colIndex) = New RealNumber(colIndex)
            Next colIndex
            For rowIndex = 0 To domainSize - 1
                For colIndex = 0 To domainSize
                    cayleyMatrix.Item(rowIndex + 1, colIndex) = New RealNumber(CInt(originalTable.Rows(rowIndex).Item(colIndex)))
                Next colIndex
            Next rowIndex

            For keyElementIndex = 1 To domainSize
                'Build header for lightTable
                For colIndex = 1 To domainSize
                    lightTable.Item(0, colIndex) = cayleyMatrix.Item(keyElementIndex, colIndex)
                Next colIndex

                'Fill in lightTable
                For colIndex = 1 To domainSize
                    cayleyIndex = CInt(lightTable.Item(0, colIndex).Value)

                    For rowIndex = 1 To domainSize
                        lightTable.Item(rowIndex, colIndex) = cayleyMatrix.Item(rowIndex, cayleyIndex)
                    Next rowIndex
                Next colIndex

                'Copy the keyElement column into the first column of lightTable
                For rowIndex = 1 To domainSize
                    lightTable.Item(rowIndex, 0) = cayleyMatrix.Item(rowIndex, keyElementIndex)
                Next rowIndex

                'Compare the rows of lightTable to the appropriate rows from cayleyMatrix
                For rowIndex = 1 To domainSize
                    cayleyIndex = CInt(lightTable.Item(rowIndex, 0).Value)

                    For colIndex = 1 To domainSize
                        Dim value1 As Integer
                        Dim value2 As Integer

                        value1 = CInt(lightTable.Item(rowIndex, colIndex).Value)
                        value2 = CInt(cayleyMatrix.Item(cayleyIndex, colIndex).Value)

                        If value1 <> value2 Then
                            Me.functionProperties.Add("associativity", False)
                            Return False
                        End If
                    Next colIndex
                Next rowIndex
            Next keyElementIndex

            Me.functionProperties.Add("associativity", True)
            Return True
        Else
            Return Me.functionProperties.Item("associativity")
        End If
    End Function

    Public Function CayleyTableGeneric() As DataTable
        If Me.functionProperties.ContainsKey("cayley table") = False Then
            Dim domainSize As Integer = Me.Codomain.Cardinality
            Dim colIndex As Integer
            Dim rowIndex As Integer
            Dim cols As New List(Of DataColumn)
            Dim rows As New List(Of DataRow)

            Dim cayTable As New DataTable("Cayley Table")

            cols.Add(New DataColumn(""))
            For colIndex = 1 To domainSize
                cols.Add(New DataColumn(colIndex.ToString))
            Next colIndex
            For colIndex = 0 To domainSize
                cols(colIndex).DataType = System.Type.GetType("System.Int32")

                cayTable.Columns.Add(cols(colIndex))
            Next colIndex

            For rowIndex = 0 To domainSize - 1
                rows.Add(cayTable.NewRow())

                rows(rowIndex).Item(0) = rowIndex + 1


                Parallel.For(1, domainSize + 1, Sub(columnIndex)
                                                    Dim tup As New Tuple(2)

                                                    tup.Element(0) = Me.Codomain.Element(rowIndex)

                                                    tup.Element(1) = Me.Codomain.Element(columnIndex - 1)
                                                    rows(rowIndex).Item(columnIndex) = Me.Codomain.IndexOf(Me.theRelation.ApplyMap(tup)) + 1
                                                End Sub)

                cayTable.Rows.Add(rows(rowIndex))
            Next

            Me.functionProperties.Add("cayley table", True)
            Me.myCayleyTable = cayTable
            Return cayTable
        Else
            Return Me.myCayleyTable
        End If
    End Function

    Private Sub CheckAssociative(ByVal index As Integer)

    End Sub

    ''' <summary>
    ''' Determines if the operation is commutative. In other words, a + b = b + a for all a and b.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsCommutative() As Boolean
        If Me.functionProperties.ContainsKey("commutivity") = False Then
            Dim domainSize As Integer = Me.Codomain.Cardinality
            Dim index1 As Integer
            Dim index2 As Integer
            Dim tup1 As New Tuple(2)
            Dim tup2 As New Tuple(2)
            Dim lhs As T
            Dim rhs As T

            For index1 = 0 To domainSize - 1
                For index2 = index1 To domainSize - 1
                    tup1.Element(0) = Me.Codomain.Element(index1)
                    tup1.Element(1) = Me.Codomain.Element(index2)
                    lhs = Me.ApplyMap(tup1)

                    tup2.Element(0) = Me.Codomain.Element(index2)
                    tup2.Element(1) = Me.Codomain.Element(index1)
                    rhs = Me.ApplyMap(tup2)

                    If lhs.Equals(rhs) = False Then
                        Me.functionProperties.Add("commutivity", False)
                        Return False
                    End If
                Next index2
            Next index1

            Me.functionProperties.Add("commutivity", True)
            Return True
        Else
            Return Me.functionProperties.Item("commutivity")
        End If
    End Function

    ''' <summary>
    ''' Determines if the operation is idempotent. In other words a + a = a for all a.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsIdempotent() As Boolean
        If Me.functionProperties.ContainsKey("idempotent") = False Then
            Dim index As Integer
            Dim tup As New Tuple(2)

            For index = 0 To Me.Codomain.Cardinality - 1
                tup.Element(0) = Me.Codomain.Element(index)
                tup.Element(1) = Me.Codomain.Element(index)

                If Me.ApplyMap(tup).Equals(Me.Codomain.Element(index)) = False Then
                    Me.functionProperties.Add("idempotent", False)
                    Return False
                End If
            Next index

            Me.functionProperties.Add("idempotent", True)
            Return True
        Else
            Return Me.functionProperties.Item("idempotent")
        End If
    End Function

    ''' <summary>
    ''' Determines if the operation has an identity element. In other words there exists an e such that for all a, a + e = e + a = a.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function HasIdentity() As Boolean
        If Me.functionProperties.ContainsKey("identity") = False Then
            Dim index1 As Integer
            Dim index2 As Integer
            Dim curT As T
            Dim otherT As T
            Dim curTup As New Tuple(2)
            Dim same As Boolean

            For index1 = 0 To Me.Codomain.Cardinality - 1
                curT = Me.Codomain.Element(index1)
                same = True

                For index2 = 0 To Me.Codomain.Cardinality - 1
                    otherT = Me.Codomain.Element(index2)

                    curTup.Element(0) = curT
                    curTup.Element(1) = otherT
                    If same = True And otherT.Equals(Me.ApplyMap(curTup)) = False Then
                        same = False
                    End If

                    curTup.Element(0) = otherT
                    curTup.Element(1) = curT
                    If same = True And otherT.Equals(Me.ApplyMap(curTup)) = False Then
                        same = False
                    End If
                Next index2

                If same = True Then
                    'We found the identity element!

                    Me.functionProperties.Add("identity", True)
                    Me.identityElement = curT
                    Return True
                End If
            Next index1

            'There is no identity element for this operation
            Me.functionProperties.Add("identity", False)
            Return False
        Else
            Return Me.functionProperties.Item("identity")
        End If
    End Function

    ''' <summary>
    ''' Determines if the operation has inverses for all elements in the domain. In other words there exists an i such that for all a, a + i = i + a = e.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function HasInverses() As Boolean
        If Me.functionProperties.ContainsKey("inverses") = False Then
            If Me.functionProperties.ContainsKey("identity") = False Then
                'First check to see if there's an identity element
                Me.HasIdentity()
            End If
            If Me.functionProperties.Item("identity") = False Then
                Me.functionProperties.Add("inverses", False)
                Return False
            End If

            Dim theSet As FiniteSet(Of T)
            Dim index As Integer
            Dim curT As T
            Dim otherT As T
            Dim curTup As New Tuple(2)
            Dim same As Boolean

            theSet = Me.Codomain.Clone

            'The identity element is its own inverse, so we don't need to check it
            theSet.DeleteElement(theSet.IndexOf(Me.Identity))

            Do While theSet.Cardinality > 0
                curT = theSet.Element(0)

                For index = 0 To theSet.Cardinality - 1
                    same = True

                    otherT = theSet.Element(index)

                    curTup.Element(0) = curT
                    curTup.Element(1) = otherT
                    If same = True And Me.identityElement.Equals(Me.ApplyMap(curTup)) = False Then
                        same = False
                    End If

                    curTup.Element(0) = otherT
                    curTup.Element(1) = curT
                    If same = True And Me.identityElement.Equals(Me.ApplyMap(curTup)) = False Then
                        same = False
                    End If

                    If same = True Then
                        Exit For
                    End If
                Next index

                If same = False Then
                    Me.functionProperties.Add("inverses", False)
                    Return False
                Else
                    'We found a pair, so remove them and keep on chugging
                    If index = 0 Then
                        'It is its own inverse
                        theSet.DeleteElement(0)
                    Else
                        theSet.DeleteElement(index)
                        theSet.DeleteElement(0)
                    End If
                End If
            Loop

            Me.functionProperties.Add("inverses", True)
            Return True
        Else
            Return Me.functionProperties.Item("inverses")
        End If
    End Function

    ''' <summary>
    ''' Determines if the operation has inverses for all elements in the domain except one specified element.
    ''' </summary>
    ''' <param name="except"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function HasInversesExcept(ByVal except As T) As Boolean
        If Me.functionProperties.ContainsKey("identity") = False Then
            'First check to see if there's an identity element
            Me.HasIdentity()
        End If
        If Me.functionProperties.Item("identity") = False Then
            Return False
        End If

        Dim index1 As Integer
        Dim index2 As Integer
        Dim curT As T
        Dim otherT As T
        Dim curTup As New Tuple(2)
        Dim same As Boolean

        For index1 = 0 To Me.Codomain.Cardinality - 1
            curT = Me.Codomain.Element(index1)

            For index2 = 0 To Me.Codomain.Cardinality - 1
                same = True

                otherT = Me.Codomain.Element(index2)

                curTup.Element(0) = curT
                curTup.Element(1) = otherT
                If same = True And Me.identityElement.Equals(Me.ApplyMap(curTup)) = False Then
                    same = False
                End If

                curTup.Element(0) = otherT
                curTup.Element(1) = curT
                If same = True And Me.identityElement.Equals(Me.ApplyMap(curTup)) = False Then
                    same = False
                End If

                If same = True Then
                    Exit For
                End If
            Next index2

            If same = False And curT.Equals(except) = False Then
                Return False
            End If
        Next index1

        Return True
    End Function

    ''' <summary>
    ''' Returns the inverse of the element. In other words, given a this function returns b such that a + b = b + a = e.
    ''' </summary>
    ''' <param name="elem"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InverseElement(ByVal elem As T) As T
        If Me.HasIdentity = False Then
            Throw New Exception("This operation does not have an identity element.")
        End If

        Dim index As Integer
        Dim tup1 As New Tuple(2)
        Dim tup2 As New Tuple(2)

        For index = 0 To Me.Codomain.Cardinality - 1
            tup1.Element(0) = elem
            tup1.Element(1) = Me.Codomain.Element(index)
            tup2.Element(0) = Me.Codomain.Element(index)
            tup2.Element(1) = elem

            If Me.ApplyMap(tup1).Equals(Me.Identity) = True And Me.ApplyMap(tup2).Equals(Me.Identity) = True Then
                Return Me.Codomain.Element(index)
            End If

        Next index

        Throw New Exception("This element does not have an inverse.")
    End Function

    ''' <summary>
    ''' Returns the restriction of this operation.
    ''' </summary>
    ''' <param name="newCodomain"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shadows Function Restriction(ByVal newCodomain As FiniteSet(Of T)) As FiniteBinaryOperation(Of T)
        If newCodomain.IsSubsetOf(Me.Codomain) = False Then
            Throw New Exception("The newCodomain is not a subset of this operation's Codomain.")
        End If

        Dim newOp As New FiniteBinaryOperation(Of T)(newCodomain, Me.theRelation)

        'Restriction of a injective function is still injective
        If Me.functionProperties.ContainsKey("injective") = True Then
            If Me.functionProperties.Item("injective") = True Then
                newOp.functionProperties.Add("injective", True)
            End If
        End If

        'Restriction of an associative operation is still associative
        If Me.functionProperties.ContainsKey("associativity") = True Then
            If Me.functionProperties.Item("associativity") = True Then
                newOp.functionProperties.Add("associativity", True)
            End If
        End If

        'Restriction of an commutative operation is still commutative
        If Me.functionProperties.ContainsKey("commutivitiy") = True Then
            If Me.functionProperties.Item("commutivitiy") = True Then
                newOp.functionProperties.Add("commutivitiy", True)
            End If
        End If

        'Check to see if it has an identity
        If Me.functionProperties.ContainsKey("identity") = True Then
            If Me.functionProperties.Item("identity") = True Then
                If newCodomain.Contains(Me.Identity) = True Then
                    newOp.identityElement = Me.Identity
                    newOp.functionProperties.Add("identity", True)
                Else
                    newOp.functionProperties.Add("identity", False)
                End If
            End If
        End If

        'Restriction of an idempotent operation is still idempotent
        If Me.functionProperties.ContainsKey("idempotent") = True Then
            If Me.functionProperties.Item("idempotent") = True Then
                newOp.functionProperties.Add("idempotent", True)
            End If
        End If

        Return newOp
    End Function

#End Region

End Class
