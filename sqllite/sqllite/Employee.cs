using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqllite
{
    class Employee
    {
        [PrimaryKey, AutoIncrement]

        public int id { get; set; }
        [MaxLength(20)]
        public string name { get; set; }
        public string age { get; set; }
    }
}
