using System;

namespace NeuralNetwork.Core.Perceptron
{
    public readonly struct TrainParams
    {
        public float TrainSpeed { get;}
        public float MaximalError { get;}

        public TrainParams(float trainSpeed, float maximalError)
        {
            if (trainSpeed > 0 && trainSpeed <= 1 &&
                maximalError > 0 && maximalError <= 1)
            {
                TrainSpeed = trainSpeed;
                MaximalError = maximalError;
            }
            else
            {
                throw new ArgumentException("Invalid train parameters. Values should be in (0,1] range.");
            }
        }
    }
}