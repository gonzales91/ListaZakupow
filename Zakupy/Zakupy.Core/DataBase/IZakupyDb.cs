using System.Collections.Generic;

namespace Zakupy.Core.DataBase
{
    public interface IZakupyDb
    {
        void CreateTables();        
        void DodajElement(string nazwaProduktu, double? cena);
        void UsunElement(int id);

        List<ZakupyModel> PobierzZakupy();
    }
}
