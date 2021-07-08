<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="panelInformacion.ascx.cs" Inherits="AMPAExt.UI.Controles.panelInformacion" %>

<div class="row">
    <div class="col-lg">
        <div class="alert alert-danger alert-dismissible" role="alert" runat="server" id="alertError" visible="false">
            <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            <span class="glyphicon glyphicon-remove-sign" aria-hidden="true">&nbsp;</span><asp:Label ID="lbError" runat="server" />
        </div>
        <div class="alert alert-warning" role="alert" runat="server" id="alertWarning" visible="false">
            <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true">&nbsp;</span><asp:Label ID="lbWarning" runat="server" />
        </div>
        <div class="alert alert-success alert-dismissible" role="alert" runat="server" id="alertSuccess" visible="false">
            <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            <span class="glyphicon glyphicon-ok-sign" aria-hidden="true">&nbsp;</span><asp:Label ID="lbSuccess" runat="server" Text="<strong>Correcto</strong>" />
        </div>
    </div>
</div>
