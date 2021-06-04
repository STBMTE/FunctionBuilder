using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionBuilder
{
    class ReversPolishNotation
    {
        private Stack<Parenthessis> Parenthesses = new Stack<Parenthessis>();
        private List<object> Reverspolishnotation = new List<object>();
        private List<string> OperationsList = new List<string>() { "!", "ln", "log", "cos", "sin", "tg", "ctg", "arcsin", "arccos", "arctg", "arcctg", "^", "Sqrt", "+", "-", "/", "*" };
        public List<object> resultToRPN = new List<object>();
        private string Expression { get; set; }

        public ReversPolishNotation(string i)
        {
            Expression = i;
            ComplementMultiplication();
            Console.WriteLine(Expression);
            ParsExpression();
            for (int l = 0; l < Reverspolishnotation.Count; l++)
            {
                Console.WriteLine(Reverspolishnotation[l]);
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
        private bool isDigit(object x)
        {
            try
            {
                Convert.ToChar(x);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public List<object> getRPN()
        {
            return Reverspolishnotation;
        }

        private void ParsExpression()
        {
            List<object> parsExpression = new List<object>();
            string result = null;
            bool flagEnd = false;

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
                    parsExpression.Add(result);
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
                    continue;
                }
            }
            Reverspolishnotation = parsExpression;
        }

        private void ChoseOperation(object x, out Operation y)
        {
            y = null;
            switch (x.ToString())
            {
                case @"+":
                    y = new Plus();
                    break;
                case @"-":
                    y = new Minus();
                    break;
                case @"*":
                    y = new Mult();
                    break;
                case @"/":
                    y = new Div();
                    break;
                case @"\":
                    y = new Div();
                    break;
                case @"log":
                    y = new Log();
                    break;
                case @"ln":
                    y = new Ln();
                    break;
                case @"sin":
                    y = new Sin();
                    break;
                case @"cos":
                    y = new Cos();
                    break;
                case @"tg":
                    y = new Tan();
                    break;
                case @"^":
                    y = new Degree();
                    break;
                case @"sqrt":
                    y = new Sqrt();
                    break;
                case @"!":
                    y = new Factorial();
                    break;
                case @"pi":
                    y = new PI();
                    break;
                case @"e":
                    y = new Exponent();
                    break;
            }
        }

        private bool CheckLeftBracket(object obj)
        {
            if (obj is Parenthessis parenthessis)
            {
                return parenthessis.IsOpening;
            }
            return false;
        }

        private void GetReversPolishNotation()
        {
            Stack<object> OutputStack = new Stack<object>();
            List<object> OutputList = new List<object>();
            int j = Reverspolishnotation.Count;
            for (j = 0; j < Reverspolishnotation.Count; j++)
            {
                object token = Reverspolishnotation[j];
                if (token is Operation operation || token is Parenthessis parenth)
                {
                    if (OutputStack.Count > 0 && !CheckLeftBracket(token))
                    {
                        if (token is Parenthessis parenthis)
                        {
                            if (!parenthis.IsOpening)
                            {
                                object obj = OutputStack.Pop();
                                while (!CheckLeftBracket(obj))
                                {
                                    OutputList.Add(obj);
                                    obj = OutputStack.Pop();
                                }/*
                                OutputStack.Pop();*/
                            }
                        }
                        if (OutputStack.Count > 0 && GetPrior(token) < GetPrior(OutputStack.Peek()))
                        {
                            OutputStack.Push(token);
                        }
                        else
                        {
                            while (OutputStack.Count > 0 && GetPrior(token) <= GetPrior(OutputStack.Peek()))
                            {
                                OutputList.Add(OutputStack.Pop());
                            }
                            OutputStack.Push(token);
                        }

                    }
                    else
                    {
                        OutputStack.Push(token);
                    }
                }
                else
                {
                    OutputList.Add(token);
                }
                /*object token = Reverspolishnotation[j];
                if(token is Double number)
                {
                    OutputList.Add(token);
                }
                if(token is Operation operato)
                {
                    if (!operato.IsPrefix || operato.Name == "e" || operato.Name == "pi")
                    {
                        OutputList.Add(token);
                    }
                    else
                    {
                        OutputStack.Push(token);
                    }
                }
                if(token is Parenthessis parenth)
                {
                    if (parenth.IsOpening)
                    {
                        OutputStack.Push(token);
                    }
                    else
                    {
                        while (Reverspolishnotation[j] is Parenthessis)
                        {
                            object token1 = Reverspolishnotation[j];
                            if(token1 is Parenthessis pare)
                            {
                                if(pare.IsOpening){
                                    break;
                                }
                                OutputList.Add(OutputStack.Pop());
                            }
                            j++;
                        }
                    }
                }*/
            }
            if (OutputStack.Count > 0)
            {
                foreach (object obj in OutputStack)
                {
                    if(obj is Parenthessis parenthessis)
                    {
                        if (!parenthessis.IsOpening)
                        {
                            continue;
                        }
                    }
                    OutputList.Add(obj);
                }
            }
            resultToRPN = OutputList;
        }

        private int GetPrior(object obj)
        {
            if (obj is Operation operation)
            {
                return operation.Prior;
            }
            if(obj is Parenthessis parenthessis)
            {
                return parenthessis.Prior;
            }
            return 0;
        }

        private List<object> ConvertToList(List<object> operands)
        {
            List<object> result = new List<object>();
            foreach (object operand in operands)
            {
                if (operand is List<object>)
                {
                    List<object> listOBJ = (List<object>)operand;
                    result.AddRange(ConvertToList(listOBJ));
                }
                else
                {
                    result.Add(operand);
                }
            }
            return result;
        }

        private List<object> ExtractOperation(Stack<object> operand, Stack<Operation> operation)
        {
            Operation op = operation.Pop();
            object[] newOP = new object[op.CountParams + 1];
            for (int z = op.CountParams - 1; z >= 0; z--)
            {
                newOP[z] = operand.Pop();
            }
            newOP[op.CountParams] = op;
            return newOP.ToList();
        }
    }
}
