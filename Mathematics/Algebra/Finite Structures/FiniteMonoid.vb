
Namespace Algebra

    ''' <summary>
    ''' Represents a finite monoid with elements of type T.
    ''' </summary>
    ''' <typeparam name="T">The Type of element in the monoid.</typeparam>
    ''' <remarks></remarks>
    Public Class FiniteMonoid(Of T As {Class, New, IEquatable(Of T)})
        Inherits FiniteSemigroup(Of T)

        Private myIdentity As T

#Region "  Constructors  "

        ''' <summary>
        ''' Creates a new finite monoid.
        ''' </summary>
        ''' <param name="newSet">The set for the monoid.</param>
        ''' <param name="newOperation">The operation for the monoid.</param>
        ''' <exception cref="DoesNotSatisfyPropertyException">Throws DoesNotSatisfyPropertyException if 'newOperation' does not have an identity element or is not associative.</exception>
        ''' <remarks></remarks>
        Public Sub New(ByVal newSet As FiniteSet(Of T), ByVal newOperation As FiniteBinaryOperation(Of T))
            MyBase.New(newSet, newOperation)

            If newOperation.HasIdentity = False Then
                Throw New DoesNotSatisfyPropertyException("The new operation does not have an identity element.")
            End If

            Me.myIdentity = Me.Operation.Identity
        End Sub

#End Region

#Region "  Properties  "

        ''' <summary>
        ''' Returns the identity element of the structure (the identity of the operation).
        ''' </summary>
        ''' <value>The identity element of the structure.</value>
        ''' <returns>Returns the identity element of the structure.</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property IdentityElement() As T
            Get
                Return Me.myIdentity
            End Get
        End Property

#End Region

#Region "  Methods  "

        ''' <summary>
        ''' Determines whether a function is a homomorphism from this monoid to another monoid.
        ''' </summary>
        ''' <typeparam name="G">The Type of elements in the other monoid.</typeparam>
        ''' <param name="codomain">The other monoid that serves as the codomain for the function.</param>
        ''' <param name="testFunction">The function to test.</param>
        ''' <returns>Returns <c>True</c> if the function is a homomorphism between the monoids, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Function IsHomomorphism(Of G As {Class, New, IEquatable(Of G)})(ByVal codomain As FiniteMonoid(Of G), ByVal testFunction As FiniteFunction(Of T, G)) As Boolean
            Dim index1 As Integer
            Dim index2 As Integer
            Dim tup1 As New Tuple(2)
            Dim tup2 As New Tuple(2)
            Dim lhs As G
            Dim rhs As G

            If testFunction.Codomain.Equals(codomain.theSet) = False Then
                Throw New Exception("The codomain of of the parameter 'testFunction' is not the parameter 'codomain'.")
            ElseIf testFunction.Domain.Equals(Me.theSet) = False Then
                Throw New Exception("The domain of of the parameter 'testFunction' is not this group.")
            End If

            'Check that f(a + b) = f(a) * f(b)
            For index1 = 0 To Me.theSet.Cardinality - 1
                For index2 = 0 To Me.theSet.Cardinality - 1
                    tup1.Element(0) = Me.theSet.Element(index1)
                    tup1.Element(1) = Me.theSet.Element(index2)
                    lhs = testFunction.ApplyMap(Me.ApplyOperation(tup1))

                    tup2.Element(0) = testFunction.ApplyMap(Me.theSet.Element(index1))
                    tup2.Element(1) = testFunction.ApplyMap(Me.theSet.Element(index2))
                    rhs = codomain.ApplyOperation(tup2)

                    If lhs.Equals(rhs) = False Then
                        Return False
                    End If
                Next index2
            Next index1

            'Check that f(1) = 1'
            If testFunction.ApplyMap(Me.IdentityElement).Equals(codomain.IdentityElement) = False Then
                Return False
            End If

            Return True
        End Function

        ''' <summary>
        ''' Determines whether a function is a isomorphism from this monoid to another monoid. In other words, it's a bijective homomorphism.
        ''' </summary>
        ''' <typeparam name="G">The Type of elements in the other monoid.</typeparam>
        ''' <param name="codomain">The other monoid that serves as the codomain for the function.</param>
        ''' <param name="testFunction">The function to test.</param>
        ''' <returns>Returns <c>True</c> if the function is an isomorphism between the monoids, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Function IsIsomorphism(Of G As {Class, New, IEquatable(Of G)})(ByVal codomain As FiniteMonoid(Of G), ByVal testFunction As FiniteFunction(Of T, G)) As Boolean
            If testFunction.Codomain.Equals(codomain.theSet) = False Then
                Throw New Exception("The codomain of of the parameter 'testFunction' is not the parameter 'codomain'.")
            ElseIf testFunction.Domain.Equals(Me.theSet) = False Then
                Throw New Exception("The domain of of the parameter 'testFunction' is not this group.")
            End If

            If testFunction.IsBijective = True And Me.IsHomomorphism(Of G)(codomain, testFunction) = True Then
                Return True
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' Determines whether a given finite set and operation will form a finite monoid.
        ''' </summary>
        ''' <param name="testSet">The finite set to test.</param>
        ''' <param name="testOperation">The operation to test.</param>
        ''' <returns>Return <c>True</c> if the given set and operation form a monoid, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Shared Function IsMonoid(ByVal testSet As FiniteSet(Of T), ByVal testOperation As FiniteBinaryOperation(Of T)) As Boolean
            Try
                Dim tempMonoid As New FiniteMonoid(Of T)(testSet, testOperation)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Determines whether a given finite semigroup will form a finite monoid.
        ''' </summary>
        ''' <param name="testSemigroup">The semigroup to test.</param>
        ''' <returns>Returns <c>True</c> if the given semigroup is also a monoid, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Shared Function IsMonoid(ByVal testSemigroup As FiniteSemigroup(Of T)) As Boolean
            Return testSemigroup.Operation.HasIdentity
        End Function

#End Region

    End Class

End Namespace
