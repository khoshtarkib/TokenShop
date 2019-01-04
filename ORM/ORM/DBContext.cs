using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System;

namespace ORM
{
    public class DBContext<T>
    {
        private enum action
        {
            add,update,remove,getOne
        }
        public string EntityCreteQuery()
        {
            string Query = "CREATE TABLE [dbo].["+this.EntityName + "](";
            PropertyInfo[] prop = this.GetType().GetProperties();
            List<SqlParameter> parameters = new List<SqlParameter>();
            for (int i = 0; i < prop.Length; i++)
            {
                MetaData meta = new MetaData();
                IEnumerable<Attribute> attr = prop[i].GetCustomAttributes();
                foreach (Attribute a in attr)
                {
                    if (a.GetType() == typeof(MetaData))
                        meta = ((MetaData)a);
                }
                bool valid_parameter = true;

                if (meta.NotDbField)
                    valid_parameter = false;

                if (valid_parameter)
                {
                    string type = "[nvarchar](50)";
                    Type tp = prop[i].PropertyType;
                    if (tp == typeof(Int32))
                        type = "[int]";
                    if (tp == typeof(Int64))
                        type = "[bigint]";
                    if (tp == typeof(bool))
                        type = "[bit]";
                    if (tp == typeof(DateTime))
                        type = "[datetime]";
                    if (tp == typeof(String))
                        type = "[nvarchar](50)";
                    Query += "[" + prop[i].Name + "]  " + type + " " + (meta.IsIdentity ? "IDENTITY(1,1)" : "") + "  " + (meta.IsIdentity ? "not NULL" : "NULL") + " " + (meta.IsIdentity ? "primary key" : "") + ",";
                }
            }
            Query += " ) ";
            return Query;
        }
        private SqlParameter[] ParameterGenerate(action fromAction)
        {
            PropertyInfo[] prop = this.GetType().GetProperties();
            List<SqlParameter> parameters = new List<SqlParameter>();
            for (int i = 0; i < prop.Length; i++)
            {
                MetaData meta = new MetaData();
                IEnumerable<Attribute> attr = prop[i].GetCustomAttributes();
                foreach(Attribute a in attr)
                {
                    if (a.GetType() == typeof(MetaData))
                        meta = ((MetaData)a);

                }
                bool valid_parameter = true;

                if (meta.NotDbField || meta.ViewField)
                    valid_parameter = false;
                if (fromAction == action.add && meta.IsIdentity)
                    valid_parameter = false;
                if (fromAction == action.remove && !meta.IsPrimary)
                    valid_parameter = false;
                if (fromAction == action.getOne && !meta.IsPrimary)
                    valid_parameter = false;

                if (valid_parameter)
                    parameters.Add(new SqlParameter("@" + prop[i].Name, prop[i].GetValue(this, null)));
            }
            return parameters.ToArray();
        }
        private string EntityName
        {
            get {
                return this.GetType().Name;
            }
        }
        public void Update()
        {
            DBMS db = new DBMS();
            db.ExecuteSp("update_" + this.EntityName, this.ParameterGenerate(action.update));
        }
        public void Add()
        {
            DBMS db = new DBMS();
            SqlDataReader dr=db.ExecuteSpReader("insert_" + this.EntityName, this.ParameterGenerate(action.add));
            PropertyInfo[] prop = this.GetType().GetProperties();
            for (int i = 0; i < prop.Length; i++)
            {
                MetaData meta = new MetaData();
                IEnumerable<Attribute> attr = prop[i].GetCustomAttributes();
                foreach (Attribute a in attr)
                {
                    if (a.GetType() == typeof(MetaData))
                        meta = ((MetaData)a);

                }
                if (meta.IsIdentity)
                {
                    Type FieldType = prop[i].PropertyType;
                    while (dr.Read())
                    {
                        if (FieldType == typeof(Int64))
                            this.GetType().GetProperty(prop[i].Name).SetValue(this, long.Parse(dr[0].ToString()), null);
                        if (FieldType == typeof(Int32))
                            this.GetType().GetProperty(prop[i].Name).SetValue(this, int.Parse(dr[0].ToString()), null);
                        if (FieldType == typeof(bool))
                            this.GetType().GetProperty(prop[i].Name).SetValue(this, bool.Parse(dr[0].ToString()), null);
                        if (FieldType == typeof(DateTime))
                            this.GetType().GetProperty(prop[i].Name).SetValue(this, (DateTime)dr[0], null);
                        if (FieldType == typeof(String))
                            this.GetType().GetProperty(prop[i].Name).SetValue(this, dr[0].ToString(), null);

                    }
                }

            }
        }
        public void Remove()
        {
            DBMS db = new DBMS();
            db.ExecuteSp("remove_" + this.EntityName, this.ParameterGenerate(action.remove));
        }
        public T GetOne()
        {
            DBMS db = new DBMS();
            SqlDataReader dr = db.ExecuteSpReader("SelectOne_" + this.EntityName, this.ParameterGenerate(action.getOne));

            T record = (T)Activator.CreateInstance(typeof(T));
            while (dr.Read())
            {
                PropertyInfo[] prop = record.GetType().GetProperties();
                for (int i = 0; i < prop.Length; i++)
                {
                    MetaData meta = new MetaData();
                    IEnumerable<Attribute> attr = prop[i].GetCustomAttributes();
                    foreach (Attribute a in attr)
                    {
                        if (a.GetType() == typeof(MetaData))
                            meta = ((MetaData)a);
                    }
                    bool valid_parameter = true;
                    if (meta.NotDbField && meta.ViewField == false)
                        valid_parameter = false;
                    try
                    {
                        object test = dr[prop[i].Name];
                    }
                    catch { valid_parameter = false; }
                    if (valid_parameter)
                    {
                        Type FieldType = prop[i].PropertyType;
                        if (FieldType == typeof(Int64))
                            record.GetType().GetProperty(prop[i].Name).SetValue(record, Int64.Parse(dr[prop[i].Name].ToString()), null);
                        if (FieldType == typeof(Int32))
                            record.GetType().GetProperty(prop[i].Name).SetValue(record, Int32.Parse(dr[prop[i].Name].ToString()), null);
                        if (FieldType == typeof(bool))
                            record.GetType().GetProperty(prop[i].Name).SetValue(record, bool.Parse(dr[prop[i].Name].ToString()), null);
                        if (FieldType == typeof(DateTime))
                            record.GetType().GetProperty(prop[i].Name).SetValue(record, DateTime.Parse(dr[prop[i].Name].ToString()), null);
                        if (FieldType == typeof(String))
                            record.GetType().GetProperty(prop[i].Name).SetValue(record, dr[prop[i].Name].ToString(), null);
                     }
                }
            }
            db.CloseAfterRead();
            return record;
        }
        /// <summary>
        /// key -> parameterName
        /// value -> parameterName
        /// </summary>
        /// <param name="Q"></param>
        /// <returns></returns>
        public List<T> GetAll(Dictionary<string,string> Q=null)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            if (Q!=null)
                foreach (KeyValuePair<string, string> q in Q)
                    parameters.Add(new SqlParameter("@"+q.Key, q.Value));
            DBMS db = new DBMS();
            SqlDataReader dr = db.ExecuteSpReader("SelectAll_" + this.EntityName, parameters.ToArray());
            List<T> records =new List<T>();
            while (dr.Read())
            {
                T record = (T)Activator.CreateInstance(typeof(T));
                PropertyInfo[] prop = record.GetType().GetProperties();
                for (int i = 0; i < prop.Length; i++)
                {
                    MetaData meta = new MetaData();
                    IEnumerable<Attribute> attr = prop[i].GetCustomAttributes();
                    foreach (Attribute a in attr)
                    {
                        if (a.GetType() == typeof(MetaData))
                            meta = ((MetaData)a);
                    }
                    bool valid_parameter = true;
                    if (meta.NotDbField && meta.ViewField==false)
                        valid_parameter = false;
                    try
                    {
                        object test = dr[prop[i].Name];
                    }
                    catch { valid_parameter = false; }

                    if (valid_parameter)
                    {
                        Type FieldType = prop[i].PropertyType;
                        if (FieldType == typeof(Int64))
                            record.GetType().GetProperty(prop[i].Name).SetValue(record, long.Parse(dr[prop[i].Name].ToString()), null);
                        if (FieldType == typeof(Int32))
                            record.GetType().GetProperty(prop[i].Name).SetValue(record, int.Parse(dr[prop[i].Name].ToString()), null);
                        if (FieldType == typeof(bool))
                            record.GetType().GetProperty(prop[i].Name).SetValue(record, bool.Parse(dr[prop[i].Name].ToString()), null);
                        if (FieldType == typeof(DateTime))
                            record.GetType().GetProperty(prop[i].Name).SetValue(record, DateTime.Parse(dr[prop[i].Name].ToString()), null);
                        if (FieldType == typeof(String))
                            record.GetType().GetProperty(prop[i].Name).SetValue(record, dr[prop[i].Name].ToString(), null);
                    }
                }
                records.Add(record);
            }
            db.CloseAfterRead();
            return records;
        }
    }
}
