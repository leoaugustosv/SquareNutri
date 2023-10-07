using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SquareList.Models;

namespace SquareList.Controllers
{
    public class LoginController : Controller
    {

        Operacoes op = new Operacoes();

        [HttpPost]
        [ActionName("Index")]
        public ActionResult Logar(Cliente user , Operacoes oper)
        {

            // enviando os dados do form para serem checados no banco
            op.LogarUser(user);

            // foi devolvido um resultado do banco, caso as informaççoes
            // do banco forem diferentes de nula, ou seja se foram
            // encontradas informações no banco serão criadas sessões
            if (user.emailCli != null && user.emailCli != null)
            {
                FormsAuthentication.SetAuthCookie(user.emailCli, false);
                Session["usuarioLogado"] = user.emailCli.ToString();
                Session["senhaLogado"] = user.senhaCli.ToString();
                Session["nomeLogado"] = user.nomeCli.ToString();
                Session["nivelLogado"] = user.codNiveisAcesso.ToString();
                

                //direcionando o usuario para pagina Index da Home
                return RedirectToAction("Index", "Home");
            }

            else
            {
                // se estiver errado usuário e senha permaneça na pagina login
                return Content("<script language='javascript' type='text/javascript'>" +
                "window.location = '/Home/Index'; " +
                "alert('Login Incorreto!');" +
            "</script>");
            }
        }
    



        // ao clicar no botão logout, estaremos deslogando do ssitema
        public ActionResult Logout()
        {
            Session["nomeLogado"] = null;
            Session["nivelLogado"] = null;
            Session["usuarioLogado"] = null; // destruindo a sessão.
            ViewBag.AlertMessage = "alert('Usuário deslogado com sucesso!')";
            return RedirectToAction("Index", "Home");
        }
    }
}


   