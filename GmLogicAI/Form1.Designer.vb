<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.lblEpoch = New System.Windows.Forms.Label()
        Me.lblLoss = New System.Windows.Forms.Label()
        Me.txtOutput = New System.Windows.Forms.TextBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.btnTrain = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(10, 10)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(86, 26)
        Me.btnStart.TabIndex = 0
        Me.btnStart.Text = "Start"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'btnStop
        '
        Me.btnStop.Location = New System.Drawing.Point(103, 10)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(86, 26)
        Me.btnStop.TabIndex = 1
        Me.btnStop.Text = "Stop"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'lblEpoch
        '
        Me.lblEpoch.AutoSize = True
        Me.lblEpoch.Location = New System.Drawing.Point(10, 48)
        Me.lblEpoch.Name = "lblEpoch"
        Me.lblEpoch.Size = New System.Drawing.Size(50, 13)
        Me.lblEpoch.TabIndex = 2
        Me.lblEpoch.Text = "Epoch: 0"
        '
        'lblLoss
        '
        Me.lblLoss.AutoSize = True
        Me.lblLoss.Location = New System.Drawing.Point(103, 48)
        Me.lblLoss.Name = "lblLoss"
        Me.lblLoss.Size = New System.Drawing.Size(41, 13)
        Me.lblLoss.TabIndex = 3
        Me.lblLoss.Text = "Loss: 0"
        '
        'txtOutput
        '
        Me.txtOutput.Font = New System.Drawing.Font("Consolas", 10.0!)
        Me.txtOutput.Location = New System.Drawing.Point(10, 69)
        Me.txtOutput.Multiline = True
        Me.txtOutput.Name = "txtOutput"
        Me.txtOutput.ReadOnly = True
        Me.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOutput.Size = New System.Drawing.Size(395, 226)
        Me.txtOutput.TabIndex = 4
        '
        'Timer1
        '
        Me.Timer1.Interval = 50
        '
        'btnTrain
        '
        Me.btnTrain.Location = New System.Drawing.Point(195, 10)
        Me.btnTrain.Name = "btnTrain"
        Me.btnTrain.Size = New System.Drawing.Size(86, 26)
        Me.btnTrain.TabIndex = 5
        Me.btnTrain.Text = "Train One"
        Me.btnTrain.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(415, 313)
        Me.Controls.Add(Me.btnTrain)
        Me.Controls.Add(Me.txtOutput)
        Me.Controls.Add(Me.lblLoss)
        Me.Controls.Add(Me.lblEpoch)
        Me.Controls.Add(Me.btnStop)
        Me.Controls.Add(Me.btnStart)
        Me.Name = "Form1"
        Me.Text = "GmAI - Live Neural Learning"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnStart As Button
    Friend WithEvents btnStop As Button
    Friend WithEvents lblEpoch As Label
    Friend WithEvents lblLoss As Label
    Friend WithEvents txtOutput As TextBox
    Friend WithEvents Timer1 As Timer
    Friend WithEvents btnTrain As Button
End Class
