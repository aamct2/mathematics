
Imports System.Collections.Generic

Namespace Topology

    Public Class FiniteTopologicalSpace(Of T As {Class, New, IEquatable(Of T)})
        Private mySet As FiniteSet(Of T)
        Private myTopology As FiniteSet(Of FiniteSet(Of T))

        Protected Friend topologyProperties As New Dictionary(Of String, Boolean)

#Region "  Constructors  "

        Public Sub New(ByVal newSet As FiniteSet(Of T), ByVal newTopology As FiniteSet(Of FiniteSet(Of T)))
            Me.mySet = newSet

            'Determine whether the topology satisfies the definition of topology on the set

            'Are all sets in the topology subsets of the space?
            Dim sampleSet As FiniteSet(Of T)
            Dim index As Integer

            For index = 0 To newTopology.Cardinality - 1
                sampleSet = newTopology.Element(index)

                If sampleSet.IsSubsetOf(Me.theSet) = False Then
                    Throw New DoesNotSatisfyPropertyException("The topology is not a set of subsets of the set of the space.")
                End If
            Next index

            'Are both the empty set and the whole set elements of the topology?
            If newTopology.Contains(Me.theSet.NullSet) = False Then
                Throw New DoesNotSatisfyPropertyException("The empty set is not an element of the topology.")
            ElseIf newTopology.Contains(Me.theSet) = False Then
                Throw New DoesNotSatisfyPropertyException("The whole set is not an element of the topology.")
            End If

            'Is any union of elements of the topology itself an element of the topology?
            ' AND
            'Is any finite intersection of elements of the topology itself an element of the topology?

            Dim PowerSetOfTopology As FiniteSet(Of FiniteSet(Of FiniteSet(Of T)))
            Dim PowerSetIndex As Integer
            Dim SetIndex As Integer
            Dim curUnion As FiniteSet(Of T)
            Dim curIntersection As FiniteSet(Of T)
            Dim curSubset As FiniteSet(Of FiniteSet(Of T))

            PowerSetOfTopology = Me.theTopology.PowerSet

            For PowerSetIndex = 0 To PowerSetOfTopology.Cardinality - 1
                curUnion = New FiniteSet(Of T)
                curIntersection = New FiniteSet(Of T)
                curSubset = PowerSetOfTopology.Element(PowerSetIndex)

                For SetIndex = 0 To curSubset.Cardinality - 1
                    curUnion = curUnion.Union(curSubset.Element(SetIndex))
                    curIntersection = curIntersection.Intersection(curSubset.Element(SetIndex))
                Next SetIndex

                If Me.theTopology.Contains(curUnion) = False Then
                    Throw New DoesNotSatisfyPropertyException("There exists a union of elements of the toplogy which is not an element of the topology.")
                ElseIf Me.theTopology.Contains(curIntersection) = False Then
                    Throw New DoesNotSatisfyPropertyException("There exists a finite intersection of elements of the toplogy which is not an element of the topology.")
                End If
            Next PowerSetIndex
        End Sub

#End Region

#Region "  Properties  "

        Public ReadOnly Property theSet() As FiniteSet(Of T)
            Get
                Return Me.mySet
            End Get
        End Property

        Public ReadOnly Property theTopology() As FiniteSet(Of FiniteSet(Of T))
            Get
                Return Me.myTopology
            End Get
        End Property

#End Region

#Region "  Methods  "

        ''' <summary>
        ''' Determine whether this space is a Kolmogorov Space (T_0).
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsKolmogorovSpace() As Boolean
            If Me.topologyProperties.ContainsKey("kolmogorov") = False Then

                'Frechet implies Kolmogorov, check to see if we've calculated that already.
                If Me.topologyProperties.ContainsKey("frechet") = True Then
                    If Me.topologyProperties("frechet") = True Then
                        Me.topologyProperties.Add("kolmogorov", True)
                        Return True
                    End If
                End If

                Dim ElementIndex1 As Integer
                Dim ElementIndex2 As Integer
                Dim OpenSetIndex As Integer
                Dim FoundContainer1 As Boolean
                Dim FoundContainer2 As Boolean
                Dim curOpenSet As FiniteSet(Of T)
                Dim Element1 As T
                Dim Element2 As T

                For ElementIndex1 = 0 To Me.theSet.Cardinality - 1
                    Element1 = Me.theSet.Element(ElementIndex1)

                    For ElementIndex2 = ElementIndex1 + 1 To Me.theSet.Cardinality - 1
                        Element2 = Me.theSet.Element(ElementIndex2)
                        FoundContainer1 = False
                        FoundContainer2 = False

                        For OpenSetIndex = 0 To Me.theTopology.Cardinality - 1
                            curOpenSet = Me.theTopology.Element(OpenSetIndex)

                            If curOpenSet.Contains(Element1) = True And curOpenSet.Contains(Element2) = False Then
                                FoundContainer1 = True
                            ElseIf curOpenSet.Contains(Element1) = False And curOpenSet.Contains(Element1) = True Then
                                FoundContainer2 = True
                            End If
                        Next OpenSetIndex

                        If FoundContainer1 = False And FoundContainer2 = False Then
                            Me.topologyProperties.Add("kolmogorov", False)
                            Return False
                        End If
                    Next ElementIndex2
                Next ElementIndex1

                Me.topologyProperties.Add("kolmogorov", True)
            End If

            Return Me.topologyProperties.Item("kolmogorov")
        End Function


        ''' <summary>
        ''' Determine whether this space is a Fréchet Space (T_1).
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsFrechet() As Boolean
            If Me.topologyProperties.ContainsKey("frechet") = False Then
                Dim ElementIndex1 As Integer
                Dim ElementIndex2 As Integer
                Dim OpenSetIndex As Integer
                Dim FoundContainer1 As Boolean
                Dim FoundContainer2 As Boolean
                Dim curOpenSet As FiniteSet(Of T)
                Dim Element1 As T
                Dim Element2 As T

                For ElementIndex1 = 0 To Me.theSet.Cardinality - 1
                    Element1 = Me.theSet.Element(ElementIndex1)

                    For ElementIndex2 = ElementIndex1 + 1 To Me.theSet.Cardinality - 1
                        Element2 = Me.theSet.Element(ElementIndex2)
                        FoundContainer1 = False
                        FoundContainer2 = False

                        For OpenSetIndex = 0 To Me.theTopology.Cardinality - 1
                            curOpenSet = Me.theTopology.Element(OpenSetIndex)

                            If curOpenSet.Contains(Element1) = True And curOpenSet.Contains(Element2) = False Then
                                FoundContainer1 = True
                            ElseIf curOpenSet.Contains(Element1) = False And curOpenSet.Contains(Element1) = True Then
                                FoundContainer2 = True
                            End If
                        Next OpenSetIndex

                        If FoundContainer1 = False Or FoundContainer2 = False Then
                            Me.topologyProperties.Add("frechet", False)
                            Return False
                        End If
                    Next ElementIndex2
                Next ElementIndex1

                Me.topologyProperties.Add("frechet", True)
            End If

            Return Me.topologyProperties.Item("frechet")
        End Function

        ''' <summary>
        ''' Determines whether a function between two topological spaces is continuous.
        ''' </summary>
        ''' <typeparam name="D">The Type of elements in the domain.</typeparam>
        ''' <typeparam name="C">The Type of elements in the codomain.</typeparam>
        ''' <param name="Domain">The topological space that is the domain of the function.</param>
        ''' <param name="Codomain">The topological space that is the codomain of the function.</param>
        ''' <param name="TestFunction">The function to test for continuity.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function IsContinuousFunction(Of D As {Class, New, IEquatable(Of D)}, C As {Class, New, IEquatable(Of C)})(ByRef Domain As FiniteTopologicalSpace(Of D), ByRef Codomain As FiniteTopologicalSpace(Of C), ByRef TestFunction As FiniteFunction(Of D, C)) As Boolean
            Dim InvImgSet As FiniteSet(Of D)
            Dim OpenSetIndex As Integer


            For OpenSetIndex = 0 To Codomain.theTopology.Cardinality - 1
                InvImgSet = TestFunction.InverseImageSet(Codomain.theTopology.Element(OpenSetIndex))
                If Domain.theTopology.Contains(InvImgSet) = False Then
                    Return False
                End If
            Next OpenSetIndex

            Return True
        End Function

        Public Shared Function IsHomeomorphism(Of D As {Class, New, IEquatable(Of D)}, C As {Class, New, IEquatable(Of C)})(ByRef Domain As FiniteTopologicalSpace(Of D), ByRef Codomain As FiniteTopologicalSpace(Of C), ByRef TestFunction As FiniteFunction(Of D, C)) As Boolean
            'Is the function bijective?
            If TestFunction.IsBijective = False Then
                Return False
            End If

            'Is the function continuous?
            If IsContinuousFunction(Of D, C)(Domain, Codomain, TestFunction) = False Then
                Return False
            End If

            'Is the inverse function continuous?
            Dim InvFunc As FiniteFunction(Of C, D)
            InvFunc = TestFunction.InverseFunction
            If IsContinuousFunction(Of C, D)(Codomain, Domain, InvFunc) = False Then
                Return False
            End If

            Return True
        End Function

#End Region

    End Class

End Namespace
