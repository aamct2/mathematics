
Imports System.Collections.Generic

Namespace Algebra

    ''' <summary>
    ''' Represents a finite magma with elements of type T.
    ''' </summary>
    ''' <typeparam name="T">The Type of element in the magma.</typeparam>
    ''' <remarks></remarks>
    Public Class FiniteMagma(Of T As {Class, New, IEquatable(Of T)})
        Private mySet As FiniteSet(Of T)
        Private myOperation As FiniteBinaryOperation(Of T)
        Protected Friend magmaProperties As New Dictionary(Of String, Boolean)
        Private allSquareElements As FiniteSet(Of T)

#Region "  Constructors  "

        ''' <summary>
        ''' Creates a new finite magma
        ''' </summary>
        ''' <param name="newSet">The set for the magma.</param>
        ''' <param name="newOperation">The operation for the magma.</param>
        ''' <exception cref="DoesNotSatisfyPropertyException">Throws DoesNotSatisfyPropertyException if the codomain of 'newOperation' is not the set of the magma.</exception>
        ''' <remarks></remarks>
        Public Sub New(ByVal newSet As FiniteSet(Of T), ByVal newOperation As FiniteBinaryOperation(Of T))
            Me.mySet = newSet

            'Only need to check that the set equals the codomain of the operation.
            'Domain of the operation, by construction of FiniteBinaryOperation, will be (codomain X codomain)
            If newOperation.Codomain.Equals(Me.theSet) = False Then
                Throw New DoesNotSatisfyPropertyException("The codomain of the new operation is not the set of the magma.")
            End If
            Me.myOperation = newOperation
        End Sub

#End Region

#Region "  Properties  "

        ''' <summary>
        ''' Returns the set of the structure.
        ''' </summary>
        ''' <value>The set of the structure.</value>
        ''' <returns>Returns the set of the structure.</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property theSet() As FiniteSet(Of T)
            Get
                Return Me.mySet
            End Get
        End Property

        ''' <summary>
        ''' Returns the operation of the structure.
        ''' </summary>
        ''' <value>The operation of the structure.</value>
        ''' <returns>Returns the operation of the structure.</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Operation() As FiniteBinaryOperation(Of T)
            Get
                Return Me.myOperation
            End Get
        End Property

#End Region

#Region "  Functions  "

        ''' <summary>
        ''' Returns the result of applying the operation of the structure to a Tuple (pair) of elements.
        ''' </summary>
        ''' <param name="input">The Tuple (pair) to apply the operation to.</param>
        ''' <returns>The output of the operation applied to the given input.</returns>
        ''' <remarks></remarks>
        Public Function ApplyOperation(ByVal input As Tuple) As T
            Return Me.Operation.ApplyMap(input)
        End Function

        ''' <summary>
        ''' Returns the set of all elements of the structure 'a' such that there exists a 'b' where 'a' = 'b' * 'b'.
        ''' </summary>
        ''' <returns>The finite set of all square elements.</returns>
        ''' <remarks></remarks>
        Public Function SetOfSquareElements() As FiniteSet(Of T)
            If Me.magmaProperties.ContainsKey("all square elements") = False Then
                Dim index As Integer
                Dim curElem As T
                Dim curTup As New Tuple(2)
                Dim result As New FiniteSet(Of T)

                For index = 0 To Me.theSet.Cardinality - 1
                    curElem = Me.theSet.Element(index)
                    curTup.Element(0) = curElem
                    curTup.Element(1) = curElem
                    result.AddElement(Me.Operation.ApplyMap(curTup))
                Next index

                Me.allSquareElements = result
                Me.magmaProperties.Add("all square elements", True)
            End If

            Return Me.allSquareElements.Clone()
        End Function

#End Region

    End Class

End Namespace
