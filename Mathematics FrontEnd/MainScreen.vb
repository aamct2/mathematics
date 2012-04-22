
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
    Private Zmod11Set As New FiniteSet(Of IntegerNumber)

    Private Zmod2Addition As FiniteBinaryOperation(Of IntegerNumber)
    Private Zmod3Addition As FiniteBinaryOperation(Of IntegerNumber)
    Private Zmod4Addition As FiniteBinaryOperation(Of IntegerNumber)
    Private Zmod11Addition As FiniteBinaryOperation(Of IntegerNumber)

    'Order: 2
    Private Zmod2Group As FiniteGroup(Of IntegerNumber)

    'Order: 3
    Private Zmod3Group As FiniteGroup(Of IntegerNumber)

    'Order: 4
    Private Zmod4Group As FiniteGroup(Of IntegerNumber)

    'Order 11
    Private Zmod11Group As FiniteGroup(Of IntegerNumber)

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
                                "Heisenberg-3 Group: " & (Heis3Time - Klein4Time).ToString
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRunTest.Click

    End Sub


    Private Sub GenerateJanko1Group()
        Dim index As Integer
        Dim n As Integer

        For index = 0 To n - 1
            Zmod11Set.AddElement(New IntegerNumber(index))
        Next index

        Zmod11Addition = New FiniteBinaryOperation(Of IntegerNumber)(Zmod11Set, New ZmodNAdditionMap(New IntegerNumber(11)))
        Zmod11Group = New FiniteGroup(Of IntegerNumber)(Zmod11Set, Zmod11Addition)

        Dim Y As New SquareMatrix(Of Zmod11)(7)
        Dim Z As New SquareMatrix(Of Zmod11)(7)

        For index = 0 To 7 - 1
            Y.Item(index, (index + 1) Mod 7) = New Zmod11(New IntegerNumber(1))
        Next index

        Z.Item(0, 0) = New Zmod11(New IntegerNumber(-3))
    End Sub
End Class

Friend Class Zmod11
    Implements ISubtractable(Of Zmod11), IDivideable(Of Zmod11), IComparable(Of Zmod11), IAbsoluteable(Of Zmod11)

    Private myValue As IntegerNumber

#Region "  Constructors  "

    Public Sub New()
        Me.Value = New IntegerNumber(0)
    End Sub

    Public Sub New(ByVal newValue As IntegerNumber)
        Me.Value = newValue Mod Zmod11.Integer11()
    End Sub

#End Region

#Region "  Properties  "

    Public Property Value() As IntegerNumber
        Get
            Return Me.myValue
        End Get
        Set(ByVal newValue As IntegerNumber)
            Me.myValue = newValue
        End Set
    End Property

    Public ReadOnly Property AdditiveIdentity() As Zmod11 Implements IAdditiveIdentity(Of Zmod11).AdditiveIdentity
        Get
            Return New Zmod11(New IntegerNumber(0))
        End Get
    End Property

    Public ReadOnly Property MultiplicativeIdentity() As Zmod11 Implements IMultiplicativeIdentity(Of Zmod11).MultiplicativeIdentity
        Get
            Return New Zmod11(New IntegerNumber(1))
        End Get
    End Property

#End Region

#Region "  Methods  "

    Private Shared Function Integer11() As IntegerNumber
        Return New IntegerNumber(11)
    End Function

    Public Function AbsoluteValue() As Zmod11 Implements IAbsoluteable(Of Zmod11).AbsoluteValue
        Return New Zmod11(Me.Value)
    End Function

    Public Function Add(ByVal otherT As Zmod11) As Zmod11 Implements IAddable(Of Zmod11).Add
        Return New Zmod11((Me.Value + otherT.Value) Mod Zmod11.Integer11())
    End Function

    Public Function Divide(ByVal otherT As Zmod11) As Zmod11 Implements IDivideable(Of Zmod11).Divide
        Return New Zmod11((Me.Value \ otherT.Value) Mod Zmod11.Integer11())
    End Function

    Public Function Multiply(ByVal otherT As Zmod11) As Zmod11 Implements IMultipliable(Of Zmod11).Multiply
        Return New Zmod11((Me.Value * otherT.Value) Mod Zmod11.Integer11())
    End Function

    Public Function Subtract(ByVal otherT As Zmod11) As Zmod11 Implements ISubtractable(Of Zmod11).Subtract
        Return New Zmod11((Me.Value - otherT.Value) Mod Zmod11.Integer11())
    End Function

    Public Function CompareTo(ByVal other As Zmod11) As Integer Implements System.IComparable(Of Zmod11).CompareTo
        If Me.Value < other.Value Then
            Return -1
        ElseIf Me.Value = other.Value Then
            Return 0
        Else
            Return 1
        End If
    End Function

#End Region

#Region "  Operators  "

    Public Shared Operator +(ByVal lhs As Zmod11, ByVal rhs As Zmod11) As Zmod11
        Return lhs.Add(rhs)
    End Operator

    Public Shared Operator -(ByVal lhs As Zmod11, ByVal rhs As Zmod11) As Zmod11
        Return lhs.Subtract(rhs)
    End Operator

    Public Shared Operator *(ByVal lhs As Zmod11, ByVal rhs As Zmod11) As Zmod11
        Return lhs.Multiply(rhs)
    End Operator

    Public Shared Operator /(ByVal lhs As Zmod11, ByVal rhs As Zmod11) As Zmod11
        Return lhs.Divide(rhs)
    End Operator

    Public Shared Operator =(ByVal lhs As Zmod11, ByVal rhs As Zmod11) As Boolean
        Return lhs.CompareTo(rhs) = 0
    End Operator

    Public Shared Operator <>(ByVal lhs As Zmod11, ByVal rhs As Zmod11) As Boolean
        Return lhs.CompareTo(rhs) <> 0
    End Operator

    Public Shared Operator >(ByVal lhs As Zmod11, ByVal rhs As Zmod11) As Boolean
        Return lhs.CompareTo(rhs) = 1
    End Operator

    Public Shared Operator <(ByVal lhs As Zmod11, ByVal rhs As Zmod11) As Boolean
        Return lhs.CompareTo(rhs) = -1
    End Operator

    Public Shared Operator >=(ByVal lhs As Zmod11, ByVal rhs As Zmod11) As Boolean
        Return (lhs.CompareTo(rhs) >= 0)
    End Operator

    Public Shared Operator <=(ByVal lhs As Zmod11, ByVal rhs As Zmod11) As Boolean
        Return (lhs.CompareTo(rhs) <= 0)
    End Operator

#End Region

End Class
