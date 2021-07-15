<%@ Page Title="Alta de usuario AMPA" Language="C#" MasterPageFile="~/Master/SiteActividad.Master" AutoEventWireup="true" CodeBehind="GestionarAsignacion.aspx.cs" Inherits="AMPAExt.UI.Actividades.GestionarAsignacion" %>
<%@ Register TagPrefix="uc" TagName="PanelInformativo" Src="~/UI/Controles/panelInformacion.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--content midd-->
    <div id="content_midd">
        <form id="form1" runat="server">
            <h1 class="tituloCentral">Gestión de extraescolares para el alumno</h1>
            <uc:PanelInformativo ID="PanelInfo" runat="server"></uc:PanelInformativo>
            <p class="espacioleft">Los campos marcados con <span class="errorValid">*</span> son obligatorios</p>
            <div class="espacioleft">
                 <div class="row">
                    <div class="col-md-12">
                        <h1 style="border-bottom:dotted; border-width:1px; width:80%; border-color:#316074">Datos del alumno</h1>
                    </div>
                     </div>
                <div class="row">
                <div class="col-lg">
                    <div class="alert alert-warning" role="alert" runat="server" id="dvIndividual" visible="false">
                        <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true">&nbsp;</span><b>Atención:</b> El socio responde de manera individual
                    </div>
                </div>
            </div>
                  <div class="row">
                    <div class="col-md-2">
                        <label for="txtNombre">Nombre: </label>
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtNombre" Enabled="false" runat="server" ToolTip="Nombre del alumno"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                 <div class="row">
                    <div class="col-md-2">
                        <label for="txtFecha">Fecha nacimiento:</label>
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtFecha" Enabled="false" ToolTip="Fecha de nacimiento" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <label for="txtCurso">Curso:</label>
                    </div>
                    <div class="col-md-8">
                         <asp:TextBox ID="txtCurso" Enabled="false" ToolTip="Curso del alumno" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                 <div class="row">
                     <div class="col-md-4"></div>
                    <div class="col-md-3">
                        <asp:Image ImageUrl="~/Content/Imagenes/flecha.gif" CssClass="flecha" runat="server" AlternateText="De origen a destino" />
                        </div>
                     <div class="col-md-5"></div>
                     </div>
                 <div class="row">
                    <div class="col-md-12">
                        <h1 style="border-bottom:dotted; border-width:1px; width:80%; border-color:#316074;"> Actividad extraescolar</h1>
                    </div>
                     </div>
                     <div class="row">
                     <div class="col-md-1">
                        <label for="cmbEmpresa">Empresa <span style="color: red">*</span>:</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList ID="cmbEmpresa" AutoPostBack="true" runat="server" OnSelectedIndexChanged="cmbEmpresa_SelectedIndexChanged" ToolTip="Seleccione la empresa de destino" CssClass="txtObligatorio" />
                    </div>
                    <div class="col-md-2">
                        <label for="cmbActividad">Actividad extraescolar <span style="color: red">*</span>:</label>
                    </div>
                    <div class="col-md-3"> 
                        <asp:DropDownList ID="cmbActividad" Enabled="false" AutoPostBack="true" DataTextField="NOMBRE" DataValueField="ID_ACTIVIDAD" runat="server" ToolTip="Seleccione la actividad extraescolar de destino" OnSelectedIndexChanged="cmbActividad_SelectedIndexChanged" CssClass="txtObligatorio" />
                    </div>
                   <div class="col-md-1">
                        <label for="cmbHorario">Horario <span style="color: red">*</span>:</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="cmbHorario" Enabled="false" runat="server" ToolTip="Seleccione el horario de destino" CssClass="txtObligatorio" />
                    </div>
                </div>

                <div class="row">
                 <div class="col-md-1"></div>
                    <div class="col-md-2"> <asp:Label ID="lblEmpresa" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label></div>
                    <div class="col-md-2"></div>
                    <div class="col-md-3"><asp:Label ID="lblActividad" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label></div>
                    <div class="col-md-1"></div>
                    <div class="col-md-3"><asp:Label ID="lblHorario" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label></div>
                    </div>
                <div class="row espaciosupBoton">
                    <div class="col-md-12">
                        <asp:Button ID="BtnAsignar" CssClass="boton " runat="server" ToolTip="Asigna la extraescolar al alumno" OnClientClick="if(!confirm('Se va a realizar la asignación de la extraescolar al alumno, ¿desea continuar?')){return false;}" OnClick="BtnAsignar_Click" Text="Alta en extraescolar" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <h1 style="border-bottom:dotted; border-width:1px; width:80%; border-color:#316074; padding-top:1.5em"> Listado de actividades extraescolares del alumno</h1>
                    </div>
                     </div>
                <div style="overflow: auto">
                  <asp:GridView ID="gvActividades" runat="server" AllowSorting="false" AutoGenerateColumns="false" Style="width: 100%;" BorderStyle="Outset" BorderWidth="1"
                        OnRowCreated="gvActividades_RowCreated" OnRowCommand="gvActividades_RowCommand" EmptyDataRowStyle-VerticalAlign="Middle"
                        EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles." CssClass="Grid" AccessKey="G">
                        <Columns>
                               <asp:TemplateField HeaderText=" " ShowHeader="False"><HeaderStyle Width="50" />
                                <ItemTemplate>
                                    <HeaderStyle Width="50"/>
                                    <asp:ImageButton ID="imgBorrarAct" runat="server" CommandArgument='<%# Eval("ACTIVIDAD_HORARIO.ID_ACT_HORARIO") %>' CommandName="Baja" CausesValidation="false" ToolTip="Baja de la actividad" OnClientClick="if(!confirm('Se va a dar de baja al alumno, de la actividad extraescolar, ¿desea continuar?')){return false;}" ImageUrl="~/Content/Imagenes/trash.png" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Wrap="false" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Empresa extraescolar" ShowHeader="true" AccessibleHeaderText="empresa"
                                 ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblCortadoEpr" runat="server"  Text='<%# Eval("ACTIVIDAD_HORARIO.ACTIVIDAD.EMPRESA.NOMBRE") %>' ToolTip='<%# Eval("ACTIVIDAD_HORARIO.ACTIVIDAD.EMPRESA.NOMBRE") %>'
                                        CssClass="acortaLabel largo" Width="250">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Actividad extraescolar" ShowHeader="true" AccessibleHeaderText="actividad"
                                 ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblCortadoDoc" runat="server"  Text='<%# Eval("ACTIVIDAD_HORARIO.ACTIVIDAD.NOMBRE") %>' ToolTip='<%# Eval("ACTIVIDAD_HORARIO.ACTIVIDAD.NOMBRE") %>'
                                        CssClass="acortaLabel largo" Width="250">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Días de la actividad" ShowHeader="true" AccessibleHeaderText="Dias"
                                ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbNombre" runat="server" Text='<%# Eval("ACTIVIDAD_HORARIO.DIAS") %>' ToolTip='<%# Eval("ACTIVIDAD_HORARIO.DIAS") %>'
                                        CssClass="acortaLabel corto" Width="100">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Horario" ShowHeader="true" AccessibleHeaderText="Horario"
                                ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbTelefono" runat="server" Text='<%# string.Concat(Eval("ACTIVIDAD_HORARIO.HORA_INI"), " - ", Eval("ACTIVIDAD_HORARIO.HORA_FIN")) %>' ToolTip='<%# string.Concat(Eval("ACTIVIDAD_HORARIO.HORA_INI"), " - ", Eval("ACTIVIDAD_HORARIO.HORA_FIN")) %>'
                                        CssClass="acortaLabel corto" Width="100">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>  
                    </div>
                 <div class="row espaciosupBoton">
                    <div class="col-md-12">
                        <asp:Button ID="BtnCancelar" CssClass="boton" runat="server" ToolTip="Vuelve a la página anterior" OnClick="BtnCancelar_Click" Text="Cancelar" />
                    </div>
                </div>
            </div>
        </form>
        <div class="clr"></div>
    </div>
</asp:Content>