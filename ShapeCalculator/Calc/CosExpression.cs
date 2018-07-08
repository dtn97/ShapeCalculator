using System;
namespace Calc
{
    public class CosExpression : TrigonometryExPression
    {
        public CosExpression()
        {
        }

        public CosExpression(Expression value)
        {
            base.value = value;
        }

        public override double calculate()
        {
            return Math.Cos(value.calculate() * Math.PI / 180);
        }

        public override Expression clone()
        {
            if (base.value == null)
            {
                return new CosExpression();
            }
            return new CosExpression(base.value.clone());
        }

        public override Expression setExp(Expression p)
        {
            base.value = p.clone();
            return this;
        }

        public override string toString(Notation ntt)
        {
            return ntt.arrange("", base.value.toString(ntt), "Cos");
        }
    }
}
