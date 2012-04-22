
Namespace Geometry

    ''' <summary>
    ''' Represents a double-precision 2-dimensional point.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PointF2
        Private myX As Double
        Private myY As Double

#Region "  Constructors  "
        ''' <summary>
        ''' Initializes a new instance of the PointF2 class to the point (0, 0).
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            Me.X = 0
            Me.Y = 0
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the PointF2 class with the specified coordinates.
        ''' </summary>
        ''' <param name="newX"></param>
        ''' <param name="newY"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal newX As Double, ByVal newY As Double)
            Me.X = newX
            Me.Y = newY
        End Sub
#End Region

#Region "  Properties  "

        ''' <summary>
        ''' Gets or sets the x-coordinate of this PointF2.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property X() As Double
            Get
                Return Me.myX
            End Get
            Set(ByVal value As Double)
                Me.myX = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the y-coordinate of this PointF2.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Y() As Double
            Get
                Return Me.myY
            End Get
            Set(ByVal value As Double)
                Me.myY = value
            End Set
        End Property

#End Region

        ''' <summary>
        ''' Determines the distance between this PointF2 and another.
        ''' </summary>
        ''' <param name="pt2"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Distance(ByRef pt2 As PointF2) As Double
            Return Math.Sqrt(Math.Pow(Me.X - pt2.X, 2) + Math.Pow(Me.Y - pt2.Y, 2))
        End Function

        ''' <summary>
        ''' Determines whether the this PointF2 and another are equal.
        ''' </summary>
        ''' <param name="pt2"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shadows Function Equals(ByRef pt2 As PointF2) As Boolean
            If (Me.X = pt2.X) And (Me.Y = pt2.Y) Then
                Return True
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' Converts this PointF2 to a human readable string.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overrides Function ToString() As String
            Return "(" & Me.X & "," & Me.Y & ")"
        End Function

#Region "  Operators  "

        ''' <summary>
        ''' Indicates whether two PointF2 objects are equal.
        ''' </summary>
        ''' <param name="lhs"></param>
        ''' <param name="rhs"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Operator =(ByVal lhs As PointF2, ByVal rhs As PointF2) As Boolean
            Return lhs.Equals(rhs)
        End Operator

        ''' <summary>
        ''' Indicates whether two PointF2 objects are not equal.
        ''' </summary>
        ''' <param name="lhs"></param>
        ''' <param name="rhs"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Operator <>(ByVal lhs As PointF2, ByVal rhs As PointF2) As Boolean
            Return Not lhs.Equals(rhs)
        End Operator

        ''' <summary>
        ''' Converts this PointF2 to a human readable string.
        ''' </summary>
        ''' <param name="pt"></param>
        ''' <param name="vect"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Operator +(ByVal pt As PointF2, ByVal vect As Vector2) As PointF2
            Return New PointF2(pt.X + vect.I, pt.Y + vect.J)
        End Operator

        Public Shared Operator -(ByVal pt As PointF2, ByVal vect As Vector2) As PointF2
            Return pt + (-1 * vect)
        End Operator

        Public Shared Widening Operator CType(ByVal pt As System.Drawing.PointF) As PointF2
            Return New PointF2(pt.X, pt.Y)
        End Operator

        Public Shared Narrowing Operator CType(ByVal pt As PointF2) As System.Drawing.PointF
            Return New System.Drawing.PointF(CSng(pt.X), CSng(pt.Y))
        End Operator

        Public Shared Widening Operator CType(ByVal pt As System.Drawing.Point) As PointF2
            Return New PointF2(pt.X, pt.Y)
        End Operator

        Public Shared Narrowing Operator CType(ByVal pt As PointF2) As System.Drawing.Point
            Return New System.Drawing.Point(CInt(pt.X), CInt(pt.Y))
        End Operator
#End Region

    End Class

End Namespace
