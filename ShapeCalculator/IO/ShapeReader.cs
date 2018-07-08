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
    }
}
