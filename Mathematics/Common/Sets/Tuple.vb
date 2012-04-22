
Imports System.Collections
Imports System.Text

Public Class Tuple
    Implements IEquatable(Of Tuple)

    Private mySize As Integer
    Private myElements As New ArrayList

#Region " Constructors "

    Public Sub New()
        Me.New(2)
    End Sub

    Public Sub New(ByVal newSize As Integer)
        Dim index As Integer

        Me.mySize = newSize
        For index = 0 To Me.Size - 1
            Me.myElements.Add("")
        Next index
    End Sub

    Public Sub New(ByVal newElements As ArrayList)
        Me.mySize = newElements.Count
        Me.myElements = newElements
    End Sub

#End Region

#Region " Properties "

    Public ReadOnly Property Size() As Integer
        Get
            Return Me.mySize
        End Get
    End Property

    Public Property Element(ByVal index As Integer) As Object
        Get
            Return Me.myElements(index)
        End Get
        Set(ByVal value As Object)
            Me.myElements(index) = value
        End Set
    End Property

#End Region

#Region " Methods "

    Public Shadows Function Equals(ByVal Tuple2 As Tuple) As Boolean Implements System.IEquatable(Of Tuple).Equals
        Dim index As Integer
        Dim curType As Type

        'Make sure the tuples are of the same size
        If Me.Size <> Tuple2.Size Then Return False

        For index = 0 To Me.Size - 1
            'Make sure the elements are of the same type
            If Me.Element(index).GetType.Equals(Tuple2.Element(index).GetType) = False Then Return False

            'Get that type
            curType = Me.Element(index).GetType

            'Find the "Equals" method for the type
            Dim methodSearchParams(0) As Type
            methodSearchParams(0) = curType
            Dim equalsMethod As System.Reflection.MethodInfo = curType.GetMethod("Equals", methodSearchParams)

            'Use that "Equals" method to determine if they are indeed equal
            Dim params(0) As Object
            params(0) = Tuple2.Element(index)
            If CType(equalsMethod.Invoke(Me.Element(index), params), Boolean) = False Then Return False
        Next index

        Return True
    End Function

    Public Overrides Function ToString() As String
        Dim strBuilder As New StringBuilder
        Dim index As Integer

        strBuilder.Append("(")

        If Me.Size > 0 Then
            strBuilder.Append(Me.Element(0).ToString)
            For index = 1 To Me.Size - 1
                strBuilder.Append(", " & Me.Element(index).ToString)
            Next index
        End If

        strBuilder.Append(")")

        Return strBuilder.ToString
    End Function

#End Region

End Class
