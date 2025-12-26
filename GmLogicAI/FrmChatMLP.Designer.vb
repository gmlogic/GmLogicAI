<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmChatMLP
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
        Me.txtFile = New System.Windows.Forms.TextBox()
        Me.btnOpenFile = New System.Windows.Forms.Button()
        Me.txtInput = New System.Windows.Forms.TextBox()
        Me.btnPredict = New System.Windows.Forms.Button()
        Me.lblOutput = New System.Windows.Forms.Label()
        Me.lstDebug = New System.Windows.Forms.ListBox()
        Me.ofdText = New System.Windows.Forms.OpenFileDialog()
        Me.pbTrain = New System.Windows.Forms.ProgressBar()
        Me.btnTrain = New System.Windows.Forms.Button()
        Me.tbTemp = New System.Windows.Forms.TrackBar()
        Me.lblTemp = New System.Windows.Forms.Label()
        Me.tmAutoLoad = New System.Windows.Forms.Timer(Me.components)
        CType(Me.tbTemp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtFile
        '
        Me.txtFile.Location = New System.Drawing.Point(20, 20)
        Me.txtFile.Name = "txtFile"
        Me.txtFile.ReadOnly = True
        Me.txtFile.Size = New System.Drawing.Size(300, 20)
        Me.txtFile.TabIndex = 0
        '
        'btnOpenFile
        '
        Me.btnOpenFile.Location = New System.Drawing.Point(340, 18)
        Me.btnOpenFile.Name = "btnOpenFile"
        Me.btnOpenFile.Size = New System.Drawing.Size(200, 23)
        Me.btnOpenFile.TabIndex = 1
        Me.btnOpenFile.Text = "Άνοιγμα αρχείου"
        Me.btnOpenFile.UseVisualStyleBackColor = True
        '
        'txtInput
        '
        Me.txtInput.Font = New System.Drawing.Font("Consolas", 12.0!)
        Me.txtInput.Location = New System.Drawing.Point(20, 71)
        Me.txtInput.Name = "txtInput"
        Me.txtInput.Size = New System.Drawing.Size(300, 26)
        Me.txtInput.TabIndex = 2
        '
        'btnPredict
        '
        Me.btnPredict.Location = New System.Drawing.Point(340, 71)
        Me.btnPredict.Name = "btnPredict"
        Me.btnPredict.Size = New System.Drawing.Size(200, 26)
        Me.btnPredict.TabIndex = 3
        Me.btnPredict.Text = "Επόμενη λέξη"
        Me.btnPredict.UseVisualStyleBackColor = True
        '
        'lblOutput
        '
        Me.lblOutput.AutoSize = True
        Me.lblOutput.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblOutput.Location = New System.Drawing.Point(20, 100)
        Me.lblOutput.Name = "lblOutput"
        Me.lblOutput.Size = New System.Drawing.Size(36, 19)
        Me.lblOutput.TabIndex = 4
        Me.lblOutput.Text = "..."
        '
        'lstDebug
        '
        Me.lstDebug.Font = New System.Drawing.Font("Consolas", 10.0!)
        Me.lstDebug.FormattingEnabled = True
        Me.lstDebug.ItemHeight = 15
        Me.lstDebug.Location = New System.Drawing.Point(2, 225)
        Me.lstDebug.Name = "lstDebug"
        Me.lstDebug.Size = New System.Drawing.Size(520, 244)
        Me.lstDebug.TabIndex = 5
        '
        'pbTrain
        '
        Me.pbTrain.Location = New System.Drawing.Point(2, 475)
        Me.pbTrain.Name = "pbTrain"
        Me.pbTrain.Size = New System.Drawing.Size(520, 23)
        Me.pbTrain.TabIndex = 6
        '
        'btnTrain
        '
        Me.btnTrain.Enabled = False
        Me.btnTrain.Location = New System.Drawing.Point(340, 39)
        Me.btnTrain.Name = "btnTrain"
        Me.btnTrain.Size = New System.Drawing.Size(200, 26)
        Me.btnTrain.TabIndex = 7
        Me.btnTrain.Text = "Training"
        Me.btnTrain.UseVisualStyleBackColor = True
        '
        'tbTemp
        '
        Me.tbTemp.Location = New System.Drawing.Point(546, 20)
        Me.tbTemp.Maximum = 150
        Me.tbTemp.Minimum = 50
        Me.tbTemp.Name = "tbTemp"
        Me.tbTemp.Size = New System.Drawing.Size(104, 45)
        Me.tbTemp.TabIndex = 8
        Me.tbTemp.TickFrequency = 10
        Me.tbTemp.Value = 80
        '
        'lblTemp
        '
        Me.lblTemp.AutoSize = True
        Me.lblTemp.Location = New System.Drawing.Point(546, 52)
        Me.lblTemp.Name = "lblTemp"
        Me.lblTemp.Size = New System.Drawing.Size(94, 13)
        Me.lblTemp.TabIndex = 9
        Me.lblTemp.Text = "Temperature: 0.80"
        '
        'FrmChatMLP
        '
        Me.ClientSize = New System.Drawing.Size(744, 503)
        Me.Controls.Add(Me.lblTemp)
        Me.Controls.Add(Me.tbTemp)
        Me.Controls.Add(Me.btnTrain)
        Me.Controls.Add(Me.pbTrain)
        Me.Controls.Add(Me.lstDebug)
        Me.Controls.Add(Me.lblOutput)
        Me.Controls.Add(Me.btnPredict)
        Me.Controls.Add(Me.txtInput)
        Me.Controls.Add(Me.btnOpenFile)
        Me.Controls.Add(Me.txtFile)
        Me.Name = "FrmChatMLP"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "GmChatAI - MLP"
        CType(Me.tbTemp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtFile As TextBox
    Friend WithEvents btnOpenFile As Button
    Friend WithEvents txtInput As TextBox
    Friend WithEvents btnPredict As Button
    Friend WithEvents lblOutput As Label
    Friend WithEvents lstDebug As ListBox
    Friend WithEvents ofdText As OpenFileDialog
    Friend WithEvents pbTrain As ProgressBar
    Friend WithEvents btnTrain As Button
    Friend WithEvents tbTemp As TrackBar
    Friend WithEvents lblTemp As Label
    Friend WithEvents tmAutoLoad As Timer
End Class
