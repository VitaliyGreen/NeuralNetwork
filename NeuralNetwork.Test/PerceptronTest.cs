using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetwork.Core.Perceptron;

namespace NeuralNetwork.Test
{
    [TestClass]
    public class PerceptronTest
    {
        [TestMethod]
        public void PerceptronWeightMatrixCountWith1HiddenLayer()
        {
            LayerStructure structure = new LayerStructure(5, new[]{2}, 3);
            Perceptron perceptron = new Perceptron(GetInitParamsByLayerStructure(structure));

            Assert.AreEqual(2, perceptron.Weights.Count());
        }

        [TestMethod]
        public void PerceptronWeightMatrixesDimension()
        {
            LayerStructure structure = new LayerStructure(10, new []{4, 4}, 7);
            Perceptron perceptron = new Perceptron(GetInitParamsByLayerStructure(structure));
            List<float[,]> expectedResult = new List<float[,]>
            {
                new float[10, 4],
                new float[4, 4],
                new float[4, 7]
            };

            bool areEqual = true;

            for (int i = 0; i < expectedResult.Count; i++)
            {
                if (expectedResult[i].GetLength(0) != perceptron.Weights[i].GetLength(0) ||
                    expectedResult[i].GetLength(1) != perceptron.Weights[i].GetLength(1))
                {
                    areEqual = false;
                    break;
                }
            }

            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void PerceptronWeightsAreFilled()
        {
            LayerStructure structure = new LayerStructure(5, new[] {9, 9}, 7);
            Perceptron perceptron = new Perceptron(GetInitParamsByLayerStructure(structure));

            perceptron.FillWeightsRandomly();

            bool isFilled = true;

            for (int i = 0; i < 5; i++)
            {
                float randomWeight = GetRandomWeight(perceptron.Weights);

                if (randomWeight < 0 || randomWeight > 1)
                {
                    isFilled = false;
                    break;
                }
            }

            Assert.IsTrue(isFilled);
        }

        [TestMethod]
        public void PerceptronWeightsAreTakenIntoAccount()
        {
            LayerStructure structure = new LayerStructure(9, new[] {7}, 5);
            Perceptron perceptron = new Perceptron(GetInitParamsByLayerStructure(structure));
            perceptron.FillWeightsRandomly();
            float[] inputVector = new[] {0.44f, 0.354f, 0.667f, 0, 0.454f, 0.48f, 0.456f, 0.44f, 0.456f};

            float[] outputVector = perceptron.CalculateOutput(inputVector);

            Assert.AreNotEqual(inputVector, outputVector, "Input and output vectors are same");
        }

        [TestMethod]
        public void PerceptronOutputIsFromZeroToOne()
        {
            LayerStructure structure = new LayerStructure(9, new[] { 6, 6 }, 5);
            Perceptron perceptron = new Perceptron(GetInitParamsByLayerStructure(structure));
            perceptron.FillWeightsRandomly();
            float[] inputVector = { 0.44f, 0.354f, 0.667f, 0, 0.454f, 0.48f, 0.456f, 0.44f, 0.456f };

            float[] outputVector = perceptron.CalculateOutput(inputVector);

            int correctValuesCount = outputVector.Count(value => value > 0 && value <= 1);

            Assert.AreEqual(outputVector.Count(), correctValuesCount);
        }

        [TestMethod]
        public void PerceptronWeightsAreChangedAfterTrainIteration()
        {
            LayerStructure structure = new LayerStructure(9, new []{7}, 5);
            Perceptron perceptron = new Perceptron(GetInitParamsByLayerStructure(structure));
            perceptron.FillWeightsRandomly();
            
            Assert.IsFalse(true);
        }

        private float GetRandomWeight(List<float[,]> perceptronWeights)
        {
            Random random = new Random();

            int weightIndex = random.Next(0, perceptronWeights.Count);
            int row = random.Next(0, perceptronWeights[weightIndex].GetLength(0));
            int column = random.Next(0, perceptronWeights[weightIndex].GetLength(1));

            return perceptronWeights[weightIndex][row, column];
        }

        private PerceptronInitParams GetInitParamsByLayerStructure(LayerStructure layerStructure)
        {
            return new PerceptronInitParams(
                layerStructure,
                ActivationFunction.Sigmoid,
                new TrainParams(0.1f, 0.1f));
        }
    }
}