
Imports Mathematics
Imports NUnit.Framework
Imports System.Collections.Generic

Namespace Common

    Namespace Set_Tests

        <TestFixture()> Public Class FiniteSet_Tests
            Private nums As List(Of IntegerNumber)
            Private numSet As FiniteSet(Of IntegerNumber)

            Public Sub New()
                MyBase.New()
            End Sub

            <SetUp()> Public Sub Init()
                nums = New List(Of IntegerNumber)
                nums.Add(New IntegerNumber(0))
                nums.Add(New IntegerNumber(1))
                nums.Add(New IntegerNumber(2))

                numSet = New FiniteSet(Of IntegerNumber)(nums)
            End Sub

#Region "  Constructor Tests  "

            <Test()> Public Sub ConstructorNewList()

                Assert.AreEqual("{0, 1, 2}", numSet.ToString)
            End Sub

#End Region

#Region "  Properties Tests  "

            <Test()> Public Sub PropertyElementGet()
                Assert.AreEqual(0, numSet.Element(0).Value)
                Assert.AreEqual(1, numSet.Element(1).Value)
                Assert.AreEqual(2, numSet.Element(2).Value)
            End Sub

            <Test()> Public Sub PropertyElementSet()
                Dim nums2 As New List(Of IntegerNumber)
                nums2.Add(New IntegerNumber(0))
                nums2.Add(New IntegerNumber(1))
                nums2.Add(New IntegerNumber(2))

                Dim numSet2 As New FiniteSet(Of IntegerNumber)(nums2)
                numSet2.Element(1) = New IntegerNumber(3)
                Assert.AreEqual(3, numSet2.Element(1).Value)
            End Sub

            <Test()> Public Sub PropertyNullSetGet()
                Assert.AreEqual("{}", numSet.NullSet.ToString)
            End Sub

#End Region

#Region "  Methods Tests  "

            <Test()> Public Sub MethodCardinality()
                Assert.AreEqual(3, numSet.Cardinality)
                Assert.AreEqual(0, numSet.NullSet.Cardinality)
            End Sub

            <Test()> Public Sub MethodAddElement()
                Dim nums2 As New List(Of IntegerNumber)
                nums2.Add(New IntegerNumber(0))
                nums2.Add(New IntegerNumber(1))
                nums2.Add(New IntegerNumber(2))

                Dim numSet2 As New FiniteSet(Of IntegerNumber)(nums2)
                numSet2.AddElement(New IntegerNumber(3))
                Assert.AreEqual(4, numSet2.Cardinality)
                Assert.AreEqual(3, numSet2.Element(3).Value)
            End Sub

            <Test()> Public Sub MethodDeleteElement()
                Dim nums2 As New List(Of IntegerNumber)
                nums2.Add(New IntegerNumber(0))
                nums2.Add(New IntegerNumber(1))
                nums2.Add(New IntegerNumber(2))

                Dim numSet2 As New FiniteSet(Of IntegerNumber)(nums2)
                numSet2.DeleteElement(1)
                Assert.AreEqual(2, numSet2.Cardinality)
                Assert.AreEqual(2, numSet2.Element(1).Value)
            End Sub

            <Test()> <ExpectedException("System.IndexOutOfRangeException")> Public Sub MethodDeleteElementIndexGreaterThanCardinality()
                numSet.DeleteElement(4)
            End Sub

            <Test()> Public Sub MethodEquals()
                Dim nums2 As New List(Of IntegerNumber)
                nums2.Add(New IntegerNumber(0))
                nums2.Add(New IntegerNumber(1))
                nums2.Add(New IntegerNumber(2))

                Dim numSet2 As New FiniteSet(Of IntegerNumber)(nums2)
                Assert.AreEqual(True, numSet.Equals(numSet2))
                Assert.AreEqual(False, numSet.Equals(numSet.NullSet))
                Assert.AreEqual(True, numSet.NullSet.Equals(numSet.NullSet))
            End Sub

            <Test()> Public Sub MethodContains()
                Assert.AreEqual(True, numSet.Contains(New IntegerNumber(1)))
                Assert.AreEqual(False, numSet.Contains(New IntegerNumber(5)))
                Assert.AreEqual(False, numSet.NullSet.Contains(New IntegerNumber(5)))
            End Sub

            <Test()> Public Sub MethodDirectProduct()
                Dim nums2 As New List(Of IntegerNumber)
                nums2.Add(New IntegerNumber(0))
                nums2.Add(New IntegerNumber(1))
                nums2.Add(New IntegerNumber(2))

                Dim numSet2 As New FiniteSet(Of IntegerNumber)(nums2)

                Assert.AreEqual("{(0, 0), (0, 1), (0, 2), (1, 0), (1, 1), (1, 2), (2, 0), (2, 1), (2, 2)}", _
                    numSet.DirectProduct(numSet2).ToString)
                Assert.AreEqual("{}", numSet.DirectProduct(Of IntegerNumber)(numSet.NullSet).ToString)
                Assert.AreEqual("{}", numSet.NullSet.DirectProduct(Of IntegerNumber)(numSet).ToString)
            End Sub

            <Test()> Public Sub MethodIsSubestOf()
                Dim nums2 As New List(Of IntegerNumber)
                nums2.Add(New IntegerNumber(0))
                nums2.Add(New IntegerNumber(2))

                Dim numSet2 As New FiniteSet(Of IntegerNumber)(nums2)

                Assert.AreEqual(True, numSet2.IsSubsetOf(numSet))

                nums2.Add(New IntegerNumber(3))
                numSet2 = New FiniteSet(Of IntegerNumber)(nums2)
                Assert.AreEqual(False, numSet2.IsSubsetOf(numSet))

                Assert.AreEqual(True, numSet.NullSet.IsSubsetOf(numSet))
            End Sub

            <Test()> Public Sub MethodIntersection()
                Dim nums2 As New List(Of IntegerNumber)
                nums2.Add(New IntegerNumber(0))
                nums2.Add(New IntegerNumber(1))
                nums2.Add(New IntegerNumber(2))

                Dim numSet2 As New FiniteSet(Of IntegerNumber)(nums2)

                Assert.AreEqual(True, numSet.Intersection(numSet2).Equals(numSet))
                Assert.AreEqual(True, numSet.Intersection(numSet.NullSet).Equals(numSet.NullSet))
                Assert.AreEqual(True, numSet.NullSet.Intersection(numSet).Equals(numSet.NullSet))

                numSet2.DeleteElement(1)
                Assert.AreEqual(True, numSet.Intersection(numSet2).Equals(numSet2))
            End Sub

            <Test()> Public Sub MethodPowerSet()
                Dim setEmptySet As New FiniteSet(Of FiniteSet(Of IntegerNumber))
                setEmptySet.AddElement(numSet.NullSet)
                Assert.AreEqual(True, numSet.NullSet.PowerSet.Equals(setEmptySet))

                Assert.AreEqual("{{}, {2}, {1}, {2, 1}, {0}, {2, 0}, {1, 0}, {2, 1, 0}}", numSet.PowerSet.ToString)
            End Sub

            <Test()> Public Sub MethodUnion()
                Dim nums2 As New List(Of IntegerNumber)
                nums2.Add(New IntegerNumber(0))
                nums2.Add(New IntegerNumber(1))
                nums2.Add(New IntegerNumber(2))

                Dim numSet2 As New FiniteSet(Of IntegerNumber)(nums2)

                Assert.AreEqual(True, numSet.Union(numSet2).Equals(numSet))
                Assert.AreEqual(True, numSet.Union(numSet.NullSet).Equals(numSet))
                Assert.AreEqual(True, numSet.NullSet.Union(numSet).Equals(numSet))

                numSet2.AddElement(New IntegerNumber(5))
                Assert.AreEqual(True, numSet.Union(numSet2).Equals(numSet2))
            End Sub

#End Region

#Region "  Operators Tests  "
            <Test()> Public Sub OperatorAnd()
                Dim nums2 As New List(Of IntegerNumber)
                nums2.Add(New IntegerNumber(0))
                nums2.Add(New IntegerNumber(1))
                nums2.Add(New IntegerNumber(2))

                Dim numSet2 As New FiniteSet(Of IntegerNumber)(nums2)

                Assert.AreEqual(True, (numSet And numSet2).Equals(numSet))
                Assert.AreEqual(True, (numSet And numSet.NullSet).Equals(numSet.NullSet))
                Assert.AreEqual(True, (numSet.NullSet And numSet).Equals(numSet.NullSet))

                numSet2.DeleteElement(1)
                Assert.AreEqual(True, (numSet And numSet2).Equals(numSet2))
            End Sub

            <Test()> Public Sub OperatorOr()
                Dim nums2 As New List(Of IntegerNumber)
                nums2.Add(New IntegerNumber(0))
                nums2.Add(New IntegerNumber(1))
                nums2.Add(New IntegerNumber(2))

                Dim numSet2 As New FiniteSet(Of IntegerNumber)(nums2)

                Assert.AreEqual(True, (numSet Or numSet2).Equals(numSet))
                Assert.AreEqual(True, (numSet Or numSet.NullSet).Equals(numSet))

                numSet2.AddElement(New IntegerNumber(5))
                Assert.AreEqual(True, (numSet Or numSet2).Equals(numSet2))
            End Sub
#End Region


        End Class

    End Namespace

End Namespace
