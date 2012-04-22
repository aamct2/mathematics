
Module CommonExceptions

    ''' <summary>
    ''' Represents an error that an object was not a member of another object.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class NotMemberOfException
        Inherits Exception

#Region "  Constructors  "

        Public Sub New()
            MyBase.New("The element is not a member of the object.")
        End Sub

        Public Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub

#End Region

    End Class

    ''' <summary>
    ''' Represents an error than an object did not satisfy a particular property.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class DoesNotSatisfyPropertyException
        Inherits Exception

#Region "  Constructors  "

        Public Sub New()
            MyBase.New("The object does not satisfy a necessary property.")
        End Sub

        Public Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub

        Public Sub New(ByVal message As String, ByVal innerException As Exception)
            MyBase.New(message, innerException)
        End Sub

#End Region

    End Class

    ''' <summary>
    ''' Represents an error that the returned value is undefined.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class UndefiniedException
        Inherits Exception

#Region "  Constructors  "

        Public Sub New()
            MyBase.New("The result is undefined.")
        End Sub

        Public Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub

        Public Sub New(ByVal message As String, ByVal innerException As Exception)
            MyBase.New(message, innerException)
        End Sub

#End Region

    End Class

End Module
