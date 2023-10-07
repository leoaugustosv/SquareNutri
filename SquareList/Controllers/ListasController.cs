using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SquareList.Models;

namespace SquareList.Controllers
{
    public class ListasController : Controller
    {


        public ActionResult ListarMeusDados()
        {

            var listarDados = new Operacoes();
            var listarTodosdados = listarDados.ListagemMeusDados();
            return PartialView("~/Views/Listas/ListarMeusDados.cshtml", listarTodosdados);
            //return PartialView("Listas/ListarMeusDados");
        }

        
        public ActionResult ListarMinhasConsultas()
        {
        var listarConsultas = new Operacoes();
        var listarAsConsultas = listarConsultas.ListagemMinhasConsultas();
            if (!listarAsConsultas.Any())
            {
                ViewData["consultas"] = null;
                return PartialView("~/Views/Listas/ListarMinhasConsultas.cshtml", listarAsConsultas);
            }
            else
            {
                ViewData["consultas"] = "value";
                return PartialView("~/Views/Listas/ListarMinhasConsultas.cshtml", listarAsConsultas);
            }
        }




    }
}