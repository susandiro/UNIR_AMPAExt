<%@ Page Language="C#" Title="Sesión caducada" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="~/UI/SesionCaducada.aspx.cs" Inherits="SesionCaducada" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="height: 100%; text-align: left;  vertical-align: central; align-content: center;">
        <div class="row">             
            <div class="col-lg-12 col-md-12" style="text-align:center">
                <h1 style="font-size: 3em;">Su sesión ha caducado</h1>
                <p>
                    <span style="color: Green; font-size: 1.7em;"><b>Pulse el botón Acceptar para volver a la ventana de acceso a la aplicación.</b></span>
                </p>                
                <p>
                    <input type="button" class="boton" id="BtnAceptar" style="width: 180px !important; float:none !important" value="Aceptar" title="Vuelve a la página de acceso a la aplicación" onclick="window.open('Login.aspx', '_self');" />
                </p>                 
            </div>
        </div>        
    </div>
</asp:Content>
