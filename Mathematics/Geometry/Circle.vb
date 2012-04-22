
Namespace Geometry

    ''' <summary>
    ''' Represents a circle.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Circle
        Private myCenterPoint As PointF3
        Private myRadius As Vector3
        Private myNormalVector As Vector3

#Region "  Constructors  "

        Public Sub New()
            Me.CenterPoint = New PointF3
            Me.Radius = New Vector3
            Me.NormalVector = New Vector3
        End Sub

        Public Sub New(ByVal newCenter As PointF3, ByVal newRadius As Vector3, ByVal newNormalVector As Vector3)
            Me.CenterPoint = newCenter
            Me.Radius = newRadius
            Me.NormalVector = newNormalVector
        End Sub

#End Region

#Region "  Properties  "

        Public Property CenterPoint() As PointF3
            Get
                Return Me.myCenterPoint
            End Get
            Set(ByVal value As PointF3)
                If Not IsNothing(value) Then
                    Me.myCenterPoint = value
                Else
                    Throw New ArgumentNullException("CenterPoint cannot be null.")
                End If
            End Set
        End Property

        Public Property Radius() As Vector3
            Get
                Return Me.myRadius
            End Get
            Set(ByVal value As Vector3)
                If Not IsNothing(value) Then
                    Me.myRadius = value
                Else
                    Throw New ArgumentNullException("Radius cannot be null.")
                End If
            End Set
        End Property

        Public Property NormalVector() As Vector3
            Get
                Return Me.myNormalVector
            End Get
            Set(ByVal value As Vector3)
                If Not IsNothing(value) Then
                    Me.myNormalVector = value
                Else
                    Throw New ArgumentNullException("NormalVector cannot be null.")
                End If
            End Set
        End Property

#End Region

        ''' <summary>
        ''' Determines the area of the circle.
        ''' </summary>
        ''' <returns>Returns the area of the circle.</returns>
        ''' <remarks></remarks>
        Public Function Area() As Double
            Return Math.PI * Math.Pow(Me.Radius.Length(), 2)
        End Function

        ''' <summary>
        ''' Determines the circumference of the circle.
        ''' </summary>
        ''' <returns>Returns the circumference of the circle.</returns>
        ''' <remarks></remarks>
        Public Function Circumference() As Double
            Return 2 * Math.PI * Me.Radius.Length()
        End Function

        ''' <summary>
        ''' Determines the closest point on the surface of the circle to a given point.
        ''' </summary>
        ''' <param name="pt">The point from which to find the closest point.</param>
        ''' <returns>Returnss the closest point on the surface of the circle to a given point.</returns>
        ''' <remarks></remarks>
        Public Function ClosestPointOnCircle(ByRef pt As PointF3) As PointF3
            Dim circPlane As New Plane(Me.CenterPoint, Me.Radius, Me.Radius.CrossProduct(Me.NormalVector))
            Dim closestPointOnPlane As PointF3 = circPlane.ClosestPointOnPlane(pt)

            If Me.Contains(closestPointOnPlane) Then
                'The circle contains closestPointOnPlane, so return that closestPointOnPlane
                Return closestPointOnPlane
            Else
                'The circle does not contain closestPointOnPlane, so the nearest point is on the boundary of the circle
                'Return the appropriate point
                Dim vect As New Vector3(Me.CenterPoint, closestPointOnPlane, Me.Radius.Length())

                Return Me.CenterPoint + vect
            End If
        End Function

        ''' <summary>
        ''' Determines whether the circle contains a given point.
        ''' </summary>
        ''' <param name="pt">The point being tested.</param>
        ''' <returns>Returns whether or not the circle contains a given point.</returns>
        ''' <remarks></remarks>
        Public Function Contains(ByVal pt As PointF3) As Boolean
            If Me.OnSamePlane(pt) = True And Me.CenterPoint.Distance(pt) - Me.Radius.Length() <= 0 Then
                Return True
            Else
                Return False
            End If
        End Function

#Region "  Functions: Distance  "

        ''' <summary>
        ''' Determines the distance from the circle to a given point.
        ''' </summary>
        ''' <param name="pt">The point from which to measure the distance.</param>
        ''' <returns>Returns the distance from the circle to a given point.</returns>
        ''' <remarks></remarks>
        Public Function Distance(ByRef pt As PointF3) As Double
            Dim closestPoint As PointF3 = Me.ClosestPointOnCircle(pt)

            Return closestPoint.Distance(pt)
        End Function

        ''' <summary>
        ''' Determines the distance from the nearest point on the surface of the circle to the nearest point on a given line.
        ''' </summary>
        ''' <param name="line2">The line from which to find the distance.</param>
        ''' <returns>Returns the distance from the nearest point on the surface of the circle to the nearest point on a given line.</returns>
        ''' <remarks></remarks>
        Public Function Distance(ByVal line2 As Line) As Double
            Dim dist As Double
            Dim closestPt As PointF3
            Dim proj As Double
            Dim radiusVect As Vector3
            Dim circPlane As New Plane(Me.CenterPoint, Me.Radius, Me.Radius.CrossProduct(Me.NormalVector))
            Dim closestVect As Vector3

            closestPt = line2.ClosestPointOnLine(Me.CenterPoint)

            If Me.OnSamePlane(line2) = True Then
                dist = Me.CenterPoint.Distance(closestPt) - Me.Radius.Length()

                If dist < 0 Then
                    'The line is on the same plane and intersects the circle
                    Return 0
                Else
                    'The line is on the same plane and does not intersect the circle
                    Return dist
                End If
            Else
                'The line is not on the same plane
                closestVect = New Vector3(Me.CenterPoint, closestPt)
                radiusVect = New Vector3(Me.CenterPoint, circPlane.ClosestPointOnPlane(closestPt), Me.Radius.Length())
                proj = radiusVect.DotProduct(closestVect) / closestVect.Length()

                Return Me.CenterPoint.Distance(closestPt) - proj
            End If
        End Function

        Public Function Distance(ByVal circ2 As Circle) As Double
            'Note: Returns a negative value if the circles intersect

            If Me.Parallel(circ2) And Me.OnSamePlane(circ2.CenterPoint) Then
                'They are on the same plane
                Return Me.CenterPoint.Distance(circ2.CenterPoint) - Me.Radius.Length() - circ2.Radius.Length()
            Else
                'TODO: Finish
            End If

            Return 0
        End Function

#End Region

#Region "  Functions: IntersectsWith  "

        Public Function IntersectsWith(ByVal line2 As Line) As Boolean
            If Me.Distance(line2) = 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function IntersectsWith(ByVal circ2 As Circle) As Boolean
            'TODO REDO
            If Me.Distance(circ2) <= 0 Then
                Return True
            Else
                Return False
            End If
        End Function

#End Region

#Region "  Functions: OnSamePlane  "

        Public Function OnSamePlane(ByRef pt As PointF3) As Boolean
            Dim plane2 As New Plane(Me.CenterPoint, Me.Radius, Me.Radius.CrossProduct(Me.NormalVector))

            If plane2.Contains(pt) Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function OnSamePlane(ByRef line2 As Line) As Boolean
            Dim plane2 As New Plane(Me.CenterPoint, Me.Radius, Me.Radius.CrossProduct(Me.NormalVector))

            Return plane2.Contains(line2)
        End Function

#End Region

        Public Function Parallel(ByVal circ2 As Circle) As Boolean
            If Me.NormalVector.Parallel(circ2.NormalVector) Then
                Return True
            Else
                Return False
            End If
        End Function

    End Class

End Namespace
