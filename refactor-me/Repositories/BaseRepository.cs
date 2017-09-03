using System.Data.SqlClient;
using System.Web;

namespace refactor_me.Repositories
{
    public abstract class BaseRepository
    {
        private const string ConnectionString =
                @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={DataDirectory}\Database.mdf;Integrated Security=True";

        internal SqlConnection GetNewConnection()
        {
            var connstr = ConnectionString.Replace("{DataDirectory}", HttpContext.Current.Server.MapPath("~/App_Data"));
            return new SqlConnection(connstr);
        }
    }
}