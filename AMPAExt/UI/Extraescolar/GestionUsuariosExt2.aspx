<%@ Page Language="C#" Title="Gestión de usuarios" MasterPageFile="~/Master/SiteExtraescolar.Master" AutoEventWireup="true" CodeBehind="GestionUsuariosExt2.aspx.cs" Inherits="AMPAExt.UI.Extraescolar.GestionUsuariosExt2" %>
<%--<%@ Register Src="~/Controles/ucPaginacion.ascx" TagPrefix="ucPaginacion" TagName="Paginacion" %>
<%@ Register Src="~/Controles/ucProductores.ascx" TagPrefix="ucProductor" TagName="Productor" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--content midd-->
    <div id="content_midd">
        <form id="form1" runat="server">
            <h1 class="tituloCentral">Gestión de usuarios de AMPA</h1>
               <div class="espacioleft">
                   <div class="row">
                            <div class="col-md-11">
                            <h2>Buscar</h2>    
                            </div>
                            <div class="col-md-1"><asp:LinkButton ID="btnExpandir"  runat="server" data-toggle="collapse" data-target="#dvFiltro" ToolTip="Mostrar/Ocultar"><img src="https://img.icons8.com/office/20/000000/collapse.png"/></asp:LinkButton></div>
                   </div>
                     <asp:Panel runat="server"  DefaultButton="btnBuscar">
                         <div class="collapse" id="dvFiltro">
                        <div class="row">
                            <div class="col-md-2">
                                <label for="<%= cmbTipoDocumento.ClientID %>">Tipo documento:</label>
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="cmbTipoDocumento" DataTextField="NOMBRE" DataValueField="ID_TIPO_DOCUMENTO" runat="server" ToolTip="Seleccione el tipo de documento del usuario" />
                            </div>
                            <div class="col-md-2">
                                <label for="<%= txtNumDoc.ClientID %>">
                                    <abbr title="Número de documento">Nº documento</abbr>:</label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtNumDoc" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                              <div class="col-md-2">
                                <label for="<%= txtNombre.ClientID %>">Nombre/Apellidos:</label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
                            </div>
                             <div class="col-md-2">
                                <label for="<%= txtTelefono.ClientID %>" >Teléfono:</label>
                            </div>
                            <div class="col-md-4">
                                 <asp:TextBox ID="txtTelefono" runat="server" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="row filtro">
                            <div class="col-md-2">
                                <asp:Button ID="btnBuscar" runat="server" CssClass="botonbuscar" Style="float: left !important" Text="Buscar" ToolTip="Busca por los valores seleccionados" OnClick="btnBuscar_Click" />
                            </div>
                            <div class="col-md-1">
                                <asp:Button ID="btnLimpiar" runat="server" CssClass="botonbuscar" Style="float: left !important" Text="Limpiar" ToolTip="Deja los campos de búsqueda en su estado inicial" OnClick="btnLimpiar_Click" />
                            </div>
                            <div class="col-md-9">
                            </div>
                        </div>
                             </div>
                    </asp:Panel>
                   
                <div class="espaciosup" style="overflow: hidden;">
                    <div>
                        <h2>Listado usuarios</h2>
                    </div>
                </div>
               <%-- <div id="dvGridUsu" style="background-color: white;" runat="server">--%>
                    <div id="dvGridUsu" runat="server" style="overflow:auto">
                    <asp:GridView ID="gvUsuarios" runat="server" AllowSorting="true" AutoGenerateColumns="false" Style="width: 100%" BorderStyle="Outset" BorderWidth="1"
                        OnSorting="gvUsuarios_Sorting" OnRowCreated="gvUsuarios_RowCreated" OnRowCommand="gvUsuarios_RowCommand" EmptyDataRowStyle-VerticalAlign="Middle"
                        EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles." HeaderStyle-CssClass="gridViewHeader" CssClass="gridView" AccessKey="G">
                        <Columns>
                             <asp:TemplateField HeaderText=" " ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgDetalleUsu" runat="server" CommandArgument='<%# Eval("ID_USUARIO") %>' CommandName="Consulta" CausesValidation="false" ToolTip="Consultar usuario" ImageUrl="~/Content/Imagenes/lupa.png" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Wrap="false" Height="26px" Width="25px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" " ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgModUsu" runat="server" CommandArgument='<%# Eval("ID_USUARIO") %>' CommandName="Modificar" CausesValidation="false" ToolTip="Modificar usuario" ImageUrl="~/Content/Imagenes/pencil.png" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Wrap="false" Height="26px" Width="25px" />
                            </asp:TemplateField>
                               <asp:TemplateField HeaderText=" " ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgBorrarUsu" runat="server" CommandArgument='<%# Eval("ID_USUARIO") %>' CommandName="Baja" CausesValidation="false" ToolTip="Baja del usuario" OnClientClick="if(!confirm('Se va a dar de baja al usuario de la AMPA, ¿desea continuar?')){return false;}" ImageUrl="~/Content/Imagenes/trash.png" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Wrap="false" Height="26px" Width="25px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Documento" ShowHeader="true" AccessibleHeaderText="Documento"
                                SortExpression="TIPO_DOCUMENTO,NUMERO_DOCUMENTO" ControlStyle-Width="110" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="110" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCortadoDoc" runat="server" Text='<%# string.Concat(Eval("TIPO_DOCUMENTO.NOMBRE"), ": ", Eval("NUMERO_DOCUMENTO")) %>' ToolTip='<%# string.Concat(Eval("TIPO_DOCUMENTO.NOMBRE"), ": ", Eval("NUMERO_DOCUMENTO")) %>'
                                        CssClass="acortaLabel documento" Width="110">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre" ShowHeader="true" AccessibleHeaderText="Nombre"
                                SortExpression="NOMBRE" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="250" />
                                <ItemTemplate>
                                    <asp:Label ID="lbNombre" runat="server" Text='<%# string.Concat(Eval("NOMBRE"), " ", Eval("APELLIDO1"), " ", Eval("APELLIDO2")) %>' ToolTip='<%# string.Concat(Eval("NOMBRE"), " ", Eval("APELLIDO1"), " ", Eval("APELLIDO2")) %>'
                                        CssClass="acortaLabel nombre" Width="250">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Teléfono" ShowHeader="true" AccessibleHeaderText="Teléfono"
                                SortExpression="TELEFONO" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="150" />
                                <ItemTemplate>
                                    <asp:Label ID="lbTelefono" runat="server" Text='<%# Eval("TELEFONO") %>' ToolTip='<%# Eval("TELEFONO") %>'
                                        CssClass="acortaLabel solicitante" Width="150">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Correo electrónico" ShowHeader="true" AccessibleHeaderText="Teléfono"
                                SortExpression="EMAIL" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="150" />
                                <ItemTemplate>
                                    <asp:Label ID="lbEmail" runat="server" Text='<%# Eval("EMAIL") %>' ToolTip='<%# Eval("EMAIL") %>'
                                        CssClass="acortaLabel solicitante" Width="150">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--estilo para paginación gridViewPaginacion
                                <asp:TemplateField HeaderText="Origen" ShowHeader="true" AccessibleHeaderText="Origen"
                                SortExpression="RAE_ACTORES1.ACTOR" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="75" />
                                <ItemTemplate>
                                    <asp:Label ID="lbActor" runat="server" Text='<%# (RAEE.Comun.TipoDatos.Actores)Enum.Parse(typeof(RAEE.Comun.TipoDatos.Actores), Eval( "ID_ACTOR_ALTA").ToString()) %>'
                                        ToolTip='<%# (RAEE.Comun.TipoDatos.Actores)Enum.Parse(typeof(RAEE.Comun.TipoDatos.Actores), Eval( "ID_ACTOR_ALTA").ToString()) %>' Width="75">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                        </Columns>
                    </asp:GridView>                    
                </div>

          <%--  <div style="height: 52px; margin: 0px; padding: 0px;">
                <ucPaginacion:Paginacion runat="server" ID="gridPaginacion"
                    OnRecargarGrid="gridPaginacion_RecargarGrid"></ucPaginacion:Paginacion>
            </div>--%>
        </div>
           </form>
        </div>
 </asp:Content>
