using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionBuilder
{
    class ReversPolishNotation
    {
        private List<object> Reverspolishnotation { get; set; } = new List<object>();

        private List<string> OperationsList { get; set; } = new List<string>()
        {
            "!", "ln", "log", "cos", "sin", "tg", "ctg", "arcsin", "arccos", "arctg", "arcctg", "^", "Sqrt", "+", "-",
            "/", "*"
        };

        public List<object> ResultToRpn { get; set; } = new List<object>();
        private string Expression { get; set; }

        public ReversPolishNotation(string i)
        {
            Expression = i;
            ComplementMultiplication();
            ParsExpression();
            for (int l = 0; l < Reverspolishnotation.Count; l++)
            {
                Console.Write(Reverspolishnotation[l]);
            }
            GetReversPolishNotation();

        }

        private void ComplementMultiplication()
        {
            string exp = null;
            for (int i = 0; i < Expression.Length; i++)
            {
                exp += Expression[i].ToString();
                if (i < Expression.Length - 1 && char.IsDigit(Expression[i]) && "lsqtaxyzpe".Contains(Expression[i + 1]))
                {
                    exp += "*";
                }
                if (i < Expression.Length - 1 && "xyzei".Contains(Expression[i]) && !"+-*/)".Contains(Expression[i + 1]))
                {
                    if (Expression[i] == 'g' && Expression[i - 2] == 'l' || Expression[i] == 'n' && Expression[i - 1] == 'l' || Expression[i] == 'i' && Expression[i - 1] == 's')
                    {
                        continue;
                    }
                    exp += "*";
                }
            }
            Expression = exp;
        }
        
        public List<object> GetRpn()
        {
            return Reverspolishnotation;
        }

        private void ParsExpression()
        {
            List<object> parsExpression = new List<object>();
            string result = null;

            for (int i = 0; i < Expression.Length; i++)
            {
                if ("0123456789,.".Contains(Expression[i]))
                {
                    while ("0123456789,.".Contains(Expression[i]))
                    {
                        if (",.".Contains(Expression[i]))
                        {
                            result += ",";
                        }
                        else
                        {
                            result += Expression[i];
                        }
                        try
                        {
                            if (char.IsDigit(Expression[i + 1]) || ",.".Contains(Expression[i + 1]))
                            {
                                i++;
                            }
                            else
                            {
                                break;
                            }
                        }
                        catch
                        {
                            break;
                        }
                    }
                    parsExpression.Add(Convert.ToDouble(result));
                    result = null;
                    continue;
                }
                if (char.IsWhiteSpace(Expression[i]))
                {
                    continue;
                }
                if ("()".Contains(Expression[i]))
                {
                    parsExpression.Add(new Parenthessis(Expression[i]));
                    continue;
                }
                if (!char.IsDigit(Expression[i]) && !".,()".Contains(Expression[i]))
                {
                    if ("xyz".Contains(Expression[i]))
                    {
                        parsExpression.Add(Expression[i]);
                    }
                    else
                    {
                        while (!char.IsDigit(Expression[i]) && !".,()+-*/^e".Contains(Expression[i]))
                        {
                            result += Expression[i];
                            try
                            {
                                if (!char.IsDigit(Expression[i + 1]) && !".,()xyz+-*/^e".Contains(Expression[i + 1]) && !char.IsWhiteSpace(Expression[i + 1]))
                                {
                                    i++;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            catch
                            {
                                break;
                            }
                        }
                        if ("+-*/^e".Contains(Expression[i]))
                        {
                            result += Expression[i];
                        }
                        ChoseOperation(result, out Operation y);
                        parsExpression.Add(y);
                        result = null;
                    }
                }
            }
            Reverspolishnotation = parsExpression;
        }

        private void ChoseOperation(object x, out Operation y)
        {
            y = x.ToString() switch
            {
                @"+" => new Plus(),
                @"-" => new Minus(),
                @"*" => new Mult(),
                @"/" => new Div(),
                @"\" => new Div(),
                @"log" => new Log(),
                @"ln" => new Ln(),
                @"sin" => new Sin(),
                @"cos" => new Cos(),
                @"tg" => new Tan(),
                @"^" => new Degree(),
                @"sqrt" => new Sqrt(),
                @"!" => new Factorial(),
                @"pi" => new PI(),
                @"e" => new Exponent(),
                _ => null
            };
        }

        private bool CheckLeftBracket(object obj)
        {
            if (obj is Parenthessis parenthessis)
            {
                return parenthessis.IsOpening;
            }
            return false;
        }

        private bool CheckForPrefix(object obj)
        {
            if (obj is Operation operation)
            {
                return operation.IsPrefix;
            }
            
            return false;
        }

        private void GetReversPolishNotation()
        {
            Stack<object> outputStack = new Stack<object>();
            List<object> outputList = new List<object>();
            for (int i = 0; i < Reverspolishnotation.Count; i++)
            {
                object token = Reverspolishnotation[i];
                if ("xyz".Contains(Convert.ToString(token)))
                {
                    outputList.Add(token);
                }
                if (token is double)
                {
                    outputList.Add(token);
                }
                if (token is Operation operation)
                {
                    if (token is Operation operation1)
                    {
                        if (!operation1.IsPrefix && !operation1.IsBinaryOperation)
                        {
                            outputList.Add(operation1);
                        }
                    }
                }
                if (token is Operation operation2)
                {
                    if (operation2.IsPrefix)
                    {
                        outputStack.Push(operation2);
                    }
                }
                if (token is Parenthessis parenthessis)
                {
                    if (parenthessis.IsOpening)
                    {
                        outputStack.Push(parenthessis);
                    }
                    else
                    {
                        object obj = outputStack.Peek();
                        while (!CheckLeftBracket(obj))
                        {
                            outputList.Add(outputStack.Pop());
                            obj = outputStack.Peek();
                        }
                        if (CheckLeftBracket(outputStack.Peek()))
                        {
                            outputStack.Pop();
                        }
                    }
                }

                if (token is Operation operation3)
                {
                    if (operation3.IsBinaryOperation)
                    {
                        if (outputStack.Count != 0)
                        {
                            object obj = outputStack.Peek();
                            while ((CheckForPrefix(obj) || PriorityComparison(token, outputStack.Peek()) ||
                                   BinaryComparison(outputStack.Peek()) && PriorityEqualse(token, outputStack.Peek())) && outputStack.Count > 0)
                            {
                                outputList.Add(outputStack.Pop());
                            }
                        }

                        outputStack.Push(token);
                    }
                }
            }

            while (outputStack.Count > 0)
            {
                outputList.Add(outputStack.Pop());
            }

            ResultToRpn = outputList;
        }

        private bool PriorityEqualse(object o1, object stackPeek)
        {
            if (o1 is Operation op && stackPeek is Operation sp)
            {
                return op.Prior == sp.Prior;
            }
            return false;
        }
        private bool BinaryComparison(object obj)
        {
            if (obj is Operation op)
            {
                return op.IsBinaryOperation;
            }
            return false;
        }

        private bool PriorityComparison(object o1, object StackPeek)
        {
            if (o1 is Operation o2 && StackPeek is Operation stackPeek)
            {
                return o2.Prior < stackPeek.Prior;
            }

            return false;
        }
    }
}
