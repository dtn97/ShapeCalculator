using System;
using System.Collections.Generic;
namespace Calc
{
    public class ExpressionFactory
    {
        private Dictionary<string, Expression> exp = new Dictionary<string, Expression>();
        
        public ExpressionFactory()
        {
            exp["+"] = new AddExpression();
            exp["-"] = new SubExpression();
            exp["*"] = new MulExpression();
            exp["/"] = new DivExpression();
            exp["^"] = new PowExpression();
            exp["sin"] = new SinExpression();
            exp["cos"] = new CosExpression();
            exp["arcsin"] = new ArcSinExpression();
            exp["arccos"] = new ArcCosExpression();
        }

        public Expression GetExpression(string s)
        {
            if (exp.ContainsKey(s))
                return exp[s].clone();
            return null;
        }
    }
}
