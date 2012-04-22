
Namespace Algebra

    ''' <summary>
    ''' Represents a finite field with elements of type T.
    ''' </summary>
    ''' <typeparam name="T">The Type of element in the field.</typeparam>
    ''' <remarks></remarks>
    Public Class FiniteField(Of T As {Class, New, IEquatable(Of T)})
        Inherits FiniteCommutativeRing(Of T)

        Private myMultiplicativeIdentity As T

#Region "  Constructors  "

        Public Sub New(ByVal newSet As FiniteSet(Of T), ByVal newAddition As FiniteBinaryOperation(Of T), ByVal newMultiplication As FiniteBinaryOperation(Of T))
            MyBase.New(newSet, newAddition, newMultiplication)

            If newMultiplication.HasIdentity = False Then
                Throw New Exception("The new multiplication does not have an identity element.")
            End If

            If newMultiplication.Identity.Equals(newAddition.Identity) = True Then
                Throw New Exception("The multiplicative identity can not equal the additive identity.")
            End If

            If newMultiplication.HasInversesExcept(Me.AdditiveIdentity) = False Then
                Throw New Exception("The new multiplication does not have inverses for all elements (excluding the additive identity).")
            End If

            Me.myMultiplicativeIdentity = Me.MultiplicationOperation.Identity
        End Sub

#End Region

#Region "  Properties  "

        Public ReadOnly Property MultiplicativeIdentity() As T
            Get
                Return Me.myMultiplicativeIdentity
            End Get
        End Property

#End Region

#Region "  Methods  "

        ''' <summary>
        ''' Determines whether this field is a subfield of another field.
        ''' </summary>
        ''' <param name="superField"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsSubfieldOf(ByVal superField As FiniteField(Of T)) As Boolean
            Return Me.IsSubringOf(superField)
        End Function

#End Region

    End Class

End Namespace
