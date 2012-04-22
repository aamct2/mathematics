Namespace Geometry

    ''' <summary>
    ''' Represents a double-precision 3-dimensional point.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PointF3
        Inherits PointF2

        Private myZ As Double

#Region "  Constructors  "

        ''' <summary>
        ''' Initializes a new instance of the PointF3 class to the point (0, 0, 0).
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            MyBase.New()
            Me.Z = 0
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the PointF3 class with the specified coordinates.
        ''' </summary>
        ''' <param name="newX"></param>
        ''' <param name="newY"></param>
        ''' <param name="newZ"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal newX As Double, ByVal newY As Double, ByVal newZ As Double)
            MyBase.New(newX, newY)

            Me.Z = newZ
        End Sub
#End Region

        ''' <summary>
        ''' Gets or sets the z-coordinate of this PointF3.
        ''' </summary>
        ''' <value>Value</value>
        ''' <returns>Returns</returns>
        ''' <remarks></remarks>
        Public Property Z() As Double
            Get
                Return Me.myZ
            End Get
            Set(ByVal value As Double)
                Me.myZ = value
            End Set
        End Property

        ''' <summary>
        ''' Determines the distance between this PointF3 and another.
        ''' </summary>
        ''' <param name="pt2"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shadows Function Distance(ByRef pt2 As PointF3) As Double
            Return Math.Sqrt(Math.Pow(Me.X - pt2.X, 2) + Math.Pow(Me.Y - pt2.Y, 2) + Math.Pow(Me.Z - pt2.Z, 2))
        End Function

        ''' <summary>
        ''' Determines whether the this PointF3 and another are equal.
        ''' </summary>
        ''' <param name="pt2"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shadows Function Equals(ByRef pt2 As PointF3) As Boolean
            If (Me.X = pt2.X) And (Me.Y = pt2.Y) And (Me.Z = pt2.Z) Then
                Return True
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' Converts this PointF3 to a human readable string.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overrides Function ToString() As String
            Return "(" & Me.X & "," & Me.Y & "," & Me.Z & ")"
        End Function

#Region "  Operators  "

        ''' <summary>
        ''' Indicates whether two PointF3 objects are equal.
        ''' </summary>
        ''' <param name="lhs"></param>
        ''' <param name="rhs"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Shadows Operator =(ByVal lhs As PointF3, ByVal rhs As PointF3) As Boolean
            Return lhs.Equals(rhs)
        End Operator

        ''' <summary>
        ''' Indicates whether two PointF3 objects are not equal.
        ''' </summary>
        ''' <param name="lhs"></param>
        ''' <param name="rhs"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Shadows Operator <>(ByVal lhs As PointF3, ByVal rhs As PointF3) As Boolean
            Return Not lhs.Equals(rhs)
        End Operator

        ''' <summary>
        ''' Translates a PointF3 by the specified vector.
        ''' </summary>
        ''' <param name="lhs"></param>
        ''' <param name="rhs"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Shadows Operator +(ByVal lhs As PointF3, ByVal rhs As Vector3) As PointF3
            Return New PointF3(lhs.X + rhs.I, lhs.Y + rhs.J, lhs.Z + rhs.K)
        End Operator

        Public Shared Shadows Operator -(ByVal pt As PointF3, ByVal vect As Vector3) As PointF3
            Return pt + (-1 * vect)
        End Operator

        Public Shared Shadows Widening Operator CType(ByVal pt As System.Drawing.PointF) As PointF3
            Return New PointF3(pt.X, pt.Y, 0)
        End Operator

        Public Shared Shadows Narrowing Operator CType(ByVal pt As PointF3) As System.Drawing.PointF
            Return New System.Drawing.PointF(CSng(pt.X), CSng(pt.Y))
        End Operator

        Public Shared Shadows Widening Operator CType(ByVal pt As System.Drawing.Point) As PointF3
            Return New PointF3(pt.X, pt.Y, 0)
        End Operator

        Public Shared Shadows Narrowing Operator CType(ByVal pt As PointF3) As System.Drawing.Point
            Return New System.Drawing.Point(CInt(pt.X), CInt(pt.Y))
        End Operator
#End Region

    End Class

End Namespace
