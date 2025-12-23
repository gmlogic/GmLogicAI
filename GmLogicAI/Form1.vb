Public Class Form1

    Private net As MLP1
    Private index As Integer = 0

    Private inputs As List(Of List(Of Double))
    Private targets As List(Of Double)

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        net = New MLP1(inputCount:=2, hiddenCount:=4, learningRate:=0.5)

        inputs = New List(Of List(Of Double)) From {
            New List(Of Double) From {0, 0},
            New List(Of Double) From {0, 1},
            New List(Of Double) From {1, 0},
            New List(Of Double) From {1, 1}
        }

        targets = New List(Of Double) From {0, 1, 1, 0}

        txtOutput.Text = "Πάτα Train"
    End Sub

    Private Sub btnTrain_Click(sender As Object, e As EventArgs) Handles btnTrain.Click

        net.TrainOne(inputs(index), targets(index))

        txtOutput.Text =
            $"Sample {index}" & vbCrLf &
            $"Input = ({inputs(index)(0)}, {inputs(index)(1)})" & vbCrLf &
            $"Target = {targets(index)}" & vbCrLf &
            $"(δες Output → Debug)"

        index = (index + 1) Mod inputs.Count
    End Sub

End Class
