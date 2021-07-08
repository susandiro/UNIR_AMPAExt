<%@ Page Language="C#" Title="Acceso" MasterPageFile="~/Master/SiteInicial.Master" EnableViewStateMac="false" AutoEventWireup="true" CodeBehind="~/UI/Login.aspx.cs" Inherits="Login" %>
<%@ Register TagPrefix="uc" TagName="PanelInformativo" Src="~/UI/Controles/panelInformacion.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content_wrapper">
        <!--content laft end-->
        <div id="content_midd" class="content_login">
            <!--content midd-->
            <form id="form1" runat="server">
                    <h1>Acceso a la aplicación</h1>
                    <div class="espacioleft">
                    <uc:PanelInformativo ID="PanelInfo" runat="server"></uc:PanelInformativo>
                     <div class="row">
                        <div class="col-md-4">
                            <label for="rdAcceso">Tipo acceso <span style="color: red">*</span>: </label>
                        </div>
                        <div class="col-md-2">
                            <asp:RadioButton ID="rdAccesoAMPA" runat="server" CssClass="rButtonNombre" ToolTip="Tipo de acceso (AMPA/Extraescolar)" Text="AMPA" Checked="true" GroupName="rdAcceso"></asp:RadioButton>
                         </div>
                        <div class="col-md-6">
                            <asp:RadioButton ID="rdAccesoExtr" runat="server" CssClass="rButtonNombre" ToolTip="Tipo de acceso (AMPA/Extraescolar)" Text="Extraescolar" GroupName="rdAcceso"></asp:RadioButton>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label for="txtUsuario">Usuario <span style="color: red">*</span>: </label>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtUsuario" runat="server" ToolTip="Login de acceso" CssClass="txtObligatorio"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label for="txtContrasena">Contraseña <span style="color: red">*</span>: </label>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtContrasena" TextMode="Password" AutoCompleteType="Disabled" ToolTip="Contraseña de acceso" runat="server" CssClass="txtObligatorio"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row espaciosup">
                        <div class="col-md-4"></div>
                        <div class="col-md-8">
                            <asp:Button ID="btnLogin" CssClass="boton" runat="server" ToolTip="Acceder a la aplicación" Text="Acceder" OnClick="btnLogin_Click" />
                        </div>
                    </div>
                </div>
            </form>
            <div class="clr"></div>
        </div>
    </div>
</asp:Content>
