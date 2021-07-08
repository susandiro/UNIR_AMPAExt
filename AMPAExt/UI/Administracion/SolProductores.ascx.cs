using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using RAEE.Modelo;
using RAEE.Comun.Clases;
using RAEE.Comun;

/// <summary>
/// Control para cargar la lista de solicitudes de productor
/// </summary>
public partial class UI_Gestion_Controles_SolProductores : System.Web.UI.UserControl
{
    #region propiedades privadas
    /// <summary>
    /// Mensaje de error generado
    /// </summary>
    private string _MensajeError = string.Empty;
    /// <summary>
    /// Página base en la que se encuentra la página actual
    /// </summary>
    private PageBase _Page { get { return Page as PageBase; } }

    /// <summary>
    /// Filtro guardado en sesión para conservar las búsquedas al volver de un detalle
    /// </summary>
    private FiltroSolicitudes FiltroInicial
    {
        get { return (Session[TipoDatos.FiltroGestorTareasProductores] == null) ? null : (FiltroSolicitudes)Session[TipoDatos.FiltroGestorTareasProductores]; }
        set { Session[TipoDatos.FiltroGestorTareasProductores] = value; }
    }

    #endregion

    #region propiedades públicas
    /// <summary>
    /// Propiedad de la expresión de la ordenación
    /// </summary>
    public string SortExpression
    {
        get { return (ViewState["SortExpression"] == null ? string.Empty : ViewState["SortExpression"].ToString()); }
        set { ViewState["SortExpression"] = value; }
    }

    /// <summary>
    /// Propiedad del sentido de la ordenación
    /// </summary>
    public string SortDirection
    {
        get { return (ViewState["SortDirection"] == null ? string.Empty : ViewState["SortDirection"].ToString()); }
        set { ViewState["SortDirection"] = value; }
    }
    #endregion

    #region Eventos
    /// <summary>
    /// Evento producido al cargar la página
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                CargarOrigen();
                CargarTipoDocumento();
                CargarTipoSolicitud();
                if (FiltroInicial != null)
                    CargarFiltro();
                Session["IdSolicitud"] = null;
                CargarGrid();
            }
        }
        catch (Exception ex)
        {
            MasterPageBase.TrazaLog.Error("Error en " + this.GetType().FullName + ".Page_load()", ex);
            _Page.Error(_MensajeError);
        }
    }
    /// <summary>
    /// Evento producido al pulsar sobre el botón limpiar del filtro. Carga el grid como en el estado inicial
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        try
        {
            tbIdSol.Text = string.Empty;
            tbSolicitante.Text = string.Empty;
            ddOrigen.ClearSelection();
            ddTipoDoc.ClearSelection();
            tbDocumento.Text = string.Empty;
            ctrProductor.LimpiarProductor();
            ddTipoSolicitud.ClearSelection();
            tbFechaDesde.Text = string.Empty;
            tbFechaHasta.Text = string.Empty;

            Session.Remove(TipoDatos.FiltroGestorTareasProductores);

            gridPaginacion.Paginacion.CampoOrdenacion = "FECHA_SOLICITUD";
            SortExpression = "FECHA_SOLICITUD";
            gridPaginacion.Paginacion.Orden = PaginacionCls.Asc;
            SortDirection = PaginacionCls.Asc;
            gridPaginacion.Paginacion.RegistroInicial = 0;
            gridPaginacion.Paginacion.PaginaActual = 1;
            CargarGrid();
            //SRS: Ya se actualiza la paginación al ActualizarLiterales dentro de la carga del grid
            //gridPaginacion.ActualizarPaginacion();
        }
        catch (Exception ex)
        {
            MasterPageBase.TrazaLog.Error("Error en " + this.GetType().FullName + ".btnLimpiar_Click()", ex);
            _Page.Error(_MensajeError);
        }
    }
    /// <summary>
    /// Evento producido al pulsar sobre el botón filtrar del filtro. Carga el grid con los criterios indicados en el filtro
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnFiltrar_Click(object sender, EventArgs e)
    {
        try
        {
            gridPaginacion.Paginacion.RegistroInicial = 0;
            gridPaginacion.Paginacion.PaginaActual = 1;
            if (FiltroInicial != null)
                FiltroInicial.Paginacion = gridPaginacion.Paginacion;
            CargarGrid();
            //SRS: Ya se actualiza la paginación al ActualizarLiterales dentro de la carga del grid
            //gridPaginacion.ActualizarPaginacion();
        }
        catch (Exception ex)
        {
            MasterPageBase.TrazaLog.Error("Error en " + this.GetType().FullName + ".btnFiltrar_click()", ex);
            _Page.Error(_MensajeError);
        }
    }
    /// <summary>
    /// Función que controla que el filtro permanezca visible o colapsado
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Colapsar(object sender, ImageClickEventArgs e)
    {
        bool colapsado;
        if (ViewState["FiltroColapsado"] != null)
        {
            colapsado = (bool)ViewState["FiltroColapsado"];
            colapsado = !colapsado;
            ViewState["FiltroColapsado"] = colapsado;
        }
        else
        {
            colapsado = false;
            ViewState["FiltroColapsado"] = false;
        }

        if (colapsado)
        {
            dvFiltro.Attributes["class"] = "cajafichadatos cajafiltro hide";
            //dvGridProd.Attributes["class"] = "formulario gridSolicitudes";
            buttonExpander.ImageUrl = "~/Content/Imagenes/expand.jpg";
        }
        else
        {
            dvFiltro.Attributes["class"] = "cajafichadatos cajafiltro";
            //dvGridProd.Attributes["class"] = "formulario gridSolicitudProdConFiltro3";
            buttonExpander.ImageUrl = "~/Content/Imagenes/collapse.jpg";
        }
    }
    #endregion

    #region Eventos del grid
    /// <summary>
    /// Recarga el grid después de la paginación.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridPaginacion_RecargarGrid(object sender, EventArgs e)
    {
        FiltroInicial.Paginacion = gridPaginacion.Paginacion;
        CargarGrid();
    }
    /// <summary>
    /// Procedimiento de la grid, al crear una fila
    /// </summary>
    protected void gvSolicitudesProd_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.style.background='#FFF0A2'");
            e.Row.Attributes.Add("onmouseout", "this.style.background='#FFFFFF'");
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            string[] auxValores = new string[7] { "FECHA_SOLICITUD", "ID_SOLICITUD", "RAE_TIPOS_SOLICITUD.TIPO_SOLICITUD", "TIPO_DOCUMENTO,NUMERO_DOCUMENTO", "NOMBRE", "NOMBRE_SOLI", "RAE_ACTORES1.ACTOR" };

            string[] auxTooltip = new string[7] { "Ordena por la fecha de creación de la solicitud", "Ordena por el identificador de la solicitud", "Ordena por el tipo de solicitud", "Ordena por el tipo y número de documento del productor", "Ordena por el nombre del productor", "Ordena por el nombre del solicitante", "Ordena por el origen de la solicitud" };
            for (int i = 0; i < auxValores.Length; i++)
            {
                if (SortExpression.ToUpper().Trim() == auxValores[i] ||
                    (FiltroInicial.Paginacion != null && FiltroInicial.Paginacion.CampoOrdenacion.Trim() == auxValores[i]))
                {
                    using (System.Web.UI.HtmlControls.HtmlGenericControl span = new System.Web.UI.HtmlControls.HtmlGenericControl())
                    {
                        if (FiltroInicial.Paginacion != null && !string.IsNullOrEmpty(FiltroInicial.Paginacion.CampoOrdenacion))
                        {
                            SortExpression = FiltroInicial.Paginacion.CampoOrdenacion;
                            SortDirection = FiltroInicial.Paginacion.Orden;
                        }

                        if (SortDirection == PaginacionCls.Asc)
                            span.Attributes["class"] = "glyphicon glyphicon-arrow-up";
                        else
                            span.Attributes["class"] = "glyphicon glyphicon-arrow-down";

                        span.Attributes["aria-hidden"] = "true";
                        span.ID = "ico1";
                        // Añadir icono a la cabecera
                        e.Row.Cells[i + 1].Controls.AddAt(0, span);
                    }
                }

                e.Row.Cells[i + 1].ToolTip = auxTooltip[i];
            }
        }
    }

    /// <summary>
    /// Procedimiento de la grid, al ejecutar un comando
    /// </summary>
    protected void gvSolicitudesProd_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (gvSolicitudesProd.Rows.Count > 0)
        {
            switch (e.CommandName)
            {
                case "Detalle":
                    //Recuperamos el identificador de la solicitud
                    Session["IdSolicitud"] = e.CommandArgument.ToString();
                    Response.Redirect(_Page.MasterBase.RelativeURL + "UI/Gestion/GestorTareas/DetalleProductor.aspx?grid=S", false);
                    break;
            }
        }
    }

    /// <summary>
    /// Procedimiento de la grid, ordenación de los datos
    /// </summary>
    protected void gvSolicitudesProd_Sorting(object sender, GridViewSortEventArgs e)
    {
        SortDirection = GetSortDirection(e.SortExpression).Equals("Ascending") ? PaginacionCls.Asc : PaginacionCls.Desc;
        SortExpression = e.SortExpression;

        gridPaginacion.Paginacion.PaginaActual = 1;
        gridPaginacion.Paginacion.RegistroInicial = 0;
        gridPaginacion.Paginacion.CampoOrdenacion = ViewState["SortExpression"].ToString();
        gridPaginacion.Paginacion.Orden = ViewState["SortDirection"].ToString();
        FiltroInicial.Paginacion = gridPaginacion.Paginacion;
        CargarGrid();
        //SRS: Ya se actualiza la paginación al ActualizarLiterales dentro de la carga del grid
        //gridPaginacion.ActualizarPaginacion();
    }

    #endregion

    #region Métodos privados
    /// <summary>
    /// Carga el combo de Orígenes de la sección de filtros
    /// </summary>
    private void CargarOrigen()
    {
        try
        {
            var origenes = _Page.NegocioComun.ObtenerActores(RAEE.Comun.TipoDatos.Aplicacion);
            ddOrigen.DataSource = origenes;
            ddOrigen.DataBind();

            ddOrigen.Items.Insert(0, new ListItem("Elija una opción", ""));
        }
        catch (Exception ex)
        {
            MasterPageBase.TrazaLog.Error("Error en " + this.GetType().FullName + ".CargarOrigen()", ex);
            _MensajeError = "Error al cargar la lista de orígenes del filtro";
            throw;
        }
    }
    /// <summary>
    /// Carga el combo de tipo documento del productor buscado
    /// </summary>
    private void CargarTipoDocumento()
    {
        try
        {
            ddTipoDoc.Items.Clear();
            ddTipoDoc.DataSource = _Page.NegocioComun.GetTiposDocumento(TipoDatos.Aplicacion);
            ddTipoDoc.DataBind();
            ddTipoDoc.Items.Insert(0, new ListItem("Elija una opción", ""));
        }
        catch (Exception ex)
        {
            MasterPageBase.TrazaLog.Error("Error en " + this.GetType().FullName + ".CargarTipoDocumento()", ex);
            _MensajeError = "Error al cargar los tipos de documentos";
            throw;
        }
    }
    /// <summary>
    /// Carga el combo de tipo de solicitud
    /// </summary>
    private void CargarTipoSolicitud()
    {
        try
        {
            List<RAE_TIPOS_SOLICITUD> tipoSol = _Page.NegocioComun.GetTiposSolicitud();

            ddTipoSolicitud.DataSource = tipoSol;
            ddTipoSolicitud.DataBind();
            ddTipoSolicitud.Items.Insert(0, new ListItem("Elija una opción", ""));
        }
        catch (Exception ex)
        {
            MasterPageBase.TrazaLog.Error("Error en " + this.GetType().FullName + ".CargarTipoSolicitud()", ex);
            _MensajeError = "Error al cargar los tipos de solicitud";
            throw;
        }
    }
    /// <summary>
    /// Asigna los valores del filtro
    /// </summary>
    /// <returns></returns>
    private FiltroSolicitudes SetFiltro()
    {
        FiltroSolicitudes filtro = new FiltroSolicitudes();
        try
        {
            filtro.Vacio = true;

            if (!string.IsNullOrEmpty(tbIdSol.Text.Trim()))
            {
                filtro.Vacio = false;
                long idSol;
                if (!long.TryParse(tbIdSol.Text, out idSol))
                    idSol = 0;
                filtro.IdSolicitud = idSol;
            }

            if (!string.IsNullOrEmpty(tbSolicitante.Text.Trim()))
            {
                filtro.Vacio = false;
                filtro.Solicitante = tbSolicitante.Text.Trim();
            }

            if (ddOrigen.SelectedIndex > 0)
            {
                filtro.Vacio = false;
                filtro.IdOrigen = int.Parse(ddOrigen.SelectedValue);
            }

            if (ddTipoDoc.SelectedIndex > 0)
            {
                filtro.Vacio = false;
                filtro.TipoDoc = ddTipoDoc.SelectedValue;
            }

            if (!string.IsNullOrEmpty(tbDocumento.Text.Trim()))
            {
                filtro.Vacio = false;
                filtro.Documento = tbDocumento.Text.Trim();
            }

            if (ctrProductor.IdProductor > 0)
            {
                filtro.Vacio = false;
                filtro.IdProductor = ctrProductor.IdProductor;
            }
            else if (!string.IsNullOrEmpty(ctrProductor.NombreProductor))
            {
                filtro.Vacio = false;
                filtro.NombreProductor = ctrProductor.NombreProductor.Trim();
            }

            if (ddTipoSolicitud.SelectedIndex > 0)
            {
                filtro.Vacio = false;
                filtro.IdTipoSol = int.Parse(ddTipoSolicitud.SelectedValue);
            }
            if (!string.IsNullOrEmpty(tbFechaDesde.Text))
            {
                filtro.Vacio = false;
                filtro.FechaDesde = DateTime.Parse(tbFechaDesde.Text);
            }
            if (!string.IsNullOrEmpty(tbFechaHasta.Text))
            {
                filtro.Vacio = false;
                filtro.FechaHasta = DateTime.Parse(tbFechaHasta.Text + " 23:59:59.999");
            }

            if (!filtro.Vacio)
                imgCirculo.ImageUrl = "~/Content/Imagenes/green_circle.JPG";
            else
            {
                if (FiltroInicial != null & !IsPostBack)
                    CargarFiltro();
                else
                    imgCirculo.ImageUrl = "~/Content/Imagenes/gray_circle.JPG";
            }

            if (FiltroInicial != null)
                filtro.Paginacion = FiltroInicial.Paginacion;

            filtro.Pagina = this.GetType().Name;
            FiltroInicial = filtro;
        }
        catch (Exception ex)
        {
            MasterPageBase.TrazaLog.Error("Error en " + this.GetType().FullName + ".SetFiltro(). Descripcion; ", ex);
            _Page.Error("Ha ocurrido un error al establecer el filtro de la página");
        }
        return filtro;
    }
    /// <summary>
    /// Asigna los valores del filtro en función de lo que venga de sesion
    /// </summary>
    private void CargarFiltro()
    {
        FiltroSolicitudes filtro = FiltroInicial;
        try
        {
            if (filtro.IdSolicitud > 0)
                tbIdSol.Text = filtro.IdSolicitud.ToString();

            if (!string.IsNullOrEmpty(filtro.Solicitante))
                tbSolicitante.Text = filtro.Solicitante;

            if (filtro.IdOrigen > 0)
                ddOrigen.SelectedValue = filtro.IdOrigen.ToString();

            if (!string.IsNullOrEmpty(filtro.TipoDoc))
                ddTipoDoc.SelectedValue = filtro.TipoDoc;

            if (!string.IsNullOrEmpty(filtro.Documento))
                tbDocumento.Text = filtro.Documento;

            if (filtro.IdProductor > 0)
                ctrProductor.IdProductor = filtro.IdProductor;
            else if (!string.IsNullOrEmpty(filtro.NombreProductor))
                ctrProductor.NombreProductor = filtro.NombreProductor;

            if (filtro.IdTipoSol > 0)
                ddTipoSolicitud.SelectedValue = filtro.IdTipoSol.ToString();

            if (filtro.FechaDesde != null)
                tbFechaDesde.Text = filtro.FechaDesde.Value.ToShortDateString();

            if (filtro.FechaHasta != null)
                tbFechaHasta.Text = filtro.FechaHasta.Value.ToShortDateString();

            if (filtro.Paginacion != null)
                gridPaginacion.Paginacion = filtro.Paginacion;
        }
        catch (Exception ex)
        {
            MasterPageBase.TrazaLog.Error("Error en " + this.GetType().FullName + ".CargarFiltro(). Descripcion; ", ex);
            _MensajeError = "Ha ocurrido un error al establecer el filtro de la página";
            throw;
        }
    }
    /// <summary>
    /// Procedimiento de la carga de la grid
    /// </summary>
    private void CargarGrid()
    {
        try
        {
            PaginacionCls paginacion = gridPaginacion.Paginacion;
            
            List<RAE_SOLICITUDES> solicitudes = new List<RAE_SOLICITUDES>();
            
            FiltroSolicitudes filtro = new FiltroSolicitudes();
            filtro = SetFiltro();

            if (string.IsNullOrEmpty(paginacion.CampoOrdenacion))
            {
                paginacion.CampoOrdenacion = "FECHA_SOLICITUD";
                SortExpression = "FECHA_SOLICITUD";
                paginacion.Orden = PaginacionCls.Asc;
                SortDirection = PaginacionCls.Asc;
            }

            int idScrap = -1;
            if (_Page.MasterBase.DatosSesionLogin.Perfil.GrupoUsuario.Codigo == RAEE.Comun.TipoDatos.GrupoUsuario.SCR.ToString())
                idScrap = _Page.MasterBase.DatosSesionLogin.IdRAEEPILAS;
            solicitudes = _Page.NegocioSolicitud.GetSolicitudesProductor(RAEE.Comun.TipoDatos.Aplicacion, filtro,idScrap, ref paginacion);
            gridPaginacion.Paginacion = paginacion;

            gvSolicitudesProd.DataSource = solicitudes;
            gvSolicitudesProd.DataBind();
            gridPaginacion.MostrarPaginacion = paginacion.NumeroRegistrosTotales > 0; //Mostramos u ocultamos la paginación dependiendo del resultado del grid.            

            gridPaginacion.ActualizarLiterales();
        }
        catch (Exception ex)
        {
            MasterPageBase.TrazaLog.Error("Error en " + this.GetType().FullName + ".CargarGrid(). Descripcion: ", ex);
            _MensajeError = "Ha ocurrido un error al cargar la lista de solicitudes";
            throw;
        }
    }
    /// <summary>
    /// Procedimiento que cambia el sentido de la ordenación
    /// </summary>
    private string GetSortDirection(string sortExpression)
    {
        if (SortExpression == sortExpression)
        {
            if (SortDirection == PaginacionCls.Asc)
                SortDirection = PaginacionCls.Desc;
            else if (SortDirection == PaginacionCls.Desc)
                SortDirection = PaginacionCls.Asc;
            return SortDirection;
        }
        else
        {
            SortExpression = sortExpression;
            SortDirection = PaginacionCls.Asc;
            return SortDirection;
        }
    }
    #endregion
}