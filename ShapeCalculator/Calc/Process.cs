using System;
using System.Collections.Generic;
namespace Calc
{
    public class Process
    {
        private Global variables;
        private List<Node> functions;

        public Process()
        {
            this.variables = new Global();
            this.functions = new List<Node>();
        }

        public Process(Dictionary<string, double> vars, List<List<string>> func)
        {
            this.setVariables(vars);
            this.setFunctions(func);
        }

        public Process(Global variable, List<Node> func)
        {
            this.variables = variable;
            this.functions = func;
        }

        private List<FormulaNode> searchFunc(string target)
        {
            List<FormulaNode> res = new List<FormulaNode>();
            foreach(FormulaNode i in this.functions)
            {
                if (i.getTarget().Equals(target))
                {
                    res.Add(i);
                }
            }
            return res;
        }

        private void setNodeActivate(string target, bool activate)
        {
            List<FormulaNode> tmp = this.searchFunc(target);
            foreach(FormulaNode i in tmp)
            {
                i.setActivate(activate);
            }
        }

        public Process setVariables(Dictionary<string, double> vars)
        {
            this.variables = new Global(vars);
            foreach(KeyValuePair<string, double> i in vars)
            {
                if (i.Value > 0)
                {
                    this.setNodeActivate(i.Key, true);
                }
            }
            return this;
        }

        public Process setVariables(Global variable)
        {
            this.variables = variable.clone();
            return this;
        }

        public Process setFunctions(List<Node> func)
        {
            this.functions = new List<Node>();
            foreach(Node i in func)
            {
                this.functions.Add(((FormulaNode)i).clone());
            }
            return this;
        }

        public Process setFunctions(List<List<string>> function)
        {
            this.functions = new List<Node>();
            foreach(List<string> i in function)
            {
                string[] tmp = i.ToArray();
                this.functions.Add(new FormulaNode().setName(tmp[1]).setTarget(tmp[0]).setExp(tmp[2]).setVariable(tmp[3]).setActivate(false));
            }
            return this;
        }

        public Process clone()
        {
            List<Node> func = new List<Node>();
            foreach(Node i in func)
            {
                func.Add(((FormulaNode)i).clone());
            }

            return new Process().setFunctions(func).setVariables(variables.clone());
        }

        public List<string> run()
        {
            List<string> res = new List<string>();
            bool canStop = true;

            while (true)
            {
                canStop = true;
                foreach (FormulaNode i in functions)
                {
                    if (i.isActivate() == false)
                    {
                        if (i.canActivate(variables))
                        {
                            //res.Add(i.toString());
                            res.Add(i.active(ref variables));
                            //res.AddRange(i.active(ref variables));
                            this.setNodeActivate(i.getTarget(), true);
                            canStop = false;
                        }
                    }
                }
                if (canStop)
                {
                    return res;
                }
            }
        }

        public Dictionary<string, double> getVariables()
        {
            return variables.getVariables();
        }

        public Process updateVar(string varName, double value)
        {
            this.variables.setValue(varName, value);
            return this;
        }

        public List<double> getEdge(string vars)
        {
            string[] tmp = vars.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            List<double> res = new List<double>();
            foreach(string i in tmp)
            {
                res.Add(variables.getValue(i));
            }
            return res;
        }

        public void refesh()
        {
            this.variables.refesh();
            foreach(FormulaNode i in this.functions)
            {
                i.refesh();
            }
        }
    }
}
