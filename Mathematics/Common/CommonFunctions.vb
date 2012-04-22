
''' <summary>
''' Provides static methods for common mathematical functions.
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class CommonFunctions

#Region "  Functions: Acos  "

    Public Shared Function Acos(ByVal number As RealNumber) As RealNumber
        Return New RealNumber(Math.Acos(number.Value))
    End Function

#End Region

#Region "  Functions: Asin  "

    Public Shared Function Asin(ByVal number As RealNumber) As RealNumber
        Return New RealNumber(Math.Asin(number.Value))
    End Function

#End Region

#Region "  Functions: Atan  "

    Public Shared Function Atan(ByVal number As RealNumber) As RealNumber
        Return New RealNumber(Math.Atan(number.Value))
    End Function

#End Region

#Region "  Functions: Ceiling  "

    Public Shared Function Ceiling(ByVal number As RealNumber) As RealNumber
        Return New RealNumber(Math.Ceiling(number.Value))
    End Function

#End Region

#Region "  Functions: Cos  "

    Public Shared Function Cos(ByVal number As RealNumber) As RealNumber
        Return New RealNumber(Math.Cos(number.Value))
    End Function

#End Region

#Region "  Functions: Cosh  "

    Public Shared Function Cosh(ByVal number As RealNumber) As RealNumber
        Return New RealNumber(Math.Cosh(number.Value))
    End Function

#End Region

#Region "  Functions: Exp  "

    Public Shared Function Exp(ByVal number As RealNumber) As RealNumber
        Return New RealNumber(Math.Exp(number.Value))
    End Function

#End Region

#Region "  Functions: Floor  "

    Public Shared Function Floor(ByVal number As RealNumber) As RealNumber
        Return New RealNumber(Math.Floor(number.Value))
    End Function

#End Region

#Region "  Functions: GCD  "

    Public Shared Function GCD(ByVal number1 As IntegerNumber, ByVal number2 As IntegerNumber) As IntegerNumber
        If number1.Value = 0 Then Return number2

        Do While number2.Value <> 0
            If number1 > number2 Then
                number1 = number1 - number2
            Else
                number2 = number2 - number1
            End If
        Loop

        Return number1
    End Function

#End Region

#Region "  Functions: Log  "

    Public Shared Function Log(ByVal number As RealNumber) As RealNumber
        Return New RealNumber(Math.Log(number.Value))
    End Function

#End Region

#Region "  Functions: Log10  "

    Public Shared Function Log10(ByVal number As RealNumber) As RealNumber
        Return New RealNumber(Math.Log10(number.Value))
    End Function

#End Region

#Region "  Functions: Max  "

    Public Shared Function Max(ByVal number1 As RealNumber, ByVal number2 As RealNumber) As RealNumber
        Return New RealNumber(Math.Max(number1.Value, number2.Value))
    End Function

#End Region

#Region "  Functions: Min  "

    Public Shared Function Min(ByVal number1 As RealNumber, ByVal number2 As RealNumber) As RealNumber
        Return New RealNumber(Math.Min(number1.Value, number2.Value))
    End Function

#End Region

#Region "  Functions: Pow  "

    Public Shared Function Pow(ByVal x As RealNumber, ByVal y As RealNumber) As RealNumber
        Return New RealNumber(Math.Pow(x.Value, y.Value))
    End Function

#End Region

#Region "  Functions: Round  "

    Public Shared Function Round(ByVal number As RealNumber) As RealNumber
        Return New RealNumber(Math.Round(number.Value))
    End Function

#End Region

#Region "  Functions: Sign  "

    Public Shared Function Sign(ByVal number As RealNumber) As RealNumber
        Return New RealNumber(Math.Sign(number.Value))
    End Function

#End Region

#Region "  Functions: Sin  "

    Public Shared Function Sin(ByVal number As RealNumber) As RealNumber
        Return New RealNumber(Math.Sin(number.Value))
    End Function

#End Region

#Region "  Functions: Sinh  "

    Public Shared Function Sinh(ByVal number As RealNumber) As RealNumber
        Return New RealNumber(Math.Sinh(number.Value))
    End Function

#End Region

#Region "  Functions: Sqrt  "

    Public Shared Function Sqrt(ByVal number As RealNumber) As RealNumber
        Return New RealNumber(Math.Sqrt(number.Value))
    End Function

#End Region

#Region "  Functions: Tan  "

    Public Shared Function Tan(ByVal number As RealNumber) As RealNumber
        Return New RealNumber(Math.Tan(number.Value))
    End Function

#End Region

#Region "  Functions: Tanh  "

    Public Shared Function Tanh(ByVal number As RealNumber) As RealNumber
        Return New RealNumber(Math.Tanh(number.Value))
    End Function

#End Region

#Region "  Functions: Truncate  "

    Public Shared Function Truncate(ByVal number As RealNumber) As RealNumber
        Return New RealNumber(Math.Truncate(number.Value))
    End Function

#End Region

End Class
