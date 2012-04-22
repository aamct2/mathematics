Namespace Geometry

    ''' <summary>
    ''' Represents a 2-dimensional vector.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Vector2
        'The standard i,j components of the vector
        Private myI As Double
        Private myJ As Double

#Region "  Constructors  "

        ''' <summary>
        ''' Initializes a new instance of the Vector2 class to the vector (0,0).
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            Me.I = 0
            Me.J = 0
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the Vector2 class with the specified components.
        ''' </summary>
        ''' <param name="newI"></param>
        ''' <param name="newJ"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal newI As Double, ByVal newJ As Double)
            Me.I = newI
            Me.J = newJ
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the Vector2 class using the specified start and end points.
        ''' </summary>
        ''' <param name="startPt"></param>
        ''' <param name="endPt"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal startPt As PointF2, ByVal endPt As PointF2)
            Me.I = endPt.X - startPt.X
            Me.J = endPt.Y - startPt.Y
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the Vector2 class using the specified start and direction points with the specified magnitude.
        ''' </summary>
        ''' <param name="startPt"></param>
        ''' <param name="directionPt"></param>
        ''' <param name="magnitude"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal startPt As PointF2, ByVal directionPt As PointF2, ByVal magnitude As Double)
            Dim directionVector As New Vector2(startPt, directionPt)
            Dim normalVector As Vector2
            normalVector = directionVector.NormalizedVector()
            normalVector = magnitude * normalVector

            Me.I = normalVector.I
            Me.J = normalVector.J
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the Vector2 class in the same direction as the specified vector with the specified magnitude.
        ''' </summary>
        ''' <param name="directionVect"></param>
        ''' <param name="magnitude"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal directionVect As Vector2, ByVal magnitude As Double)
            Dim normalVector As Vector2
            normalVector = directionVect.NormalizedVector()
            normalVector = magnitude * normalVector

            Me.I = normalVector.I
            Me.J = normalVector.J
        End Sub

#End Region

#Region "  Properties  "

        ''' <summary>
        ''' Gets or sets the I-component of this Vector2.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property I() As Double
            Get
                Return Me.myI
            End Get
            Set(ByVal value As Double)
                Me.myI = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the x-coordinate of this Vector2.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property J() As Double
            Get
                Return Me.myJ
            End Get
            Set(ByVal value As Double)
                Me.myJ = value
            End Set
        End Property

        ''' <summary>
        ''' Gets the 2-dimensional zero-vector.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared ReadOnly Property ZeroVector() As Vector2
            Get
                Return New Vector2(0, 0)
            End Get
        End Property

#End Region

#Region "  Methods  "

        ''' <summary>
        ''' Adds this Vector2 and another.
        ''' </summary>
        ''' <param name="vect2"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Add(ByRef vect2 As Vector2) As Vector2
            Return New Vector2(Me.I + vect2.I, Me.J + vect2.J)
        End Function

        ''' <summary>
        ''' Takes the cross-product of this Vector2 and another.
        ''' </summary>
        ''' <param name="vect2"></param>
        ''' <returns></returns>
        ''' <remarks>This returns a Vector3, not a Vector2.</remarks>
        Public Function CrossProduct(ByRef vect2 As Vector2) As Vector3
            'Doesn't make a whole lot of sense for 2D vectors
            'a x b = [a2b3 - a3b2, a3b1 - a1b3, a1b2 - a2b1]

            Return New Vector3(0, 0, (Me.I * vect2.J - Me.J * vect2.I))
        End Function

        ''' <summary>
        ''' Takes the dot-product of this Vector2 and another.
        ''' </summary>
        ''' <param name="vect2"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function DotProduct(ByRef vect2 As Vector2) As Double
            Return ((Me.I * vect2.I) + (Me.J * vect2.J))
        End Function

        ''' <summary>
        ''' Shadowed. Determines whether the this Vector2 and another are equal.
        ''' </summary>
        ''' <param name="vect2"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shadows Function Equals(ByRef vect2 As Vector2) As Boolean
            If (Me.I = vect2.I) And (Me.J = vect2.J) Then
                Return True
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' Determines whether the length of this Vector2.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overridable Function Length() As Double
            Return Math.Sqrt(Math.Pow(Me.I, 2) + Math.Pow(Me.J, 2))
        End Function

        ''' <summary>
        ''' Determines the unit-length equivalent vector of this Vector2.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NormalizedVector() As Vector2
            If Me.I = 0 And Me.J = 0 Then
                Return Me
            Else
                Return (1 / Me.Length()) * Me
            End If
        End Function

        ''' <summary>
        ''' Determines whether the this Vector2 and another are parallel.
        ''' </summary>
        ''' <param name="vect2"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Parallel(ByRef vect2 As Vector2) As Boolean
            If Me.CrossProduct(vect2) = Vector3.zeroVector Then
                Return True
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' Determines whether the this Vector2 and another are perpendicular.
        ''' </summary>
        ''' <param name="vect2"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Perpendicular(ByRef vect2 As Vector2) As Boolean
            If Me.DotProduct(vect2) = 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' Multiplies this Vector2 by a scalar.
        ''' </summary>
        ''' <param name="scalar"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ScalarMultiply(ByVal scalar As Double) As Vector2
            Return New Vector2(scalar * Me.I, scalar * Me.J)
        End Function

        ''' <summary>
        ''' Subtracts a Vector2 from this Vector2.
        ''' </summary>
        ''' <param name="vect2"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Subtract(ByRef vect2 As Vector2) As Vector2
            Return New Vector2(Me.I - vect2.I, Me.J - vect2.J)
        End Function

        ''' <summary>
        ''' Converts this Vector2 to a human readable string.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overrides Function ToString() As String
            Return "<" & Me.I & "," & Me.J & ">"
        End Function

        ''' <summary>
        ''' Rotates this Vector2 by a given number of radians.
        ''' </summary>
        ''' <param name="angle">The angle to rotate by in radians.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Rotate(ByVal angle As Double) As Vector2
            Return New Vector2(Me.I * Math.Cos(angle) - Me.J * Math.Sin(angle), Me.I * Math.Sin(angle) + Me.J * Math.Cos(angle))
        End Function

#End Region

#Region "  Operators  "

        ''' <summary>
        ''' Indicates whether two Vector2 objects are equal.
        ''' </summary>
        ''' <param name="lhs"></param>
        ''' <param name="rhs"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Operator =(ByVal lhs As Vector2, ByVal rhs As Vector2) As Boolean
            Return lhs.Equals(rhs)
        End Operator

        ''' <summary>
        ''' Indicates whether two Vector2 objects are not equal.
        ''' </summary>
        ''' <param name="lhs"></param>
        ''' <param name="rhs"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Operator <>(ByVal lhs As Vector2, ByVal rhs As Vector2) As Boolean
            Return Not lhs.Equals(rhs)
        End Operator

        ''' <summary>
        ''' Adds two Vector2 together.
        ''' </summary>
        ''' <param name="lhs"></param>
        ''' <param name="rhs"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Operator +(ByVal lhs As Vector2, ByVal rhs As Vector2) As Vector2
            Return lhs.Add(rhs)
        End Operator

        ''' <summary>
        ''' Subtracts two Vector2 together.
        ''' </summary>
        ''' <param name="lhs"></param>
        ''' <param name="rhs"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Operator -(ByVal lhs As Vector2, ByVal rhs As Vector2) As Vector2
            Return lhs.Subtract(rhs)
        End Operator

        ''' <summary>
        ''' Multiplies a Vector2 by a scalar.
        ''' </summary>
        ''' <param name="lhs"></param>
        ''' <param name="rhs"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Operator *(ByVal lhs As Double, ByVal rhs As Vector2) As Vector2
            Return rhs.ScalarMultiply(lhs)
        End Operator

        ''' <summary>
        ''' Multiplies a Vector2 by a scalar.
        ''' </summary>
        ''' <param name="lhs"></param>
        ''' <param name="rhs"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Operator *(ByVal lhs As Vector2, ByVal rhs As Double) As Vector2
            Return lhs.ScalarMultiply(rhs)
        End Operator

        Public Shared Widening Operator CType(ByVal vect2 As Vector2) As Mathematics.Algebra.Vector(Of RealNumber)
            Dim resultVect As New Mathematics.Algebra.Vector(Of RealNumber)(2)

            resultVect.Item(0) = New RealNumber(vect2.I)
            resultVect.Item(1) = New RealNumber(vect2.J)

            Return resultVect
        End Operator

        Public Shared Widening Operator CType(ByVal vect As Mathematics.Algebra.Vector(Of RealNumber)) As Vector2
            Dim resultVect As New Vector2

            resultVect.I = vect.Item(0).Value
            resultVect.J = vect.Item(1).Value

            Return resultVect
        End Operator

#End Region

    End Class

End Namespace