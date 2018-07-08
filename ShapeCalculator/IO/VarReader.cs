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
    }
}
