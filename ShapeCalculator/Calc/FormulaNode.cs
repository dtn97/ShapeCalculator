using System;
using System.Collections.Generic;
namespace Calc
{
    public class FormulaNode : Node
    {
        private string name;
        private string target;
        private HashSet<string> variable;
        private string exp;

        public FormulaNode setName(string name)
        {
            this.name = name;
            return this;
        }

        public FormulaNode setTarget(string target)
        {
            this.target = target;
            return this;
        }

        public string getTarget()
        {
            return this.target;
        }

        public FormulaNode setVariable(HashSet<string> variable)
        {
            this.variable = variable;
            return this;
        }

        public FormulaNode setVariable(string var)
        {
            string[] tmp = var.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            variable = new HashSet<string>();
            foreach(string i in tmp)
            {
                variable.Add(i);
            }
            return this;
        }

        public FormulaNode setExp(string exp)
        {
            this.exp = exp;
            return this;
        }

        public FormulaNode setActivate(bool activate)
        {
            this.activate = activate;
            return this;
        }

        public FormulaNode()
        {
        }

        public FormulaNode(string name, string target, string exp, string varible, bool activate)
        {
            base.activate = activate;
            this.name = name;
            this.target = target;
            this.exp = exp;
            string[] tmp = varible.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            this.variable = new HashSet<string>();
            foreach (string i in tmp)
            {
                this.variable.Add(i);
            }
        }

        public FormulaNode(string name, string target, string exp, HashSet<string> varible, bool activate)
        {
            base.activate = activate;
            this.name = name;
            this.target = target;
            this.exp = exp;
            this.variable = varible;
        }

        public override Node clone()
        {
            return new FormulaNode(name, target, exp, variable, base.activate);
        }

        public bool canActivate(Global global)
        {
            int cnt = 0;
            foreach(string i in variable)
            {
                if (global.haveValue(i))
                {
                    ++cnt;
                }
            }
            return cnt == variable.Count;
        }

        public List<string> active(ref Global global)
        {
            string[] tmp = exp.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            string str = " ";
            for (int i = 0; i < tmp.Length; ++i)
            {
                if (variable.Contains(tmp[i]))
                {
                    tmp[i] = global.getValue(tmp[i]).ToString();
                }
                str += tmp[i] + " ";
            }

            Expression expression = ExpressionConvert.stringToExpression(str);
            global.setValue(target, expression.calculate());

            base.activate = true;

            //return (target +" =  " + global.getValue(target).ToString());

            List<string> res = new List<string>();
            res.Add(this.name);
            res.Add(target + " = " + expression.toString(InfixNotation.getInstance()) + " =  " + global.getValue(target).ToString());
            return res;
        }

        public string toString()
        {
            return this.name;
        }

        public void refesh()
        {
            base.activate = false;
        }
    }
}
