using SquareList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SquareList.Controllers
{
    public class AtualizaController : Controller
    {
        [HttpPost]
        public ActionResult UpdateDados(Cliente cliente)
        {

            Models.ultimateModel model = new Models.ultimateModel();
            model.operacoesmodel = new Models.Operacoes();
            model.operacoesmodel.AtualizarCliente(cliente);

            return Content("<script language='javascript' type='text/javascript'>" +
                "alert('Dados atualizados com sucesso!'); " +
                "window.location = '/Home/Index';" +
            "</script>");
        }
    }
}