
''' <summary>
''' Defines a method for adding two elements together.
''' </summary>
''' <typeparam name="T"></typeparam>
''' <remarks></remarks>
Public Interface IAddable(Of T)

    Function Add(ByVal otherT As T) As T

End Interface
