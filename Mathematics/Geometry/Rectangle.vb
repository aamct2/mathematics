Namespace Geometry

    ''' <summary>
    ''' Represents a rectangle.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Rectangle
        Private myOrigin As PointF3
        Private myEdge1 As Vector3
        Private myEdge2 As Vector3

#Region "  Constructors  "

        Public Sub New()
            Me.Origin = New PointF3
            Me.Edge1 = New Vector3
            Me.Edge2 = New Vector3
        End Sub

        Public Sub New(ByVal newOrigin As PointF3, ByVal newEdge1 As Vector3, ByVal newEdge2 As Vector3)
            Me.Origin = newOrigin
            Me.Edge1 = newEdge1
            Me.Edge2 = newEdge2
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

        Public Property Edge1() As Vector3
            Get
                Return Me.myEdge1
            End Get
            Set(ByVal value As Vector3)
                If Not IsNothing(value) Then
                    Me.myEdge1 = value
                Else
                    Throw New ArgumentNullException("Edge1 cannot be null.")
                End If
            End Set
        End Property

        Public Property Edge2() As Vector3
            Get
                Return Me.myEdge2
            End Get
            Set(ByVal value As Vector3)
                If Not IsNothing(value) Then
                    Me.myEdge2 = value
                Else
                    Throw New ArgumentNullException("Edge2 cannot be null.")
                End If
            End Set
        End Property

        Public ReadOnly Property Side1() As LineSegment
            Get
                Return New LineSegment(Me.Origin, Me.Edge1)
            End Get
        End Property

        Public ReadOnly Property Side2() As LineSegment
            Get
                Return New LineSegment(Me.Origin, Me.Edge2)
            End Get
        End Property

        Public ReadOnly Property Side3() As LineSegment
            Get
                Return New LineSegment(Me.Origin + Me.Edge2, Me.Edge1)
            End Get
        End Property

        Public ReadOnly Property Side4() As LineSegment
            Get
                Return New LineSegment(Me.Origin + Me.Edge1, Me.Edge2)
            End Get
        End Property

        Public ReadOnly Property NormalVector() As Vector3
            Get
                Return Me.Edge1.CrossProduct(Me.Edge2)
            End Get
        End Property

        Public ReadOnly Property CenterPoint() As PointF3
            Get
                Return Me.Origin + (0.5 * Me.Edge1) + (0.5 * Me.Edge2)
            End Get
        End Property

#End Region

#Region "  Methods  "

        Public Function Area() As Double
            Return (Me.Edge1.Length() * Me.Edge2.Length())
        End Function

        Public Function ClosestPointOnRectangle(ByRef pt As PointF3) As PointF3
            Dim rectPlane As New Plane(Me.Origin, Me.Edge1, Me.Edge2)

            Dim planeClosest As PointF3 = rectPlane.ClosestPointOnPlane(pt)

            If Me.Contains(planeClosest) Then
                Return planeClosest
            Else
                Dim distances(3) As Double
                Dim distanceBetweenSides(1) As Double
                Dim lines(3) As Line

                lines(0) = Me.Side1
                lines(1) = Me.Side2
                lines(2) = Me.Side3
                lines(3) = Me.Side4

                distances(0) = lines(0).Distance(planeClosest)
                distances(1) = lines(1).Distance(planeClosest)
                distances(2) = lines(2).Distance(planeClosest)
                distances(3) = lines(3).Distance(planeClosest)

                distanceBetweenSides(0) = Me.Side1.Distance(Me.Side3)
                distanceBetweenSides(1) = Me.Side2.Distance(Me.Side4)

                If distances(0) > distanceBetweenSides(0) Or _
                    distances(2) > distanceBetweenSides(0) Then

                    If distances(0) > distances(2) Then
                        'Clamp to Side3
                        planeClosest = Me.Side3.ClosestPointOnLine(planeClosest)
                    Else
                        'Clamp to Side1
                        planeClosest = Me.Side1.ClosestPointOnLine(planeClosest)
                    End If
                End If

                If distances(1) > distanceBetweenSides(1) Or _
                    distances(3) > distanceBetweenSides(1) Then

                    If distances(1) > distances(3) Then
                        'Clamp to Side4
                        planeClosest = Me.Side4.ClosestPointOnLine(planeClosest)
                    Else
                        'Clamp to Side2
                        planeClosest = Me.Side2.ClosestPointOnLine(planeClosest)
                    End If
                End If

                Return planeClosest
            End If
        End Function

#Region "  Functions: Contains  "

        Public Function Contains(ByRef pt As PointF3) As Boolean
            Dim distances(3) As Double
            Dim distanceBetweenSides(1) As Double

            distances(0) = Me.Side1.Distance(pt)
            distances(1) = Me.Side2.Distance(pt)
            distances(2) = Me.Side3.Distance(pt)
            distances(3) = Me.Side4.Distance(pt)

            distanceBetweenSides(0) = Me.Side1.Distance(Me.Side3)
            distanceBetweenSides(1) = Me.Side2.Distance(Me.Side4)

            'Check to see if the point is contained within the rectangle
            If distances(0) <= distanceBetweenSides(0) And distances(3) <= distanceBetweenSides(0) And _
               distances(2) <= distanceBetweenSides(1) And distances(4) <= distanceBetweenSides(1) And _
               Me.OnSamePlane(pt) = True Then

                Return True
            Else
                Return False
            End If
        End Function

#End Region

#Region "  Functions: Distance  "

        ''' <summary>
        ''' Returns the distance between a point and this rectangle.
        ''' </summary>
        ''' <param name="pt"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Distnace(ByRef pt As PointF3) As Double
            If Me.Contains(pt) Then
                If Me.Side1.Contains(pt) Or Me.Side2.Contains(pt) Or _
                   Me.Side3.Contains(pt) Or Me.Side4.Contains(pt) Then
                    Return 0.0
                Else
                    'TODO: Finish
                End If
            Else
                Return Me.ClosestPointOnRectangle(pt).Distance(pt)
            End If

            Return 0
        End Function

        Public Function Distance(ByRef circ As Circle) As Double
            If Me.Edge1.CrossProduct(Me.Edge2).Parallel(circ.NormalVector) Then
                'They are on the same plane
                Dim centerLine As New LineSegment(Me.CenterPoint, circ.CenterPoint)
                Dim endPt As PointF3

                If centerLine.IntersectsWith(Me.Side1) Then
                    endPt = Me.Side1.ClosestPointOnLine(Me.CenterPoint)
                ElseIf centerLine.IntersectsWith(Me.Side2) Then
                    endPt = Me.Side2.ClosestPointOnLine(Me.CenterPoint)
                ElseIf centerLine.IntersectsWith(Me.Side3) Then
                    endPt = Me.Side3.ClosestPointOnLine(Me.CenterPoint)
                Else
                    endPt = Me.Side4.ClosestPointOnLine(Me.CenterPoint)
                End If

                Return Me.CenterPoint.Distance(circ.CenterPoint) - endPt.Distance(Me.CenterPoint) - circ.Radius.Length()
            Else
                'TODO: Finish
            End If

            Return 0
        End Function

#End Region

#Region "  Functions: IntersectsWith  "

        Public Function IntersectsWith(ByRef rect2 As Rectangle) As Boolean
            'TODO: FIX

            Dim diagonalVect As Vector3 = Me.Edge1 + Me.Edge2
            Dim diagonalLineSeg As New LineSegment(Me.Origin, diagonalVect)

            With rect2
                If diagonalLineSeg.IntersectsWith(rect2.Side1) Or _
                   diagonalLineSeg.IntersectsWith(rect2.Side2) Or _
                   diagonalLineSeg.IntersectsWith(rect2.Side3) Or _
                   diagonalLineSeg.IntersectsWith(rect2.Side4) Then
                    Return True
                Else
                    Return False
                End If
            End With
        End Function

        Public Function IntersectsWith(ByRef circ As Circle) As Boolean
            If Me.Distance(circ) <= 0 Then
                Return True
            Else
                Return False
            End If
        End Function

#End Region

#Region "  Functions: OnSamePlane  "

        Public Function OnSamePlane(ByRef pt As PointF3) As Boolean
            Dim plane As New Plane(Me.Origin, Me.Edge1, Me.Edge2)

            If plane.Contains(pt) Then
                Return True
            Else
                Return False
            End If
        End Function

#End Region

        Public Function Parallel(ByRef rect2 As Rectangle) As Boolean
            If Me.NormalVector.Parallel(rect2.NormalVector) Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function Perimeter() As Double
            Return 2 * Me.Edge1.Length() + 2 * Me.Edge2.Length()
        End Function

#End Region

#Region "  Operators  "

        Public Shared Widening Operator CType(ByVal rect As System.Drawing.RectangleF) As Rectangle
            Return New Rectangle(CType(rect.Location, PointF3), New Vector3(CType(rect.Location, PointF3), New PointF3(rect.Location.X + rect.Width, rect.Location.Y, 0)), _
                        New Vector3(CType(rect.Location, PointF3), New PointF3(rect.Location.X, rect.Location.Y + rect.Height, 0)))
        End Operator

        Public Shared Narrowing Operator CType(ByVal rect As Rectangle) As System.Drawing.RectangleF
            Return New System.Drawing.RectangleF(CType(rect.Origin, System.Drawing.PointF), _
                        New System.Drawing.SizeF(CSng(rect.Edge1.Length()), CSng(rect.Edge2.Length())))
        End Operator

        Public Shared Widening Operator CType(ByVal rect As System.Drawing.Rectangle) As Rectangle
            Return New Rectangle(CType(rect.Location, PointF3), New Vector3(CType(rect.Location, PointF3), New PointF3(rect.Location.X + rect.Width, rect.Location.Y, 0)), _
                        New Vector3(CType(rect.Location, PointF3), New PointF3(rect.Location.X, rect.Location.Y + rect.Height, 0)))
        End Operator

        Public Shared Narrowing Operator CType(ByVal rect As Rectangle) As System.Drawing.Rectangle
            Return New System.Drawing.Rectangle(CType(rect.Origin, System.Drawing.Point), _
                        New System.Drawing.Size(CInt(rect.Edge1.Length()), CInt(rect.Edge2.Length())))
        End Operator

#End Region

    End Class

End Namespace
