using System;
using System.Collections.Generic;
using System.IO;
using Android.Content.Res;

namespace IO
{
    public class AboutReader
    {
        private static AboutReader instance = null;

        private AboutReader()
        {
        }

        public static AboutReader getInstance()
        {
            if (instance == null)
            {
                instance = new AboutReader();
            }
            return instance;
        }

        public List<string> getNames(AssetManager assets)
        {
            List<string> res = new List<string>();
            string str;
            using (StreamReader sr = new StreamReader(assets.Open("About.txt")))
            {
                str = sr.ReadToEnd();
            }
            string[] lines = str.Split(new String[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string i in lines)
            {
                res.Add(i);
            }
            return res;
        }
    }
}
