Public Class MLP

    ' ===== ΔΟΜΗ ΔΙΚΤΥΟΥ =====
    Private _inputNeuronCount As Integer
    Private _hiddenNeuronCount As Integer
    Private _outputNeuronCount As Integer
    Private _learningRate As Double

    ' ===== ΒΑΡΗ =====
    Private _weightsInputHidden(,) As Double
    Private _weightsHiddenOutput(,) As Double

    ' ===== BIAS =====
    Private _biasHidden() As Double
    Private _biasOutput() As Double

    Private rnd As New Random()

    ' ===== ΤΕΛΕΥΤΑΙΕΣ ΤΙΜΕΣ (για visualization) =====
    Public LastHiddenOutputs() As Double
    Public LastPredictedValue As Double

    ' ===== READ-ONLY PROPERTIES =====
    Public ReadOnly Property HiddenNeuronCount As Integer
        Get
            Return _hiddenNeuronCount
        End Get
    End Property

    ' ===== CONSTRUCTOR =====
    Public Sub New(inputCount As Integer,
                   hiddenCount As Integer,
                   outputCount As Integer,
                   learningRateValue As Double)

        _inputNeuronCount = inputCount
        _hiddenNeuronCount = hiddenCount
        _outputNeuronCount = outputCount
        _learningRate = learningRateValue

        ReDim _weightsInputHidden(_hiddenNeuronCount - 1, _inputNeuronCount - 1)
        ReDim _biasHidden(_hiddenNeuronCount - 1)

        ReDim _weightsHiddenOutput(_outputNeuronCount - 1, _hiddenNeuronCount - 1)
        ReDim _biasOutput(_outputNeuronCount - 1)

        InitializeWeights(_weightsInputHidden)
        InitializeWeights(_weightsHiddenOutput)
    End Sub

    Private Sub InitializeWeights(ByRef weights(,) As Double)
        For i = 0 To weights.GetLength(0) - 1
            For j = 0 To weights.GetLength(1) - 1
                weights(i, j) = rnd.NextDouble() * 2 - 1
            Next
        Next
    End Sub

    Private Function Sigmoid(x As Double) As Double
        Return 1.0 / (1.0 + Math.Exp(-x))
    End Function

    Private Function SigmoidDerivative(y As Double) As Double
        Return y * (1 - y)
    End Function

    ' ===== ΕΝΑ ΒΗΜΑ ΕΚΠΑΙΔΕΥΣΗΣ =====
    Public Function TrainOneStep(inputA As Double,
                                 inputB As Double,
                                 targetValue As Double) As Double

        Dim inputVector() As Double = {inputA, inputB}
        Dim hiddenOutputs(_hiddenNeuronCount - 1) As Double

        ' ---- FORWARD: Input → Hidden ----
        For i = 0 To _hiddenNeuronCount - 1
            Dim sum As Double = _biasHidden(i)
            For j = 0 To _inputNeuronCount - 1
                sum += _weightsInputHidden(i, j) * inputVector(j)
            Next
            hiddenOutputs(i) = Sigmoid(sum)
        Next

        ' ---- FORWARD: Hidden → Output ----
        Dim outputSum As Double = _biasOutput(0)
        For i = 0 To _hiddenNeuronCount - 1
            outputSum += _weightsHiddenOutput(0, i) * hiddenOutputs(i)
        Next

        Dim predictedValue As Double = Sigmoid(outputSum)

        ' ---- ΑΠΟΘΗΚΕΥΣΗ ΓΙΑ UI ----
        LastHiddenOutputs = CType(hiddenOutputs.Clone(), Double())
        LastPredictedValue = predictedValue

        ' ---- LOSS ----
        Dim errorValue As Double = predictedValue - targetValue
        Dim lossValue As Double = 0.5 * errorValue * errorValue

        ' ---- BACKPROP: OUTPUT ----
        Dim deltaOutput As Double = errorValue * SigmoidDerivative(predictedValue)

        For i = 0 To _hiddenNeuronCount - 1
            _weightsHiddenOutput(0, i) -= _learningRate * deltaOutput * hiddenOutputs(i)
        Next
        _biasOutput(0) -= _learningRate * deltaOutput

        ' ---- BACKPROP: HIDDEN ----
        For i = 0 To _hiddenNeuronCount - 1
            Dim deltaHidden As Double =
                _weightsHiddenOutput(0, i) * deltaOutput * SigmoidDerivative(hiddenOutputs(i))

            For j = 0 To _inputNeuronCount - 1
                _weightsInputHidden(i, j) -= _learningRate * deltaHidden * inputVector(j)
            Next

            _biasHidden(i) -= _learningRate * deltaHidden
        Next

        Return lossValue
    End Function

End Class
