<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainScreen
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.cmdRunTest = New System.Windows.Forms.Button()
        Me.lblTimeStamps = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'cmdRunTest
        '
        Me.cmdRunTest.Location = New System.Drawing.Point(183, 321)
        Me.cmdRunTest.Name = "cmdRunTest"
        Me.cmdRunTest.Size = New System.Drawing.Size(138, 77)
        Me.cmdRunTest.TabIndex = 0
        Me.cmdRunTest.Text = "Run Test"
        Me.cmdRunTest.UseVisualStyleBackColor = True
        '
        'lblTimeStamps
        '
        Me.lblTimeStamps.AutoSize = True
        Me.lblTimeStamps.Location = New System.Drawing.Point(44, 22)
        Me.lblTimeStamps.Name = "lblTimeStamps"
        Me.lblTimeStamps.Size = New System.Drawing.Size(65, 13)
        Me.lblTimeStamps.TabIndex = 1
        Me.lblTimeStamps.Text = "TimeStamps"
        '
        'MainScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(509, 410)
        Me.Controls.Add(Me.lblTimeStamps)
        Me.Controls.Add(Me.cmdRunTest)
        Me.Name = "MainScreen"
        Me.Text = "Mathematics FrontEnd"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdRunTest As System.Windows.Forms.Button
    Friend WithEvents lblTimeStamps As System.Windows.Forms.Label

End Class
