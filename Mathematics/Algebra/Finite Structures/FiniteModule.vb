
Namespace Algebra

    ''' <summary>
    ''' Represents a finite left module with elements of type T and scalars of type Scalar.
    ''' </summary>
    ''' <typeparam name="T">The Type of element in the module.</typeparam>
    ''' <typeparam name="Scalar">The Type of scalar elements.</typeparam>
    ''' <remarks></remarks>
    Public Class FiniteLeftModule(Of T As {Class, New, IEquatable(Of T)}, Scalar As {Class, New, IEquatable(Of Scalar)})
        Private mySet As FiniteSet(Of T)
        Private myAddition As FiniteBinaryOperation(Of T)
        Private myLeftScalarMultiplication As FiniteLeftExternalBinaryOperation(Of Scalar, T)

        Private myAdditiveIdentity As T

        Private underlyingAGrp As FiniteAbelianGroup(Of T)
        Private myScalarRing As FiniteRing(Of Scalar)

#Region "  Constructors  "

        ''' <summary>
        ''' Creates a new finite left module.
        ''' </summary>
        ''' <param name="newSet">The set for the module.</param>
        ''' <param name="newAddition">The addition operation for the module.</param>
        ''' <param name="newMultiplication">The multiplication operation for the module.</param>
        ''' <param name="newScalarRing">The ring of scalars for the module (ring must have a multiplicative identity).</param>
        ''' <exception cref="DoesNotSatisfyPropertyException">Throws DoesNotSatisfyPropertyException if 'newOperation' does not satisfy all the left module axioms.</exception>
        ''' <remarks></remarks>
        Public Sub New(ByVal newSet As FiniteSet(Of T), ByVal newAddition As FiniteBinaryOperation(Of T), ByVal newMultiplication As FiniteLeftExternalBinaryOperation(Of Scalar, T), ByVal newScalarRing As FiniteRing(Of Scalar))
            Me.mySet = newSet

            'Check to make sure addition forms an abelian group
            Try
                underlyingAGrp = New FiniteAbelianGroup(Of T)(Me.theSet, newAddition)
            Catch Ex As Exception
                Throw New DoesNotSatisfyPropertyException("The new addition operation does not form an abelian group with the module's set.", Ex)
            End Try
            Me.myAddition = newAddition
            Me.myLeftScalarMultiplication = newMultiplication
            Me.myScalarRing = newScalarRing

            'Check to make sure the distributive properties hold
            Dim sIndex1 As Integer  'Scalar Indices
            Dim sIndex2 As Integer
            Dim tIndex1 As Integer  'Module's Set Indices
            Dim tIndex2 As Integer
            Dim tup1 As New Tuple(2)
            Dim tup2 As New Tuple(2)
            Dim tup3 As New Tuple(2)
            Dim tup4 As New Tuple(2)
            Dim tup5 As New Tuple(2)
            Dim lhs As T
            Dim rhs As T

            For sIndex1 = 0 To Me.scalarRing.theSet.Cardinality - 1
                For sIndex2 = 0 To Me.scalarRing.theSet.Cardinality - 1
                    For tIndex1 = 0 To Me.theSet.Cardinality - 1
                        For tIndex2 = 0 To Me.theSet.Cardinality - 1
                            'Distributivity 1
                            'a * (x + y) = (a * x) + (a * y)
                            tup1.Element(0) = Me.theSet.Element(tIndex1)
                            tup1.Element(1) = Me.theSet.Element(tIndex2)
                            tup2.Element(0) = Me.scalarRing.theSet.Element(sIndex1)
                            tup2.Element(1) = Me.ApplyAddition(tup1)
                            lhs = Me.ApplyLeftScalarMultiplication(tup2)

                            tup3.Element(0) = Me.scalarRing.theSet.Element(sIndex1)
                            tup3.Element(1) = Me.theSet.Element(tIndex1)
                            tup4.Element(0) = Me.scalarRing.theSet.Element(sIndex1)
                            tup4.Element(1) = Me.theSet.Element(tIndex2)
                            tup5.Element(0) = Me.ApplyLeftScalarMultiplication(tup3)
                            tup5.Element(1) = Me.ApplyLeftScalarMultiplication(tup4)
                            rhs = Me.ApplyAddition(tup5)

                            If lhs.Equals(rhs) = False Then
                                Throw New DoesNotSatisfyPropertyException("The distributive property [a * (x + y) = (a * x) + (a * y)] does not hold for: " & _
                                    "a = " & Me.ScalarRing.theSet.Element(sIndex1).ToString & _
                                    ", x = " & Me.theSet.Element(tIndex1).ToString & _
                                    ", y = " & Me.theSet.Element(tIndex2).ToString & ".")
                            End If

                            'Distributivity 2
                            '(a + b) * x = (a * x) + (b * x)
                            tup1.Element(0) = Me.scalarRing.theSet.Element(sIndex1)
                            tup1.Element(1) = Me.scalarRing.theSet.Element(sIndex2)
                            tup2.Element(0) = Me.scalarRing.ApplyAddition(tup1)
                            tup2.Element(1) = Me.theSet.Element(tIndex1)
                            lhs = Me.ApplyLeftScalarMultiplication(tup2)

                            tup3.Element(0) = Me.scalarRing.theSet.Element(sIndex1)
                            tup3.Element(1) = Me.theSet.Element(tIndex1)
                            tup4.Element(0) = Me.scalarRing.theSet.Element(sIndex2)
                            tup4.Element(1) = Me.theSet.Element(tIndex1)
                            tup5.Element(0) = Me.ApplyLeftScalarMultiplication(tup3)
                            tup5.Element(1) = Me.ApplyLeftScalarMultiplication(tup4)
                            rhs = Me.ApplyAddition(tup5)

                            If lhs.Equals(rhs) = False Then
                                Throw New DoesNotSatisfyPropertyException("The distributive property [(a + b) * x = (a * x) + (b * x)] does not hold for: " & _
                                    "a = " & Me.ScalarRing.theSet.Element(sIndex1).ToString & _
                                    ", b = " & Me.ScalarRing.theSet.Element(sIndex2).ToString & _
                                    ", x = " & Me.theSet.Element(tIndex1).ToString & ".")
                            End If

                            'Distributivity 3
                            '(ab) * x = a * (b * x)
                            tup1.Element(0) = Me.scalarRing.theSet.Element(sIndex1)
                            tup1.Element(1) = Me.scalarRing.theSet.Element(sIndex2)
                            tup2.Element(0) = Me.ScalarRing.ApplyMultiplication(tup1)
                            tup2.Element(1) = Me.theSet.Element(tIndex1)
                            lhs = Me.ApplyLeftScalarMultiplication(tup2)

                            tup3.Element(0) = Me.scalarRing.theSet.Element(sIndex2)
                            tup3.Element(1) = Me.theSet.Element(tIndex1)
                            tup4.Element(0) = Me.scalarRing.theSet.Element(sIndex1)
                            tup4.Element(1) = Me.ApplyLeftScalarMultiplication(tup3)
                            rhs = Me.ApplyLeftScalarMultiplication(tup4)

                            If lhs.Equals(rhs) = False Then
                                Throw New DoesNotSatisfyPropertyException("The distributive property [(ab) * x = a * (b * x)] does not hold for: " & _
                                    "a = " & Me.ScalarRing.theSet.Element(sIndex1).ToString & _
                                    ", b = " & Me.ScalarRing.theSet.Element(sIndex2).ToString & _
                                    ", x = " & Me.theSet.Element(tIndex1).ToString & ".")
                            End If
                        Next tIndex2
                    Next tIndex1
                Next sIndex2
            Next sIndex1

            If Me.scalarRing.MultiplicationOperation.HasIdentity = False Then
                Throw New DoesNotSatisfyPropertyException("The scalar ring does not have a multiplicative identity.")
            End If

            If Me.ScalarRing.MultiplicationOperation.Identity.Equals(Me.LeftScalarMultiplicationOperation.LeftIdentity) = False Then
                Throw New DoesNotSatisfyPropertyException("The scalar ring's multiplicative identity is not the multiplicative identity of the module's multiplication.")
            End If

            Me.myAdditiveIdentity = Me.AdditionOperation.Identity
        End Sub

#End Region

#Region "  Properties  "

        ''' <summary>
        ''' Returns the set of the structure.
        ''' </summary>
        ''' <value>The set of the structure.</value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property theSet() As FiniteSet(Of T)
            Get
                Return Me.mySet
            End Get
        End Property

        ''' <summary>
        ''' Returns the addition operation of the structure.
        ''' </summary>
        ''' <value>The addition operation of the structure.</value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property AdditionOperation() As FiniteBinaryOperation(Of T)
            Get
                Return Me.myAddition
            End Get
        End Property

        ''' <summary>
        ''' Returns the additive identity of the structure (the identity of the addition operation).
        ''' </summary>
        ''' <value>The additive identity of the structure.</value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property AdditiveIdentity() As T
            Get
                Return Me.myAdditiveIdentity
            End Get
        End Property

        ''' <summary>
        ''' Returns the multiplication operation of the structure.
        ''' </summary>
        ''' <value>The multiplication operation of the structure.</value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property LeftScalarMultiplicationOperation() As FiniteLeftExternalBinaryOperation(Of Scalar, T)
            Get
                Return Me.myLeftScalarMultiplication
            End Get
        End Property

        ''' <summary>
        ''' Returns the underlying abelian group.
        ''' </summary>
        ''' <value></value>
        ''' <returns>The underlying abelian group.</returns>
        ''' <remarks>The underlying abelian group is formed by taking the set together with the addition operation.</remarks>
        Public ReadOnly Property UnderlyingAbelianGroup() As FiniteAbelianGroup(Of T)
            Get
                Return Me.underlyingAGrp
            End Get
        End Property

        ''' <summary>
        ''' Returns the scalar ring associated with the module.
        ''' </summary>
        ''' <value>The scalar ring associated with the module.</value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ScalarRing() As FiniteRing(Of Scalar)
            Get
                Return Me.myScalarRing
            End Get
        End Property

#End Region

#Region "  Functions  "

        ''' <summary>
        ''' Returns the result of applying the addition operation of the structure to a Tuple (pair) of elements.
        ''' </summary>
        ''' <param name="input">The Tuple (pair) to apply the addition operation to. Both elements must be from the underlying abelian group.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ApplyAddition(ByVal input As Tuple) As T
            Return Me.AdditionOperation.ApplyMap(input)
        End Function

        ''' <summary>
        ''' Returns the result of applying the multiplication operation of the structure to a Tuple (pair) of elements.
        ''' </summary>
        ''' <param name="input">The Tuple (pair) to apply the multiplication operation to. The first element of the tuple must be from the ring of scalars, the second element must be from the underlying abelian group.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ApplyLeftScalarMultiplication(ByVal input As Tuple) As T
            Return Me.LeftScalarMultiplicationOperation.ApplyMap(input)
        End Function

#End Region

    End Class

End Namespace
