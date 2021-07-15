<%@ Page Language="C#" Title="Gestión de usuarios" MasterPageFile="~/Master/SiteSocio.Master" AutoEventWireup="true" CodeBehind="GestionSocios.aspx.cs" Inherits="AMPAExt.UI.Socios.GestionSocios" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--content midd-->
    <div id="content_midd">
        <form id="form1" runat="server">
            <h1 class="tituloCentral">Gestión de socios de la AMPA</h1>
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
                            <div class="col-md-2">
                                <label for="<%= cmbTipoDocumento.ClientID %>">Tipo documento:</label>
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="cmbTipoDocumento" DataTextField="NOMBRE" DataValueField="ID_TIPO_DOCUMENTO" runat="server" ToolTip="Seleccione el tipo de documento del socio" />
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
                                <label for="<%= txtNumSocio.ClientID %>" > <abbr title="Número de socio">Nº socio:</abbr></label>
                            </div>
                            <div class="col-md-4">
                                 <asp:TextBox ID="txtNumSocio" TextMode="Number" runat="server" ></asp:TextBox>
                            </div>
                              <div class="col-md-2">
                                <label for="<%= txtNombre.ClientID %>">Nombre/Apellidos:</label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
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
                        <h2>Listado socios</h2>
                    </div>
                </div>
                    <div id="dvGridUsu" runat="server" style="overflow:auto">
                    <asp:GridView ID="gvSocios" runat="server" AllowSorting="false" AutoGenerateColumns="false" Style="width: 100%;" BorderStyle="Outset" BorderWidth="1"
                        OnRowCreated="gvSocios_RowCreated" OnRowCommand="gvSocios_RowCommand" EmptyDataRowStyle-VerticalAlign="Middle"
                        EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles." CssClass="Grid" AccessKey="G">
                        <Columns>
                             <asp:TemplateField HeaderText=" " ShowHeader="False">
                                  <HeaderStyle Width="50" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgDetalleUsu" runat="server" CommandArgument='<%# Eval("ID_TUTOR") %>' CommandName="Consulta" CausesValidation="false" ToolTip="Consultar socio" ImageUrl="~/Content/Imagenes/lupa.png" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Wrap="false" Height="26px" Width="25px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" " ShowHeader="False">
                                 <HeaderStyle Width="50" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgModiUsu" runat="server" CommandArgument='<%# Eval("ID_TUTOR") %>' CommandName="Modificar" CausesValidation="false" ToolTip="Modificar socio" ImageUrl="~/Content/Imagenes/pencil.png" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Wrap="false" Height="26px" Width="25px" />
                            </asp:TemplateField>
                               <asp:TemplateField HeaderText=" " ShowHeader="False">
                                    <HeaderStyle Width="50" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgBorrarUsu" runat="server" CommandArgument='<%# Eval("ID_TUTOR") %>' CommandName="Baja" CausesValidation="false" ToolTip="Baja del socio" OnClientClick="if(!confirm('Se va a dar de baja el socio de la AMPA, ¿desea continuar?')){return false;}" ImageUrl="~/Content/Imagenes/trash.png" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Wrap="false" Height="26px" Width="25px" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Nº de socio" ShowHeader="true" AccessibleHeaderText="nSocio"
                                ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblNumSoc" runat="server" Text='<%# Eval("ID_TUTOR") %>' ToolTip='<%# Eval("ID_TUTOR") %>'
                                        CssClass="acortaLabel" Width="50">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Documento padre/tutor1" ShowHeader="true" AccessibleHeaderText="Documento"
                                ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblCortadoDoc" runat="server" Text='<%# string.Concat(Eval("TIPO_DOCUMENTO.NOMBRE"), ": ", Eval("T1_NUMERO_DOCUMENTO")) %>' ToolTip='<%# string.Concat(Eval("TIPO_DOCUMENTO.NOMBRE"), ": ", Eval("T1_NUMERO_DOCUMENTO")) %>'
                                        CssClass="acortaLabel corto" Width="150">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre padre/tutor1 " ShowHeader="true" AccessibleHeaderText="Nombre"
                                ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbNombre" runat="server" Text='<%# string.Concat(Eval("T1_NOMBRE"), " ", Eval("T1_APELLIDO1"), " ", Eval("T1_APELLIDO2")) %>' ToolTip='<%# string.Concat(Eval("T1_NOMBRE"), " ", Eval("T1_APELLIDO1"), " ", Eval("T1_APELLIDO2")) %>'
                                        CssClass="acortaLabel largo" Width="250">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Teléfono padre/tutor1" ShowHeader="true" AccessibleHeaderText="Telefono"
                                ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbTelefono" runat="server" Text='<%# Eval("T1_TELEFONO") %>' ToolTip='<%# Eval("T1_TELEFONO") %>'
                                        CssClass="acortaLabel corto" Width="150">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Documento padre/tutor2" ShowHeader="true" AccessibleHeaderText="Documento"
                                ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblCortadoDoc2" runat="server" Text='<%# (Eval("TIPO_DOCUMENTO1") != null)? string.Concat(Eval("TIPO_DOCUMENTO1.NOMBRE"), ": ", Eval("T2_NUMERO_DOCUMENTO")):"" %>' ToolTip='<%# (Eval("TIPO_DOCUMENTO1") != null)? string.Concat(Eval("TIPO_DOCUMENTO1.NOMBRE"), ": ", Eval("T2_NUMERO_DOCUMENTO")):"" %>'
                                        CssClass="acortaLabel corto" Width="150">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre padre/tutor2 " ShowHeader="true" AccessibleHeaderText="Nombre"
                                ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbNombre2" runat="server" Text='<%# string.Concat(Eval("T2_NOMBRE"), " ", Eval("T2_APELLIDO1"), " ", Eval("T2_APELLIDO2")) %>' ToolTip='<%# string.Concat(Eval("T2_NOMBRE"), " ", Eval("T2_APELLIDO1"), " ", Eval("T2_APELLIDO2")) %>'
                                        CssClass="acortaLabel largo" Width="250">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Teléfono padre/tutor2" ShowHeader="true" AccessibleHeaderText="Telefono"
                                ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbTelefono2" runat="server" Text='<%# Eval("T2_TELEFONO") %>' ToolTip='<%# Eval("T2_TELEFONO") %>'
                                        CssClass="acortaLabel corto" Width="150">
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
