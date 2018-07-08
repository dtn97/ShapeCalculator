using System;
using System.Collections.Generic;
namespace Calc
{
    public class FormulaBuilder
    {
        private FormulaNode instance = new FormulaNode();

        public FormulaBuilder()
        {
            
        }

        public Node build()
        {
            return instance.clone();
        }

        public FormulaBuilder setName(string name)
        {
            instance.setName(name);
            return this;
        }

        public FormulaBuilder setTarget(string target)
        {
            instance.setTarget(target);
            return this;
        }

        public FormulaBuilder setVariable(string variable)
        {
            instance.setVariable(variable);
            return this;
        }

        public FormulaBuilder setVariable(HashSet<string> variable)
        {
            instance.setVariable(variable);
            return this;
        }

        public FormulaBuilder setExp(string exp)
        {
            instance.setExp(exp);
            return this;
        }

        public FormulaBuilder setActivate(bool activate)
        {
            instance.setActivate(activate);
            return this;
        }
    }
}
