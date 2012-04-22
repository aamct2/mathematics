
''' <summary>
''' A base class that defines a map.
''' </summary>
''' <typeparam name="T">The type used in the domain.</typeparam>
''' <typeparam name="G">The type used in the codomain.</typeparam>
''' <remarks></remarks>
Public MustInherit Class Map(Of T As {Class, New, IEquatable(Of T)}, G As {Class, New, IEquatable(Of G)})

    Public MustOverride Function ApplyMap(ByVal input As T) As G

End Class

''' <summary>
''' Defines a map by using a set of ordered pairs.
''' </summary>
''' <typeparam name="X">The Type of input values for the map.</typeparam>
''' <typeparam name="Y">The Type of output values for the map.</typeparam>
''' <remarks></remarks>
Public Class SetDefinedMap(Of X As {Class, New, IEquatable(Of X)}, Y As {Class, New, IEquatable(Of Y)})
    Inherits Map(Of X, Y)

    Private myRelation As FiniteSet(Of OrderedPair(Of X, Y))

    Public Sub New(ByRef newRelation As FiniteSet(Of OrderedPair(Of X, Y)))
        Me.myRelation = newRelation
    End Sub

    Private ReadOnly Property Relation As FiniteSet(Of OrderedPair(Of X, Y))
        Get
            Return Me.myRelation
        End Get
    End Property

    Public Overrides Function ApplyMap(ByVal input As X) As Y
        Dim index As Integer

        For index = 0 To Me.Relation.Cardinality - 1
            If Me.Relation.Element(index).Element(0).Equals(input) = True Then
                Return CType(Me.Relation.Element(index).Element(1), Y)
            End If
        Next index

        Throw New UndefiniedException("The input value is not a valid input for this map.")
    End Function
End Class

Public Class CompositionMap(Of T As {Class, New, IEquatable(Of T)}, intermediatary As {Class, New, IEquatable(Of intermediatary)}, G As {Class, New, IEquatable(Of G)})
    Inherits Map(Of T, G)

    Private myInnerMap As Map(Of T, intermediatary)
    Private myOuterMap As Map(Of intermediatary, G)

    Public Sub New(ByVal innerMap As Map(Of T, intermediatary), ByVal outerMap As Map(Of intermediatary, G))
        Me.myInnerMap = innerMap
        Me.myOuterMap = outerMap
    End Sub

    Public Overrides Function ApplyMap(ByVal input As T) As G
        Return Me.myOuterMap.ApplyMap(Me.myInnerMap.ApplyMap(input))
    End Function
End Class
