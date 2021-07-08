<%@ Page Title="Alta de empresa extraescolar" Language="C#" MasterPageFile="~/Master/SiteActividad.Master" AutoEventWireup="true" CodeBehind="ModificarActividad.aspx.cs" Inherits="AMPAExt.UI.Actividades.ModificarActividad" %>

<%@ Register TagPrefix="uc" TagName="PanelInformativo" Src="~/UI/Controles/panelInformacion.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--content midd-->
    <div id="content_midd">
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            <h1 class="tituloCentral">Modificación de actividad extraescolar</h1>
            <uc:PanelInformativo ID="PanelInfo" runat="server"></uc:PanelInformativo>
            <p class="espacioleft">Los campos marcados con <span class="errorValid">*</span> son obligatorios</p>
            <div class="espacioleft">
                <h1 style="border-bottom: dotted; border-width: 1px; width: 80%; border-color: #316074; padding-top: 1.5em">Actividad extraescolares </h1>
                <div class="row" runat="server" id="dvEmpresa">
                    <div class="col-md-3">
                        <label for="cmbEmpresa">Empresa extraescolar <span style="color: red">*</span>:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:DropDownList ID="cmbEmpresa" DataTextField="NOMBRE" DataValueField="ID_EMPRESA" runat="server" Enabled="false" ToolTip="Empresa extraescolar" AutoPostBack="true" CssClass="txtObligatorio" OnSelectedIndexChanged="cmbEmpresa_SelectedIndexChanged" />
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblEmpresa" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row" runat="server" id="dvAMPA">
                    <div class="col-md-3">
                        <label for="cmbAMPA">AMPA <span style="color: red">*</span>:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:DropDownList ID="cmbAMPA" DataTextField="NOMBRE" Enabled="false" DataValueField="ID_AMPA" runat="server" ToolTip="AMPA" CssClass="txtObligatorio" />
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblAMPA" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtActNombre">Actividad <span style="color: red">*</span>: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtActNombre" runat="server" ToolTip="Nombre de la actividad extraescolar" CssClass="txtObligatorio"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblActNombre" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtActDescripcion">Descripción <span style="color: red">*</span>: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtActDescripcion" ToolTip="Descripción de la actividad" runat="server" CssClass="txtObligatorio"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblActDescripcion" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                <h1 style="border-bottom: dotted; border-width: 1px; width: 80%; border-color: #316074; padding-top: 1.5em">Horario en que se imparte
                    <asp:Label ID="lblActHorario" runat="server" Text="Es obligatorio introducir al menos un horario" CssClass="errorValid" Visible="false"></asp:Label></h1>
                <asp:UpdatePanel runat="server" ID="upActividad">
                    <ContentTemplate>
                <div class="row">
                    <div class="col-md-3">
                    </div>
                    <div class="col-md-7">
                        <div class="row" style="padding-top: 0">
                            <div class="col-md-1">
                                <label for="chkDias">Días:</label>
                            </div>
                            <div class="col-md-11">
                                <asp:CheckBoxList runat="server" ToolTip="Marcar los días en los que se realiza la actividad" ID="chkDias" CssClass="chDias" RepeatLayout="Table" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Lunes" Value="L">Lunes</asp:ListItem>
                                    <asp:ListItem Text="Martes" Value="M">Martes</asp:ListItem>
                                    <asp:ListItem Text="Miércoles" Value="X">Miércoles</asp:ListItem>
                                    <asp:ListItem Text="Jueves" Value="J">Jueves</asp:ListItem>
                                    <asp:ListItem Text="Viernes" Value="V">Viernes</asp:ListItem>
                                </asp:CheckBoxList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <label for="txtHoraIni">Hora inicio:</label>
                            </div>
                            <div class="col-md-8">
                                <input type="time" runat="server" id="txtHoraIni">
                            </div>

                            <div class="col-md-1"></div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <label for="txtHoraFin">Hora fin:</label>
                            </div>
                            <div class="col-md-8">
                                <input type="time" runat="server" id="txtHoraFin">
                            </div>
                            <div class="col-md-1"></div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <label for="cmbMonitor">Monitor:</label>
                            </div>
                            <div class="col-md-8">
                                <asp:DropDownList Enabled="false" ID="cmbMonitor" DataTextField="NOMBRE" DataValueField="ID_MONITOR" runat="server" ToolTip="Monitor de la actividad" />
                            </div>
                            <div class="col-md-1"></div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <label for="txtActCuota">Cuota mensual (en €) <span style="color: red">*</span>:</label>
                            </div>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtActCuota" TextMode="Number" ToolTip="Cuota mensual en €" runat="server" CssClass="txtObligatorio"></asp:TextBox>
                            </div>
                            <div class="col-md-1"></div>
                        </div>
                        <div class="row">
                            <div class="col-md-12" style="float: left!important; vertical-align: middle;">
                                <asp:LinkButton CssClass="botonForm" ID="BtnAnadirHora" runat="server" ToolTip="Añade el horario a la actividad" OnClick="BtnAnadirHora_Click"><img src="../../Content/Imagenes/Horario.png" style="padding-right:10px"/> Añadir horario</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                    </div>
                    <div class="col-md-6">
                        <asp:GridView ID="gvHorarios" runat="server" AllowSorting="false" AutoGenerateColumns="false" BorderStyle="Outset" BorderWidth="1"
                            OnRowCreated="gv_RowCreated" OnRowCommand="gvHorarios_RowCommand" EmptyDataRowStyle-VerticalAlign="Middle"
                            EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles." Width="100%" CssClass="Grid" AccessKey="G">
                            <Columns>
                                <asp:TemplateField HeaderText=" " ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgBorrar" runat="server" CommandArgument='<%# Eval("ID_ACT_HORARIO") %>' CommandName="Baja" CausesValidation="false" ToolTip="Borrar horario" OnClientClick="if(!confirm('Se va a borrar el horario de la actividad, ¿desea continuar?')){return false;}" ImageUrl="~/Content/Imagenes/trash.png" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="center" Wrap="false" Height="26px" Width="25px" />
                                </asp:TemplateField>
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
                    <div class="col-md-7">
                        <div class="row">
                            <div class="col-md-3">
                                <label for="cmbActDescuentos">Descuento:</label>
                            </div>
                            <div class="col-md-8">
                                <asp:DropDownList ID="cmbActDescuentos" DataTextField="NOMBRE" DataValueField="ID_DESCUENTO" runat="server" ToolTip="Descuentos aplicables" />
                            </div>
                            <div class="col-md-1"></div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <label for="txtValor">Valor:</label>
                            </div>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtValor" TextMode="Number" ToolTip="Valor del descuento en %" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-1"></div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3"></div>
                    <div class="col-md-9">
                        <asp:LinkButton CssClass="botonForm" ID="BtnDescuento" runat="server" ToolTip="Añade el descuento a la actividad" OnClick="BtnDescuento_Click"><img src="../../Content/Imagenes/Dinero.png" style="padding-right:10px"/> Añadir descuento</asp:LinkButton>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3"></div>
                    <div class="col-md-6">
                        <asp:GridView ID="gvDescuento" runat="server" AllowSorting="false" AutoGenerateColumns="false" BorderStyle="Outset" BorderWidth="1"
                            OnRowCreated="gv_RowCreated" OnRowCommand="gvDescuento_RowCommand" EmptyDataRowStyle-VerticalAlign="Middle"
                            EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles." Width="100%" CssClass="Grid" AccessKey="G">
                            <Columns>
                                <asp:TemplateField HeaderText=" " ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgDesBorrar" runat="server" CommandArgument='<%# Eval("ID_ACT_DESCUENTO") %>' CommandName="Baja" CausesValidation="false" ToolTip="Borrar descuento" OnClientClick="if(!confirm('Se va a borrar el descuento sobre la actividad, ¿desea continuar?')){return false;}" ImageUrl="~/Content/Imagenes/trash.png" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="center" Wrap="false" Height="26px" Width="25px" />
                                </asp:TemplateField>
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
                        </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="BtnDescuento" EventName="Click" />
                         <asp:AsyncPostBackTrigger ControlID="BtnAnadirHora" EventName="Click" />
                    </Triggers>
                    </asp:UpdatePanel>
                <div class="row espaciosupBoton">
                    <div class="col-md-2">
                        <asp:Button ID="BtnModificar" CssClass="boton " runat="server" ToolTip="Modifica los datos de la actividad extraescolar" OnClick="BtnModificar_Click" Text="Modificar" />
                    </div>
                     <div class="col-md-10">
                        <asp:Button ID="BtnCancelar" CssClass="boton" runat="server" ToolTip="Vuelve a la página anterior" OnClick="BtnCancelar_Click" Text="Cancelar" />
                    </div>
                </div>
            </div>
        </form>
        <div class="clr"></div>
    </div>
</asp:Content>
