using System;
namespace Calc
{
    public abstract class BinaryExpression : Expression
    {

        protected Expression exp1 = null;
        protected Expression exp2 = null;

        abstract public double calculate();
        public abstract string toString(Notation ntt);
        public abstract Expression setExp(Expression p1, Expression p2);
        public abstract Expression clone();
    }
}
