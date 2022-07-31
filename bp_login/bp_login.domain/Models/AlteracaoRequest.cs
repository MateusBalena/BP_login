using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bp_login.domain.Models
{
    //Model utilizado para receber as requisições de informação para alterar informações do usuário
    public class AlteracaoRequest
    {
        public string? nome { get; set; }
        public DateTime? data { get; set; }
        public string action { get; set; }
    }
}
