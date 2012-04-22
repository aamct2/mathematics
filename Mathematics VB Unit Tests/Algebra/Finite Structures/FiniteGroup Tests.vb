
Imports Mathematics
Imports Mathematics.Algebra
Imports NUnit.Framework

Namespace Algebra

    Namespace FiniteStructures

        <TestFixture()> Public Class FiniteGroup_Tests
            Private Zmod1Group As FiniteGroup(Of IntegerNumber)
            Private Zmod2Group As FiniteGroup(Of IntegerNumber)
            Private Zmod3Group As FiniteGroup(Of IntegerNumber)
            Private Zmod4Group As FiniteGroup(Of IntegerNumber)

            'Order: 8
            'non-Abelian, Nilpotent, Solvable, non-T-Group
            Private Dih8Group As FiniteGroup(Of SquareMatrix(Of RealNumber))

            'Order: 8
            'non-Abelian, Nilpotent, Solvable, Dedekind, T-Group, Special
            Private QuatGroup As FiniteGroup(Of SquareMatrix(Of ComplexNumber))

            'Order: 6
            'non-Abelian, non-Nilpotent, Solvable
            '(Not sure, by the system claims Sym3Group is metanilpotent. Need to verify)
            Private Sym3Group As FiniteGroup(Of SquareMatrix(Of RealNumber))

            'Order: 3
            'Simple, Abelian, Nilpotent, Solvable
            Private Alt3Group As FiniteGroup(Of SquareMatrix(Of RealNumber))

            'Order: 12
            'non-Simple, non-Abelian, non-Nilpotent, Solvable, non-T-Group
            Private Alt4Group As FiniteGroup(Of SquareMatrix(Of RealNumber))

            'Order: 60
            'Simple, non-Abelian, non-Nilpotent, non-Solvable
            Private Alt5Group As FiniteGroup(Of SquareMatrix(Of RealNumber))

            'Order: 4
            'Abelian, Nilpotent, Solvable, non-Cyclic
            Private Klein4Group As FiniteGroup(Of Tuple)

            Public Sub New()
                MyBase.New()

                Dim Zmod1Set As New FiniteSet(Of IntegerNumber)
                Zmod1Set.AddElement(New IntegerNumber(0))
                Dim Zmod1Addition As New FiniteBinaryOperation(Of IntegerNumber)(Zmod1Set, New ZmodNAdditionMap(New IntegerNumber(1)))
                Zmod1Group = New FiniteGroup(Of IntegerNumber)(Zmod1Set, Zmod1Addition)

                Dim Zmod2Set As New FiniteSet(Of IntegerNumber)
                Zmod2Set.AddElement(New IntegerNumber(0))
                Zmod2Set.AddElement(New IntegerNumber(1))
                Dim Zmod2Addition As New FiniteBinaryOperation(Of IntegerNumber)(Zmod2Set, New ZmodNAdditionMap(New IntegerNumber(2)))
                Zmod2Group = New FiniteGroup(Of IntegerNumber)(Zmod2Set, Zmod2Addition)

                Dim Zmod3Set As New FiniteSet(Of IntegerNumber)
                Zmod3Set.AddElement(New IntegerNumber(0))
                Zmod3Set.AddElement(New IntegerNumber(1))
                Zmod3Set.AddElement(New IntegerNumber(2))
                Dim Zmod3Addition As New FiniteBinaryOperation(Of IntegerNumber)(Zmod3Set, New ZmodNAdditionMap(New IntegerNumber(3)))
                Zmod3Group = New FiniteGroup(Of IntegerNumber)(Zmod3Set, Zmod3Addition)

                Dim Zmod4Set As New FiniteSet(Of IntegerNumber)
                Zmod4Set.AddElement(New IntegerNumber(0))
                Zmod4Set.AddElement(New IntegerNumber(1))
                Zmod4Set.AddElement(New IntegerNumber(2))
                Zmod4Set.AddElement(New IntegerNumber(3))
                Dim Zmod4Addition As New FiniteBinaryOperation(Of IntegerNumber)(Zmod4Set, New ZmodNAdditionMap(New IntegerNumber(4)))
                Zmod4Group = New FiniteGroup(Of IntegerNumber)(Zmod4Set, Zmod4Addition)

                Dih8Group = Dihedral8Group()

                QuatGroup = QuaternionGroup()

                Sym3Group = SymmetricGroup(3)
                Alt3Group = AlternatingGroup(3)

                Alt4Group = AlternatingGroup(4)

                Alt5Group = AlternatingGroup(5)

                Klein4Group = Zmod2Group.DirectProduct(Zmod2Group)
            End Sub

            <SetUp()> Public Sub Init()

            End Sub

#Region "  Constructor Tests  "

            <Test()> Public Sub ConstructorNew()
                Dim Zmod2Set As New FiniteSet(Of IntegerNumber)
                Zmod2Set.AddElement(New IntegerNumber(0))
                Zmod2Set.AddElement(New IntegerNumber(1))

                Dim Zmod2Addition As New FiniteBinaryOperation(Of IntegerNumber)(Zmod2Set, New ZmodNAdditionMap(New IntegerNumber(2)))

                Zmod2Group = New FiniteGroup(Of IntegerNumber)(Zmod2Set, Zmod2Addition)
            End Sub

#End Region

#Region "  Method Tests  "

            <Test()> Public Sub MethodCeter()
                Assert.AreEqual(True, Zmod2Group.Center.Equals(Zmod2Group.theSet))
            End Sub

            <Test()> Public Sub MethodCenterGroup()
                Assert.AreEqual(True, Zmod2Group.CenterGroup.Equals(Zmod2Group))
            End Sub

            <Test()> Public Sub MethodDerivedSubgroup()
                'As Parallel
                Assert.AreEqual(True, Zmod2Group.DerivedSubgroup.Equals(Zmod2Group.TrivialSubgroup), "Zmod2 Derived Subgroup - Parallel")
                Assert.AreEqual(True, Sym3Group.DerivedSubgroup.Equals(Alt3Group), "Sym3 Derived Subgroup - Parallel")

                'As Sequential
                Assert.AreEqual(True, Sym3Group.DerivedSubgroup(False).Equals(Alt3Group), "Sym3 Derived Subgroup - Sequential")
            End Sub

            <Test()> Public Sub MethodDirectProduct()
                Dim Zmod2xZmod2 As FiniteGroup(Of Tuple) = Zmod2Group.DirectProduct(Zmod2Group)
            End Sub

            <Test()> Public Sub MethodEquals()
                Assert.AreEqual(False, Zmod2Group.Equals(Zmod4Group))
            End Sub

            <Test()> Public Sub MethodGeneratedSet()
                Dim generatorSet1 As New FiniteSet(Of IntegerNumber)
                generatorSet1.AddElement(New IntegerNumber(1))
                Dim testMap As New ZmodNAdditionMap(New IntegerNumber(4))
                Dim result As FiniteSet(Of IntegerNumber)

                result = FiniteGroup(Of Mathematics.IntegerNumber).GeneratedSet(generatorSet1, testMap)

                Assert.AreEqual(True, result.Equals(Zmod4Group.theSet))
            End Sub

            <Test()> Public Sub MethodIsAbelian()
                Assert.AreEqual(False, Dihedral8Group.IsAbelian())
                Assert.AreEqual(True, Alt3Group.IsAbelian())
                Assert.AreEqual(True, Zmod4Group.IsAbelian())
                Assert.AreEqual(True, Klein4Group.IsAbelian())
                Assert.AreEqual(False, Alt4Group.IsAbelian())
                Assert.AreEqual(False, Alt5Group.IsAbelian())
                Assert.AreEqual(False, Sym3Group.IsAbelian())
                Assert.AreEqual(False, QuatGroup.IsAbelian())
            End Sub

            <Test()> Public Sub MethodIsConjugate()
                Dim elem1 As SquareMatrix(Of RealNumber)
                Dim elem2 As SquareMatrix(Of RealNumber)

                elem1 = Sym3Group.IdentityElement
                elem2 = Sym3Group.IdentityElement

                Assert.AreEqual(True, Sym3Group.IsConjugate(elem1, elem2))

                elem2 = Sym3Group.theSet.Element(3)

                Assert.AreEqual(False, Sym3Group.IsConjugate(elem1, elem2))
            End Sub

            <Test()> Public Sub MethodIsCyclic()
                Assert.AreEqual(True, Zmod2Group.IsCyclic())
                Assert.AreEqual(True, Zmod3Group.IsCyclic())
                Assert.AreEqual(False, Klein4Group.IsCyclic())
            End Sub

            <Test()> Public Sub MethodIsDedekind()
                Assert.AreEqual(True, QuatGroup.IsDedekind(), "Dedekind: Quaternion Group Test")
                Console.Out.WriteLine("Completed QuatGroup Test")

                Assert.AreEqual(False, Dih8Group.IsDedekind(), "Dedekind: Dihedral 8 Group Test")
                Console.Out.WriteLine("Completed Dihedral8Group Test")
            End Sub

            <Test()> Public Sub MethodIsHamiltonian()
                Assert.AreEqual(True, QuatGroup.IsHamiltonian())
                Assert.AreEqual(False, Zmod4Group.IsHamiltonian())
            End Sub

            <Test()> Public Sub MethodIsHypoabelian()
                Assert.AreEqual(True, Dih8Group.IsHypoabelian())
            End Sub

            <Test()> Public Sub MethodIsImperfect()
                Assert.AreEqual(True, Dih8Group.IsImperfect())
                Assert.AreEqual(True, Alt3Group.IsImperfect())
            End Sub

            <Test()> Public Sub MethodIsMetabelian()
                Assert.AreEqual(True, Dih8Group.IsMetabelian())
                Assert.AreEqual(True, QuatGroup.IsMetabelian()) 'Non-abelian, but is metabelian
            End Sub

            <Test()> Public Sub MethodIsMetanilpotent()
                Assert.AreEqual(True, Zmod4Group.IsMetanilpotent())
                Assert.AreEqual(True, Dihedral8Group.IsMetanilpotent())
                Assert.AreEqual(True, Sym3Group.IsMetanilpotent())
            End Sub

            <Test()> Public Sub MethodIsNilpotent()
                Assert.AreEqual(True, Dihedral8Group.IsNilpotent())
                Console.Out.WriteLine("Completed Dihedral8Group Test")

                Assert.AreEqual(True, Zmod4Group.IsNilpotent())
                Console.Out.WriteLine("Completed Zmod4Group Test")

                Assert.AreEqual(True, QuatGroup.IsNilpotent())
                Console.Out.WriteLine("Completed QuatGroup Test")

                Assert.AreEqual(True, Klein4Group.IsNilpotent())
                Console.Out.WriteLine("Completed Klein4Group Test")

                Assert.AreEqual(True, Alt3Group.IsNilpotent())
                Console.Out.WriteLine("Completed Alt3Group Test")

                Assert.AreEqual(False, Alt4Group.IsNilpotent())
                Console.Out.WriteLine("Completed Alt4Group Test")

                Assert.AreEqual(False, Sym3Group.IsNilpotent())
                Console.Out.WriteLine("Completed Sym3Group Test")

                'Assert.AreEqual(False, Alt5Group.IsNilpotent())
                'Console.Out.WriteLine("Completed Alt5Group Test")
            End Sub

            <Test()> Public Sub MethodIsNormalSubgroupOf()
                Assert.AreEqual(True, Zmod4Group.TrivialSubgroup.IsNormalSubgroupOf(Zmod4Group))
                Assert.AreEqual(True, Zmod4Group.IsNormalSubgroupOf(Zmod4Group))
                Assert.AreEqual(True, Dih8Group.CenterGroup.IsNormalSubgroupOf(Dih8Group))
                Assert.AreEqual(True, Dih8Group.DerivedSubgroup.IsNormalSubgroupOf(Dih8Group))
            End Sub

            <Test()> Public Sub MethodIsPerfect()
                Assert.AreEqual(False, Zmod2Group.IsPerfect())
            End Sub

            <Test()> Public Sub MethodIsSimple()
                Assert.AreEqual(True, Zmod2Group.IsSimple())
                Assert.AreEqual(True, Alt3Group.IsSimple())
                Assert.AreEqual(False, Alt4Group.IsSimple())
                'Assert.AreEqual(True, Alt5Group.IsSimple())
                Assert.AreEqual(False, Zmod4Group.IsSimple())
                Assert.AreEqual(False, Klein4Group.IsSimple())
            End Sub

            <Test()> Public Sub MethodIsSolvable()
                Assert.AreEqual(True, Dihedral8Group.IsSolvable())
                Console.Out.WriteLine("Completed Dihedral8Group Test")

                Assert.AreEqual(True, Klein4Group.IsSolvable())
                Console.Out.WriteLine("Completed Klein4Group Test")

                Assert.AreEqual(True, Alt3Group.IsSolvable())
                Console.Out.WriteLine("Completed Alt3Group Test")

                Assert.AreEqual(True, Sym3Group.IsSolvable())
                Console.Out.WriteLine("Completed Sym3Group Test")

                'Assert.AreEqual(True, Alt4Group.IsSolvable())
                'Console.Out.WriteLine("Completed Alt4Group Test")

                'TODO: This test currently takes about 8 minutes!
                'Assert.AreEqual(False, Alt5Group.IsSolvable())
                'Console.Out.WriteLine("Completed Alt5Group Test")
            End Sub

            <Test()> Public Sub MethodIsSpecial()

            End Sub

            <Test()> Public Sub MethodIsSubgroupOf()
                Assert.AreEqual(True, Alt3Group.IsSubgroupOf(Sym3Group), "Alt3 is a subgroup of Sym3 Test")
                Assert.AreEqual(False, Zmod2Group.IsSubgroupOf(Zmod4Group), "Zmod2 subgroup of Zmod4 Test")
                Assert.AreEqual(False, Zmod3Group.IsSubgroupOf(Zmod4Group), "Zmod3 subgroup of Zmod4 Test")
            End Sub

            <Test()> Public Sub MethodIsTGroup()
                Assert.AreEqual(True, Zmod4Group.IsTGroup(), "Zmod4Group Test")
                Assert.AreEqual(True, QuatGroup.IsTGroup(), "QuatGroup Test")
                Assert.AreEqual(False, Dih8Group.IsTGroup(), "Dihedral8Group Test")
                Assert.AreEqual(False, Alt4Group.IsTGroup(), "Alt4Group Test")
            End Sub

            <Test()> Public Sub MethodLeftCoset()
                Dim desiredSet As New FiniteSet(Of IntegerNumber)
                desiredSet.AddElement(New IntegerNumber(1))
                desiredSet.AddElement(New IntegerNumber(3))

                Dim newSet As New FiniteSet(Of IntegerNumber)
                newSet.AddElement(New IntegerNumber(0))
                newSet.AddElement(New IntegerNumber(2))
                Dim newOp As New FiniteBinaryOperation(Of IntegerNumber)(newSet, Zmod4Group.Operation.theRelation)
                Dim subgroup As New FiniteGroup(Of IntegerNumber)(newSet, newOp)

                Dim firstLCoset As FiniteSet(Of IntegerNumber)
                firstLCoset = Zmod4Group.LeftCoset(subgroup, New IntegerNumber(0))

                Dim secondLCoset As FiniteSet(Of IntegerNumber)
                secondLCoset = Zmod4Group.LeftCoset(subgroup, New IntegerNumber(2))

                Assert.AreEqual(True, firstLCoset.Equals(newSet), "0 Test")
                Assert.AreEqual(True, Zmod4Group.LeftCoset(subgroup, New IntegerNumber(1)).Equals(desiredSet), "1 Test")
                Assert.AreEqual(True, secondLCoset.Equals(newSet), "2 Test")
                Assert.AreEqual(True, Zmod4Group.LeftCoset(subgroup, New IntegerNumber(3)).Equals(desiredSet), "3 Test")


                '-------------------------------------------------------------------------'

                Dim k4Subgroup1 As FiniteGroup(Of Tuple)
                'Dim k4Subgroup2 As FiniteGroup(Of Tuple)
                'Dim k4Subgroup3 As FiniteGroup(Of Tuple)
                k4Subgroup1 = Klein4Group.SetOfAllSubgroups.Element(1)
                'k4Subgroup2 = Klein4Group.SetOfAllSubgroups.Element(2)
                'k4Subgroup3 = Klein4Group.SetOfAllSubgroups.Element(3)
                Dim desiredKSet As New FiniteSet(Of Tuple)
                desiredKSet.AddElement(Klein4Group.theSet.Element(1))
                desiredKSet.AddElement(Klein4Group.theSet.Element(2))

                Assert.AreEqual(True, Klein4Group.LeftCoset(k4Subgroup1, Klein4Group.theSet.Element(1)).Equals(desiredKSet), "Klein Test")
            End Sub

            <Test()> Public Sub MethodFrattiniSubgroup()
                'Console.Out.WriteLine(Zmod4Group.FrattiniSubgroup.theSet.ToString)
            End Sub

            <Test()> Public Sub MethodOrder()
                Assert.AreEqual(8, Dih8Group.Order)
                Assert.AreEqual(4, Zmod4Group.Order)
            End Sub

            <Test()> Public Sub MethodOrderElement()
                Assert.AreEqual(1, Dih8Group.Order(Dih8Group.IdentityElement))
                Assert.AreEqual(4, Zmod4Group.Order(New IntegerNumber(3)))
            End Sub

            <Test()> Public Sub MethodPerfectCore()
                Assert.AreEqual(True, Dih8Group.PerfectCore.Equals(Dih8Group.TrivialSubgroup))
            End Sub

            <Test()> Public Sub MethodQuotientGroup()
                Dim quotGroup As FiniteGroup(Of FiniteSet(Of SquareMatrix(Of RealNumber))) = Dih8Group.QuotientGroup(Dih8Group.CenterGroup)
            End Sub

            <Test()> Public Sub MethodRightCoset()
                Dim desiredSet As New FiniteSet(Of IntegerNumber)
                desiredSet.AddElement(New IntegerNumber(1))
                desiredSet.AddElement(New IntegerNumber(3))

                Dim newSet As New FiniteSet(Of IntegerNumber)
                newSet.AddElement(New IntegerNumber(0))
                newSet.AddElement(New IntegerNumber(2))
                Dim newOp As New FiniteBinaryOperation(Of IntegerNumber)(newSet, Zmod4Group.Operation.theRelation)
                Dim subgroup As New FiniteGroup(Of IntegerNumber)(newSet, newOp)

                Assert.AreEqual(True, Zmod4Group.RightCoset(subgroup, New IntegerNumber(0)).Equals(newSet))
                Assert.AreEqual(True, Zmod4Group.RightCoset(subgroup, New IntegerNumber(1)).Equals(desiredSet))
            End Sub

            <Test()> Public Sub MethodSetOfAllSubgroups()
                Dim desiredSet1 As New FiniteSet(Of FiniteGroup(Of IntegerNumber))
                desiredSet1.AddElement(Zmod1Group)
                desiredSet1.AddElement(Zmod2Group)
                Dim desiredSet2 As New FiniteSet(Of FiniteGroup(Of IntegerNumber))
                desiredSet2.AddElement(Zmod1Group)
                desiredSet2.AddElement(Zmod3Group)

                Assert.AreEqual(True, Zmod2Group.SetOfAllSubgroups.Equals(desiredSet1))
                Assert.AreEqual(True, Zmod3Group.SetOfAllSubgroups.Equals(desiredSet2))
            End Sub

#End Region

        End Class

    End Namespace

End Namespace

