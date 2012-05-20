
Imports System.Collections.Generic

Namespace Algebra

    Public Class FiniteLeftGroupAction(Of LeftOperand As {Class, New, IEquatable(Of LeftOperand)}, RightOperand As {Class, New, IEquatable(Of RightOperand)})
        Inherits FiniteLeftExternalBinaryOperation(Of LeftOperand, RightOperand)

        Private LeftOperandDomain As FiniteGroup(Of LeftOperand)
        Private RightOperandDomain As FiniteSet(Of RightOperand)

        Protected Friend groupActionProperties As New Dictionary(Of String, Boolean)

        Private allOrbits As FiniteSet(Of FiniteSet(Of RightOperand))

#Region "  Constructors  "

        Public Sub New(ByVal newGroup As FiniteGroup(Of LeftOperand), ByVal newCodomain As FiniteSet(Of RightOperand), ByVal newRelation As Map(Of Tuple, RightOperand))
            MyBase.New(newGroup.theSet, newCodomain, newRelation)

            Me.LeftOperandDomain = newGroup
            Me.RightOperandDomain = newCodomain

            '''''''''''''''''''''''''
            '   Test Associativity  '
            '''''''''''''''''''''''''
            Dim aIndex As Integer
            Dim bIndex As Integer
            Dim cIndex As Integer

            Dim aElem As LeftOperand
            Dim bElem As LeftOperand
            Dim cElem As RightOperand
            Dim leftTuple1 As New Tuple(2)
            Dim leftTuple2 As New Tuple(2)
            Dim rightTuple1 As New Tuple(2)
            Dim rightTuple2 As New Tuple(2)
            Dim leftResult As RightOperand
            Dim rightResult As RightOperand

            For aIndex = 0 To Me.LeftOperandDomain.Order - 1
                For bIndex = 0 To Me.LeftOperandDomain.Order - 1
                    For cIndex = 0 To Me.RightOperandDomain.Cardinality - 1
                        aElem = Me.LeftOperandDomain.theSet.Element(aIndex)
                        bElem = Me.LeftOperandDomain.theSet.Element(bIndex)
                        cElem = Me.RightOperandDomain.Element(cIndex)

                        leftTuple1.Element(0) = aElem
                        leftTuple1.Element(1) = bElem
                        leftTuple2.Element(0) = Me.LeftOperandDomain.ApplyOperation(leftTuple1)
                        leftTuple2.Element(1) = cElem
                        leftResult = Me.theRelation.ApplyMap(leftTuple2)

                        rightTuple1.Element(0) = bElem
                        rightTuple1.Element(1) = cElem
                        rightTuple2.Element(0) = aElem
                        rightTuple2.Element(1) = Me.theRelation.ApplyMap(rightTuple1)
                        rightResult = Me.theRelation.ApplyMap(rightTuple2)

                        If (leftResult.Equals(rightResult) = False) Then
                            Throw New DoesNotSatisfyPropertyException("The action operation is not associative.")
                        End If
                    Next cIndex
                Next bIndex
            Next aIndex

            '''''''''''''''''''''
            '   Test Identity   '
            '''''''''''''''''''''
            aElem = Me.LeftOperandDomain.IdentityElement
            For cIndex = 0 To Me.RightOperandDomain.Cardinality - 1
                cElem = Me.RightOperandDomain.Element(cIndex)
                leftTuple1.Element(0) = aElem
                leftTuple1.Element(1) = cElem
                leftResult = Me.theRelation.ApplyMap(leftTuple1)

                If (leftResult.Equals(cElem) = False) Then
                    Throw New DoesNotSatisfyPropertyException("The group identity element is not the identity of the action.")
                End If
            Next cIndex
        End Sub

#End Region


#Region "  Methods  "


        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>Sometimes referred to as "effective."</remarks>
        Public Function IsFaithful() As Boolean
            If Me.groupActionProperties.ContainsKey("faithful") = False Then
                Dim gIndex As Integer
                Dim xIndex As Integer
                Dim gElem As LeftOperand
                Dim xElem As RightOperand
                Dim curTuple As New Tuple(2)
                Dim success As Boolean

                For gIndex = 0 To Me.LeftOperandDomain.Order - 1
                    gElem = Me.LeftOperandDomain.theSet.Element(gIndex)

                    If (gElem.Equals(Me.LeftOperandDomain.IdentityElement) = False) Then
                        success = False

                        For xIndex = 0 To Me.RightOperandDomain.Cardinality - 1
                            xElem = Me.RightOperandDomain.Element(xIndex)

                            curTuple.Element(0) = gElem
                            curTuple.Element(1) = xElem

                            If Me.theRelation.ApplyMap(curTuple).Equals(xElem) = False Then
                                success = True
                                Exit For
                            End If
                        Next xIndex

                        If success = False Then
                            Me.groupActionProperties.Add("faithful", False)
                            Return False
                        End If
                    End If
                Next gIndex

                Me.groupActionProperties.Add("faithful", True)
            End If

            Return Me.groupActionProperties.Item("faithful")
        End Function

        ''' <summary>
        ''' Determines whether or not the group action is free.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>The action is free if for any x in X, g.x = h.x implies g = h. Equivalently: if there exists an x in X such that g.x = x (that is, if g has at least one fixed point), then g is the identity.</remarks>
        Public Function IsFree() As Boolean
            If Me.groupActionProperties.ContainsKey("free") = False Then
                Dim gIndex As Integer
                Dim xIndex As Integer
                Dim gElem As LeftOperand
                Dim xElem As RightOperand
                Dim curTuple As New Tuple(2)
                Dim failure As Boolean

                For gIndex = 0 To Me.LeftOperandDomain.Order - 1
                    gElem = Me.LeftOperandDomain.theSet.Element(gIndex)

                    If (gElem.Equals(Me.LeftOperandDomain.IdentityElement) = False) Then
                        failure = False

                        For xIndex = 0 To Me.RightOperandDomain.Cardinality - 1
                            xElem = Me.RightOperandDomain.Element(xIndex)

                            curTuple.Element(0) = gElem
                            curTuple.Element(1) = xElem

                            If Me.theRelation.ApplyMap(curTuple).Equals(xElem) = True Then
                                failure = True
                                Exit For
                            End If
                        Next xIndex

                        If failure = True Then
                            Me.groupActionProperties.Add("free", False)
                            Return False
                        End If
                    End If
                Next gIndex

                Me.groupActionProperties.Add("free", True)
            End If

            Return Me.groupActionProperties.Item("Free")
        End Function

        ''' <summary>
        ''' Determines whether the group action is regular. That is to say, the group action is both transitive and free.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsRegular() As Boolean
            If Me.groupActionProperties.ContainsKey("regular") = False Then
                If (Me.IsTransitive = True And Me.IsFree = True) Then
                    Me.groupActionProperties.Add("regular", True)
                Else
                    Me.groupActionProperties.Add("regular", False)
                End If
            End If

            Return Me.groupActionProperties.Item("regular")
        End Function

        ''' <summary>
        ''' Determines whether the action is transitive.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>\forall x, y \in X \exists g \in G such that gx = y</remarks>
        Public Function IsTransitive() As Boolean
            If Me.groupActionProperties.ContainsKey("transitive") = False Then
                Dim xIndex As Integer
                Dim yIndex As Integer
                Dim gIndex As Integer
                Dim foundG As Boolean

                Dim xElem As RightOperand
                Dim yElem As RightOperand
                Dim gElem As LeftOperand

                Dim curTuple As New Tuple(2)

                For xIndex = 0 To Me.RightOperandDomain.Cardinality - 1
                    xElem = Me.RightOperandDomain.Element(xIndex)

                    For yIndex = 0 To Me.RightOperandDomain.Cardinality - 1
                        yElem = Me.RightOperandDomain.Element(yIndex)
                        foundG = False

                        For gIndex = 0 To Me.LeftOperandDomain.Order - 1
                            gElem = Me.LeftOperandDomain.theSet.Element(gIndex)

                            curTuple.Element(0) = gElem
                            curTuple.Element(1) = xElem

                            If Me.theRelation.ApplyMap(curTuple).Equals(yElem) = True Then
                                foundG = True
                                Exit For
                            End If
                        Next gIndex

                        If foundG = False Then
                            Me.groupActionProperties.Add("transitive", False)
                            Return False
                        End If
                    Next yIndex
                Next xIndex

                Me.groupActionProperties.Add("transitive", True)
            End If

            Return Me.groupActionProperties.Item("transitive")
        End Function

        ''' <summary>
        ''' Returns the orbit with respect to a given element of the set being acted upon.
        ''' </summary>
        ''' <param name="elem"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Orbit(ByVal elem As RightOperand) As FiniteSet(Of RightOperand)
            If (Me.RightOperandDomain.Contains(elem) = False) Then
                Throw New NotMemberOfException("The element passed into the Orbit function of the group action is not a member of the set.")
            End If

            Dim result As New FiniteSet(Of RightOperand)

            Dim gIndex As Integer
            Dim gElem As LeftOperand
            Dim curTuple As New Tuple(2)

            curTuple.Element(1) = elem

            For gIndex = 0 To Me.LeftOperandDomain.Order - 1
                gElem = Me.LeftOperandDomain.theSet.Element(gIndex)

                curTuple.Element(0) = gElem
                result.AddElement(Me.theRelation.ApplyMap(curTuple))
            Next gIndex

            Return result
        End Function

        ''' <summary>
        ''' Returns the stabilizer subgroup with respect to a given element of the set being acted upon.
        ''' </summary>
        ''' <param name="elem"></param>
        ''' <returns></returns>
        ''' <remarks>The stabilizer is a subgroup of the group in the group action.</remarks>
        Public Function Stabilizer(ByVal elem As RightOperand) As FiniteGroup(Of LeftOperand)
            If (Me.RightOperandDomain.Contains(elem) = False) Then
                Throw New NotMemberOfException("The element passed into the Orbit function of the group action is not a member of the set.")
            End If

            If Me.groupActionProperties.ContainsKey("Free") Then
                If Me.IsFree = True Then
                    'The group action is free if and only if all stabilizers are trivial.
                    Return Me.LeftOperandDomain.TrivialSubgroup
                End If
            End If

            Dim resultSet As New FiniteSet(Of LeftOperand)

            Dim gIndex As Integer
            Dim gElem As LeftOperand
            Dim curTuple As New Tuple(2)

            curTuple.Element(1) = elem

            For gIndex = 0 To Me.LeftOperandDomain.Order - 1
                gElem = Me.LeftOperandDomain.theSet.Element(gIndex)

                curTuple.Element(0) = gElem
                If Me.theRelation.ApplyMap(curTuple).Equals(elem) Then
                    resultSet.AddElement(gElem)
                End If
            Next gIndex


            Dim subgroupOperation As FiniteBinaryOperation(Of LeftOperand) = Me.LeftOperandDomain.Operation.Restriction(resultSet)

            Dim theStabilzer As New FiniteGroup(Of LeftOperand)(resultSet, subgroupOperation, Me.LeftOperandDomain.SubgroupClosedProperties)

            Return theStabilzer
        End Function


        ''' <summary>
        ''' Returns the set of all orbits of the group action.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>This is a partition of the set acted upon by the group action.</remarks>
        Public Function SetOfAllOrbits() As FiniteSet(Of FiniteSet(Of RightOperand))
            If Me.groupActionProperties.ContainsKey("all orbits") = False Then
                Dim xIndex As Integer
                Dim xElem As RightOperand
                Dim usedElements As New FiniteSet(Of RightOperand)
                Dim result As New FiniteSet(Of FiniteSet(Of RightOperand))
                Dim curOrbit As FiniteSet(Of RightOperand)

                For xIndex = 0 To Me.RightOperandDomain.Cardinality - 1
                    xElem = Me.RightOperandDomain.Element(xIndex)

                    If (usedElements.Contains(xElem) = False) Then
                        curOrbit = Me.Orbit(xElem)
                        usedElements = usedElements.Union(curOrbit)
                        result.AddElementWithoutCheck(curOrbit)
                    End If
                Next xIndex

                Me.allOrbits = result
                Me.groupActionProperties.Add("all orbits", True)
            End If

            Return Me.allOrbits
        End Function

#End Region

    End Class

End Namespace