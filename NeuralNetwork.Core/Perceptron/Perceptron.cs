using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork.Core.Perceptron
{
    /// <summary>
    /// Represents a Neural Network object
    /// </summary>
    public class Perceptron
    {
        public LayerStructure LayerStructure { get; }

        public Func<float, float> ActivationFunction { get; }

        public float Bias { get; }
        
        public TrainParams TrainParams { get; }

        public List<float[,]> Weights { get; private set; }

        public Perceptron(PerceptronInitParams initializationParameters)
        {
            LayerStructure = initializationParameters.LayerStructure;
            ActivationFunction = initializationParameters.ActivationFunction;
            TrainParams = initializationParameters.TrainParameters;
            Bias = initializationParameters.Bias;
            
            CreateEmptyWeights(LayerStructure);
        }

        public void Train(float[] inputVector, float[] correctAnswers)
        {
            throw new NotImplementedException("Training of Perceptron is not implemented yet. ");


        }

        public float[] CalculateOutput(float[] inputVector)
        {
            float[] values = inputVector;
            
            foreach (float[,] weights in Weights)
            {
                float[] layerOutput = GetLayerOutput(values, weights);
                values = layerOutput;
            }

            return values;
        }
        
        public void FillWeightsRandomly()
        {
            Random random = new Random();

            foreach (float[,] weights in Weights)
            {
                for (int row = 0; row < weights.GetLength(0); row++)
                {
                    for (int column = 0; column < weights.GetLength(1); column++)
                    {
                        weights[row, column] = Convert.ToSingle(random.NextDouble());
                    }
                }
            }
        }

        private float[] GetLayerOutput(float[] inputVector, float[,] weights)
        {
            if (inputVector.Length == weights.GetLength(0))
            {
                int outLength = weights.GetLength(1);
                float[] layerOutput = new float[outLength];

                for (int i = 0; i < outLength; i++)
                {
                    float[] column = GetColumn(i, weights);
                    layerOutput[i] = ActivationFunction(column.Multiply(inputVector) + Bias);
                }

                return layerOutput;
            }

            throw new ArgumentException("Wrong number of inputs");
        }

        private float[] GetColumn(int columnIndex, float[,] weights)
        {
            int numberOfRows = weights.GetLength(0);
            float[] column = new float[numberOfRows];

            for (int row = 0; row < numberOfRows; row++)
            {
                column[row] = weights[row, columnIndex];
            }

            return column;
        }

        private void CreateEmptyWeights(LayerStructure layerStructure)
        {
            Weights = new List<float[,]>();

            float[,] firstLayerWeight = new float[layerStructure.InputLayerNodesCount,
                layerStructure.HiddenLayers.ElementAt(0)];
            Weights.Add(firstLayerWeight);

            for (int i = 1; i < layerStructure.HiddenLayers.Count(); i++)
            {
                float[,] hiddenLayerWeights =
                    new float[layerStructure.HiddenLayers.ElementAt(i - 1), layerStructure.HiddenLayers.ElementAt(i)];
                Weights.Add(hiddenLayerWeights);
            }

            float[,] lastLayerWeight =
                new float[layerStructure.HiddenLayers.Last(), layerStructure.OutputLayerNodesCount];
            Weights.Add(lastLayerWeight);
        }
    }
}