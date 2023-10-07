using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SquareList.Models;

namespace SquareList.Controllers
{
    public class ConsultaController : Controller
    {
        Operacoes op = new Operacoes();

        [HttpPost]
        [ActionName("Index")]
        public ActionResult Consult(Consulta consulta)
        {


            Models.ultimateModel model = new Models.ultimateModel();
            model.operacoesmodel = new Models.Operacoes();

            //Calculo minDay
            string minDia = DateTime.Now.AddDays(+1).Day.ToString();
            string minMes = DateTime.Now.Month.ToString();
            string minAno = DateTime.Now.Year.ToString();
            string min = "";
            min += string.Format("{0}-", minAno);
            if (Convert.ToInt32(minMes) < 10)
            {
                min += string.Format("0{0}-", minMes);
            }
            else
            {
                min += string.Format("{0}-", minMes);
            }
            if (Convert.ToInt32(minDia) < 10)
            {
                min += string.Format("0{0}", minDia);
            }
            else
            {
                min += string.Format("{0}", minDia);
            }

            //CalculoMaxDay
            string maxDia = DateTime.Now.AddDays(75).Day.ToString();
            string maxMes = DateTime.Now.AddDays(75).Month.ToString();
            string maxAno = DateTime.Now.AddDays(75).Year.ToString();
            string max = "";
            max += string.Format("{0}-", maxAno);
            if (Convert.ToInt32(maxMes) < 10)
            {
                max += string.Format("0{0}-", maxMes);
            }
            else
            {
                max += string.Format("{0}-", maxMes);
            }
            if (Convert.ToInt32(maxDia) < 10)
            {
                max += string.Format("0{0}", maxDia);
            }
            else
            {
                max += string.Format("{0}", maxDia);
            }

            ViewBag.Min = min;
            ViewBag.Max = max;

            model.operacoesmodel.MarcarConsulta(consulta);
            if (op.rowsAfetadas != 0)
            {
                return Content("<script language='javascript' type='text/javascript'>" +
                    "alert('Consulta marcada com sucesso!'); " +
                    "window.location = '/Home/Index';" +
                "</script>");
            }
            else
            {

                return Content("<script language='javascript' type='text/javascript'>" +
                   "alert('Já existe uma consulta marcada nesse horário com o mesmo nutricionista! Tente outro horário.'); " +
                   "window.location = '/Home/Index';" +
               "</script>");
            }
        }

    }
}