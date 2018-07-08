using System;
namespace Calc
{
    public class AddExpression : BinaryExpression
    {
        public AddExpression()
        {
        }

        public AddExpression(Expression p1, Expression p2)
        {
            base.exp1 = p1;
            base.exp2 = p2;
        }

        public override double calculate()
        {
            return exp1.calculate() + exp2.calculate();
        }

        public override Expression clone()
        {
            if (exp1 == null && exp2 == null)
            {
                return new AddExpression();
            }
            return new AddExpression(exp1.clone(), exp2.clone());
        }

        public override Expression setExp(Expression p1, Expression p2)
        {
            base.exp1 = p1;
            base.exp2 = p2;
            return this;
        }

        public override string toString(Notation ntt)
        {
            return ntt.arrange(exp1.toString(ntt), exp2.toString(ntt), "+");
        }
    }
}
