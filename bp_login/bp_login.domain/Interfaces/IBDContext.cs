using System.Data.SqlClient;

namespace bp_login.data.Context
{
    public interface IBDContext
    {
        SqlConnection data();
    }
}