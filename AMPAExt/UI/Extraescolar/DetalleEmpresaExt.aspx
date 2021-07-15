<%@ Page Title="Alta de empresa extraescolar" Language="C#" MasterPageFile="~/Master/SiteExtraescolar.Master" AutoEventWireup="true" CodeBehind="DetalleEmpresaExt.aspx.cs" Inherits="AMPAExt.UI.Extraescolar.DetalleEmpresaExt" %>
<%@ Register TagPrefix="uc" TagName="PanelInformativo" Src="~/UI/Controles/panelInformacion.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--content midd-->
    <div id="content_midd">
        <form id="form1" runat="server">
            <h1 class="tituloCentral">Consulta de empresa extraescolar</h1>
            <uc:PanelInformativo ID="PanelInfo" runat="server"></uc:PanelInformativo>
            <div class="espacioleft">
               <div class="row">
                    <div class="col-md-3">
                        <label for="txtNumDocumento">NIF:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtNumDocumento" ToolTip="Número de documento de la empresa extraescolar" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtNombre">Nombre: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtNombre" ToolTip="Nombre de la empresa extraescolar" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtRazonSocial">Razón social:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtRazonSocial" ToolTip="Razón social de la empresa extraescolar" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtTelefono">Teléfono:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtTelefono" runat="server" MaxLength="15" ToolTip="Teléfono de contacto de la empresa extraescolar" ReadOnly="true" />
                    </div>
                    <div class="col-md-2"></div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtWeb">Página web :</label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtWeb" ToolTip="Página web de la empresa extraescolar" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                <div class="row espaciosupBoton">
                     <div class="col-md-12">
                        <asp:Button ID="BtnVolver" CssClass="boton " runat="server" ToolTip="Vuelve a la página anterior" OnClick="BtnVolver_Click" Text="Volver" />
                    </div>
                </div>
            </div>
        </form>
        <div class="clr"></div>
    </div>
</asp:Content>
