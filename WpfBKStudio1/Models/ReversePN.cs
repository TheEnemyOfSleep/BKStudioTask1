﻿using System;
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

        private void ReversePolishNotationEnqueue(string token)
        {
            if (token != "-u")
                _reversePolishNotation.Enqueue(token);
            else
            {
                _reversePolishNotation.Enqueue("-1");
                _reversePolishNotation.Enqueue("*");
            }
        }

        public string GetReversePolishNotation(string originalExpression)
        {
            Stack<string> operationStack = new Stack<string>();
            _reversePolishNotation = new Queue<string>();
            string lastOperator = "-u"; // By default we wait unar operation

            // Shunting yard algorithm with unary support
            for (int i = 0; i < originalExpression.Length; i++)
            {
                string token = originalExpression[i].ToString();

                if (MathParser.IsDelimeter(token)) //If token is whitespace or =
                {
                    continue;
                } else if (Char.IsDigit(originalExpression[i]))
                {
                    string digit = string.Empty;

                    // Get all multi - digit numbers;
                    while (!MathParser.IsDelimeter(originalExpression[i]) && (!MathParser.IsOperator(originalExpression[i])) &&
                           originalExpression[i] != '(' && originalExpression[i] != ')')
                    {
                        digit += originalExpression[i];
                        i++;

                        if (i == originalExpression.Length) break;
                        lastOperator = String.Empty;
                    }
                    //Put number into enqueue
                    ReversePolishNotationEnqueue(digit.Replace('.', ','));
                    i--;
                } else if (MathParser.IsOperator(token))
                {
                    //Add unary support
                    if (!String.IsNullOrEmpty(lastOperator))
                    {
                        if (token == "-")
                            token = "-u";
                        else
                            throw new FormatException("Incorrect expression format:\n Operators are repeated.");
                    }
                    //Base on priority and associative get operators
                    while (operationStack.Count > 0 && MathParser.IsOperator(operationStack.Peek()) &&
                          (MathParser.IsLeftAssociative(token) && MathParser.GetPriority(token) <= MathParser.GetPriority(operationStack.Peek()) ||
                          MathParser.IsRightAssociative(token) && MathParser.GetPriority(token) < MathParser.GetPriority(operationStack.Peek())))
                    {
                        ReversePolishNotationEnqueue(operationStack.Pop());
                    }
                    operationStack.Push(token);
                    lastOperator = token;
                } else if (token == "(")
                {
                    operationStack.Push(token);
                    lastOperator = String.Empty;
                } else if (token == ")")
                {
                    //Remove the closed parenthesis from the operation stack
                    lastOperator = String.Empty;
                    while (operationStack.Count > 0 && operationStack.Peek() != "(")
                    {
                        ReversePolishNotationEnqueue(operationStack.Pop());
                        if (operationStack.Count == 0)
                            throw new FormatException("Incorrect expression format:\n Mismatched parentheses");
                    }
                    operationStack.Pop();

                    if (operationStack.Count > 0)
                    {
                        string top = operationStack.Peek();
                        if (!MathParser.IsOperator(top) && top != ")" && top != "(")
                        {
                            ReversePolishNotationEnqueue(operationStack.Pop());
                        }
                    }
                }
            }
            while (operationStack.Count > 0)
            {
                if (operationStack.Peek() == "(")
                    throw new FormatException("Incorrect expression format:\n Mismatched parentheses");
                else
                    ReversePolishNotationEnqueue(operationStack.Pop());
            }

            _reverseExpression = String.Join(' ', _reversePolishNotation.ToArray());
            return _reverseExpression;
        }

        public string Calculate()
        {
            double result = 0;
            List<string> reversePolishNotation = _reversePolishNotation.ToList();


            Stack<double> temp = new Stack<double>();

            for (int i = 0; i < reversePolishNotation.Count; i++)
            {
                if (!MathParser.IsOperator(reversePolishNotation[i]))
                {
                    temp.Push(double.Parse(reversePolishNotation[i]));
                } else
                {
                    double x = temp.Pop();
                    double y = temp.Pop();

                    switch(reversePolishNotation[i])
                    {
                        case "+": result = y + x; break;
                        case "-": case "−": result = y - x; break;
                        case "*": case "×": result = y * x; break;
                        case "/": case "÷": result = y / x; break;
                        case "^": result = Math.Pow(y, x); break;
                    }
                    temp.Push(result);
                }
            }

            return result.ToString();
        }
    }
}
