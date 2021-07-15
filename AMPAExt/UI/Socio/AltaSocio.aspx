<%@ Page Title="Alta de empresa extraescolar" Language="C#" MasterPageFile="~/Master/SiteSocio.Master" AutoEventWireup="true" CodeBehind="AltaSocio.aspx.cs" Inherits="AMPAExt.UI.Socio.AltaSocio" %>

<%@ Register TagPrefix="uc" TagName="PanelInformativo" Src="~/UI/Controles/panelInformacion.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--content midd-->
    <div id="content_midd">
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            <h1 class="tituloCentral">Nuevo socio de AMPA</h1>
            <uc:PanelInformativo ID="PanelInfo" runat="server"></uc:PanelInformativo>
            <p class="espacioleft">Los campos marcados con <span class="errorValid">*</span> son obligatorios</p>
            <div class="espacioleft">
                <h1 style="border-bottom: dotted; border-width: 1px; width: 80%; border-color: #316074; padding-top: 1.5em">Padres/tutores </h1>
                <div class="row">
                    <div class="col-md-3">
                        <label for="rdIndividual">Responden de forma individual:</label>
                    </div>
                    <div class="col-md-2">
                        <asp:RadioButton ID="rdIndividualNo" runat="server" CssClass="rButtonNombre" ToolTip="Indicador de si los padres/tutores responden de forma individual: No" Text="NO" Checked="true" GroupName="rdIndividual"></asp:RadioButton>
                    </div>
                    <div class="col-md-6">
                        <asp:RadioButton ID="rdIndividualSi" runat="server" CssClass="rButtonNombre" ToolTip="Indicador de si los padres/tutores responden de forma individual: Sí" Text="Sí" GroupName="rdIndividual"></asp:RadioButton>
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
                        <label for="txtNombre1">Nombre <span style="color: red">*</span>: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtNombre1" runat="server" ToolTip="Nombre del padre/tutor 1" CssClass="txtObligatorio"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblNombre1" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtApellido1_1">Primer apellido <span style="color: red">*</span>: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtApellido1_1" ToolTip="Primer apellido del usuario del padre/tutor 1" runat="server" CssClass="txtObligatorio"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblApellido1_1" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtApellido2_1">Segundo apellido: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtApellido2_1" ToolTip="Segundo apellido del padre/tutor 1" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="cmbTipoDocumento1">Tipo documento<span style="color: red">*</span>:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:DropDownList ID="cmbTipoDocumento1" DataTextField="NOMBRE" DataValueField="ID_TIPO_DOCUMENTO" runat="server" ToolTip="Seleccione el tipo de documento del padre/tutor 1" CssClass="txtObligatorio" />
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblTipoDocumento1" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtNumDocumento1">Número de documento<span style="color: red">*</span>:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtNumDocumento1" ToolTip="Número de documento del padre/tutor 1" runat="server" CssClass="txtObligatorio"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblNumDocumento1" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtTelefono1">Teléfono <span style="color: red">*</span>: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtTelefono1" TextMode="Phone" ToolTip="Teléfono del padre/tutor1" runat="server" CssClass="txtObligatorio"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblTelefono1" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtemail1">Correo electrónico <span style="color: red">*</span>:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtemail1" TextMode="Email" ToolTip="Correo electrónico del padre/tutor 1" runat="server" CssClass="txtObligatorio"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblEmail1" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
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
                        <asp:TextBox ID="txtNombre2" runat="server" ToolTip="Nombre del padre/tutor 2"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtApellido1_2">Primer apellido: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtApellido1_2" ToolTip="Primer apellido del padre/tutor 2" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtApellido2_2">Segundo apellido: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtApellido2_2" ToolTip="Segundo apellido del padre/tutor 2" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="cmbTipoDocumento2">Tipo documento:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:DropDownList ID="cmbTipoDocumento2" DataTextField="NOMBRE" DataValueField="ID_TIPO_DOCUMENTO" runat="server" ToolTip="Seleccione el tipo de documento del padre/tutor 2" />
                    </div>
                    <div class="col-md-2"></div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtNumDocumento2">Número de documento:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtNumDocumento2" ToolTip="Número de documento del padre/tutor 2" runat="server" ></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtTelefono2">Teléfono: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtTelefono2" TextMode="Phone" ToolTip="Teléfono del padre/tutor 2" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtemail2">Correo electrónico:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtemail2" TextMode="Email" ToolTip="Correo electrónico del padre/tutor 2" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2"></div>
                </div>

                <h1 style="border-bottom: dotted; border-width: 1px; width: 80%; border-color: #316074; padding-top: 1.5em">Hijos/alumnos del centro escolar</h1>
                 <asp:Label ID="lblHijos" runat="server" Text="Es obligatorio incluir al menos un hijo/alumno para ser socio" CssClass="errorValid" Visible="false"></asp:Label>

                 <div class="row">
                    <div class="col-md-3">
                        <label for="txtNombre">Nombre <span style="color: red">*</span>: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtNombre" runat="server" ToolTip="Nombre del hijo" CssClass="txtObligatorio"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblNombre" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtApellido1">Primer apellido <span style="color: red">*</span>: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtApellido1" ToolTip="Primer apellido del hijo" runat="server" CssClass="txtObligatorio"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblApellido1" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="txtApellido2">Segundo apellido: </label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtApellido2" ToolTip="Segundo apellido del hijo" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblApellido2" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                   <div class="row">
                    <div class="col-md-3">
                        <label for="txtFecha">Fecha nacimiento<span style="color: red">*</span>:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtFecha" TextMode="Date" ToolTip="Fecha de nacimiento" runat="server" CssClass="txtObligatorio"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblFecha" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                <asp:UpdatePanel runat="server" ID="upClase">
<ContentTemplate>
                <div class="row">
                    <div class="col-md-3">
                        <label for="cmbCurso">Curso <span style="color: red">*</span>:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:DropDownList ID="cmbCurso" DataTextField="NOMBRE" DataValueField="ID_CURSO" runat="server" ToolTip="Seleccione el curso" CssClass="txtObligatorio" AutoPostBack="true" OnSelectedIndexChanged="cmbCurso_SelectedIndexChanged" />
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblCurso" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
                   <div class="row">
                    <div class="col-md-3">
                        <label for="cmbClase">Clase <span style="color: red">*</span>:</label>
                    </div>
                    <div class="col-md-7">
                        <asp:DropDownList ID="cmbClase" runat="server" ToolTip="Seleccione la clase" Enabled="false" CssClass="txtObligatorio" />
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblClase" runat="server" Text="Obligatorio" CssClass="errorValid" Visible="false"></asp:Label>
                    </div>
                </div>
    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cmbCurso" />
                    </Triggers>
                    </asp:UpdatePanel>

                <div class="row">
                    <div class="col-md-12" style="float: left!important; vertical-align: middle;">
                        <asp:LinkButton CssClass="botonForm" ID="BtnAnadirHijo" runat="server" ToolTip="Añade un hijo como socio de AMPA" OnClick="BtnAnadirHijo_Click"><img src="../../Content/Imagenes/hijos.png" style="padding-right:10px; max-width:50px" /> Añadir hijo/alumno</asp:LinkButton>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3"></div>
                    <div class="col-md-6">
                        <asp:GridView ID="gvHijo" runat="server" AllowSorting="false" AutoGenerateColumns="false" BorderStyle="Outset" BorderWidth="1"
                            OnRowCreated="gv_RowCreated" OnRowCommand="gvHijo_RowCommand" EmptyDataRowStyle-VerticalAlign="Middle"
                            EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="No hay registros disponibles." Width="100%" CssClass="Grid" AccessKey="G">
                            <Columns>
                                <asp:TemplateField HeaderText=" " ShowHeader="False">
                                    <ItemTemplate>
                                        <HeaderStyle Width="50" />
                                        <asp:ImageButton ID="imgBorrar" runat="server" CommandArgument='<%# Eval("ID_ALUMNO") %>' CommandName="Baja" CausesValidation="false" ToolTip="Baja del socio" OnClientClick="if(!confirm('Se va a borrar el hijo/alumno para el socio, ¿desea continuar?')){return false;}" ImageUrl="~/Content/Imagenes/trash.png" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="center" Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre del alumno" ShowHeader="true" AccessibleHeaderText="Nombre"
                                    ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbnombre" runat="server" Text='<%# string.Concat(Eval("NOMBRE"), " ", Eval("APELLIDO1"), " ", Eval("APELLIDO2")) %>' ToolTip='<%# string.Concat(Eval("NOMBRE"), " ", Eval("APELLIDO1"), " ", Eval("APELLIDO2")) %>'
                                            CssClass="acortaLabel largo" Width="250">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Fecha de nacimiento" ShowHeader="true" AccessibleHeaderText="Fecha"
                                    ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbFecha" runat="server" Text='<%# DateTime.Parse(Eval("FECHA_NACIMIENTO").ToString()).ToShortDateString().ToString() %>' ToolTip='<%# DateTime.Parse(Eval("FECHA_NACIMIENTO").ToString()).ToShortDateString().ToString() %>'
                                            CssClass="acortaLabel corto" Width="100">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Curso" ShowHeader="true" AccessibleHeaderText="Curso"
                                    ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbCurso" runat="server" Text='<%# string.Concat(Eval("CURSO_CLASE.CURSO.NOMBRE"), " ", Eval("CURSO_CLASE.CLASE.NOMBRE")) %>'
                                           CssClass="acortaLabel corto" Width="150">
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
                        <asp:Button ID="btnAceptar" CssClass="boton " runat="server" ToolTip="Da de alta un nuevo socio para la AMPA" OnClick="BtnAceptar_Click" Text="Aceptar" />
                    </div>
                </div>
            </div>
        </form>
        <div class="clr"></div>
    </div>
</asp:Content>
