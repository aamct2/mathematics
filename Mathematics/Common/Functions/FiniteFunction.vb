
Imports System.Collections.Generic

Public Class FiniteFunction(Of T As {Class, New, IEquatable(Of T)}, G As {Class, New, IEquatable(Of G)})
    Implements IEquatable(Of FiniteFunction(Of T, G))

    Private myDomain As FiniteSet(Of T)
    Private myCodomain As FiniteSet(Of G)
    Private myRelation As Map(Of T, G)
    Protected Friend functionProperties As New Dictionary(Of String, Boolean)
    Private myImage As FiniteSet(Of G)
    Private myInverse As FiniteFunction(Of G, T)

#Region "  Constructors  "

    Public Sub New(ByVal newDomain As FiniteSet(Of T), ByVal newCodomain As FiniteSet(Of G), ByVal newRelation As Map(Of T, G))
        Me.myDomain = newDomain
        Me.myCodomain = newCodomain

        Dim index As Integer

        For index = 0 To Me.Domain.Cardinality - 1
            If Me.Codomain.Contains(newRelation.ApplyMap(Me.Domain.Element(index))) = False Then
                Throw New Exception("The codomain does not contain the output element for the domain element at index " & index & ".")
            End If
        Next index

        Me.myRelation = newRelation
    End Sub

#End Region

#Region "  Properties  "

    Public ReadOnly Property Domain() As FiniteSet(Of T)
        Get
            Return Me.myDomain
        End Get
    End Property

    Public ReadOnly Property Codomain() As FiniteSet(Of G)
        Get
            Return Me.myCodomain
        End Get
    End Property

    Public ReadOnly Property theRelation() As Map(Of T, G)
        Get
            Return Me.myRelation
        End Get
    End Property

#End Region

#Region " Methods "

    Public Function ApplyMap(ByVal input As T) As G

        If Me.Domain.Contains(input) = False Then
            Throw New Exception("Domain does not contain input element!")
        End If

        'No need to check if the output is in the codomain.
        'The relation is checked to be well-defined when theRelation is set.

        Return Me.theRelation.ApplyMap(input)
    End Function

    ''' <summary>
    ''' Returns a composition of function in which this function is the outer function of the composition.
    ''' </summary>
    ''' <typeparam name="S"></typeparam>
    ''' <param name="innerFunction"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Composition(Of S As {Class, New, IEquatable(Of S)})(ByVal innerFunction As FiniteFunction(Of S, T)) As FiniteFunction(Of S, G)
        If innerFunction.Codomain.Equals(Me.Domain) = False Then
            Throw New Exception("The codomain of innerFunction must be equal to the domain of this function.")
        End If

        Dim newMap As New CompositionMap(Of S, T, G)(innerFunction.theRelation, Me.theRelation)
        Dim newFunc As New FiniteFunction(Of S, G)(innerFunction.Domain, Me.Codomain, newMap)

        If Me.functionProperties.ContainsKey("injective") = True And innerFunction.functionProperties.ContainsKey("injective") = True Then
            If Me.IsInjective = True And innerFunction.IsInjective = True Then
                newFunc.functionProperties.Add("injective", True)
            End If
        End If

        If Me.functionProperties.ContainsKey("surjective") = True And innerFunction.functionProperties.ContainsKey("surjective") = True Then
            If Me.IsSurjective = True And innerFunction.IsSurjective = True Then
                newFunc.functionProperties.Add("surjective", True)
            End If
        End If

        If Me.functionProperties.ContainsKey("bijective") = True And innerFunction.functionProperties.ContainsKey("bijective") = True Then
            If Me.IsBijective = True And innerFunction.IsBijective = True Then
                newFunc.functionProperties.Add("bijective", True)
            End If
        End If

        Return newFunc
    End Function

    Public Shadows Function Equals(ByVal other As FiniteFunction(Of T, G)) As Boolean Implements System.IEquatable(Of FiniteFunction(Of T, G)).Equals
        If other.Domain.Equals(Me.Domain) = False Or other.Codomain.Equals(Me.Codomain) = False Then
            Return False
        End If

        Dim index As Integer

        For index = 0 To Me.Domain.Cardinality - 1
            If Me.ApplyMap(Me.Domain.Element(index)).Equals(other.ApplyMap(Me.Domain.Element(index))) = False Then
                Return False
            End If
        Next index

        Return True
    End Function

    Public Function EquivalentMaps(ByVal otherMap As Map(Of T, G), ByVal testDomain As FiniteSet(Of T), ByVal testCodomain As FiniteSet(Of G)) As Boolean
        Try
            Dim testFunc1 As New FiniteFunction(Of T, G)(testDomain, testCodomain, Me.theRelation)
            Dim testFunc2 As New FiniteFunction(Of T, G)(testDomain, testCodomain, otherMap)

            Return testFunc1.Equals(testFunc2)
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Returns the image of the function.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ImageSet() As FiniteSet(Of G)
        If IsNothing(Me.myImage) = True Then
            Dim domIndex As Integer
            Dim ImgSet As New FiniteSet(Of G)

            For domIndex = 0 To Me.Domain.Cardinality - 1
                ImgSet.AddElement(Me.ApplyMap(Me.Domain.Element(domIndex)))
            Next domIndex

            Me.myImage = ImgSet

            Return ImgSet
        Else
            Return Me.myImage
        End If
    End Function

    ''' <summary>
    ''' Returns the inverse image of a set.
    ''' </summary>
    ''' <param name="ResultSet">The sets of elements who inverse image is desired.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InverseImageSet(ByRef ResultSet As FiniteSet(Of G)) As FiniteSet(Of T)
        Dim ResultSetIndex As Integer
        Dim InvImgSet As New FiniteSet(Of T)
        Dim curResult As G
        Dim domIndex As Integer

        For ResultSetIndex = 0 To ResultSet.Cardinality - 1
            curResult = ResultSet.Element(ResultSetIndex)

            'TODO: Could be optimized
            For domIndex = 0 To Me.Domain.Cardinality - 1
                If Me.ApplyMap(Me.Domain.Element(domIndex)).Equals(curResult) Then
                    InvImgSet.AddElement(Me.Domain.Element(domIndex))
                End If
            Next domIndex
        Next ResultSetIndex

        Return InvImgSet
    End Function

    ''' <summary>
    ''' Returns the inverse of this function if it exists. Otherwise throws an UndefinedException.
    ''' </summary>
    ''' <returns></returns>
    ''' <exception cref="UndefiniedException">Throws an UndefinedException if an inverse function does not exist (ie. if this function is not bijective).</exception>
    ''' <remarks></remarks>
    Public Function InverseFunction() As FiniteFunction(Of G, T)
        If IsNothing(Me.myInverse) = True Then
            If Me.IsBijective = False Then
                Throw New UndefiniedException("This function is not bijective and therefore an inverse function does not exist.")
            End If

            Dim InverseRelation As New FiniteSet(Of OrderedPair(Of G, T))

            Dim index As Integer
            Dim curInput As T

            For index = 0 To Me.Domain.Cardinality - 1
                curInput = Me.Domain.Element(index)

                InverseRelation.AddElement(New OrderedPair(Of G, T)(Me.ApplyMap(curInput), curInput))
            Next index

            Dim InverseMap As New SetDefinedMap(Of G, T)(InverseRelation)

            Me.myInverse = New FiniteFunction(Of G, T)(Me.Codomain, Me.Domain, InverseMap)
        End If

        Return Me.myInverse
    End Function

    Public Function IsInjective() As Boolean
        If Me.functionProperties.ContainsKey("injective") = False Then
            Dim index1 As Integer
            Dim index2 As Integer

            For index1 = 0 To Me.Domain.Cardinality - 1
                For index2 = 0 To Me.Domain.Cardinality - 1
                    If index1 <> index2 Then
                        If Me.ApplyMap(Me.Domain.Element(index1)).Equals(Me.ApplyMap(Me.Domain.Element(index2))) = True Then
                            Me.functionProperties.Add("injective", False)
                            Return False
                        End If
                    End If
                Next index2
            Next index1

            Me.functionProperties.Add("injective", True)
            Return True
        Else
            Return Me.functionProperties.Item("injective")
        End If
    End Function

    Public Function IsSurjective() As Boolean
        If Me.functionProperties.ContainsKey("surjective") = False Then
            Dim domIndex As Integer
            Dim codomIndex As Integer
            Dim found As Boolean

            For codomIndex = 0 To Me.Codomain.Cardinality - 1
                found = False

                For domIndex = 0 To Me.Domain.Cardinality - 1
                    If Me.ApplyMap(Me.Domain.Element(domIndex)).Equals(Me.Codomain.Element(codomIndex)) = True Then
                        found = True
                        Exit For
                    End If
                Next domIndex

                If found = False Then
                    Me.functionProperties.Add("surjective", False)
                    Return False
                End If
            Next codomIndex

            Me.functionProperties.Add("surjective", True)
            Return True
        Else
            Return Me.functionProperties.Item("surjective")
        End If
    End Function

    Public Function IsBijective() As Boolean
        If Me.functionProperties.ContainsKey("bijective") = False Then
            If Me.functionProperties.ContainsKey("injective") = False Then
                Me.IsInjective()
            End If

            If Me.functionProperties.ContainsKey("surjective") = False Then
                Me.IsSurjective()
            End If

            If Me.functionProperties.Item("injective") = True And Me.functionProperties.Item("surjective") = True Then
                Me.functionProperties.Add("bijective", True)
                Return True
            Else
                Me.functionProperties.Add("bijective", False)
                Return False
            End If
        Else
            Return Me.functionProperties.Item("bijective")
        End If
    End Function

    Public Function Restriction(ByVal newDomain As FiniteSet(Of T)) As FiniteFunction(Of T, G)
        If newDomain.IsSubsetOf(Me.Domain) = False Then
            Throw New Exception("The newDomain is not a subset of this function's Domain.")
        End If

        Dim newFunc As New FiniteFunction(Of T, G)(newDomain, Me.Codomain, Me.theRelation)

        'Injectivity is inherited by resriction
        If Me.functionProperties.ContainsKey("injective") = True Then
            If Me.functionProperties.Item("injective") = True Then
                newFunc.functionProperties.Add("injective", True)
            End If
        End If

        Return newFunc
    End Function

#End Region

End Class
