
Imports System.Collections.Generic

Namespace Algebra

    ''' <summary>
    ''' Represents a finite commutative ring with elements of type T.
    ''' </summary>
    ''' <typeparam name="T">The Type of element in the commutative ring.</typeparam>
    ''' <remarks></remarks>
    Public Class FiniteCommutativeRing(Of T As {Class, New, IEquatable(Of T)})
        Inherits FiniteRing(Of T)

        Private commutativeRingProperties As New Dictionary(Of String, Boolean)

#Region "  Constructors  "

        ''' <summary>
        ''' Creates a new finite commutative ring.
        ''' </summary>
        ''' <param name="newSet">The set for the commutative ring.</param>
        ''' <param name="newAddition">The addition operation for the commutative ring.</param>
        ''' <param name="newMultiplication">The multiplication operation for the commutative ring.</param>
        ''' <exception cref="DoesNotSatisfyPropertyException">Throws DoesNotSatisfyPropertyException if 'newOperation' does not satisfy all the commutative ring axioms.</exception>
        ''' <remarks></remarks>
        Public Sub New(ByVal newSet As FiniteSet(Of T), ByVal newAddition As FiniteBinaryOperation(Of T), ByVal newMultiplication As FiniteBinaryOperation(Of T))
            MyBase.New(newSet, newAddition, newMultiplication)

            If newMultiplication.IsCommutative = False Then
                Throw New DoesNotSatisfyPropertyException("The new multiplication is not commutative.")
            End If
        End Sub

#End Region

#Region "  Methods  "

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsIntegralDomain() As Boolean
            If Me.commutativeRingProperties.ContainsKey("integral domain") = False Then
                If Me.IsCommutative = False Then
                    Me.commutativeRingProperties.Add("integral domain", False)
                    Return False
                End If

                If Me.SetOfAllZeroDivisors.Cardinality > 0 Then
                    Me.commutativeRingProperties.Add("integral domain", False)
                    Return False
                End If

                If Me.theSet.Cardinality = 1 Then
                    Me.commutativeRingProperties.Add("integral domain", False)
                    Return False
                End If

                If Me.HasMultiplicativeInverse = False Then
                    Me.commutativeRingProperties.Add("integral domain", False)
                    Return False
                End If

                Me.commutativeRingProperties.Add("integral domain", True)
            End If

            Return Me.commutativeRingProperties.Item("integral domain")
        End Function

        ''' <summary>
        ''' Determines whether a set is a prime ideal of this commutative ring.
        ''' </summary>
        ''' <param name="testIdeal"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsPrimeIdeal(ByVal testIdeal As FiniteSet(Of T)) As Boolean
            'In commutative rings, left and right ideals conincide, so just check one to see if it's two-sided
            If Me.IsLeftIdeal(testIdeal) = False Then
                Return False
            End If

            'Check that it is a proper subset
            If testIdeal.IsProperSubsetOf(testIdeal) = False Then
                Return False
            End If

            'If a,b are in Ring and ab is in Ideal, then a or b is in Ideal
            Dim index1 As Integer
            Dim index2 As Integer
            Dim tup As New Tuple(2)

            For index1 = 0 To Me.theSet.Cardinality - 1
                tup.Element(0) = Me.theSet.Element(index1)

                For index2 = 0 To Me.theSet.Cardinality - 1
                    tup.Element(1) = Me.theSet.Element(index2)

                    If testIdeal.Contains(Me.ApplyMultiplication(tup)) = True Then
                        If testIdeal.Contains(Me.theSet.Element(index1)) = False And testIdeal.Contains(Me.theSet.Element(index2)) = False Then
                            Return False
                        End If
                    End If
                Next index2
            Next index1

            Return True
        End Function

        ''' <summary>
        ''' Returns the radical of an ideal of the commutative ring.
        ''' </summary>
        ''' <param name="ideal">The ideal for which to create its radical.</param>
        ''' <exception cref="NotIdealException">Throws NotIdealException if parameter 'ideal' is not an idea of the commutative ring.</exception>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Radical(ByVal ideal As FiniteSet(Of T)) As FiniteSet(Of T)
            If Me.IsLeftIdeal(ideal) = False Then
                Throw New NotIdealException("The parameter 'ideal' is not an ideal of the commutative ring.")
            End If

            Dim index As Integer
            Dim newSet As New FiniteSet(Of T)
            Dim tup As New Tuple(2)
            Dim curT As T

            For index = 0 To Me.theSet.Cardinality - 1
                curT = Me.theSet.Element(index)

                If ideal.Contains(curT) = True Then
                    newSet.AddElement(curT)
                Else
                    tup.Element(0) = Me.theSet.Element(index)
                    tup.Element(1) = Me.theSet.Element(index)
                    curT = Me.ApplyMultiplication(tup)
                    Do While curT.Equals(Me.theSet.Element(index)) = False
                        If ideal.Contains(curT) = True Then
                            newSet.AddElement(curT)
                            Exit Do
                        End If
                    Loop
                End If
            Next index

            Return newSet
        End Function

#End Region

    End Class

End Namespace
