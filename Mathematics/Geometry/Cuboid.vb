
Namespace Geometry

    ''' <summary>
    ''' Represents a cuboid (a three dimensional solid with 6 sides that are all rectangular).
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Cuboid
        Inherits Rectangle

        Private myEdge3 As Vector3

        Public Property Edge3() As Vector3
            Get
                Return Me.myEdge3
            End Get
            Set(ByVal value As Vector3)
                If Not IsNothing(value) Then
                    Me.myEdge3 = value
                Else
                    Throw New ArgumentNullException("Edge3 cannot be null.")
                End If
            End Set
        End Property

#Region "  Constructors  "

        Public Sub New()
            MyBase.New()
            Me.Edge3 = New Vector3
        End Sub

        Public Sub New(ByVal newOrigin As PointF3, ByVal newEdge1 As Vector3, _
                       ByVal newEdge2 As Vector3, ByVal newEdge3 As Vector3)
            MyBase.New(newOrigin, newEdge1, newEdge2)
            Me.Edge3 = newEdge3
        End Sub

#End Region

#Region "  Functions: distance  "

        Public Shadows Function Distance(ByRef pt As PointF3) As Double
            'TODO

            Return 0
        End Function

#End Region

        Public Function SurfaceArea() As Double
            Dim len1 As Double = Me.Edge1.Length()
            Dim len2 As Double = Me.Edge2.Length()
            Dim len3 As Double = Me.Edge3.Length()

            Return 2 * (len1 * len2 + len2 * len3 + len1 * len3)
        End Function

        Public Function Volume() As Double
            Return Me.Edge1.Length() * Me.Edge2.Length() * Me.Edge3.Length()
        End Function

    End Class

End Namespace
