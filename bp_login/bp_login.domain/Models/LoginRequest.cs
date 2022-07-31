using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bp_login.domain.Models
{
    //Model para definir as requisições de informações para o Login
    public class LoginRequest
    {
        public string login { get; set; }
        public string senha { get; set; }
    }
}
