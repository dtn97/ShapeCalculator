using System;
using System.Collections.Generic;
using System.IO;
using Android.Content.Res;

namespace IO
{
    public class FuncReader
    {
        private static FuncReader instance = null;
        private FuncReader()
        {
        }

        public static FuncReader getInstance()
        {
            if (instance == null)
            {
                instance = new FuncReader();
            }
            return instance;
        }

        public List<List<string>> getFunc(AssetManager assets, string type)
        {
            List<List<string>> res = new List<List<string>>();
            string str;
            using (StreamReader sr = new StreamReader(assets.Open(type + "Funcs.txt")))
            {
                str = sr.ReadToEnd();
            }

            string[] lines = str.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            int len = lines.Length;
            for (int i = 0; i < len; i += 4)
            {
                List<string> tmp = new List<string>();
                for (int j = 0; j < 4; ++j)
                {
                    tmp.Add(lines[i + j]);
                }
                res.Add(tmp);
            }

            return res;
        }
    }
}
