using System;
namespace Calc
{
    public class ConstExpression : Expression
    {
        double value;
        
        public ConstExpression()
        {
            this.value = 0;
        }

        public ConstExpression(double v){
            this.value = v;
        }

        public double calculate()
        {
            return value;
        }

        public Expression clone()
        {
            return new ConstExpression(this.value);
        }

        public string toString(Notation ntt)
        {
            return " " + value.ToString() + " ";
        }
    }
}
