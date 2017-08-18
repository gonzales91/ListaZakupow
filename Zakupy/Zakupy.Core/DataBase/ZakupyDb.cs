using SQLite;

namespace Zakupy.Core.DataBase
{
    [Table("Zakupy")]
    internal class ZakupyDb
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }

        [MaxLength(20), Column("NazwaProduktu")]
        public string NazwaProduktu { get; set; }

        [Indexed]
        public double? Cena { get; set; }
    }
}
