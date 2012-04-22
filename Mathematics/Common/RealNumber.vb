
''' <summary>
''' Provides a wrapper for the Double type that is usable by other Mathematics classes.
''' </summary>
''' <remarks></remarks>
Public Class RealNumber
    Implements ISubtractable(Of RealNumber), IDivideable(Of RealNumber), IComparable(Of RealNumber), IAbsoluteable(Of RealNumber)

    Private myValue As Double

#Region "  Constructors  "

    Public Sub New()
        Me.Value = 0.0
    End Sub

    Public Sub New(ByVal newValue As Double)
        Me.Value = newValue
    End Sub

#End Region

#Region "  Properties  "

    Public Property Value() As Double
        Get
            Return Me.myValue
        End Get
        Set(ByVal newValue As Double)
            Me.myValue = newValue
        End Set
    End Property

    Public ReadOnly Property AdditiveIdentity() As RealNumber Implements ISubtractable(Of RealNumber).AdditiveIdentity
        Get
            Return New RealNumber(0)
        End Get
    End Property

    Public ReadOnly Property MultiplicativeIdentity() As RealNumber Implements IDivideable(Of RealNumber).MultiplicativeIdentity
        Get
            Return New RealNumber(1)
        End Get
    End Property

#End Region

#Region "  Methods  "

    Public Function AbsoluteValue() As RealNumber Implements IAbsoluteable(Of Mathematics.RealNumber).AbsoluteValue
        Return New RealNumber(Math.Abs(Me.Value))
    End Function

    Public Function Add(ByVal num2 As RealNumber) As RealNumber Implements ISubtractable(Of RealNumber).Add
        Return New RealNumber(Me.Value + num2.Value)
    End Function

    Public Function CompareTo(ByVal num2 As RealNumber) As Integer Implements IComparable(Of RealNumber).CompareTo
        If Me.Value < num2.Value Then
            Return -1
        ElseIf Me.Value = num2.Value Then
            Return 0
        Else
            Return 1
        End If
    End Function

    Public Function Divide(ByVal num2 As RealNumber) As RealNumber Implements IDivideable(Of Mathematics.RealNumber).Divide
        If num2.CompareTo(Me.AdditiveIdentity) = 0 Then
            Throw New DivideByZeroException("num2 cannot be zero.")
        Else
            Return New RealNumber(Me.Value / num2.Value)
        End If
    End Function

    Public Shared Function Ln(ByRef num As RealNumber) As RealNumber
        If num = 0 Then
            Throw New UndefiniedException
        Else
            Return New RealNumber(Math.Log(num.Value))
        End If
    End Function

    Public Function Multiply(ByVal num2 As RealNumber) As RealNumber Implements IDivideable(Of RealNumber).Multiply
        Return New RealNumber(Me.Value * num2.Value)
    End Function

    Public Function Subtract(ByVal num2 As RealNumber) As RealNumber Implements ISubtractable(Of RealNumber).Subtract
        Return New RealNumber(Me.Value - num2.Value)
    End Function

    Public Function Power(ByRef num2 As RealNumber) As RealNumber
        Return New RealNumber(Math.Pow(Me.Value, num2.Value))
    End Function

#Region "  Trig Functions  "

    Public Shared Function Sin(ByRef num As RealNumber) As RealNumber
        Return New RealNumber(Math.Sin(num.Value))
    End Function

    Public Shared Function Cos(ByRef num As RealNumber) As RealNumber
        Return New RealNumber(Math.Cos(num.Value))
    End Function

    Public Shared Function Tan(ByRef num As RealNumber) As RealNumber
        Return New RealNumber(Math.Tan(num.Value))
    End Function

    Public Shared Function Arcsin(ByRef num As RealNumber) As RealNumber
        Return New RealNumber(Math.Asin(num.Value))
    End Function

    Public Shared Function Arccos(ByRef num As RealNumber) As RealNumber
        Return New RealNumber(Math.Acos(num.Value))
    End Function

    Public Shared Function Arctan(ByRef num As RealNumber) As RealNumber
        Return New RealNumber(Math.Atan(num.Value))
    End Function

    Public Shared Function Arctan2(ByRef num1 As RealNumber, ByRef num2 As RealNumber) As RealNumber
        Return New RealNumber(Math.Atan2(num1.Value, num2.Value))
    End Function

#End Region

    Public Shared Function Sqrt(ByRef num As RealNumber) As RealNumber
        Return New RealNumber(Math.Sqrt(num.Value))
    End Function

    Public Overrides Function ToString() As String
        Return Me.Value.ToString
    End Function

#End Region

#Region "  Operators  "

#Region "  Operators: +  "

    Public Shared Operator +(ByVal lhs As RealNumber, ByVal rhs As RealNumber) As RealNumber
        Return lhs.Add(rhs)
    End Operator

    Public Shared Operator +(ByVal lhs As Double, ByVal rhs As RealNumber) As RealNumber
        Dim leftOperand As New RealNumber(lhs)
        Return leftOperand.Add(rhs)
    End Operator

    Public Shared Operator +(ByVal lhs As RealNumber, ByVal rhs As Double) As RealNumber
        Return lhs.Add(New RealNumber(rhs))
    End Operator

#End Region

#Region "  Operators: -  "

    Public Shared Operator -(ByVal lhs As RealNumber, ByVal rhs As RealNumber) As RealNumber
        Return lhs.Subtract(rhs)
    End Operator

    Public Shared Operator -(ByVal lhs As Double, ByVal rhs As RealNumber) As RealNumber
        Dim leftOperand As New RealNumber(lhs)
        Return leftOperand.Subtract(rhs)
    End Operator

    Public Shared Operator -(ByVal lhs As RealNumber, ByVal rhs As Double) As RealNumber
        Return lhs.Subtract(New RealNumber(rhs))
    End Operator

#End Region

#Region "  Operators: *  "

    Public Shared Operator *(ByVal lhs As RealNumber, ByVal rhs As RealNumber) As RealNumber
        Return lhs.Multiply(rhs)
    End Operator

    Public Shared Operator *(ByVal lhs As Double, ByVal rhs As RealNumber) As RealNumber
        Dim leftOperand As New RealNumber(lhs)
        Return leftOperand.Multiply(rhs)
    End Operator

    Public Shared Operator *(ByVal lhs As RealNumber, ByVal rhs As Double) As RealNumber
        Return lhs.Multiply(New RealNumber(rhs))
    End Operator

#End Region

#Region "  Operators: /  "

    Public Shared Operator /(ByVal lhs As RealNumber, ByVal rhs As RealNumber) As RealNumber
        Return lhs.Divide(rhs)
    End Operator

    Public Shared Operator /(ByVal lhs As Double, ByVal rhs As RealNumber) As RealNumber
        Dim leftOperand As New RealNumber(lhs)
        Return leftOperand.Divide(rhs)
    End Operator

    Public Shared Operator /(ByVal lhs As RealNumber, ByVal rhs As Double) As RealNumber
        Return lhs.Divide(New RealNumber(rhs))
    End Operator

#End Region

#Region "  Operators: =  "

    Public Shared Operator =(ByVal lhs As RealNumber, ByVal rhs As RealNumber) As Boolean
        Return lhs.CompareTo(rhs) = 0
    End Operator

    Public Shared Operator =(ByVal lhs As Double, ByVal rhs As RealNumber) As Boolean
        Dim leftOperand As New RealNumber(lhs)
        Return leftOperand.CompareTo(rhs) = 0
    End Operator

    Public Shared Operator =(ByVal lhs As RealNumber, ByVal rhs As Double) As Boolean
        Return lhs.CompareTo(New RealNumber(rhs)) = 0
    End Operator

#End Region

#Region "  Operators: <>  "

    Public Shared Operator <>(ByVal lhs As RealNumber, ByVal rhs As RealNumber) As Boolean
        Return lhs.CompareTo(rhs) <> 0
    End Operator

    Public Shared Operator <>(ByVal lhs As Double, ByVal rhs As RealNumber) As Boolean
        Dim leftOperand As New RealNumber(lhs)
        Return leftOperand.CompareTo(rhs) <> 0
    End Operator

    Public Shared Operator <>(ByVal lhs As RealNumber, ByVal rhs As Double) As Boolean
        Return lhs.CompareTo(New RealNumber(rhs)) <> 0
    End Operator

#End Region

#Region "  Operators: >  "

    Public Shared Operator >(ByVal lhs As RealNumber, ByVal rhs As RealNumber) As Boolean
        Return lhs.CompareTo(rhs) = 1
    End Operator

    Public Shared Operator >(ByVal lhs As Double, ByVal rhs As RealNumber) As Boolean
        Dim leftOperand As New RealNumber(lhs)
        Return leftOperand.CompareTo(rhs) = 1
    End Operator

    Public Shared Operator >(ByVal lhs As RealNumber, ByVal rhs As Double) As Boolean
        Return lhs.CompareTo(New RealNumber(rhs)) = 1
    End Operator

#End Region

#Region "  Operators: <  "

    Public Shared Operator <(ByVal lhs As RealNumber, ByVal rhs As RealNumber) As Boolean
        Return lhs.CompareTo(rhs) = -1
    End Operator

    Public Shared Operator <(ByVal lhs As Double, ByVal rhs As RealNumber) As Boolean
        Dim leftOperand As New RealNumber(lhs)
        Return leftOperand.CompareTo(rhs) = -1
    End Operator

    Public Shared Operator <(ByVal lhs As RealNumber, ByVal rhs As Double) As Boolean
        Return lhs.CompareTo(New RealNumber(rhs)) = -1
    End Operator

#End Region

#Region "  Operators: >=  "

    Public Shared Operator >=(ByVal lhs As RealNumber, ByVal rhs As RealNumber) As Boolean
        Return (lhs.CompareTo(rhs) >= 0)
    End Operator

    Public Shared Operator >=(ByVal lhs As Double, ByVal rhs As RealNumber) As Boolean
        Dim leftOperand As New RealNumber(lhs)
        Return leftOperand.CompareTo(rhs) >= 0
    End Operator

    Public Shared Operator >=(ByVal lhs As RealNumber, ByVal rhs As Double) As Boolean
        Return lhs.CompareTo(New RealNumber(rhs)) >= 0
    End Operator

#End Region

#Region "  Operators: <=  "

    Public Shared Operator <=(ByVal lhs As RealNumber, ByVal rhs As RealNumber) As Boolean
        Return (lhs.CompareTo(rhs) <= 0)
    End Operator

    Public Shared Operator <=(ByVal lhs As Double, ByVal rhs As RealNumber) As Boolean
        Dim leftOperand As New RealNumber(lhs)
        Return leftOperand.CompareTo(rhs) <= 0
    End Operator

    Public Shared Operator <=(ByVal lhs As RealNumber, ByVal rhs As Double) As Boolean
        Return lhs.CompareTo(New RealNumber(rhs)) <= 0
    End Operator

#End Region

#Region "  Operators: ^  "

    Public Shared Operator ^(ByVal lhs As RealNumber, ByVal rhs As RealNumber) As RealNumber
        Return New RealNumber(lhs.Value ^ rhs.Value)
    End Operator

    Public Shared Operator ^(ByVal lhs As Double, ByVal rhs As RealNumber) As RealNumber
        Return New RealNumber(lhs ^ rhs.Value)
    End Operator

    Public Shared Operator ^(ByVal lhs As RealNumber, ByVal rhs As Double) As RealNumber
        Return New RealNumber(lhs.Value ^ rhs)
    End Operator

#End Region

#End Region


End Class
