using System;
namespace Calc
{
    public class VarNode : Node
    {
        private string name;
        private double value;
        
        public VarNode()
        {
        }

        public VarNode(string name, double value, bool activate)
        {
            this.name = name;
            this.value = value;
            base.activate = activate;
        }

        public override Node clone()
        {
            return new VarNode(this.name, this.value, base.activate);
        }


    }
}
