<%@ Page Title="Alta de empresa extraescolar" Language="C#" MasterPageFile="~/Master/SiteSocio.Master" AutoEventWireup="true" CodeBehind="DetalleSocio.aspx.cs" Inherits="AMPAExt.UI.Socio.DetalleSocio" %>

<%@ Register TagPrefix="uc" TagName="PanelInformativo" Src="~/UI/Controles/panelInformacion.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--content midd-->
    <div id="content_midd">
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            <h1 class="tituloCentral">Consulta de socio de AMPA con número de socio <asp:Label ID="lblNumSocio" runat="server" ToolTip="Número de socio"></asp:Label></h1>
            <uc:PanelInformativo ID="PanelInfo" runat="server"></uc:PanelInformativo>
            <div class="espacioleft">
                <h1 style="border-bottom: dotted; border-width: 1px; width: 80%; border-color: #316074; padding-top: 1.5em">Padres/tutores </h1>
                <div class="row">
                    <div class="col-md-3">
                        <label for="rdIndividual">Responden de forma individual:</label>
                    </div>
                    <div class="col-md-2">
                        <asp:RadioButton ID="rdIndividualNo" Enabled="false" runat="server" CssClass="rButtonNombre" ToolTip="Indicador de si los padres/tutores responden de forma individual: No" Text="NO" Checked="true" GroupName="rdIndividual"></asp:RadioButton>
                    </div>
                    <div class="col-md-6">
                        <asp:RadioButton ID="rdIndividualSi" Enabled="false" runat="server" CssClass="rButtonNombre" ToolTip="Indicador de si los padres/tutores responden de forma individual: Sí" Text="Sí" GroupName="rdIndividual"></asp:RadioButton>
                    </div>
                    <div class="col-md-1"></div>
                </div>

                <div class="row">
                    <div class="col-md-1">
                    </div>
                    <div class="col-md-11">
                        <h2 style="border-bottom: dotted; border-width: 1px; width: 80%; border-color: #316074; padding-top: 1.5em">Padre/tutor 1 </h2>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtNombre1">Nombre: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtNombre1" Enabled="false" runat="server" ToolTip="Nombre del padre/tutor 1" ></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtApellido1_1">Primer apellido: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtApellido1_1" Enabled="false"  ToolTip="Primer apellido del usuario del padre/tutor 1" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtApellido2_1">Segundo apellido: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtApellido2_1" Enabled="false"  ToolTip="Segundo apellido del padre/tutor 1" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtTipoDocumento1">Tipo documento:</label>
                    </div>
                    <div class="col-md-7">
                         <asp:TextBox ID="txtTipoDocumento1" Enabled="false" ToolTip="Tipo de documento del padre/tutor 1" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtNumDocumento1">Número de documento:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtNumDocumento1" Enabled="false" ToolTip="Número de documento del padre/tutor 1" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtTelefono1">Teléfono: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtTelefono1" Enabled="false" ToolTip="Teléfono del padre/tutor1" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtemail1">Correo electrónico:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtemail1" Enabled="false" ToolTip="Correo electrónico del padre/tutor 1" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>

                <div class="row">
                    <div class="col-md-1">
                    </div>
                    <div class="col-md-11">
                        <h2 style="border-bottom: dotted; border-width: 1px; width: 80%; border-color: #316074; padding-top: 1.5em">Padre/tutor 2 (Opcional) </h2>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-3">
                        <label for="txtNombre2">Nombre: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtNombre2" Enabled="false" runat="server" ToolTip="Nombre del padre/tutor 2"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtApellido1_2">Primer apellido: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtApellido1_2" Enabled="false" ToolTip="Primer apellido del padre/tutor 2" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtApellido2_2">Segundo apellido: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtApellido2_2" Enabled="false" ToolTip="Segundo apellido del padre/tutor 2" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtTipoDocumento2">Tipo documento:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtTipoDocumento2" Enabled="false" ToolTip="Tipo de documento del padre/tutor 2" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtNumDocumento2">Número de documento:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtNumDocumento2" Enabled="false" ToolTip="Número de documento del padre/tutor 2" runat="server" ></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtTelefono2">Teléfono: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtTelefono2" Enabled="false" ToolTip="Teléfono del padre/tutor 2" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtemail2">Correo electrónico:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtemail2" Enabled="false" ToolTip="Correo electrónico del padre/tutor 2" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>

                <h1 style="border-bottom: dotted; border-width: 1px; width: 80%; border-color: #316074; padding-top: 1.5em">Hijos/alumnos del centro escolar)</h1>
                 <asp:Label ID="lblHijos" runat="server" Text="Es obligatorio incluir al menos un hijo/alumno para ser socio" CssClass="errorValid" Visible="false"></asp:Label>
                <div class="row">
                    <div class="col-md-3"></div>
                    <div class="col-md-6">
                        <asp:GridView ID="gvHijo" runat="server" Enabled ="false"  AllowSorting="false" AutoGenerateColumns="false" BorderStyle="Outset" BorderWidth="1"
                            OnRowCreated="gv_RowCreated" EmptyDataRowStyle-VerticalAlign="Middle"
                            EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles." Width="100%" CssClass="Grid" AccessKey="G">
                            <Columns>
                                <asp:TemplateField HeaderText="Nombre del alumno" ShowHeader="true" AccessibleHeaderText="Nombre"
                                    ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle Width="250" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbnombre" runat="server" Text='<%# string.Concat(Eval("NOMBRE"), " ", Eval("APELLIDO1"), " ", Eval("APELLIDO2")) %>' ToolTip='<%# string.Concat(Eval("NOMBRE"), " ", Eval("APELLIDO1"), " ", Eval("APELLIDO2")) %>'
                                            Width="250">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Fecha de nacimiento" ShowHeader="true" AccessibleHeaderText="Fecha"
                                    ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle Width="250" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbFecha" runat="server" Text='<%# DateTime.Parse(Eval("FECHA_NACIMIENTO").ToString()).ToShortDateString().ToString() %>' ToolTip='<%# DateTime.Parse(Eval("FECHA_NACIMIENTO").ToString()).ToShortDateString().ToString() %>'
                                            Width="250">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Curso" ShowHeader="true" AccessibleHeaderText="Curso"
                                    ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle Width="100" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbCurso" runat="server" Text='<%# string.Concat(Eval("CURSO_CLASE.CURSO.NOMBRE"), " ", Eval("CURSO_CLASE.CLASE.NOMBRE")) %>'
                                            Width="100">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-md-3"></div>
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
