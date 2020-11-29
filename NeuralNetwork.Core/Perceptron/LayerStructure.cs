using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork.Core.Perceptron
{
    public struct LayerStructure
    {
        public int InputLayerNodesCount { get; set; }
        public IEnumerable<int> HiddenLayers { get; set; }
        public int OutputLayerNodesCount { get; set; }

        public LayerStructure(int inputsCount, IEnumerable<int> hiddenLayers, int outputsCount)
        {

            if (hiddenLayers.Any())
            {
                InputLayerNodesCount = inputsCount;
                HiddenLayers = hiddenLayers;
                OutputLayerNodesCount = outputsCount;
            }
            else
            {
                throw new ArgumentException("Hidden layer can not be empty");
            }
        }
    }
}