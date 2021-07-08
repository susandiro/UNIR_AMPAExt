<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DividendoDigital.master" AutoEventWireup="true" CodeFile="GestionUsuarios.aspx.cs" Inherits="Conexion_GestionUsuarios" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxControlToolkit" %>
<asp:Content ID="Head" ContentPlaceHolderID="dividendoHead" runat="Server">
    <base id="hola" target="_self" />
</asp:Content>
<asp:Content ClientIDMode="Static" ID="Body" ContentPlaceHolderID="dividendoBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager" runat="server" EnablePartialRendering="true"
        EnableScriptGlobalization="true" EnableScriptLocalization="true" EnablePageMethods="true"
        AsyncPostBackTimeout="300000">
    </asp:ScriptManager>
    <asp:HiddenField runat="server" ID="hdTamanio" />
    <link href="../Content/SedeElectronica.css" rel="stylesheet" />
    <link href="../Content/custom.css" rel="stylesheet" />
    <script src="../Scripts/basic.js"></script>
    <script type="text/javascript">
         $(document).on('change', 'select', function () {
            $(this).attr('title', $(this).find('option:selected').text());
        });

        function pageLoad() {
            $(":text:contains('readonly')").each(function (i) {
                this.title = this.value;
            });    
            $("select option").attr("title", "");
            $("select option").each(function (i) {
                this.title = this.text;
            });
            $('select').each(function (index) {
                $(this).attr('title', $(this).find('option:selected').text());
            });
            hideLoading();
        }
    </script>
    <div id="main_container">
        <div class="center_content">
            <div class="center_completo">
                <h1 id="DvTitulo"><span id="LblTitulo">LISTADO DE USUARIOS</span></h1>
                <asp:Panel ID="pnlFiltro" runat="server" DefaultButton="btnBuscar">
                    <div class="DvHead">Filtros</div>
                    <div class="DvContent">
                        <div class="tbl">
                            <div class="fila">
                                <div style="display: table-cell; text-align: left;padding-right: 5px">
                                    Id. Usuario:
                                </div>
                                <div style="display: table-cell; padding-right:20px;">
                                    <asp:TextBox runat="server" ID="txtIdUsuario" Width="50px"></asp:TextBox>
                                    <AjaxControlToolkit:FilteredTextBoxExtender ID="FilterIdUsuario" runat="server" TargetControlID="txtIdUsuario" FilterType="Numbers" />
                                </div>
                                <div style="display: table-cell; text-align: right; padding-right: 5px">
                                    Login:
                                </div>
                                <div style="display: table-cell;padding-right:20px;">
                                    <asp:TextBox runat="server" ID="txtLogin" Width="90%"></asp:TextBox>
                                </div>
                                <div style="display: table-cell; text-align: right; padding-right: 5px">
                                    Tipo usuario:
                                </div>
                                <div style="display: table-cell; padding-right:20px;width:130px;">
                                    <asp:DropDownList ID="cmbGrupoUsuario" DataTextField="DESCRIPCION" DataValueField="CODIGO" runat="server" Width="90%" />
                                </div>
                                  <div style="display: table-cell;text-align: right; padding-right: 5px">
                                    Ver:
                                </div>
                                <div style="display: table-cell; padding-right:20px; width:110px;">
                                   <asp:DropDownList ID="cmbVer" runat="server" Width="90%" />
                                </div>
                                <div style="display: table-cell; text-align: right; padding-right:5px; ">
                                    <asp:Button runat="server" ID="btnBuscar" Text="Buscar" OnClick="btnBuscar_Click" />
                                </div>
                                 <div style="display: table-cell; text-align: right; ">
                                    <asp:Button runat="server" ID="btnLimpiar" Text="Limpiar" OnClick="limpiar_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <div style="padding-bottom: 20px; height:25px;font-weight:bold; font-size:1.1em;">
                    <asp:Button runat="server" ID="btnNuevo" Text="Nuevo usuario" OnClick="btnNuevo_Click" />
                </div>
                <div class="DvHead">Resultado de la Búsqueda</div>
                <div class="DvContent">
                    <div class="tbl">
                        <asp:Panel runat="server" ID="PanelGrid">
                                <asp:GridView ID="gvResultados" runat="server" AutoGenerateColumns="false" Width="100%" DataKeyNames="IdUsuario,Certificado" CssClass="Grid"
                                    EmptyDataRowStyle-VerticalAlign="Middle" EmptyDataRowStyle-HorizontalAlign="Center" Font-Size="15px"
                                    EmptyDataText="No hay resultados de usuarios." EmptyDataRowStyle-Font-Bold="true" OnRowDataBound="gvResultados_RowDataBound"
                                    OnRowCommand="gvResultados_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="Certificado" Visible="false" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgDetalle" runat="server" CausesValidation="false" CommandName="Detalle"
                                                    CommandArgument='<%# Eval("IdUsuario") %>' ImageUrl="~/Images/icoDetalle.gif"
                                                    ToolTip="Detalle/Modificación del usuario" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" Wrap="False" Height="35px" Width="35px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgBorrar" runat="server" CausesValidation="false" CommandName="Eliminar"
                                                    CommandArgument='<%# Eval("IdUsuario") %>' ImageUrl="~/Images/icono_borrar.gif"
                                                    ToolTip="Eliminar usuario" OnClientClick="if(!confirm('Se va a proceder a eliminar al usuario seleccionado, ¿desea continuar?')){return false;}" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Wrap="False" Height="35px" Width="35px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="IdUsuario" HeaderText="Id Usuario" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Login" HeaderText="Login usuario">
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="GrupoUsuario" HeaderText="Tipo usuario"></asp:BoundField>
                                        <asp:BoundField DataField="Perfiles" HeaderText="Perfil"></asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Button ID="BtnRefrescar" runat="server" Text="Button" Style="display: none;"
        OnClick="refrescar" />
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlPopUp" Style="background-color: White; display: none; align-items: center; align-content: center;">
                <asp:Panel runat="server" ID="pnlDrag" Style="cursor: move; background-color: Blue; border: solid 1px Gray; color: White; font-weight: bolder; padding: 5px;">
                    <div style="float: left;">
                        <asp:Label runat="server" ID="txtTituloPopUp" />
                    </div>
                    <div style="clear: both;">
                    </div>
                </asp:Panel>
                <iframe runat="server" id="framePopUp" width="935" height="900" class="clsFrame"></iframe>
            </asp:Panel>
            <AjaxControlToolkit:ModalPopupExtender runat="server" ID="mpGestionar" TargetControlID="pnlPopUp"
                PopupControlID="pnlPopUp" BackgroundCssClass="modalBackground" Drag="true" OnCancelScript="closePopUp()">
            </AjaxControlToolkit:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
     <div id='loadingmsg' class="updateProgress" style='margin: auto auto auto auto;'>
        <div class="modalCarga" style="position: relative; top: 0;">
            <div class="tabla" id="tablamodalcarga" runat="server" visible="true">
                <div class="fila">
                    <div class="columnaReloj" style="padding-top: 15px; padding-left: 5px; vertical-align: middle">
                        <asp:Image Style="vertical-align: middle;" ID="Image2" runat="server" AlternateText="Espere, por favor..."
                            ImageUrl="~/images/cargando.gif" />
                    </div>
                    <div class="columnaReloj" style="font-size:95%; padding-top: 15px; padding-left: 5px; vertical-align: middle">
                        Espere, por favor...
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id='loadingover' class="modalBackground">
    </div>
</asp:Content>--%>