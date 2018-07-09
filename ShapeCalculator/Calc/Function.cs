using System;
using System.Collections.Generic;

namespace Calc
{
    public class Function
    {
        private List<FormulaNode> formulaNodes;

        public Function(List<List<string>> value)
        {
            formulaNodes = new List<FormulaNode>();
            foreach(List<string> i in value){
                FormulaNode tmp = new FormulaNode();
                tmp.setName(i[1]);
                tmp.setTarget(i[0]);
                tmp.setExp(i[2]);
                tmp.setVariable(i[3]);
                formulaNodes.Add(tmp);
            }
        }

        public void remove(string var){
            for (int i = 0; i < formulaNodes.Count; ++i){
                if (formulaNodes[i].containsVar(var)){
                    formulaNodes.RemoveAt(i);
                    --i;
                }
            }
        }

        public string toString(){
            if (formulaNodes.Count == 0){
                return "";
            }

            string res = "";

            foreach (FormulaNode i in formulaNodes){
                res += i.toStringFunc();
            }

            if (res[res.Length - 1] == '\n')
                res.Remove(res.Length - 1);

            return res;
        }
    }
}
