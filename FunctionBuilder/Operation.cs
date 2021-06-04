using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionBuilder
{
    public class Parenthessis
    {
        public bool IsOpening { get; }

        public Parenthessis(char parenthesis)
        {
            IsOpening = parenthesis == '(';
        }

        public override string ToString()
        {
            return IsOpening ? "(" : ")";
        }
        public int Prior = 0;
    }
    public abstract class Operation
    {
        public abstract string Name { get; }
        public abstract int CountParams { get; }
        public abstract double Calculate(double[] @params);
        public abstract int Prior { get; }
        public abstract bool IsPrefix { get; }
        public abstract bool IsBinaryOperation { get; }
        public override string ToString()
        {
            return Name;
        }

    }
    public class Plus : Operation
    {
        public override string Name => "+";
        public override int CountParams => 2;
        public override double Calculate(double[] @params) { return @params[1] + @params[0]; }

        public override int Prior => 1;

        public override bool IsPrefix => false;
        public override bool IsBinaryOperation => true;
    }
    public class Minus : Operation
    {
        public override string Name => "-";
        public override int CountParams => 2;
        public override double Calculate(double[] @params) { return @params[1] - @params[0]; }
        public override int Prior => 1;
        public override bool IsPrefix => false;
        public override bool IsBinaryOperation => true;
    }
    class Mult : Operation
    {
        public override string Name => "*";
        public override int CountParams => 2;
        public override double Calculate(double[] @params) { return @params[1] * @params[0]; }
        public override int Prior => 2;
        public override bool IsPrefix => false;
        public override bool IsBinaryOperation => true;
    }
    class Div : Operation
    {
        public override string Name => "/";
        public override int CountParams => 2;
        public override double Calculate(double[] @params) { return @params[1] / @params[0]; }
        public override int Prior => 2;
        public override bool IsPrefix => false;
        public override bool IsBinaryOperation => true;
    }
    class Log : Operation
    {
        public override string Name => "log";
        public override int CountParams => 2;
        public override double Calculate(double[] @params) { return Math.Log(@params[0], @params[1]); }                    //params 1 = основание
        public override int Prior => 3;
        public override bool IsPrefix => true;
        public override bool IsBinaryOperation => true;
    }

    class Ln : Operation
    {
        public override string Name => "ln";

        public override int CountParams => 1;

        public override int Prior => 3;

        public override double Calculate(double[] @params)
        {
            return Math.Log(@params[0]);
        }
        public override bool IsPrefix => true;
        public override bool IsBinaryOperation => false;
    }

    class Sin : Operation
    {
        public override string Name => "sin";
        public override int CountParams => 1;
        public override double Calculate(double[] @params) { return Math.Sin(@params[0]); }
        public override int Prior => 3;
        public override bool IsPrefix => true;
        public override bool IsBinaryOperation => false;
    }
    class Cos : Operation
    {
        public override string Name => "cos";
        public override int CountParams => 1;
        public override double Calculate(double[] @params) { return Math.Cos(@params[0]); }
        public override int Prior => 3;
        public override bool IsPrefix => true;
        public override bool IsBinaryOperation => false;
    }
    class Tan : Operation
    {
        public override string Name => "tg";
        public override int CountParams => 1;
        public override double Calculate(double[] @params) { return Math.Tan(@params[0]); }
        public override int Prior => 3;
        public override bool IsPrefix => true;
        public override bool IsBinaryOperation => false;
    }

    class CoTan : Operation
    {
        public override string Name => "ctg";

        public override int CountParams => 1;

        public override int Prior => 3;

        public override double Calculate(double[] @params)
        {
            return 1.0 / Math.Tan(@params[0]);
        }
        public override bool IsPrefix => true;
        public override bool IsBinaryOperation => false;
    }
    class ArcSin : Operation
    {
        public override string Name => "arcsin";
        public override int CountParams => 1;
        public override double Calculate(double[] @params) { return Math.Asin(@params[0]); }
        public override int Prior => 3;
        public override bool IsPrefix => true;
        public override bool IsBinaryOperation => false;
    }

    class ArcCos : Operation
    {
        public override string Name => "arccos";
        public override int CountParams => 1;
        public override double Calculate(double[] @params) { return Math.Acos(@params[0]); }
        public override int Prior => 3;
        public override bool IsPrefix => true;
        public override bool IsBinaryOperation => false;
    }

    class ArcTan : Operation
    {
        public override string Name => "arctg";
        public override int CountParams => 1;
        public override double Calculate(double[] @params) { return Math.Atan(@params[0]); }
        public override int Prior => 3;
        public override bool IsPrefix => true;
        public override bool IsBinaryOperation => false;
    }

    class ArcCoTan : Operation
    {
        public override string Name => "arcctg";
        public override int CountParams => 1;
        public override double Calculate(double[] @params) { return Math.PI / 2 - Math.Atan(@params[0]); }
        public override int Prior => 3;
        public override bool IsPrefix => true;
        public override bool IsBinaryOperation => false;
    }

    class Degree : Operation
    {
        public override string Name => "^";
        public override int CountParams => 2;
        public override double Calculate(double[] @params) { return Math.Pow(@params[1], @params[0]); }
        public override int Prior => 3;
        public override bool IsPrefix => false;
        public override bool IsBinaryOperation => false;
    }
    class Sqrt : Operation
    {
        public override string Name => "sqrt";
        public override int CountParams => 1;
        public override double Calculate(double[] @params) { return Math.Sqrt(@params[0]); }
        public override int Prior => 3;
        public override bool IsPrefix => true;
        public override bool IsBinaryOperation => false;
    }
    class Factorial : Operation
    {
        public override string Name => "!";

        public override int CountParams => 1;

        public override int Prior => 3;

        public override bool IsPrefix => false;

        public override double Calculate(double[] @params)
        {
            return CalculationFactorial(@params[0]);
        }

        private double CalculationFactorial(double param)
        {
            if (param == 1)
            {
                return 1;
            }
            return param * CalculationFactorial(param - 1);
        }
        public override bool IsBinaryOperation => false;
    }

    public class PI : Operation
    {
        public override string Name => "pi";

        public override int CountParams => 0;

        public override int Prior => 3;

        public override double Calculate(double[] @params)
        {
            return Math.PI;
        }
        public override bool IsPrefix => false;
        public override bool IsBinaryOperation => false;
    }

    public class Exponent : Operation
    {
        public override string Name => "e";

        public override int CountParams => 0;

        public override int Prior => 3;

        public override double Calculate(double[] @params)
        {
            return Math.E;
        }
        public override bool IsPrefix => false;
        public override bool IsBinaryOperation => false;
    }

    public class Exception : Operation
    {
        public override string Name => "Exception";

        public override int CountParams => 0;

        public override int Prior => int.MaxValue;

        public override double Calculate(double[] @params)
        {
            throw new NotImplementedException();
        }
        public override bool IsPrefix => false;
        public override bool IsBinaryOperation => false;
    }
}
