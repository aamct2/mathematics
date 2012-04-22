Namespace Geometry

    ''' <summary>
    ''' Represents a line.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Line
        Private myOrigin As PointF3
        Private direction As Vector3

#Region "  Constructors  "

        Public Sub New()
            Me.DirectionVector = New Vector3
        End Sub

        Public Sub New(ByVal newOrigin As PointF3, ByVal newDirection As Vector3)
            Me.Origin = newOrigin
            Me.DirectionVector = newDirection
        End Sub

#End Region

#Region "  Properties  "

        Public Property Origin() As PointF3
            Get
                Return Me.myOrigin
            End Get
            Set(ByVal value As PointF3)
                If Not IsNothing(value) Then
                    Me.myOrigin = value
                Else
                    Throw New ArgumentNullException("Origin cannot be null.")
                End If
            End Set
        End Property

        Public Property DirectionVector() As Vector3
            Get
                Return Me.direction
            End Get
            Set(ByVal value As Vector3)
                If Not IsNothing(value) Then
                    Me.direction = value
                Else
                    Throw New ArgumentNullException("DirectionVector cannot be null.")
                End If
            End Set
        End Property

#End Region

        ''' <summary>
        ''' Returns the closest point on the line to a given point.
        ''' </summary>
        ''' <param name="pt">The point from which to find the nearest point on the line.</param>
        ''' <returns>Returns the closest point on the line to a given point.</returns>
        ''' <remarks></remarks>
        Public Overridable Function ClosestPointOnLine(ByRef pt As PointF3) As PointF3
            Dim w As New Vector3(Me.Origin, pt)
            Dim proj As Double = w.DotProduct(Me.DirectionVector)

            Return Me.Origin + CSng((proj / Math.Pow(Me.DirectionVector.Length(), 2))) * Me.DirectionVector
        End Function

#Region "  Functions: Distance  "

        ''' <summary>
        ''' Determines the shortest distance from a given point to the line.
        ''' </summary>
        ''' <param name="pt">The point from which to find the distance.</param>
        ''' <returns>Returns the shortest distance from a given point to the line.</returns>
        ''' <remarks></remarks>
        Public Function Distance(ByRef pt As PointF3) As Double
            Return pt.Distance(Me.ClosestPointOnLine(pt))
        End Function

        ''' <summary>
        ''' Determines the shortest distance from another given line to this line.
        ''' </summary>
        ''' <param name="line2">The line from which to find the distance.</param>
        ''' <returns>Returns the shortest distance from another given line to this line.</returns>
        ''' <remarks></remarks>
        Public Function Distance(ByRef line2 As Line) As Double
            Dim w0 As New Vector3(Me.Origin, line2.Origin)
            Dim a As Double = Me.DirectionVector.DotProduct(Me.DirectionVector)
            Dim b As Double = Me.DirectionVector.DotProduct(line2.DirectionVector)
            Dim c As Double = line2.DirectionVector.DotProduct(line2.DirectionVector)
            Dim d As Double = Me.DirectionVector.DotProduct(w0)
            Dim e As Double = line2.DirectionVector.DotProduct(w0)

            Dim denom As Double = a * c - Math.Pow(b, 2)

            'Check if lines are parallel
            If denom = 0 Then
                Dim wc As Vector2 = w0 - (e / c) * line2.DirectionVector
                Return wc.DotProduct(wc)
            Else
                Dim wc As Vector2 = w0 + ((b * e - c * d) / denom) * Me.DirectionVector _
                                     - ((a * e - b * d) / denom) * line2.DirectionVector
                Return wc.DotProduct(wc)
            End If
        End Function

        Public Function Distance(ByRef circ As Circle) As Double
            'REDO: Assumes they are on same plane

            Return Me.Distance(circ.CenterPoint) - circ.Radius.Length()
        End Function

#End Region

#Region "  Functions: IntersectsWith  "

        Public Function IntersectsWith(ByRef line2 As Line) As Boolean
            If Me.Distance(line2) = 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function IntersectsWith(ByRef circ As Circle) As Boolean
            If Me.Distance(circ) <= 0 Then
                Return True
            Else
                Return False
            End If
        End Function

#End Region

    End Class

End Namespace
