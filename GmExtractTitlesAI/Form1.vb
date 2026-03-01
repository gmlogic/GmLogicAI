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

    Private Sub btnRemoveSelectedFile_Click(sender As Object, e As EventArgs) Handles btnRemoveSelectedFile.Click
        RemoveSelectedFile()
    End Sub

    Private Sub lstSelectedFiles_KeyDown(sender As Object, e As KeyEventArgs) Handles lstSelectedFiles.KeyDown
        If e.KeyCode = Keys.Delete Then
            RemoveSelectedFile()
            e.Handled = True
        End If
    End Sub

    Private Sub RemoveSelectedFile()
        If lstSelectedFiles.SelectedIndex < 0 Then
            Return
        End If

        Dim selectedPath = lstSelectedFiles.SelectedItem.ToString()
        imageFiles.RemoveAll(Function(path) String.Equals(path, selectedPath, StringComparison.OrdinalIgnoreCase))
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
        Dim normalizedInput = NormalizeOcrArtifacts(raw)
        Dim converted = ConvertPolytonicToMonotonic(normalizedInput)
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


    Private Function NormalizeOcrArtifacts(input As String) As String
        If String.IsNullOrWhiteSpace(input) Then
            Return String.Empty
        End If

        Dim normalized = input

        ' Common OCR substitutions for Greek text.
        normalized = normalized.Replace("µ", "μ")
        normalized = normalized.Replace("᾿", "'")
        normalized = normalized.Replace("῾", "'")
        normalized = normalized.Replace("΄", "´")

        ' Fix line-break hyphenation for wrapped words.
        normalized = Regex.Replace(normalized,
                                   "([\p{IsGreek}\p{IsGreekExtended}])-\r?\n([\p{IsGreek}\p{IsGreekExtended}])",
                                   "$1$2")

        ' Normalize whitespace noise from OCR.
        normalized = Regex.Replace(normalized, "[ \t]+", " ")

        Return normalized
    End Function

    Private Function ConvertPolytonicToMonotonic(input As String) As String
        If String.IsNullOrEmpty(input) Then
            Return String.Empty
        End If

        Dim sb As New StringBuilder()
        Dim normalized = input.Normalize(NormalizationForm.FormD)
        Dim i As Integer = 0

        While i < normalized.Length
            Dim ch = normalized(i)

            If CharUnicodeInfo.GetUnicodeCategory(ch) = UnicodeCategory.NonSpacingMark Then
                i += 1
                Continue While
            End If

            Dim marks As New List(Of Char)()
            Dim j = i + 1
            While j < normalized.Length AndAlso CharUnicodeInfo.GetUnicodeCategory(normalized(j)) = UnicodeCategory.NonSpacingMark
                marks.Add(normalized(j))
                j += 1
            End While

            sb.Append(ConvertGreekClusterToMonotonic(ch, marks))
            i = j
        End While

        Return sb.ToString().Normalize(NormalizationForm.FormC)
    End Function

    Private Function ConvertGreekClusterToMonotonic(baseChar As Char, marks As List(Of Char)) As String
        Dim hasAccent = marks.Any(Function(m) IsAccentMark(m))
        Dim hasDiaeresis = marks.Any(Function(m) m = ChrW(&H308))

        Select Case baseChar
            Case "α"c
                Return If(hasAccent, "ά", "α")
            Case "ε"c
                Return If(hasAccent, "έ", "ε")
            Case "η"c
                Return If(hasAccent, "ή", "η")
            Case "ι"c
                If hasDiaeresis AndAlso hasAccent Then Return "ΐ"
                If hasDiaeresis Then Return "ϊ"
                Return If(hasAccent, "ί", "ι")
            Case "ο"c
                Return If(hasAccent, "ό", "ο")
            Case "υ"c
                If hasDiaeresis AndAlso hasAccent Then Return "ΰ"
                If hasDiaeresis Then Return "ϋ"
                Return If(hasAccent, "ύ", "υ")
            Case "ω"c
                Return If(hasAccent, "ώ", "ω")
            Case "Α"c
                Return If(hasAccent, "Ά", "Α")
            Case "Ε"c
                Return If(hasAccent, "Έ", "Ε")
            Case "Η"c
                Return If(hasAccent, "Ή", "Η")
            Case "Ι"c
                If hasDiaeresis Then Return "Ϊ"
                Return If(hasAccent, "Ί", "Ι")
            Case "Ο"c
                Return If(hasAccent, "Ό", "Ο")
            Case "Υ"c
                If hasDiaeresis Then Return "Ϋ"
                Return If(hasAccent, "Ύ", "Υ")
            Case "Ω"c
                Return If(hasAccent, "Ώ", "Ω")
            Case Else
                Return baseChar
        End Select
    End Function

    Private Function IsAccentMark(mark As Char) As Boolean
        Select Case AscW(mark)
            Case &H300, ' grave
                 &H301, ' acute
                 &H342, ' perispomeni
                 &H340, ' varia
                 &H341  ' oxia
                Return True
            Case Else
                Return False
        End Select
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
        Dim lines = Regex.Split(cleanText, "\r?\n")
        Dim titles As New List(Of String)()

        For i = 0 To lines.Length - 1
            Dim line = lines(i).Trim()
            If IsSeparatorLine(line) Then
                Continue For
            End If

            Dim prevEmpty = (i = 0) OrElse IsSeparatorLine(lines(i - 1))
            Dim nextEmpty = (i = lines.Length - 1) OrElse IsSeparatorLine(lines(i + 1))

            If Not (prevEmpty AndAlso nextEmpty) Then
                Continue For
            End If

            Dim stripped = Regex.Replace(line, "^[^\p{Lu}]*", String.Empty)
            If stripped.Length = 0 Then
                Continue For
            End If

            If Not IsLikelyHeadingText(stripped) Then
                Continue For
            End If

            titles.Add(stripped)
        Next

        Return titles.Distinct().ToList()
    End Function

    Private Function IsSeparatorLine(line As String) As Boolean
        If String.IsNullOrWhiteSpace(line) Then
            Return True
        End If

        Dim compact = line.Trim()

        ' Consider punctuation/digits-only OCR artifacts as separators.
        Return Regex.IsMatch(compact, "^[\p{P}\p{S}\p{N}\s]+$")
    End Function

    Private Function ContainsGreekLetter(text As String) As Boolean
        Return Regex.IsMatch(text, "[\p{IsGreek}\p{IsGreekExtended}]")
    End Function


    Private Function IsLikelyHeadingText(text As String) As Boolean
        If text.Length = 0 OrElse text.Length > 120 Then
            Return False
        End If

        If Not ContainsGreekLetter(text) Then
            Return False
        End If

        If IsBookHeaderLine(text) Then
            Return False
        End If

        Dim words = Regex.Split(text.Trim(), "\s+").Where(Function(w) w.Length > 0).ToList()
        If words.Count < 2 OrElse words.Count > 12 Then
            Return False
        End If

        If Not Regex.IsMatch(text, "[\p{IsGreek}\p{IsGreekExtended}]") Then
            Return False
        End If

        If Not Regex.IsMatch(text, "[\p{Ll}]") Then
            Return False
        End If

        If Regex.IsMatch(text, "[,:;""'`-]\s*$") Then
            Return False
        End If

        Return True
    End Function

    Private Function IsBookHeaderLine(text As String) As Boolean
        Dim compact = text.Trim()
        If compact.StartsWith("ΠΡΟΣ ", StringComparison.OrdinalIgnoreCase) Then
            Return True
        End If

        ' OCR headers usually all-uppercase with chapter/verse markers.
        If Regex.IsMatch(compact, "^[\p{Lu}\p{IsGreek}\p{IsGreekExtended}\s\.'΄’0-9-]+$") AndAlso
           Not Regex.IsMatch(compact, "[\p{Ll}]") Then
            Return True
        End If

        Return False
    End Function

End Class
