using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scientific_Equipment.Tools
{
    class ColumnAttribute : Attribute
    /*генерация запросов */
    {
        public string Column { get; }
        public ColumnAttribute(string column)
        {
            Column = column;
        }
    }
}