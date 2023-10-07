using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SquareList.Models;

namespace SquareList.Controllers
{
    public class HomeController : Controller
    {


        [HttpGet]
        public ActionResult Index()
        {
            Models.ultimateModel model = new Models.ultimateModel();
            model.consultamodel = new Models.Consulta();
            model.clientemodel = new Models.Cliente();
            model.nutricionistamodel = new Models.Nutricionista();
            model.operacoesmodel = new Models.Operacoes();
            
            
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(Cliente cliente)
        {
            Models.ultimateModel model = new Models.ultimateModel();
            model.operacoesmodel = new Models.Operacoes();
            model.operacoesmodel.InserirCliente(cliente);
            return Content("<script language='javascript' type='text/javascript'>" +
                "window.location = '/Home/Index'; " +
                "alert('Usuário cadastrado com sucesso!');" +
            "</script>");
        }



    }
}