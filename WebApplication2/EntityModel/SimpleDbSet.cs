using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WebApplication2.EntityModel
{
    public class SimpleDbSet<T>
    {
        private readonly SqlConnection _sqlConnection;

        public SimpleDbSet(SqlConnection connection)
        {
            _sqlConnection = connection;
        }

        public T QueryStringFind(string query)
        {
            T obj = (T)Activator.CreateInstance(typeof(T));
            using (SqlCommand command=new SqlCommand(query,_sqlConnection))
            {
                using (SqlDataReader read = command.ExecuteReader())
                {       
                    while (read.Read())
                    {
                        foreach (PropertyInfo item in obj.GetType().GetProperties())
                        {
                            item.SetValue(obj, read[item.Name]);
                        }
                        return obj;
                    }
                }
            }
            return obj;
        }

        public IEnumerable<T> QueryStringAsList(string query)
        {
            List<T> data = new List<T>();
            using (SqlCommand command=new SqlCommand(query,_sqlConnection))
            {
                using (SqlDataReader read=command.ExecuteReader())
                {
                    PropertyInfo[] props = typeof(T).GetProperties();
                    while (read.Read())
                    {
                        data.Add(FillData(read, props));
                    }
                }
            }
            return data;
        }

        private T FillData(SqlDataReader reader,PropertyInfo[] props)
        {
            T obj = (T)Activator.CreateInstance(typeof(T));
            foreach (PropertyInfo item in props)
            {
                if(reader[item.Name]!=DBNull.Value && reader[item.Name]!=null)
                item.SetValue(obj, reader[item.Name]);
            }
            return obj;
        }
    }
}