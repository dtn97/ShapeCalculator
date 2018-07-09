using System;
using System.Collections.Generic;
using System.IO;
using Android.Content.Res;

namespace IO
{
    public class ShapeReader
    {
        private static ShapeReader instance = null;

        private ShapeReader()
        {
        }

        public static ShapeReader getInstance(){
            if (instance == null){
                instance = new ShapeReader();
            }
            return instance;
        }

        public List<string> getShapes(AssetManager assets, string fileName){
            List<string> res = new List<string>();
            string str;
            using (StreamReader sr = new StreamReader(assets.Open(fileName)))
            {
                str = sr.ReadToEnd();
            }
            string[] lines = str.Split(new String[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string i in lines){
                res.Add(i);
            }
            return res;
        }

        public List<string> getShapes(IO.MyDatabase database)
        {
            Calc.Data data = database.GetItemAsync("Shape").Result;
            List<string> res = new List<string>();
            string[] lines = data.value.Split(new String[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string i in lines)
            {
                res.Add(i);
            }
            return res;
        }

        public string readShape(AssetManager assets, string fileName)
        {
            string str;
            using (StreamReader sr = new StreamReader(assets.Open(fileName)))
            {
                str = sr.ReadToEnd();
            }
            return str;
        }
    }
}
