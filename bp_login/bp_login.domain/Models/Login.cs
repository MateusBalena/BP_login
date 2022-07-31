using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bp_login.domain.Models
{
    //Model para definir as informações dos usuários a serem exibidas e manipuladas
    public class Login
    {
        public string login { get; set; }
        public string nome { get; set; }
        public DateTime? data_aniversario { get; set; }
        public DateTime? ultimaSessao { get; set; }
    }
}
