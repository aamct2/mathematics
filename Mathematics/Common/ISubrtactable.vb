
''' <summary>
''' Defines methods for subtracting two elements as well as adding them and retrieving the additive identity.
''' </summary>
''' <typeparam name="T"></typeparam>
''' <remarks></remarks>
Public Interface ISubtractable(Of T)
    Inherits IAddable(Of T), IAdditiveIdentity(Of T)

    Function Subtract(ByVal otherT As T) As T

End Interface
