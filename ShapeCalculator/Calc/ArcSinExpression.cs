using System;
namespace Calc
{
    public class ArcSinExpression : TrigonometryExPression

    {
        public ArcSinExpression()
        {
        }

        public ArcSinExpression(Expression value)
        {
            base.value = value;
        }

        public override double calculate()
        {
            return (Math.Asin(value.calculate()) / Math.PI * 180);
        }

        public override Expression clone()
        {
            if (base.value == null)
            {
                return new ArcSinExpression();
            }
            return new ArcSinExpression(base.value.clone());
        }

        public override Expression setExp(Expression p)
        {
            base.value = p.clone();
            return this;
        }

        public override string toString(Notation ntt)
        {
            return ntt.arrange("", base.value.toString(ntt), "arcsin");
        }
    }
}
