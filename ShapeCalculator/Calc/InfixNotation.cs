using System;
namespace Calc
{
    public class InfixNotation : Notation
    {
        static private InfixNotation instance = null;

        public static Notation getInstance()
        {
            if (instance == null)
            {
                instance = new InfixNotation();
            }
            return instance;
        }

        private InfixNotation()
        {
            
        }

        public override string arrange(string exp1, string exp2, string op)
        {
            return "(" + exp1 + " " + op + " " + exp2 + ")";
        }
    }
}
