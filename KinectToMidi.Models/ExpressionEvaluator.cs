using Ciloci.Flee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectToMidi.Models
{
    /// <summary>
    /// Tools for evaluating expressions
    /// </summary>
    public class ExpressionEvaluator
    {
        private IDynamicExpression m_eDynamicExpression;
        private ExpressionContext m_contextExpression;
        /// <summary>
        /// Evaluates epression and returns calculated value
        /// </summary>
        /// <param name="sp"></param>
        public byte GetValue(float x, float y, float z, string expressionStr, byte defaultValue)
        {
            byte value = defaultValue;
            if (!string.IsNullOrEmpty(expressionStr))
            {
                if (!byte.TryParse(expressionStr, out value))
                {
                    try
                    {
                        if (m_eDynamicExpression == null)
                        {
                            InitDynamicExpression(expressionStr);
                        }
                        m_contextExpression.Variables["X"] = x;
                        m_contextExpression.Variables["Y"] = y;
                        m_contextExpression.Variables["Z"] = z;
                        // Evaluate the expressions
                        float result = Convert.ToSingle(m_eDynamicExpression.Evaluate());
                        if (result > 127)
                            result = 127;
                        else if (result < 0)
                            result = 0;
                        value = (byte)result;
                    }
                    catch (ExpressionCompileException ex)
                    {
                        //handle parse expression error
                    }
                }
            }
            return value;
        }

        /// <summary>
        /// Initialize dynamic expression
        /// </summary>
        /// <param name="context"></param>
        /// <param name="eDynamic"></param>
        /// <param name="expression"></param>
        public void InitDynamicExpression(string expression)
        {
            if (m_contextExpression == null)
            {
                m_contextExpression = new ExpressionContext();
                m_contextExpression.Imports.AddType(typeof(Math));
                m_contextExpression.Variables["X"] = 0F;
                m_contextExpression.Variables["Y"] = 0F;
                m_contextExpression.Variables["Z"] = 0F;
            }

            // Create a dynamic expression that evaluates to an Object
            m_eDynamicExpression = m_contextExpression.CompileDynamic(expression);
        }
    }
}
