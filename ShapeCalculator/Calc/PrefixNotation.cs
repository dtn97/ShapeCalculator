using System;
namespace Calc
{
    public class PrefixNotation : Notation
    {
        static private PrefixNotation instance = null;

        public static Notation getInstance()
        {
            if (instance == null)
            {
                instance = new PrefixNotation();
            }
            return instance;
        }

        private PrefixNotation()
        {
        }

        public override string arrange(string exp1, string exp2, string op)
        {
            return op + " " + exp1 + " " + exp2;
        }
    }
}
