using bp_login.domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace bp_login.Filtros
{
    //Filtro usado SEMPRE que qualquer ação dentro ASP.NET CORE é chamado
    public class FiltroGlobal : IActionFilter, IOrderedFilter
    {
        public int Order { get; set; } = int.MinValue;

        //Função executada sempre depois da ação final ser chamada
        public void OnActionExecuted(ActionExecutedContext context) { }

        //Função executada sempre antes da ação final ser chamada
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Controller is Controller controller)
            {
                try
                {
                    Login usuario = JsonConvert.DeserializeObject<Login>(context.HttpContext.Session.GetString("usuarioSessao"));
                    controller.ViewBag.usuario = usuario;
                }
                catch (Exception) { controller.ViewBag.usuario = null; }
            }
        }
    }
}
