Imports System.Text
Imports System.Globalization

' =========================================
' TEXT & NLP HELPERS
' =========================================
Module Helpers

    ' ---------- TEXT CLEAN ----------
    Public Function RemoveTones(text As String) As String
        Dim norm = text.Normalize(NormalizationForm.FormD)
        Dim sb As New StringBuilder()

        For Each ch In norm
            If CharUnicodeInfo.GetUnicodeCategory(ch) <>
               UnicodeCategory.NonSpacingMark Then
                sb.Append(ch)
            End If
        Next

        Return sb.ToString().Normalize(NormalizationForm.FormC)
    End Function

    Public Function CleanText(text As String) As String
        Dim sb As New StringBuilder()

        For Each ch In text
            If Char.IsLetter(ch) OrElse Char.IsWhiteSpace(ch) Then
                sb.Append(ch)
            Else
                sb.Append(" ")
            End If
        Next

        Return sb.ToString()
    End Function

    ' ---------- TOKENIZE ----------
    Public Function Tokenize(text As String) As List(Of String)
        Dim words As New List(Of String)

        For Each w In text.Split(" "c)
            Dim s = w.Trim()
            If s.Length > 0 Then words.Add(s)
        Next

        Return words
    End Function

    ' ---------- VOCAB ----------
    Public Function BuildVocab(words As List(Of String)) As List(Of String)
        Dim vocab As New List(Of String)

        For Each w In words
            If Not vocab.Contains(w) Then vocab.Add(w)
        Next

        Return vocab
    End Function

    Public Function BuildWordToIndex(
        vocab As List(Of String)
    ) As Dictionary(Of String, Integer)

        Dim dict As New Dictionary(Of String, Integer)

        For i = 0 To vocab.Count - 1
            dict(vocab(i)) = i
        Next

        Return dict
    End Function

    ' ===== ΝΕΑ (ΣΤΑΔΙΟ 1) =====

    Public Function BuildWordFrequency(words As List(Of String)) _
        As Dictionary(Of String, Integer)

        Dim freq As New Dictionary(Of String, Integer)

        For Each w In words
            If freq.ContainsKey(w) Then
                freq(w) += 1
            Else
                freq(w) = 1
            End If
        Next

        Return freq
    End Function


    Public Function FilterWordsByFrequency(
        words As List(Of String),
        freq As Dictionary(Of String, Integer),
        minCount As Integer
    ) As List(Of String)

        Dim result As New List(Of String)

        For Each w In words
            If freq(w) >= minCount Then
                result.Add(w)
            End If
        Next

        Return result
    End Function

    ' ---------- ONE-HOT ----------
    Public Function OneHot(
        word As String,
        vocab As List(Of String)
    ) As List(Of Double)

        Dim vec As New List(Of Double)

        For Each v In vocab
            If v = word Then
                vec.Add(1.0)
            Else
                vec.Add(0.0)
            End If
        Next

        Return vec
    End Function

    ' ---------- VECTOR → WORD ----------
    Public Function VectorToWord(
        v As List(Of Double),
        vocab As List(Of String)
    ) As String

        Dim maxIndex As Integer = 0

        For i = 1 To v.Count - 1
            If v(i) > v(maxIndex) Then maxIndex = i
        Next

        Return vocab(maxIndex)
    End Function

    Public Sub SaveVocab(path As String, vocab As List(Of String))
        IO.File.WriteAllLines(path, vocab)
    End Sub

    Public Function LoadVocab(path As String) As List(Of String)
        Return IO.File.ReadAllLines(path).ToList()
    End Function

    ' ---------- CONCAT 2 ONE-HOT ----------
    Public Function TwoWordOneHot(
    w1 As String,
    w2 As String,
    vocab As List(Of String)
) As List(Of Double)

        Dim v1 = OneHot(w1, vocab)
        Dim v2 = OneHot(w2, vocab)

        Dim result As New List(Of Double)
        result.AddRange(v1)
        result.AddRange(v2)

        Return result
    End Function

    Public Function SampleWord(
    output As List(Of Double),
    vocab As List(Of String),
    temperature As Double
) As String

        Dim expVals As New List(Of Double)
        Dim sum As Double = 0

        For Each v In output
            Dim e = Math.Exp(v / temperature)
            expVals.Add(e)
            sum += e
        Next

        Randomize()
        Dim r As Double = Rnd()
        Dim cumulative As Double = 0

        For i = 0 To expVals.Count - 1
            cumulative += expVals(i) / sum
            If r <= cumulative Then
                Return vocab(i)
            End If
        Next

        Return vocab(0)

    End Function


End Module

' =========================================
' SIMPLE MLP (FORWARD ONLY)
' =========================================
Public Class MLP

    Public InputSize As Integer
    Public HiddenSize As Integer
    Public OutputSize As Integer

    Public W1 As List(Of List(Of Double))
    Public W2 As List(Of List(Of Double))

    Public Sub New(inputSize As Integer,
                   hiddenSize As Integer,
                   outputSize As Integer)

        Me.InputSize = inputSize
        Me.HiddenSize = hiddenSize
        Me.OutputSize = outputSize

        W1 = InitMatrix(inputSize, hiddenSize)
        W2 = InitMatrix(hiddenSize, outputSize)
    End Sub

    ' ---------- INIT ----------
    Private Function InitMatrix(
        rows As Integer,
        cols As Integer
    ) As List(Of List(Of Double))

        Dim rnd As New Random()
        Dim m As New List(Of List(Of Double))

        For i = 0 To rows - 1
            Dim row As New List(Of Double)
            For j = 0 To cols - 1
                row.Add(rnd.NextDouble() * 2 - 1) ' -1 .. 1
            Next
            m.Add(row)
        Next

        Return m
    End Function

    ' ---------- SIGMOID ----------
    Private Function Sigmoid(x As Double) As Double
        Return 1.0 / (1.0 + Math.Exp(-x))
    End Function

    ' ---------- PREDICT ----------
    Public Function Predict(
        input As List(Of Double)
    ) As List(Of Double)

        Dim hidden As New List(Of Double)
        Dim output As New List(Of Double)

        ' Input → Hidden
        For j = 0 To HiddenSize - 1
            Dim sum As Double = 0
            For i = 0 To InputSize - 1
                sum += input(i) * W1(i)(j)
            Next
            hidden.Add(Sigmoid(sum))
        Next

        ' Hidden → Output
        For k = 0 To OutputSize - 1
            Dim sum As Double = 0
            For j = 0 To HiddenSize - 1
                sum += hidden(j) * W2(j)(k)
            Next
            output.Add(Sigmoid(sum))
        Next

        Return output
    End Function

    Public Sub Train(
    input As List(Of Double),
    target As List(Of Double),
    lr As Double)

        Dim hidden As New List(Of Double)
        Dim output As New List(Of Double)

        ' ---------- FORWARD ----------
        For j = 0 To HiddenSize - 1
            Dim sum As Double = 0
            For i = 0 To InputSize - 1
                sum += input(i) * W1(i)(j)
            Next
            hidden.Add(Sigmoid(sum))
        Next

        For k = 0 To OutputSize - 1
            Dim sum As Double = 0
            For j = 0 To HiddenSize - 1
                sum += hidden(j) * W2(j)(k)
            Next
            output.Add(Sigmoid(sum))
        Next

        ' ---------- OUTPUT ERROR ----------
        Dim outputError As New List(Of Double)
        For k = 0 To OutputSize - 1
            Dim err = target(k) - output(k)
            outputError.Add(err * output(k) * (1 - output(k)))
        Next

        ' ---------- HIDDEN ERROR ----------
        Dim hiddenError As New List(Of Double)
        For j = 0 To HiddenSize - 1
            Dim sum As Double = 0
            For k = 0 To OutputSize - 1
                sum += outputError(k) * W2(j)(k)
            Next
            hiddenError.Add(sum * hidden(j) * (1 - hidden(j)))
        Next

        ' ---------- UPDATE W2 ----------
        For j = 0 To HiddenSize - 1
            For k = 0 To OutputSize - 1
                W2(j)(k) += lr * outputError(k) * hidden(j)
            Next
        Next

        ' ---------- UPDATE W1 ----------
        For i = 0 To InputSize - 1
            For j = 0 To HiddenSize - 1
                W1(i)(j) += lr * hiddenError(j) * input(i)
            Next
        Next

    End Sub

    Public Sub SaveWeights(path As String)

        Dim ci = Globalization.CultureInfo.InvariantCulture

        Using sw As New IO.StreamWriter(path)

            sw.WriteLine("W1")
            For Each row In W1
                sw.WriteLine(String.Join(";", row.Select(
                Function(x) x.ToString(ci))))
            Next

            sw.WriteLine("W2")
            For Each row In W2
                sw.WriteLine(String.Join(";", row.Select(
                Function(x) x.ToString(ci))))
            Next

        End Using

    End Sub


    Public Sub LoadWeights(path As String)

        Dim lines = IO.File.ReadAllLines(path).ToList()
        Dim ci = Globalization.CultureInfo.InvariantCulture
        Dim lineIndex As Integer = 0

        ' ---- FIND W1 ----
        If lines(lineIndex).Trim() <> "W1" Then
            Throw New Exception("Invalid weight file: W1 not found")
        End If
        lineIndex += 1

        ' ---- READ W1 ----
        For i = 0 To W1.Count - 1

            If lines(lineIndex).Trim() = "W2" Then
                Throw New Exception("W1 size mismatch with file")
            End If

            Dim parts = lines(lineIndex).Split(";"c)

            For j = 0 To parts.Length - 1
                Dim s = parts(j).
                        Replace(",", ".").
                        Replace(vbCr, "").
                        Replace(vbLf, "").
                        Replace(ChrW(160), "").
                        Trim()

                Dim v As Double
                If Not Double.TryParse(
                    s,
                    Globalization.NumberStyles.Float Or
                    Globalization.NumberStyles.AllowExponent,
                    ci,
                    v) Then

                    Throw New Exception($"Invalid number in W1 at {i},{j}: '{s}'")
                End If

                W1(i)(j) = v
            Next

            lineIndex += 1
        Next

        ' ---- EXPECT W2 ----
        If lines(lineIndex).Trim() <> "W2" Then
            Throw New Exception("Invalid weight file: W2 not found")
        End If
        lineIndex += 1

        ' ---- READ W2 ----
        For i = 0 To W2.Count - 1

            Dim parts = lines(lineIndex).Split(";"c)

            For j = 0 To parts.Length - 1
                Dim s = parts(j).
                        Replace(",", ".").
                        Replace(vbCr, "").
                        Replace(vbLf, "").
                        Replace(ChrW(160), "").
                        Trim()

                Dim v As Double
                If Not Double.TryParse(
                    s,
                    Globalization.NumberStyles.Float Or
                    Globalization.NumberStyles.AllowExponent,
                    ci,
                    v) Then

                    Throw New Exception($"Invalid number in W2 at {i},{j}: '{s}'")
                End If

                W2(i)(j) = v
            Next

            lineIndex += 1
        Next

    End Sub


End Class
