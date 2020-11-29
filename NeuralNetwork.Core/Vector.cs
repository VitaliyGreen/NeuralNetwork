using System;
using System.Linq;

namespace NeuralNetwork.Core
{
    public static class Vector
    {
        public static float Multiply(this float[] left, float[] right)
        {
            if (left.Length == right.Length)
            {
                float result = 0;

                for (int index = 0; index < left.Length; index++)
                {
                    result += left[index] * right[index];
                }

                return result;
            }

            throw new ArgumentException("Invalid arguments. Can not multiply vectors with different length");
        }
    }
}