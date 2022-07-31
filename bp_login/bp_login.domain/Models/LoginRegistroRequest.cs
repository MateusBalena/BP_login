using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bp_login.domain.Models
{
    //Model para definir as requisições de informações de registro de usuário
    public class LoginRegistroRequest
    {
        public string login { get; set; }
        public string nome { get; set; }
        public string senha { get; set; }
        public DateTime data_aniversario { get; set; }

    }
}
