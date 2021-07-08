<%@ Page Title="Alta de empresa extraescolar" Language="C#" MasterPageFile="~/Master/SiteExtraescolar.Master" AutoEventWireup="true" CodeBehind="AltaEmpresaExt.aspx.cs" Inherits="AMPAExt.UI.Extraescolar.AltaEmpresaExt" %>
<%@ Register TagPrefix="uc" TagName="PanelInformativo" Src="~/UI/Controles/panelInformacion.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--content midd-->
    <div id="content_midd">
        <form id="form1" runat="server">
            <h1 class="tituloCentral">Nueva empresa extraescolar</h1>
            <uc:PanelInformativo ID="PanelInfo" runat="server"></uc:PanelInformativo>
            <p class="espacioleft" id="pObligatorios" runat="server">Los campos marcados con <span class="errorValid">*</span> son obligatorios</p>
            <div class="espacioleft">
                 <h1 style="border-bottom:dotted; border-width:1px; width:80%; border-color:#316074">Datos de la empresa extraescolar</h1>
               <div class="row">
                    <div class="col-md-3">
                        <label for="txtNumDocumento">NIF <span style="color: red">*</span>:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtNumDocumento" ToolTip="Número de documento de la empresa extraescolar" runat="server" CssClass="txtObligatorio"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblNumDocumento" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtNombre">Nombre <span style="color: red">*</span>: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtNombre" ToolTip="Nombre de la empresa extraescolar" runat="server" CssClass="txtObligatorio"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblNombre" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtRazonSocial">Razón social <span style="color: red">*</span>:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtRazonSocial" ToolTip="Razón social de la empresa extraescolar" runat="server" CssClass="txtObligatorio"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblRazonSocial" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtTelefono">Teléfono <span style="color: red">*</span>:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtTelefono" runat="server" ToolTip="Teléfono de contacto de la empresa extraescolar" CssClass="txtObligatorio" />
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblTelefono" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtWeb">Página web :</label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtWeb" ToolTip="Página web de la empresa extraescolar" runat="server" ></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblWeb" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                <h1 style="border-bottom:dotted; border-width:1px; width:80%; border-color:#316074; padding-top:1.5em"> Datos del usuario</h1>
                 <div class="row">
                    <div class="col-md-3">
                        <label for="txtUNombre">Nombre <span style="color: red">*</span>: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtUNombre" runat="server" ToolTip="Nombre del usuario" CssClass="txtObligatorio"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblUNombre" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtUApellido1">Primer apellido <span style="color: red">*</span>: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtUApellido1" ToolTip="Primer apellido del usuario" runat="server" CssClass="txtObligatorio"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblUApellido1" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtUApellido2">Segundo apellido: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtUApellido2" ToolTip="Segundo apellido del usuario" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblUApellido2" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="cmbUTipoDocumento">Tipo documento <span style="color: red">*</span>:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:DropDownList ID="cmbUTipoDocumento" DataTextField="NOMBRE" DataValueField="ID_TIPO_DOCUMENTO" runat="server" ToolTip="Seleccione el tipo de documento del usuario" CssClass="txtObligatorio" />
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblUTipoDocumento" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-3">
                        <label for="txtUNumDocumento">Número de documento <span style="color: red">*</span>:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtUNumDocumento" ToolTip="Número de documento del usuario" runat="server" CssClass="txtObligatorio"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblUNumDocumento" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtUTelefono">Teléfono: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtUTelefono" TextMode="Phone" ToolTip="Teléfono del usuario" runat="server"></asp:TextBox>
                    </div>
                     <div class="col-md-2">
                        <asp:Label ID="lblUTelefono" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtUemail">Correo electrónico <span runat="server" style="color: red">*</span>:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtUemail" TextMode="Email" ToolTip="Correo electrónico del usuario" runat="server" CssClass="txtObligatorio"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblUemail" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row espaciosupBoton">
                    <div class="col-md-12">
                        <asp:Button ID="btnAceptar" CssClass="boton " runat="server" ToolTip="Da de alta a la empresa extraescolar" OnClick="BtnAceptar_Click" Text="Aceptar" />
                    </div>
                </div>
            </div>
        </form>
        <div class="clr"></div>
    </div>
</asp:Content>
