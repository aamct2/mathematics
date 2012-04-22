
''' <summary>
''' Represents a complex number that is usable by other Mathematics classes.
''' </summary>
''' <remarks></remarks>
Public Class ComplexNumber
    Implements ISubtractable(Of ComplexNumber), IDivideable(Of ComplexNumber), IComparable(Of ComplexNumber), IAbsoluteable(Of ComplexNumber)

    Private myRealPart As RealNumber
    Private myImaginaryPart As RealNumber

#Region "  Constructors  "

    Public Sub New()
        Me.New(New RealNumber(0.0), New RealNumber(0.0))
    End Sub

    Public Sub New(ByVal real As RealNumber, ByVal imaginary As RealNumber)
        Me.RealPart = real
        Me.ImaginaryPart = imaginary
    End Sub

#End Region

#Region "  Properties  "

    Public Property RealPart() As RealNumber
        Get
            Return Me.myRealPart
        End Get
        Set(ByVal value As RealNumber)
            Me.myRealPart = value
        End Set
    End Property

    Public Property ImaginaryPart() As RealNumber
        Get
            Return Me.myImaginaryPart
        End Get
        Set(ByVal value As RealNumber)
            Me.myImaginaryPart = value
        End Set
    End Property

    Public ReadOnly Property AdditiveIdentity() As ComplexNumber Implements ISubtractable(Of Mathematics.ComplexNumber).AdditiveIdentity
        Get
            Return New ComplexNumber(New RealNumber(0.0), New RealNumber(0.0))
        End Get
    End Property

    Public ReadOnly Property MultiplicativeIdentity() As ComplexNumber Implements IDivideable(Of Mathematics.ComplexNumber).MultiplicativeIdentity
        Get
            Return New ComplexNumber(New RealNumber(1.0), New RealNumber(1.0))
        End Get
    End Property

#End Region

#Region "  Methods  "

    Public Function AbsoluteValue() As ComplexNumber Implements IAbsoluteable(Of Mathematics.ComplexNumber).AbsoluteValue
        Return New ComplexNumber(New RealNumber(Math.Sqrt(Me.RealPart.Value * Me.RealPart.Value + Me.ImaginaryPart.Value * Me.ImaginaryPart.Value)), New RealNumber(0.0))
    End Function

    Public Function Add(ByVal num2 As ComplexNumber) As ComplexNumber Implements ISubtractable(Of Mathematics.ComplexNumber).Add
        Return New ComplexNumber(Me.RealPart + num2.RealPart, Me.ImaginaryPart + num2.ImaginaryPart)
    End Function

    Public Shared Function Arg(ByRef num As ComplexNumber) As RealNumber
        If num.RealPart = 0 And num.ImaginaryPart = 0 Then
            Throw New UndefiniedException
        ElseIf num.RealPart > 0 Or num.ImaginaryPart <> 0 Then
            Return 2 * RealNumber.Arctan(num.ImaginaryPart / (RealNumber.Sqrt(num.RealPart ^ 2 + num.ImaginaryPart ^ 2) + num.RealPart))
        Else
            Return New RealNumber(Math.PI)
        End If
    End Function

    Public Function CompareTo(ByVal num2 As ComplexNumber) As Integer Implements IComparable(Of Mathematics.ComplexNumber).CompareTo
        If Me.AbsoluteValue.RealPart.Value < num2.AbsoluteValue.RealPart.Value Then
            Return -1
        ElseIf Me.RealPart.Value = num2.RealPart.Value And _
               Me.ImaginaryPart.Value = num2.ImaginaryPart.Value Then
            Return 0
        Else
            Return 1
        End If
    End Function

    Public Function Conjugate() As ComplexNumber
        Return New ComplexNumber(Me.RealPart, -1 * Me.ImaginaryPart)
    End Function

    Public Function Divide(ByVal num2 As ComplexNumber) As ComplexNumber Implements IDivideable(Of Mathematics.ComplexNumber).Divide
        If num2.RealPart.Value = 0 And num2.ImaginaryPart.Value = 0 Then
            Throw New DivideByZeroException("num2 cannot be zero")
        Else
            Dim denom As RealNumber = (num2.RealPart * num2.RealPart + num2.ImaginaryPart * num2.ImaginaryPart)
            Return New ComplexNumber((Me.RealPart * num2.RealPart + Me.ImaginaryPart * num2.ImaginaryPart) / denom, _
                                     (Me.ImaginaryPart * num2.RealPart - Me.RealPart * num2.ImaginaryPart) / denom)
        End If
    End Function

    Public Shared Function Log(ByRef num As ComplexNumber) As ComplexNumber
        Dim ArgNum As RealNumber

        Try
            ArgNum = ComplexNumber.Arg(num)
        Catch ex As UndefiniedException
            Throw New UndefiniedException("Arg of 'num' is undefined.", ex)
        End Try

        Return New ComplexNumber(0.5 * RealNumber.Ln(num.RealPart ^ 2 + num.ImaginaryPart ^ 2), ArgNum)
    End Function

    Public Function Multiply(ByVal num2 As ComplexNumber) As ComplexNumber Implements IDivideable(Of Mathematics.ComplexNumber).Multiply
        Return New ComplexNumber(Me.RealPart * num2.RealPart - Me.ImaginaryPart * num2.ImaginaryPart, Me.ImaginaryPart * num2.RealPart + Me.RealPart * num2.ImaginaryPart)
    End Function

    Public Shared Function Modulus(ByRef num As ComplexNumber) As RealNumber
        Return RealNumber.Sqrt(num.RealPart ^ 2 + num.ImaginaryPart ^ 2)
    End Function

    ''' <summary>
    ''' Returns a pair of complex numbers of the form re^(i*phi). The first number is 'r'. The second number is 'phi'.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PolarForm() As Tuple(Of RealNumber, RealNumber)
        Dim r As RealNumber
        Dim phi As RealNumber

        If Me.RealPart = 0 And Me.ImaginaryPart = 0 Then
            Throw New UndefiniedException
        End If

        r = ComplexNumber.Modulus(Me)
        phi = RealNumber.Arccos(Me.RealPart)

        Return New Tuple(Of RealNumber, RealNumber)(r, phi)
    End Function

    Public Function Subtract(ByVal num2 As ComplexNumber) As ComplexNumber Implements ISubtractable(Of Mathematics.ComplexNumber).Subtract
        Return New ComplexNumber(Me.RealPart - num2.RealPart, Me.ImaginaryPart - num2.ImaginaryPart)
    End Function

    Public Overrides Function ToString() As String
        If Me.ImaginaryPart < 0 Then
            Return Me.RealPart.ToString & " - " & Me.ImaginaryPart.AbsoluteValue.ToString & "i"
        Else
            Return Me.RealPart.ToString & " + " & Me.ImaginaryPart.ToString & "i"
        End If
    End Function

#End Region

#Region "  Operators  "

    Public Shared Operator +(ByVal lhs As ComplexNumber, ByVal rhs As ComplexNumber) As ComplexNumber
        Return lhs.Add(rhs)
    End Operator

    Public Shared Operator -(ByVal lhs As ComplexNumber, ByVal rhs As ComplexNumber) As ComplexNumber
        Return lhs.Subtract(rhs)
    End Operator

    Public Shared Operator *(ByVal lhs As ComplexNumber, ByVal rhs As ComplexNumber) As ComplexNumber
        Return lhs.Multiply(rhs)
    End Operator

    Public Shared Operator /(ByVal lhs As ComplexNumber, ByVal rhs As ComplexNumber) As ComplexNumber
        Return lhs.Divide(rhs)
    End Operator

    Public Shared Operator =(ByVal lhs As ComplexNumber, ByVal rhs As ComplexNumber) As Boolean
        Return lhs.CompareTo(rhs) = 0
    End Operator

    Public Shared Operator <>(ByVal lhs As ComplexNumber, ByVal rhs As ComplexNumber) As Boolean
        Return lhs.CompareTo(rhs) <> 0
    End Operator

#End Region

End Class
