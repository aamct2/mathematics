
''' <summary>
''' Defines methods for dividing two elements as well as multiplying them and retrieving the multiplicative identity.
''' </summary>
''' <typeparam name="T"></typeparam>
''' <remarks></remarks>
Public Interface IDivideable(Of T)
    Inherits IMultipliable(Of T), IMultiplicativeIdentity(Of T)

    Function Divide(ByVal otherT As T) As T

End Interface
