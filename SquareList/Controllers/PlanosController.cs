using SquareList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SquareList.Controllers
{
    public class PlanosController : Controller
    {
        
        [HttpPost]
        [ActionName("Index")]
        public ActionResult AssinaturaPlano(Cliente cliente)
        {
            Models.ultimateModel model = new Models.ultimateModel();
            model.operacoesmodel = new Models.Operacoes();
            model.operacoesmodel.AssinarPlano(cliente);
            return Content("<script language='javascript' type='text/javascript'>" +
                "window.location = '/Home/Index'; " +
                "alert('Plano assinado com sucesso!');" +
            "</script>");
        }
    }
}