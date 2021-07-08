<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="AMPAExt.UI.Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div id="content_wrapper">
       <div id="content_midd" class="index" style="text-align:center">
            <form id="form1" runat="server">
            <h1><b>ERROR DE APLICACIÓN</b></h1>
            <h2>
                Por favor, inténtelo más tarde y si el problema persiste, póngase en contacto con el administrador del sistema.
            </h2>
            <p style="text-align: center;">Pulse el botón Actualizar volver al sistema o Salir para salir de la aplicación.</p>
            <br />
            <br />
            <div style="height: 100%;" id="dvError" runat="server" visible="false">
                <fieldset id="fldDetalleError">
                    <legend>Detalle del error</legend>
                    <p style="max-height: 250px; overflow: auto">
                        <asp:Literal ID="mensaje" runat="server"></asp:Literal>
                    </p>
                </fieldset>
            </div>
            <div class="row">
               <div class="col-md-5"></div>
               <div class="col-md-2" style="width: 33.33%;display: flex;justify-content: center"><asp:Button id="Btnvolver" UseSubmitBehavior="false" Text="Volver" runat="server" CssClass="boton" title="Volver a la página inicial" OnClick="Btnvolver_Click" /></div>
               <div class="col-md-5"></div>
            </div> 
         </form>       
       </div>
     </div>
</asp:Content>
