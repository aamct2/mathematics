
Imports System.Collections.Generic

Namespace Algebra

    ''' <summary>
    ''' Represents a finite abelian group with elements of type T.
    ''' </summary>
    ''' <typeparam name="T">The Type of element in the abelian group.</typeparam>
    ''' <remarks></remarks>
    Public Class FiniteAbelianGroup(Of T As {Class, New, IEquatable(Of T)})
        Inherits FiniteGroup(Of T)

#Region "  Constructors  "

        ''' <summary>
        ''' Creates a new finite abelian group.
        ''' </summary>
        ''' <param name="newSet">The set for the abelian group.</param>
        ''' <param name="newOperation">The operation for the abelian group.</param>
        ''' <exception cref="DoesNotSatisfyPropertyException">Throws DoesNotSatisfyPropertyException if 'newOperation' does not have an identity element, does not have inverses for every element, is not commutative, or is not associative.</exception>
        ''' <remarks></remarks>
        Public Sub New(ByVal newSet As FiniteSet(Of T), ByVal newOperation As FiniteBinaryOperation(Of T))
            MyBase.New(newSet, newOperation)

            If newOperation.IsCommutative = False Then
                Throw New DoesNotSatisfyPropertyException("The new operation is not commutative.")
            End If
        End Sub

        ''' <summary>
        ''' Creates a new finite abelian group and stores any initially known properties of the group.
        ''' </summary>
        ''' <param name="newSet">The set for the abelian group.</param>
        ''' <param name="newOperation">The operation for the abelian group.</param>
        ''' <param name="knownProperties">Any known properties about the group already.</param>
        ''' <exception cref="DoesNotSatisfyPropertyException">Throws DoesNotSatisfyPropertyException if 'newOperation' does not have an identity element, does not have inverses for every element, is not commutative, or is not associative.</exception>
        ''' <remarks></remarks>
        Protected Friend Sub New(ByVal newSet As FiniteSet(Of T), ByVal newOperation As FiniteBinaryOperation(Of T), ByVal knownProperties As Dictionary(Of String, Boolean))
            MyBase.New(newSet, newOperation, knownProperties)

            If newOperation.IsCommutative = False Then
                Throw New DoesNotSatisfyPropertyException("The new operation is not commutative.")
            End If
        End Sub

#End Region

#Region "  Methods  "

        ''' <summary>
        ''' Determines whether a given finite set and operation will form a finite abelian group.
        ''' </summary>
        ''' <param name="testSet">The finite set to test.</param>
        ''' <param name="testOperation">The operation to test.</param>
        ''' <returns>Returns <c>True</c> if the given set and operation form an abelian group, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Shared Function IsAbelianGroup(ByVal testSet As FiniteSet(Of T), ByVal testOperation As FiniteBinaryOperation(Of T)) As Boolean
            Try
                Dim tempAbelianGroup As New FiniteAbelianGroup(Of T)(testSet, testOperation)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Determines whether a given finite group will form a finite abelian group.
        ''' </summary>
        ''' <param name="testGroup">The group to test.</param>
        ''' <returns>Returns <c>True</c> if the given group is also an abelian group, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Shared Function IsAbelianGroup(ByVal testGroup As FiniteGroup(Of T)) As Boolean
            Return testGroup.Operation.IsCommutative
        End Function

#End Region

    End Class

End Namespace
