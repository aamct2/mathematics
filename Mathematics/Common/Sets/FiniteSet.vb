
Imports System.Collections.Generic
Imports System.Text

''' <summary>
''' Represents a finite set with elements of type T
''' </summary>
''' <typeparam name="T"></typeparam>
''' <remarks></remarks>
Public Class FiniteSet(Of T As {Class, IEquatable(Of T)})
    Implements IEquatable(Of FiniteSet(Of T)), ISubtractable(Of FiniteSet(Of T))

    Private myElements As New List(Of T)

#Region "  Constructors  "

    Public Sub New()

    End Sub

    Public Sub New(ByVal elementsList As List(Of T))
        Me.myElements = elementsList
    End Sub

#End Region

#Region "  Properties  "

    ''' <summary>
    ''' Gets/Sets an element in this set.
    ''' </summary>
    ''' <param name="index"></param>
    ''' <exception cref="IndexOutOfRangeException">Throws IndexOutOfRangeException if parameter 'index' is out of range for the set.</exception>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Element(ByVal index As Integer) As T
        Get
            If index <= Me.myElements.Count - 1 Then
                Return Me.myElements.Item(index)
            Else
                Throw New IndexOutOfRangeException("Index out of range in FiniteSet.")
            End If
        End Get
        Set(ByVal value As T)
            If index <= Me.myElements.Count - 1 Then
                Me.myElements.Item(index) = value
            Else
                Throw New IndexOutOfRangeException("Index out of range in FiniteSet.")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Returns the null set (also known as the empty set).
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property NullSet() As FiniteSet(Of T) Implements IAdditiveIdentity(Of FiniteSet(Of T)).AdditiveIdentity
        Get
            Return New FiniteSet(Of T)
        End Get
    End Property

#End Region

#Region "  Methods  "

    ''' <summary>
    ''' Adds an element to this set.
    ''' </summary>
    ''' <param name="newElement"></param>
    ''' <remarks></remarks>
    Public Sub AddElement(ByVal newElement As T)
        Dim elem As T
        Dim obj As New Object

        SyncLock obj

            For Each elem In Me.myElements
                If elem.Equals(newElement) Then
                    Exit Sub
                End If
            Next elem

            Me.myElements.Add(newElement)

        End SyncLock
    End Sub

    ''' <summary>
    ''' Add an element to this set, without checking to see if it's already in the set.
    ''' </summary>
    ''' <param name="newElement"></param>
    ''' <remarks></remarks>
    Friend Sub AddElementWithoutCheck(ByVal newElement As T)
        Me.myElements.Add(newElement)
    End Sub

    ''' <summary>
    ''' Returns the number of elements in this set.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Cardinality() As Integer
        Return Me.myElements.Count
    End Function

    ''' <summary>
    ''' Creates a clone of this set.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Clone() As FiniteSet(Of T)
        Dim newSet As New FiniteSet(Of T)
        Dim index As Integer

        For index = 0 To Me.Cardinality - 1
            newSet.AddElement(Me.Element(index))
        Next index

        Return newSet
    End Function

    ''' <summary>
    ''' Determines whether this set contains a given element.
    ''' </summary>
    ''' <param name="elem"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Contains(ByVal elem As T) As Boolean
        Dim index As Integer

        For index = 0 To Me.Cardinality - 1
            If Me.Element(index).Equals(elem) Then Return True
        Next index

        Return False
    End Function

    ''' <summary>
    ''' Deletes an elements from this set.
    ''' </summary>
    ''' <param name="index"></param>
    ''' <exception cref="IndexOutOfRangeException">Throws IndexOutOfRangeException if parameter 'index' is out of range for the set.</exception>
    ''' <remarks></remarks>
    Public Sub DeleteElement(ByVal index As Integer)
        If index <= Me.myElements.Count - 1 Then
            Me.myElements.RemoveAt(index)
        Else
            Throw New IndexOutOfRangeException("Index out of range in FiniteSet.")
        End If
    End Sub

    ''' <summary>
    ''' Returns the set-theoretic difference of this set minus another set.
    ''' </summary>
    ''' <param name="otherSet"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Difference(ByVal otherSet As FiniteSet(Of T)) As FiniteSet(Of T) Implements ISubtractable(Of FiniteSet(Of T)).Subtract
        Dim lIndex As Integer
        Dim rIndex As Integer
        Dim newSet As New FiniteSet(Of T)
        Dim found As Boolean

        For lIndex = 0 To Me.Cardinality - 1
            found = False

            For rIndex = 0 To otherSet.Cardinality - 1
                If Me.Element(lIndex).Equals(otherSet.Element(rIndex)) = True Then found = True
            Next rIndex

            If found = False Then
                newSet.AddElement(Me.Element(lIndex))
            End If
        Next lIndex

        Return newSet
    End Function

    ''' <summary>
    ''' Returns the direct product (also known as the cartesian product) of this set with another set.
    ''' </summary>
    ''' <typeparam name="G"></typeparam>
    ''' <param name="Set2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DirectProduct(Of G As {Class, New, IEquatable(Of G)})(ByVal Set2 As FiniteSet(Of G)) As FiniteSet(Of Tuple)
        Dim lIndex As Integer
        Dim rIndex As Integer
        Dim newSet As New FiniteSet(Of Tuple)
        Dim curPair As Tuple
        Dim curLeftElem As T

        'By convention, anything direct product the null set is the null set
        'For all sets A:    A x {} = {}
        If Me.Equals(Me.NullSet) = True Or Set2.Equals(Set2.NullSet) = True Then
            Return newSet.NullSet
        End If

        For lIndex = 0 To Me.Cardinality - 1
            curLeftElem = Me.Element(lIndex)

            For rIndex = 0 To Set2.Cardinality - 1
                curPair = New Tuple(2)
                curPair.Element(0) = curLeftElem
                curPair.Element(1) = Set2.Element(rIndex)

                newSet.AddElementWithoutCheck(curPair)
            Next rIndex
        Next lIndex

        Return newSet
    End Function

    ''' <summary>
    ''' Determines whether this set is equal to a given set.
    ''' </summary>
    ''' <param name="Set2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shadows Function Equals(ByVal Set2 As FiniteSet(Of T)) As Boolean Implements System.IEquatable(Of FiniteSet(Of T)).Equals
        If Me.Cardinality <> Set2.Cardinality Then Return False

        Dim lIndex As Integer
        Dim rIndex As Integer
        Dim found As Boolean

        For lIndex = 0 To Me.Cardinality - 1
            found = False

            For rIndex = 0 To Me.Cardinality - 1
                If Me.Element(lIndex).Equals(Set2.Element(rIndex)) Then found = True
            Next rIndex

            If found = False Then Return False
        Next lIndex

        Return True
    End Function

    ''' <summary>
    ''' Returns the index of 'elem' in the set.
    ''' </summary>
    ''' <param name="elem">The element for which to find the index.</param>
    ''' <returns>Returns the index of 'elem' in the set or -1 if the element is not found.</returns>
    ''' <remarks></remarks>
    Public Function IndexOf(ByVal elem As T) As Integer
        Dim index As Integer

        For index = 0 To Me.Cardinality
            If Me.Element(index).Equals(elem) Then
                Return index
            End If
        Next index

        Return -1
    End Function

    ''' <summary>
    ''' Returns the intersection of this set with a given set.
    ''' </summary>
    ''' <param name="otherSet"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Intersection(ByVal otherSet As FiniteSet(Of T)) As FiniteSet(Of T)
        Dim newSet As New FiniteSet(Of T)

        Dim lIndex As Integer
        Dim rIndex As Integer

        'Don't waste time cranking the intersection if one of the sets is the null set
        If Me.Equals(Me.NullSet) = True Or otherSet.Equals(Me.NullSet) = True Then
            Return Me.NullSet
        End If

        For lIndex = 0 To Me.Cardinality - 1
            For rIndex = 0 To otherSet.Cardinality - 1
                If Me.Element(lIndex).Equals(otherSet.Element(rIndex)) Then
                    newSet.AddElement(Me.Element(lIndex))
                End If
            Next rIndex
        Next lIndex

        Return newSet
    End Function

    ''' <summary>
    ''' Determines whether this set is a subset of a given set.
    ''' </summary>
    ''' <param name="superSet"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsSubsetOf(ByVal superSet As FiniteSet(Of T)) As Boolean
        Dim index As Integer

        For index = 0 To Me.Cardinality - 1
            If superSet.Contains(Me.Element(index)) = False Then Return False
        Next index

        Return True
    End Function

    ''' <summary>
    ''' Determines whether a given set of sets is a partition of this set.
    ''' </summary>
    ''' <param name="testPartition"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsPartition(ByVal testPartition As FiniteSet(Of FiniteSet(Of T))) As Boolean
        Dim index1 As Integer
        Dim index2 As Integer
        Dim testSet As New FiniteSet(Of T)

        'Check to see that it is a set of non-empty subsets
        For index1 = 0 To testPartition.Cardinality - 1
            If testPartition.Element(index1).Equals(Me.NullSet) = True Or testPartition.Element(index1).IsSubsetOf(Me) = False Then
                Return False
            End If
        Next index1

        'Check to see the union of all elements of testPartition is this set
        For index1 = 0 To testPartition.Cardinality - 1
            testSet = testSet.Union(testPartition.Element(index1))
        Next index1
        If testSet.Equals(Me) = False Then Return False

        'Check to see that the elements of testPartition are pairwise-disjoint
        For index1 = 0 To testPartition.Cardinality - 1
            For index2 = 0 To testPartition.Cardinality - 1
                If index1 <> index2 And testPartition.Element(index1).Intersection(testPartition.Element(index2)).Equals(Me.NullSet) = False Then
                    Return False
                End If
            Next index2
        Next index1

        Return True
    End Function

    ''' <summary>
    ''' Determines whether this set is a proper subset of a given set.
    ''' </summary>
    ''' <param name="superSet"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsProperSubsetOf(ByVal superSet As FiniteSet(Of T)) As Boolean
        If Me.IsSubsetOf(superSet) = True And Me.Equals(superSet) = False Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Returns the powerset of this set.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PowerSet() As FiniteSet(Of FiniteSet(Of T))
        Dim newPowerSet As New FiniteSet(Of FiniteSet(Of T))

        If Me.Equals(Me.NullSet) = True Then
            newPowerSet.AddElement(Me.NullSet)
            Return newPowerSet
        End If

        Dim baseElemSet As New FiniteSet(Of T)
        baseElemSet.AddElement(Me.Element(0))
        Dim differenceSet As FiniteSet(Of T) = Me.Difference(baseElemSet)

        Return differenceSet.PowerSet.Union(FamilyPlusElement(Me.Element(0), differenceSet.PowerSet))
    End Function


    Private Function FamilyPlusElement(ByVal elem As T, ByVal family As FiniteSet(Of FiniteSet(Of T))) As FiniteSet(Of FiniteSet(Of T))
        Dim index As Integer
        Dim newFamily As New FiniteSet(Of FiniteSet(Of T))
        Dim elemSet As New FiniteSet(Of T)

        elemSet.AddElement(elem)

        For index = 0 To family.Cardinality - 1
            'newFamily.AddElement(family.Element(index).Union(elemSet))
            newFamily.AddElementWithoutCheck(family.Element(index).Union(elemSet))
        Next index

        Return newFamily
    End Function

    ''' <summary>
    ''' Returns a string listing all elements of this set.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function ToString() As String
        Dim strBuilder As New StringBuilder
        Dim index As Integer

        strBuilder.Append("{")

        If Me.Cardinality > 0 Then
            strBuilder.Append(Me.Element(0).ToString)
            For index = 1 To Me.Cardinality - 1
                strBuilder.Append(", " & Me.Element(index).ToString)
            Next index
        End If

        strBuilder.Append("}")

        Return strBuilder.ToString
    End Function

    ''' <summary>
    ''' Returns the union of this set with another set.
    ''' </summary>
    ''' <param name="otherSet"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Union(ByVal otherSet As FiniteSet(Of T)) As FiniteSet(Of T) Implements IAddable(Of FiniteSet(Of T)).Add
        Dim newSet As New FiniteSet(Of T)

        Dim index As Integer

        'Don't waste time cranking the intersection if one of the sets is the null set
        If Me.Equals(Me.NullSet) = True Then
            Return otherSet.Clone
        ElseIf otherSet.Equals(Me.NullSet) = True Then
            Return Me.Clone
        End If

        For index = 0 To Me.Cardinality - 1
            newSet.AddElement(Me.Element(index))
        Next index

        For index = 0 To otherSet.Cardinality - 1
            newSet.AddElement(otherSet.Element(index))
        Next index

        Return newSet
    End Function

#End Region

#Region "  Operators  "

    ''' <summary>
    ''' Performs set union on the two sets.
    ''' </summary>
    ''' <param name="lhs"></param>
    ''' <param name="rhs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Operator +(ByVal lhs As FiniteSet(Of T), ByVal rhs As FiniteSet(Of T)) As FiniteSet(Of T)
        Return lhs.Union(rhs)
    End Operator

    ''' <summary>
    ''' Performs set-theoretic difference on the two sets.
    ''' </summary>
    ''' <param name="lhs"></param>
    ''' <param name="rhs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Operator -(ByVal lhs As FiniteSet(Of T), ByVal rhs As FiniteSet(Of T)) As FiniteSet(Of T)
        Return lhs.Difference(rhs)
    End Operator

    ''' <summary>
    ''' Performs set intersection on the two sets.
    ''' </summary>
    ''' <param name="lhs"></param>
    ''' <param name="rhs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Operator And(ByVal lhs As FiniteSet(Of T), ByVal rhs As FiniteSet(Of T)) As FiniteSet(Of T)
        Return lhs.Intersection(rhs)
    End Operator

    ''' <summary>
    ''' Performs set union on the two sets.
    ''' </summary>
    ''' <param name="lhs"></param>
    ''' <param name="rhs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Operator Or(ByVal lhs As FiniteSet(Of T), ByVal rhs As FiniteSet(Of T)) As FiniteSet(Of T)
        Return lhs.Union(rhs)
    End Operator

#End Region

End Class

