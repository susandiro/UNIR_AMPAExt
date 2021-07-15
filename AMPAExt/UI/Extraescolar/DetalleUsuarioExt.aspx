<%@ Page Title="Alta de usuario AMPA" Language="C#" MasterPageFile="~/Master/SiteExtraescolar.Master" AutoEventWireup="true" CodeBehind="DetalleUsuarioExt.aspx.cs" Inherits="AMPAExt.UI.Extraescolar.DetalleUsuarioExt" %>
<%@ Register TagPrefix="uc" TagName="PanelInformativo" Src="~/UI/Controles/panelInformacion.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--content midd-->
    <div id="content_midd">
        <form id="form1" runat="server">
            <h1 class="tituloCentral">Consulta de usuario de empresa extraescolar</h1>
            <uc:PanelInformativo ID="PanelInfo" runat="server"></uc:PanelInformativo>
            <div class="espacioleft">
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtNombre">Nombre: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtNombre" runat="server" ToolTip="Nombre del usuario" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtApellido1">Primer apellido:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtApellido1" ToolTip="Primer apellido del usuario" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtApellido2">Segundo apellido: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtApellido2" ToolTip="Segundo apellido del usuario" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtTipoDocumento">Tipo documento:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtTipoDocumento" Enabled="false" ToolTip="Tipo de documento del usuario" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>

                <div class="row">
                    <div class="col-md-3">
                        <label for="txtNumDocumento">Número de documento:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtNumDocumento" Enabled="false" ToolTip="Número de documento del usuario" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtTelefono">Teléfono: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtTelefono" TextMode="Phone" MaxLength="15" ToolTip="Teléfono del usuario" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                     <div class="col-md-2"></div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtemail">Correo electrónico:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtemail" TextMode="Email" ToolTip="Correo electrónico del usuario" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                  <div class="row">
                    <div class="col-md-3">
                        <label for="cmbEmpresa">Empresa extraescolar:</label>
                    </div>
                    <div class="col-md-7">
                         <asp:TextBox ID="txtEmpresa" Enabled="false" ToolTip="Empresa extraescolar a la que pertenece el usuario" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                    </div>
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