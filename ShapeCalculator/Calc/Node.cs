using System;
namespace Calc
{
    public abstract class Node
    {
        protected bool activate = false;
        public bool isActivate()
        {
            return activate;
        }
        public abstract Node clone();
    }
}
