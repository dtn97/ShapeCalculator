using System;
using SQLite;

namespace Calc
{
    public class Data
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string name { get; set; }
        public string value { get; set; }
        public Data()
        {
            this.name = "";
            this.value = "";
        }
    }
}
