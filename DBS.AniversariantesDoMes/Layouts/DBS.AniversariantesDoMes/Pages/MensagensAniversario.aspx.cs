/* 
                          Desenvolvido por DBS IT Services
                         http://www.dbsitservices.com.br
                                     
           Copyright 2011 DBS IT Services © Todos os direitos reservados 

 Este aquivo contém código fonte de aplicativo desenvolvido pela DBS IT Services. 
 É expressamente proibida a alteração, distribuição ou venda desses arquivos sem 
 aprovação  formal da DBS IT Services e do cliente contratante desse serviço sob
 proteção da legislação vigente.
 
*/
using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Collections.Generic;
using DBS.AniversariantesDoMes.Value_Objects;
using System.Text;
using System.Linq;
using System.Web.UI.WebControls;

namespace DBS.AniversariantesDoMes.Layouts.DBS.AniversariantesDoMes.Pages
{
    public partial class MensagensAniversario : LayoutsPageBase
    {
        //Variáveis
        private int idAniversariante;
        private int ano;

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (Request.QueryString["idAniversariante"] == null)
                {
                    Response.Write("Falta o parametro 'idAniversariante'");
                    Response.End();
                }

                idAniversariante = int.Parse(Request.QueryString["idAniversariante"].ToString());

                carregaMensagens();
            }
            catch (Exception ex)
            {
                lblLinhasMensagens.Text = ex.ToString();
            }          
        }

        private void Salvar()
        {
            using (SPWeb web = SPContext.Current.Site.RootWeb)
            {
                SPList lstMural = web.Lists["Mural de Mensagens"];
                SPListItem novoItem;                
                SPUser usuarioDestino = new SPFieldUserValue(web, Convert.ToInt32(idAniversariante), null).User;
                
                novoItem = lstMural.Items.Add();
                novoItem["Title"] = txtTitulo.Text;
                novoItem["Mensagem"] = richTxtDescription.Text;
                novoItem["Destinatario"] = usuarioDestino;
                novoItem["TipoMensagem"] = "Aniversario";

                web.AllowUnsafeUpdates = true;
                novoItem.Update();
                web.AllowUnsafeUpdates = false;

                btnSalvar.Enabled = false;
           
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            Salvar();
            Response.Redirect(Request.Url.ToString(), true);
            btnSalvar.Enabled = false;
        }

        private void carregaMensagens()
        {
            using (SPWeb web = SPContext.Current.Site.RootWeb)
            {
                SPList listMsgs = web.Lists["Mural de Mensagens"];
                SPList usuarios = web.SiteUserInfoList;
                SPListItem usuario = usuarios.GetItemById(idAniversariante);

                
                lblNomeDestinatario.Text = usuario["Nome Resumido"].ToString();

                DateTime dataNascimento = DateTime.Parse(usuario["Data Nascimento"].ToString());

                lblNiverNome.Text = usuario["Nome Resumido"].ToString();
                lblNiverData.Text = "(" + dataNascimento.Date.ToString("dd/MM") + ")";

                //carrega todas as mensagens de aniversário da pessoa
                var query = new SPQuery
                {
                    Query = @"<Where>
                                <And>
                                    <Eq>
                                        <FieldRef Name=""Destinatario"" LookupId= ""TRUE""/>
                                        <Value Type=""User"">" + idAniversariante.ToString() + @"</Value>
                                    </Eq>
                                    <Eq>
                                        <FieldRef Name='TipoMensagem' />
                                        <Value Type='Choice'>Aniversario</Value>
                                    </Eq>
                                </And>
                              </Where>
                             <OrderBy>
                                <FieldRef Name='Created_x0020_Date' Ascending='False' />
                             </OrderBy>"
                };

                SPListItemCollection mensagens = listMsgs.GetItems(query);

                StringBuilder sb = new StringBuilder();
                foreach (SPListItem item in mensagens)
                {
                    //Só pego as mensagens desse ano, senão eu pulo
                    if (DateTime.Now.Year != DateTime.Parse(item["Created"].ToString()).Year)
                    {
                        continue;
                    }

                    //captura o id do autor para depois pegar o nome resumido do mesmo
                    //todo: Tá feia essa linha, pode melhorar!
                    SPListItem autor = usuarios.GetItemById(int.Parse(item["Author"].ToString().Substring(0, item["Author"].ToString().IndexOf('#')-1)));
                    
                    Mensagem_VO msg = new Mensagem_VO();
                    msg.Id = item.ID;
                    msg.Mensagem = item["Mensagem"] == null ? "(sem mensagem)" : item["Mensagem"].ToString();
                    //msg.Autor = item["Author"] == null ? "" : item["Author"].ToString();
                    msg.Autor = autor["Nome Resumido"] == null ? autor["Name"].ToString() : autor["Nome Resumido"].ToString();
                    msg.Titulo = item["Title"] == null ? "(sem título)" : item["Title"].ToString();  
                    msg.Data = DateTime.Parse(item["Created"].ToString());

                    sb.AppendLine("<tr><td>");
                    sb.AppendLine("<p><span class='Titulo'>" + msg.Titulo + "</span><br />");
                    sb.AppendLine("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    sb.AppendLine("<span class='SubTexto'>" + msg.Autor + "&nbsp;" + msg.Data + "</span></p>");
                    sb.AppendLine("<p class='Mensagem'>" + msg.Mensagem + "</p>");
                    sb.AppendLine("</td></tr>");
                    sb.AppendLine("<tr><td height='20'><hr class='ms-rteElement-Hr ms-rteFontSize-3'/></td></tr>");
                }         
                      
                lblLinhasMensagens.Text = sb.ToString();

            }
        }
    }
}
