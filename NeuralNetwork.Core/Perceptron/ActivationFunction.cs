using System;

namespace NeuralNetwork.Core.Perceptron
{
    public static class ActivationFunction
    {
        public static Func<float, float> Sigmoid => 
            x => (float) (1 / (1 + Math.Pow(Math.E, -x)));

        public static Func<float, float> ReLU =>
            x => x < 0 ? 0 : x;
    }
}