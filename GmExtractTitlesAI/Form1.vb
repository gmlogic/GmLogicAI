Option Strict On

Imports System.Diagnostics
Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Public Class Form1
    Private ReadOnly imageFiles As New List(Of String)()
    Private ReadOnly tessPath As String = "C:\Program Files\Tesseract-OCR\tessdata"
    Private ReadOnly tesseractExePath As String = "C:\Program Files\Tesseract-OCR\tesseract.exe"

    Private Sub btnLoadTiff_Click(sender As Object, e As EventArgs) Handles btnLoadTiff.Click
        Dim selected As New List(Of String)()

        Using dialog As New OpenFileDialog()
            dialog.Title = "Select TIFF files"
            dialog.Filter = "TIFF files (*.tif;*.tiff)|*.tif;*.tiff|All files (*.*)|*.*"
            dialog.Multiselect = True

            Dim result = dialog.ShowDialog(Me)
            If result = DialogResult.OK Then
                selected.AddRange(dialog.FileNames)
            End If
        End Using

        If selected.Count = 0 Then
            selected.AddRange(GetDefaultImageFiles())
        End If

        imageFiles.Clear()
        imageFiles.AddRange(selected.Distinct(StringComparer.OrdinalIgnoreCase))
        RefreshSelectedFileList()
    End Sub

    Private Sub btnOcrPages_Click(sender As Object, e As EventArgs) Handles btnOcrPages.Click
        If imageFiles.Count = 0 Then
            imageFiles.AddRange(GetDefaultImageFiles())
            RefreshSelectedFileList()
        End If

        Dim missing = imageFiles.Where(Function(path) Not File.Exists(path)).ToList()
        If missing.Count > 0 Then
            MessageBox.Show("Missing files:" & Environment.NewLine & String.Join(Environment.NewLine, missing), "File error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim pagesText As New StringBuilder()

        For i = 0 To imageFiles.Count - 1
            Dim filePath = imageFiles(i)
            Dim ocr = RunTesseractOcr(filePath)

            If String.IsNullOrWhiteSpace(ocr) Then
                ocr = $"[No OCR output for page {i + 1}]"
            End If

            pagesText.AppendLine($"===== PAGE {i + 1}: {Path.GetFileName(filePath)} =====")
            pagesText.AppendLine(ocr.Trim())
            pagesText.AppendLine()
        Next

        txtRawPolytonic.Text = pagesText.ToString().Trim()
    End Sub

    Private Sub btnConvertToMonotonic_Click(sender As Object, e As EventArgs) Handles btnConvertToMonotonic.Click
        Dim raw = txtRawPolytonic.Text
        Dim converted = ConvertPolytonicToMonotonic(raw)
        Dim cleaned = CleanTextForProcessing(converted)
        txtCleanMonotonic.Text = cleaned
    End Sub

    Private Sub btnExtractTitles_Click(sender As Object, e As EventArgs) Handles btnExtractTitles.Click
        Dim source = txtCleanMonotonic.Text
        If String.IsNullOrWhiteSpace(source) Then
            MessageBox.Show("Run OCR and convert text first.", "No input", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim titles = ExtractParagraphTitles(source)
        txtExtractedTitles.Text = String.Join(Environment.NewLine, titles)
    End Sub

    Private Function GetDefaultImageFiles() As IEnumerable(Of String)
        Return New String() {
            "Q:\Vellas\Vellas-0331.tif",
            "Q:\Vellas\Vellas-0332.tif",
            "Q:\Vellas\Vellas-0333.tif",
            "Q:\Vellas\Vellas-0334.tif",
            "Q:\Vellas\Vellas-0335.tif"
        }
    End Function

    Private Sub RefreshSelectedFileList()
        lstSelectedFiles.Items.Clear()
        For Each filePath In imageFiles
            lstSelectedFiles.Items.Add(filePath)
        Next
    End Sub

    Private Function RunTesseractOcr(filePath As String) As String
        If Not IsTesseractAvailable() Then
            Return $"[tesseract executable not found at PATH or default location: {tesseractExePath}]"
        End If

        Dim tempOutputBase = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"))
        Dim outputTxt = tempOutputBase & ".txt"

        Try
            Dim psi As New ProcessStartInfo()
            psi.FileName = GetTesseractCommand()
            psi.Arguments = $"""{filePath}"" ""{tempOutputBase}"" -l ell --tessdata-dir ""{tessPath}"""
            psi.CreateNoWindow = True
            psi.UseShellExecute = False
            psi.RedirectStandardError = True
            psi.RedirectStandardOutput = True

            Using proc = Process.Start(psi)
                If proc Is Nothing Then
                    Return String.Empty
                End If

                proc.WaitForExit(120000)
                If Not proc.HasExited Then
                    proc.Kill()
                    Return "[OCR timeout]"
                End If

                If proc.ExitCode <> 0 Then
                    Dim err = proc.StandardError.ReadToEnd()
                    Return $"[OCR failed: {err}]"
                End If
            End Using

            If File.Exists(outputTxt) Then
                Return File.ReadAllText(outputTxt)
            End If

            Return String.Empty
        Catch ex As Exception
            Return $"[OCR exception: {ex.Message}]"
        Finally
            If File.Exists(outputTxt) Then
                File.Delete(outputTxt)
            End If
        End Try
    End Function


    Private Function GetTesseractCommand() As String
        If File.Exists(tesseractExePath) Then
            Return tesseractExePath
        End If

        Return "tesseract"
    End Function

    Private Function IsTesseractAvailable() As Boolean
        Try
            Dim psi As New ProcessStartInfo()
            psi.FileName = GetTesseractCommand()
            psi.Arguments = "--version"
            psi.CreateNoWindow = True
            psi.UseShellExecute = False
            psi.RedirectStandardOutput = True
            psi.RedirectStandardError = True

            Using proc = Process.Start(psi)
                If proc Is Nothing Then
                    Return False
                End If

                proc.WaitForExit(5000)
                Return proc.ExitCode = 0
            End Using
        Catch
            Return False
        End Try
    End Function

    Private Function ConvertPolytonicToMonotonic(input As String) As String
        If String.IsNullOrEmpty(input) Then
            Return String.Empty
        End If

        Dim normalized = input.Normalize(NormalizationForm.FormD)
        Dim sb As New StringBuilder()

        For Each ch In normalized
            Dim cat = CharUnicodeInfo.GetUnicodeCategory(ch)
            If cat <> UnicodeCategory.NonSpacingMark Then
                sb.Append(ch)
            End If
        Next

        Dim withoutMarks = sb.ToString().Normalize(NormalizationForm.FormC)
        withoutMarks = withoutMarks.Replace("ς", "σ")
        Return withoutMarks
    End Function

    Private Function CleanTextForProcessing(input As String) As String
        If String.IsNullOrWhiteSpace(input) Then
            Return String.Empty
        End If

        Dim allowedPattern = "[^\p{L}\p{N}\p{P}\p{Z}\r\n]"
        Dim cleaned = Regex.Replace(input, allowedPattern, " ")

        cleaned = Regex.Replace(cleaned, "[ \t]+", " ")
        cleaned = Regex.Replace(cleaned, "\n{3,}", Environment.NewLine & Environment.NewLine)

        Return cleaned.Trim()
    End Function

    Private Function ExtractParagraphTitles(cleanText As String) As List(Of String)
        Dim lines = cleanText.Split({Environment.NewLine}, StringSplitOptions.None)
        Dim titles As New List(Of String)()

        For i = 0 To lines.Length - 1
            Dim line = lines(i).Trim()
            If line.Length = 0 Then
                Continue For
            End If

            Dim prevEmpty = (i = 0) OrElse String.IsNullOrWhiteSpace(lines(i - 1))
            Dim nextEmpty = (i = lines.Length - 1) OrElse String.IsNullOrWhiteSpace(lines(i + 1))

            If Not (prevEmpty AndAlso nextEmpty) Then
                Continue For
            End If

            Dim stripped = Regex.Replace(line, "^[^\p{Lu}]*", String.Empty)
            If stripped.Length = 0 Then
                Continue For
            End If

            Dim first = stripped(0)
            If Char.IsUpper(first) Then
                titles.Add(stripped)
            End If
        Next

        Return titles.Distinct().ToList()
    End Function

End Class
