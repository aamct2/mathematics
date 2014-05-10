Public Class Zmod11
    Implements ISubtractable(Of Zmod11), IDivideable(Of Zmod11), IComparable(Of Zmod11), IAbsoluteable(Of Zmod11)

    Private myValue As IntegerNumber

#Region "  Constructors  "

    Public Sub New()
        Me.Value = New IntegerNumber(0)
    End Sub

    Public Sub New(ByVal newValue As IntegerNumber)
        Me.Value = newValue Mod Zmod11.Integer11()
    End Sub

#End Region

#Region "  Properties  "

    Public Property Value() As IntegerNumber
        Get
            Return Me.myValue
        End Get
        Set(ByVal newValue As IntegerNumber)
            Me.myValue = newValue
        End Set
    End Property

    Public ReadOnly Property AdditiveIdentity() As Zmod11 Implements IAdditiveIdentity(Of Zmod11).AdditiveIdentity
        Get
            Return New Zmod11(New IntegerNumber(0))
        End Get
    End Property

    Public ReadOnly Property MultiplicativeIdentity() As Zmod11 Implements IMultiplicativeIdentity(Of Zmod11).MultiplicativeIdentity
        Get
            Return New Zmod11(New IntegerNumber(1))
        End Get
    End Property

#End Region

#Region "  Methods  "

    Private Shared Function Integer11() As IntegerNumber
        Return New IntegerNumber(11)
    End Function

    Public Function AbsoluteValue() As Zmod11 Implements IAbsoluteable(Of Zmod11).AbsoluteValue
        Return New Zmod11(Me.Value)
    End Function

    Public Function Add(ByVal otherT As Zmod11) As Zmod11 Implements IAddable(Of Zmod11).Add
        Return New Zmod11((Me.Value + otherT.Value) Mod Zmod11.Integer11())
    End Function

    Public Function Divide(ByVal otherT As Zmod11) As Zmod11 Implements IDivideable(Of Zmod11).Divide
        Return New Zmod11((Me.Value \ otherT.Value) Mod Zmod11.Integer11())
    End Function

    Public Function Multiply(ByVal otherT As Zmod11) As Zmod11 Implements IMultipliable(Of Zmod11).Multiply
        Return New Zmod11((Me.Value * otherT.Value) Mod Zmod11.Integer11())
    End Function

    Public Function Subtract(ByVal otherT As Zmod11) As Zmod11 Implements ISubtractable(Of Zmod11).Subtract
        Return New Zmod11((Me.Value - otherT.Value) Mod Zmod11.Integer11())
    End Function

    Public Function CompareTo(ByVal other As Zmod11) As Integer Implements System.IComparable(Of Zmod11).CompareTo
        If Me.Value < other.Value Then
            Return -1
        ElseIf Me.Value = other.Value Then
            Return 0
        Else
            Return 1
        End If
    End Function

#End Region

#Region "  Operators  "

    Public Shared Operator +(ByVal lhs As Zmod11, ByVal rhs As Zmod11) As Zmod11
        Return lhs.Add(rhs)
    End Operator

    Public Shared Operator -(ByVal lhs As Zmod11, ByVal rhs As Zmod11) As Zmod11
        Return lhs.Subtract(rhs)
    End Operator

    Public Shared Operator *(ByVal lhs As Zmod11, ByVal rhs As Zmod11) As Zmod11
        Return lhs.Multiply(rhs)
    End Operator

    Public Shared Operator /(ByVal lhs As Zmod11, ByVal rhs As Zmod11) As Zmod11
        Return lhs.Divide(rhs)
    End Operator

    Public Shared Operator =(ByVal lhs As Zmod11, ByVal rhs As Zmod11) As Boolean
        Return lhs.CompareTo(rhs) = 0
    End Operator

    Public Shared Operator <>(ByVal lhs As Zmod11, ByVal rhs As Zmod11) As Boolean
        Return lhs.CompareTo(rhs) <> 0
    End Operator

    Public Shared Operator >(ByVal lhs As Zmod11, ByVal rhs As Zmod11) As Boolean
        Return lhs.CompareTo(rhs) = 1
    End Operator

    Public Shared Operator <(ByVal lhs As Zmod11, ByVal rhs As Zmod11) As Boolean
        Return lhs.CompareTo(rhs) = -1
    End Operator

    Public Shared Operator >=(ByVal lhs As Zmod11, ByVal rhs As Zmod11) As Boolean
        Return (lhs.CompareTo(rhs) >= 0)
    End Operator

    Public Shared Operator <=(ByVal lhs As Zmod11, ByVal rhs As Zmod11) As Boolean
        Return (lhs.CompareTo(rhs) <= 0)
    End Operator

#End Region

End Class
