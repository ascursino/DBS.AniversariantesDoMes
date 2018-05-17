<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MensagensAniversario.aspx.cs" Inherits="DBS.AniversariantesDoMes.Layouts.DBS.AniversariantesDoMes.Pages.MensagensAniversario" DynamicMasterPageFile="~masterurl/default.master" %>



<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">

<SharePoint:CssRegistration ID="CssRegistration1" runat="server" Name="/_layouts/DBS.AniversariantesDoMes/Aniversariantes.css"></SharePoint:CssRegistration>

</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">

    <table width="100%" border="0" cellspacing="1" cellpadding="1">
      <tr>
        <td width="5%"><img src="/_layouts/images/DBS.AniversariantesDoMes/bolo.png" width="52" height="39" id="imgBolo" alt="" /></td>
        <td width="95%"><asp:Label ID="lblNiverNome" runat="server" text="{nome Resumido}" CssClass="NomeAniversariante" />
        <asp:Label ID="lblNiverData" runat="server" text="{(dia/mês)}" CssClass="SubTexto" /></td>
      </tr>
    </table>
    <br/>
    <table width="100%" border="0" cellspacing="1" cellpadding="1">
      <tr>
        <td width="500" valign="top" bgcolor="#DDECFF">
          <table width="95%">
          <tr>
            <td colspan="2" class="Texto">Deixe sua mensagem para <asp:Label ID="lblNomeDestinatario" runat="server" text="{nome Resumido}" /></td>
          </tr>
          <tr>
            <td colspan="2">&nbsp;</td>
            </tr>
          <tr>
            <td class="Texto">Titulo:&nbsp;</td>
            <td><asp:TextBox ID="txtTitulo" runat="server" CssClass="Texto">Parabéns!</asp:TextBox></td>
          </tr>
          <tr>
            <td class="Texto">Mensagem:&nbsp;</td>
            <td><SharePoint:InputFormTextBox ID="richTxtDescription" runat="server" 
                TextMode="MultiLine" Rows="10"
                RichTextMode="FullHtml" RichText="true"></SharePoint:InputFormTextBox></td>
          </tr>
          <tr>
            <td colspan="2">&nbsp;</td>
          </tr>
          <tr>
            <td colspan="2"><asp:Button ID="btnSalvar" runat="server" OnClick="btnSalvar_Click" Text="Salvar" /></td>
          </tr>
        </table>
        </td>
        <td width="*" valign="top">
            <div id="DV_Mensagem" style="border:1px solid #CCCCCC; height:330px; overflow:auto;">
                  <table width="100%" border="0" cellspacing="1" cellpadding="1">
                      <asp:Label id="lblLinhasMensagens" runat="server" />
                      <!--
                      <tr>
                        <td><p><span class="Titulo">Felicidades!!!!</span><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <span class="SubTexto">Andréa Cursino 16/02/2012 20:00</span></p>
                          <p class="Mensagem">Parabéns, felciidade e muitos anos de vida!</p></td>
                      </tr>
                      <tr>
                        <td height="20"><hr size="1" noshade="noshade" /></td>
                      </tr>
                      -->
                  </table>
            </div>
        </td>
      </tr>
    </table>
    <p>&nbsp;</p>



<!-- area para exibir o aniversariante
<p class="NomeAniversariante">
   <img src="/_layouts/images/DBS.AniversariantesDoMes/bolo.png" id="imgBolo"/>
   <asp:Label ID="lblNomeResumidoEData_1" runat="server" text="{nome Resumido + dia/mes" ></asp:Label>
</p>
<p>&nbsp;</p>
-->

<!-- area para deixar a mensagem
<table id="tbNovaMsg">
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Deixe sua mensagem para " CssClass="coluna1"></asp:Label>        
            <asp:Label ID="lblNomeDestinatario_1" runat="server" Text="{Destinatário}:"></asp:Label>
        </td>      
    </tr>

    <tr>
        <td>
            <asp:Label ID="Label2" runat="server" Text="Título:" CssClass="coluna1"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:TextBox ID="txtTitulo_1" runat="server">Parabéns!</asp:TextBox>
        </td>
    </tr>
    <tr>    
        <td>
            <SharePoint:InputFormTextBox ID="richTxtDescription_1" runat="server" 
                TextMode="MultiLine" Rows="10"
                RichTextMode="FullHtml" RichText="true"></SharePoint:InputFormTextBox>
        </td>        
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnSalvar_1" runat="server" OnClick="btnSalvar_Click" Text="Salvar" />
        </td>
    </tr>
    
</table>
-->

<!-- area para lista das mensagens
<table class="tabMensagens" border="0" cellspacing="0">

    <tr>    
        <td colspan="2">

          
        </td>
        
    </tr>

    <tr>  
        <asp:Label id="lblLinhasMensagens_1" runat="server" />
    </tr>
    
    <tr>    
        <td colspan="2">
            
        </td>
    </tr>    

</table>

<br />
<br />
<br />
-->

</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
Application Page
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
My Application Page
</asp:Content>
