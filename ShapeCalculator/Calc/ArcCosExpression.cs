using System;
namespace Calc
{
    public class ArcCosExpression : TrigonometryExPression
    {
        public ArcCosExpression()
        {
        }

        public ArcCosExpression(Expression value)
        {
            base.value = value;
        }

        public override double calculate()
        {
            return (Math.Acos(value.calculate()) / Math.PI * 180);
        }

        public override Expression clone()
        {
            if (base.value == null)
            {
                return new ArcCosExpression();
            }
            return new ArcCosExpression(base.value.clone());
        }

        public override Expression setExp(Expression p)
        {
            base.value = p.clone();
            return this;
        }

        public override string toString(Notation ntt)
        {
            return ntt.arrange("", base.value.toString(ntt), "arccos");
        }
    }
}
