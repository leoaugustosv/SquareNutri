using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SquareList.Models
{
    public class Operacoes
    {

        private Banco banco;

        protected static int codeUser { get; set; }
        public string nomeUser { get; set; }
        public int nivelUser { get; set; }
        public int rowsAfetadas { get; set; }

        //---------------------------------
        //CADASTRO DE USUÁRIO
        //---------------------------------

        public void InserirCliente(Cliente cliente)
        {
            var strQuery = "";
            using (banco = new Banco())
            {
                strQuery += "INSERT INTO tbCliente(codNiveisAcesso, nomeCli, sobrenomeCli, telCli, cpfCli, sexCli, endCli, complCli, emailCli, senhaCli, planoCliente)";
                strQuery += string.Format("VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}', '{9}', '{10}');", 1 , cliente.nomeCli, cliente.sobrenomeCli, cliente.telCli, cliente.cpfCli, cliente.sexoCli, cliente.endCli, cliente.complCli, cliente.emailCli, cliente.senhaCli, "Nenhum");
                banco.Executar(strQuery);
            }

            strQuery = "";
            strQuery += string.Format("SELECT complCli FROM tbCliente WHERE emailCli = '{0}';", cliente.emailCli);
            SqlCommand command = new SqlCommand(strQuery);

            using (banco = new Banco())
            {


                var nomeUsu = banco.Retorno(strQuery);

                while (nomeUsu.Read())
                {
                    Cliente complement = new Cliente();
                    {
                        complement.complCli = Convert.ToString(nomeUsu["complCli"]);
                    }
                    strQuery = "";
                    strQuery += string.Format("UPDATE tbCliente SET complCli='{0}' ;", "N/A");

                    banco.Executar(strQuery);
                }
            }

        }


        //---------------------------------
        //MARCANDO CONSULTA NORMAL
        //---------------------------------


        public void MarcarConsulta(Consulta consulta)
        {
            var strQuery = "";



            using (banco = new Banco())
            {
                strQuery += string.Format("SELECT * FROM tbConsulta WHERE codNutricionista = '{0}' AND dataConsulta= CONVERT(DATETIME, '{1}' , 103) AND horaConsulta='{2}'", consulta.codNutricionista, consulta.dataConsulta, consulta.horaConsulta);
                var nomeUsur = banco.Retorno(strQuery);

                if (nomeUsur.HasRows)
                {
                    rowsAfetadas = 1;
                    nomeUsur.Close();
                }
                else
                {
                    rowsAfetadas = 0;
                    using (banco = new Banco())
                    {
                        strQuery = "";
                        strQuery += "INSERT INTO tbConsulta(codNutricionista, dataConsulta, horaConsulta)";
                        strQuery += string.Format("VALUES ('{0}', CONVERT(DATETIME, '{1}' , 103) , '{2}')", consulta.codNutricionista, consulta.dataConsulta, consulta.horaConsulta);
                        rowsAfetadas = 1;

                        banco.Executar(strQuery);

                    }

                    string userEmail = Convert.ToString(HttpContext.Current.Session["usuarioLogado"]);
                    strQuery = "";
                    strQuery += string.Format("SELECT codCli FROM tbCliente WHERE emailCli = '{0}';", userEmail);
                    SqlCommand command = new SqlCommand(strQuery);

                    using (banco = new Banco())
                    {


                        var nomeUsu = banco.Retorno(strQuery);

                        while (nomeUsu.Read())
                        {
                            Consulta dto = new Consulta();
                            {
                                dto.codCli = Convert.ToInt32(nomeUsu["codCli"]);
                            }
                            strQuery = "";
                            strQuery += string.Format("UPDATE tbConsulta SET codCli='{0}' WHERE codCli IS NULL ;", dto.codCli);

                            banco.Executar(strQuery);
                        }
                    }



                    strQuery = "";
                    strQuery += string.Format("SELECT * FROM tbNutri tbNut JOIN tbConsulta tbCons ON tbNut.codNutri = tbCons.codNutricionista WHERE tbNut.codNutri = '{0}';", consulta.codNutricionista);
                    SqlCommand commando = new SqlCommand(strQuery);

                    using (banco = new Banco())
                    {


                        var nomeUsu = banco.Retorno(strQuery);

                        while (nomeUsu.Read())
                        {
                            Consulta nutrio = new Consulta();
                            {
                                nutrio.nomeNutricionista = Convert.ToString(nomeUsu["nomeNutri"]);
                                nutrio.especialidadeNutri = Convert.ToString(nomeUsu["especialidade"]);
                            }
                            strQuery = "";
                            strQuery += string.Format("UPDATE tbConsulta SET nomeNutricionista='{0}', especialidadeNutri='{1}' FROM tbConsulta tbCons JOIN tbNutri tbNut ON tbCons.codNutricionista = tbNut.codNutri WHERE tbCons.codNutricionista='{2}';", nutrio.nomeNutricionista, nutrio.especialidadeNutri, consulta.codNutricionista);

                            banco.Executar(strQuery);
                        }
                    }

                }
            }
            
        }

        //---------------------------------
        //LOGANDO USUÁRIO
        //---------------------------------

        public Cliente LogarUser(Cliente login)
        {
            var strQuery = "";
            strQuery += string.Format("SELECT * FROM tbCliente WHERE emailCli='{0}' AND senhaCli='{1}'", login.emailCli, login.senhaCli);

            using (banco = new Banco())
            {

                var nomeUsu = banco.Retorno(strQuery);

                if (nomeUsu.HasRows)
                {

                    while (nomeUsu.Read())
                    {
                        Cliente dto = new Cliente();
                        {
                            dto.emailCli = Convert.ToString(nomeUsu["emailCli"]);
                            dto.senhaCli = Convert.ToString(nomeUsu["senhaCli"]);
                            dto.nomeCli = Convert.ToString(nomeUsu["nomeCli"]);
                            dto.codNiveisAcesso = Convert.ToInt32(nomeUsu["codNiveisAcesso"]);

                        }

                        login.nomeCli = dto.nomeCli;
                        login.codNiveisAcesso = dto.codNiveisAcesso;

                        
                    }
                    
                        
                    
                }


                else
                {
                    login.emailCli = null;
                    login.senhaCli = null;
                    login.nomeCli = null;
                    login.codNiveisAcesso = 0;
                }
            }


            return login;

        }
        //-------------------------
        //LISTANDO DADOS DO CLIENTE
        //-------------------------
        public List<Cliente> ListagemMeusDados()
        {
            using (banco = new Banco())
            {
                string userEmail = Convert.ToString(HttpContext.Current.Session["usuarioLogado"]);
                var strQuery = "";
                strQuery += string.Format("SELECT * FROM tbCliente WHERE emailCli = '{0}';", userEmail);
                var retorno = banco.Retorno(strQuery);
                return MeusDados(retorno);
            }

        }

        public List<Cliente> MeusDados(SqlDataReader retorno)
        {
            var listaDados = new List<Cliente>();
            while (retorno.Read())
            {
                var dadosUsuario = new Cliente()
                {
                    codCli = Int32.Parse(retorno["codCli"].ToString()),
                    nomeCli = retorno["nomeCli"].ToString(),
                    sobrenomeCli = retorno["sobrenomeCli"].ToString(),
                    sexoCli = retorno["sexCli"].ToString(),
                    cpfCli = retorno["cpfCli"].ToString(),
                    telCli = retorno["telCli"].ToString(),
                    endCli = retorno["endCli"].ToString(),
                    complCli = retorno["complCli"].ToString(),
                    planoCliente = retorno["planoCliente"].ToString(),
                    emailCli = retorno["emailCli"].ToString(),
                    senhaCli = retorno["senhaCli"].ToString()
                };

                listaDados.Add(dadosUsuario);


            }
            retorno.Close();
            return listaDados;

        }

        //---------------------------------
        //LISTANDO CONSULTAS DO CLIENTE
        //---------------------------------


        public List<Consulta> ListagemMinhasConsultas()
        {
            string userEmail = Convert.ToString(HttpContext.Current.Session["usuarioLogado"]);
            var strQuery = "";
            strQuery += string.Format("SELECT codCli FROM tbCliente WHERE emailCli = '{0}';", userEmail);
            SqlCommand command = new SqlCommand(strQuery);

            using (banco = new Banco())
            {


                var nomeUsu = banco.Retorno(strQuery);

                while (nomeUsu.Read())
                {
                    Consulta dto = new Consulta();
                    {
                        dto.codCli = Convert.ToInt32(nomeUsu["codCli"]);
                    }
                    codeUser = dto.codCli;
                }
                
            


                var strQuery2 = "";
                strQuery2 += string.Format("SELECT * FROM tbConsulta tbCons JOIN tbCliente tbCli ON tbCons.codCli = tbCli.codCli WHERE tbCli.codCli='{0}' AND tbCli.emailCli='{1}';", codeUser, userEmail);
                var retorno = banco.Retorno(strQuery2);
                return MinhasConsultas(retorno);
            }

        }

        public List<Consulta> MinhasConsultas(SqlDataReader retorno)
        {
            var listaConsulta = new List<Consulta>();
            while (retorno.Read())
            {
                var consultaUsuario = new Consulta()
                {
                    codConsulta = Int32.Parse(retorno["codConsulta"].ToString()),
                    codNutricionista = Int32.Parse(retorno["codNutricionista"].ToString()),
                    dataConsulta = DateTime.Parse(retorno["dataConsulta"].ToString()),
                    horaConsulta = retorno["horaConsulta"].ToString(),
                    nomeNutricionista = retorno["nomeNutricionista"].ToString(),
                    especialidadeNutri = retorno["especialidadeNutri"].ToString()
                };

                listaConsulta.Add(consultaUsuario);


            }
            retorno.Close();
            return listaConsulta;

        }


        //---------------------------------
        //ASSINANDO PLANO
        //---------------------------------

        public void AssinarPlano(Cliente cliente)
        {
            var strQuery = "";
            using (banco = new Banco())
            {
                strQuery += string.Format("UPDATE tbCliente SET planoCliente='{0}' ;", cliente.planoCliente);
                banco.Executar(strQuery);
            }

        }



        //-------------------------
        //MASTER - LISTANDO OS CLIENTES
        //-------------------------
        public List<Cliente> ListagemClientes()
        {
            using (banco = new Banco())
            {
                var strQuery = "";
                strQuery += string.Format("SELECT * FROM tbCliente WHERE codNiveisAcesso='{0}'", 1);
                var retorno = banco.Retorno(strQuery);
                return allClientes(retorno);
            }

        }

        public List<Cliente> allClientes(SqlDataReader retorno)
        {
            var listaDados = new List<Cliente>();
            while (retorno.Read())
            {
                var dadosClientes = new Cliente()
                {
                    codCli = Int32.Parse(retorno["codCli"].ToString()),
                    nomeCli = retorno["nomeCli"].ToString(),
                    sobrenomeCli = retorno["sobrenomeCli"].ToString(),
                    sexoCli = retorno["sexCli"].ToString(),
                    cpfCli = retorno["cpfCli"].ToString(),
                    telCli = retorno["telCli"].ToString(),
                    endCli = retorno["endCli"].ToString(),
                    complCli = retorno["complCli"].ToString(),
                    planoCliente = retorno["planoCliente"].ToString(),
                    emailCli = retorno["emailCli"].ToString(),
                    senhaCli = retorno["senhaCli"].ToString()
                };

                listaDados.Add(dadosClientes);


            }
            retorno.Close();
            return listaDados;

        }


        //-------------------------
        //MASTER - LISTANDO TODAS AS CONSULTAS
        //-------------------------
        public List<Consulta> ListagemTodasConsultas()
        {
            using (banco = new Banco())
            {
                var strQuery = "";
                strQuery += string.Format("SELECT * FROM tbConsulta ORDER BY dataConsulta desc", 1);
                var retorno = banco.Retorno(strQuery);
                return allConsultas(retorno);
            }

        }

        public List<Consulta> allConsultas(SqlDataReader retorno)
        {
            {
                var listaConsulta = new List<Consulta>();
                while (retorno.Read())
                {
                    var consultaUsuario = new Consulta()
                    {
                        codConsulta = Int32.Parse(retorno["codConsulta"].ToString()),
                        codNutricionista = Int32.Parse(retorno["codNutricionista"].ToString()),
                        dataConsulta = DateTime.Parse(retorno["dataConsulta"].ToString()),
                        horaConsulta = retorno["horaConsulta"].ToString(),
                        nomeNutricionista = retorno["nomeNutricionista"].ToString(),
                        especialidadeNutri = retorno["especialidadeNutri"].ToString(),
                        codCli = Int32.Parse(retorno["codCli"].ToString())
                    };

                    listaConsulta.Add(consultaUsuario);


                }
                retorno.Close();
                return listaConsulta;

            }
        }



        //-------------------------
        //MASTER - LISTANDO AS CONSULTAS DE HOJE
        //-------------------------
        public List<Consulta> ListagemHojeConsultas()
        {
            using (banco = new Banco())
            {
                var strQuery = "";
                strQuery += string.Format("SELECT * FROM tbConsulta WHERE dataConsulta = cast(getdate() as date) ORDER BY horaConsulta ASC");
                var retorno = banco.Retorno(strQuery);
                return hojeConsultas(retorno);
            }

        }

        public List<Consulta> hojeConsultas(SqlDataReader retorno)
        {
            {
                var listaConsulta = new List<Consulta>();
                while (retorno.Read())
                {
                    var consultaUsuario = new Consulta()
                    {
                        codConsulta = Int32.Parse(retorno["codConsulta"].ToString()),
                        codNutricionista = Int32.Parse(retorno["codNutricionista"].ToString()),
                        dataConsulta = DateTime.Parse(retorno["dataConsulta"].ToString()),
                        horaConsulta = retorno["horaConsulta"].ToString(),
                        nomeNutricionista = retorno["nomeNutricionista"].ToString(),
                        especialidadeNutri = retorno["especialidadeNutri"].ToString(),
                        codCli = Int32.Parse(retorno["codCli"].ToString())
                    };

                    listaConsulta.Add(consultaUsuario);


                }
                retorno.Close();
                return listaConsulta;

            }
        }


        //-------------------------
        //MASTER - MARCANDO CONSULTA
        //-------------------------


        public void MarcandoConsultaMaster(Consulta consulta)
        {
            var strQuery = "";

            using (banco = new Banco())
            {
                strQuery += "INSERT INTO tbConsulta(codNutricionista, dataConsulta, horaConsulta, codCli)";
                strQuery += string.Format("VALUES ('{0}', CONVERT(DATETIME, '{1}' , 103) , '{2}', '{3}')", consulta.codNutricionista, consulta.dataConsulta, consulta.horaConsulta, consulta.codCli);


                banco.Executar(strQuery);




            }



            strQuery = "";
            strQuery += string.Format("SELECT * FROM tbNutri tbNut JOIN tbConsulta tbCons ON tbNut.codNutri = tbCons.codNutricionista WHERE tbNut.codNutri = '{0}';", consulta.codNutricionista);
            SqlCommand commando = new SqlCommand(strQuery);

            using (banco = new Banco())
            {


                var nomeUsu = banco.Retorno(strQuery);

                while (nomeUsu.Read())
                {
                    Consulta nutrio = new Consulta();
                    {
                        nutrio.nomeNutricionista = Convert.ToString(nomeUsu["nomeNutri"]);
                        nutrio.especialidadeNutri = Convert.ToString(nomeUsu["especialidade"]);
                    }
                    strQuery = "";
                    strQuery += string.Format("UPDATE tbConsulta SET nomeNutricionista='{0}', especialidadeNutri='{1}' FROM tbConsulta tbCons JOIN tbNutri tbNut ON tbCons.codNutricionista = tbNut.codNutri WHERE tbCons.codNutricionista='{2}' AND tbCons.codCli='{3}';", nutrio.nomeNutricionista, nutrio.especialidadeNutri, consulta.codNutricionista, consulta.codCli);

                    banco.Executar(strQuery);
                }
            }


        }



        //-------------------------
        //MASTER - DESMARCANDO CONSULTA
        //-------------------------


        public void DesmarcandoConsultaMaster(Consulta consulta)
        {
            var strQuery = "";

            using (banco = new Banco())
            {
                strQuery += string.Format("DELETE FROM tbConsulta WHERE codConsulta='{0}';", consulta.codConsulta);


                banco.Executar(strQuery);




            }



           


        }



        //-------------------------
        //Atualizando Dados do Cliente
        //-------------------------


        public void AtualizarCliente(Cliente cliente)
        {
            string userEmail = Convert.ToString(HttpContext.Current.Session["usuarioLogado"]);
            var strQuery = "";
            using (banco = new Banco())
            {
                strQuery += string.Format("UPDATE tbCliente SET telCli='{0}', endCli='{1}', complCli='{2}', senhaCli='{3}' WHERE emailCli = '{4}';", cliente.telCli, cliente.endCli, cliente.complCli, cliente.senhaCli, userEmail);
                banco.Executar(strQuery);
            }

        }




    }
}


                    
               