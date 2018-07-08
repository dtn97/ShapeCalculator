using System;
using System.Collections.Generic;
namespace Calc
{
    public class PriorityExpression
    {
        private static PriorityExpression instance = null;
        private Dictionary<string, int> priority = new Dictionary<string, int>();

        private PriorityExpression()
        {
            priority["("] = -1;
            priority["+"] = 1;
            priority["-"] = 1;
            priority["*"] = 2;
            priority["/"] = 2;
            priority["^"] = 3;
            priority["sin"] = 4;
            priority["cos"] = 4;
            priority["arcsin"] = 4;
            priority["arccos"] = 4;
            priority[")"] = 5;
        }

        public int getPriority(string temp)
        {
            if (priority.ContainsKey(temp))
            {
                return priority[temp];
            }
            return 0;
        }

        public static PriorityExpression getInstance()
        {
            if (instance == null)
            {
                instance = new PriorityExpression();
            }
            return instance;
        }
    }
}
