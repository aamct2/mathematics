
Imports System.Collections.Generic

Namespace Algebra

    ''' <summary>
    ''' Represents a finite field with elements of type T.
    ''' </summary>
    ''' <typeparam name="T">The Type of element in the field.</typeparam>
    ''' <remarks></remarks>
    Public Class FiniteField(Of T As {Class, New, IEquatable(Of T)})
        Inherits FiniteCommutativeRing(Of T)

        Private myMultiplicativeIdentity As T
        Private underlyingMultiplicationAGrp As FiniteAbelianGroup(Of T)
        Private myCharacteristic As Integer
        Private fieldProperties As New Dictionary(Of String, Boolean)

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

            'Customize the Multiplication Operation for the underlying Abelian Group
            Dim customCodomain As FiniteSet(Of T) = newSet.Clone
            customCodomain.DeleteElement(customCodomain.IndexOf(newAddition.Identity))
            Dim customMult As New FiniteBinaryOperation(Of T)(customCodomain, newMultiplication.theRelation)

            'Check to make sure multiplication forms an abelian group without the zero
            Try
                underlyingMultiplicationAGrp = New FiniteAbelianGroup(Of T)(customCodomain, customMult)
            Catch Ex As Exception
                Throw New DoesNotSatisfyPropertyException("The new multiplication operation does not form an abelian group with the nonzero elements of the Field.", Ex)
            End Try

            Me.myMultiplicativeIdentity = customMult.Identity
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
        ''' Returns the characteristic of the field.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>The characteristic is the smallest number of times one must add the multiplicative identity to get the additive identity.</remarks>
        Public Function Characteristic() As Integer
            If (Me.fieldProperties.ContainsKey("characteristic") = False) Then
                Dim theChar As Integer = 2
                Dim curValue As T
                Dim curTup As New Tuple(2)

                curTup.Element(0) = Me.MultiplicativeIdentity
                curTup.Element(1) = Me.MultiplicativeIdentity
                curValue = Me.ApplyAddition(curTup)
                Do Until (curValue.Equals(Me.AdditiveIdentity))
                    curTup.Element(1) = Me.MultiplicativeIdentity
                    curValue = Me.ApplyAddition(curTup)
                    theChar += 1
                Loop

                myCharacteristic = theChar
                Me.fieldProperties.Add("characteristic", True)
            End If

            Return myCharacteristic
        End Function

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
