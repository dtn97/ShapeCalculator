using System;
namespace Calc
{
    public abstract class TrigonometryExPression : Expression
    {
        protected Expression value = null;
        public abstract double calculate();
        public abstract Expression clone();
        public abstract string toString(Notation ntt);
        public abstract Expression setExp(Expression p);
    }
}
