<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnLoadTiff = New System.Windows.Forms.Button()
        Me.lblFiles = New System.Windows.Forms.Label()
        Me.lstSelectedFiles = New System.Windows.Forms.ListBox()
        Me.btnOcrPages = New System.Windows.Forms.Button()
        Me.btnConvertToMonotonic = New System.Windows.Forms.Button()
        Me.btnExtractTitles = New System.Windows.Forms.Button()
        Me.tableMain = New System.Windows.Forms.TableLayoutPanel()
        Me.txtRawPolytonic = New System.Windows.Forms.TextBox()
        Me.txtCleanMonotonic = New System.Windows.Forms.TextBox()
        Me.txtExtractedTitles = New System.Windows.Forms.TextBox()
        Me.lblRawText = New System.Windows.Forms.Label()
        Me.lblCleanText = New System.Windows.Forms.Label()
        Me.lblTitles = New System.Windows.Forms.Label()
        Me.tableMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnLoadTiff
        '
        Me.btnLoadTiff.Location = New System.Drawing.Point(12, 12)
        Me.btnLoadTiff.Name = "btnLoadTiff"
        Me.btnLoadTiff.Size = New System.Drawing.Size(155, 30)
        Me.btnLoadTiff.TabIndex = 0
        Me.btnLoadTiff.Text = "Load TIFF pages"
        Me.btnLoadTiff.UseVisualStyleBackColor = True
        '
        'lblFiles
        '
        Me.lblFiles.AutoSize = True
        Me.lblFiles.Location = New System.Drawing.Point(176, 20)
        Me.lblFiles.Name = "lblFiles"
        Me.lblFiles.Size = New System.Drawing.Size(87, 16)
        Me.lblFiles.TabIndex = 1
        Me.lblFiles.Text = "Loaded files:"
        '
        'lstSelectedFiles
        '
        Me.lstSelectedFiles.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right)), System.Windows.Forms.AnchorStyles)
        Me.lstSelectedFiles.FormattingEnabled = True
        Me.lstSelectedFiles.ItemHeight = 16
        Me.lstSelectedFiles.Location = New System.Drawing.Point(12, 48)
        Me.lstSelectedFiles.Name = "lstSelectedFiles"
        Me.lstSelectedFiles.Size = New System.Drawing.Size(1239, 84)
        Me.lstSelectedFiles.TabIndex = 2
        '
        'btnOcrPages
        '
        Me.btnOcrPages.Location = New System.Drawing.Point(12, 138)
        Me.btnOcrPages.Name = "btnOcrPages"
        Me.btnOcrPages.Size = New System.Drawing.Size(155, 30)
        Me.btnOcrPages.TabIndex = 3
        Me.btnOcrPages.Text = "OCR all pages"
        Me.btnOcrPages.UseVisualStyleBackColor = True
        '
        'btnConvertToMonotonic
        '
        Me.btnConvertToMonotonic.Location = New System.Drawing.Point(173, 138)
        Me.btnConvertToMonotonic.Name = "btnConvertToMonotonic"
        Me.btnConvertToMonotonic.Size = New System.Drawing.Size(205, 30)
        Me.btnConvertToMonotonic.TabIndex = 4
        Me.btnConvertToMonotonic.Text = "Convert + clean text"
        Me.btnConvertToMonotonic.UseVisualStyleBackColor = True
        '
        'btnExtractTitles
        '
        Me.btnExtractTitles.Location = New System.Drawing.Point(384, 138)
        Me.btnExtractTitles.Name = "btnExtractTitles"
        Me.btnExtractTitles.Size = New System.Drawing.Size(155, 30)
        Me.btnExtractTitles.TabIndex = 5
        Me.btnExtractTitles.Text = "Extract titles"
        Me.btnExtractTitles.UseVisualStyleBackColor = True
        '
        'tableMain
        '
        Me.tableMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tableMain.ColumnCount = 3
        Me.tableMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.tableMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.tableMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.tableMain.Controls.Add(Me.lblRawText, 0, 0)
        Me.tableMain.Controls.Add(Me.lblCleanText, 1, 0)
        Me.tableMain.Controls.Add(Me.lblTitles, 2, 0)
        Me.tableMain.Controls.Add(Me.txtRawPolytonic, 0, 1)
        Me.tableMain.Controls.Add(Me.txtCleanMonotonic, 1, 1)
        Me.tableMain.Controls.Add(Me.txtExtractedTitles, 2, 1)
        Me.tableMain.Location = New System.Drawing.Point(12, 174)
        Me.tableMain.Name = "tableMain"
        Me.tableMain.RowCount = 2
        Me.tableMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.tableMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tableMain.Size = New System.Drawing.Size(1239, 483)
        Me.tableMain.TabIndex = 6
        '
        'txtRawPolytonic
        '
        Me.txtRawPolytonic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtRawPolytonic.Location = New System.Drawing.Point(3, 29)
        Me.txtRawPolytonic.Multiline = True
        Me.txtRawPolytonic.Name = "txtRawPolytonic"
        Me.txtRawPolytonic.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtRawPolytonic.Size = New System.Drawing.Size(407, 451)
        Me.txtRawPolytonic.TabIndex = 3
        '
        'txtCleanMonotonic
        '
        Me.txtCleanMonotonic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCleanMonotonic.Location = New System.Drawing.Point(416, 29)
        Me.txtCleanMonotonic.Multiline = True
        Me.txtCleanMonotonic.Name = "txtCleanMonotonic"
        Me.txtCleanMonotonic.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtCleanMonotonic.Size = New System.Drawing.Size(407, 451)
        Me.txtCleanMonotonic.TabIndex = 4
        '
        'txtExtractedTitles
        '
        Me.txtExtractedTitles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtExtractedTitles.Location = New System.Drawing.Point(829, 29)
        Me.txtExtractedTitles.Multiline = True
        Me.txtExtractedTitles.Name = "txtExtractedTitles"
        Me.txtExtractedTitles.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtExtractedTitles.Size = New System.Drawing.Size(407, 451)
        Me.txtExtractedTitles.TabIndex = 5
        '
        'lblRawText
        '
        Me.lblRawText.AutoSize = True
        Me.lblRawText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblRawText.Location = New System.Drawing.Point(3, 0)
        Me.lblRawText.Name = "lblRawText"
        Me.lblRawText.Size = New System.Drawing.Size(407, 26)
        Me.lblRawText.TabIndex = 0
        Me.lblRawText.Text = "OCR text (polytonic)"
        Me.lblRawText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCleanText
        '
        Me.lblCleanText.AutoSize = True
        Me.lblCleanText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCleanText.Location = New System.Drawing.Point(416, 0)
        Me.lblCleanText.Name = "lblCleanText"
        Me.lblCleanText.Size = New System.Drawing.Size(407, 26)
        Me.lblCleanText.TabIndex = 1
        Me.lblCleanText.Text = "Monotonic + cleaned"
        Me.lblCleanText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTitles
        '
        Me.lblTitles.AutoSize = True
        Me.lblTitles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTitles.Location = New System.Drawing.Point(829, 0)
        Me.lblTitles.Name = "lblTitles"
        Me.lblTitles.Size = New System.Drawing.Size(407, 26)
        Me.lblTitles.TabIndex = 2
        Me.lblTitles.Text = "Extracted paragraph titles"
        Me.lblTitles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1263, 669)
        Me.Controls.Add(Me.tableMain)
        Me.Controls.Add(Me.btnExtractTitles)
        Me.Controls.Add(Me.btnConvertToMonotonic)
        Me.Controls.Add(Me.btnOcrPages)
        Me.Controls.Add(Me.lstSelectedFiles)
        Me.Controls.Add(Me.lblFiles)
        Me.Controls.Add(Me.btnLoadTiff)
        Me.MinimumSize = New System.Drawing.Size(1080, 550)
        Me.Name = "Form1"
        Me.Text = "GmExtractTitlesAI"
        Me.tableMain.ResumeLayout(False)
        Me.tableMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnLoadTiff As Button
    Friend WithEvents lblFiles As Label
    Friend WithEvents lstSelectedFiles As ListBox
    Friend WithEvents btnOcrPages As Button
    Friend WithEvents btnConvertToMonotonic As Button
    Friend WithEvents btnExtractTitles As Button
    Friend WithEvents tableMain As TableLayoutPanel
    Friend WithEvents txtRawPolytonic As TextBox
    Friend WithEvents txtCleanMonotonic As TextBox
    Friend WithEvents txtExtractedTitles As TextBox
    Friend WithEvents lblRawText As Label
    Friend WithEvents lblCleanText As Label
    Friend WithEvents lblTitles As Label
End Class
