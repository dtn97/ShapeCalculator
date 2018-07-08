using System;
using System.Collections.Generic;
namespace Calc
{
    public class ProcessBuilder
    {
        private Process instance = new Process();
        public ProcessBuilder()
        {
        }

        public ProcessBuilder setVariables(Global var)
        {
            instance.setVariables(var);
            return this;
        }

        public ProcessBuilder setFunctions(List<Node> func)
        {
            instance.setFunctions(func);
            return this;
        }

        public ProcessBuilder setFunctions(List<List<string>> func)
        {
            instance.setFunctions(func);
            return this;
        }

        public Process build()
        {
            return instance.clone();
        }
    }
}
