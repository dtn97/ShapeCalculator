using System;
namespace Calc
{
    public class PostfixNotation : Notation
    {
        static private PostfixNotation instance = null;

        public static Notation getInstance()
        {
            if (instance == null)
            {
                instance = new PostfixNotation();
            }
            return instance;
        }

        private PostfixNotation()
        {
        }

        public override string arrange(string exp1, string exp2, string op)
        {
            return exp1 + " " + exp2 + " " + op;
        }
    }
}
