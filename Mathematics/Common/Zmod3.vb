Public Class Zmod3
    Implements ISubtractable(Of Zmod3), IDivideable(Of Zmod3), IComparable(Of Zmod3), IAbsoluteable(Of Zmod3)

    Private myValue As IntegerNumber

#Region "  Constructors  "

    Public Sub New()
        Me.Value = New IntegerNumber(0)
    End Sub

    Public Sub New(ByVal newValue As IntegerNumber)
        Me.Value = newValue Mod Zmod3.Integer3()
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

    Public ReadOnly Property AdditiveIdentity() As Zmod3 Implements IAdditiveIdentity(Of Zmod3).AdditiveIdentity
        Get
            Return New Zmod3(New IntegerNumber(0))
        End Get
    End Property

    Public ReadOnly Property MultiplicativeIdentity() As Zmod3 Implements IMultiplicativeIdentity(Of Zmod3).MultiplicativeIdentity
        Get
            Return New Zmod3(New IntegerNumber(1))
        End Get
    End Property

#End Region

#Region "  Methods  "

    Private Shared Function Integer3() As IntegerNumber
        Return New IntegerNumber(3)
    End Function

    Public Function AbsoluteValue() As Zmod3 Implements IAbsoluteable(Of Zmod3).AbsoluteValue
        Return New Zmod3(Me.Value)
    End Function

    Public Function Add(ByVal otherT As Zmod3) As Zmod3 Implements IAddable(Of Zmod3).Add
        Return New Zmod3((Me.Value + otherT.Value) Mod Zmod3.Integer3())
    End Function

    Public Function Divide(ByVal otherT As Zmod3) As Zmod3 Implements IDivideable(Of Zmod3).Divide
        Return New Zmod3((Me.Value \ otherT.Value) Mod Zmod3.Integer3())
    End Function

    Public Function Multiply(ByVal otherT As Zmod3) As Zmod3 Implements IMultipliable(Of Zmod3).Multiply
        Return New Zmod3((Me.Value * otherT.Value) Mod Zmod3.Integer3())
    End Function

    Public Function Subtract(ByVal otherT As Zmod3) As Zmod3 Implements ISubtractable(Of Zmod3).Subtract
        Return New Zmod3((Me.Value - otherT.Value) Mod Zmod3.Integer3())
    End Function

    Public Function CompareTo(ByVal other As Zmod3) As Integer Implements System.IComparable(Of Zmod3).CompareTo
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

    Public Shared Operator +(ByVal lhs As Zmod3, ByVal rhs As Zmod3) As Zmod3
        Return lhs.Add(rhs)
    End Operator

    Public Shared Operator -(ByVal lhs As Zmod3, ByVal rhs As Zmod3) As Zmod3
        Return lhs.Subtract(rhs)
    End Operator

    Public Shared Operator *(ByVal lhs As Zmod3, ByVal rhs As Zmod3) As Zmod3
        Return lhs.Multiply(rhs)
    End Operator

    Public Shared Operator /(ByVal lhs As Zmod3, ByVal rhs As Zmod3) As Zmod3
        Return lhs.Divide(rhs)
    End Operator

    Public Shared Operator =(ByVal lhs As Zmod3, ByVal rhs As Zmod3) As Boolean
        Return lhs.CompareTo(rhs) = 0
    End Operator

    Public Shared Operator <>(ByVal lhs As Zmod3, ByVal rhs As Zmod3) As Boolean
        Return lhs.CompareTo(rhs) <> 0
    End Operator

    Public Shared Operator >(ByVal lhs As Zmod3, ByVal rhs As Zmod3) As Boolean
        Return lhs.CompareTo(rhs) = 1
    End Operator

    Public Shared Operator <(ByVal lhs As Zmod3, ByVal rhs As Zmod3) As Boolean
        Return lhs.CompareTo(rhs) = -1
    End Operator

    Public Shared Operator >=(ByVal lhs As Zmod3, ByVal rhs As Zmod3) As Boolean
        Return (lhs.CompareTo(rhs) >= 0)
    End Operator

    Public Shared Operator <=(ByVal lhs As Zmod3, ByVal rhs As Zmod3) As Boolean
        Return (lhs.CompareTo(rhs) <= 0)
    End Operator

#End Region

End Class

