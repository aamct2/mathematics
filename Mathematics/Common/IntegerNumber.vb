
''' <summary>
''' Provides a wrapper for the Long type that is usable by other Mathematics classes.
''' </summary>
''' <remarks></remarks>
Public Class IntegerNumber
    Implements ISubtractable(Of IntegerNumber), IMultipliable(Of IntegerNumber), IMultiplicativeIdentity(Of IntegerNumber), IComparable(Of IntegerNumber), IEquatable(Of IntegerNumber), IAbsoluteable(Of IntegerNumber)

    Private myValue As Long

#Region "  Constructors  "

    Public Sub New()
        Me.Value = 0
    End Sub

    Public Sub New(ByVal newValue As Long)
        Me.Value = newValue
    End Sub

#End Region

#Region "  Properties  "

    Public Property Value() As Long
        Get
            Return Me.myValue
        End Get
        Set(ByVal value As Long)
            Me.myValue = value
        End Set
    End Property

    Public ReadOnly Property AdditiveIdentity() As IntegerNumber Implements IAdditiveIdentity(Of IntegerNumber).AdditiveIdentity
        Get
            Return New IntegerNumber(0)
        End Get
    End Property

    Public ReadOnly Property MultiplicativeIdentity() As IntegerNumber Implements IMultiplicativeIdentity(Of IntegerNumber).MultiplicativeIdentity
        Get
            Return New IntegerNumber(1)
        End Get
    End Property

#End Region

#Region "  Methods  "

    Public Function AbsoluteValue() As IntegerNumber Implements IAbsoluteable(Of IntegerNumber).AbsoluteValue
        Return New IntegerNumber(Math.Abs(Me.Value))
    End Function

    Public Function Add(ByVal num2 As IntegerNumber) As IntegerNumber Implements IAddable(Of IntegerNumber).Add
        Return New IntegerNumber(Me.Value + num2.Value)
    End Function

    Public Function CompareTo(ByVal num2 As IntegerNumber) As Integer Implements System.IComparable(Of IntegerNumber).CompareTo
        If Me.Value < num2.Value Then
            Return -1
        ElseIf Me.Value = num2.Value Then
            Return 0
        Else
            Return 1
        End If
    End Function

    Public Function Multiply(ByVal num2 As IntegerNumber) As IntegerNumber Implements IMultipliable(Of IntegerNumber).Multiply
        Return New IntegerNumber(Me.Value * num2.Value)
    End Function

    Public Function Subtract(ByVal num2 As IntegerNumber) As IntegerNumber Implements ISubtractable(Of IntegerNumber).Subtract
        Return New IntegerNumber(Me.Value - num2.Value)
    End Function

    Public Function IntegerDivide(ByVal num2 As IntegerNumber) As IntegerNumber
        If num2.Value = 0 Then
            Throw New DivideByZeroException
        End If

        Return New IntegerNumber(Me.Value \ num2.Value)
    End Function

    Public Shadows Function Equals(ByVal other As IntegerNumber) As Boolean Implements System.IEquatable(Of IntegerNumber).Equals
        If Me.CompareTo(other) = 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Overrides Function ToString() As String
        Return Me.Value.ToString
    End Function

#End Region

#Region "  Operators  "

    Public Shared Operator +(ByVal lhs As IntegerNumber, ByVal rhs As IntegerNumber) As IntegerNumber
        Return lhs.Add(rhs)
    End Operator

    Public Shared Operator -(ByVal lhs As IntegerNumber, ByVal rhs As IntegerNumber) As IntegerNumber
        Return lhs.Subtract(rhs)
    End Operator

    Public Shared Operator *(ByVal lhs As IntegerNumber, ByVal rhs As IntegerNumber) As IntegerNumber
        Return lhs.Multiply(rhs)
    End Operator

    Public Shared Operator \(ByVal lhs As IntegerNumber, ByVal rhs As IntegerNumber) As IntegerNumber
        Return lhs.IntegerDivide(rhs)
    End Operator

    Public Shared Operator Mod(ByVal lhs As IntegerNumber, ByVal rhs As IntegerNumber) As IntegerNumber
        Return lhs.Subtract(rhs.Multiply(lhs.IntegerDivide(rhs)))
    End Operator

    Public Shared Operator =(ByVal lhs As IntegerNumber, ByVal rhs As IntegerNumber) As Boolean
        Return lhs.CompareTo(rhs) = 0
    End Operator

    Public Shared Operator <>(ByVal lhs As IntegerNumber, ByVal rhs As IntegerNumber) As Boolean
        Return lhs.CompareTo(rhs) <> 0
    End Operator

    Public Shared Operator >(ByVal lhs As IntegerNumber, ByVal rhs As IntegerNumber) As Boolean
        Return lhs.CompareTo(rhs) = 1
    End Operator

    Public Shared Operator <(ByVal lhs As IntegerNumber, ByVal rhs As IntegerNumber) As Boolean
        Return lhs.CompareTo(rhs) = -1
    End Operator

    Public Shared Operator >=(ByVal lhs As IntegerNumber, ByVal rhs As IntegerNumber) As Boolean
        Return (lhs.CompareTo(rhs) >= 0)
    End Operator

    Public Shared Operator <=(ByVal lhs As IntegerNumber, ByVal rhs As IntegerNumber) As Boolean
        Return (lhs.CompareTo(rhs) <= 0)
    End Operator

#End Region

End Class
