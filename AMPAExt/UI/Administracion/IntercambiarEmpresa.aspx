<%@ Page Title="Alta de usuario AMPA" Language="C#" MasterPageFile="~/Master/SiteAdministracion.Master" AutoEventWireup="true" CodeBehind="IntercambiarEmpresa.aspx.cs" Inherits="AMPAExt.UI.Administracion.IntercambiarEmpresa" %>
<%@ Register TagPrefix="uc" TagName="PanelInformativo" Src="~/UI/Controles/panelInformacion.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--content midd-->
    <div id="content_midd">
        <form id="form1" runat="server">
            <h1 class="tituloCentral">Intercambiar alumnos de actividad extraescolar</h1>
            <uc:PanelInformativo ID="PanelInfo" runat="server"></uc:PanelInformativo>
            <p class="espacioleft">Los campos marcados con <span class="errorValid">*</span> son obligatorios</p>
            <div class="espacioleft">
                 <div class="row">
                    <div class="col-md-12">
                        <h1 style="border-bottom:dotted; border-width:1px; width:80%; border-color:#316074">Origen</h1>
                    </div>
                     </div>
                 <div class="row">
                    <div class="col-md-1">
                        <label for="cmbEmpresaOrigen">Empresa <span style="color: red">*</span>:</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList ID="cmbEmpresaOrigen" runat="server" AutoPostBack="true" ToolTip="Seleccione la empresa de origen" OnSelectedIndexChanged="cmbEmpresaOrigen_SelectedIndexChanged" CssClass="txtObligatorio" />
                    </div>
                    <div class="col-md-2">
                        <label for="cmbActividadOri">Actividad extraescolar <span style="color: red">*</span>:</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="cmbActividadOri" Enabled="false" AutoPostBack="true"  DataTextField="NOMBRE" DataValueField="ID_ACTIVIDAD" runat="server" ToolTip="Seleccione la actividad extraescolar de origen" OnSelectedIndexChanged="cmbActividadOri_SelectedIndexChanged" CssClass="txtObligatorio" />
                    </div>
                     <div class="col-md-1">
                        <label for="cmbHorarioOri">Horario <span style="color: red">*</span>:</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="cmbHorarioOri" Enabled="false" runat="server" ToolTip="Seleccione el horario de origen" CssClass="txtObligatorio" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-1"></div>
                    <div class="col-md-2"> <asp:Label ID="lblEmpresaOri" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label></div>
                    <div class="col-md-2"></div>
                    <div class="col-md-3"><asp:Label ID="lblActividadOri" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label></div>
                    <div class="col-md-1"></div>
                    <div class="col-md-3"><asp:Label ID="lblHorarioOri" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label></div>
                </div>
                 <div class="row">
                     <div class="col-md-4"></div>
                    <div class="col-md-3">
                        <asp:Image ImageUrl="~/Content/Imagenes/flecha.gif" CssClass="flecha" runat="server" AlternateText="De origen a destino" />
                        </div>
                     <div class="col-md-5"></div>
                     </div>
                 <div class="row">
                    <div class="col-md-12">
                        <h1 style="border-bottom:dotted; border-width:1px; width:80%; border-color:#316074;"> Destino</h1>
                    </div>
                     </div>
               <div class="row">
                     <div class="col-md-1">
                        <label for="cmbEmpresaDest">Empresa <span style="color: red">*</span>:</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList ID="cmbEmpresaDest" AutoPostBack="true" runat="server" OnSelectedIndexChanged="cmbEmpresaDest_SelectedIndexChanged" ToolTip="Seleccione la empresa de destino" CssClass="txtObligatorio" />
                    </div>
                    <div class="col-md-2">
                        <label for="cmbActividadDes">Actividad extraescolar <span style="color: red">*</span>:</label>
                    </div>
                    <div class="col-md-3"> 
                        <asp:DropDownList ID="cmbActividadDes" Enabled="false" AutoPostBack="true" DataTextField="NOMBRE" DataValueField="ID_ACTIVIDAD" runat="server" ToolTip="Seleccione la actividad extraescolar de destino" OnSelectedIndexChanged="cmbActividadDes_SelectedIndexChanged" CssClass="txtObligatorio" />
                    </div>
                   <div class="col-md-1">
                        <label for="cmbHorarioDes">Horario <span style="color: red">*</span>:</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="cmbHorarioDes" Enabled="false" runat="server" ToolTip="Seleccione el horario de destino" CssClass="txtObligatorio" />
                    </div>
                </div>
                <div class="row">
                 <div class="col-md-1"></div>
                    <div class="col-md-2"> <asp:Label ID="lblEmpresaDest" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label></div>
                    <div class="col-md-2"></div>
                    <div class="col-md-3"><asp:Label ID="lblActividadDes" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label></div>
                    <div class="col-md-1"></div>
                    <div class="col-md-3"><asp:Label ID="lblHorarioDes" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label></div>
                    </div>
                <div class="row espaciosupBoton">
                    <div class="col-md-12">
                        <asp:Button ID="BtnIntercambiar" CssClass="boton " runat="server" ToolTip="Intercambia los alumnos de la actividad de origen a la de destino" OnClientClick="if(!confirm('Se va a realizar el intercambio de actividades extraescolares entre las empresas, ¿desea continuar?')){return false;}" OnClick="BtnIntercambiar_Click" Text="Intercambiar actividad" />
                    </div>
                </div>
            </div>
        </form>
        <div class="clr"></div>
    </div>
</asp:Content>