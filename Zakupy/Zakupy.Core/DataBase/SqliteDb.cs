using MvvmCross.Platform;
using MvvmCross.Plugins.Sqlite;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zakupy.Core.DataBase
{
    public class SqliteDb : IZakupyDb
    {
        private readonly SQLiteConnection _connection;
        private readonly List<Type> _tables = new List<Type>
        {
            typeof(ZakupyDb)
        };

        public SqliteDb(IMvxSqliteConnectionFactory factory)
        {
            _connection = factory.GetConnection("zakupy.sqlite");
            CreateTables();
        }

        public void CreateTables()
        {
            foreach (Type table in _tables)
                _connection.CreateTable(table);
        }

        public List<ZakupyModel> PobierzZakupy()
        {
            List<ZakupyDb> dbItem = _connection
                .Table<ZakupyDb>()
                .ToList();

            List<ZakupyModel> result = dbItem
                .Select(i => new ZakupyModel { Id = i.Id, NazwaProduktu = i.NazwaProduktu, Cena = i.Cena })
                .ToList();

            return result;
        }

        public void DodajElement(string nazwaProduktu, double? cena)
        {
            var zakupyDb = new ZakupyDb();
            zakupyDb.NazwaProduktu = nazwaProduktu;
            zakupyDb.Cena = cena;

            _connection.Insert(zakupyDb);
        }

        public void UsunElement(int id)
        {
            _connection.Delete<ZakupyDb>(id);
        }
    }
}
