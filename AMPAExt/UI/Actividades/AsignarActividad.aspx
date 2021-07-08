<%@ Page Language="C#" Title="Gestión de usuarios" MasterPageFile="~/Master/SiteActividad.Master" AutoEventWireup="true" CodeBehind="AsignarActividad.aspx.cs" Inherits="AMPAExt.UI.Extraescolar.AsignarActividad" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--content midd-->
    <div id="content_midd">
        <form id="form1" runat="server">
            <h1 class="tituloCentral">Gestión de alumnos en extraescolares</h1>
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
                                <label for="<%= txtNombre.ClientID %>">Nombre/Apellidos:</label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
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
                                <label for="<%= cmbCurso.ClientID %>">Curso:</label>
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="cmbCurso"  DataTextField="NOMBRE" DataValueField="ID_CURSO" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbCurso_SelectedIndexChanged" ToolTip="Seleccione el curso del alumno" />
                            </div>
                             <div class="col-md-2">
                                <label for="<%= cmbClase.ClientID %>">Clase:</label>
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="cmbClase" Enabled="false" runat="server" ToolTip="Seleccione la clase del alumno" />
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
                        <h2>Listado de alumnos</h2>
                    </div>
                </div>
                    <div id="dvGridAlumno" runat="server" style="overflow:auto">
                    <asp:GridView ID="gvAlumno" runat="server" AllowSorting="false" AutoGenerateColumns="false" Style="width: 100%;" BorderStyle="Outset" BorderWidth="1"
                        OnRowCreated="gvAlumno_RowCreated" OnRowCommand="gvAlumno_RowCommand" EmptyDataRowStyle-VerticalAlign="Middle"
                        EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles." CssClass="Grid" AccessKey="G">
                        <Columns>
                                <asp:TemplateField HeaderText=" " ShowHeader="False">
                                    <HeaderStyle Width="50" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgAsignar" runat="server" CommandArgument='<%# Eval("ID_ALUMNO") %>' CommandName="Gestionar" CausesValidation="false" ToolTip="Gestiona las extraescolares para el alumno" ImageUrl="~/Content/Imagenes/Extraescolar.png" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="center" Wrap="false"  />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre del alumno" ShowHeader="true" AccessibleHeaderText="Nombre"
                                    ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbnombre" runat="server" Text='<%# string.Concat(Eval("NOMBRE"), " ", Eval("APELLIDO1"), " ", Eval("APELLIDO2")) %>' ToolTip='<%# string.Concat(Eval("NOMBRE"), " ", Eval("APELLIDO1"), " ", Eval("APELLIDO2")) %>'
                                            >
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Curso" ShowHeader="true" AccessibleHeaderText="Curso"
                                    ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbCurso" runat="server" Text='<%# string.Concat(Eval("CURSO_CLASE.CURSO.NOMBRE"), " ", Eval("CURSO_CLASE.CLASE.NOMBRE")) %>'>
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
