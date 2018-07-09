using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Android.Content.Res;
using Android.Util;
using SQLite;

namespace IO
{
    public class MyDatabase
    {
        private static string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        private SQLiteAsyncConnection database = null;
        public MyDatabase(AssetManager assets)
        {
            database = new SQLiteAsyncConnection(System.IO.Path.Combine(folder, "Data.db"));

            try
            {
                database.CreateTableAsync<Calc.Data>().Wait();
                database.InsertAsync(this.getShapes(assets));
                List<Calc.Data> tmp = this.getInfo(assets);
                foreach(Calc.Data i in tmp){
                    database.InsertAsync(i);
                }

            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
            }
        }

        public Task<List<Calc.Data>> GetItemsAsync()
        {
            return database.Table<Calc.Data>().ToListAsync();
        }

        public Task<Calc.Data> GetItemAsync(string name)
        {
            return database.Table<Calc.Data>().Where(i => i.name.Equals(name)).FirstOrDefaultAsync();
        }

        public Task<Calc.Data> GetItemAsync(int id)
        {
            return database.Table<Calc.Data>().Where(i => i.id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Calc.Data item)
        {
            if (item.id > 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        internal void reset(AssetManager assets)
        {
            database.DropTableAsync<Calc.Data>().Wait();
            database.CreateTableAsync<Calc.Data>().Wait();
            database.InsertAsync(this.getShapes(assets));
            List<Calc.Data> tmp = this.getInfo(assets);
            foreach (Calc.Data i in tmp)
            {
                database.InsertAsync(i);
            }
        }

        public Task<int> DeleteItemAsync(Calc.Data item)
        {
            return database.DeleteAsync(item);
        }

        private Calc.Data getShapes(AssetManager assets){
            Calc.Data res = new Calc.Data();
            res.name = "Shape";
            res.value = ShapeReader.getInstance().readShape(assets, "Shape.txt");
            return res;
        }

        private List<Calc.Data> getInfo(AssetManager assets){
            List<Calc.Data> res = new List<Calc.Data>();

            List<string> tmp = ShapeReader.getInstance().getShapes(assets, "Shape.txt");

            foreach(string i in tmp){
                Calc.Data item = new Calc.Data();
                item.name = i + "Variable";
                item.value = VarReader.getInstance().readVariables(assets, i);
                res.Add(item);
            }

            foreach(string i in tmp){
                Calc.Data item = new Calc.Data();
                item.name = i + "Function";
                item.value = FuncReader.getInstance().readFunc(assets, i);
                res.Add(item);
            }

            return res;
        }
    }
}
