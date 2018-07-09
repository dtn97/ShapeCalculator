using System;
using System.Collections.Generic;
using Android.Content.Res;

namespace IO
{
    public class FuncWriter
    {
        private static FuncWriter instance = null;
        private FuncWriter()
        {
            
        }

        public static FuncWriter getInstance(){
            if (instance == null){
                instance = new FuncWriter();
            }
            return instance;
        }

        public void writeFuncs(AssetManager assets, string type, Dictionary<string, List<string>> funcs){
            
        }
    }
}
