Public Class Form1

    Private net As MLP
    Private epoch As Integer = 0
    Private running As Boolean = False

    Private X As Double(,) = {
        {0, 0},
        {0, 1},
        {1, 0},
        {1, 1}
    }

    Private Y As Double() = {0, 1, 1, 0}

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        net = New MLP(2, 4, 1, 0.5)
        UpdateUI(0)
    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        running = True
        Timer1.Start()
    End Sub

    Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStop.Click
        running = False
        Timer1.Stop()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Not running Then Return

        Dim loss As Double = 0

        For i As Integer = 1 To 50
            epoch += 1
            For j As Integer = 0 To 3
                loss += net.TrainOne(X(j, 0), X(j, 1), Y(j))
            Next
        Next

        UpdateUI(loss)
    End Sub

    Private Sub UpdateUI(loss As Double)
        lblEpoch.Text = "Epoch: " & epoch
        lblLoss.Text = "Loss: " & loss.ToString("0.000000")

        Dim sb As New System.Text.StringBuilder()
        sb.AppendLine("XOR predictions:")
        For i As Integer = 0 To 3
            Dim p = net.Predict(X(i, 0), X(i, 1))
            sb.AppendLine($"{X(i, 0)} XOR {X(i, 1)} -> {p:0.0000}")
        Next

        txtOutput.Text = sb.ToString()
    End Sub

End Class
