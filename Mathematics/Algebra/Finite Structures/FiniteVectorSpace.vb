
Namespace Algebra

    ''' <summary>
    ''' Represents a finite vector space with elements of type T and scalars of type Scalar.
    ''' </summary>
    ''' <typeparam name="T">The Type of element in the vector space.</typeparam>
    ''' <typeparam name="Scalar">The Type of scalar elements.</typeparam>
    ''' <remarks></remarks>
    Public Class FiniteVectorSpace(Of T As {Class, New, IEquatable(Of T)}, Scalar As {Class, New, IEquatable(Of Scalar)})
        Inherits FiniteLeftModule(Of T, Scalar)

        Private myScalarField As FiniteField(Of Scalar)


#Region "  Constructors  "

        ''' <summary>
        ''' Creates a new finite vector space.
        ''' </summary>
        ''' <param name="newSet">The set for the vector space.</param>
        ''' <param name="newAddition">The addition operation for the vector space.</param>
        ''' <param name="newMultiplication">The multiplication operation for the vector space.</param>
        ''' <param name="newScalarField">The field of scalars for the vector space.</param>
        ''' <exception cref="DoesNotSatisfyPropertyException">Throws DoesNotSatisfyPropertyException if 'newOperation' does not satisfy all the vector space axioms.</exception>
        ''' <remarks></remarks>
        Public Sub New(ByVal newSet As FiniteSet(Of T), ByVal newAddition As FiniteBinaryOperation(Of T), ByVal newMultiplication As FiniteLeftExternalBinaryOperation(Of Scalar, T), ByVal newScalarField As FiniteField(Of Scalar))
            MyBase.New(newSet, newAddition, newMultiplication, newScalarField)

            Me.myScalarField = newScalarField
        End Sub

#End Region

#Region "  Properties  "

        ''' <summary>
        ''' Returns the scalar field associated with the module.
        ''' </summary>
        ''' <value>The scalar field associated with the module.</value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ScalarField() As FiniteField(Of Scalar)
            Get
                Return Me.myScalarField
            End Get
        End Property

#End Region

#Region "  Methods  "

        ''' <summary>
        ''' Returns the trivial subspace of this vector space.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function TrivialSubspace() As FiniteVectorSpace(Of T, Scalar)
            Dim newSet As New FiniteSet(Of T)
            newSet.AddElement(Me.AdditiveIdentity)
            Dim newAdd As FiniteBinaryOperation(Of T) = Me.AdditionOperation.Restriction(newSet)
            Dim newMult As New FiniteLeftExternalBinaryOperation(Of Scalar, T)(Me.ScalarField.theSet, newSet, Me.LeftScalarMultiplicationOperation.theRelation)

            Return New FiniteVectorSpace(Of T, Scalar)(newSet, newAdd, newMult, Me.ScalarField)
        End Function

#End Region

    End Class

End Namespace

