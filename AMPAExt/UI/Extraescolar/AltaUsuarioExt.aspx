<%@ Page Title="Alta de usuario AMPA" Language="C#" MasterPageFile="~/Master/SiteExtraescolar.Master" AutoEventWireup="true" CodeBehind="AltaUsuarioExt.aspx.cs" Inherits="AMPAExt.UI.Extraescolar.AltaUsuarioExt" %>
<%@ Register TagPrefix="uc" TagName="PanelInformativo" Src="~/UI/Controles/panelInformacion.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--content midd-->
    <div id="content_midd">
        <form id="form1" runat="server">
            <h1 class="tituloCentral">Nuevo usuario de empresa extraescolar</h1>
            <uc:PanelInformativo ID="PanelInfo" runat="server"></uc:PanelInformativo>
            <p class="espacioleft">Los campos marcados con <span class="errorValid">*</span> son obligatorios</p>
            <div class="espacioleft">
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtUsuario">Nombre <span style="color: red">*</span>: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtNombre" runat="server" ToolTip="Nombre del usuario" CssClass="txtObligatorio"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblNombre" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtApellido1">Primer apellido <span style="color: red">*</span>: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtApellido1" ToolTip="Primer apellido del usuario" runat="server" CssClass="txtObligatorio"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblApellido1" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtApellido2">Segundo apellido: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtApellido2" ToolTip="Segundo apellido del usuario" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblApellido2" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="cmbTipoDocumento">Tipo documento<span style="color: red">*</span>:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:DropDownList ID="cmbTipoDocumento" DataTextField="NOMBRE" DataValueField="ID_TIPO_DOCUMENTO" runat="server" ToolTip="Seleccione el tipo de documento del usuario" CssClass="txtObligatorio" />
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblTipoDocumento" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-3">
                        <label for="txtNumDocumento">Número de documento<span style="color: red">*</span>:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtNumDocumento" ToolTip="Número de documento del usuario" runat="server" CssClass="txtObligatorio"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblNumDocumento" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtTelefono">Teléfono: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtTelefono" TextMode="Phone" ToolTip="Teléfono del usuario" runat="server"></asp:TextBox>
                    </div>
                     <div class="col-md-2">
                        <asp:Label ID="lblTelefono" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtemail">Correo electrónico<span style="color: red">*</span>:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtemail" TextMode="Email" ToolTip="Correo electrónico del usuario" runat="server" CssClass="txtObligatorio"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblEmail" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                  <div class="row">
                    <div class="col-md-3">
                        <label for="cmbEmpresa">Empresa extraescolar <span style="color: red">*</span>:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:DropDownList ID="cmbEmpresa" DataTextField="NOMBRE" DataValueField="ID_EMPRESA" runat="server" ToolTip="Seleccione la empresa" CssClass="txtObligatorio" />
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblEmpresa" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row espaciosupBoton">
                    <div class="col-md-12">
                        <asp:Button ID="btnAceptar" CssClass="boton " runat="server" ToolTip="Da de alta al nuevo usuario" OnClick="BtnAceptar_Click" Text="Aceptar" />
                    </div>
                </div>
            </div>
        </form>
        <div class="clr"></div>
    </div>
</asp:Content>