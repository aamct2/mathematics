
Imports System.Collections.Generic

Namespace Algebra

    ''' <summary>
    ''' Represents a finite semigroup with elements of type T.
    ''' </summary>
    ''' <typeparam name="T">The Type of element in the semigroup.</typeparam>
    ''' <remarks></remarks>
    Public Class FiniteSemigroup(Of T As {Class, New, IEquatable(Of T)})
        Inherits FiniteMagma(Of T)

        Private semigroupProperties As New Dictionary(Of String, Boolean)

#Region "  Constructors  "

        ''' <summary>
        ''' Creates a new finite semigroup.
        ''' </summary>
        ''' <param name="newSet">The set for the semigroup.</param>
        ''' <param name="newOperation">The operation for the semigroup.</param>
        ''' <exception cref="DoesNotSatisfyPropertyException">Throws DoesNotSatisfyPropertyException if 'newOperation' is not associative.</exception>
        ''' <remarks></remarks>
        Public Sub New(ByVal newSet As FiniteSet(Of T), ByVal newOperation As FiniteBinaryOperation(Of T))
            MyBase.New(newSet, newOperation)

            If newOperation.IsAssociative = False Then
                Throw New DoesNotSatisfyPropertyException("The new operation is not associative.")
            End If
        End Sub

#End Region

#Region "  Methods  "

        ''' <summary>
        ''' Returns whether or not the semigroup is also a band (a semigroup that is idempotent)
        ''' </summary>
        ''' <returns>Returns <c>True</c> if the semigroup is also a band, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Function IsBand() As Boolean
            If Me.semigroupProperties.ContainsKey("band") = False Then
                Me.semigroupProperties.Add("band", Me.Operation.IsIdempotent())
            End If

            Return Me.semigroupProperties.Item("band")
        End Function

        ''' <summary>
        ''' Determines whether a given finite set and operation will form a finite semigroup.
        ''' </summary>
        ''' <param name="testSet">The finite set to test.</param>
        ''' <param name="testOperation">The operation to test.</param>
        ''' <returns>Returns <c>True</c> if the given set and operation will form a semigroup, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Shared Function IsSemigroup(ByVal testSet As FiniteSet(Of T), ByVal testOperation As FiniteBinaryOperation(Of T)) As Boolean
            Try
                Dim tempSemigroup As New FiniteSemigroup(Of T)(testSet, testOperation)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Determines whether a given finite magma will form a finite semigroup.
        ''' </summary>
        ''' <param name="testMagma">The magma to test.</param>
        ''' <returns>Returns <c>True</c> if the given magma is also a semigroup, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Shared Function IsSemigroup(ByVal testMagma As FiniteMagma(Of T)) As Boolean
            Return testMagma.Operation.IsAssociative
        End Function

        ''' <summary>
        ''' Returns whether or not the semigroup is also a semilattice (a semigroup that is both a band and commutative)
        ''' </summary>
        ''' <returns>Returns <c>True</c> if the semigroup is also a semilattice, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Function IsSemilattice() As Boolean
            If Me.semigroupProperties.ContainsKey("semilattice") = False Then
                Me.semigroupProperties.Add("semilattice", Me.IsBand And Me.Operation.IsCommutative)
            End If

            Return Me.semigroupProperties.Item("semilattice")
        End Function

#End Region

    End Class

End Namespace
