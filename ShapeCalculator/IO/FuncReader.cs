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

        public List<List<string>> getFunc(MyDatabase database, string type)
        {
            List<List<string>> res = new List<List<string>>();

            Calc.Data data = database.GetItemAsync(type + "Function").Result;

            if (data == null){
                return null;
            }

            string[] lines = data.value.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
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

        public List<string> getFunc(string str)
        {
            List<string> res = new List<string>();
            string[] lines = str.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            int len = lines.Length;
            for (int i = 0; i < len; i += 4)
            {
                res.Add(lines[i + 1]);
            }

            return res;
        }

        public string readFunc(AssetManager assets, string type)
        {
            string str;
            using (StreamReader sr = new StreamReader(assets.Open(type + "Funcs.txt")))
            {
                str = sr.ReadToEnd();
            }

            return str;
        }

        public List<string> getFunctions(AssetManager assets, string type){
            List<List<string>> tmp = this.getFunc(assets, type);
            List<String> res = new List<string>();
            foreach(List<string> i in tmp){
                res.Add(i[1]);
            }
            return res;
        }

        public List<string> getFunctions(MyDatabase database, string type)
        {
            List<List<string>> tmp = this.getFunc(database, type);
            if (tmp == null){
                return null;
            }
            List<String> res = new List<string>();
            foreach (List<string> i in tmp)
            {
                res.Add(i[1]);
            }
            return res;
        }

        public List<List<string>> getFunctions(string str)
        {
            List<List<string>> res = new List<List<string>>();

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
