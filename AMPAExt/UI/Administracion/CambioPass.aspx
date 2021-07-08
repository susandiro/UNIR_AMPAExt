<%@ Page Title="RII_AEE - Cambio contraseña" Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/Principal.master" CodeFile="CambioPass.aspx.cs" Inherits="UI_Administracion_CambioPass" ValidateRequest="false" %>

<%@ Register Assembly="GUAI" Namespace="GUAI.UI.Controles" TagPrefix="guai" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upUsuario" runat="server">
        <ContentTemplate>
            <script type="text/javascript" language="javascript">
                function validaDatos(grupo, mensaje) {
                    if (typeof (Page_ClientValidate) == 'function')
                        Page_ClientValidate(grupo);

                    if (Page_IsValid)
                        return confirm(mensaje);
                }
            </script>
            <h1>Cambio de contraseña</h1>
            <div class="formulario" style="overflow: auto;">
                <div id="form">
                    <div class="alert alert-danger alert-dismissible" role="alert" runat="server" id="alertError" visible="false">
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <span class="glyphicon glyphicon-remove-sign" aria-hidden="true">&nbsp;</span><asp:Label ID="lbError" runat="server" />
                    </div>
                    <div class="alert alert-warning" role="alert" clientidmode="Static" runat="server" id="alertWarning" visible="false">
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true">&nbsp;</span><asp:Label ID="lbWarning" runat="server" />
                    </div>
                    <div class="alert alert-success alert-dismissible" role="alert" runat="server" id="alertSuccess" visible="false">
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <span class="glyphicon glyphicon-ok-sign" aria-hidden="true">&nbsp;</span><asp:Label ID="lbSuccess" runat="server" Text="<strong>Correcto</strong>" />
                    </div>
                   <p class="espacioleft">Los campos marcados con <span class="errorValid">*</span> son obligatorios</p>
                    <div class="row">
                        <div class="col-md-2">
                            <label for="<%= tbAntigua.ClientID %>" style="font-size: 100%">Contraseña antigua <span class="error">*</span>:</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox TextMode="Password" ID="tbAntigua" runat="server" AccessKey="a" TabIndex="1" Width="100%" CssClass="Obligatorio" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RVFAntigua" runat="server" Text="* Requerido"
                                ErrorMessage="Nombre" CssClass="error" ControlToValidate="tbAntigua" ValidationGroup="RFUsuario" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <label for="<%= tbNueva.ClientID %>" style="font-size: 100%">Contraseña nueva <span class="error">*</span>:</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox TextMode="Password" ID="tbNueva" runat="server" AccessKey="n" TabIndex="2" Width="100%" CssClass="Obligatorio" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:RequiredFieldValidator ID="RFVNueva" runat="server" ErrorMessage="Login" Text="* Requerido" CssClass="error" SetFocusOnError="True" ControlToValidate="tbNueva" ValidationGroup="RFUsuario" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <label for="<%= tbRepetir.ClientID %>" style="font-size: 100%">Repetir contraseña <span class="error">*</span>:</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox TextMode="Password" ID="tbRepetir" runat="server" AccessKey="r" TabIndex="3" Width="100%" CssClass="Obligatorio" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:RequiredFieldValidator ID="RFVRepetir" runat="server" ErrorMessage="Login" Text="* Requerido" CssClass="error" SetFocusOnError="True" ControlToValidate="tbRepetir" ValidationGroup="RFUsuario" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                   <br /><br />
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Button ID="btnCambiar" runat="server" CssClass="botonbuscar" Style="float: left !important; margin: 1px;" Text="Cambiar contraseña" TabIndex="4" CausesValidation="true" ValidationGroup="RFUsuario"  ToolTip="Realiza el cambio de contraseña para el usuario" OnClientClick="return validaDatos(RFUsuario,'Se van a guardar los datos del usuario, ¿desea continuar?');"  OnClick="btnCambiar_Click" />
                        </div>
                    </div>
                    
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>    
</asp:Content>

