using System;
using System.Collections.Generic;
using AMPA.Modelo;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMPAExt.UI.Extraescolar
{
    public partial class GestionUsuariosExt2 : PageBase
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
        private Comun.FiltroUsuario FiltroInicial
        {
            get { return (Session["FiltroUsuario"] == null) ? null : (Comun.FiltroUsuario)Session["FiltroUsuario"]; }
            set { Session["FiltroUsuario"] = value; }
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
                    CargarTipoDocumento();
                    if (FiltroInicial != null)
                        CargarFiltro();
                    Session["IdSolicitud"] = null;
                    CargarGrid();
                }
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".Page_load()", ex);
                _Page.Error(_MensajeError);
            }
        }

        #endregion

        #region Métodos privados
        /// <summary>
        /// Carga el combo de la interface con el listado de números de documento del sistema
        /// </summary>
        private void CargarTipoDocumento()
        {
            try
            {
                cmbTipoDocumento.DataSource = ((PageBase)Page).NegTablasMaestras.GetTiposDocumento();
                cmbTipoDocumento.DataBind();
                cmbTipoDocumento.Items.Insert(0, new ListItem("-- Seleccione --", ""));
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al obtener el listado de tipos de documento de la tabla maestra.", ex);
                ScriptManager.RegisterStartupScript(Page, GetType(), "Usuario", "alert('Se ha producido un error al obtener el listado de tipos de documento');", true);
            }

        }

        #endregion

        /// <summary>
        /// Evento producido al pulsar sobre el botón limpiar del filtro. Carga el grid como en el estado inicial
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                cmbTipoDocumento.ClearSelection();
                txtNumDoc.Text = string.Empty;
                txtNombre.Text = string.Empty;
                txtTelefono.Text = string.Empty;
                Session.Remove("FiltroUsuario");

                //gridPaginacion.Paginacion.CampoOrdenacion = "FECHA_SOLICITUD";
                SortExpression = "NOMBRE";
                //gridPaginacion.Paginacion.Orden = PaginacionCls.Asc;
                SortDirection = Comun.TipoDatos.Ordenacion.ASC.ToString();
                //gridPaginacion.Paginacion.RegistroInicial = 0;
                //gridPaginacion.Paginacion.PaginaActual = 1;
                CargarGrid();
              
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al limpiar los datos de filtro", ex);
                ScriptManager.RegisterStartupScript(Page, GetType(), "GestorUsuario", "alert('Se ha producido un error al limpiar los datos del filtro');", true);
            }
        }
        /// <summary>
        /// Evento producido al pulsar sobre el botón filtrar del filtro. Carga el grid con los criterios indicados en el filtro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                //gridPaginacion.Paginacion.RegistroInicial = 0;
                //gridPaginacion.Paginacion.PaginaActual = 1;
                //if (FiltroInicial != null)
                //    FiltroInicial.Paginacion = gridPaginacion.Paginacion;
                CargarGrid();
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al filtrar", ex);
                ScriptManager.RegisterStartupScript(Page, GetType(), "GestorUsuario", "alert('Se ha producido al buscar en el listado');", true);
            }
        }
       
       



        #region Eventos del grid
        /// <summary>
        /// Recarga el grid después de la paginación.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridPaginacion_RecargarGrid(object sender, EventArgs e)
        {
            //FiltroInicial.Paginacion = gridPaginacion.Paginacion;
            //CargarGrid();
        }
        /// <summary>
        /// Procedimiento de la grid, al crear una fila
        /// </summary>
        protected void gvUsuarios_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.background='#FFF0A2'");
                e.Row.Attributes.Add("onmouseout", "this.style.background='#FFFFFF'");
            }
            //if (e.Row.RowType == DataControlRowType.Header)
            //{
            //    string[] auxValores = new string[7] { "NOMBRE", "ID_SOLICITUD", "RAE_TIPOS_SOLICITUD.TIPO_SOLICITUD", "TIPO_DOCUMENTO,NUMERO_DOCUMENTO", "NOMBRE", "NOMBRE_SOLI", "RAE_ACTORES1.ACTOR" };

            //    string[] auxTooltip = new string[7] { "Ordena por la fecha de creación de la solicitud", "Ordena por el identificador de la solicitud", "Ordena por el tipo de solicitud", "Ordena por el tipo y número de documento del productor", "Ordena por el nombre del productor", "Ordena por el nombre del solicitante", "Ordena por el origen de la solicitud" };
            //    for (int i = 0; i < auxValores.Length; i++)
            //    {
            //        if (SortExpression.ToUpper().Trim() == auxValores[i] ||
            //            (FiltroInicial.Paginacion != null && FiltroInicial.Paginacion.CampoOrdenacion.Trim() == auxValores[i]))
            //        {
            //            using (System.Web.UI.HtmlControls.HtmlGenericControl span = new System.Web.UI.HtmlControls.HtmlGenericControl())
            //            {
            //                if (FiltroInicial.Paginacion != null && !string.IsNullOrEmpty(FiltroInicial.Paginacion.CampoOrdenacion))
            //                {
            //                    SortExpression = FiltroInicial.Paginacion.CampoOrdenacion;
            //                    SortDirection = FiltroInicial.Paginacion.Orden;
            //                }

            //                if (SortDirection == PaginacionCls.Asc)
            //                    span.Attributes["class"] = "glyphicon glyphicon-arrow-up";
            //                else
            //                    span.Attributes["class"] = "glyphicon glyphicon-arrow-down";

            //                span.Attributes["aria-hidden"] = "true";
            //                span.ID = "ico1";
            //                // Añadir icono a la cabecera
            //                e.Row.Cells[i + 1].Controls.AddAt(0, span);
            //            }
            //        }

            //        e.Row.Cells[i + 1].ToolTip = auxTooltip[i];
            //    }
            //}
        }

        /// <summary>
        /// Procedimiento de la grid, al ejecutar un comando
        /// </summary>
        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (gvUsuarios.Rows.Count > 0)
            {
                //Se recupera el identificador del usuario
                int idUsuario = int.Parse(e.CommandArgument.ToString());
                switch (e.CommandName)
                {
                    case "Modificar":
                        Session["IdUsuario"] = e.CommandArgument.ToString();
                        Response.Redirect(_Page.MasterBase.RelativeURL + "/Administracion/ModificarUsuario.aspx?grid=S", false);
                        break;
                    case "Baja":
                        try
                        {
                            if (idUsuario == MasterBase.DatosSesionLogin.IdUsuario)
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", "alert('El usuario no puede darse de baja a sí mismo.');", true);
                                break;
                            }
                            else
                            {
                                //Se borra al usuario de la AMPA
                                if (!NegUsuario.BajaUsuarioAMPA(idUsuario, MasterBase.DatosSesionLogin.IdEmpresa))
                                {
                                    Comun.Log.TrazaLog.Error("No se ha podido dar de baja el usuario " + idUsuario.ToString());
                                    Error("Se ha producido un error al intentar dar de baja al usuario para la AMPA");
                                    return;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Comun.Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".gvUsuarios_RowCommand().Eliminar(IdUsuario: " + idUsuario.ToString() + "). Descripcion: " + ex.ToString());
                            ScriptManager.RegisterStartupScript(Page, GetType(), "gestor", "alert('Se ha producido un error al dar de baja al usuario');", true);
                            return;
                        }
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", "alert('El usuario ha sido dado de baja correctamente.');", true);
                        //Se carha el grid de nuevo para que desaparezca el usuario borrado
                        CargarGrid();
                        break;
                }
            }
        }

        /// <summary>
        /// Procedimiento de la grid, ordenación de los datos
        /// </summary>
        protected void gvUsuarios_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection = GetSortDirection(e.SortExpression).Equals("Ascending") ? Comun.TipoDatos.Ordenacion.ASC.ToString(): Comun.TipoDatos.Ordenacion.DESC.ToString();
            SortExpression = e.SortExpression;

            //gridPaginacion.Paginacion.PaginaActual = 1;
            //gridPaginacion.Paginacion.RegistroInicial = 0;

            FiltroInicial.CampoOrdenacion = ViewState["SortExpression"].ToString();
            FiltroInicial.OdenacionAscendente = (ViewState["SortDirection"].ToString() == Comun.TipoDatos.Ordenacion.ASC.ToString());
            CargarGrid();
        }

        #endregion

        //    #region Métodos privados

        /// <summary>
        /// Asigna los valores del filtro
        /// </summary>
        /// <returns></returns>
        private Comun.FiltroUsuario SetFiltro()
        {
            Comun.FiltroUsuario filtro = new Comun.FiltroUsuario();
            try
            {
                filtro.Vacio = true;
                if (cmbTipoDocumento.SelectedIndex > 0)
                {
                    filtro.Vacio = false;
                    filtro.IdTipoDocumento = int.Parse(cmbTipoDocumento.SelectedValue);
                }

                if (!string.IsNullOrEmpty(txtNumDoc.Text.Trim()))
                {
                    filtro.Vacio = false;
                    filtro.NumeroDocumento = txtNumDoc.Text.Trim();
                }

                if (!string.IsNullOrEmpty(txtNombre.Text.Trim()))
                {
                    filtro.Vacio = false;
                    filtro.Nombre = txtNombre.Text.Trim();
                }

                if (!string.IsNullOrEmpty(txtTelefono.Text.Trim()))
                {
                    filtro.Vacio = false;
                    filtro.Telefono = txtTelefono.Text.Trim();
                }

                //if (!filtro.Vacio)
                //    imgCirculo.ImageUrl = "~/Content/Imagenes/green_circle.JPG";
                //else
                //{
                //    if (FiltroInicial != null & !IsPostBack)
                //        CargarFiltro();
                //    else
                //        imgCirculo.ImageUrl = "~/Content/Imagenes/gray_circle.JPG";
                //}

                //if (FiltroInicial != null)
                //    filtro.Paginacion = FiltroInicial.Paginacion;

                //filtro.Pagina = this.GetType().Name;
                FiltroInicial = filtro;
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".SetFiltro(). Descripcion; ", ex);
                _Page.Error("Ha ocurrido un error al establecer el filtro de la página");
            }
            return filtro;
        }
        /// <summary>
        /// Asigna los valores del filtro en función de lo que venga de sesion
        /// </summary>
        private void CargarFiltro()
        {
            Comun.FiltroUsuario filtro = FiltroInicial;
            try
            {
                if (filtro.IdTipoDocumento > 0)
                    cmbTipoDocumento.SelectedValue = filtro.IdTipoDocumento.ToString();

                if (!string.IsNullOrEmpty(filtro.NumeroDocumento))
                    txtNumDoc.Text = filtro.NumeroDocumento;

                if (!string.IsNullOrEmpty(filtro.Nombre))
                    txtNombre.Text = filtro.Nombre;
                if (!string.IsNullOrEmpty(filtro.Telefono))
                    txtTelefono.Text = filtro.Telefono;
                //if (filtro.Paginacion != null)
                //    gridPaginacion.Paginacion = filtro.Paginacion;
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".CargarFiltro(). Descripcion; ", ex);
                _MensajeError = "Ha ocurrido un error al establecer el filtro de la página";
                throw;
            }
        }
        /// <summary>
        /// Procedimiento de la carga el listado
        /// </summary>
        private void CargarGrid()
        {
            try
            {
                //PaginacionCls paginacion = gridPaginacion.Paginacion;

                List<USUARIO_AMPA> listado = new List<USUARIO_AMPA>();
                Comun.FiltroUsuario filtro = new Comun.FiltroUsuario();
                filtro = SetFiltro();

                if (string.IsNullOrEmpty(filtro.CampoOrdenacion))
                {
                    filtro.CampoOrdenacion = "NOMBRE";
                    filtro.OdenacionAscendente = true;
                }

                listado = NegUsuario.GetUsuariosByAMPA(MasterBase.DatosSesionLogin.IdEmpresa, filtro);

               // solicitudes = _Page.NegocioSolicitud.GetSolicitudesProductor(RAEE.Comun.TipoDatos.Aplicacion, filtro, idScrap, ref paginacion);
              //  gridPaginacion.Paginacion = paginacion;

                gvUsuarios.DataSource = listado;
                gvUsuarios.DataBind();
                //gridPaginacion.MostrarPaginacion = paginacion.NumeroRegistrosTotales > 0; //Mostramos u ocultamos la paginación dependiendo del resultado del grid.            

                //gridPaginacion.ActualizarLiterales();
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al cargar el grid", ex);
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
                if (SortDirection == Comun.TipoDatos.Ordenacion.ASC.ToString())
                    SortDirection = Comun.TipoDatos.Ordenacion.DESC.ToString();
                else if (SortDirection == Comun.TipoDatos.Ordenacion.DESC.ToString())
                    SortDirection = Comun.TipoDatos.Ordenacion.ASC.ToString();
                return SortDirection;
            }
            else
            {
                SortExpression = sortExpression;
                return SortDirection;
            }
        }
        //    #endregion
    }
}