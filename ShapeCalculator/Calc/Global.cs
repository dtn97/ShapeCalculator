using System;
using System.Collections.Generic;
namespace Calc
{
    public class Global
    {
        private Dictionary<string, double> varibles;

        public Global()
        {
            varibles = new Dictionary<string, double>();
        }

        public Global(Dictionary<string, double> varibles)
        {
            this.varibles = varibles;
        }

        public double getValue(string name)
        {
            if (varibles.ContainsKey(name))
                return varibles[name];
            return -1;
        }

        public void setValue(string name, double value)
        {
            varibles[name] = value;
        }

        public bool haveValue(string name)
        {
            if (varibles.ContainsKey(name))
                return varibles[name] > 0;
            return false;
        }

        public Global clone()
        {
            return new Global(this.varibles);
        }

        public Dictionary<string, double> getVariables()
        {
            return varibles;
        }

        public void refesh()
        {
            List<string> tmp = new List<string>();
            foreach(KeyValuePair<string, double> i in varibles)
            {
                tmp.Add(i.Key);
            }
            foreach(string i in tmp)
            {
                varibles[i] = -1;
            }
        }
    }
}
