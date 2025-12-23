Public Class MLP1

    ' ===== ΔΟΜΗ ΔΙΚΤΥΟΥ =====
    Private inputCount As Integer
    Private hiddenCount As Integer
    Private learningRate As Double

    ' ===== ΒΑΡΗ =====
    ' weightsInputHidden(h)(i)
    Private weightsInputHidden As List(Of List(Of Double))
    ' weightsHiddenOutput(0)(h)
    Private weightsHiddenOutput As List(Of Double)

    ' ===== BIAS =====
    Private biasHidden As List(Of Double)
    Private biasOutput As Double

    Private rnd As New Random()

    ' ===== CONSTRUCTOR =====
    Public Sub New(inputCount As Integer, hiddenCount As Integer, learningRate As Double)

        Me.inputCount = inputCount
        Me.hiddenCount = hiddenCount
        Me.learningRate = learningRate

        InitializeNetwork()
        DebugDump("INIT")
    End Sub

    ' ===== INIT =====
    Private Sub InitializeNetwork()

        weightsInputHidden = New List(Of List(Of Double))
        For h = 0 To hiddenCount - 1
            Dim row As New List(Of Double)
            For i = 0 To inputCount - 1
                row.Add(RandomWeight())
            Next
            weightsInputHidden.Add(row)
        Next

        weightsHiddenOutput = New List(Of Double)
        For h = 0 To hiddenCount - 1
            weightsHiddenOutput.Add(RandomWeight())
        Next

        biasHidden = New List(Of Double)
        For h = 0 To hiddenCount - 1
            biasHidden.Add(0)
        Next

        biasOutput = 0
    End Sub

    Private Function RandomWeight() As Double
        Return rnd.NextDouble() * 2 - 1
    End Function

    ' ===== ACTIVATION =====
    Private Function Sigmoid(x As Double) As Double
        Return 1 / (1 + Math.Exp(-x))
    End Function

    Private Function SigmoidDeriv(y As Double) As Double
        Return y * (1 - y)
    End Function

    ' ===== TRAIN ONE SAMPLE =====
    Public Sub TrainOne(inputVector As List(Of Double), target As Double)

        ' --- FORWARD (Hidden) ---
        Dim hiddenOut As New List(Of Double)

        For h = 0 To hiddenCount - 1
            Dim net As Double = biasHidden(h)
            For i = 0 To inputCount - 1
                net += weightsInputHidden(h)(i) * inputVector(i)
            Next
            hiddenOut.Add(Sigmoid(net))
        Next

        ' --- FORWARD (Output) ---
        Dim outputNet As Double = biasOutput
        For h = 0 To hiddenCount - 1
            outputNet += weightsHiddenOutput(h) * hiddenOut(h)
        Next

        Dim prediction As Double = Sigmoid(outputNet)

        ' --- ERROR ---
        Dim errorValue As Double = prediction - target
        Dim deltaOutput As Double = errorValue * SigmoidDeriv(prediction)

        ' --- BACKPROP OUTPUT ---
        For h = 0 To hiddenCount - 1
            weightsHiddenOutput(h) += -learningRate * deltaOutput * hiddenOut(h)
        Next
        biasOutput += -learningRate * deltaOutput

        ' --- BACKPROP HIDDEN ---
        For h = 0 To hiddenCount - 1
            Dim deltaHidden As Double =
                weightsHiddenOutput(h) * deltaOutput * SigmoidDeriv(hiddenOut(h))

            For i = 0 To inputCount - 1
                weightsInputHidden(h)(i) += -learningRate * deltaHidden * inputVector(i)
            Next

            biasHidden(h) += -learningRate * deltaHidden
        Next

        DebugDump("AFTER TRAIN")
    End Sub

    ' ===== DEBUG =====
    Private Sub DebugDump(title As String)

        Debug.Print($"=== {title} ===")

        Debug.Print("-- weightsInputHidden --")
        For h = 0 To hiddenCount - 1
            For i = 0 To inputCount - 1
                Debug.Print($"W1({h},{i}) = {weightsInputHidden(h)(i):0.000000}")
            Next
            Debug.Print($"b1({h}) = {biasHidden(h):0.000000}")
        Next

        Debug.Print("-- weightsHiddenOutput --")
        For h = 0 To hiddenCount - 1
            Debug.Print($"W2({h}) = {weightsHiddenOutput(h):0.000000}")
        Next
        Debug.Print($"b2 = {biasOutput:0.000000}")
    End Sub

End Class
