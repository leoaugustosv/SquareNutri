using SquareList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SquareList.Controllers
{
    public class AcoesController : Controller
    {
        [HttpPost]
        public ActionResult DesmarcarConsulta(Consulta consulta)
        {
            
            Models.ultimateModel model = new Models.ultimateModel();
            model.operacoesmodel = new Models.Operacoes();
            model.operacoesmodel.DesmarcandoConsultaMaster(consulta);

            return Content("<script language='javascript' type='text/javascript'>" +
                "alert('Consulta desmarcada com sucesso!'); " +
                "window.location = '/Home/Index';" +
            "</script>");
        }
    }
}