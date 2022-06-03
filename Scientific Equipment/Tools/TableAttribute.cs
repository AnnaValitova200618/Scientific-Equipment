using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scientific_Equipment.Tools
{
    class TableAttribute : Attribute
    /*генерация запросов */
    {
        public string Table { get; }
        public TableAttribute(string table)
        {
            Table = table;
        }
    }
}
