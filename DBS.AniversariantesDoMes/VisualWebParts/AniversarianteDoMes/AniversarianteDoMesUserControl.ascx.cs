using System;
using System.Web.UI;
using Microsoft.SharePoint;
using System.Collections.Generic;
using System.Text;
using DBS.AniversariantesDoMes.VO;

namespace DBS.AniversariantesDoMes.VisualWebParts.AniversarianteDoMes
{
    public partial class AniversarianteDoMesUserControl : UserControl
    {

        private string[] MESES = {"Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", 
                                     "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"};

        private SPWeb oWebsiteRoot;

        //Nome da coluna de nascimento na lista
        private const String COLUNA_NASCIMENTO = "Data Nascimento";
        private const String CAMINHO_LISTA_MURAL = "../Lists/Mural";

        private const String NOME_IMAGEM_BALAOZINHO = "/_layouts/images/DBS.AniversariantesDoMes/Chat_32.png";
        private const String NOME_PAGINA_MSG_ANIVERSARIANTE = "/_layouts/DBS.AniversariantesDoMes/Pages/MensagensAniversario.aspx";

        private Guid guidListMural;

        //todo: corrigir a visão e os parametros passados
        private const String VISAO_LISTA_MURAL = "Lists/Mural/";

        private void carregaConfereVariaveis()
        {
            oWebsiteRoot = SPContext.Current.Site.RootWeb;

            //Lista vazia que pode ter as listas e colunas não encontradas
            StringBuilder itensNaoEncontrados = new StringBuilder();

            #region "Lista de Usuários"

            String[] colunasListaUsuarios = { "Data Nascimento", "Nome Resumido" };

            SPList usuarios = oWebsiteRoot.SiteUserInfoList;

            foreach (String coluna in colunasListaUsuarios)
            {
                try
                {
                    Guid z = usuarios.Fields[coluna].Id;
                }
                catch (ArgumentException)
                {
                    itensNaoEncontrados.AppendLine("SiteUserInfoList[\"" + coluna + "\"]");
                }
            }

            #endregion "Lista de Usuários"

            #region "Lista do Mural"

            StringBuilder sb = new StringBuilder();

            foreach (SPList lista in oWebsiteRoot.Lists)
            {
                sb.AppendLine("titulo = " + lista.Title + " ; ID = " + lista.ID.ToString());
            }

            try
            {
                guidListMural = oWebsiteRoot.Lists["Mural de Mensagens"].ID;
            }
            catch (ArgumentException)
            {
                itensNaoEncontrados.AppendLine("Lista \"Mural\"");
            }

            #endregion "Lista do Mural"

            if (itensNaoEncontrados.ToString().Length > 0)
            {
                throw new Exception("Os seguintes itens não foram encontrados: " +
                    Environment.NewLine + itensNaoEncontrados.ToString() +
                    " Certifique-se que eles existam no seu site antes de utilizar essa webpart.");
            }
        }

        private List<Aniversariante_VO> getAniversariantes(int mes)
        {
            List<Aniversariante_VO> lstAniversariantes = new List<Aniversariante_VO>();
            //Todos os usuários
            using (SPWeb web = SPContext.Current.Site.RootWeb)
            {
                foreach (SPListItem usuario in usuarios.Items)
                {
                    try
                    {
                        // try to get user-name
                        SPUser spUser = new SPFieldUserValue(web, Convert.ToInt32(usuario.ID), null).User;
                        if (spUser == null)
                        {
                          continue;
                        }                                                                
                      //  web.Users.GetByID(usuario.ID);
                    }
                    catch (Exception)
                    {
                        continue;
                    }

                    if (usuario[COLUNA_NASCIMENTO] != null)
                    {
                        try
                        {
                            DateTime dataAniversario = DateTime.Parse(usuario[COLUNA_NASCIMENTO].ToString());
                            if (dataAniversario.Month == mes)
                            {
                                Aniversariante_VO aniv = new Aniversariante_VO();
                                aniv.DataAniversario = dataAniversario;
                                aniv.IdAniversariante = int.Parse(usuario["ID"].ToString());
                                aniv.NomeResumido = usuario["Nome Resumido"].ToString();

                                lstAniversariantes.Add(aniv);
                            }
                        }
                        catch (Exception)
                        {
                            throw new Exception("Erro fazendo parse na data de nascimento do usuário " + usuario["Nome"].ToString());
                        }
                    }
                }//fim do foreach usuário
            }//fim do using
            return lstAniversariantes;
        }

        private String montaTabelaAniversariantes()
        {
            //coloque aqui o mês que quer procurar
            int mes = DateTime.Today.Month;

            List<Aniversariante_VO> aniversariantes = getAniversariantes(mes);
            StringBuilder sb = new StringBuilder();

            //Título com o Mês corrente
            sb.Append("<div class=\"tituloMes\">");
            sb.Append(MESES[mes - 1]);
            sb.AppendLine("</div>");

            //Se não tiver nehum não faço nada!
            if (aniversariantes.Count == 0)
            {
                return "";
            }

            //Começo a criar a lista
            sb.AppendLine("<UL class=\"ul-aniversariantes\">");

            foreach (Aniversariante_VO aniv in aniversariantes)
            {

                //todo: tenho que capturar o List ID da lista                                                                                     

                String link = "<a class=\"ms-addnew\" id=\"idHomePageNewItem\" href=\"#\" onClick=\"openDialog('" + oWebsiteRoot.Url + NOME_PAGINA_MSG_ANIVERSARIANTE + "?idAniversariante=" + aniv.IdAniversariante + "')\" >";


                //String link = "<a class=\"ms-addnew\" id=\"idHomePageNewItem\" href=\"" + oWebsiteRoot.Url + NOME_PAGINA_MSG_ANIVERSARIANTE + "?idDestinatario=" + aniv.IdAniversariante + "\" >";

                //Informo a classe do aniversariante, se é do mês ou do dia
                if (aniv.DataAniversario.Date == DateTime.Now.Date)
                {
                    sb.Append("<li class=\"aniversarianteDeHoje\">");
                }
                else
                {
                    sb.Append("<li class=\"aniversariante\">");
                }
                sb.Append(link);
                sb.Append(String.Format("{0:dd/MM}", aniv.DataAniversario) + " - ");
                sb.Append(aniv.NomeResumido + " ");
                sb.Append("<img src=\"" + NOME_IMAGEM_BALAOZINHO + "\" title=\"Deixe uma mensagem\"/>");
                sb.Append("</a>");

                // sb.Append(link);

                sb.AppendLine("</li>");

                //Todo: colocar link para página de mural de recados
            }
            sb.AppendLine("</UL>");

            return sb.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    carregaConfereVariaveis();

                    Literal1.Text = montaTabelaAniversariantes();

                    //Response.Write(Literal1.Text);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
            }
        }

    }
}
