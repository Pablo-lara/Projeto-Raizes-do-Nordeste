using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ProjetoRaizes.Filters
{
    public class VerificarConsentimento : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            bool usuarioAceitouTermos = false;

            if (!usuarioAceitouTermos)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 403,
                    Content = "Acesso negado: Você precisa aceitar os termos de uso de dados."
                };
            }
        }
    }
}
