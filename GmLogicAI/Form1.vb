Public Class Form1

    Private neuralNetwork As MLP
    Private currentEpoch As Integer = 0
    Private trainingIndex As Integer = 0

    ' ===== ΔΕΔΟΜΕΝΑ XOR =====
    Private trainingInputs As Double(,) = {
        {0, 0},
        {0, 1},
        {1, 0},
        {1, 1}
    }

    Private trainingTargets As Double() = {0, 1, 1, 0}

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        neuralNetwork = New MLP(
            inputCount:=2,
            hiddenCount:=4,
            outputCount:=1,
            learningRateValue:=0.5)

        lblEpoch.Text = "Epoch: 0"
        lblLoss.Text = "Loss: 0"
        txtOutput.Text = "Πάτα 'Train One' για ένα βήμα εκπαίδευσης"
    End Sub

    ' ===== ΚΟΥΜΠΙ: TRAIN ONE STEP =====
    Private Sub btnStep_Click(sender As Object, e As EventArgs) Handles btnStep.Click

        Dim inputA As Double = trainingInputs(trainingIndex, 0)
        Dim inputB As Double = trainingInputs(trainingIndex, 1)
        Dim targetValue As Double = trainingTargets(trainingIndex)

        Dim lossValue As Double =
            neuralNetwork.TrainOneStep(inputA, inputB, targetValue)

        currentEpoch += 1
        ShowTrainingDetails(inputA, inputB, targetValue, lossValue)

        trainingIndex = (trainingIndex + 1) Mod trainingTargets.Length
    End Sub

    ' ===== ΑΠΕΙΚΟΝΙΣΗ ΤΙΜΩΝ =====
    Private Sub ShowTrainingDetails(inputA As Double,
                                    inputB As Double,
                                    targetValue As Double,
                                    lossValue As Double)

        lblEpoch.Text = "Epoch: " & currentEpoch
        lblLoss.Text = "Loss: " & lossValue.ToString("0.000000")

        Dim sb As New System.Text.StringBuilder()

        sb.AppendLine("=== TRAIN ONE STEP ===")
        sb.AppendLine($"Inputs: ({inputA}, {inputB})")
        sb.AppendLine($"Target Value: {targetValue}")
        sb.AppendLine()

        sb.AppendLine("Hidden Layer Outputs:")
        For i = 0 To neuralNetwork.HiddenNeuronCount - 1
            sb.AppendLine($"  Hidden[{i}] = {neuralNetwork.LastHiddenOutputs(i):0.0000}")
        Next

        sb.AppendLine()
        sb.AppendLine($"Predicted Value: {neuralNetwork.LastPredictedValue:0.0000}")
        sb.AppendLine($"Error: {(neuralNetwork.LastPredictedValue - targetValue):0.0000}")
        sb.AppendLine($"Loss: {lossValue:0.000000}")

        txtOutput.Text = sb.ToString()
    End Sub

End Class
