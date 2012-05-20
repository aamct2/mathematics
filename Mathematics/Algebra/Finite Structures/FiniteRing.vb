
Imports System.Collections.Generic
Imports System.Threading.Tasks

Namespace Algebra

    ''' <summary>
    ''' Represents a finite ring with elements of type T.
    ''' </summary>
    ''' <typeparam name="T">The Type of element in the ring.</typeparam>
    ''' <remarks></remarks>
    Public Class FiniteRing(Of T As {Class, New, IEquatable(Of T)})
        Implements IEquatable(Of FiniteRing(Of T))

        Private mySet As FiniteSet(Of T)
        Private myAddition As FiniteBinaryOperation(Of T)
        Private myMultiplication As FiniteBinaryOperation(Of T)
        Private underlyingAGrp As FiniteAbelianGroup(Of T)
        Private underlyingSGrp As FiniteSemigroup(Of T)
        Private myAdditiveIdentity As T

        Private allTwoSidedIdeals As FiniteSet(Of FiniteSet(Of T))

        Private ringProperties As New Dictionary(Of String, Boolean)

#Region "  Constructors  "

        ''' <summary>
        ''' Creates a new finite ring.
        ''' </summary>
        ''' <param name="newSet">The set for the ring.</param>
        ''' <param name="newAddition">The addition operation for the ring.</param>
        ''' <param name="newMultiplication">The multiplication operation for the ring.</param>
        ''' <exception cref="DoesNotSatisfyPropertyException">Throws DoesNotSatisfyPropertyException if 'newOperation' does not satisfy all the ring axioms.</exception>
        ''' <remarks></remarks>
        Public Sub New(ByVal newSet As FiniteSet(Of T), ByVal newAddition As FiniteBinaryOperation(Of T), ByVal newMultiplication As FiniteBinaryOperation(Of T))
            Me.mySet = newSet

            'Check to make sure addition forms an abelian group
            Try
                underlyingAGrp = New FiniteAbelianGroup(Of T)(Me.theSet, newAddition)
            Catch Ex As Exception
                Throw New DoesNotSatisfyPropertyException("The new addition operation does not form an abelian group with the ring's set.", Ex)
            End Try
            Me.myAddition = newAddition

            'Check to make sure multiplication forms a semigroup
            Try
                underlyingSGrp = New FiniteSemigroup(Of T)(Me.theSet, newMultiplication)
            Catch Ex As Exception
                Throw New DoesNotSatisfyPropertyException("The new multiplication operation does not form a semigroup with the ring's set.", Ex)
            End Try
            Me.myMultiplication = newMultiplication

            'Check to make sure the distributive properties hold
            Dim index1 As Integer
            Dim index2 As Integer
            Dim index3 As Integer
            Dim tup1 As New Tuple(2)
            Dim tup2 As New Tuple(2)
            Dim tup3 As New Tuple(2)
            Dim tup4 As New Tuple(2)
            Dim tup5 As New Tuple(2)
            Dim lhs As T
            Dim rhs As T

            For index1 = 0 To Me.theSet.Cardinality - 1
                For index2 = 0 To Me.theSet.Cardinality - 1
                    For index3 = 0 To Me.theSet.Cardinality - 1
                        'Distributivity 1
                        'a * (b + c) = (a * b) + (a * c)
                        tup1.Element(0) = Me.theSet.Element(index2)
                        tup1.Element(1) = Me.theSet.Element(index3)
                        tup2.Element(0) = Me.theSet.Element(index1)
                        tup2.Element(1) = Me.ApplyAddition(tup1)
                        lhs = Me.ApplyMultiplication(tup2)

                        tup3.Element(0) = Me.theSet.Element(index1)
                        tup3.Element(1) = Me.theSet.Element(index2)
                        tup4.Element(0) = Me.theSet.Element(index1)
                        tup4.Element(1) = Me.theSet.Element(index3)
                        tup5.Element(0) = Me.ApplyMultiplication(tup3)
                        tup5.Element(1) = Me.ApplyMultiplication(tup4)
                        rhs = Me.ApplyAddition(tup5)

                        If lhs.Equals(rhs) = False Then
                            Throw New DoesNotSatisfyPropertyException("The distributive property [a * (b + c) = (a * b) + (a * c)] does not hold for: " & _
                                "a = " & Me.theSet.Element(index1).ToString() & _
                                "b = " & Me.theSet.Element(index2).ToString() & _
                                "c = " & Me.theSet.Element(index3).ToString() & ".")
                        End If

                        'Distributivity 2
                        '(a + b) * c = (a * c) + (b * c)
                        tup1.Element(0) = Me.theSet.Element(index1)
                        tup1.Element(1) = Me.theSet.Element(index2)
                        tup2.Element(0) = Me.ApplyAddition(tup1)
                        tup2.Element(1) = Me.theSet.Element(index3)
                        lhs = Me.ApplyMultiplication(tup2)

                        tup3.Element(0) = Me.theSet.Element(index1)
                        tup3.Element(1) = Me.theSet.Element(index3)
                        tup4.Element(0) = Me.theSet.Element(index2)
                        tup4.Element(1) = Me.theSet.Element(index3)
                        tup5.Element(0) = Me.ApplyMultiplication(tup3)
                        tup5.Element(1) = Me.ApplyMultiplication(tup4)
                        rhs = Me.ApplyAddition(tup5)

                        If lhs.Equals(rhs) = False Then
                            Throw New DoesNotSatisfyPropertyException("The distributive property [(a + b) * c = (a * c) + (b * c)] does not hold for: " & _
                                "a = " & Me.theSet.Element(index1).ToString() & _
                                "b = " & Me.theSet.Element(index2).ToString() & _
                                "c = " & Me.theSet.Element(index3).ToString() & ".")
                        End If
                    Next index3
                Next index2
            Next index1

            Me.myAdditiveIdentity = Me.AdditionOperation.Identity
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
        Public ReadOnly Property MultiplicationOperation() As FiniteBinaryOperation(Of T)
            Get
                Return Me.myMultiplication
            End Get
        End Property

        ''' <summary>
        ''' Returns the underlying abelian group.
        ''' </summary>
        ''' <value>The underlying abelian group.</value>
        ''' <returns></returns>
        ''' <remarks>The underlying abelian group is formed by taking the set together with the addition operation.</remarks>
        Public ReadOnly Property UnderlyingAbelianGroup() As FiniteAbelianGroup(Of T)
            Get
                Return Me.underlyingAGrp
            End Get
        End Property

        ''' <summary>
        ''' Returns the underlying semigroup.
        ''' </summary>
        ''' <value>The underlying semigroup.</value>
        ''' <returns></returns>
        ''' <remarks>The underlying semigroup is formed by taking the set together with the multiplication operation.</remarks>
        Public ReadOnly Property UnderlyingSemigroup() As FiniteSemigroup(Of T)
            Get
                Return Me.underlyingSGrp
            End Get
        End Property

#End Region

#Region "  Functions  "

        ''' <summary>
        ''' Returns the result of applying the addition operation of the structure to a <c>Tuple</c> (pair) of elements.
        ''' </summary>
        ''' <param name="input">The <c>Tuple</c> (pair) to apply the addition operation to.</param>
        ''' <returns>Returns the result of applying the addition operation of the structure to a given <c>Tuple</c> (pair) of elements.</returns>
        ''' <remarks></remarks>
        Public Function ApplyAddition(ByVal input As Tuple) As T
            Return Me.AdditionOperation.ApplyMap(input)
        End Function

        ''' <summary>
        ''' Returns the result of applying the multiplication operation of the structure to a <c>Tuple</c> (pair) of elements.
        ''' </summary>
        ''' <param name="input">The <c>Tuple</c> (pair) to apply the multiplication operation to.</param>
        ''' <returns>Returns the result of applying the multiplication operation of the structure to a given <c>Tuple</c> (pair) of elements.</returns>
        ''' <remarks></remarks>
        Public Function ApplyMultiplication(ByVal input As Tuple) As T
            Return Me.MultiplicationOperation.ApplyMap(input)
        End Function

        ''' <summary>
        ''' Returns the center of the ring as a set. In other words, the set of all elements that commute with every other element in the ring (with respect to multiplication).
        ''' </summary>
        ''' <returns>Returns the center of the ring as a <c>FiniteSet</c>.</returns>
        ''' <remarks></remarks>
        Public Function Center() As FiniteSet(Of T)
            Dim newSet As New FiniteSet(Of T)

            Dim index1 As Integer
            Dim index2 As Integer
            Dim tup1 As New Tuple(2)
            Dim tup2 As New Tuple(2)
            Dim InCenter As Boolean

            For index1 = 0 To Me.theSet.Cardinality - 1
                tup1.Element(0) = Me.theSet.Element(index1)
                tup2.Element(1) = Me.theSet.Element(index1)
                InCenter = True

                For index2 = 0 To Me.theSet.Cardinality - 1
                    tup1.Element(1) = Me.theSet.Element(index2)
                    tup2.Element(0) = Me.theSet.Element(index2)

                    If Me.ApplyMultiplication(tup1).Equals(Me.ApplyMultiplication(tup2)) = False Then
                        InCenter = False
                        Exit For
                    End If
                Next index2

                If InCenter = True Then
                    newSet.AddElement(Me.theSet.Element(index1))
                End If
            Next index1

            Return newSet
        End Function

        ''' <summary>
        ''' Returns the center of the ring as a commutative ring.
        ''' </summary>
        ''' <returns>Returns the center of the ring as a commutative ring.</returns>
        ''' <remarks></remarks>
        Public Function CenterRing() As FiniteCommutativeRing(Of T)
            Return New FiniteCommutativeRing(Of T)(Me.theSet, Me.AdditionOperation, Me.MultiplicationOperation)
        End Function

        ''' <summary>
        ''' Returns the commutator of two elements, in a sense given an indication of how badly the operations fails to commute for two elements. [a,b] = ab - ba.
        ''' </summary>
        ''' <param name="lhs">The left-hand element in the operation.</param>
        ''' <param name="rhs">The right-hand element in the operation.</param>
        ''' <exception cref="NotMemberOfException">Throws NotMemberOfException if parameter 'lhs' or 'rhs' is not a member of the ring.</exception>
        ''' <returns>Returns the commutator of two given elements.</returns>
        ''' <remarks></remarks>
        Public Function Commutator(ByVal lhs As T, ByVal rhs As T) As T
            Dim tup1 As New Tuple(2)
            Dim tup2 As New Tuple(2)
            Dim tup3 As New Tuple(2)

            If Me.theSet.Contains(lhs) = False Or Me.theSet.Contains(rhs) = False Then
                Throw New NotMemberOfException("The parameter 'lhs' or 'rhs' is not a member of the ring.")
            End If

            tup1.Element(0) = lhs
            tup1.Element(1) = rhs
            tup2.Element(0) = rhs
            tup2.Element(1) = lhs
            tup3.Element(0) = Me.ApplyMultiplication(tup1)
            tup3.Element(1) = Me.AdditionOperation.InverseElement(Me.ApplyMultiplication(tup2))

            Return Me.ApplyAddition(tup3)
        End Function

        ''' <summary>
        ''' Returns the direct product of this ring with a given ring.
        ''' </summary>
        ''' <typeparam name="G">The Type of elements in the second ring.</typeparam>
        ''' <param name="otherRing">The other finite ring to take the direct product with.</param>
        ''' <returns>Returns the direct product of this ring with a given ring.</returns>
        ''' <remarks></remarks>
        Public Function DirectProduct(Of G As {Class, New, IEquatable(Of G)})(ByVal otherRing As FiniteRing(Of G)) As FiniteRing(Of Tuple)
            Dim newSet As FiniteSet(Of Tuple)

            newSet = Me.theSet.DirectProduct(Of G)(otherRing.theSet)

            Dim newAddMap As New DirectProductMap(Of G)(Me.AdditionOperation.theRelation, otherRing.AdditionOperation.theRelation)
            Dim newMultMap As New DirectProductMap(Of G)(Me.MultiplicationOperation.theRelation, otherRing.MultiplicationOperation.theRelation)

            Dim newAddOpProps As New Dictionary(Of String, Boolean)
            Dim newMultOpProps As New Dictionary(Of String, Boolean)

            newAddOpProps.Add("associativity", True)
            newAddOpProps.Add("inverses", True)
            newAddOpProps.Add("commutivity", True)

            newMultOpProps.Add("associativity", True)

            Dim newAddOp As New FiniteBinaryOperation(Of Tuple)(newSet, newAddMap, newAddOpProps)
            Dim newMultOp As New FiniteBinaryOperation(Of Tuple)(newSet, newMultMap, newMultOpProps)

            Return New FiniteRing(Of Tuple)(newSet, newAddOp, newMultOp)
        End Function

        ''' <summary>
        ''' Determines whether two rings are equal. In other words, if two rings have the same set and operations.
        ''' </summary>
        ''' <param name="other">The other finite ring to compare with.</param>
        ''' <returns>Returns <c>True</c> if this ring and another given ring are equal, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Shadows Function Equals(ByVal other As FiniteRing(Of T)) As Boolean Implements System.IEquatable(Of FiniteRing(Of T)).Equals
            If Me.theSet.Equals(other.theSet) = True And Me.AdditionOperation.Equals(other.AdditionOperation) = True And _
                Me.MultiplicationOperation.Equals(other.MultiplicationOperation) = True Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function HasMultiplicativeInverse() As Boolean

        End Function

        ''' <summary>
        ''' Determines whether the ring is a boolean ring. In other words, all its elements are idempotent (with respect to multiplication).
        ''' </summary>
        ''' <returns>Returns <c>True</c> if the ring is a boolean ring, <c>False</c> otherwise.</returns>
        ''' <remarks></remarks>
        Public Function IsBoolean() As Boolean
            If Me.ringProperties.ContainsKey("boolean") = False Then
                If Me.MultiplicationOperation.HasIdentity = True Then
                    Me.ringProperties.Add("boolean", Me.MultiplicationOperation.IsIdempotent)
                Else
                    Me.ringProperties.Add("boolean", False)
                End If
            End If

            Return Me.ringProperties.Item("boolean")
        End Function

        ''' <summary>
        ''' Determines whether the ring is a commutative ring. In other words, the multiplication operation is also commutative.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsCommutative() As Boolean
            If Me.ringProperties.ContainsKey("commutative") = False Then
                Me.ringProperties.Add("commutative", Me.MultiplicationOperation.IsCommutative)
            End If

            Return Me.ringProperties.Item("commutative")
        End Function

        ''' <summary>
        ''' Determines whether a function is a homomorphism from this ring to another ring.
        ''' </summary>
        ''' <typeparam name="G"></typeparam>
        ''' <param name="codomain"></param>
        ''' <param name="testFunction"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsHomomorphism(Of G As {Class, New, IEquatable(Of G)})(ByVal codomain As FiniteRing(Of G), ByVal testFunction As FiniteFunction(Of T, G)) As Boolean
            Dim index1 As Integer
            Dim index2 As Integer
            Dim tup1 As New Tuple(2)
            Dim tup2 As New Tuple(2)
            Dim lhs As G
            Dim rhs As G

            If testFunction.Codomain.Equals(codomain.theSet) = False Then
                Throw New Exception("The codomain of of the parameter 'testFunction' is not the parameter 'codomain'.")
            ElseIf testFunction.Domain.Equals(Me.theSet) = False Then
                Throw New Exception("The domain of of the parameter 'testFunction' is not this ring.")
            End If

            'Check to see that it satisfies the group homomorphism properties
            If Me.UnderlyingAbelianGroup.IsHomomorphism(Of G)(codomain.UnderlyingAbelianGroup, testFunction) = False Then
                Return False
            End If

            'Check to see that it satisfies the multiplication homomorphism properties
            For index1 = 0 To Me.theSet.Cardinality - 1
                For index2 = 0 To Me.theSet.Cardinality - 1
                    tup1.Element(0) = Me.theSet.Element(index1)
                    tup1.Element(1) = Me.theSet.Element(index2)
                    lhs = testFunction.ApplyMap(Me.ApplyMultiplication(tup1))

                    tup2.Element(0) = testFunction.ApplyMap(Me.theSet.Element(index1))
                    tup2.Element(1) = testFunction.ApplyMap(Me.theSet.Element(index2))
                    rhs = codomain.ApplyMultiplication(tup2)

                    If lhs.Equals(rhs) = False Then
                        Return False
                    End If
                Next index2
            Next index1

            Return True
        End Function

        ''' <summary>
        ''' Determines whether a function is a isomorphism from this ring to another ring. In other words, it's a bijective homomorphism.
        ''' </summary>
        ''' <typeparam name="G"></typeparam>
        ''' <param name="codomain"></param>
        ''' <param name="testFunction"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsIsomorphism(Of G As {Class, New, IEquatable(Of G)})(ByVal codomain As FiniteRing(Of G), ByVal testFunction As FiniteFunction(Of T, G)) As Boolean
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
        ''' Determines whether the ring satisfies the jacobian identity. For all x,y,z in the ring, xyz + zxy + yzx = 0.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsJacobian() As Boolean
            If Me.ringProperties.ContainsKey("jacobian") = False Then
                Dim index1 As Integer
                Dim index2 As Integer
                Dim index3 As Integer
                Dim tup1 As New Tuple(2)
                Dim tup2 As New Tuple(2)
                Dim x As T
                Dim y As T
                Dim z As T
                Dim elem1 As T
                Dim elem2 As T
                Dim elem3 As T

                For index1 = 0 To Me.theSet.Cardinality - 1
                    For index2 = 0 To Me.theSet.Cardinality - 1
                        For index3 = 0 To Me.theSet.Cardinality - 1
                            x = Me.theSet.Element(index1)
                            y = Me.theSet.Element(index2)
                            z = Me.theSet.Element(index3)

                            tup1.Element(0) = x
                            tup1.Element(1) = y
                            tup2.Element(0) = Me.ApplyMultiplication(tup1)
                            tup2.Element(1) = z
                            elem1 = Me.ApplyMultiplication(tup2)

                            tup1.Element(0) = z
                            tup1.Element(1) = x
                            tup2.Element(0) = Me.ApplyMultiplication(tup1)
                            tup2.Element(1) = y
                            elem2 = Me.ApplyMultiplication(tup2)

                            tup1.Element(0) = y
                            tup1.Element(1) = z
                            tup2.Element(0) = Me.ApplyMultiplication(tup1)
                            tup2.Element(1) = x
                            elem3 = Me.ApplyMultiplication(tup2)

                            tup1.Element(0) = elem1
                            tup1.Element(1) = elem2
                            tup2.Element(0) = Me.ApplyAddition(tup1)
                            tup2.Element(1) = elem3

                            If Me.AdditiveIdentity.Equals(Me.ApplyAddition(tup2)) = False Then
                                Me.ringProperties.Add("jacobian", False)
                                Return False
                            End If
                        Next index3
                    Next index2
                Next index1

                Me.ringProperties.Add("jacobian", True)
                Return True
            Else
                Return Me.ringProperties.Item("jacobian")
            End If
        End Function

        ''' <summary>
        ''' Determines whether a set is a left-ideal of this ring.
        ''' </summary>
        ''' <param name="testIdeal"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsLeftIdeal(ByVal testIdeal As FiniteSet(Of T)) As Boolean
            'Check to see that the testIdeal will form a group
            Try
                If FiniteGroup(Of T).IsGroup(testIdeal, Me.AdditionOperation.Restriction(testIdeal)) = False Then
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try

            'Check to see that it is a subgroup
            Dim idealGrp As New FiniteGroup(Of T)(testIdeal, Me.AdditionOperation.Restriction(testIdeal))

            If idealGrp.IsSubgroupOf(Me.UnderlyingAbelianGroup) = False Then
                Return False
            End If

            'Check to make sure:
            'rx is in Ideal, for all x in Ideal and r in Ring.
            Dim idealIndex As Integer
            Dim ringIndex As Integer
            Dim tup As New Tuple(2)

            For ringIndex = 0 To Me.theSet.Cardinality - 1
                tup.Element(0) = Me.theSet.Element(ringIndex)

                For idealIndex = 0 To testIdeal.Cardinality - 1
                    tup.Element(1) = testIdeal.Element(idealIndex)

                    If testIdeal.Contains(Me.ApplyMultiplication(tup)) = False Then
                        Return False
                    End If
                Next idealIndex
            Next ringIndex

            Return True
        End Function

        ''' <summary>
        ''' Determines whether a set is a right-ideal of this ring.
        ''' </summary>
        ''' <param name="testIdeal"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsRightIdeal(ByVal testIdeal As FiniteSet(Of T)) As Boolean
            'Check to see that the testIdeal will form a group
            Try
                If FiniteGroup(Of T).IsGroup(testIdeal, Me.AdditionOperation.Restriction(testIdeal)) = False Then
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try

            'Check to see that it is a subgroup
            Dim idealGrp As New FiniteGroup(Of T)(testIdeal, Me.AdditionOperation.Restriction(testIdeal))

            If idealGrp.IsSubgroupOf(Me.UnderlyingAbelianGroup) = False Then
                Return False
            End If

            'Check to make sure:
            'xr is in Ideal, for all x in Ideal and r in Ring.
            Dim idealIndex As Integer
            Dim ringIndex As Integer
            Dim tup As New Tuple(2)

            For ringIndex = 0 To Me.theSet.Cardinality - 1
                tup.Element(1) = Me.theSet.Element(ringIndex)

                For idealIndex = 0 To testIdeal.Cardinality - 1
                    tup.Element(0) = testIdeal.Element(idealIndex)

                    If testIdeal.Contains(Me.ApplyMultiplication(tup)) = False Then
                        Return False
                    End If
                Next idealIndex
            Next ringIndex

            Return True
        End Function

        ''' <summary>
        ''' Determines whether this ring is simple. In other words, it is a non-zero ring and it has no two-sided ideals besides the trivial ideal and itself.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsSimple() As Boolean
            If Me.ringProperties.ContainsKey("simple") = False Then
                'Check to see if this is the trivial ring
                If Me.theSet.Cardinality = 1 Then
                    Me.ringProperties.Add("simple", False)
                    Return False
                End If

                Dim allIdeals As FiniteSet(Of FiniteSet(Of T)) = Me.SetOfAllTwoSidedIdeals

                If allIdeals.Cardinality > 2 Then
                    Me.ringProperties.Add("simple", False)
                    Return False
                Else
                    Me.ringProperties.Add("simple", True)
                    Return True
                End If
            Else
                Return Me.ringProperties.Item("simple")
            End If
        End Function

        ''' <summary>
        ''' Determines whether this ring is a subring of another ring.
        ''' </summary>
        ''' <param name="superRing"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsSubringOf(ByVal superRing As FiniteRing(Of T)) As Boolean
            If Me.theSet.IsProperSubsetOf(superRing.theSet) = False Then Return False

            If Me.AdditionOperation.EquivalentMaps(superRing.AdditionOperation.theRelation, superRing.AdditionOperation.Domain, superRing.AdditionOperation.Codomain) = False Then
                Return False
            ElseIf Me.MultiplicationOperation.EquivalentMaps(superRing.MultiplicationOperation.theRelation, superRing.MultiplicationOperation.Domain, superRing.MultiplicationOperation.Codomain) = False Then
                Return False
            Else
                Return True
            End If
        End Function

        ''' <summary>
        ''' Determines whether a set is a two-sided ideal of this ring. In other words, that it is both a left- and right-ideal.
        ''' </summary>
        ''' <param name="testIdeal"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsTwoSidedIdeal(ByVal testIdeal As FiniteSet(Of T)) As Boolean
            If Me.IsLeftIdeal(testIdeal) = False Then
                Return False
            ElseIf Me.IsRightIdeal(testIdeal) = False Then
                Return False
            Else
                Return True
            End If
        End Function

        ''' <summary>
        ''' Returns a set of all two-sided ideals of this ring.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SetOfAllTwoSidedIdeals() As FiniteSet(Of FiniteSet(Of T))
            If Me.ringProperties.ContainsKey("all two-sided ideals") = False Then
                Dim newSet As New FiniteSet(Of FiniteSet(Of T))

                Dim index As Integer
                Dim pwrSet As FiniteSet(Of FiniteSet(Of T)) = Me.theSet.PowerSet()

                For index = 0 To pwrSet.Cardinality - 1
                    If Me.IsTwoSidedIdeal(pwrSet.Element(index)) = True Then
                        newSet.AddElement(pwrSet.Element(index))
                    End If
                Next index

                Me.allTwoSidedIdeals = newSet
                Me.ringProperties.Add("all two-sided ideals", True)
            End If

            Return Me.allTwoSidedIdeals.Clone()
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SetOfAllLeftZeroDivisors() As FiniteSet(Of T)
            Dim newSet As New FiniteSet(Of T)

            Parallel.For(0, Me.theSet.Cardinality - 1, Sub(index)
                                                           Dim elem As T = Me.theSet.Element(index)
                                                           Dim elementIndex As Integer

                                                           If (Me.AdditiveIdentity.Equals(elem) = False) Then
                                                               For elementIndex = 0 To Me.theSet.Cardinality - 1
                                                                   Dim curElem As T = Me.theSet.Element(elementIndex)

                                                                   If (Me.AdditiveIdentity.Equals(curElem) = False) Then
                                                                       Dim input As New Tuple(2)

                                                                       input.Element(0) = elem
                                                                       input.Element(1) = curElem

                                                                       If Me.ApplyMultiplication(input).Equals(Me.AdditiveIdentity) Then
                                                                           newSet.AddElement(elem)
                                                                           Exit For
                                                                       End If
                                                                   End If
                                                               Next elementIndex
                                                           End If
                                                       End Sub)

            Return newSet
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SetOfAllRightZeroDivisors() As FiniteSet(Of T)
            Dim newSet As New FiniteSet(Of T)

            Parallel.For(0, Me.theSet.Cardinality - 1, Sub(index)
                                                           Dim elem As T = Me.theSet.Element(index)
                                                           Dim elementIndex As Integer

                                                           If (Me.AdditiveIdentity.Equals(elem) = False) Then
                                                               For elementIndex = 0 To Me.theSet.Cardinality - 1
                                                                   Dim curElem As T = Me.theSet.Element(elementIndex)

                                                                   If (Me.AdditiveIdentity.Equals(curElem) = False) Then
                                                                       Dim input As New Tuple(2)

                                                                       input.Element(0) = curElem
                                                                       input.Element(1) = elem

                                                                       If Me.ApplyMultiplication(input).Equals(Me.AdditiveIdentity) Then
                                                                           newSet.AddElement(elem)
                                                                           Exit For
                                                                       End If
                                                                   End If
                                                               Next elementIndex
                                                           End If
                                                       End Sub)

            Return newSet
        End Function

        ''' <summary>
        ''' Returns the set of all zero divisors (those that are both left and right zero divisors).
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SetOfAllZeroDivisors() As FiniteSet(Of T)
            Return Me.SetOfAllLeftZeroDivisors.Intersection(Me.SetOfAllRightZeroDivisors)
        End Function

        ''' <summary>
        ''' Returns the trivial subring.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function TrivialSubring() As FiniteRing(Of T)
            Dim newSet As New FiniteSet(Of T)
            newSet.AddElement(Me.AdditiveIdentity)
            Dim newAdditionOp As FiniteBinaryOperation(Of T) = Me.AdditionOperation.Restriction(newSet)
            Dim newMultiplicationOp As FiniteBinaryOperation(Of T) = Me.MultiplicationOperation.Restriction(newSet)

            Return New FiniteRing(Of T)(newSet, newAdditionOp, newMultiplicationOp)
        End Function

#End Region

        ''' <summary>
        ''' The map for a direct product (cartesian product) ring.
        ''' </summary>
        ''' <typeparam name="G"></typeparam>
        ''' <remarks></remarks>
        Private Class DirectProductMap(Of G As {Class, New, IEquatable(Of G)})
            Inherits Map(Of Tuple, Tuple)

            Private myLeftRingMap As Map(Of Tuple, T)
            Private myRightRingMap As Map(Of Tuple, G)

            Public Sub New(ByVal leftRingMap As Map(Of Tuple, T), ByVal rightRingMap As Map(Of Tuple, G))
                Me.myLeftRingMap = leftRingMap
                Me.myRightRingMap = rightRingMap
            End Sub

            Public Overrides Function ApplyMap(ByVal input As Tuple) As Tuple
                Dim lhs As Tuple = CType(input.Element(0), Tuple)
                Dim rhs As Tuple = CType(input.Element(1), Tuple)

                Dim lhs1 As T = CType(lhs.Element(0), T)
                Dim lhs2 As G = CType(lhs.Element(1), G)
                Dim rhs1 As T = CType(rhs.Element(0), T)
                Dim rhs2 As G = CType(rhs.Element(1), G)

                Dim tup1 As New Tuple(2)
                Dim tup2 As New Tuple(2)
                Dim result As New Tuple(2)

                tup1.Element(0) = lhs1
                tup1.Element(1) = rhs1
                tup2.Element(0) = lhs2
                tup2.Element(1) = rhs2

                result.Element(0) = Me.myLeftRingMap.ApplyMap(tup1)
                result.Element(1) = Me.myRightRingMap.ApplyMap(tup2)

                Return result
            End Function
        End Class

    End Class

    ''' <summary>
    ''' Represents an error that an object was not an ideal. 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class NotIdealException
        Inherits Exception

#Region "  Constructors  "

        Public Sub New()
            MyBase.New("The parameter is not an ideal of the ring.")
        End Sub

        Public Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub

#End Region

    End Class

    ''' <summary>
    ''' Represents an error that an object was not a subgring. 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class NotSubringException
        Inherits Exception

#Region "  Constructors  "

        Public Sub New()
            MyBase.New("The parameter is not a subring of the ring.")
        End Sub

        Public Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub

#End Region

    End Class

End Namespace