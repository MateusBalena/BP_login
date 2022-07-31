using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bp_login.data.Context
{
    public class BDContext : IBDContext
    {        
        private readonly IConfiguration config;

        public BDContext(IConfiguration config) { this.config = config; }

        //Configura a conexão com o banco de dados baseado na connectionString declara no arquivo "appsettings.json"
        public SqlConnection data() => new SqlConnection(config.GetConnectionString("data"));
    }
}
