using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinectToMidi.Models;
using NUnit.Framework;
namespace KinectToMidi.Models.Tests
{
    [TestFixture()]
    public class ExpressionEvaluatorTests
    {
        [Test()]
        public void GetValue_SimpleExpression_ReturnsValue()
        {
            ExpressionEvaluator expressionEvaluator = new ExpressionEvaluator();
            var result = expressionEvaluator.GetValue(1, 2, 3, "X + Y * Z", 0);
            Assert.AreEqual(7, result);
        }

        [Test()]
        public void GetValue_InvalidExpression_ReturnsDefaultValue()
        {
            ExpressionEvaluator expressionEvaluator = new ExpressionEvaluator();
            var result = expressionEvaluator.GetValue(1, 2, 3, "", 0);
            Assert.AreEqual(0, result);
        }
    }
}
