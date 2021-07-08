<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SolProductores.ascx.cs" Inherits="UI_Gestion_Controles_SolProductores" %>
<%@ Register Src="~/Controles/ucPaginacion.ascx" TagPrefix="ucPaginacion" TagName="Paginacion" %>
<%@ Register Src="~/Controles/ucProductores.ascx" TagPrefix="ucProductor" TagName="Productor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:UpdatePanel ID="GestionProd" runat="server" style="height: 100%; width: 100%; position: relative; overflow: hidden;" UpdateMode="Always">
    <ContentTemplate>
        <div id="divContenedor" style="height: 100%;">
            <div style="height: 91%; max-height: 91%;">
                <div id="divTituloFiltro" class="titleForm" style="width: 100%; overflow: hidden;">
                    <div>
                        <img id="img2" runat="server" src="~/Content/Imagenes/bulletOptionsMenu.gif" alt="Filtros" />
                        <asp:Label ID="labelFiltro" runat="server" Text="Filtros" />
                    </div>
                    <div style="float: right; vertical-align: middle;">
                        <asp:ImageButton ID="buttonExpander" runat="server" OnClick="Colapsar" AlternateText="Mostrar/Ocultar" ToolTip="Mostrar/Ocultar" ImageUrl="~/Content/Imagenes/expand.jpg" ImageAlign="Right" />
                        <asp:ImageButton ID="imgCirculo" CssClass="imageButtonClass" runat="server" AlternateText="" ImageUrl="~/Content/Imagenes/gray_circle.JPG"
                            Width="15" Height="15" ImageAlign="Left" Style="cursor: default" Enabled="false" />
                    </div>
                </div>
                <div class="cajafichadatos  hide" id="dvFiltro" runat="server">
                    <asp:Panel runat="server" DefaultButton="btnFiltrar">
                        <div class="row">
                            <div class="col-md-1">
                                <label for="<%= tbIdSol.ClientID %>" style="font-size: 100%">
                                    <abbr title="Identificador de solicitud">Id. sol.</abbr>:</label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="tbIdSol" runat="server" Width="100%" TabIndex="1"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="Ftb1" runat="server" TargetControlID="tbIdSol" FilterType="Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                            </div>
                            <div class="col-md-1">
                                <label for="<%= tbSolicitante.ClientID %>" style="font-size: 100%">Solicitante:</label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="tbSolicitante" runat="server" Width="100%" TabIndex="2"></asp:TextBox>
                            </div>
                            <div class="col-md-1">
                                <label for="<%= ddOrigen.ClientID %>" style="font-size: 100%">Origen:</label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList DataTextField="ACTOR" DataValueField="ID_ACTOR" ID="ddOrigen" runat="server" Width="100%" TabIndex="3"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-1">
                                <label for="<%= ddTipoDoc.ClientID %>" style="font-size: 100%">
                                    <abbr title="Tipo de documento del productor">Tipo&nbsp;doc.</abbr>:</label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList DataTextField="TIPO_DOCUMENTO" DataValueField="TIPO_DOCUMENTO" ID="ddTipoDoc" runat="server" Width="100%" TabIndex="4"></asp:DropDownList>
                            </div>
                            <div class="col-md-1">
                                <label for="<%= tbDocumento.ClientID %>" style="font-size: 100%">Documento:</label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="tbDocumento" runat="server" Width="100%" TabIndex="5"></asp:TextBox>
                            </div>
                            <div class="col-md-1">
                                <label for="<%= ctrProductor.ClientID %>" style="font-size: 100%">Productor:</label>
                            </div>
                            <div class="col-md-3">
                                <ucProductor:Productor ID="ctrProductor" Ancho="100%" runat="server" TabIndexTexto="6" TabIndexIcono="7" Modo="Editable" Estado="Activo" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-1">
                                <label for="<%= ddTipoSolicitud.ClientID %>" style="font-size: 100%">Tipo&nbsp;solicitud:</label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddTipoSolicitud" DataTextField="TIPO_SOLICITUD" DataValueField="ID_TIPO_SOLICITUD" runat="server" Width="100%" TabIndex="8"></asp:DropDownList>
                            </div>
                            <div class="col-md-1">
                                <label for="<%= tbFechaDesde.ClientID %>" style="font-size: 100%">
                                    <abbr title="Fecha de solicitud desde">Desde</abbr>:</label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="tbFechaDesde" runat="server" TabIndex="9" Width="100%" CssClass="calendar"></asp:TextBox>
                            </div>
                            <div class="col-md-1"></div>
                            <div class="col-md-1">
                                <label for="<%= tbFechaHasta.ClientID %>" style="font-size: 100%">
                                    <abbr title="Fecha de solicitud hasta">Hasta</abbr>:</label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="tbFechaHasta" runat="server" TabIndex="10" Width="100%" CssClass="calendar"></asp:TextBox>
                            </div>
                            <div class="col-md-1"></div>
                        </div>
                        <div class="row filtro">
                            <div class="col-md-2">
                                <asp:Button ID="btnFiltrar" runat="server" CssClass="botbuscar" TabIndex="11" Style="float: left !important" Text="Filtrar" ToolTip="Filtra por los valores seleccionados" OnClick="btnFiltrar_Click" />
                            </div>
                            <div class="col-md-1">
                                <asp:Button ID="btnLimpiar" runat="server" CssClass="botbuscar" TabIndex="12" Style="float: left !important" Text="Limpiar" ToolTip="Deja los campos de filtro en su estado inicial" OnClick="btnLimpiar_Click" />
                            </div>
                            <div class="col-md-9">
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div id="divTituloGrid" class="titleForm" style="width: 100%; overflow: hidden;">
                    <div>
                        <img id="seccion2" runat="server" src="~/Content/Imagenes/bulletOptionsMenu.gif" alt="Solicitudes de productores" />
                        <span>Solicitudes de productores</span>
                    </div>
                </div>
                <div class="formulario gridSolicitudes" id="dvGridProd" style="background-color: white;" runat="server">
                    <asp:GridView TabIndex="13" ID="gvSolicitudesProd" runat="server" AllowSorting="true" AutoGenerateColumns="false" CssClass="gred" Style="width: 100%" BorderStyle="Outset" BorderWidth="1"
                        OnSorting="gvSolicitudesProd_Sorting" OnRowCreated="gvSolicitudesProd_RowCreated" OnRowCommand="gvSolicitudesProd_RowCommand" EmptyDataRowStyle-VerticalAlign="Middle"
                        EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles." AccessKey="G">
                        <Columns>
                            <asp:TemplateField HeaderText=" " ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="imgDetalleProd" runat="server" CommandArgument='<%# Eval("ID_SOLICITUD") %>' CommandName="Detalle" CausesValidation="false" ToolTip="Acceder a la solicitud" BorderStyle="None">
                            <span class="glyphicon glyphicon-pencil naranja"></span>
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Wrap="false" Height="26px" Width="25px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="FECHA_SOLICITUD" HeaderText="Fecha sol." ShowHeader="true" AccessibleHeaderText="Fecha de creación de la solicitud" ItemStyle-Wrap="false" ItemStyle-Width="83" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" SortExpression="FECHA_SOLICITUD" DataFormatString="{0:d}"></asp:BoundField>
                            <asp:BoundField DataField="ID_SOLICITUD" HeaderText="Id. sol." ShowHeader="true" AccessibleHeaderText="Identificador de la solicitud" ItemStyle-Wrap="false" ItemStyle-Width="50" ItemStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left" SortExpression="ID_SOLICITUD"></asp:BoundField>
                            <asp:BoundField DataField="RAE_TIPOS_SOLICITUD.TIPO_SOLICITUD" HeaderText="Tipo solicitud" SortExpression="RAE_TIPOS_SOLICITUD.TIPO_SOLICITUD">
                                <HeaderStyle Wrap="False" HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="80" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Documento" ShowHeader="true" AccessibleHeaderText="Documento"
                                SortExpression="TIPO_DOCUMENTO,NUMERO_DOCUMENTO" ControlStyle-Width="110" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="110" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCortadoDoc" runat="server" Text='<%# string.Concat(Eval("TIPO_DOCUMENTO"), ": ", Eval("NUMERO_DOCUMENTO")) %>' ToolTip='<%# string.Concat(Eval("TIPO_DOCUMENTO"), ": ", Eval("NUMERO_DOCUMENTO")) %>'
                                        CssClass="acortaLabel documento" Width="110">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre" ShowHeader="true" AccessibleHeaderText="Nombre"
                                SortExpression="NOMBRE" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="250" />
                                <ItemTemplate>
                                    <asp:Label ID="lbNombre" runat="server" Text='<%# Eval("NOMBRE") %>' ToolTip='<%# Eval("NOMBRE") %>'
                                        CssClass="acortaLabel nombre" Width="250">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Solicitante" ShowHeader="true" AccessibleHeaderText="Solicitante"
                                SortExpression="NOMBRE_SOLI" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="150" />
                                <ItemTemplate>
                                    <asp:Label ID="lbNombreSol" runat="server" Text='<%# Eval("NOMBRE_SOLI") %>' ToolTip='<%# Eval("NOMBRE_SOLI") %>'
                                        CssClass="acortaLabel solicitante" Width="150">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Origen" ShowHeader="true" AccessibleHeaderText="Origen"
                                SortExpression="RAE_ACTORES1.ACTOR" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="75" />
                                <ItemTemplate>
                                    <asp:Label ID="lbActor" runat="server" Text='<%# (RAEE.Comun.TipoDatos.Actores)Enum.Parse(typeof(RAEE.Comun.TipoDatos.Actores), Eval( "ID_ACTOR_ALTA").ToString()) %>'
                                        ToolTip='<%# (RAEE.Comun.TipoDatos.Actores)Enum.Parse(typeof(RAEE.Comun.TipoDatos.Actores), Eval( "ID_ACTOR_ALTA").ToString()) %>' Width="75">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>                    
                </div>
            </div>
            <div style="height: 52px; margin: 0px; padding: 0px;">
                <ucPaginacion:Paginacion runat="server" ID="gridPaginacion"
                    OnRecargarGrid="gridPaginacion_RecargarGrid"></ucPaginacion:Paginacion>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
