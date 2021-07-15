<%@ Page Title="Alta de empresa extraescolar" Language="C#" MasterPageFile="~/Master/SiteExtraescolar.Master" AutoEventWireup="true" CodeBehind="ModificarEmpresaExt.aspx.cs" Inherits="AMPAExt.UI.Extraescolar.ModificarEmpresaExt" %>
<%@ Register TagPrefix="uc" TagName="PanelInformativo" Src="~/UI/Controles/panelInformacion.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--content midd-->
    <div id="content_midd">
        <form id="form1" runat="server">
            <h1 class="tituloCentral">Modificación de empresa extraescolar</h1>
            <uc:PanelInformativo ID="PanelInfo" runat="server"></uc:PanelInformativo>
            <p class="espacioleft">Los campos marcados con <span class="errorValid">*</span> son obligatorios</p>
            <div class="espacioleft">
                <h1 style="border-bottom:dotted; border-width:1px; width:80%; border-color:#316074">Datos de la empresa extraescolar</h1>
               <div class="row">
                    <div class="col-md-3">
                        <label for="txtNumDocumento">NIF <span style="color: red">*</span>:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtNumDocumento" ToolTip="Número de documento de la empresa extraescolar" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblNumDocumento" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtNombre">Nombre <span style="color: red">*</span>: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtNombre" ToolTip="Nombre de la empresa extraescolar" runat="server" CssClass="txtObligatorio"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblNombre" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtRazonSocial">Razón social <span style="color: red">*</span>:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtRazonSocial" ToolTip="Razón social de la empresa extraescolar" runat="server" CssClass="txtObligatorio"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblRazonSocial" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtTelefono">Teléfono <span style="color: red">*</span>:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtTelefono" runat="server" MaxLength="15" ToolTip="Teléfono de contacto de la empresa extraescolar" CssClass="txtObligatorio" />
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblTelefono" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtWeb">Página web :</label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtWeb" ToolTip="Página web de la empresa extraescolar" runat="server" ></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblWeb" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                 <h1 style="border-bottom:dotted; border-width:1px; width:80%; border-color:#316074; padding-top:1.5em">Listado de monitores</h1>
                  <div id="dvGridMoni" runat="server" style="overflow: auto">
                    <asp:GridView ID="gvMonitores" runat="server" AllowSorting="false" AutoGenerateColumns="false" Style="width: 100%;" BorderStyle="Outset" BorderWidth="1"
                        OnRowCreated="gvMonitores_RowCreated" OnRowCommand="gvMonitores_RowCommand" EmptyDataRowStyle-VerticalAlign="Middle"
                        EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles." CssClass="Grid" AccessKey="G">
                        <Columns>
                            <asp:TemplateField HeaderText=" " ShowHeader="False">
                                <HeaderStyle Width="50" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgDetalle" runat="server" CommandArgument='<%# Eval("ID_MONITOR") %>' CommandName="Consulta" CausesValidation="false" ToolTip="Consultar monitor" ImageUrl="~/Content/Imagenes/lupa.png" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Wrap="false" Height="26px" Width="25px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" " ShowHeader="False">
                                <HeaderStyle Width="50" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgModiUsu" runat="server" CommandArgument='<%# Eval("ID_MONITOR") %>' CommandName="Modificar" CausesValidation="false" ToolTip="Modificar monitor" ImageUrl="~/Content/Imagenes/pencil.png" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Wrap="false" Height="26px" Width="25px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" " ShowHeader="False">
                                <HeaderStyle Width="50" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgBorrarUsu" runat="server" CommandArgument='<%# Eval("ID_MONITOR") %>' CommandName="Baja" CausesValidation="false" ToolTip="Baja del monitor" OnClientClick="if(!confirm('Se va a dar de baja al monitor para la empresa, ¿desea continuar?')){return false;}" ImageUrl="~/Content/Imagenes/trash.png" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Wrap="false" Height="26px" Width="25px" />
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
                <div class="row espaciosupBoton">
                    <div class="col-md-2">
                        <asp:Button ID="BtnModificar" CssClass="boton" runat="server" ToolTip="Modifica los datos de la empresa extraescolar" OnClick="BtnModificar_Click" Text="Modificar" />
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="BtnAltaMonitor" CssClass="boton" runat="server" ToolTip="Abre la ventana de alta de monitor" OnClick="BtnAltaMonitor_Click" Text="Nuevo monitor" />
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="BtnCancelar" CssClass="boton" runat="server" ToolTip="Vuelve a la página anterior" OnClick="BtnCancelar_Click" Text="Cancelar" />
                    </div>
                </div>
            </div>
        </form>
        <div class="clr"></div>
    </div>
</asp:Content>
