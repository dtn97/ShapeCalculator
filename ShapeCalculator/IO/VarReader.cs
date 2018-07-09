using System;
using System.Collections.Generic;
using System.IO;
using Android.Content.Res;

namespace IO
{
    public class VarReader
    {
        private static VarReader instance = null;
        private VarReader()
        {
        }

        public static VarReader getInstance(){
            if (instance == null){
                instance = new VarReader();
            }
            return instance;
        }

        public Dictionary<string, double> getVars(AssetManager assets, string type){
            Dictionary<string, double> res = new Dictionary<string, double>();
            string str;
            using (StreamReader sr = new StreamReader(assets.Open(type + "Vars.txt")))
            {
                str = sr.ReadToEnd();
            }
            string[] lines = str.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach(string i in lines){
                res.Add(i, -1);
            }
            return res;
        }

        public List<string> getVars(string str)
        {
            List<string> res = new List<string>();
            string[] lines = str.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string i in lines)
            {
                res.Add(i);
            }
            return res;
        }

        public List<string> getVars(MyDatabase database, string type){
            Calc.Data data = database.GetItemAsync(type + "Variable").Result;
            if (data == null){
                return null;
            }
            return this.getVars(data.value);
        }

        public List<string> getVariables(AssetManager assets, string type)
        {
            string str;
            using (StreamReader sr = new StreamReader(assets.Open(type + "Vars.txt")))
            {
                str = sr.ReadToEnd();
            }
            string[] lines = str.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            return new List<string>(lines);
        }

        public string readVariables(AssetManager assets, string type)
        {
            string str;
            using (StreamReader sr = new StreamReader(assets.Open(type + "Vars.txt")))
            {
                str = sr.ReadToEnd();
            }
            return str;
        }
    }
}
