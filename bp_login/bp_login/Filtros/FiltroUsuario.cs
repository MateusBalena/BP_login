using bp_login.domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace bp_login.Filtros
{
    //Filtro usado como atributo para ações dentro ASP.NET CORE
    public class FiltroUsuario : Attribute, IActionFilter, IOrderedFilter
    {
        public int Order { get; set; } = int.MinValue;

        //Função executada sempre depois da ação final ser chamada
        public void OnActionExecuted(ActionExecutedContext context) { }

        //Função executada sempre antes da ação final ser chamada
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Controller is Controller controller)
            {
                Login usuario;
                try
                { usuario = JsonConvert.DeserializeObject<Login>(context.HttpContext.Session.GetString("usuarioSessao"));
                    controller.ViewBag.usuario = usuario; }
                catch (Exception) { usuario = null; controller.ViewBag.usuario = null; }

                if (usuario != null)
                {
                    context.Result = new RedirectToActionResult("Index", "Home", null);
                }
            }
        }
    }
}
