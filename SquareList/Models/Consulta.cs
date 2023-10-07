using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SquareList.Models
{
    public class Consulta
    {
        public int codConsulta { get; set; }
        public int codCli { get; set; }
        public int codNutricionista { get; set; }
        public string nomeNutricionista { get; set; }
        public string especialidadeNutri { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime dataConsulta { get; set; }

        public string horaConsulta { get; set; }
        
        public string Pagamento { get; set; }

    }
}