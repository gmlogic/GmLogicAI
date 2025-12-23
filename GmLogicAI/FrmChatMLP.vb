Public Class FrmChatMLP

    ' -------- DATA --------
    Private words As List(Of String)
    Private vocab As List(Of String)
    Private wordToIndex As Dictionary(Of String, Integer)
    Private mlp As MLP

    Private Sub FrmChatMLP_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblOutput.Text = "..."
        lstDebug.Items.Add("Έτοιμο – άνοιξε αρχείο")
    End Sub

    ' -------- OPEN FILE --------
    Private Sub btnOpenFile_Click(sender As Object, e As EventArgs) Handles btnOpenFile.Click

        ofdText.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
        ofdText.Title = "Επέλεξε αρχείο κειμένου"
        ofdText.FileName = "Q:\Vellas\Vellas-text\Vellas Ιωάννης.txt"
        If ofdText.ShowDialog() <> DialogResult.OK Then Exit Sub

        txtFile.Text = ofdText.FileName
        LoadTextFile(ofdText.FileName)

    End Sub

    Private Sub LoadTextFile(path As String)

        lstDebug.Items.Clear()
        lstDebug.Items.Add("Φόρτωση αρχείου...")

        Dim rawText As String =
        My.Computer.FileSystem.ReadAllText(path)

        rawText = rawText.ToLower()
        rawText = RemoveTones(rawText)
        rawText = CleanText(rawText)

        Dim basePath = Application.StartupPath
        Dim vocabPath = IO.Path.Combine(basePath, "mlp_vocab.txt")
        Dim weightPath = IO.Path.Combine(basePath, "mlp_weights.txt")

        If IO.File.Exists(vocabPath) AndAlso IO.File.Exists(weightPath) Then

            vocab = LoadVocab(vocabPath)
            wordToIndex = BuildWordToIndex(vocab)

            mlp = New MLP(vocab.Count, 32, vocab.Count)
            mlp.LoadWeights(weightPath)

            lstDebug.Items.Add("Vocab + weights φορτώθηκαν ✔")

        Else
            words = Tokenize(rawText)

            ' --- ΣΤΑΔΙΟ 1: ΜΕΙΩΣΗ VOCAB ---
            Dim freq = BuildWordFrequency(words)

            Dim minCount As Integer = 3   ' 👈 άλλαξε σε 2 / 3 / 5 αν θες
            words = FilterWordsByFrequency(words, freq, minCount)
            vocab = BuildVocab(words)

            lstDebug.Items.Add("Words (after filter): " & words.Count)
            lstDebug.Items.Add("Vocab (after filter): " & vocab.Count)

            wordToIndex = BuildWordToIndex(vocab)

            wordToIndex = BuildWordToIndex(vocab)

            mlp = New MLP(vocab.Count, 32, vocab.Count)

            TrainMLP(epochs:=20, lr:=0.1)

            SaveVocab(vocabPath, vocab)
            mlp.SaveWeights(weightPath)

            lstDebug.Items.Add("Training + vocab αποθηκεύτηκαν ✔")

        End If

    End Sub


    ' -------- PREDICT --------
    Private Sub btnPredict_Click(sender As Object, e As EventArgs) Handles btnPredict.Click

        If mlp Is Nothing Then
            lblOutput.Text = "Άνοιξε πρώτα αρχείο"
            Exit Sub
        End If

        Dim inputWord As String = txtInput.Text.Trim().ToLower()
        If inputWord = "" Then Exit Sub

        If Not wordToIndex.ContainsKey(inputWord) Then
            lblOutput.Text = "Άγνωστη λέξη"
            Exit Sub
        End If

        Dim inputVec = OneHot(inputWord, vocab)
        Dim outputVec = mlp.Predict(inputVec)
        Dim nextWord = VectorToWord(outputVec, vocab)

        lblOutput.Text = nextWord
        lstDebug.Items.Add(inputWord & " → " & nextWord)

    End Sub

    Private Sub TrainMLP(epochs As Integer, lr As Double)

        lstDebug.Items.Add("Ξεκινά training...")
        pbTrain.Minimum = 0
        pbTrain.Maximum = 100
        pbTrain.Value = 0

        Dim totalSteps As Integer = epochs * (words.Count - 1)
        Dim currentStep As Integer = 0

        For epoch = 1 To epochs

            For i = 0 To words.Count - 2

                Dim w1 = words(i)
                Dim w2 = words(i + 1)

                Dim inputVec = OneHot(w1, vocab)
                Dim targetVec = OneHot(w2, vocab)

                mlp.Train(inputVec, targetVec, lr)

                ' ---- PROGRESS ----
                currentStep += 1

                If currentStep Mod 1000 = 0 Then
                    Dim percent As Integer =
                        CInt((currentStep / totalSteps) * 100)

                    If percent > 100 Then percent = 100
                    pbTrain.Value = percent
                    Application.DoEvents()
                End If

            Next

            lstDebug.Items.Add("Epoch " & epoch & " / " & epochs)
            lstDebug.TopIndex = lstDebug.Items.Count - 1
            Application.DoEvents()

        Next

        pbTrain.Value = 100
        lstDebug.Items.Add("Training ολοκληρώθηκε")

    End Sub



End Class
