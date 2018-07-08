using System;
namespace Calc
{
    public class VarExpression : Expression
    {
        private string name;
        
        public VarExpression()
        {
        }

        public VarExpression(string name)
        {
            this.name = name;
        }

        public double calculate()
        {
            return 0;
        }

        public Expression clone()
        {
            return new VarExpression(name);
        }

        public string toString(Notation ntt)
        {
            return this.name;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public string getName()
        {
            return this.name;
        }
    }
}
