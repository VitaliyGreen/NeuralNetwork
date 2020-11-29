using System;

namespace NeuralNetwork.Core.Perceptron
{
    public readonly struct PerceptronInitParams
    {
        public LayerStructure LayerStructure { get; }
        public Func<float, float> ActivationFunction { get; }
        public TrainParams TrainParameters { get; }
        public float Bias { get; }

        public PerceptronInitParams(
            LayerStructure layerStructure,
            Func<float, float> activationFunction,
            TrainParams trainParameters,
            float bias = 1)
        {
            LayerStructure = layerStructure;
            ActivationFunction = activationFunction;
            TrainParameters = trainParameters;
            Bias = bias;
        }
    }
}