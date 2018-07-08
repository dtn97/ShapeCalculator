using System;
namespace Calc
{
    public class SinExpression : TrigonometryExPression
    {
        public SinExpression()
        {
        }

        public SinExpression(Expression value)
        {
            base.value = value;
        }

        public override double calculate()
        {
            return Math.Sin(value.calculate() * Math.PI / 180);
        }

        public override Expression clone()
        {
            if (base.value == null)
            {
                return new SinExpression();
            }
            return new SinExpression(base.value.clone());
        }

        public override Expression setExp(Expression p)
        {
            base.value = p.clone();
            return this;
        }

        public override string toString(Notation ntt)
        {
            return ntt.arrange("", base.value.toString(ntt), "Sin");
        }
    }
}
