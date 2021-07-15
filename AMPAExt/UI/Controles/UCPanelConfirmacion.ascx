<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCPanelConfirmacion.ascx.cs" Inherits="AMPAExt.UI.Controles.UCPanelConfirmacion" %>

<script type="text/javascript">
    function CerrarMensajeConfirmacion() {
        $('#panelConfirmacion').hide(600);
        $('#fondoGris2').hide(0); 
           
        }
        function AbrirMensajeConfirmacion() { 
            $('#fondoGris2').show(0); 
            $('#panelConfirmacion').show(600); 
        } 
</script>
<div id="fondoGris2" runat="server" ClientIDMode="Static" class="fondoGris"></div>
<div id="panelConfirmacion" ClientIDMode="Static" runat="server" class="card panelConfirmacion">
    <div class="card-head bg-warning">
        <h2 class="card-title" style="margin-left: 10px;">
            <asp:Label ID="lblConfirmacionTitle" ClientIDMode="Static" runat="server" Text="Atención"></asp:Label>
        </h2>
    </div>
    <div class="card-body">
        <div style="overflow-y: auto; margin-bottom: 10px;">
            <asp:Label ID="lblMensajeConfirmacion" runat="server"></asp:Label>
        </div>
        <div style="text-align: left; float: left; bottom: 5px; height: 10% !important;">
            <asp:UpdatePanel ID="udp" runat="server">
                <ContentTemplate>
                    <input runat="server" id="btnAceptar" type="button" class="boton" value="Aceptar" onserverclick="BtnAceptar_Click" 
                        title="Confirma la acción" style="width: 100px; height: auto" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div style="text-align: right; float:right; bottom: 5px; height: 10% !important;">
            <input runat="server" id="buttonCancelar" type="button" class="boton" onclick="CerrarMensajeConfirmacion()" title="Cierra la ventana" value="Cancelar" style="width: 100px; height: auto" />
        </div>
    </div>
</div>