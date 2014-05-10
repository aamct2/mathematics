
Imports Mathematics
Imports Mathematics.Algebra

''' <summary>
''' This project is used for testing purposes. Later it will be used as a front-end for the library.
''' </summary>
''' <remarks></remarks>
Public Class MainScreen

    Private Zmod2Set As New FiniteSet(Of IntegerNumber)
    Private Zmod3Set As New FiniteSet(Of IntegerNumber)
    Private Zmod4Set As New FiniteSet(Of IntegerNumber)

    Private Zmod2Addition As FiniteBinaryOperation(Of IntegerNumber)
    Private Zmod3Addition As FiniteBinaryOperation(Of IntegerNumber)
    Private Zmod4Addition As FiniteBinaryOperation(Of IntegerNumber)

    'Order: 2
    Private Zmod2Group As FiniteGroup(Of IntegerNumber)

    'Order: 3
    Private Zmod3Group As FiniteGroup(Of IntegerNumber)

    'Order: 4
    Private Zmod4Group As FiniteGroup(Of IntegerNumber)

    'Order: 8
    Private Dih8Group As FiniteGroup(Of SquareMatrix(Of RealNumber))

    'Order: 8
    Private QuatGroup As FiniteGroup(Of SquareMatrix(Of ComplexNumber))

    'Order: 6
    Private Sym3Group As FiniteGroup(Of SquareMatrix(Of RealNumber))

    'Order: 3
    'Simple, Abelian, Nilpotent, Solvable
    Private Alt3Group As FiniteGroup(Of SquareMatrix(Of RealNumber))

    'Order: 12
    'non-Simple, non-Abelian, non-Nilpotent, Solvable
    Private Alt4Group As FiniteGroup(Of SquareMatrix(Of RealNumber))

    'Order: 60
    'Simple, non-Abelian, non-Nilpotent, non-Solvable
    Private Alt5Group As FiniteGroup(Of SquareMatrix(Of RealNumber))

    'Order: 4
    'Abelian, Nilpotent, Solvable, non-Cyclic
    Private Klein4Group As FiniteGroup(Of Tuple)

    'Order: 27
    Private Heis3Group As FiniteGroup(Of SquareMatrix(Of Zmod3))

    'Order: 175,560
    Private J1Group As FiniteGroup(Of SquareMatrix(Of Zmod11))

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.GenerateCommonStructures()
    End Sub

    Private Sub GenerateCommonStructures()
        Dim startTime As Date
        Dim Z2Time As Date
        Dim Z3Time As Date
        Dim Z4Time As Date
        Dim Dih8Time As Date
        Dim QuatTime As Date
        Dim Sym3Time As Date
        Dim Alt3Time As Date
        Dim Alt4Time As Date
        Dim Alt5Time As Date
        Dim Klein4Time As Date
        Dim Heis3Time As Date
        Dim J1Time As Date
        Dim endTime As Date

        startTime = Now

        Zmod2Set.AddElement(New IntegerNumber(0))
        Zmod2Set.AddElement(New IntegerNumber(1))
        Zmod2Addition = New FiniteBinaryOperation(Of IntegerNumber)(Zmod2Set, New ZmodNAdditionMap(New IntegerNumber(2)))
        Zmod2Group = New FiniteGroup(Of IntegerNumber)(Zmod2Set, Zmod2Addition)
        Z2Time = Now

        Zmod3Set.AddElement(New IntegerNumber(0))
        Zmod3Set.AddElement(New IntegerNumber(1))
        Zmod3Set.AddElement(New IntegerNumber(2))
        Zmod3Addition = New FiniteBinaryOperation(Of IntegerNumber)(Zmod3Set, New ZmodNAdditionMap(New IntegerNumber(3)))
        Zmod3Group = New FiniteGroup(Of IntegerNumber)(Zmod3Set, Zmod3Addition)
        Z3Time = Now

        Zmod4Set.AddElement(New IntegerNumber(0))
        Zmod4Set.AddElement(New IntegerNumber(1))
        Zmod4Set.AddElement(New IntegerNumber(2))
        Zmod4Set.AddElement(New IntegerNumber(3))
        Zmod4Addition = New FiniteBinaryOperation(Of IntegerNumber)(Zmod4Set, New ZmodNAdditionMap(New IntegerNumber(4)))
        Zmod4Group = New FiniteGroup(Of IntegerNumber)(Zmod4Set, Zmod4Addition)
        Z4Time = Now

        Dih8Group = Dihedral8Group()
        Dih8Time = Now

        Dih8Group.IsAbelian()

        QuatGroup = QuaternionGroup()
        QuatTime = Now

        Sym3Group = SymmetricGroup(3)
        Sym3Time = Now
        Alt3Group = AlternatingGroup(3)
        Alt3Time = Now

        Alt4Group = AlternatingGroup(4)
        Alt4Time = Now

        Alt5Group = AlternatingGroup(5)
        Alt5Time = Now

        Klein4Group = Zmod2Group.DirectProduct(Zmod2Group)
        Klein4Time = Now

        Heis3Group = Heisenberg3Group()
        Heis3Time = Now

        J1Group = Janko1Group()
        J1Time = Now

        'Dim zSet As New FiniteSet(Of IntegerNumber)
        'Dim zGrp As FiniteGroup(Of IntegerNumber)
        'Dim index As Integer
        'Dim n As Integer
        'n = 15
        'For index = 0 To n - 1
        '    zSet.AddElement(New IntegerNumber(index))
        'Next index
        'zGrp = New FiniteGroup(Of IntegerNumber)(zSet, New FiniteBinaryOperation(Of IntegerNumber)(zSet, New ZmodNAdditionMap(New IntegerNumber(n))))
        'Dim pwrSet As FiniteSet(Of FiniteSet(Of IntegerNumber))
        'pwrSet = zGrp.theSet.PowerSet()

        Dim k4Subgroup1 As FiniteGroup(Of Tuple)
        Dim k4Subgroup2 As FiniteGroup(Of Tuple)
        Dim k4Subgroup3 As FiniteGroup(Of Tuple)
        k4Subgroup1 = Klein4Group.SetOfAllSubgroups.Element(1)
        k4Subgroup2 = Klein4Group.SetOfAllSubgroups.Element(2)
        k4Subgroup3 = Klein4Group.SetOfAllSubgroups.Element(3)
        Dim LCoset As FiniteSet(Of Tuple)

        LCoset = Klein4Group.LeftCoset(k4Subgroup1, Klein4Group.theSet.Element(1))

        Dim desiredKSet As New FiniteSet(Of Tuple)
        desiredKSet.AddElement(Klein4Group.theSet.Element(1))
        desiredKSet.AddElement(Klein4Group.theSet.Element(2))

        Dim generatorSet1 As New FiniteSet(Of IntegerNumber)
        generatorSet1.AddElement(New IntegerNumber(1))
        Dim testMap As New ZmodNAdditionMap(New IntegerNumber(2))
        Dim result As FiniteSet(Of IntegerNumber)

        result = FiniteGroup(Of Mathematics.IntegerNumber).GeneratedSet(generatorSet1, testMap)

        endTime = Now

        lblTimeStamps.Text = "Total Time to Generate Common Structures: " & (endTime - startTime).ToString & vbCrLf & _
                                "Zmod2 Group: " & (Z2Time - startTime).ToString & vbCrLf & _
                                "Zmod3 Group: " & (Z3Time - Z2Time).ToString & vbCrLf & _
                                "Zmod4 Group: " & (Z4Time - Z3Time).ToString & vbCrLf & _
                                "Dihedral Group of Order 8: " & (Dih8Time - Z4Time).ToString & vbCrLf & _
                                "Quaternion Group: " & (QuatTime - Dih8Time).ToString & vbCrLf & _
                                "Symmetric-3 Group: " & (Sym3Time - QuatTime).ToString & vbCrLf & _
                                "Alternating-3 Group: " & (Alt3Time - Sym3Time).ToString & vbCrLf & _
                                "Alternating-4 Group: " & (Alt4Time - Alt3Time).ToString & vbCrLf & _
                                "Alternating-5 Group: " & (Alt5Time - Alt4Time).ToString & vbCrLf & _
                                "Klein-4 Group: " & (Klein4Time - Alt5Time).ToString & vbCrLf & _
                                "Heisenberg-3 Group: " & (Heis3Time - Klein4Time).ToString & vbCrLf & _
                                "Janko 1 Group: " & (J1Time - Heis3Time).ToString
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRunTest.Click

    End Sub

End Class
