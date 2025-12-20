Public Class MLP

    Private inputSize As Integer
    Private hiddenSize As Integer
    Private outputSize As Integer
    Private learningRate As Double

    Private W1(,) As Double
    Private b1() As Double

    Private W2(,) As Double
    Private b2() As Double

    Private rnd As New Random()

    Public Sub New(inputSize As Integer, hiddenSize As Integer, outputSize As Integer, lr As Double)
        Me.inputSize = inputSize
        Me.hiddenSize = hiddenSize
        Me.outputSize = outputSize
        Me.learningRate = lr

        ReDim W1(hiddenSize - 1, inputSize - 1)
        ReDim b1(hiddenSize - 1)

        ReDim W2(outputSize - 1, hiddenSize - 1)
        ReDim b2(outputSize - 1)

        InitWeights(W1)
        InitWeights(W2)
    End Sub

    Private Sub InitWeights(ByRef W(,) As Double)
        For i = 0 To W.GetLength(0) - 1
            For j = 0 To W.GetLength(1) - 1
                W(i, j) = rnd.NextDouble() * 2 - 1
            Next
        Next
    End Sub

    Private Function Sigmoid(x As Double) As Double
        Return 1.0 / (1.0 + Math.Exp(-x))
    End Function

    Private Function SigmoidDeriv(y As Double) As Double
        Return y * (1 - y)
    End Function

    Public Function Predict(x1 As Double, x2 As Double) As Double
        Dim x() As Double = {x1, x2}
        Dim h(hiddenSize - 1) As Double

        For i = 0 To hiddenSize - 1
            Dim sum As Double = b1(i)
            For j = 0 To inputSize - 1
                sum += W1(i, j) * x(j)
            Next
            h(i) = Sigmoid(sum)
        Next

        Dim output As Double = b2(0)
        For i = 0 To hiddenSize - 1
            output += W2(0, i) * h(i)
        Next

        Return Sigmoid(output)
    End Function

    Public Function TrainOne(x1 As Double, x2 As Double, yTrue As Double) As Double
        Dim x() As Double = {x1, x2}
        Dim h(hiddenSize - 1) As Double

        ' Forward
        For i = 0 To hiddenSize - 1
            Dim sum As Double = b1(i)
            For j = 0 To inputSize - 1
                sum += W1(i, j) * x(j)
            Next
            h(i) = Sigmoid(sum)
        Next

        Dim outputSum As Double = b2(0)
        For i = 0 To hiddenSize - 1
            outputSum += W2(0, i) * h(i)
        Next

        Dim yPred As Double = Sigmoid(outputSum)
        Dim errorVal As Double = yPred - yTrue
        Dim loss As Double = 0.5 * errorVal * errorVal

        ' Backprop output
        Dim dOut As Double = errorVal * SigmoidDeriv(yPred)

        For i = 0 To hiddenSize - 1
            W2(0, i) -= learningRate * dOut * h(i)
        Next
        b2(0) -= learningRate * dOut

        ' Backprop hidden
        For i = 0 To hiddenSize - 1
            Dim dHidden As Double = W2(0, i) * dOut * SigmoidDeriv(h(i))
            For j = 0 To inputSize - 1
                W1(i, j) -= learningRate * dHidden * x(j)
            Next
            b1(i) -= learningRate * dHidden
        Next

        Return loss
    End Function

End Class
