
Namespace Geometry

    ''' <summary>
    ''' Represents a sphere.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Sphere
        Inherits Circle

#Region "  Constructors  "

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal newCenter As PointF3, ByVal newRadius As Vector3)
            Me.CenterPoint = newCenter
            Me.Radius = newRadius
            Me.NormalVector = newRadius.CrossProduct(-1 * newRadius)
        End Sub

        Public Sub New(ByVal maximalCrossection As Circle)
            MyBase.New(maximalCrossection.CenterPoint, maximalCrossection.Radius, maximalCrossection.NormalVector)
        End Sub

#End Region

#Region "  Functions: Distance  "

        Public Shadows Function Distance(ByRef sphere2 As Sphere) As Double
            Dim dist As Double = Me.CenterPoint.Distance(sphere2.CenterPoint)

            Return dist - Me.Radius.Length - sphere2.Radius.Length
        End Function

        Public Shadows Function Distance(ByVal plane2 As Plane) As Double
            Dim dist As Double = Me.CenterPoint.Distance(plane2.ClosestPointOnPlane(Me.CenterPoint))

            Return dist - Me.Radius.Length()
        End Function

#End Region

#Region "  Functions: IntersectsWith  "

        Public Shadows Function IntersectsWith(ByRef sphere2 As Sphere) As Boolean
            If Me.Distance(sphere2) < 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Shadows Function IntersectsWith(ByVal plane2 As Plane) As Boolean
            If Me.Distance(plane2) < 0 Then
                Return True
            Else
                Return False
            End If
        End Function

#End Region

        ''' <summary>
        ''' Returns a maximal crossection of the sphere that is parallel to the xy-plane.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function MaximalCrossection() As Circle
            Return New Circle(Me.CenterPoint, _
                    New Vector3(Me.CenterPoint, New PointF3(Me.CenterPoint.X, Me.CenterPoint.Y + Me.Radius.Length(), Me.CenterPoint.Z)), _
                    New Vector3(0, 0, 1))
        End Function

        Public Function SurfaceArea() As Double
            Return 4 * Math.PI * Math.Pow(Me.Radius.Length(), 2)
        End Function

        Public Function Volume() As Double
            Return (4 / 3) * Math.PI * Math.Pow(Me.Radius.Length(), 3)
        End Function
    End Class

End Namespace
