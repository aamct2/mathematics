
Imports System.Collections

Public Class OrderedPair(Of XType As {Class, New, IEquatable(Of XType)}, YType As {Class, New, IEquatable(Of YType)})
    Inherits Tuple
    Implements IEquatable(Of OrderedPair(Of XType, YType))

#Region "  Constructors  "

    Public Sub New()
        MyBase.New()

        Me.Element(0) = New XType
        Me.Element(1) = New YType
    End Sub

    Public Sub New(ByRef newX As XType, ByRef newY As YType)
        MyBase.New()

        Me.Element(0) = newX
        Me.Element(1) = newY
    End Sub

#End Region

#Region "  Properties  "

    Public Property x() As XType
        Get
            Return CType(Me.Element(0), XType)
        End Get
        Set(ByVal value As XType)
            Me.Element(0) = value
        End Set
    End Property

    Public Property y() As YType
        Get
            Return CType(Me.Element(1), YType)
        End Get
        Set(ByVal value As YType)
            Me.Element(1) = value
        End Set
    End Property

#End Region

    Public Shadows Function Equals(ByVal other As OrderedPair(Of XType, YType)) As Boolean Implements System.IEquatable(Of OrderedPair(Of XType, YType)).Equals
        If Me.x.Equals(other.x) = True And Me.y.Equals(other.y) = True Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
