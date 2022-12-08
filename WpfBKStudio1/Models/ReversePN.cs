using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfBKStudio1.Models
{
    public class ReversePN
    {
        private string _reverseExpression { get; set; }
        private Queue<string> _reversePolishNotation { get; set; }

        public ReversePN()
        {
        }

        public string GetReversePolishNotation(string originalExpression)
        {
            Stack<string> operationStack = new Stack<string>();
            _reversePolishNotation = new Queue<string>();
            string lastOperator = "-u"; // By default we wait unar operation
            for (int i = 0; i < originalExpression.Length; i++)
            {
                string token = originalExpression[i].ToString();

                if (MathParser.IsDelimeter(token))
                {
                    continue;
                } else if (Char.IsDigit(originalExpression[i]))
                {
                    string digit = string.Empty;

                    while (!MathParser.IsDelimeter(originalExpression[i]) && (!MathParser.IsOperator(originalExpression[i])) &&
                           originalExpression[i] != '(' && originalExpression[i] != ')')
                    {
                        digit += originalExpression[i];
                        i++;

                        if (i == originalExpression.Length) break;
                        lastOperator = String.Empty;
                    }
                    _reversePolishNotation.Enqueue(digit.Replace('.', ','));
                    i--;
                } else if (MathParser.IsOperator(token))
                {
                    if(!String.IsNullOrEmpty(lastOperator))
                    {
                        token = "-u";
                    }
                    while (operationStack.Count > 0 && MathParser.IsOperator(operationStack.Peek()) &&
                          (MathParser.IsLeftAssociative(token) && MathParser.GetPriority(token) <= MathParser.GetPriority(operationStack.Peek()) ||
                          MathParser.IsRightAssociative(token) && MathParser.GetPriority(token) < MathParser.GetPriority(operationStack.Peek())))
                    {
                        if (operationStack.Peek() != "-u")
                        {
                            _reversePolishNotation.Enqueue(operationStack.Pop());
                        } else
                        {
                            operationStack.Pop();
                            _reversePolishNotation.Enqueue("-1");
                            _reversePolishNotation.Enqueue("*");
                        }
                    }
                    operationStack.Push(token);
                    lastOperator = token;
                } else if (token == "(")
                {
                    operationStack.Push(token);
                    lastOperator = String.Empty;
                } else if (token == ")")
                {
                    lastOperator = String.Empty;
                    while (operationStack.Count > 0 && operationStack.Peek() != "(")
                    {
                        _reversePolishNotation.Enqueue(operationStack.Pop());
                        if (operationStack.Count == 0)
                            throw new FormatException("Mismatched parentheses");
                    }
                    operationStack.Pop();

                    if (operationStack.Count > 0)
                    {
                        string top = operationStack.Peek();
                        if (!MathParser.IsOperator(top) && top != ")" && top != "(")
                        {
                            _reversePolishNotation.Enqueue(operationStack.Pop());
                        }
                    }
                }
            }
            while (operationStack.Count > 0)
            {
                if (operationStack.Peek() == "(")
                    throw new FormatException("Mismatched parentheses");
                else if (operationStack.Peek() == "-u")
                {
                    operationStack.Pop();
                    _reversePolishNotation.Enqueue("-1");
                    _reversePolishNotation.Enqueue("*");
                } else
                    _reversePolishNotation.Enqueue(operationStack.Pop());
            }

            _reverseExpression = String.Join(' ', _reversePolishNotation.ToArray());
            return _reverseExpression;
        }

        public string Calculate()
        {
            double result = 0;
            string[] reversePolishNotation = _reversePolishNotation.ToArray();

            Stack<double> temp = new Stack<double>();

            for (int i = 0; i < reversePolishNotation.Length; i++)
            {
                if(!MathParser.IsOperator(reversePolishNotation[i]))
                {
                    temp.Push(double.Parse(reversePolishNotation[i]));
                } else
                {
                    double x = temp.Pop();
                    double y = temp.Pop();

                    switch(reversePolishNotation[i])
                    {
                        case "+": result = y + x; break;
                        case "-": result = y - x; break;
                        case "*": result = y * x; break;
                        case "/": result = y / x; break;
                        case "^": result = Math.Pow(y, x); break;
                    }
                    temp.Push(result);
                }
            }

            return result.ToString();
        }
    }
}
