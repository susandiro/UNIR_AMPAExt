<%@ Page Title="Alta de empresa extraescolar" Language="C#" MasterPageFile="~/Master/SiteActividad.Master" AutoEventWireup="true" CodeBehind="DetalleActividad.aspx.cs" Inherits="AMPAExt.UI.Actividades.DetalleActividad" %>

<%@ Register TagPrefix="uc" TagName="PanelInformativo" Src="~/UI/Controles/panelInformacion.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--content midd-->
    <div id="content_midd">
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            <h1 class="tituloCentral">Consulta de actividad extraescolar</h1>
            <uc:PanelInformativo ID="PanelInfo" runat="server"></uc:PanelInformativo>
            <div class="espacioleft">
                <h1 style="border-bottom: dotted; border-width: 1px; width: 80%; border-color: #316074; padding-top: 1.5em">Actividad extraescolares </h1>
                <div class="row" runat="server" id="dvEmpresa">
                    <div class="col-md-3">
                        <label for="txtEmpresa">Empresa extraescolar:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtEmpresa" runat="server" ToolTip="Empresa extraescolar" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                <div class="row" runat="server" id="dvAMPA">
                    <div class="col-md-3">
                        <label for="cmbAMPA">AMPA:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtAMPA" runat="server" ToolTip="AMPA" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtActNombre">Actividad: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtActNombre" runat="server" Enabled="false" ToolTip="Nombre de la actividad extraescolar"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtActDescripcion">Descripción: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtActDescripcion" ToolTip="Descripción de la actividad" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                <h1 style="border-bottom: dotted; border-width: 1px; width: 80%; border-color: #316074; padding-top: 1.5em">Horario en que se imparte</h1>
                <div class="row">
                    <div class="col-md-3">
                    </div>
                    <div class="col-md-6">
                        <asp:GridView ID="gvHorarios" runat="server" AllowSorting="false" AutoGenerateColumns="false" BorderStyle="Outset" BorderWidth="1"
                            EmptyDataRowStyle-VerticalAlign="Middle" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles." Enabled="false" Width="100%" CssClass="Grid" AccessKey="G">
                            <Columns>
                                <asp:TemplateField HeaderText="Días" ShowHeader="true" AccessibleHeaderText="Dias"
                                    ControlStyle-Width="110" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle Width="110" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCortadoDoc" runat="server" Text='<%# Eval("DIAS") %>' ToolTip='<%# Eval("DIAS") %>'
                                            Width="250">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Hora inicio" ShowHeader="true" AccessibleHeaderText="HoraIni"
                                    ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle Width="100" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbhoraini" runat="server" Text='<%# Eval("HORA_INI") %>' ToolTip='<%# Eval("HORA_INI") %>'
                                            Width="100">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Hora finalización" ShowHeader="true" AccessibleHeaderText="HoraFin"
                                    ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle Width="100" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbhorafin" runat="server" Text='<%# Eval("HORA_FIN") %>' ToolTip='<%# Eval("HORA_FIN") %>'
                                            Width="100">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cuota" ShowHeader="true" AccessibleHeaderText="Cuota"
                                    ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle Width="100" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbCuota" runat="server" Text='<%# Eval("CUOTA") %>' ToolTip='<%# Eval("CUOTA") %>'
                                            Width="100">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Monitor" ShowHeader="true" AccessibleHeaderText="Monitor"
                                    ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle Width="100" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbMonitor" runat="server" Text='<%# Eval("MONITOR.NOMBRE") %>' ToolTip='<%# Eval("MONITOR.NOMBRE") %>'
                                            Width="100">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-md-3">
                    </div>
                </div>
                <h1 style="border-bottom: dotted; border-width: 1px; width: 80%; border-color: #316074; padding-top: 1.5em">Descuentos aplicables</h1>

                <div class="row">
                    <div class="col-md-3"></div>
                    <div class="col-md-6">
                        <asp:GridView ID="gvDescuento" runat="server" AllowSorting="false" AutoGenerateColumns="false" BorderStyle="Outset" BorderWidth="1"
                            EmptyDataRowStyle-VerticalAlign="Middle" Enabled="false"
                            EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles." Width="100%" CssClass="Grid" AccessKey="G">
                            <Columns>
                                <asp:TemplateField HeaderText="Descuento" ShowHeader="true" AccessibleHeaderText="Descuento"
                                    ControlStyle-Width="250" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle Width="250" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCortadoDoc" runat="server" Text='<%# Eval("DESCUENTO.NOMBRE") %>' ToolTip='<%# Eval("DESCUENTO.NOMBRE") %>'
                                            Width="250">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Valor" ShowHeader="true" AccessibleHeaderText="Valor"
                                    ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle Width="100" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbValor" runat="server" Text='<%# Eval("VALOR") %>' ToolTip='<%# Eval("VALOR") %>'
                                            Width="100">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-md-3">
                    </div>
                </div>
                <div class="row espaciosupBoton">
                    <div class="col-md-12">
                        <asp:Button ID="BtnVolver" CssClass="boton " runat="server" ToolTip="Vuelve a la página anterior" OnClick="BtnVolver_Click" Text="Volver" />
                    </div>
                </div>
            </div>
        </form>
        <div class="clr"></div>
    </div>
</asp:Content>
