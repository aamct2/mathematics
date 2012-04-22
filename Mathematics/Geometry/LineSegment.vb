Namespace Geometry

    ''' <summary>
    ''' Represents a line segment.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class LineSegment
        Inherits Line

#Region "  Constructors  "

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal newOrigin As PointF3, ByVal newDirection As Vector3)
            MyBase.New(newOrigin, newDirection)
        End Sub

        Public Sub New(ByVal newOrigin As PointF3, ByVal newEndPoint As PointF3)
            MyBase.New(newOrigin, New Vector3(newOrigin, newEndPoint))
        End Sub

#End Region

#Region "  Properties  "

        Public ReadOnly Property EndPoint() As PointF3
            Get
                Return Me.Origin + Me.DirectionVector
            End Get
        End Property

#End Region

        Public Overrides Function ClosestPointOnLine(ByRef pt As PointF3) As PointF3
            Dim ptVectorRep As New Vector3(Me.Origin, pt)

            Dim proj As Double = ptVectorRep.DotProduct(Me.DirectionVector)

            If proj <= 0 Then
                Return Me.Origin
            Else
                If proj >= Math.Pow(Me.Length(), 2) Then
                    Return Me.EndPoint
                Else
                    Return Me.Origin + ((proj / Math.Pow(Me.Length(), 2)) * Me.DirectionVector)
                End If
            End If
        End Function

        Public Function Contains(ByVal pt As PointF3) As Boolean
            If Me.ClosestPointOnLine(pt) = pt Then
                Return True
            Else
                Return False
            End If
        End Function

#Region "  Functions: Distance  "

        Public Shadows Function Distance(ByRef pt As PointF3) As Double
            Return pt.Distance(Me.ClosestPointOnLine(pt))
        End Function

        Public Shadows Function Distance(ByRef lineSeg2 As LineSegment) As Double
            Dim w0 As New Vector3(Me.Origin, lineSeg2.Origin)
            Dim a As Double = Me.DirectionVector.DotProduct(Me.DirectionVector)
            Dim b As Double = Me.DirectionVector.DotProduct(lineSeg2.DirectionVector)
            Dim c As Double = lineSeg2.DirectionVector.DotProduct(lineSeg2.DirectionVector)
            Dim d As Double = Me.DirectionVector.DotProduct(w0)
            Dim e As Double = lineSeg2.DirectionVector.DotProduct(w0)
            Dim denom As Double = a * c - Math.Pow(b, 2)

            Dim sn As Double
            Dim sd As Double
            Dim tn As Double
            Dim td As Double
            Dim s_c As Double
            Dim t_c As Double

            'If denom is zero, try finding closest point on lineSeg2 to Me.origin
            If denom = 0 Then
                'clamp s_c to 0
                sd = c
                td = c
                sn = 0.0
                tn = e
            Else
                'clamp s_c within [0,1]
                sd = denom
                td = denom
                sn = b * e - c * d
                tn = a * e - b * d

                If (sn < 0.0) Then
                    'clamp s_c to 0
                    sn = 0.0
                    tn = e
                    td = c
                ElseIf (sn > sd) Then
                    'clamp s_c to 1
                    sn = sd
                    tn = e + b
                    td = c
                End If
            End If

            'clamp t_c within [0,1]
            'clamp t_c to 0
            If (tn < 0.0) Then
                t_c = 0.0
                'clamp s_c to 0
                If (-d < 0.0F) Then
                    s_c = 0.0
                    'clamp s_c to 1
                ElseIf (-d > a) Then
                    s_c = 1.0
                Else
                    s_c = -d / a
                End If
                'clamp t_c to 1
            ElseIf (tn > td) Then
                t_c = 1.0
                'clamp s_c to 0
                If ((-d + b) < 0.0F) Then
                    s_c = 0.0
                    'clamp s_c to 1
                ElseIf ((-d + b) > a) Then
                    s_c = 1.0
                Else
                    s_c = (-d + b) / a
                End If
            Else
                t_c = tn / td
                s_c = sn / sd
            End If

            'Compute difference vector and distance squared
            Dim wc As Vector3 = w0 + s_c * Me.DirectionVector - t_c * lineSeg2.DirectionVector
            Return wc.DotProduct(wc)
        End Function

        Public Shadows Function Distance(ByRef circ As Circle) As Double
            If Me.OnSamePlane(circ) Then
                Return Me.Distance(circ.CenterPoint) - circ.Radius.Length()
            Else
                'TODO
            End If

            Return 0
        End Function

#End Region

        Public Shadows Function Equals(ByRef line2 As LineSegment) As Boolean
            If ((Me.Origin = line2.Origin) And (Me.DirectionVector = line2.DirectionVector)) Or _
               ((Me.Origin = line2.EndPoint) And (Me.DirectionVector = -1 * line2.DirectionVector)) Then
                Return True
            Else
                Return False
            End If
        End Function

#Region "  Functions: IntersectsWith  "

        Public Shadows Function IntersectsWith(ByRef lineSeg2 As LineSegment) As Boolean
            If Me.Distance(lineSeg2) = 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Shadows Function IntersectsWith(ByRef circ As Circle) As Boolean
            If Me.Distance(circ) <= 0 Then
                Return True
            Else
                Return False
            End If
        End Function

#End Region

        Public Function OnSamePlane(ByRef circ As Circle) As Boolean
            Dim plane As New Plane(circ.CenterPoint, circ.Radius, circ.Radius.CrossProduct(circ.NormalVector))

            If plane.Contains(Me) Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function Length() As Double
            Return Me.DirectionVector.Length()
        End Function

#Region "  Operators  "

        Public Shared Operator =(ByVal lhs As LineSegment, ByVal rhs As LineSegment) As Boolean
            Return lhs.Equals(rhs)
        End Operator

        Public Shared Operator <>(ByVal lhs As LineSegment, ByVal rhs As LineSegment) As Boolean
            Return Not lhs.Equals(rhs)
        End Operator

#End Region

    End Class

End Namespace