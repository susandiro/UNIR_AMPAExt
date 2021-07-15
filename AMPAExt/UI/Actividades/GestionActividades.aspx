<%@ Page Language="C#" Title="Mantenimiento de empresas" MasterPageFile="~/Master/SiteActividad.Master" AutoEventWireup="true" CodeBehind="GestionActividades.aspx.cs" Inherits="AMPAExt.UI.Actividades.GestionActividades" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="Paneles" TagName="uc" Src="~/UI/Controles/UCPanelConfirmacion.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--content midd-->
    <div id="content_midd">
        <form id="form1" runat="server">
             <asp:ScriptManager ID="ScriptManager1" runat="server" />
            <script type="text/javascript">
    function CerrarMensaje() {
        $('#panelConfirmacion').hide(600);
        $('#fondoGris2').hide(0); 
            
        }
            </script>
            <h1 class="tituloCentral">Gestión de actividades extraescolares</h1>
            <div class="espacioleft">
                <Paneles:uc runat="server" ID="PnlConfirmacion" Visible="false" onAceptarCrear="PnlConfirmacion_AceptarCrear"></Paneles:uc>
                <div class="row">
                    <div class="col-md-11">
                        <h2>Buscar actividades</h2>
                    </div>
                    <div class="col-md-1">
                        <asp:LinkButton ID="btnExpandir" CssClass="icon" runat="server" data-toggle="collapse" data-target="#dvFiltro" ToolTip="Mostrar/Ocultar"><img src="https://img.icons8.com/office/20/000000/collapse.png"/></asp:LinkButton></div>
                </div>
                <asp:Panel runat="server" DefaultButton="btnBuscar">
                    <div class="collapse show" id="dvFiltro">
                        <div class="row">
                            <div class="col-md-1">
                                <label for="<%= cmbEmpresa.ClientID %>">Empresa:</label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="cmbEmpresa" runat="server" ToolTip="Seleccione la empresa" CssClass="obligatorio" />
                            </div>       
                            <div class="col-md-2"></div>
                            <div class="col-md-1">
                                <label for="<%= cmbEmpresa.ClientID %>">AMPA:</label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="cmbAMPA" runat="server" ToolTip="Seleccione la AMPA" CssClass="obligatorio" />
                            </div>
                            <div class="col-md-2"></div>
                        </div>
                        <div class="row">
                            <div class="col-md-1">
                                <label for="<%= txtNombre.ClientID %>">Nombre:</label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
                            </div>
                             <div class="col-md-2"></div>
                            <div class="col-md-1">
                                <label for="<%= cmbActivo.ClientID %>">Activo:</label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="cmbActivo" runat="server" ToolTip="Seleccione si sólo extraescolares activas S/N" />
                            </div>
                            <div class="col-md-2"></div>
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
                        <h2>Listado actividades</h2>
                    </div>
                </div>
                <div id="dvGridemp" runat="server" style="overflow: auto">
                    <asp:GridView ID="gvActividad" runat="server" AllowSorting="false" AutoGenerateColumns="false" Style="width: 100%;" BorderStyle="Outset" BorderWidth="1"
                        OnRowCreated="gvActividad_RowCreated" OnRowCommand="gvActividad_RowCommand" EmptyDataRowStyle-VerticalAlign="Middle"
                        EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles." CssClass="Grid" DataKeyNames="ACTIVO" AccessKey="G">
                        <Columns>
                            <asp:TemplateField HeaderText=" " ShowHeader="False">
                                 <HeaderStyle Width="50" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgCon" runat="server" CommandArgument='<%# Eval("ID_ACTIVIDAD") %>' CommandName="Consulta" CausesValidation="false" ToolTip="Consulta actividad" ImageUrl="~/Content/Imagenes/lupa.png" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Wrap="false" Height="26px" Width="25px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" " ShowHeader="False">
                                <HeaderStyle Width="50" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgMod" runat="server" CommandArgument='<%# Eval("ID_ACTIVIDAD") %>' CommandName="modificar" CausesValidation="false" ToolTip="Modificar actividad" ImageUrl="~/Content/Imagenes/pencil.png" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Wrap="false" Height="26px" Width="25px" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText=" " ShowHeader="False">
                                 <HeaderStyle Width="50" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgBorrar" runat="server" CommandArgument='<%# Eval("ID_ACTIVIDAD") %>' CommandName="Baja" CausesValidation="false" ToolTip="Baja de la actividad" OnClientClick="if(!confirm('Se va a dar de baja la actividad, ¿desea continuar?')){return false;}" ImageUrl="~/Content/Imagenes/trash.png" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Wrap="false" Height="26px" Width="25px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre actividad" ShowHeader="true" AccessibleHeaderText="Nombre actividad"
                                ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblCortadoDoc" runat="server" Text='<%# Eval("NOMBRE") %>' ToolTip='<%# Eval("NOMBRE") %>'
                                        CssClass="acortaLabel corto" Width="150">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Empresa" ShowHeader="true" AccessibleHeaderText="Empresa"
                                ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbEmpresa" runat="server" Text='<%# Eval("EMPRESA.NOMBRE") %>' ToolTip='<%# Eval("EMPRESA.NOMBRE") %>'
                                        CssClass="acortaLabel largo" Width="250">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AMPA" ShowHeader="true" AccessibleHeaderText="AMPA"
                                ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbAMPA" runat="server" Text='<%# Eval("AMPA.NOMBRE") %>' ToolTip='<%# Eval("AMPA.NOMBRE") %>'
                                        CssClass="acortaLabel largo" Width="250">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Activo" ShowHeader="true" AccessibleHeaderText="Activo"
                                ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbActivo" runat="server" Text='<%# Eval("ACTIVO") %>' ToolTip='<%# Eval("ACTIVO") %>'
                                        CssClass="acortaLabel corto" Width="150">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    
                    <%--<button type="button" id="BtnMostrar" runat="server" class="btn btn-primary" data-toggle="modal" data-target="#mdConfirmacion"></button>
                    <asp:Button ID="BtnMostrar" runat="server" Text="Confirmar" data-toggle="modal" data-target="#mdConfirmacion" ToolTip="Confirma" OnClick="BtnMostrar_Click" />
                      <div class="modal fade" runat="server" id="mdConfirmacion">
                        <div class="modal-dialog modal-dialog-centered">
                          <div class="modal-content">
                            <!-- Modal Header -->
                            <div class="modal-header">
                              <h4 class="modal-title">Confirmación de baja de actividad extraescolar</h4>
                              <button type="button" class="close" data-dismiss="modal">&times;</button>
                            </div>
                            <!-- Modal body -->
                            <div class="modal-body">
                              Se han encontrado alumnos asociados a la actividad. En caso de continuar, los alumnos también se darán de baja en la actividad, ¿desea continuar?
                            </div>
                            <!-- Modal footer -->
                            <div class="modal-footer">
                                <asp:Button ID="BtnConfirmar" runat="server" Text="Confirmar" ToolTip="Confirma la baja de la actividad y de los alumnos"  OnClick="BtnConfirmar_Click" />
                              <button type="button" class="btn btn-danger" data-dismiss="modal">Cancelar</button>
                            </div>
                          </div>
                        </div>
                      </div>--%>
                </div>
            </div>
            
        </form>
    </div>
</asp:Content>
