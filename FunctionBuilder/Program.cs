using System;
using System.Collections.Generic;

namespace FunctionBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            ReversPolishNotation RPN = new ReversPolishNotation(Console.ReadLine());
            List<object> rpn = RPN.resultToRPN;
            Console.WriteLine("__________________________________________________________");
            for(int i = 0; i < rpn.Count; i++)
            {
                Console.Write(" {0} ",rpn[i]);
            }
        }
    }
}
/*
                if (operators.Contains(c))
                {
                    if (stack.Count > 0 && !c.Equals("("))
                    {
                        if (c.Equals(")"))
                        {
                            string s = stack.Pop();
                            while (s != "(")
                            {
                                outputSeparated.Add(s);
                                s = stack.Pop();
                            }
                        }
                        else if (GetPriority(c) > GetPriority(stack.Peek()))
                            stack.Push(c);
                        else
                        {
                            while (stack.Count > 0 && GetPriority(c) <= GetPriority(stack.Peek()))
                                outputSeparated.Add(stack.Pop());
                            stack.Push(c);
                        }
                    }
                    else
                        stack.Push(c);
                }
                else
                    outputSeparated.Add(c);
 */