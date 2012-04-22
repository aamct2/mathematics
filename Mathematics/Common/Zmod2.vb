Public Class Zmod2
    Implements ISubtractable(Of Zmod2), IDivideable(Of Zmod2), IComparable(Of Zmod2), IAbsoluteable(Of Zmod2), IEquatable(Of Zmod2)

    Private myValue As IntegerNumber

#Region "  Constructors  "

    Public Sub New()
        Me.Value = New IntegerNumber(0)
    End Sub

    Public Sub New(ByVal newValue As IntegerNumber)
        Me.Value = newValue Mod Zmod2.Integer2()
    End Sub

#End Region

#Region "  Properties  "

    Public Property Value() As IntegerNumber
        Get
            Return Me.myValue
        End Get
        Set(ByVal newValue As IntegerNumber)
            Me.myValue = newValue Mod Integer2()
        End Set
    End Property

    Public ReadOnly Property AdditiveIdentity() As Zmod2 Implements IAdditiveIdentity(Of Zmod2).AdditiveIdentity
        Get
            Return New Zmod2(New IntegerNumber(0))
        End Get
    End Property

    Public ReadOnly Property MultiplicativeIdentity() As Zmod2 Implements IMultiplicativeIdentity(Of Zmod2).MultiplicativeIdentity
        Get
            Return New Zmod2(New IntegerNumber(1))
        End Get
    End Property

#End Region

#Region "  Methods  "

    Private Shared Function Integer2() As IntegerNumber
        Return New IntegerNumber(2)
    End Function

    Public Function AbsoluteValue() As Zmod2 Implements IAbsoluteable(Of Zmod2).AbsoluteValue
        Return New Zmod2(Me.Value)
    End Function

    Public Function Add(ByVal otherT As Zmod2) As Zmod2 Implements IAddable(Of Zmod2).Add
        Return New Zmod2((Me.Value + otherT.Value) Mod Zmod2.Integer2())
    End Function

    Public Function Divide(ByVal otherT As Zmod2) As Zmod2 Implements IDivideable(Of Zmod2).Divide
        Return New Zmod2((Me.Value \ otherT.Value) Mod Zmod2.Integer2())
    End Function

    Public Function Multiply(ByVal otherT As Zmod2) As Zmod2 Implements IMultipliable(Of Zmod2).Multiply
        Return New Zmod2((Me.Value * otherT.Value) Mod Zmod2.Integer2())
    End Function

    Public Function Subtract(ByVal otherT As Zmod2) As Zmod2 Implements ISubtractable(Of Zmod2).Subtract
        Return New Zmod2((Me.Value - otherT.Value).AbsoluteValue() Mod Zmod2.Integer2())
    End Function

    Public Function CompareTo(ByVal other As Zmod2) As Integer Implements System.IComparable(Of Zmod2).CompareTo
        If Me.Value < other.Value Then
            Return -1
        ElseIf Me.Value = other.Value Then
            Return 0
        Else
            Return 1
        End If
    End Function

    Public Shadows Function Equals(ByVal other As Zmod2) As Boolean Implements System.IEquatable(Of Zmod2).Equals
        If Me.CompareTo(other) = 0 Then
            Return True
        Else
            Return False
        End If
    End Function

#End Region

#Region "  Operators  "

    Public Shared Operator +(ByVal lhs As Zmod2, ByVal rhs As Zmod2) As Zmod2
        Return lhs.Add(rhs)
    End Operator

    Public Shared Operator -(ByVal lhs As Zmod2, ByVal rhs As Zmod2) As Zmod2
        Return lhs.Subtract(rhs)
    End Operator

    Public Shared Operator *(ByVal lhs As Zmod2, ByVal rhs As Zmod2) As Zmod2
        Return lhs.Multiply(rhs)
    End Operator

    Public Shared Operator /(ByVal lhs As Zmod2, ByVal rhs As Zmod2) As Zmod2
        Return lhs.Divide(rhs)
    End Operator

    Public Shared Operator =(ByVal lhs As Zmod2, ByVal rhs As Zmod2) As Boolean
        Return lhs.CompareTo(rhs) = 0
    End Operator

    Public Shared Operator <>(ByVal lhs As Zmod2, ByVal rhs As Zmod2) As Boolean
        Return lhs.CompareTo(rhs) <> 0
    End Operator

    Public Shared Operator >(ByVal lhs As Zmod2, ByVal rhs As Zmod2) As Boolean
        Return lhs.CompareTo(rhs) = 1
    End Operator

    Public Shared Operator <(ByVal lhs As Zmod2, ByVal rhs As Zmod2) As Boolean
        Return lhs.CompareTo(rhs) = -1
    End Operator

    Public Shared Operator >=(ByVal lhs As Zmod2, ByVal rhs As Zmod2) As Boolean
        Return (lhs.CompareTo(rhs) >= 0)
    End Operator

    Public Shared Operator <=(ByVal lhs As Zmod2, ByVal rhs As Zmod2) As Boolean
        Return (lhs.CompareTo(rhs) <= 0)
    End Operator

#End Region


    Private Class Zmod2AdditionMap
        Inherits Map(Of Tuple, Zmod2)

        Public Overrides Function ApplyMap(ByVal input As Tuple) As Zmod2
            Dim input1 As Zmod2
            Dim input2 As Zmod2

            input1 = CType(input.Element(0), Zmod2)
            input2 = CType(input.Element(1), Zmod2)

            Return input1 + input2
        End Function
    End Class

End Class



