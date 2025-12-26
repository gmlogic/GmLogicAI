Imports System.Security.Cryptography

Public Class FrmChatMLP

    ' ================= DATA =================
    Private words As List(Of String)
    Private vocab As List(Of String)
    Private wordToIndex As Dictionary(Of String, Integer)
    Private mlp As MLP
    Private currentTextPath As String = ""
    Private modelLoaded As Boolean = False

    ' ================= FORM LOAD =================
    Private Sub FrmChatMLP_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblOutput.Text = "..."
        lstDebug.Items.Clear()
        lstDebug.Items.Add("Έτοιμο – άνοιξε αρχείο")

        txtInput.Enabled = False
        btnPredict.Enabled = False
        btnTrain.Enabled = False

        ' default αρχείο (μόνο πρόταση)
        Dim defaultFile As String = "Q:\Vellas\Vellas-text\Vellas Ιωάννης.txt"
        If IO.File.Exists(defaultFile) Then
            currentTextPath = defaultFile
            txtFile.Text = defaultFile
            ofdText.FileName = defaultFile
            ofdText.InitialDirectory = IO.Path.GetDirectoryName(defaultFile)
            tmAutoLoad.Start()
        End If
    End Sub

    ' ================= TIMER AUTO LOAD =================
    Private Sub tmAutoLoad_Tick(sender As Object, e As EventArgs) Handles tmAutoLoad.Tick
        tmAutoLoad.Stop()
        If Not String.IsNullOrEmpty(currentTextPath) Then
            LoadTextFile(currentTextPath)
        End If
    End Sub

    ' ================= OPEN FILE =================
    Private Sub btnOpenFile_Click(sender As Object, e As EventArgs) Handles btnOpenFile.Click

        ofdText.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
        ofdText.Title = "Επέλεξε αρχείο κειμένου"

        If ofdText.ShowDialog() <> DialogResult.OK Then Exit Sub

        txtFile.Text = ofdText.FileName
        currentTextPath = ofdText.FileName

        txtInput.Enabled = False
        btnPredict.Enabled = False
        btnTrain.Enabled = False

        LoadTextFile(currentTextPath)
    End Sub

    ' ================= LOAD MODEL (NO TRAINING) =================
    Private Sub LoadTextFile(path As String)

        modelLoaded = False
        lstDebug.Items.Clear()
        lstDebug.Items.Add("Φόρτωση αρχείου...")

        txtInput.Enabled = False
        btnPredict.Enabled = False

        Dim basePath = Application.StartupPath
        Dim vocabPath = IO.Path.Combine(basePath, "mlp_vocab.txt")
        Dim weightPath = IO.Path.Combine(basePath, "mlp_weights.txt")

        If IO.File.Exists(vocabPath) AndAlso IO.File.Exists(weightPath) Then
            Try
                vocab = LoadVocab(vocabPath)
                wordToIndex = BuildWordToIndex(vocab)

                mlp = New MLP(vocab.Count * 2, 32, vocab.Count)
                mlp.LoadWeights(weightPath)

                lstDebug.Items.Add("Vocab + weights φορτώθηκαν ✔")
                modelLoaded = True

                txtInput.Enabled = True
                btnPredict.Enabled = True
                btnTrain.Enabled = True
                Exit Sub

            Catch ex As Exception
                lstDebug.Items.Add("⚠ Ασύμβατα weights – πάτησε Training")
            End Try
        Else
            lstDebug.Items.Add("ℹ Δεν υπάρχουν weights – πάτησε Training")
        End If

        btnTrain.Enabled = True
    End Sub

    ' ================= TRAIN =================
    Private Sub btnTrain_Click(sender As Object, e As EventArgs) Handles btnTrain.Click

        If String.IsNullOrEmpty(currentTextPath) Then
            MessageBox.Show("Άνοιξε πρώτα αρχείο κειμένου")
            Exit Sub
        End If

        btnTrain.Enabled = False
        btnPredict.Enabled = False
        txtInput.Enabled = False

        lstDebug.Items.Add("Ξεκινά training...")

        Dim rawText As String = My.Computer.FileSystem.ReadAllText(currentTextPath)
        rawText = rawText.ToLower()
        rawText = RemoveTones(rawText)
        rawText = CleanText(rawText)

        words = Tokenize(rawText)
        Dim freq = BuildWordFrequency(words)
        words = FilterWordsByFrequency(words, freq, 2)

        vocab = BuildVocab(words)
        wordToIndex = BuildWordToIndex(vocab)

        lstDebug.Items.Add("Words: " & words.Count)
        lstDebug.Items.Add("Vocab: " & vocab.Count)

        mlp = New MLP(vocab.Count * 2, 32, vocab.Count)

        TrainMLP(epochs:=20, lr:=0.1)

        Dim basePath = Application.StartupPath
        SaveVocab(IO.Path.Combine(basePath, "mlp_vocab.txt"), vocab)
        mlp.SaveWeights(IO.Path.Combine(basePath, "mlp_weights.txt"))

        lstDebug.Items.Add("Training ολοκληρώθηκε ✔")

        modelLoaded = True
        txtInput.Enabled = True
        btnPredict.Enabled = True
        btnTrain.Enabled = True

    End Sub

    ' ================= TRAIN LOOP =================
    Private Sub TrainMLP(epochs As Integer, lr As Double)

        pbTrain.Minimum = 0
        pbTrain.Maximum = 100
        pbTrain.Value = 0

        Dim totalSteps As Integer = epochs * (words.Count - 2)
        Dim currentStep As Integer = 0

        For epoch = 1 To epochs
            For i = 0 To words.Count - 3
                Dim inputVec = TwoWordOneHot(words(i), words(i + 1), vocab)
                Dim targetVec = OneHot(words(i + 2), vocab)
                mlp.Train(inputVec, targetVec, lr)

                currentStep += 1
                If currentStep Mod 1000 = 0 Then
                    pbTrain.Value = CInt((currentStep / totalSteps) * 100)
                    Application.DoEvents()
                End If
            Next
            lstDebug.Items.Add("Epoch " & epoch & " / " & epochs)
            Application.DoEvents()
        Next

        pbTrain.Value = 100
    End Sub

    ' ================= GENERATE TEXT =================
    Private Function GenerateText(seed As String, steps As Integer) As String

        Dim inputText = CleanText(RemoveTones(seed.ToLower()))
        Dim list = inputText.Split(" "c).ToList()
        If list.Count < 2 Then Return seed

        For i = 1 To steps
            Dim w1 = list(list.Count - 2)
            Dim w2 = list(list.Count - 1)
            If Not vocab.Contains(w1) OrElse Not vocab.Contains(w2) Then Exit For

            Dim vec = mlp.Predict(TwoWordOneHot(w1, w2, vocab))
            list.Add(SampleWord(vec, vocab, tbTemp.Value / 100.0))
        Next

        Return String.Join(" ", list)
    End Function

    ' ================= PREDICT =================
    Private Sub btnPredict_Click(sender As Object, e As EventArgs) Handles btnPredict.Click
        If Not modelLoaded Then Exit Sub
        lblOutput.Text = GenerateText(txtInput.Text, 20)
    End Sub

    ' ================= TEMPERATURE =================
    Private Sub tbTemp_Scroll(sender As Object, e As EventArgs) Handles tbTemp.Scroll
        lblTemp.Text = "Temperature: " & (tbTemp.Value / 100.0).ToString("0.00")
    End Sub

End Class
