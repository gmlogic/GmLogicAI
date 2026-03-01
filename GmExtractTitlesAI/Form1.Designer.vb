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
        Me.lstSelectedFiles = New System.Windows.Forms.TextBox()
        Me.btnOcrPages = New System.Windows.Forms.Button()
        Me.btnRemoveSelectedFile = New System.Windows.Forms.Button()
        Me.btnConvertToMonotonic = New System.Windows.Forms.Button()
        Me.btnExtractTitles = New System.Windows.Forms.Button()
        Me.lblRawText = New System.Windows.Forms.Label()
        Me.lblCleanText = New System.Windows.Forms.Label()
        Me.lblTitles = New System.Windows.Forms.Label()
        Me.txtRawPolytonic = New System.Windows.Forms.TextBox()
        Me.txtCleanMonotonic = New System.Windows.Forms.TextBox()
        Me.txtExtractedTitles = New System.Windows.Forms.TextBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnLoadTiff
        '
        Me.btnLoadTiff.Location = New System.Drawing.Point(2, 2)
        Me.btnLoadTiff.Margin = New System.Windows.Forms.Padding(2)
        Me.btnLoadTiff.Name = "btnLoadTiff"
        Me.btnLoadTiff.Size = New System.Drawing.Size(116, 24)
        Me.btnLoadTiff.TabIndex = 0
        Me.btnLoadTiff.Text = "Load TIFF pages"
        Me.btnLoadTiff.UseVisualStyleBackColor = True
        '
        'lblFiles
        '
        Me.lblFiles.AutoSize = True
        Me.lblFiles.Location = New System.Drawing.Point(275, 8)
        Me.lblFiles.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblFiles.Name = "lblFiles"
        Me.lblFiles.Size = New System.Drawing.Size(67, 13)
        Me.lblFiles.TabIndex = 1
        Me.lblFiles.Text = "Loaded files:"
        '
        'lstSelectedFiles
        '
        Me.lstSelectedFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstSelectedFiles.Location = New System.Drawing.Point(0, 0)
        Me.lstSelectedFiles.Margin = New System.Windows.Forms.Padding(2)
        Me.lstSelectedFiles.Multiline = True
        Me.lstSelectedFiles.Name = "lstSelectedFiles"
        Me.lstSelectedFiles.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.lstSelectedFiles.Size = New System.Drawing.Size(947, 153)
        Me.lstSelectedFiles.TabIndex = 2
        '
        'btnOcrPages
        '
        Me.btnOcrPages.Location = New System.Drawing.Point(3, 3)
        Me.btnOcrPages.Margin = New System.Windows.Forms.Padding(2)
        Me.btnOcrPages.Name = "btnOcrPages"
        Me.btnOcrPages.Size = New System.Drawing.Size(89, 24)
        Me.btnOcrPages.TabIndex = 3
        Me.btnOcrPages.Text = "OCR all pages"
        Me.btnOcrPages.UseVisualStyleBackColor = True
        '
        'btnRemoveSelectedFile
        '
        Me.btnRemoveSelectedFile.Location = New System.Drawing.Point(122, 2)
        Me.btnRemoveSelectedFile.Margin = New System.Windows.Forms.Padding(2)
        Me.btnRemoveSelectedFile.Name = "btnRemoveSelectedFile"
        Me.btnRemoveSelectedFile.Size = New System.Drawing.Size(137, 24)
        Me.btnRemoveSelectedFile.TabIndex = 7
        Me.btnRemoveSelectedFile.Text = "Remove selected file"
        Me.btnRemoveSelectedFile.UseVisualStyleBackColor = True
        '
        'btnConvertToMonotonic
        '
        Me.btnConvertToMonotonic.Location = New System.Drawing.Point(318, 3)
        Me.btnConvertToMonotonic.Margin = New System.Windows.Forms.Padding(2)
        Me.btnConvertToMonotonic.Name = "btnConvertToMonotonic"
        Me.btnConvertToMonotonic.Size = New System.Drawing.Size(89, 24)
        Me.btnConvertToMonotonic.TabIndex = 4
        Me.btnConvertToMonotonic.Text = "Convert + clean text"
        Me.btnConvertToMonotonic.UseVisualStyleBackColor = True
        '
        'btnExtractTitles
        '
        Me.btnExtractTitles.Location = New System.Drawing.Point(633, 3)
        Me.btnExtractTitles.Margin = New System.Windows.Forms.Padding(2)
        Me.btnExtractTitles.Name = "btnExtractTitles"
        Me.btnExtractTitles.Size = New System.Drawing.Size(72, 24)
        Me.btnExtractTitles.TabIndex = 5
        Me.btnExtractTitles.Text = "Extract titles"
        Me.btnExtractTitles.UseVisualStyleBackColor = True
        '
        'lblRawText
        '
        Me.lblRawText.AutoSize = True
        Me.lblRawText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblRawText.Location = New System.Drawing.Point(3, 32)
        Me.lblRawText.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblRawText.Name = "lblRawText"
        Me.lblRawText.Size = New System.Drawing.Size(310, 30)
        Me.lblRawText.TabIndex = 0
        Me.lblRawText.Text = "OCR text (polytonic)"
        Me.lblRawText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCleanText
        '
        Me.lblCleanText.AutoSize = True
        Me.lblCleanText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCleanText.Location = New System.Drawing.Point(318, 32)
        Me.lblCleanText.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblCleanText.Name = "lblCleanText"
        Me.lblCleanText.Size = New System.Drawing.Size(310, 30)
        Me.lblCleanText.TabIndex = 1
        Me.lblCleanText.Text = "Monotonic + cleaned"
        Me.lblCleanText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTitles
        '
        Me.lblTitles.AutoSize = True
        Me.lblTitles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTitles.Location = New System.Drawing.Point(633, 32)
        Me.lblTitles.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblTitles.Name = "lblTitles"
        Me.lblTitles.Size = New System.Drawing.Size(311, 30)
        Me.lblTitles.TabIndex = 2
        Me.lblTitles.Text = "Extracted paragraph titles"
        Me.lblTitles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRawPolytonic
        '
        Me.txtRawPolytonic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtRawPolytonic.Location = New System.Drawing.Point(3, 65)
        Me.txtRawPolytonic.Margin = New System.Windows.Forms.Padding(2)
        Me.txtRawPolytonic.Multiline = True
        Me.txtRawPolytonic.Name = "txtRawPolytonic"
        Me.txtRawPolytonic.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtRawPolytonic.Size = New System.Drawing.Size(310, 284)
        Me.txtRawPolytonic.TabIndex = 3
        '
        'txtCleanMonotonic
        '
        Me.txtCleanMonotonic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCleanMonotonic.Location = New System.Drawing.Point(318, 65)
        Me.txtCleanMonotonic.Margin = New System.Windows.Forms.Padding(2)
        Me.txtCleanMonotonic.Multiline = True
        Me.txtCleanMonotonic.Name = "txtCleanMonotonic"
        Me.txtCleanMonotonic.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtCleanMonotonic.Size = New System.Drawing.Size(310, 284)
        Me.txtCleanMonotonic.TabIndex = 4
        '
        'txtExtractedTitles
        '
        Me.txtExtractedTitles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtExtractedTitles.Location = New System.Drawing.Point(633, 65)
        Me.txtExtractedTitles.Margin = New System.Windows.Forms.Padding(2)
        Me.txtExtractedTitles.Multiline = True
        Me.txtExtractedTitles.Name = "txtExtractedTitles"
        Me.txtExtractedTitles.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtExtractedTitles.Size = New System.Drawing.Size(311, 284)
        Me.txtExtractedTitles.TabIndex = 5
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.TableLayoutPanel1)
        Me.SplitContainer1.Size = New System.Drawing.Size(947, 544)
        Me.SplitContainer1.SplitterDistance = 188
        Me.SplitContainer1.TabIndex = 8
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnLoadTiff)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblFiles)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnRemoveSelectedFile)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.lstSelectedFiles)
        Me.SplitContainer2.Size = New System.Drawing.Size(947, 188)
        Me.SplitContainer2.SplitterDistance = 31
        Me.SplitContainer2.TabIndex = 8
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TableLayoutPanel1.Controls.Add(Me.txtExtractedTitles, 2, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.lblTitles, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lblCleanText, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.txtCleanMonotonic, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.lblRawText, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.btnExtractTitles, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.txtRawPolytonic, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.btnConvertToMonotonic, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btnOcrPages, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(947, 352)
        Me.TableLayoutPanel1.TabIndex = 6
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(947, 544)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MinimumSize = New System.Drawing.Size(814, 454)
        Me.Name = "Form1"
        Me.Text = "GmExtractTitlesAI"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnLoadTiff As Button
    Friend WithEvents lblFiles As Label
    Friend WithEvents lstSelectedFiles As TextBox
    Friend WithEvents btnOcrPages As Button
    Friend WithEvents btnRemoveSelectedFile As Button
    Friend WithEvents btnConvertToMonotonic As Button
    Friend WithEvents btnExtractTitles As Button
    Friend WithEvents txtRawPolytonic As TextBox
    Friend WithEvents txtCleanMonotonic As TextBox
    Friend WithEvents txtExtractedTitles As TextBox
    Friend WithEvents lblRawText As Label
    Friend WithEvents lblCleanText As Label
    Friend WithEvents lblTitles As Label
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
End Class
