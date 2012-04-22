Namespace Geometry

    ''' <summary>
    ''' Represents a 3-dimensional vector.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Vector3
        Inherits Vector2

        'The standard i,j,k components of the vector
        Private myK As Double

#Region "  Constructors  "

        Public Sub New()
            MyBase.New()
            Me.K = 0
        End Sub

        Public Sub New(ByVal newI As Double, ByVal newJ As Double, ByVal newK As Double)
            MyBase.New(newI, newJ)

            Me.K = newK
        End Sub

        Public Sub New(ByVal startPt As PointF3, ByVal endPt As PointF3)
            MyBase.New(startPt, endPt)

            Me.K = endPt.Z - startPt.Z
        End Sub

        Public Sub New(ByVal startPt As PointF3, ByVal directionPt As PointF3, ByVal magnitude As Double)
            Dim directionVector As New Vector3(startPt, directionPt)
            Dim normalVector As Vector3
            normalVector = directionVector.NormalizedVector()
            normalVector = magnitude * normalVector

            Me.I = normalVector.I
            Me.J = normalVector.J
            Me.K = normalVector.K
        End Sub

        Public Sub New(ByVal directionVect As Vector3, ByVal magnitude As Double)
            Dim normalizedVect As Vector3
            normalizedVect = magnitude * directionVect.NormalizedVector()

            Me.I = normalizedVect.I
            Me.J = normalizedVect.J
            Me.K = normalizedVect.K
        End Sub

#End Region

#Region "  Properties  "

        Public Property K() As Double
            Get
                Return Me.myK
            End Get
            Set(ByVal value As Double)
                Me.myK = value
            End Set
        End Property

        Public Shared Shadows ReadOnly Property zeroVector() As Vector3
            Get
                Return New Vector3(0, 0, 0)
            End Get
        End Property

#End Region

#Region "  Methods  "

        Public Shadows Function Add(ByRef vect2 As Vector3) As Vector3
            Return New Vector3(Me.I + vect2.I, Me.J + vect2.J, Me.K + vect2.K)
        End Function

        Public Shadows Function CrossProduct(ByRef vect2 As Vector3) As Vector3
            'a x b = [a2b3 - a3b2, a3b1 - a1b3, a1b2 - a2b1]

            Return New Vector3((Me.J * vect2.K - Me.K * vect2.J), _
                                (Me.K * vect2.I - Me.I * vect2.K), _
                                (Me.I * vect2.J - Me.J * vect2.I))
        End Function

        Public Shadows Function DotProduct(ByRef vect2 As Vector3) As Double
            Return ((Me.I * vect2.I) + (Me.J * vect2.J) + (Me.K * vect2.K))
        End Function

        Public Shadows Function Equals(ByRef vect2 As Vector3) As Boolean
            If (Me.I = vect2.I) And (Me.J = vect2.J) And (Me.K = vect2.K) Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Overrides Function Length() As Double
            Return Math.Sqrt(Math.Pow(Me.I, 2) + Math.Pow(Me.J, 2) + Math.Pow(Me.K, 2))
        End Function

        Public Shadows Function NormalizedVector() As Vector3
            If Me.I = 0 And Me.J = 0 And Me.K = 0 Then
                Return Me
            Else
                Return (1 / Me.Length()) * Me
            End If
        End Function

        Public Shadows Function Parallel(ByRef vect2 As Vector3) As Boolean
            If Me.CrossProduct(vect2) = Vector3.zeroVector Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Shadows Function Perpendicular(ByRef vect2 As Vector3) As Boolean
            If Me.DotProduct(vect2) = 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Shadows Function Rotate(ByRef angleIJ As Double, ByRef angleIK As Double) As Vector3
            Dim vectIJ As New Vector2(Me.I, Me.J)

            vectIJ = vectIJ.Rotate(angleIJ)

            Dim vectIK As New Vector2(vectIJ.I, Me.K)

            vectIK = vectIK.Rotate(angleIK)

            Return New Vector3(vectIK.I, vectIJ.J, vectIK.J)
        End Function

        Public Shadows Function ScalarMultiply(ByVal scalar As Double) As Vector3
            Return New Vector3(scalar * Me.I, scalar * Me.J, scalar * Me.K)
        End Function

        Public Shadows Function Subtract(ByRef vect2 As Vector3) As Vector3
            Return New Vector3(Me.I - vect2.I, Me.J - vect2.J, Me.K - vect2.K)
        End Function

        Public Overrides Function ToString() As String
            Return "<" & Me.I & "," & Me.J & "," & Me.K & ">"
        End Function

#End Region

#Region "  Operators  "

        Public Shared Shadows Operator =(ByVal lhs As Vector3, ByVal rhs As Vector3) As Boolean
            Return lhs.Equals(rhs)
        End Operator

        Public Shared Shadows Operator <>(ByVal lhs As Vector3, ByVal rhs As Vector3) As Boolean
            Return Not lhs.Equals(rhs)
        End Operator

        Public Shared Shadows Operator +(ByVal lhs As Vector3, ByVal rhs As Vector3) As Vector3
            Return lhs.Add(rhs)
        End Operator

        Public Shared Shadows Operator -(ByVal lhs As Vector3, ByVal rhs As Vector3) As Vector3
            Return lhs.Subtract(rhs)
        End Operator

        Public Shared Shadows Operator *(ByVal lhs As Double, ByVal rhs As Vector3) As Vector3
            Return rhs.ScalarMultiply(lhs)
        End Operator

        Public Shared Shadows Operator *(ByVal lhs As Vector3, ByVal rhs As Double) As Vector3
            Return lhs.ScalarMultiply(rhs)
        End Operator

        Public Shared Shadows Widening Operator CType(ByVal vect3 As Vector3) As Mathematics.Algebra.Vector(Of RealNumber)
            Dim resultVect As New Mathematics.Algebra.Vector(Of RealNumber)(3)

            resultVect.Item(0) = New RealNumber(vect3.I)
            resultVect.Item(1) = New RealNumber(vect3.J)
            resultVect.Item(2) = New RealNumber(vect3.K)

            Return resultVect
        End Operator

        Public Shared Shadows Widening Operator CType(ByVal vect As Mathematics.Algebra.Vector(Of RealNumber)) As Vector3
            Dim resultVect As New Vector3

            resultVect.I = vect.Item(0).Value
            resultVect.J = vect.Item(1).Value
            resultVect.K = vect.Item(2).Value

            Return resultVect
        End Operator

#End Region

    End Class

End Namespace