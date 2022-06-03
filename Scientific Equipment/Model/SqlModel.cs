using MySql.Data.MySqlClient;
using Scientific_Equipment.DTO;
using Scientific_Equipment.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scientific_Equipment.Model
{
    class SqlModel
    {
        private SqlModel() { }
        static SqlModel sqlModel;
        public static SqlModel GetInstance()
        {
            if (sqlModel == null)
                sqlModel = new SqlModel();
            return sqlModel;
        }




        //INSERT INTO `group` set title='1125', year = 2018;
        // возвращает ID добавленной записи
        public int Insert<T>(T value)
        {
            string table;
            List<(string, object)> values;
            GetMetaData(value, out table, out values);
            var query = CreateInsertQuery(table, values);
            var db = MySqlDB.GetDB();
            // лучше эти 2 запроса объединить в один с помощью транзакции
            int id = db.GetNextID(table);
            db.ExecuteNonQuery(query.Item1, query.Item2);
            return id;
        }
        // обновляет объект в бд по его id
        public void Update<T>(T value) where T : BaseDTO
        {
            string table;
            List<(string, object)> values;
            GetMetaData(value, out table, out values);
            var query = CreateUpdateQuery(table, values, value.ID);
            var db = MySqlDB.GetDB();
            db.ExecuteNonQuery(query.Item1, query.Item2);
        }

        public void Delete<T>(T value) where T : BaseDTO
        {
            var type = value.GetType();
            string table = GetTableName(type);
            var db = MySqlDB.GetDB();
            string query = $"delete from `{table}` where id = {value.ID}";
            db.ExecuteNonQuery(query);
        }

        public int GetNumRows(Type type)
        {
            string table = GetTableName(type);
            return MySqlDB.GetDB().GetRowsCount(table);
        }

        private static string GetTableName(Type type)
        {
            var tableAtrributes = type.GetCustomAttributes(typeof(TableAttribute), false);
            return ((TableAttribute)tableAtrributes.First()).Table;
        }


        public List<Scientists> ListScientists()
        {
            var scientists = new List<Scientists>();
            var mySqlDB = MySqlDB.GetDB();
            string query = "select * from scientists";
            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        scientists.Add(new Scientists
                        {
                            ID = dr.GetInt32("ID"),
                            firstname = dr.GetString("Firstname"),
                            patronymic = dr.GetString("Patronymic"),
                            lastname = dr.GetString("Lastname"),
                            login = dr.GetString("Login"),
                            password = dr.GetString("Password"),
                            position = dr.GetString("Position")
                        });
                    }
                }
                mySqlDB.CloseConnection();
            }
            return scientists;
        }


        public List<Equipment> ListEquipments()
        {
            var equipments = new List<Equipment>();
            var mySqlDB = MySqlDB.GetDB();
            string query = "select * from equipment, scientists where ID_responsible = scientists.ID";
            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        equipments.Add(new Equipment
                        {
                            ID = dr.GetInt32("ID"),
                            name = dr.GetString("Name"),
                            dimensions = dr.GetString("Dimensions"),
                            weight = dr.GetDecimal("Weight"),
                            id_responsible = dr.GetInt32("ID_responsible"),

                            Scientists = new Scientists { ID = dr.GetInt32("ID_responsible"), lastname = dr.GetString("Lastname") }
                        });
                    }
                }
                mySqlDB.CloseConnection();
            }
            return equipments;
        }

        private static (string, MySqlParameter[]) CreateInsertQuery(string table, List<(string, object)> values)//Создание записи в таблице
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"INSERT INTO `{table}` set ");
            List<MySqlParameter> parameters = InitParameters(values, stringBuilder);
            return (stringBuilder.ToString(), parameters.ToArray());
        }

        private static (string, MySqlParameter[]) CreateUpdateQuery(string table, List<(string, object)> values, int id)//Обновление записи в таблице
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"UPDATE `{table}` set ");
            List<MySqlParameter> parameters = InitParameters(values, stringBuilder);
            stringBuilder.Append($" WHERE id = {id}");
            return (stringBuilder.ToString(), parameters.ToArray());
        }

        private static List<MySqlParameter> InitParameters(List<(string, object)> values, StringBuilder stringBuilder)//создает набор параметров к запросу. в каждом параметре значение для определенного столбца
        {
            var parameters = new List<MySqlParameter>();
            int count = 1;
            var rows = values.Select(s =>
            {
                parameters.Add(new MySqlParameter($"p{count}", s.Item2));
                return $"{s.Item1} = @p{count++}";
            });
            stringBuilder.Append(string.Join(',', rows));
            return parameters;
        }

        private static void GetMetaData<T>(T value, out string table, out List<(string, object)> values)//перебор свойств на объекте, для получения списка колонок, в которые потом пойдет запись
        {
            var type = value.GetType();
            var tableAtrributes = type.GetCustomAttributes(typeof(TableAttribute), false);
            table = ((TableAttribute)tableAtrributes.First()).Table;
            values = new List<(string, object)>();
            var props = type.GetProperties();
            foreach (var prop in props)
            {
                var columnAttributes = prop.GetCustomAttributes(typeof(ColumnAttribute), false);
                if (columnAttributes.Length > 0)
                {
                    string column = ((ColumnAttribute)columnAttributes.First()).Column;
                    values.Add(new(column, prop.GetValue(value)));
                }
            }
        }
    }
}

