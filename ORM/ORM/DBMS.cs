using System.Data;
using System.Data.SqlClient;

namespace ORM
{
  public class DBMS
  {
       // private string Conection = "Data Source=.;Initial Catalog=dgbtcouk_DB;User ID=dgbtcouk_User; Password=1qaz@WSX;Pooling=true; Max Pool Size=200";
        public static string Conection = "data source=.;initial catalog=TokenShop;integrated security=true;Pooling=true; Max Pool Size=5";
    SqlConnection sqlcon = null;
    SqlCommand sqlcom = new SqlCommand();
    SqlDataReader dr;
    private DataTable MyTable = (DataTable)null;
    public void ExecuteSp(string SPName, SqlParameter[] Parameter)
    {
        sqlcon = new SqlConnection(Conection);
        sqlcom.Connection = sqlcon;
        sqlcon.Open();
        sqlcom.CommandType = CommandType.StoredProcedure;
        sqlcom.CommandText = SPName;
        sqlcom.Parameters.Clear();
        foreach (SqlParameter spv in Parameter)
            sqlcom.Parameters.Add(spv);
        sqlcom.ExecuteNonQuery();
        sqlcon.Close();
        SqlConnection.ClearPool(sqlcon);
    }
    public void CloseAfterRead()
    {
        dr = null;
        sqlcon.Close();
        SqlConnection.ClearPool(sqlcon);
    }
    public SqlDataReader ExecuteSpReader(string SPName, SqlParameter[] Parameter)
    {
        sqlcon = new SqlConnection(Conection);
        sqlcom.Connection = sqlcon;
        sqlcon.Open();
        sqlcom.CommandType = CommandType.StoredProcedure;
        sqlcom.CommandText = SPName;
        sqlcom.Parameters.Clear();
        foreach (SqlParameter spv in Parameter)
            sqlcom.Parameters.Add(spv);
        dr = null;
        dr = sqlcom.ExecuteReader();
        return dr;
    }
    public DataTable ExecuteReader(string Query)
    {
        sqlcon = new SqlConnection(Conection);
        sqlcom.Connection = sqlcon;;
        sqlcon.Open();
        sqlcom.CommandText = Query;
        MyTable = new DataTable();
        MyTable.Load(sqlcom.ExecuteReader());
        sqlcon.Close();
        return MyTable;
    }
    public void Execute(string Query)
    {
        this.sqlcom.Connection = this.sqlcon;
        this.sqlcon.Open();
        sqlcom.CommandText = Query;
        sqlcom.ExecuteNonQuery();
    }
  }
}
