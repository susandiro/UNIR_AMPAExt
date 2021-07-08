<%@ Page Language="C#" Title="Mantenimiento de empresas" MasterPageFile="~/Master/SiteExtraescolar.Master" AutoEventWireup="true" CodeBehind="MantenimientoExt.aspx.cs" Inherits="AMPAExt.UI.Extraescolar.MantenimientoExt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--content midd-->
    <div id="content_midd">
        <form id="form1" runat="server">
            <h1 class="tituloCentral">Mantenimiento de empresas extraescolares</h1>
               <div class="espacioleft">
                   <div class="row">
                            <div class="col-md-11">
                            <h2>Buscar</h2>    
                            </div>
                            <div class="col-md-1"><asp:LinkButton ID="btnExpandir" CssClass="icon"  runat="server" data-toggle="collapse" data-target="#dvFiltro" ToolTip="Mostrar/Ocultar"><img src="https://img.icons8.com/office/20/000000/collapse.png"/></asp:LinkButton></div>
                   </div>
                     <asp:Panel runat="server"  DefaultButton="btnBuscar">
                         <div class="collapse show" id="dvFiltro">
                        <div class="row">
                            <div class="col-md-1">
                                <label for="<%= txtNIF.ClientID %>">
                                    <abbr title="Número de Identificación Fiscal">NIF</abbr>:</label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtNIF" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-1">
                                <label for="<%= txtNombre.ClientID %>">Nombre:</label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
                            </div>
                              <div class="col-md-2">
                                <label for="<%= cmbActivo.ClientID %>">Activo:</label>
                            </div>
                            <div class="col-md-2">
                                 <asp:DropDownList ID="cmbActivo" runat="server" ToolTip="Seleccione si sólo empresas activas S/N" />
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
                        <h2>Listado empresas</h2>
                    </div>
                </div>
                    <div id="dvGridemp" runat="server" style="overflow:auto">
                    <asp:GridView ID="gvEmpresas" runat="server" AllowSorting="false" AutoGenerateColumns="false" Style="width: 100%;" BorderStyle="Outset" BorderWidth="1"
                        OnRowCreated="gvEmpresas_RowCreated" OnRowCommand="gvEmpresas_RowCommand" EmptyDataRowStyle-VerticalAlign="Middle"
                        EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles." CssClass="Grid" AccessKey="G">
                        <Columns>
                             <asp:TemplateField HeaderText=" " ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgDetalleUsu" runat="server" CommandArgument='<%# Eval("ID_EMPRESA") %>' CommandName="Consulta" CausesValidation="false" ToolTip="Consultar empresa" ImageUrl="~/Content/Imagenes/lupa.png" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Wrap="false" Height="26px" Width="25px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" " ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgModiUsu" runat="server" CommandArgument='<%# Eval("ID_EMPRESA") %>' CommandName="Modificar" CausesValidation="false" ToolTip="Modificar empresa" ImageUrl="~/Content/Imagenes/pencil.png" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Wrap="false" Height="26px" Width="25px" />
                            </asp:TemplateField>
                               <asp:TemplateField HeaderText=" " ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgBorrarUsu" runat="server" CommandArgument='<%# Eval("ID_EMPRESA") %>' CommandName="Baja" CausesValidation="false" ToolTip="Baja de la empresa" OnClientClick="if(!confirm('Se va a dar de baja a la empresa, ¿desea continuar?')){return false;}" ImageUrl="~/Content/Imagenes/trash.png" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Wrap="false" Height="26px" Width="25px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Documento" ShowHeader="true" AccessibleHeaderText="Documento"
                                ControlStyle-Width="110" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="110" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCortadoDoc" runat="server" Text='<%# Eval("EMPRESA.NIF") %>' ToolTip='<%# Eval("EMPRESA.NIF") %>'
                                        CssClass="acortaLabel documento" Width="110">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre" ShowHeader="true" AccessibleHeaderText="Nombre"
                                ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="250" />
                                <ItemTemplate>
                                    <asp:Label ID="lbNombre" runat="server" Text='<%# Eval("EMPRESA.NOMBRE") %>' ToolTip='<%# Eval("EMPRESA.NOMBRE") %>'
                                        CssClass="acortaLabel nombre" Width="250">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Teléfono" ShowHeader="true" AccessibleHeaderText="Teléfono"
                                ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="150" />
                                <ItemTemplate>
                                    <asp:Label ID="lbTelefono" runat="server" Text='<%# Eval("EMPRESA.TELEFONO") %>' ToolTip='<%# Eval("EMPRESA.TELEFONO") %>'
                                        CssClass="acortaLabel solicitante" Width="150">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Activo" ShowHeader="true" AccessibleHeaderText="Activo"
                                ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="150" />
                                <ItemTemplate>
                                    <asp:Label ID="lbActivo" runat="server" Text='<%# Eval("ACTIVO") %>' ToolTip='<%# Eval("ACTIVO") %>'
                                        CssClass="acortaLabel solicitante" Width="150">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>                    
                </div>
        </div>
           </form>
        </div>
 </asp:Content>
