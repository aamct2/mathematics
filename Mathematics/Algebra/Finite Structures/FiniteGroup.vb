
Imports System.Collections.Generic
Imports System.Data
Imports System.Threading.Tasks

Imports System.Diagnostics

Namespace Algebra

    ''' <summary>
    ''' Represents a finite group with elements of type T.
    ''' </summary>
    ''' <typeparam name="T">The Type of element in the group.</typeparam>
    ''' <remarks></remarks>
    Public Class FiniteGroup(Of T As {Class, New, IEquatable(Of T)})
        Inherits FiniteMonoid(Of T)
        Implements IEquatable(Of FiniteGroup(Of T))

        Private allSubgroups As FiniteSet(Of FiniteGroup(Of T))
        Private allNormalSubgroups As FiniteSet(Of FiniteGroup(Of T))

        Protected Friend groupProperties As New Dictionary(Of String, Boolean)

#Region "  Constructors  "

        ''' <summary>
        ''' Creates a new finite group.
        ''' </summary>
        ''' <param name="newSet">The set for the group.</param>
        ''' <param name="newOperation">The operation for the group.</param>
        ''' <exception cref="DoesNotSatisfyPropertyException">Throws DoesNotSatisfyPropertyException if 'newOperation' does not have an identity element, does not have inverses for every element, or is not associative.</exception>
        ''' <remarks></remarks>
        Public Sub New(ByVal newSet As FiniteSet(Of T), ByVal newOperation As FiniteBinaryOperation(Of T))
            MyBase.New(newSet, newOperation)

            If newOperation.HasInverses = False Then
                Throw New DoesNotSatisfyPropertyException("The new operation does not have inverses for every element.")
            End If
        End Sub

        ''' <summary>
        ''' Creates a new finite group and stores any initially known properties of the group.
        ''' </summary>
        ''' <param name="newSet">The set for the group.</param>
        ''' <param name="newOperation">The operation for the group.</param>
        ''' <param name="knownProperties">Any known properties about the group already.</param>
        ''' <exception cref="DoesNotSatisfyPropertyException">Throws DoesNotSatisfyPropertyException if 'newOperation' does not have an identity element, does not have inverses for every element, or is not associative.</exception>
        ''' <remarks></remarks>
        Protected Friend Sub New(ByVal newSet As FiniteSet(Of T), ByVal newOperation As FiniteBinaryOperation(Of T), ByVal knownProperties As Dictionary(Of String, Boolean))
            MyBase.New(newSet, newOperation)

            If newOperation.HasInverses = False Then
                Throw New DoesNotSatisfyPropertyException("The new operation does not have inverses for every element.")
            End If

            Dim enumer As Dictionary(Of String, Boolean).Enumerator = knownProperties.GetEnumerator

            Do While enumer.MoveNext = True
                If Me.groupProperties.ContainsKey(enumer.Current.Key) = False Then
                    Me.groupProperties.Add(enumer.Current.Key, enumer.Current.Value)
                End If
            Loop
        End Sub

#End Region

#Region "  Methods  "

        ''' <summary>
        ''' Returns the center of the group as a set. In other words, the set of all elements that commute with every other element in the group.
        ''' </summary>
        ''' <returns>Returns the center of the group as a set.</returns>
        ''' <remarks></remarks>
        Public Function Center() As FiniteSet(Of T)
            Dim newSet As New FiniteSet(Of T)

            Dim index1 As Integer
            Dim index2 As Integer
            Dim InCenter As Boolean

            For index1 = 0 To Me.Order - 1
                InCenter = True

                For index2 = 0 To Me.Order - 1
                    If Me.Commutator(Me.theSet.Element(index1), Me.theSet.Element(index2)).Equals(Me.IdentityElement) = False Then
                        InCenter = False
                        Exit For
                    End If
                Next index2

                If InCenter = True Then
                    newSet.AddElement(Me.theSet.Element(index1))
                End If
            Next index1
            Return newSet
        End Function

        ''' <summary>
        ''' Returns the center of the group as a group. (In fact, this group is always abelian.)
        ''' </summary>
        ''' <returns>Returns the center of the group as a group.</returns>
        ''' <remarks></remarks>
        Public Function CenterGroup() As FiniteGroup(Of T)
            Dim knownProperties As Dictionary(Of String, Boolean) = Me.SubgroupClosedProperties
            Dim newSet As FiniteSet(Of T) = Me.Center

            If knownProperties.ContainsKey("abelian") = False Then
                knownProperties.Add("abelian", True)
            End If

            Dim newOp As FiniteBinaryOperation(Of T) = Me.Operation.Restriction(newSet)

            Return New FiniteGroup(Of T)(newSet, newOp, knownProperties)
        End Function

        ''' <summary>
        ''' Returns the centralizer (as a group) of a subset of a group.
        ''' </summary>
        ''' <param name="subgroup"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function CentralizerSubgroup(ByVal subgroup As FiniteGroup(Of T)) As FiniteGroup(Of T)
            Return New FiniteGroup(Of T)(Me.Centralizer(subgroup.theSet), Me.Operation)
        End Function

        ''' <summary>
        ''' Returns the centralizer (as a subset) of a subset of a group.
        ''' </summary>
        ''' <param name="subset"></param>
        ''' <returns></returns>
        ''' <remarks>C_G(S) = {g \in G | sg = gs \forall s \in S}</remarks>
        Public Function Centralizer(ByVal subset As FiniteSet(Of T)) As FiniteSet(Of T)
            If subset.IsSubsetOf(Me.theSet) = False Then
                Throw New Exception("The 'subset' variable is not actually a subset of the group.")
            End If

            Dim newSet As New FiniteSet(Of T)
            Dim setOfCentralizers As New FiniteSet(Of FiniteSet(Of T))

            Dim index As Integer
            For index = 0 To subset.Cardinality - 1
                Dim curElem As T = subset.Element(index)
                Dim curCentralizer As FiniteSet(Of T) = Me.Centralizer(curElem)

                If (curCentralizer.Cardinality > 0) Then
                    setOfCentralizers.AddElement(curCentralizer)
                End If
            Next index

            If (setOfCentralizers.Cardinality > 0) Then
                newSet = setOfCentralizers.Element(0)

                For index = 1 To setOfCentralizers.Cardinality - 1
                    Dim curSet As FiniteSet(Of T) = setOfCentralizers.Element(index)

                    newSet = newSet.Intersection(curSet)
                Next index
            End If

            Return newSet
        End Function

        ''' <summary>
        ''' Returns the centralizer, with respect to this group, of a given element of the group.
        ''' </summary>
        ''' <param name="elem">The element whose centralizer, with respect to this group, we are to form.</param>
        ''' <exception cref="NotMemberOfException">Throws NotMemberOfException if parameter 'elem' is not a member of the group.</exception>
        ''' <returns>Returns the centralizer of a given element as a <c>FiniteSet</c>.</returns>
        ''' <remarks></remarks>
        Public Function Centralizer(ByVal elem As T) As FiniteSet(Of T)
            If Me.theSet.Contains(elem) = False Then
                Throw New NotMemberOfException("The parameter 'elem' is not a member of the group.")
            End If

            Dim newSet As New FiniteSet(Of T)

            'Dim index As Integer

            'TODO: Test!
            Parallel.For(0, Me.Order - 1, Sub(index)
                                              If Me.Commutator(elem, Me.theSet.Element(index)).Equals(Me.IdentityElement) = True Then
                                                  newSet.AddElement(Me.theSet.Element(index))
                                              End If
                                          End Sub)

            'For index = 0 To Me.Order - 1
            '    If Me.Commutator(elem, Me.theSet.Element(index)).Equals(Me.IdentityElement) = True Then
            '        newSet.AddElement(Me.theSet.Element(index))
            '    End If
            'Next index

            Return newSet
        End Function

        ''' <summary>
        ''' Returns the conjugacy class of an element.
        ''' </summary>
        ''' <param name="elem">The element whose conjugacy class is to be returned.</param>
        ''' <exception cref="NotMemberOfException">Throws NotMemberOfException if parameter 'elem' is not a member of the group.</exception>
        ''' <returns>Returns the conjugacy class of an element as a <c>FiniteSet</c>.</returns>
        ''' <remarks></remarks>
        Public Function ConjugacyClass(ByVal elem As T) As FiniteSet(Of T)
            If Me.theSet.Contains(elem) = False Then
                Throw New NotMemberOfException("The parameter 'elem' is not an element of this group.")
            End If

            Dim newSet As New FiniteSet(Of T)
            Dim index As Integer
            Dim tup1 As New Tuple(2)
            Dim tup2 As New Tuple(2)

            tup1.Element(1) = elem
            For index = 0 To Me.theSet.Cardinality - 1
                tup1.Element(0) = Me.theSet.Element(index)
                tup2.Element(0) = Me.ApplyOperation(tup1)
                tup2.Element(1) = Me.Operation.InverseElement(Me.theSet.Element(index))

                newSet.AddElement(Me.ApplyOperation(tup2))
            Next index

            Return newSet
        End Function

        ''' <summary>
        ''' Returns the commutator of two elements, in a sense given an indication of how badly the operation fails to commute for two elements. [g,h] = g^-1 + h^-1 + g + h.
        ''' </summary>
        ''' <param name="lhs">The first element.</param>
        ''' <param name="rhs">The second element.</param>
        ''' <exception cref="NotMemberOfException">Throws NotMemberOfException if parameter 'lhs' or 'rhs' is not a member of the group.</exception>
        ''' <returns>Returns the commutator of two elements.</returns>
        ''' <remarks></remarks>
        Public Function Commutator(ByVal lhs As T, ByVal rhs As T) As T
            Dim tup1 As New Tuple(2)
            Dim tup2 As New Tuple(2)
            Dim tup3 As New Tuple(2)

            If Me.theSet.Contains(lhs) = False Or Me.theSet.Contains(rhs) = False Then
                Throw New NotMemberOfException("The parameter 'lhs' or 'rhs' is not a member of the group.")
            End If

            tup1.Element(0) = Me.Operation.InverseElement(lhs)
            tup1.Element(1) = Me.Operation.InverseElement(rhs)
            tup2.Element(0) = Me.Operation.ApplyMap(tup1)
            tup2.Element(1) = lhs
            tup3.Element(0) = Me.Operation.ApplyMap(tup2)
            tup3.Element(1) = rhs

            Return Me.ApplyOperation(tup3)
        End Function

        ''' <summary>
        ''' Returns the derived subgroup (also known as the commutator subgroup) of this group. In other words, the group whose elements are all the possible commutators of this group.
        ''' </summary>
        ''' <returns>Returns the derived subgroup.</returns>
        ''' <param name="useParallel">Optional: Determines whether a parallel or sequential algorithm is used. Default is to use the parallel algorithm.</param>
        ''' <remarks></remarks>
        Public Function DerivedSubgroup(Optional ByVal useParallel As Boolean = True) As FiniteGroup(Of T)
            Dim newSet As New FiniteSet(Of T)
            Dim generatorSet As New FiniteSet(Of T)
            Dim newOperation As FiniteBinaryOperation(Of T)
            Dim index1 As Integer
            Dim index2 As Integer

            If useParallel = True Then
                Parallel.For(0, Me.Order, Sub(index)
                                              For index2 = 0 To Me.Order - 1
                                                  generatorSet.AddElement(Me.Commutator(Me.theSet.Element(index), Me.theSet.Element(index2)))
                                              Next index2
                                          End Sub)
            Else
                For index1 = 0 To Me.Order - 1
                    For index2 = 0 To Me.Order - 1
                        generatorSet.AddElement(Me.Commutator(Me.theSet.Element(index1), Me.theSet.Element(index2)))
                    Next index2
                Next index1
            End If

            Try
                newSet = FiniteGroup(Of T).GeneratedSet(generatorSet, Me.Operation.theRelation)
            Catch ex As Exception
                Throw New Exception("DerivedSubroup is unable to generate a set based on the set of commutators.")
            End Try

            If newSet.Contains(Me.IdentityElement) = False Then
                Stop
            End If

            newOperation = Me.Operation.Restriction(newSet)

            Return New FiniteGroup(Of T)(newSet, newOperation, Me.SubgroupClosedProperties)
        End Function

        ''' <summary>
        ''' Returns the direct product of this group with a given group.
        ''' </summary>
        ''' <typeparam name="G">The Type of elements in the second group.</typeparam>
        ''' <param name="otherGroup">The other finite group to take the direct product with.</param>
        ''' <returns>Returns the direct product of this group with another group.</returns>
        ''' <remarks></remarks>
        Public Function DirectProduct(Of G As {Class, New, IEquatable(Of G)})(ByVal otherGroup As FiniteGroup(Of G)) As FiniteGroup(Of Tuple)
            Dim newSet As FiniteSet(Of Tuple)

            newSet = Me.theSet.DirectProduct(Of G)(otherGroup.theSet)

            Dim newMap As New DirectProductMap(Of G)(Me.Operation.theRelation, otherGroup.Operation.theRelation)

            Dim newOpProps As New Dictionary(Of String, Boolean)

            newOpProps.Add("associativity", True)
            newOpProps.Add("inverses", True)

            Dim newOp As New FiniteBinaryOperation(Of Tuple)(newSet, newMap, newOpProps)

            Return New FiniteGroup(Of Tuple)(newSet, newOp, Me.ProductClosedProperties(Of G)(otherGroup))
        End Function

        ''' <summary>
        ''' Returns the double coset of a given element with two given subgroup.
        ''' </summary>
        ''' <param name="leftSubgroup">The subgroup with which to form the left coset.</param>
        ''' <param name="rightSubgroup">The subgroup with which to form the right coset.</param>
        ''' <param name="elem">The element whose double coset is to be returned.</param>
        ''' <exception cref="NotSubgroupException">Thorws NotSubgroupException if parameter 'leftSubgroup' or 'rightSubgroup' is not a subgroup of this group.</exception>
        ''' <returns>Returns the double coset of a given element with two given subgroups.</returns>
        ''' <remarks></remarks>
        Public Function DoubleCoset(ByVal leftSubgroup As FiniteGroup(Of T), ByVal rightSubgroup As FiniteGroup(Of T), ByVal elem As T) As FiniteSet(Of T)
            If leftSubgroup.IsSubgroupOf(Me) = False Then
                Throw New NotSubgroupException("The parameter 'leftSubgroup' is not a subgroup of this group.")
            ElseIf rightSubgroup.IsSubgroupOf(Me) = False Then
                Throw New NotSubgroupException("The parameter 'rightSubgroup' is not a subgroup of this group.")
            End If

            Dim newSet As New FiniteSet(Of T)
            Dim leftIndex As Integer
            Dim rightIndex As Integer
            Dim tup1 As New Tuple(2)
            Dim tup2 As New Tuple(2)

            For leftIndex = 0 To leftSubgroup.Order - 1
                For rightIndex = 0 To rightSubgroup.Order - 1
                    tup1.Element(0) = leftSubgroup.theSet.Element(leftIndex)
                    tup1.Element(1) = elem
                    tup2.Element(0) = Me.ApplyOperation(tup1)
                    tup2.Element(1) = rightSubgroup.theSet.Element(rightIndex)

                    newSet.AddElement(Me.ApplyOperation(tup2))
                Next rightIndex
            Next leftIndex

            Return newSet
        End Function

        ''' <summary>
        ''' Determines whether two groups are equal. In other words, if two groups have the same set and operation.
        ''' </summary>
        ''' <param name="other">The other finite group to compare with.</param>
        ''' <returns>Returns <c>True</c> if the two groups have the same set and operation, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Shadows Function Equals(ByVal other As FiniteGroup(Of T)) As Boolean Implements System.IEquatable(Of FiniteGroup(Of T)).Equals
            If Me.theSet.Equals(other.theSet) = True And Me.Operation.Equals(other.Operation) = True Then
                Return True
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' Returns the Frattini subgroup of this group. In other words, the intersection of all maximal subgroups. If the group has no maximal subgroups, the Frattini subgroup is the whole group.
        ''' </summary>
        ''' <returns>Returns the frattini subgroup of this group.</returns>
        ''' <remarks></remarks>
        Public Function FrattiniSubgroup() As FiniteGroup(Of T)
            Dim maxSubgroups As FiniteSet(Of FiniteGroup(Of T)) = Me.SetOfAllMaximalSubgroups()

            If maxSubgroups.Cardinality = 0 Then
                Return Me
            End If

            Dim newSet As New FiniteSet(Of T)
            Dim index As Integer

            newSet = newSet.Union(maxSubgroups.Element(0).theSet)
            For index = 1 To maxSubgroups.Cardinality - 1
                newSet = newSet.Intersection(maxSubgroups.Element(index).theSet)
            Next index

            Dim newOp As FiniteBinaryOperation(Of T) = Me.Operation.Restriction(newSet)

            Return New FiniteGroup(Of T)(newSet, newOp, Me.SubgroupClosedProperties)
        End Function

        ''' <summary>
        ''' Determines whether a given element generates the group.
        ''' </summary>
        ''' <param name="elem">The element to test as a generator of the group.</param>
        ''' <exception cref="NotMemberOfException">Throws NotMemberOfException if parameter 'elem' is not a member of the group.</exception>
        ''' <returns>Returns <c>True</c> if a given element generates the whole group, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Function GeneratesGroup(ByVal elem As T) As Boolean
            If Me.theSet.Contains(elem) = False Then
                Throw New NotMemberOfException("The parameter 'elem' is not a member of this group.")
            End If

            If Me.Order(elem) = Me.Order Then
                Return True
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' Attempts to generate a set given a set of generators and a map.
        ''' </summary>
        ''' <param name="generatorSet">The set of generators.</param>
        ''' <param name="theMap">The map to apply on the set of generators.</param>
        ''' <returns>A set generated by a set of generators and a map.</returns>
        ''' <exception cref="Exception">Throws Exception if it is not able to generate the set after 3000 cycles. This means the set would probably be infinite.</exception>
        ''' <exception cref="UndefiniedException">Throws UndefinedException if while trying to apply theMap, a tuple of elements are not members of the domain.</exception>
        ''' <remarks></remarks>
        Public Shared Function GeneratedSet(ByVal generatorSet As FiniteSet(Of T), ByVal theMap As Map(Of Tuple, T)) As FiniteSet(Of T)
            Dim index1 As Integer
            Dim index2 As Integer
            Dim result As FiniteSet(Of T)
            Dim cycleIndex As Integer
            Dim foundNewElement As Boolean
            Dim curElem As T
            Dim curTup As Tuple

            cycleIndex = 0
            result = generatorSet.Clone()

            Do Until cycleIndex >= 3000
                foundNewElement = False

                For index1 = result.Cardinality - 1 To 0 Step -1
                    curTup = New Tuple(2)
                    curTup.Element(0) = result.Element(index1)

                    For index2 = result.Cardinality - 1 To 0 Step -1
                        curTup.Element(1) = result.Element(index2)
                        Try
                            curElem = theMap.ApplyMap(curTup)
                        Catch ex As Exception
                            Throw New UndefiniedException("Error: In GeneratedSet, the tuple " & curTup.ToString & " is not a member of the domain of theMap.")
                        End Try
                        If result.Contains(curElem) = False Then
                            result.AddElement(curElem)
                            foundNewElement = True
                        End If
                    Next index2
                Next index1

                If foundNewElement = False Then
                    Return result
                End If

                cycleIndex += 1
            Loop

            Throw New Exception("Error: GeneratedSet went on an infinite loop!")
        End Function

        ''' <summary>
        ''' Returns the group table for the group where each entry uses the index of the element in the set as the element's name.
        ''' </summary>
        ''' <param name="useParallel">Optional: Determines whether the group table is calculated using a parallel algorithm. By default, the algorithm is parallel.</param>
        ''' <returns>Returns the group table for the group as a <c>DataTable</c> using the index of the element in the set as the element's name.</returns>
        ''' <remarks></remarks>
        Public Function GroupTableGeneric(Optional ByVal useParallel As Boolean = True) As DataTable
            Dim colIndex As Integer
            Dim rowIndex As Integer
            Dim cols As New List(Of DataColumn)
            Dim rows As New List(Of DataRow)
            Dim tup As New Tuple(2)

            Dim grpTable As New DataTable("Group Table")

            cols.Add(New DataColumn(""))
            For colIndex = 1 To Me.Order
                cols.Add(New DataColumn(colIndex.ToString))
            Next colIndex
            For colIndex = 0 To Me.Order
                cols(colIndex).DataType = System.Type.GetType("System.Int32")

                grpTable.Columns.Add(cols(colIndex))
            Next colIndex

            For rowIndex = 0 To Me.Order - 1
                rows.Add(grpTable.NewRow())

                rows(rowIndex).Item(0) = rowIndex + 1

                If useParallel = True Then
                    Parallel.For(1, Me.Order + 1, Sub(columnIndex)
                                                      Dim theTup As New Tuple(2)

                                                      theTup.Element(0) = Me.theSet.Element(rowIndex)

                                                      theTup.Element(1) = Me.theSet.Element(columnIndex - 1)
                                                      rows(rowIndex).Item(columnIndex) = Me.theSet.IndexOf(Me.ApplyOperation(theTup)) + 1
                                                  End Sub)
                Else
                    tup.Element(0) = Me.theSet.Element(rowIndex)

                    For colIndex = 1 To Me.Order
                        tup.Element(1) = Me.theSet.Element(colIndex - 1)
                        rows(rowIndex).Item(colIndex) = Me.theSet.IndexOf(Me.ApplyOperation(tup)) + 1
                    Next colIndex
                End If

                grpTable.Rows.Add(rows(rowIndex))
            Next

            Return grpTable
        End Function

        ''' <summary>
        ''' Returns the group table for the group where each entry uses T.ToString as the element's name.
        ''' </summary>
        ''' <returns>Returns the group table for the group as a <c>DataTable</c> using the <c>ToString</c> method of the element as the element's name.</returns>
        ''' <remarks></remarks>
        Public Function GroupTableSpecific() As DataTable
            Dim colIndex As Integer
            Dim rowIndex As Integer
            Dim cols As New List(Of DataColumn)
            Dim rows As New List(Of DataRow)
            Dim tup As New Tuple(2)

            Dim grpTable As New DataTable("Group Table")

            cols.Add(New DataColumn(""))
            For colIndex = 1 To Me.Order
                cols.Add(New DataColumn(colIndex.ToString))
            Next colIndex
            For colIndex = 0 To Me.Order
                cols(colIndex).DataType = System.Type.GetType("System.String")

                grpTable.Columns.Add(cols(colIndex))
            Next colIndex

            For rowIndex = 0 To Me.Order - 1
                rows.Add(grpTable.NewRow())

                rows(rowIndex).Item(0) = rowIndex + 1

                tup.Element(0) = Me.theSet.Element(rowIndex)

                For colIndex = 1 To Me.Order
                    tup.Element(1) = Me.theSet.Element(colIndex - 1)
                    rows(rowIndex).Item(colIndex) = Me.ApplyOperation(tup).ToString
                Next colIndex

                grpTable.Rows.Add(rows(rowIndex))
            Next

            Return grpTable
        End Function

        ''' <summary>
        ''' Determines whether this group is abelian. In other words, if its operation is commutative.
        ''' </summary>
        ''' <returns>Returns <c>True</c> if the group is abelian, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Function IsAbelian() As Boolean
            If Me.groupProperties.ContainsKey("abelian") = False Then
                Me.groupProperties.Add("abelian", Me.Operation.IsCommutative)
            End If

            Return Me.groupProperties.Item("abelian")
        End Function

        ''' <summary>
        ''' Determines whether this group is ambivalent. In other words, if every element and its inverse are conjugates.
        ''' </summary>
        ''' <returns>Returns <c>True</c> if the group is ambivalent, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Function IsAmbivalent() As Boolean
            If Me.groupProperties.ContainsKey("ambivalent") = False Then
                Dim index As Integer
                Dim curElem As T
                Dim curInverse As T

                For index = 0 To Me.Order - 1
                    curElem = Me.theSet.Element(index)
                    curInverse = Me.Operation.InverseElement(curElem)
                    If Me.IsConjugate(curElem, curInverse) = False Then
                        Me.groupProperties.Add("ambivalent", False)
                        Return False
                    End If
                Next index

                Me.groupProperties.Add("ambivalent", True)
                Return True
            Else
                Return Me.groupProperties.Item("ambivalent")
            End If
        End Function

        ''' <summary>
        ''' Determines whether two elements of the group are conjugate to eachother. Two elements A and B are conjugate if there exists a G such that G + A + G^-1 = B.
        ''' </summary>
        ''' <param name="elem1">The first element.</param>
        ''' <param name="elem2">The second element.</param>
        ''' <exception cref="NotMemberOfException">Throws NotMemberOfException if parameter 'elem1' or 'elem2' is not a member of the group.</exception>
        ''' <returns>Returns <c>True</c> if the two elements are conjugates, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Function IsConjugate(ByVal elem1 As T, ByVal elem2 As T) As Boolean
            If Me.theSet.Contains(elem1) = False Then
                Throw New NotMemberOfException("The parameter 'elem1' is not a member of this group.")
            ElseIf Me.theSet.Contains(elem2) = False Then
                Throw New NotMemberOfException("The parameter 'elem2' is not a member of this group.")
            End If

            Dim index As Integer
            Dim tup1 As New Tuple(2)
            Dim tup2 As New Tuple(2)

            tup1.Element(1) = elem1

            For index = 0 To Me.theSet.Cardinality - 1
                tup1.Element(0) = Me.theSet.Element(index)
                tup2.Element(0) = Me.ApplyOperation(tup1)
                tup2.Element(1) = Me.Operation.InverseElement(Me.theSet.Element(index))

                If Me.ApplyOperation(tup2).Equals(elem2) = True Then
                    Return True
                End If
            Next index

            Return False
        End Function

        ''' <summary>
        ''' Determines whether or not this group is cyclic.
        ''' </summary>
        ''' <returns>Returns <c>True</c> if the group is cyclic, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Function IsCyclic() As Boolean
            If Me.groupProperties.ContainsKey("cyclic") = False Then
                Dim index As Integer

                For index = 0 To Me.Order() - 1
                    If Me.GeneratesGroup(Me.theSet.Element(index)) = True Then
                        Me.groupProperties.Add("cyclic", True)
                        Return True
                    End If
                Next index

                Me.groupProperties.Add("cyclic", False)
                Return False
            Else
                Return Me.groupProperties.Item("cyclic")
            End If
        End Function

        ''' <summary>
        ''' Determines whether or not this group is a Dedekind group. In other words, that all its subgroups are normal.
        ''' </summary>
        ''' <returns>Returns <c>True</c> if the group is a Dedekind group, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Function IsDedekind() As Boolean
            If Me.groupProperties.ContainsKey("dedekind") = False Then
                'Abelian implies Dedekind, check to see if we've calculated that already.
                If Me.groupProperties.ContainsKey("abelian") = True Then
                    If Me.groupProperties.Item("abelian") = True Then
                        Me.groupProperties.Add("dedekind", True)
                        Return True
                    End If
                End If

                'Nilpotent + T-Group implies Dedekind, check to see if we've calculated that already.
                If Me.groupProperties.ContainsKey("nilpotent") = True And Me.groupProperties.ContainsKey("t-group") = True Then
                    If Me.groupProperties.Item("nilpotent") = True And Me.groupProperties.Item("t-group") Then
                        Me.groupProperties.Add("dedekind", True)
                        Return True
                    End If
                End If

                If Me.SetOfAllSubgroups.Equals(Me.SetOfAllNormalSubgroups) = True Then
                    Me.groupProperties.Add("dedekind", True)
                    Return True
                Else
                    Me.groupProperties.Add("dedekind", False)
                    Return False
                End If
            Else
                Return Me.groupProperties.Item("dedekind")
            End If
        End Function

        ''' <summary>
        ''' Determines whether or not this group is a Hamiltonian group. In other words, a non-abelian Dedekind group.
        ''' </summary>
        ''' <returns>Returns <c>True</c> if the group is a Hamiltonian group, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Function IsHamiltonian() As Boolean
            If Me.groupProperties.ContainsKey("hamiltonian") = False Then
                If Me.IsDedekind = True And Me.IsAbelian = False Then
                    Me.groupProperties.Add("hamiltonian", True)
                    Return True
                Else
                    Me.groupProperties.Add("hamiltonian", False)
                    Return False
                End If
            Else
                Return Me.groupProperties.Item("hamiltonian")
            End If
        End Function

        ''' <summary>
        ''' Determines whether a given finite set and operation will form a finite group.
        ''' </summary>
        ''' <param name="testSet">The finite set to test.</param>
        ''' <param name="testOperation">The operation to test.</param>
        ''' <returns>Returns <c>True</c> if a given set and operation will form a group, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Shared Function IsGroup(ByVal testSet As FiniteSet(Of T), ByVal testOperation As FiniteBinaryOperation(Of T)) As Boolean
            Try
                Dim tempGroup As New FiniteGroup(Of T)(testSet, testOperation)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Determines whether a given finite monoid will form a finite group.
        ''' </summary>
        ''' <param name="testMonoid">The monoid to test.</param>
        ''' <returns>Returns <c>True</c> if a given monoid is also a group, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Shared Function IsGroup(ByVal testMonoid As FiniteMonoid(Of T)) As Boolean
            Return testMonoid.Operation.HasInverses
        End Function

        ''' <summary>
        ''' Determines whether a function is a homomorphism from this group to another group.
        ''' </summary>
        ''' <typeparam name="G">The Type of elements in the other group.</typeparam>
        ''' <param name="codomain">The other group (which forms the codomain of the function).</param>
        ''' <param name="testFunction">The function to test whether or not it is a homomorphism.</param>
        ''' <returns>Returns <c>True</c> if the function is a group homomorphism, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Shadows Function IsHomomorphism(Of G As {Class, New, IEquatable(Of G)})(ByVal codomain As FiniteGroup(Of G), ByVal testFunction As FiniteFunction(Of T, G)) As Boolean
            If testFunction.Codomain.Equals(codomain.theSet) = False Then
                Throw New Exception("The codomain of of the parameter 'testFunction' is not the parameter 'codomain'.")
            ElseIf testFunction.Domain.Equals(Me.theSet) = False Then
                Throw New Exception("The domain of of the parameter 'testFunction' is not this group.")
            End If

            Dim result As Boolean

            'Check to see that it is a homomorphism of the underlying monoids
            result = MyBase.IsHomomorphism(codomain, testFunction)

            Return result
        End Function

        ''' <summary>
        ''' Determines whether the group is hypoabelian. In other words, that its perfect core is trivial.
        ''' </summary>
        ''' <returns>Returns <c>True</c> if the group is hypoabelian, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Function IsHypoabelian() As Boolean
            If Me.groupProperties.ContainsKey("hypoabelian") = False Then
                'Solvable implies hypoabelian, check to see if we've calculated that already.
                If Me.groupProperties.ContainsKey("solvable") = True Then
                    If Me.groupProperties.Item("solvable") = True Then
                        Me.groupProperties.Add("hypoabelian", True)
                        Return True
                    End If
                End If

                If Me.PerfectCore.Equals(Me.TrivialSubgroup) = True Then
                    Me.groupProperties.Add("hypoabelian", True)
                    Return True
                Else
                    Me.groupProperties.Add("hypoabelian", False)
                    Return False
                End If
            Else
                Return Me.groupProperties.Item("hypoabelian")
            End If
        End Function

        ''' <summary>
        ''' Determines whether the group is imperfect. In other words, that it has no nontrivial quotient group that is perfect.
        ''' </summary>
        ''' <returns>Returns <c>True</c> if the group is imperfect, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Function IsImperfect() As Boolean
            If Me.groupProperties.ContainsKey("imperfect") = False Then
                'Abelian implies imperfect, check to see if we've calculated that already.
                If Me.groupProperties.ContainsKey("abelian") = True Then
                    If Me.groupProperties.Item("abelian") = True Then
                        Me.groupProperties.Add("imperfect", True)
                        Return True
                    End If
                End If

                'Hypoabelian implies imperfect, check to see if we've calculated that already.
                If Me.groupProperties.ContainsKey("hypoabelian") = True Then
                    If Me.groupProperties.Item("hypoabelian") = True Then
                        Me.groupProperties.Add("imperfect", True)
                        Return True
                    End If
                End If

                'Nilpotent implies imperfect, check to see if we've calculated that already.
                If Me.groupProperties.ContainsKey("nilpotent") = True Then
                    If Me.groupProperties.Item("nilpotent") = True Then
                        Me.groupProperties.Add("imperfect", True)
                        Return True
                    End If
                End If

                'Solvable implies imperfect, check to see if we've calculated that already.
                If Me.groupProperties.ContainsKey("solvable") = True Then
                    If Me.groupProperties.Item("solvable") = True Then
                        Me.groupProperties.Add("imperfect", True)
                        Return True
                    End If
                End If

                Dim index As Integer
                Dim curQuotient As FiniteGroup(Of FiniteSet(Of T))

                For index = 0 To Me.SetOfAllNormalSubgroups.Cardinality - 1
                    curQuotient = Me.QuotientGroup(Me.SetOfAllNormalSubgroups.Element(index))

                    'Ignore trivial quotient groups
                    If curQuotient.Order > 1 Then
                        If curQuotient.IsPerfect = True Then
                            Me.groupProperties.Add("imperfect", False)
                            Return False
                        End If
                    End If
                Next index

                Me.groupProperties.Add("imperfect", True)
                Return True
            Else
                Return Me.groupProperties.Item("imperfect")
            End If
        End Function

        ''' <summary>
        ''' Determines whether a function is a isomorphism from this group to another group. In other words, it's a bijective homomorphism.
        ''' </summary>
        ''' <typeparam name="G">The Type of elements in the other group.</typeparam>
        ''' <param name="codomain">The other group (which forms the codomain of the function).</param>
        ''' <param name="testFunction">The function to test whether or not it is a isomorphism.</param>
        ''' <returns>Returns <c>True</c> if the function is a group isomorphism, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Shadows Function IsIsomorphism(Of G As {Class, New, IEquatable(Of G)})(ByVal codomain As FiniteGroup(Of G), ByVal testFunction As FiniteFunction(Of T, G)) As Boolean
            If testFunction.Codomain.Equals(codomain.theSet) = False Then
                Throw New Exception("The codomain of of the parameter 'testFunction' is not the parameter 'codomain'.")
            ElseIf testFunction.Domain.Equals(Me.theSet) = False Then
                Throw New Exception("The domain of of the parameter 'testFunction' is not this group.")
            End If

            Return MyBase.IsIsomorphism(codomain, testFunction)
        End Function

        ''' <summary>
        ''' Determines whether this group is a maximal subgroup of a given group.
        ''' </summary>
        ''' <param name="superGroup">The supergroup (or ambient group) to test if this group is a maximal subgroup of it.</param>
        ''' <returns>Returns <c>True</c> if the group is a maximal subgroup of a given group, <c>False</c> otherwise.</returns>
        ''' <remarks>A maximal subgroup is a proper subgroup that is not strictly contained by any other proper subgroup.</remarks>
        Public Function IsMaximalSubgroupOf(ByVal superGroup As FiniteGroup(Of T)) As Boolean
            'Check to make sure it is a proper subgroup
            If Me.IsProperSubgroupOf(superGroup) = False Then Return False

            'Check to make sure no other proper subgroup strictly contains this group
            Dim allSupergroupSubgroups As FiniteSet(Of FiniteGroup(Of T)) = superGroup.SetOfAllSubgroups
            Dim index As Integer

            For index = 0 To allSupergroupSubgroups.Cardinality - 1
                If Me.Equals(allSupergroupSubgroups.Element(index)) = False Then
                    If allSupergroupSubgroups.Element(index).theSet.IsProperSubsetOf(superGroup.theSet) = True And _
                        Me.IsSubgroupOf(allSupergroupSubgroups.Element(index)) = True Then

                        Return False
                    End If
                End If
            Next index

            Return True
        End Function

        ''' <summary>
        ''' Determines whether this group is metabelian. In other words, if its derived subgroup is abelian.
        ''' </summary>
        ''' <returns>Returns <c>True</c> if the group is metabelian, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Function IsMetabelian() As Boolean
            If Me.groupProperties.ContainsKey("metabelian") = False Then
                'Abelian implies metabelian, check to see if we've calculated that already.
                If Me.groupProperties.ContainsKey("abelian") = True Then
                    If Me.groupProperties.Item("abelian") = True Then
                        Me.groupProperties.Add("metabelian", True)
                        Return True
                    End If
                End If

                'Metacyclic implies metabelian, check to see if we've calculated that already.
                If Me.groupProperties.ContainsKey("metacyclic") = True Then
                    If Me.groupProperties.Item("metacyclic") = True Then
                        Me.groupProperties.Add("metabelian", True)
                        Return True
                    End If
                End If

                'Cyclic implies metabelian, check to see if we've calculated that already.
                If Me.groupProperties.ContainsKey("cyclic") = True Then
                    If Me.groupProperties.Item("cyclic") = True Then
                        Me.groupProperties.Add("cyclic", True)
                        Return True
                    End If
                End If

                If Me.DerivedSubgroup.Operation.IsCommutative() = True Then
                    Me.groupProperties.Add("metabelian", True)
                    Return True
                Else
                    Me.groupProperties.Add("metabelian", False)
                    Return False
                End If
            Else
                Return Me.groupProperties.Item("metabelian")
            End If
        End Function

        Public Function IsMetacyclic() As Boolean
            If Me.groupProperties.ContainsKey("metacyclic") = False Then
                'Cyclic implies metacyclic, check to see if we've calculated that already.
                If Me.groupProperties.ContainsKey("cyclic") Then
                    If Me.groupProperties.Item("Cyclic") = True Then
                        Me.groupProperties.Add("metacyclic", True)
                        Return True
                    End If
                End If

                Dim normalSubgroups As FiniteSet(Of FiniteGroup(Of T)) = Me.allNormalSubgroups
                Dim subgroupIndex As Integer

                For subgroupIndex = 0 To normalSubgroups.Cardinality - 1
                    Dim curSubgroup As FiniteGroup(Of T) = normalSubgroups.Element(subgroupIndex)

                    If curSubgroup.IsCyclic And Me.QuotientGroup(curSubgroup).IsCyclic Then
                        Me.groupProperties.Add("metacyclic", True)
                        Return True
                    End If
                Next subgroupIndex
            Else
                Return Me.groupProperties.Item("metacyclic")
            End If
        End Function

        ''' <summary>
        ''' Determines whether this group is metanilpotent. In other words, if it has a nilpotent normal subgroup whose quotient group is also nilpotent.
        ''' </summary>
        ''' <returns>Returns <c>True</c> if the group is metanilpotent, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Function IsMetanilpotent() As Boolean
            If Me.groupProperties.ContainsKey("metanilpotent") = False Then
                'Abelian implies metanilpotent, check to see if we've calculated that already.
                If Me.groupProperties.ContainsKey("abelian") = True Then
                    If Me.groupProperties.Item("abelian") = True Then
                        Me.groupProperties.Add("metanilpotent", True)
                        Return True
                    End If
                End If

                'Metabelian implies metanilpotent, check to see if we've calculated that already.
                If Me.groupProperties.ContainsKey("metabelian") = True Then
                    If Me.groupProperties.Item("metabelian") = True Then
                        Me.groupProperties.Add("metanilpotent", True)
                        Return True
                    End If
                End If

                'Nilpotent implies metanilpotent, check to see if we've calculated that already.
                If Me.groupProperties.ContainsKey("nilpotent") = True Then
                    If Me.groupProperties.Item("nilpotent") = True Then
                        Me.groupProperties.Add("metanilpotent", True)
                        Return True
                    End If
                End If


                Dim subgroupIndex As Integer
                Dim subgroups As FiniteSet(Of FiniteGroup(Of T))
                Dim curElem As FiniteGroup(Of T)

                subgroups = Me.SetOfAllNormalSubgroups()

                For subgroupIndex = 0 To Me.SetOfAllNormalSubgroups.Cardinality - 1
                    curElem = subgroups.Element(subgroupIndex)
                    If curElem.IsNilpotent() = True Then
                        'We found a potential candidate. Check to see if its Quotient Group is also Nilpotent.
                        If Me.QuotientGroup(curElem).IsNilpotent = True Then
                            'Great! We have a metanilpotent group!
                            Me.groupProperties.Add("metanilpotent", True)
                            Return True
                        End If
                    End If
                Next subgroupIndex

                Me.groupProperties.Add("metanilpotent", False)
                Return False
            Else
                Return Me.groupProperties.Item("metanilpotent")
            End If
        End Function

        ''' <summary>
        ''' Determines whether this group is nilpotent.
        ''' </summary>
        ''' <returns>Returns <c>True</c> if the group is nilpotent, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Function IsNilpotent() As Boolean
            If Me.groupProperties.ContainsKey("nilpotent") = False Then
                'Abelian implies nilpotent, check to see if we've calculated that already.
                If Me.groupProperties.ContainsKey("abelian") = True Then
                    If Me.groupProperties.Item("abelian") = True Then
                        Me.groupProperties.Add("nilpotent", True)
                        Return True
                    End If
                End If

                'Dedekind implies nilpotent, check to see if we've calculated that already.
                If Me.groupProperties.ContainsKey("dedekind") = True Then
                    If Me.groupProperties.Item("dedekind") = True Then
                        Me.groupProperties.Add("nilpotent", True)
                        Return True
                    End If
                End If

                'Special implies nilpotent, check to see if we've calculated that already.
                If Me.groupProperties.ContainsKey("special") = True Then
                    If Me.groupProperties.Item("special") = True Then
                        Me.groupProperties.Add("nilpotent", True)
                        Return True
                    End If
                End If

                Dim allMySubgroups As FiniteSet(Of FiniteGroup(Of T)) = Me.SetOfAllSubgroups
                Dim index As Integer

                For index = 0 To allMySubgroups.Cardinality - 1
                    'Check to make sure it's proper
                    If allMySubgroups.Element(index).theSet.IsProperSubsetOf(Me.theSet) Then

                        If allMySubgroups.Element(index).IsSelfNormalizingSubgroupOf(Me) = True Then
                            Me.groupProperties.Add("nilpotent", False)
                            Return False
                        End If

                    End If
                Next index

                Me.groupProperties.Add("nilpotent", True)
                Return True
            Else
                Return Me.groupProperties.Item("nilpotent")
            End If
        End Function

        ''' <summary>
        ''' Determines whether this group is a normal subgroup of another group.
        ''' </summary>
        ''' <param name="superGroup">The supergroup (or ambient group) to test if this group is a normal subgroup of it.</param>
        ''' <returns>Returns <c>True</c> if the group is a normal subgroup of a given group, <c>False</c> otherwise.</returns>
        ''' <remarks>A subgroup is normal if and only if all left and right cosets coincide.</remarks>
        Public Function IsNormalSubgroupOf(ByVal superGroup As FiniteGroup(Of T)) As Boolean
            'First check to make sure it is a subgroup
            If Me.IsSubgroupOf(superGroup) = False Then Return False

            'Check to see if it is the whole group, which is trivialy normal
            If Me.Order = superGroup.Order Then
                Return True
            End If

            'Check to see if it is of index 2, which would quickly imply that it is normal
            If Me.SubgroupIndex(superGroup) = 2 Then
                Return True
            End If

            'Alright, crank it the long way
            Dim index As Integer

            For index = 0 To superGroup.theSet.Cardinality - 1
                If superGroup.LeftCoset(Me, superGroup.theSet.Element(index)).Equals(superGroup.RightCoset(Me, superGroup.theSet.Element(index))) = False Then
                    Return False
                End If
            Next index

            Return True
        End Function

        ''' <summary>
        ''' Determines whether this group is perfect. In other words, it is equal to its derived subgroup.
        ''' </summary>
        ''' <returns>Returns <c>True</c> if the group is perfect, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Function IsPerfect() As Boolean
            If Me.groupProperties.ContainsKey("perfect") = False Then
                If Me.Equals(Me.DerivedSubgroup) = True Then
                    Me.groupProperties.Add("perfect", True)
                    Return True
                Else
                    Me.groupProperties.Add("perfect", False)
                    Return False
                End If
            Else
                Return Me.groupProperties.Item("perfect")
            End If
        End Function

        ''' <summary>
        ''' Determines whether this group is a proper subgroup of a given group. In other words it is a subgroup but not equal to the whole group.
        ''' </summary>
        ''' <param name="superGroup">The supergroup (or ambient group) to test if this group is a proper subgroup of it.</param>
        ''' <returns>Returns <c>True</c> if the group is a proper subgroup of a given group, <c>False</c> otherwise.</returns>
        ''' <remarks>A subgroup is proper if it not the whole group.</remarks>
        Public Function IsProperSubgroupOf(ByVal superGroup As FiniteGroup(Of T)) As Boolean
            'Check to make sure it is a subgroup
            If Me.IsSubgroupOf(superGroup) = False Then Return False

            'Check to make sure it's a proper subgroup
            If Me.theSet.IsProperSubsetOf(superGroup.theSet) = False Then Return False

            Return True
        End Function

        ''' <summary>
        ''' Determines whether this group is a self-normalizing subgroup of a given group. In other words, it is a subgroup equal to its own normalizer in the supergroup.
        ''' </summary>
        ''' <param name="superGroup">The supergroup (or ambient group) to test if this group is a self-normalizing subgroup of it.</param>
        ''' <returns>Returns <c>True</c> if the group is a self-normalizing subgroup of a given group, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Function IsSelfNormalizingSubgroupOf(ByVal superGroup As FiniteGroup(Of T)) As Boolean
            'Check to make sure it is a subgroup
            If Me.IsSubgroupOf(superGroup) = False Then
                Return False
            End If

            Return Me.theSet.Equals(superGroup.Normalizer(Me, True))
        End Function

        ''' <summary>
        ''' Determines whether this group is simple. In other words, if this group's only normal subgroups is the group itself and the trivial subgroup.
        ''' </summary>
        ''' <returns>Returns <c>True</c> if the group is simple, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Function IsSimple() As Boolean
            If Me.groupProperties.ContainsKey("simple") = False Then
                'Check to make sure the group's only normal subgroups are itself and the trivial subgroup
                'And check to make sure it is not the trivial group itself
                If Me.SetOfAllNormalSubgroups.Cardinality <= 2 And Me.Order > 1 Then
                    Me.groupProperties.Add("simple", True)
                    Return True
                Else
                    Me.groupProperties.Add("simple", False)
                    Return False
                End If
            Else
                Return Me.groupProperties.Item("simple")
            End If
        End Function

        ''' <summary>
        ''' Determines whether this group is solvable.
        ''' </summary>
        ''' <returns>Returns <c>True</c> if the group is solvable, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Function IsSolvable() As Boolean
            If Me.groupProperties.ContainsKey("solvable") = False Then
                'Abelian implies solvable, check to see if we've calculated that already.
                If Me.groupProperties.ContainsKey("abelian") = True Then
                    If Me.groupProperties.Item("abelian") = True Then
                        Me.groupProperties.Add("solvable", True)
                        Return True
                    End If
                End If

                'Dedekind implies solvable, check to see if we've calculated that already.
                If Me.groupProperties.ContainsKey("dedekind") = True Then
                    If Me.groupProperties.Item("dedekind") = True Then
                        Me.groupProperties.Add("solvable", True)
                        Return True
                    End If
                End If

                'Nilpotent implies solvable, check to see if we've calculated that already.
                If Me.groupProperties.ContainsKey("nilpotent") = True Then
                    If Me.groupProperties.Item("nilpotent") = True Then
                        Me.groupProperties.Add("solvable", True)
                        Return True
                    End If
                End If

                'Metabelian implies solvable, check to see if we've calculated that already.
                If Me.groupProperties.ContainsKey("metabelian") = True Then
                    If Me.groupProperties.Item("metabelian") = True Then
                        Me.groupProperties.Add("solvable", True)
                        Return True
                    End If
                End If

                'Metanilpotent implies solvable, check to see if we've calculated that already.
                If Me.groupProperties.ContainsKey("metanilpotent") = True Then
                    If Me.groupProperties.Item("metanilpotent") = True Then
                        Me.groupProperties.Add("solvable", True)
                        Return True
                    End If
                End If

                'Special implies solvable, check to see if we've calculated that already.
                If Me.groupProperties.ContainsKey("special") = True Then
                    If Me.groupProperties.Item("special") = True Then
                        Me.groupProperties.Add("solvable", True)
                        Return True
                    End If
                End If

                Dim curGrp As FiniteGroup(Of T)
                Dim supGrp As FiniteGroup(Of T)

                curGrp = Me.DerivedSubgroup()
                supGrp = Me

                Do While curGrp.IsProperSubgroupOf(supGrp)
                    supGrp = curGrp
                    curGrp = supGrp.DerivedSubgroup()
                Loop

                If curGrp.Equals(Me.TrivialSubgroup) = True Then
                    Me.groupProperties.Add("solvable", True)
                    Return True
                Else
                    Me.groupProperties.Add("solvable", False)
                    Return False
                End If
            Else
                Return Me.groupProperties.Item("solvable")
            End If
        End Function

        ''' <summary>
        ''' Determines whether this group is special. In other words, that its center, derived subgroup, and Frattini subgroup coincide.
        ''' </summary>
        ''' <returns>Returns <c>True</c> if the group is special, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Function IsSpecial() As Boolean
            If Me.groupProperties.ContainsKey("special") = False Then
                Dim derSubgroup As FiniteGroup(Of T) = Me.DerivedSubgroup

                'TODO: Check to make sure the group's order is a prime power

                If Me.CenterGroup.Equals(derSubgroup) = True And Me.FrattiniSubgroup.Equals(derSubgroup) = True Then
                    Me.groupProperties.Add("special", True)
                    Return True
                Else
                    Me.groupProperties.Add("special", False)
                    Return False
                End If
            Else
                Return Me.groupProperties.Item("special")
            End If
        End Function

        ''' <summary>
        ''' Determines whether this group is a subgroup of another group.
        ''' </summary>
        ''' <param name="superGroup">The supergroup (or ambient group) to test if this group is a subgroup of it.</param>
        ''' <returns>Returns <c>True</c> if the group is a subgroup of a given group, <c>False</c> otherwise.</returns>
        ''' <remarks>A subgroup is a subset of the group and satisfies the definition of a group itself.</remarks>
        Public Function IsSubgroupOf(ByVal superGroup As FiniteGroup(Of T)) As Boolean
            If Me.theSet.IsSubsetOf(superGroup.theSet) = False Then Return False

            If Me.Operation.EquivalentMaps(superGroup.Operation.theRelation, superGroup.Operation.Domain, superGroup.Operation.Codomain) = False Then
                Return False
            Else
                'Collect any useful new inherited information since it is a subgroup.
                Dim enumer As Dictionary(Of String, Boolean).Enumerator = superGroup.SubgroupClosedProperties.GetEnumerator

                Do While enumer.MoveNext = True
                    If Me.groupProperties.ContainsKey(enumer.Current.Key) = False Then
                        Me.groupProperties.Add(enumer.Current.Key, enumer.Current.Value)
                    End If
                Loop

                Return True
            End If
        End Function

        ''' <summary>
        ''' Determines whether this group is a transitively normal subgroup of another group. In other words it is a normal subgroup and any normal subgroup of this group is also a normal subgroup of the superGroup.
        ''' </summary>
        ''' <param name="superGroup">The supergroup (or ambient group) to test if this group is a transitively-normal subgroup of it.</param>
        ''' <returns>Returns <c>True</c> if the group is a transitively normal subgroup of a given group, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Function IsTransitivelyNormalSubgroupOf(ByVal superGroup As FiniteGroup(Of T)) As Boolean
            'First check to make sure it is a normal subgroup
            If Me.IsNormalSubgroupOf(superGroup) = False Then Return False

            'Then check it's normal subgroups
            Dim allMyNormalSubgroups As FiniteSet(Of FiniteGroup(Of T)) = Me.SetOfAllNormalSubgroups
            Dim index As Integer

            For index = 0 To allMyNormalSubgroups.Cardinality - 1
                If allMyNormalSubgroups.Element(index).IsNormalSubgroupOf(superGroup) = False Then
                    Return False
                End If
            Next index

            Return True
        End Function

        ''' <summary>
        ''' Determines whether this group is a T-Group. In other words, all normal subgroups are transitively normal.
        ''' </summary>
        ''' <returns>Returns <c>True</c> if the group is a T-Group, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Function IsTGroup() As Boolean
            If Me.groupProperties.ContainsKey("t-group") = False Then
                'Abelian implies T-Group, check to see if we've calculated that already.
                If Me.groupProperties.ContainsKey("abelian") = True Then
                    If Me.groupProperties.Item("abelian") = True Then
                        Me.groupProperties.Add("t-group", True)
                        Return True
                    End If
                End If

                'Dedekind implies T-Group, check to see if we've calculated that already.
                If Me.groupProperties.ContainsKey("dedekind") = True Then
                    If Me.groupProperties.Item("dedekind") = True Then
                        Me.groupProperties.Add("t-group", True)
                        Return True
                    End If
                End If

                'T*-Group implies T-Group, check to see if we've calculated that already.
                If Me.groupProperties.ContainsKey("t*-group") = True Then
                    If Me.groupProperties.Item("t*-group") = True Then
                        Me.groupProperties.Add("t-group", True)
                        Return True
                    End If
                End If

                Dim allMyNormalSubgroups As FiniteSet(Of FiniteGroup(Of T)) = Me.SetOfAllNormalSubgroups
                Dim index As Integer

                For index = 0 To allMyNormalSubgroups.Cardinality - 1
                    If allMyNormalSubgroups.Element(index).IsTransitivelyNormalSubgroupOf(Me) = False Then
                        Me.groupProperties.Add("t-group", False)
                        Return False
                    End If
                Next index

                Me.groupProperties.Add("t-group", True)
                Return True
            Else
                Return Me.groupProperties.Item("t-group")
            End If
        End Function

        ''' <summary>
        ''' Determines whether this group is a T*-Group. In other words, all subgroups are T-Groups.
        ''' </summary>
        ''' <returns>Returns <c>True</c> if the group is a T*-Group, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Function IsTStarGroup() As Boolean
            If Me.groupProperties.ContainsKey("t*-group") = False Then
                'Abelian implies T*-Group, check to see if we've calculated that already.
                If Me.groupProperties.ContainsKey("abelian") = True Then
                    If Me.groupProperties.Item("abelian") = True Then
                        Me.groupProperties.Add("t*-group", True)
                        Return True
                    End If
                End If

                'Dedekind implies T*-Group, check to see if we've calculated that already.
                If Me.groupProperties.ContainsKey("dedekind") = True Then
                    If Me.groupProperties.Item("dedekind") = True Then
                        Me.groupProperties.Add("t*-group", True)
                        Return True
                    End If
                End If

                Dim index As Integer
                Dim subgroups As FiniteSet(Of FiniteGroup(Of T))

                subgroups = Me.SetOfAllSubgroups()

                For index = 0 To subgroups.Cardinality - 1
                    If subgroups.Element(index).IsTGroup() = False Then
                        Me.groupProperties.Add("t*-group", False)
                        Return False
                    End If
                Next index

                Me.groupProperties.Add("t*-group", True)
                Return True
            Else
                Return Me.groupProperties.Item("t*-group")
            End If
        End Function

        ''' <summary>
        ''' Returns the left coset of a particular element with a particular subgroup.
        ''' </summary>
        ''' <param name="subgroup">The subgroup with which to form the left coset.</param>
        ''' <param name="elem">The element whose left coset is to be returned.</param>
        ''' <exception cref="NotSubgroupException">Throws NotSubgroupException if parameter 'subgroup' is not a subgroup of this group.</exception>
        ''' <exception cref="NotMemberOfException">Throws NotMemberOfException if parameter 'elem' is not a member of the group.</exception>
        ''' <returns>Returns the left coset of a given element and given subgroup as a <c>FiniteSet</c>.</returns>
        ''' <remarks></remarks>
        Public Function LeftCoset(ByVal subgroup As FiniteGroup(Of T), ByVal elem As T) As FiniteSet(Of T)
            If subgroup.IsSubgroupOf(Me) = False Then
                Throw New NotSubgroupException("The parameter 'subgroup' is not a subgroup of this group.")
            ElseIf Me.theSet.Contains(elem) = False Then
                Throw New NotMemberOfException("The parameter 'elem' is not an element of this group.")
            End If

            Dim newSet As New FiniteSet(Of T)
            Dim index As Integer
            Dim curTup As New Tuple(2)

            curTup.Element(0) = elem


            'Parallel.For(0, subgroup.theSet.Cardinality, Sub(i)
            '                                                 Dim index As Integer = i
            '                                                 Dim curTup As New Tuple(2)

            '                                                 curTup.Element(0) = elem
            '                                                 curTup.Element(1) = subgroup.theSet.Element(index)
            '                                                 newSet.AddElement(Me.ApplyOperation(curTup))

            '                                                 'Debug.WriteLine(index)
            '                                             End Sub)

            For index = 0 To subgroup.theSet.Cardinality - 1
                curTup.Element(1) = subgroup.theSet.Element(index)
                newSet.AddElement(Me.ApplyOperation(curTup))
            Next index

            Return newSet
        End Function

        ''' <summary>
        ''' Returns the normalizer (as a group) of a subgroup with respect to this group.
        ''' </summary>
        ''' <param name="subgroup">The subgroup whose normalizer is to be returned.</param>
        ''' <returns>Returns the normalizer of a given subgroup as a <c>FiniteGroup</c>.</returns>
        ''' <remarks>Uses the parallel version of <c>Noramlizer(subgroup, UseParallel)</c>.</remarks>
        Public Function NormalizerSubgroup(ByVal subgroup As FiniteGroup(Of T)) As FiniteGroup(Of T)
            Return New FiniteGroup(Of T)(Me.Normalizer(subgroup), Me.Operation)
        End Function

        ''' <summary>
        ''' Returns the normalizer of a subgroup with respect to this group.
        ''' </summary>
        ''' <param name="subgroup">The subgroup whose normalizer is to be returned.</param>
        ''' <param name="UseParallel">Optional. Determines whether or not a parallel or sequential calculation of the normalizer will be performed. Default is to use a parallel calculation.</param>
        ''' <exception cref="NotSubgroupException">Throws NotSubgroupException if parameter 'subgroup' is not a subgroup of this group.</exception>
        ''' <returns>Returns the normalizer of a given subgroup as a <c>FiniteSet</c>.</returns>
        ''' <remarks></remarks>
        Public Function Normalizer(ByVal subgroup As FiniteGroup(Of T), Optional ByVal UseParallel As Boolean = True) As FiniteSet(Of T)
            If subgroup.IsSubgroupOf(Me) = False Then
                Throw New NotSubgroupException("The parameter 'subgroup' is not a subgroup of this group.")
            End If

            Dim newSet As New FiniteSet(Of T)

            If UseParallel = True Then
                Parallel.For(0, Me.Order, Sub(index)
                                              If Me.LeftCoset(subgroup, Me.theSet.Element(index)).Equals(Me.RightCoset(subgroup, Me.theSet.Element(index))) = True Then
                                                  newSet.AddElement(Me.theSet.Element(index))
                                              End If
                                          End Sub)
            Else
                Dim index As Integer

                For index = 0 To Me.Order - 1
                    If Me.LeftCoset(subgroup, Me.theSet.Element(index)).Equals(Me.RightCoset(subgroup, Me.theSet.Element(index))) = True Then
                        newSet.AddElement(Me.theSet.Element(index))
                    End If
                Next index
            End If


            Return newSet
        End Function

        ''' <summary>
        ''' Returns the order of the group.
        ''' </summary>
        ''' <returns>Returns the order of the group.</returns>
        ''' <remarks></remarks>
        Public Function Order() As Integer
            Return Me.theSet.Cardinality
        End Function

        ''' <summary>
        ''' Returns the order of a particular element of the group.
        ''' </summary>
        ''' <param name="elem">The element whose order is to be returned.</param>
        ''' <exception cref="NotMemberOfException">Throws NotMemberOfException if parameter 'elem' is not a member of the group.</exception>
        ''' <returns>Returns the order of a given element.</returns>
        ''' <remarks></remarks>
        Public Function Order(ByVal elem As T) As Integer
            If Me.theSet.Contains(elem) = False Then
                Throw New NotMemberOfException("The parameter 'elem' is not a member of the group.")
            End If

            'If Me.Order > 5 Then

            'Deal with the trivial case
            If elem.Equals(Me.IdentityElement) Then
                Return 1
            End If

            'The order of an element must divide the order of the group, thus we only need to check those powers.
            Dim possibleOrders As List(Of Integer)
            Dim index As Integer
            Dim curPwr As T
            Dim curTup As Tuple

            'Initialize the current power of the element
            curPwr = elem

            'Find all possible values for the order of the element
            possibleOrders = Me.FindFactors(Me.Order)

            For index = 1 To possibleOrders.Count - 1
                curTup = New Tuple(2)
                curTup.Element(0) = curPwr
                curTup.Element(1) = Me.Power(elem, possibleOrders.Item(index) - possibleOrders.Item(index - 1))
                curPwr = Me.ApplyOperation(curTup)

                If curPwr.Equals(Me.IdentityElement) Then
                    Return possibleOrders.Item(index)
                End If
            Next index

            Throw New Exception("Error: Could not find order of element?! There must be a problem with the code.")

            'Else
            '    Dim theOrder As Integer = 1
            '    Dim curElem As T
            '    Dim curTup As New Tuple(2)

            '    curElem = elem

            '    curTup.Element(1) = elem

            '    Do Until curElem.Equals(Me.IdentityElement)
            '        curTup.Element(0) = curElem

            '        curElem = Me.ApplyOperation(curTup)
            '        theOrder += 1
            '    Loop

            '    Return theOrder
            'End If
        End Function

        ''' <summary>
        ''' Returns the perfect core of the group. In other words, its largest perfect subgroup.
        ''' </summary>
        ''' <returns>Returns the perfect core of the group.</returns>
        ''' <remarks></remarks>
        Public Function PerfectCore() As FiniteGroup(Of T)
            Dim allMySubgroups As FiniteSet(Of FiniteGroup(Of T)) = Me.SetOfAllSubgroups
            Dim perfectSubgroups As New FiniteSet(Of FiniteGroup(Of T))
            Dim index As Integer
            Dim largestIndex As Integer

            'Find all the perfect subgroups
            For index = 0 To allMySubgroups.Cardinality - 1
                If allMySubgroups.Element(index).IsPerfect = True Then
                    perfectSubgroups.AddElement(allMySubgroups.Element(index))
                End If
            Next index

            'Find the largest one
            largestIndex = 0
            For index = 0 To perfectSubgroups.Cardinality - 1
                If perfectSubgroups.Element(index).Order > perfectSubgroups.Element(largestIndex).Order Then
                    largestIndex = index
                End If
            Next index

            Return perfectSubgroups.Element(largestIndex)
        End Function

        ''' <summary>
        ''' Returns the power of a given element.
        ''' </summary>
        ''' <param name="elem">The element to multiply by itself to the exponent-th degree.</param>
        ''' <param name="exponent">The number of times to multiply the element by itself.</param>
        ''' <returns>Returns the power of a given element.</returns>
        ''' <remarks></remarks>
        Public Function Power(ByVal elem As T, ByVal exponent As Integer) As T
            Dim x As T
            Dim g As T
            Dim curTup As Tuple

            g = elem

            x = Me.IdentityElement

            If exponent Mod 2 = 1 Then
                curTup = New Tuple(2)
                curTup.Element(0) = x
                curTup.Element(1) = g
                x = Me.ApplyOperation(curTup)
            End If

            Do While exponent > 1
                curTup = New Tuple(2)
                curTup.Element(0) = g
                curTup.Element(1) = g
                g = Me.ApplyOperation(curTup)

                exponent = exponent \ 2

                If exponent Mod 2 = 1 Then
                    curTup = New Tuple(2)
                    curTup.Element(0) = x
                    curTup.Element(1) = g
                    x = Me.ApplyOperation(curTup)
                End If
            Loop

            Return x
        End Function

        ''' <summary>
        ''' Returns the quotient group formed from this group modulo a normal subgroup.
        ''' </summary>
        ''' <param name="normalSubgroup">The normal subgroup which is to be used as the divisor in the quotient construction.</param>
        ''' <exception cref="NotSubgroupException">Throws NotSubgroupException if parameter 'normalSubgroup' is not a normal subgroup of this group.</exception>
        ''' <returns>Returns the quotient group formed from this group modulo a normal subgroup.</returns>
        ''' <remarks></remarks>
        Public Function QuotientGroup(ByVal normalSubgroup As FiniteGroup(Of T)) As FiniteGroup(Of FiniteSet(Of T))
            If normalSubgroup.IsNormalSubgroupOf(Me) = False Then
                Throw New NotSubgroupException("The parameter 'normalSubgroup' is not a normal subgroup of this group.")
            End If

            Dim quotientSet As New FiniteSet(Of FiniteSet(Of T))
            Dim index As Integer

            For index = 0 To Me.theSet.Cardinality - 1
                quotientSet.AddElement(Me.LeftCoset(normalSubgroup, Me.theSet.Element(index)))
            Next index

            Dim newMap As New QuotientMap(Me.Operation.theRelation)

            Dim newOpProps As New Dictionary(Of String, Boolean)

            newOpProps.Add("associativity", True)
            newOpProps.Add("inverses", True)

            Dim newOp As New FiniteBinaryOperation(Of FiniteSet(Of T))(quotientSet, newMap, newOpProps)

            Return New FiniteGroup(Of FiniteSet(Of T))(quotientSet, newOp, Me.QuotientClosedProperties)
        End Function

        ''' <summary>
        ''' Returns the right coset of a particular element with a particular subgroup.
        ''' </summary>
        ''' <param name="subgroup">The subgroup with which to form the right coset.</param>
        ''' <param name="elem">The element whose right coset is to be returned.</param>
        ''' <exception cref="NotSubgroupException">Throws NotSubgroupException if parameter 'subgroup' is not a subgroup of this group.</exception>
        ''' <exception cref="NotMemberOfException">Throws NotMemberOfException if parameter 'elem' is not a member of the group.</exception>
        ''' <returns>Returns the right coset of a given element and a given subgroup.</returns>
        ''' <remarks></remarks>
        Public Function RightCoset(ByVal subgroup As FiniteGroup(Of T), ByVal elem As T) As FiniteSet(Of T)
            If subgroup.IsSubgroupOf(Me) = False Then
                Throw New NotSubgroupException("The parameter 'subgroup' is not a subgroup of this group.")
            ElseIf Me.theSet.Contains(elem) = False Then
                Throw New NotMemberOfException("The parameter 'elem' is not an element of this group.")
            End If

            Dim newSet As New FiniteSet(Of T)
            Dim index As Integer
            Dim curTup As New Tuple(2)

            curTup.Element(1) = elem

            For index = 0 To subgroup.theSet.Cardinality - 1
                curTup.Element(0) = subgroup.theSet.Element(index)
                newSet.AddElement(Me.ApplyOperation(curTup))
            Next index

            Return newSet
        End Function

        ''' <summary>
        ''' Returns the set of all conjugacy classes of this group.
        ''' </summary>
        ''' <returns>Returns the set of all conjugacy classes of this group as a <c>FiniteSet(Of FiniteSet)</c>.</returns>
        ''' <remarks></remarks>
        Public Function SetOfAllConjugacyClasses() As FiniteSet(Of FiniteSet(Of T))
            Dim newSet As New FiniteSet(Of FiniteSet(Of T))

            Dim index As Integer

            For index = 0 To Me.theSet.Cardinality - 1
                newSet.AddElement(Me.ConjugacyClass(Me.theSet.Element(index)))
            Next index

            Return newSet
        End Function

        ''' <summary>
        ''' Returns the set of all maximal subgroups of this group (does NOT include improper subgroups).
        ''' </summary>
        ''' <returns>Returns the set of all maximal subgroups as a <c>FiniteSet</c>.</returns>
        ''' <remarks>A maximal subgroup is a proper subgroup that is not strictly contained by any other proper subgroup.</remarks>
        Public Function SetOfAllMaximalSubgroups() As FiniteSet(Of FiniteGroup(Of T))
            Dim newSet As New FiniteSet(Of FiniteGroup(Of T))

            Dim index As Integer
            Dim subgroups As FiniteSet(Of FiniteGroup(Of T)) = Me.SetOfAllSubgroups

            For index = 0 To subgroups.Cardinality - 1
                If subgroups.Element(index).IsMaximalSubgroupOf(Me) = True Then
                    newSet.AddElement(subgroups.Element(index))
                End If
            Next index

            Return newSet
        End Function

        ''' <summary>
        ''' Returns the set of all normal subgroups of this group (includes improper subgroups).
        ''' </summary>
        ''' <returns>Returns the set of all normal subgroups as a <c>FiniteSet</c>.</returns>
        ''' <remarks></remarks>
        Public Function SetOfAllNormalSubgroups() As FiniteSet(Of FiniteGroup(Of T))
            If Me.groupProperties.ContainsKey("all normal subgroups") = False Then
                'Check to see if it is an abelian group, makes this trivial
                If Me.groupProperties.ContainsKey("abelian") = True Then
                    If Me.groupProperties.Item("abelian") = True Then
                        Me.allNormalSubgroups = Me.SetOfAllSubgroups()
                        Me.groupProperties.Add("all normal subgroups", True)
                        Return Me.allNormalSubgroups.Clone
                    End If
                End If

                Dim newSet As New FiniteSet(Of FiniteGroup(Of T))

                Dim subgroups As FiniteSet(Of FiniteGroup(Of T)) = Me.SetOfAllSubgroups

                Parallel.For(0, subgroups.Cardinality, Sub(index)
                                                           If subgroups.Element(index).IsNormalSubgroupOf(Me) = True Then
                                                               newSet.AddElement(subgroups.Element(index))
                                                           End If
                                                       End Sub)

                Me.allNormalSubgroups = newSet
                Me.groupProperties.Add("all normal subgroups", True)
            End If

            Return Me.allNormalSubgroups.Clone()
        End Function

        ''' <summary>
        ''' Returns the set of all subgroups of this group (includes improper subgroups).
        ''' </summary>
        ''' <returns>Returns the set of all subgroups as a <c>FiniteSet</c>.</returns>
        ''' <remarks></remarks>
        Public Function SetOfAllSubgroups() As FiniteSet(Of FiniteGroup(Of T))
            If Me.groupProperties.ContainsKey("all subgroups") = False Then
                Dim newSet As New FiniteSet(Of FiniteGroup(Of T))

                Dim ModifiedSet As FiniteSet(Of T)
                ModifiedSet = Me.theSet.Clone
                ModifiedSet.DeleteElement(ModifiedSet.IndexOf(Me.IdentityElement))

                Dim index As Integer
                Dim pwrSet As FiniteSet(Of FiniteSet(Of T)) = Me.theSet.PowerSet()
                Dim testOperation As FiniteBinaryOperation(Of T)
                Dim knownProps As Dictionary(Of String, Boolean) = Me.SubgroupClosedProperties
                Dim emptySet As New FiniteSet(Of T)
                Dim curSet As FiniteSet(Of T)

                'Generating the powerset is an expensive operation.
                'All subgroups must contain the identity element. So remove it, then create the powerset, then add it back in to each of these candidates.
                pwrSet = ModifiedSet.PowerSet
                For index = 0 To pwrSet.Cardinality - 1
                    curSet = pwrSet.Element(index)
                    curSet.AddElementWithoutCheck(Me.IdentityElement)
                    pwrSet.Element(index) = curSet
                Next index

                'Order of the subgroup must divide the order of the group (finite only)
                'So only bother trying those
                For index = pwrSet.Cardinality - 1 To 0 Step -1
                    If Me.Order Mod pwrSet.Element(index).Cardinality <> 0 Then
                        pwrSet.DeleteElement(index)
                    End If
                Next index

                For index = 0 To pwrSet.Cardinality - 1
                    Try
                        testOperation = Me.Operation.Restriction(pwrSet.Element(index))

                        If FiniteGroup(Of T).IsGroup(pwrSet.Element(index), testOperation) = True Then
                            newSet.AddElement(New FiniteGroup(Of T)(pwrSet.Element(index), testOperation, knownProps))
                        End If
                    Catch ex As Exception
                        'If there was an exception, the operation does not exist, so that element of the power set is not a group
                    End Try
                Next index

                Me.allSubgroups = newSet
                Me.groupProperties.Add("all subgroups", True)





                'TODO: Finish!

                'We could use the power set to find all possible subgroups. But the power set has 2^n elements and gets big fast.
                'We know that the order of the subgroup must divide the order of the group (since this is a finite group).
                'So a better algorithm would be:
                '   1) Find all factors of the order
                '   2) Find all possible subsets with those as the cardinality where one of the elements is also the identity element
                '   3) Test to see if they form a subgroup

                'Dim factors As List(Of Integer)
                'Dim subsets As New FiniteSet(Of FiniteSet(Of T))
                'Dim tempSet As FiniteSet(Of T)

                'factors = Me.FindFactors(Me.Order)

                ''Automatically at the trivial set
                'factors.Remove(1)
                'tempSet = New FiniteSet(Of T)
                'tempSet.AddElementWithoutCheck(Me.IdentityElement)
                'subsets.AddElement(tempSet)

                ''Automatically add the whole set
                'If Me.Order > 1 Then
                '    factors.Remove(Me.Order)
                '    subsets.AddElement(Me.theSet.Clone())
                'End If

                ''Now go through the list of factors and find all subsets with those cardinalities
                'Parallel.For(0, factors.Count, Sub(index)
                '                                   'The first element is always the identity element.
                '                                   'We just need to fill in the rest of the subset.

                '                                   Dim curSet As FiniteSet(Of T)
                '                                   Dim curFactor As Integer
                '                                   Dim firstElemIndex As Integer

                '                                   curFactor = factors.Item(index)

                '                                   For firstElemIndex = 0 To curFactor - 2
                '                                       curSet = New FiniteSet(Of T)
                '                                       curSet.AddElementWithoutCheck(Me.IdentityElement)

                '                                       'TODO: Finish
                '                                   Next firstElemIndex
                '                               End Sub)
            End If

            Return Me.allSubgroups.Clone()
        End Function

        ''' <summary>
        ''' Finds all factors of a given number.
        ''' </summary>
        ''' <param name="num">The number for which to find the factors.</param>
        ''' <returns>Returns all the factors of a given integer as a <c>List</c>.</returns>
        ''' <remarks></remarks>
        Private Function FindFactors(ByVal num As Integer) As List(Of Integer)
            Dim index As Integer
            Dim result As New List(Of Integer)

            result.Add(1)

            For index = 2 To num
                If num Mod index = 0 Then
                    'We found a factor
                    result.Add(index)
                End If
            Next index

            Return result
        End Function

        ''' <summary>
        ''' Returns the index of this group as a subgroup of a given supergroup.
        ''' </summary>
        ''' <param name="superGroup">The subgroup whose index is to be returned.</param>
        ''' <exception cref="NotSubgroupException">Throws NotSubgroupException if this group is not a subgroup of parameter 'superGroup'.</exception>
        ''' <returns>Returns the index of this group as a subgroup of a given supergroup.</returns>
        ''' <remarks></remarks>
        Public Function SubgroupIndex(ByVal superGroup As FiniteGroup(Of T)) As Double
            If Me.IsSubgroupOf(superGroup) = False Then
                Throw New NotSubgroupException("This group is not a subgroup of the parameter 'superGroup'.")
            End If

            Return Me.Order / superGroup.Order
        End Function


        ''' <summary>
        ''' Returns the trivial subgroup of this group.
        ''' </summary>
        ''' <returns>Returns the trivial subgroup of this group.</returns>
        ''' <remarks></remarks>
        Public Function TrivialSubgroup() As FiniteGroup(Of T)
            Dim newSet As New FiniteSet(Of T)
            newSet.AddElement(Me.IdentityElement)
            Dim newOp As FiniteBinaryOperation(Of T) = Me.Operation.Restriction(newSet)

            Return New FiniteGroup(Of T)(newSet, newOp, Me.SubgroupClosedProperties)
        End Function


        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="superGroup"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function WeylGroup(ByVal superGroup As FiniteGroup(Of T)) As FiniteGroup(Of FiniteSet(Of T))
            Dim normalizer As FiniteGroup(Of T) = superGroup.NormalizerSubgroup(Me)
            Dim centralizer As FiniteGroup(Of T) = superGroup.CentralizerSubgroup(Me)

            Return normalizer.QuotientGroup(centralizer)
        End Function


        ''' <summary>
        ''' Returns a Dictionary(Of String, Boolean) of all the properties known about this group that are inherited by any subgroup.
        ''' </summary>
        ''' <returns>Returns a Dictionary(Of String, Boolean) of all the properties known about this group that are inherited by any subgroup.</returns>
        ''' <remarks></remarks>
        Friend Function SubgroupClosedProperties() As Dictionary(Of String, Boolean)
            Dim newProps As New Dictionary(Of String, Boolean)

            If Me.groupProperties.ContainsKey("abelian") = True Then
                If Me.groupProperties.Item("abelian") = True Then
                    newProps.Add("abelian", True)
                End If
            End If

            If Me.groupProperties.ContainsKey("cyclic") = True Then
                If Me.groupProperties.Item("cyclic") = True Then
                    newProps.Add("cyclic", True)
                End If
            End If

            If Me.groupProperties.ContainsKey("dedekind") = True Then
                If Me.groupProperties.Item("dedekind") = True Then
                    newProps.Add("dedekind", True)
                End If
            End If

            If Me.groupProperties.ContainsKey("metabelian") = True Then
                If Me.groupProperties.Item("metabelian") = True Then
                    newProps.Add("metabelian", True)
                End If
            End If

            If Me.groupProperties.ContainsKey("metanilpotent") = True Then
                If Me.groupProperties.Item("metanilpotent") = True Then
                    newProps.Add("metanilpotent", True)
                End If
            End If

            If Me.groupProperties.ContainsKey("nilpotent") = True Then
                If Me.groupProperties.Item("nilpotent") = True Then
                    newProps.Add("nilpotent", True)
                End If
            End If

            If Me.groupProperties.ContainsKey("solvable") = True Then
                If Me.groupProperties.Item("solvable") = True Then
                    newProps.Add("solvable", True)
                End If
            End If

            If Me.groupProperties.ContainsKey("t*-group") = True Then
                If Me.groupProperties.Item("t*-group") = True Then
                    newProps.Add("t*-group", True)
                End If
            End If

            Return newProps
        End Function

        ''' <summary>
        ''' Returns a Dictionary(Of String, Boolean) of all the properties known about this group that are inherited by any quotient group.
        ''' </summary>
        ''' <returns>Returns a Dictionary(Of String, Boolean) of all the properties known about this group that are inherited by any quotient group.</returns>
        ''' <remarks></remarks>
        Friend Function QuotientClosedProperties() As Dictionary(Of String, Boolean)
            Dim newProps As New Dictionary(Of String, Boolean)

            If Me.groupProperties.ContainsKey("abelian") = True Then
                If Me.groupProperties.Item("abelian") = True Then
                    newProps.Add("abelian", True)
                End If
            End If

            If Me.groupProperties.ContainsKey("ambivalent") = True Then
                If Me.groupProperties.Item("ambivalent") = True Then
                    newProps.Add("ambivalent", True)
                End If
            End If

            If Me.groupProperties.ContainsKey("cyclic") = True Then
                If Me.groupProperties.Item("cyclic") = True Then
                    newProps.Add("cyclic", True)
                End If
            End If

            If Me.groupProperties.ContainsKey("dedekind") = True Then
                If Me.groupProperties.Item("dedekind") = True Then
                    newProps.Add("dedekind", True)
                End If
            End If

            If Me.groupProperties.ContainsKey("metabelian") = True Then
                If Me.groupProperties.Item("metabelian") = True Then
                    newProps.Add("metabelian", True)
                End If
            End If

            If Me.groupProperties.ContainsKey("metanilpotent") = True Then
                If Me.groupProperties.Item("metanilpotent") = True Then
                    newProps.Add("metanilpotent", True)
                End If
            End If

            If Me.groupProperties.ContainsKey("nilpotent") = True Then
                If Me.groupProperties.Item("nilpotent") = True Then
                    newProps.Add("nilpotent", True)
                End If
            End If

            If Me.groupProperties.ContainsKey("perfect") = True Then
                If Me.groupProperties.Item("perfect") = True Then
                    newProps.Add("perfect", True)
                End If
            End If

            If Me.groupProperties.ContainsKey("solvable") = True Then
                If Me.groupProperties.Item("solvable") = True Then
                    newProps.Add("solvable", True)
                End If
            End If

            If Me.groupProperties.ContainsKey("t-group") = True Then
                If Me.groupProperties.Item("t-group") = True Then
                    newProps.Add("t-group", True)
                End If
            End If

            Return newProps
        End Function

        ''' <summary>
        ''' Returns a Dictionary(Of String, Boolean) of all the properties known about this group and another group that are inherited by a direct product.
        ''' </summary>
        ''' <typeparam name="G">The Type of elements in the other group.</typeparam>
        ''' <param name="otherGroup">The other group with which a direct product is being formed.</param>
        ''' <returns>Returns a Dictionary(Of String, Boolean) of all the properties known about this group and another group that are inherited by a direct product.</returns>
        ''' <remarks></remarks>
        Friend Function ProductClosedProperties(Of G As {Class, New, IEquatable(Of G)})(ByVal otherGroup As FiniteGroup(Of G)) As Dictionary(Of String, Boolean)
            Dim newProps As New Dictionary(Of String, Boolean)

            'The product of abelian groups is abelian
            If Me.groupProperties.ContainsKey("abelian") And otherGroup.groupProperties.ContainsKey("abelian") Then
                If Me.groupProperties.Item("abelian") = True And otherGroup.groupProperties.ContainsKey("abelian") = True Then
                    newProps.Add("abelian", True)
                End If
            End If

            'The product of ambivalent groups is ambivalent
            If Me.groupProperties.ContainsKey("ambivalent") = True Then
                If Me.groupProperties.Item("ambivalent") = True Then
                    newProps.Add("ambivalent", True)
                End If
            End If

            'Taken from: http://groupprops.subwiki.org/wiki/Cyclic_group
            'A direct product of cyclic groups need not be cyclic.
            'It is cyclic if and only if the two groups have relatively prime orders
            'TODO: Implement this test

            'The product of metabelian groups is metabelian
            If Me.groupProperties.ContainsKey("metabelian") And otherGroup.groupProperties.ContainsKey("metabelian") Then
                If Me.groupProperties.Item("metabelian") = True And otherGroup.groupProperties.ContainsKey("metabelian") = True Then
                    newProps.Add("metabelian", True)
                End If
            End If

            'The product of nilpotent groups is nilpotent
            If Me.groupProperties.ContainsKey("nilpotent") And otherGroup.groupProperties.ContainsKey("nilpotent") Then
                If Me.groupProperties.Item("nilpotent") = True And otherGroup.groupProperties.ContainsKey("nilpotent") = True Then
                    newProps.Add("nilpotent", True)
                End If
            End If

            'The product of perfect groups is perfect
            If Me.groupProperties.ContainsKey("perfect") And otherGroup.groupProperties.ContainsKey("perfect") Then
                If Me.groupProperties.Item("perfect") = True And otherGroup.groupProperties.ContainsKey("perfect") = True Then
                    newProps.Add("perfect", True)
                End If
            End If

            'The product of solvable groups is solvable
            If Me.groupProperties.ContainsKey("solvable") And otherGroup.groupProperties.ContainsKey("solvable") Then
                If Me.groupProperties.Item("solvable") = True And otherGroup.groupProperties.ContainsKey("solvable") = True Then
                    newProps.Add("solvable", True)
                End If
            End If

            Return newProps
        End Function

#End Region

        ''' <summary>
        ''' The map for a quotient group.
        ''' </summary>
        ''' <remarks></remarks>
        Private Class QuotientMap
            Inherits Map(Of Tuple, FiniteSet(Of T))

            Private myGroupMap As Map(Of Tuple, T)

            Public Sub New(ByVal groupMap As Map(Of Tuple, T))
                Me.myGroupMap = groupMap
            End Sub

            Public Overrides Function ApplyMap(ByVal input As Tuple) As FiniteSet(Of T)
                Dim newSet As New FiniteSet(Of T)
                Dim set1 As FiniteSet(Of T) = CType(input.Element(0), FiniteSet(Of T))
                Dim set2 As FiniteSet(Of T) = CType(input.Element(1), FiniteSet(Of T))

                Dim index1 As Integer
                Dim index2 As Integer
                Dim curTup As New Tuple(2)

                For index1 = 0 To set1.Cardinality - 1
                    For index2 = 0 To set2.Cardinality - 1
                        curTup.Element(0) = set1.Element(index1)
                        curTup.Element(1) = set2.Element(index2)

                        newSet.AddElement(Me.myGroupMap.ApplyMap(curTup))
                    Next index2
                Next index1

                Return newSet
            End Function
        End Class

        ''' <summary>
        ''' The map for a direct product (cartesian product) group.
        ''' </summary>
        ''' <typeparam name="G"></typeparam>
        ''' <remarks></remarks>
        Private Class DirectProductMap(Of G As {Class, New, IEquatable(Of G)})
            Inherits Map(Of Tuple, Tuple)

            Private myLeftGroupMap As Map(Of Tuple, T)
            Private myRightGroupMap As Map(Of Tuple, G)

            Public Sub New(ByVal leftGroupMap As Map(Of Tuple, T), ByVal rightGroupMap As Map(Of Tuple, G))
                Me.myLeftGroupMap = leftGroupMap
                Me.myRightGroupMap = rightGroupMap
            End Sub

            Public Overrides Function ApplyMap(ByVal input As Tuple) As Tuple
                Dim lhs As Tuple = CType(input.Element(0), Tuple)
                Dim rhs As Tuple = CType(input.Element(1), Tuple)

                Dim lhs1 As T = CType(lhs.Element(0), T)
                Dim lhs2 As G = CType(lhs.Element(1), G)
                Dim rhs1 As T = CType(rhs.Element(0), T)
                Dim rhs2 As G = CType(rhs.Element(1), G)

                Dim tup1 As New Tuple(2)
                Dim tup2 As New Tuple(2)
                Dim result As New Tuple(2)

                tup1.Element(0) = lhs1
                tup1.Element(1) = rhs1
                tup2.Element(0) = lhs2
                tup2.Element(1) = rhs2

                result.Element(0) = Me.myLeftGroupMap.ApplyMap(tup1)
                result.Element(1) = Me.myRightGroupMap.ApplyMap(tup2)

                Return result
            End Function
        End Class

    End Class

    ''' <summary>
    ''' Represents an error that an object was not a subgroup. 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class NotSubgroupException
        Inherits Exception

#Region "  Constructors  "

        Public Sub New()
            MyBase.New("The parameter is not a subgroup of the group.")
        End Sub

        Public Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub

#End Region

    End Class

End Namespace
