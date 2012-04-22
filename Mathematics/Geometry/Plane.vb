Namespace Geometry

    ''' <summary>
    ''' Represents a plane.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Plane

        Private myOrigin As PointF3
        Private myDirection1 As Vector3
        Private myDirection2 As Vector3

#Region "  Constructors  "

        Public Sub New()
            Me.Origin = New PointF3
            Me.DirectionVector1 = New Vector3
            Me.DirectionVector2 = New Vector3
        End Sub

        Public Sub New(ByVal newOrigin As PointF3, ByVal newDirection1 As Vector3, ByVal newDirection2 As Vector3)
            Me.Origin = newOrigin
            Me.DirectionVector1 = newDirection1
            Me.DirectionVector2 = newDirection2
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

        Public Property DirectionVector1() As Vector3
            Get
                Return Me.myDirection1
            End Get
            Set(ByVal value As Vector3)
                If Not IsNothing(value) Then
                    Me.myDirection1 = value
                Else
                    Throw New ArgumentNullException("DirectionVector1 cannot be null.")
                End If
            End Set
        End Property

        Public Property DirectionVector2() As Vector3
            Get
                Return Me.myDirection2
            End Get
            Set(ByVal value As Vector3)
                If Not IsNothing(value) Then
                    Me.myDirection2 = value
                Else
                    Throw New ArgumentNullException("DirectionVector2 cannot be null.")
                End If
            End Set
        End Property

        Public ReadOnly Property NormalVector() As Vector3
            Get
                Return Me.DirectionVector1.CrossProduct(Me.DirectionVector2)
            End Get
        End Property

#End Region

#Region "  Functions: Contains  "

        Public Function Contains(ByRef pt As PointF3) As Boolean
            If Me.Distance(pt) = 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function Contains(ByRef lineSeg As LineSegment) As Boolean
            If Me.Distance(lineSeg.Origin) = 0 And Me.Distance(lineSeg.EndPoint) = 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function Contains(ByRef line As Line) As Boolean
            Dim lineNormalVect As Vector3

            lineNormalVect = line.DirectionVector.CrossProduct(Me.DirectionVector1)

            If Me.Contains(line.Origin) = True And lineNormalVect.Parallel(Me.NormalVector) = True Then
                Return True
            Else
                Return False
            End If
        End Function

#End Region

        Public Function ClosestPointOnPlane(ByVal pt As PointF3) As PointF3
            Dim w As New Vector3(Me.Origin, pt)

            Return pt - ((Me.NormalVector.DotProduct(w) / Me.NormalVector.Length()) * Me.NormalVector.NormalizedVector())

            Return New PointF3
        End Function

#Region "  Functions: Distance  "

        Public Function Distance(ByRef pt As PointF3) As Double
            Dim w As New Vector3(Me.Origin, pt)

            Return Math.Abs(Me.NormalVector.DotProduct(w)) / Me.NormalVector.Length()
        End Function

#End Region

        Public Shadows Function Equals(ByRef plane2 As Plane) As Boolean
            If Me.Parallel(plane2) And Me.Contains(plane2.Origin) Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function Parallel(ByRef plane2 As Plane) As Boolean
            If Me.NormalVector.Parallel(plane2.NormalVector) Then
                Return True
            Else
                Return False
            End If
        End Function

#Region "  Operators  "

        Public Shared Operator =(ByVal lhs As Plane, ByVal rhs As Plane) As Boolean
            Return lhs.Equals(rhs)
        End Operator

        Public Shared Operator <>(ByVal lhs As Plane, ByVal rhs As Plane) As Boolean
            Return Not lhs.Equals(rhs)
        End Operator

#End Region

    End Class

End Namespace
