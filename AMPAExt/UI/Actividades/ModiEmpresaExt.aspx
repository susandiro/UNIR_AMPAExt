<%@ Page Title="Alta de empresa extraescolar" Language="C#" MasterPageFile="~/Master/SiteExtraescolar.Master" AutoEventWireup="true" CodeBehind="ModiEmpresaExt.aspx.cs" Inherits="AMPAExt.UI.Extraescolar.ModiEmpresaExt" %>
<%@ Register TagPrefix="uc" TagName="PanelInformativo" Src="~/UI/Controles/panelInformacion.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--content midd-->
    <div id="content_midd">
        <form id="form1" runat="server">
            <h1 class="tituloCentral">Mantenimiento de empresa extraescolar</h1>
            <uc:PanelInformativo ID="PanelInfo" runat="server"></uc:PanelInformativo>
            <p class="espacioleft">Los campos marcados con <span class="errorValid">*</span> son obligatorios</p>
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
                <h1 style="border-bottom:dotted; border-width:1px; width:80%; border-color:#316074; padding-top:1.5em"> Actividades extraescolares </h1>
                 <div class="row">
                    <div class="col-md-3">
                        <label for="txtActNombre">Actividad <span style="color: red">*</span>: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtActNombre" runat="server" ToolTip="Nombre de la actividad extraescolar" CssClass="txtObligatorio"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblActNombre" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtActDescripcion">Descripción <span style="color: red">*</span>: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtActDescripcion" ToolTip="Descripción de la actividad" runat="server" CssClass="txtObligatorio"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblActDescripcion" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtActHorario">Horario <span style="color: red">*</span>:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtActHorario" ToolTip="Horario en el que se imparte" runat="server" CssClass="txtObligatorio"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblActHorario" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>

                 <div class="row">
                    <div class="col-md-3">
                        <label for="txtActCuota">Cuota mensual (en €) <span style="color: red">*</span>:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtActCuota" ToolTip="Cuota mensual en €" runat="server" CssClass="txtObligatorio"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblActCuota" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                  <div class="row">
                    <div class="col-md-3">
                        <label for="txtActDescuentos">Descuentos aplicables:</label>
                    </div>
                    <div class="col-md-7">
                        <textarea ID="txtActDescuentos" ToolTip="Cuota mensual en €" runat="server" CssClass="txtObligatorio"></textarea>
                    </div>
                    <div class="col-md-2"></div>
                </div>





                <div class="row">
                    <div class="col-md-3">
                        <label for="cmbUTipoDocumento"></label>
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
                        <label for="txtUNumDocumento">Número de documento<span style="color: red">*</span>:</label>
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
                        <label for="txtUemail">Correo electrónico<span style="color: red">*</span>:</label>
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
                        <asp:Button ID="btnActualizar" CssClass="boton " runat="server" ToolTip="Actualiza los datos de la empresa extraescolar" OnClick="BtnActualizar_Click" Text="Aceptar" />
                    </div>
                </div>
            </div>
        </form>
        <div class="clr"></div>
    </div>
</asp:Content>
