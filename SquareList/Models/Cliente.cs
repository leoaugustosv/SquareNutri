using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SquareList.Models
{
    public class Cliente    
    {
        public int codCli { get; set; }
        public int codNiveisAcesso { get; set; }
        public string planoCliente  { get; set; }
        [Display(Name = "nomeCli")]
        [RegularExpression(@"[a-zA-Z]+\s?[a-zA-Z]+$", ErrorMessage = "Nome inválido")]
        public string nomeCli { get; set; }

        [Display(Name = "sobrenomeCli")]
        [RegularExpression(@"[a-zA-Z]+\s?[a-zA-Z]+$", ErrorMessage = "Sobrenome inválido")]
        public string sobrenomeCli { get; set; }

        [Display(Name = "sexCli")]
        public string sexoCli { get; set; }

        [Display(Name = "cpfCli")]
        [RegularExpression(@"/^[0-9]{3}.?[0-9]{3}.?[0-9]{3}-?[0-9]{2}/", ErrorMessage = "Cpf inválido")]
        public string cpfCli { get; set; }

        [Display(Name = "telCli")]
        public string telCli { get; set; }

        [Display(Name = "endCli")]
        public string endCli { get; set; }

        [Display(Name = "complCli")]
        public string complCli { get; set; }

        [Display(Name = "emailCli")]
        public string emailCli { get; set; }

        [Display(Name = "senhaCli")]
        public string senhaCli { get; set; }
       


    }

}
