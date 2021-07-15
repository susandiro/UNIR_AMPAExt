<%@ Page Language="C#" Title="Gestión de usuarios" MasterPageFile="~/Master/SiteExtraescolar.Master" AutoEventWireup="true" CodeBehind="GestionUsuariosExt.aspx.cs" Inherits="AMPAExt.UI.Extraescolar.GestionUsuariosExt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--content midd-->
    <div id="content_midd">
        <form id="form1" runat="server">
            <h1 class="tituloCentral">Gestión de usuarios de empresa extraescolar</h1>
            <div class="espacioleft">
                <div class="row">
                    <div class="col-md-11">
                        <h2>Buscar</h2>
                    </div>
                    <div class="col-md-1">
                        <asp:LinkButton ID="btnExpandir" CssClass="icon" runat="server" data-toggle="collapse" data-target="#dvFiltro" ToolTip="Mostrar/Ocultar"><img src="https://img.icons8.com/office/20/000000/collapse.png"/></asp:LinkButton></div>
                </div>
                <asp:Panel runat="server" DefaultButton="btnBuscar">
                    <div class="collapse show" id="dvFiltro">
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
                                <label for="<%= txtTelefono.ClientID %>">Teléfono:</label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtTelefono" MaxLength="15" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row" id="dvEmpresa" runat="server">
                            <div class="col-md-2">
                                <label for="<%= cmbEmpresa.ClientID %>">Empresa:</label>
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="cmbEmpresa" DataTextField="EMPRESA.NOMBRE" DataValueField="ID_EMPRESA" runat="server" ToolTip="Seleccione la empresa" />
                            </div>
                            <div class="col-md-6"></div>
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
                <div id="dvGridUsu" runat="server" style="overflow: auto">
                    <asp:GridView ID="gvUsuarios" runat="server" AllowSorting="false" AutoGenerateColumns="false" Style="width: 100%;" BorderStyle="Outset" BorderWidth="1"
                        OnRowCreated="gvUsuarios_RowCreated" OnRowCommand="gvUsuarios_RowCommand" EmptyDataRowStyle-VerticalAlign="Middle"
                        EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles." CssClass="Grid" AccessKey="G">
                        <Columns>
                            <asp:TemplateField HeaderText=" " ShowHeader="False">
                                <HeaderStyle Width="50" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgDetalleUsu" runat="server" CommandArgument='<%# Eval("ID_USUARIO_EMP") %>' CommandName="Consulta" CausesValidation="false" ToolTip="Consultar usuario" ImageUrl="~/Content/Imagenes/lupa.png" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Wrap="false"  />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" " ShowHeader="False">
                                <HeaderStyle Width="50" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgModiUsu" runat="server" CommandArgument='<%# Eval("ID_USUARIO_EMP") %>' CommandName="Modificar" CausesValidation="false" ToolTip="Modificar usuario" ImageUrl="~/Content/Imagenes/pencil.png" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Wrap="false"  />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" " ShowHeader="False">
                                <HeaderStyle Width="50" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgBorrarUsu" runat="server" CommandArgument='<%# string.Concat(Eval("ID_USUARIO_EMP"), ",", Eval("ID_EMPRESA")) %>' CommandName="Baja" CausesValidation="false" ToolTip="Baja del usuario" OnClientClick="if(!confirm('Se va a dar de baja al usuario de la empresa, ¿desea continuar?')){return false;}" ImageUrl="~/Content/Imagenes/trash.png" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Wrap="false" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Documento" ShowHeader="true" AccessibleHeaderText="Documento"
                                ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                  <ItemTemplate>
                                    <asp:Label ID="lblCortadoDoc" runat="server" Text='<%# string.Concat(Eval("TIPO_DOCUMENTO.NOMBRE"), ": ", Eval("NUMERO_DOCUMENTO")) %>' ToolTip='<%# string.Concat(Eval("TIPO_DOCUMENTO.NOMBRE"), ": ", Eval("NUMERO_DOCUMENTO")) %>'
                                        CssClass="acortaLabel corto" Width="150">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre" ShowHeader="true" AccessibleHeaderText="Nombre"
                                ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                               <ItemTemplate>
                                    <asp:Label ID="lbNombre" runat="server" Text='<%# string.Concat(Eval("NOMBRE"), " ", Eval("APELLIDO1"), " ", Eval("APELLIDO2")) %>' ToolTip='<%# string.Concat(Eval("NOMBRE"), " ", Eval("APELLIDO1"), " ", Eval("APELLIDO2")) %>'
                                        CssClass="acortaLabel largo" Width="250">
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
                            <asp:TemplateField HeaderText="Teléfono" ShowHeader="true" AccessibleHeaderText="Teléfono"
                                ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                               
                                <ItemTemplate>
                                    <asp:Label ID="lbTelefono" runat="server" Text='<%# Eval("TELEFONO") %>' ToolTip='<%# Eval("TELEFONO") %>'
                                        CssClass="acortaLabel corto" Width="150">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Correo electrónico" ShowHeader="true" AccessibleHeaderText="Teléfono"
                                ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                
                                <ItemTemplate>
                                    <asp:Label ID="lbEmail" runat="server" Text='<%# Eval("EMAIL") %>' ToolTip='<%# Eval("EMAIL") %>'
                                        CssClass="acortaLabel medio" Width="200">
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
