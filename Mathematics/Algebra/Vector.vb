
Imports System.Collections.Generic

Namespace Algebra

    ''' <summary>
    ''' Represents a vector with components of type T.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <remarks></remarks>
    Public Class Vector(Of T As {Class, New, ISubtractable(Of T), IMultipliable(Of T), IMultiplicativeIdentity(Of T)})
        Private myElements As New List(Of T)

#Region "  Constructors  "

        Public Sub New()
            Me.New(3)
        End Sub

        Public Sub New(ByVal size As Integer)
            Dim index As Integer

            For index = 0 To size - 1
                Me.myElements.Add(New T)
            Next index
        End Sub

#End Region

#Region "  Properties  "

        Public ReadOnly Property Count() As Integer
            Get
                Return Me.myElements.Count()
            End Get
        End Property

        Public Property Item(ByVal index As Integer) As T
            Get
                If index > Me.Count - 1 Then
                    Throw New IndexOutOfRangeException("index cannot be greater than the number of elements in the vector.")
                ElseIf index < 0 Then
                    Throw New IndexOutOfRangeException("index cannot be less than 0.")
                Else
                    Return Me.myElements.Item(index)
                End If
            End Get
            Set(ByVal value As T)
                If IsNothing(value) Then
                    Throw New ArgumentNullException("value cannot be null.")
                ElseIf index > Me.Count - 1 Then
                    Throw New IndexOutOfRangeException("index cannot be greater than the number of elements in the vector.")
                ElseIf index < 0 Then
                    Throw New IndexOutOfRangeException("index cannot be less than 0.")
                Else
                    Me.myElements.Item(index) = value
                End If
            End Set
        End Property

#End Region

    End Class

End Namespace