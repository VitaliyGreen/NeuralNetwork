using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetwork.Core;

namespace NeuralNetwork.Test
{
    [TestClass]
    public class VectorTest
    {
        private readonly string _message = "Actual result {0} is unexpected. Expected is {1}";

        [TestMethod]
        public void VectorMultiplication1X1()
        {
            float[] vectorA = {2.2f};
            float[] vectorB = {4.4f};
            float expectedResult = 9.68f;

            float result = vectorA.Multiply(vectorB);

            Assert.AreEqual(Math.Round(expectedResult, 2), Math.Round(result, 2), String.Format(_message, result, expectedResult));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void VectorMultiplyIncorrectFormat()
        {
            float[] a = {2.4f};
            float[] b = {4.5f, 1.2f};

            a.Multiply(b);
        }

        [TestMethod]
        public void VectorMultiplication()
        {
            float[] a = {1.2f, 5.1f, 2.4f, 25.6f, 1.2f, 0.144f};
            float[] b = {0.486f, 5.24f, 6.12f, 0.568f, 65.21f, 6.001f};
            float expectedResult = 135.652f;

            float actualResult = a.Multiply(b);

            Assert.AreEqual(Math.Round(expectedResult, 3), Math.Round(actualResult, 3), String.Format(_message, actualResult, expectedResult));
        }
    }
}