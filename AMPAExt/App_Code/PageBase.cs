using System;
using System.Text;
using System.Web.UI;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using AMPAExt.Negocio;
using AMPAExt.Comun;
public class PageBase: System.Web.UI.Page
{
    #region Propiedades
    /// <summary>
    /// Propiedad privada para almacenar el mensaje de error que se quiere visualizar por pantalla
    /// </summary>
    private string _mensajeError = string.Empty;

    /// <summary>
    /// Devuelve la página maestra ya como una MasterPageBase para acceder a todos sus métodos
    /// </summary>
    public MasterPageBase MasterBase
    {
        get { return (MasterPageBase)this.Master; }
    }

    /// <summary>
    /// Mensaje de error a visualizar en la página de error
    /// </summary>
    public string MensajeError
    {
        get
        {
            if (string.IsNullOrEmpty(_mensajeError))
            {
                if (Session["MensajeError"] != null)
                    _mensajeError = Session["MensajeError"].ToString();
                else
                    _mensajeError = "Ha ocurrido un error en la aplicación. Inténtelo de nuevo más tarde.";
            }
            return _mensajeError;
        }
        set
        {
            Session["MensajeError"] = value;
            _mensajeError = value;
        }
    }

    #region Instancias a las clases de negocio
    /// <summary>
    /// Instancia de la clase de negocio de tratamiento de usuarios (privada)
    /// </summary>
    private Administracion _negUsuario;
    /// <summary>
    /// Instancia de la clase de negocio de tratamiento de usuarios
    /// </summary>
    public Administracion NegUsuario
    {
        get
        {
            if (_negUsuario == null)
                _negUsuario = new Administracion();
            return _negUsuario;
        }
    }

    /// <summary>
    /// Instancia de la clase de negocio de tratamiento de empresas extraescolares (privada)
    /// </summary>
    private Extraescolar _negExtraescolar;

    /// <summary>
    /// Instancia de la clase de negocio de tratamiento de empresas extraescolares
    /// </summary>
    public Extraescolar NegExtraescolar
    {
        get
        {
            if (_negExtraescolar == null)
                _negExtraescolar = new Extraescolar();
            return _negExtraescolar;
        }
    }

    /// <summary>
    /// Instancia de la clase de negocio de tratamiento de actividades extraescolares (privada)
    /// </summary>
    private Actividad _negActividad;

    /// <summary>
    /// Instancia de la clase de negocio de tratamiento de actividades extraescolares
    /// </summary>
    public Actividad NegActividad
    {
        get
        {
            if (_negActividad == null)
                _negActividad = new Actividad();
            return _negActividad;
        }
    }

    /// <summary>
    /// Instancia de la clase de negocio de tratamiento de socios (privada)
    /// </summary>
    private Socio _negSocio;

    /// <summary>
    /// Instancia de la clase de negocio de tratamiento de socios 
    /// </summary>
    public Socio NegSocio
    {
        get
        {
            if (_negSocio == null)
                _negSocio = new Socio();
            return _negSocio;
        }
    }

    /// <summary>
    /// Instancia de la clase de negocio de tratamiento de las tablas maestras (privada)
    /// </summary>
    private TablasMaestras _negTablasMaestras;
    /// <summary>
    /// Instancia de la clase de negocio de tratamiento de las tablas maestras
    /// </summary>
    public TablasMaestras NegTablasMaestras
    {
        get
        {
            if (_negTablasMaestras == null)
                _negTablasMaestras = new TablasMaestras();
            return _negTablasMaestras;
        }
    }

    /// <summary>
    /// Instancia de la clase de negocio de tratamiento de datos comunes (privada)
    /// </summary>
    private Comun _negocioComun;
    /// <summary>
    /// Instancia de la clase de negocio de tratamiento de datos
    /// </summary>
    public Comun NegComun
    {
        get
        {
            if (_negocioComun == null)
                _negocioComun = new Comun();
            return _negocioComun;
        }
    }

    #endregion
    #endregion

    //#region Métodos

    //  /// <summary>
    ///// Exporta los datos de un grid a un documento Excel
    ///// </summary>
    ///// <param name="titulo"></param>
    ///// <param name="gvDatos"></param>
    //public void ExportarGridViewGenerico(string titulo, GridView gvDatos)
    //{
    //    List<int> filasSaltadas = new List<int>();
    //    ExportarGridViewGenerico(titulo, gvDatos, filasSaltadas);
    //}

    ///// <summary>
    ///// Exporta los datos de un grid a un documento Excel
    ///// </summary>
    ///// <param name="titulo"></param>
    ///// <param name="gvDatos"></param>
    ///// <param name="filasSaltadas">En los grids de intranet se muestra una fila y en los de extranet se muestra otra</param>
    //public void ExportarGridViewGenerico(string titulo, GridView gvDatos, List<int> filasSaltadas)
    //{
    //    int numMaxFilasExel = 0;
    //    int cont = 0;

    //    try
    //    {
    //        int.TryParse(System.Configuration.ConfigurationManager.AppSettings["numMaxFilasExel"], out numMaxFilasExel);

    //        if (gvDatos != null && gvDatos.Rows != null && gvDatos.Rows.Count > 0)
    //        {
    //            if (gvDatos.Rows.Count <= numMaxFilasExel)
    //            {
    //                DataTable dt = new DataTable(titulo);

    //                foreach (TableCell cell in gvDatos.HeaderRow.Cells)
    //                {
    //                    if (filasSaltadas != null)
    //                    {
    //                        if (!filasSaltadas.Contains(cont))
    //                        {
    //                            dt.Columns.Add(HttpUtility.HtmlDecode(cell.Text));
    //                            cell.BackColor = gvDatos.HeaderStyle.BackColor;
    //                        }
    //                    }
    //                    else
    //                    {
    //                        dt.Columns.Add(HttpUtility.HtmlDecode(cell.Text));
    //                        cell.BackColor = gvDatos.HeaderStyle.BackColor;
    //                    }

    //                    cont++;
    //                }

    //                foreach (GridViewRow row in gvDatos.Rows)
    //                {
    //                    cont = 0;
    //                    int fila = 0;
    //                    dt.Rows.Add();

    //                    foreach (TableCell cell in row.Cells)
    //                    {
    //                        if (filasSaltadas != null)
    //                        {
    //                            if (!filasSaltadas.Contains(cont))
    //                            {
    //                                dt.Rows[dt.Rows.Count - 1][fila] = HttpUtility.HtmlDecode(cell.Text);
    //                                fila++;

    //                                if (row.RowIndex % 2 == 0)
    //                                {
    //                                    cell.BackColor = gvDatos.AlternatingRowStyle.BackColor;
    //                                }
    //                                else
    //                                {
    //                                    cell.BackColor = gvDatos.RowStyle.BackColor;
    //                                }
    //                                cell.CssClass = "textmode";
    //                            }
    //                        }
    //                        else
    //                        {
    //                            dt.Rows[dt.Rows.Count - 1][fila] = HttpUtility.HtmlDecode(cell.Text);
    //                            fila++;

    //                            if (row.RowIndex % 2 == 0)
    //                            {
    //                                cell.BackColor = gvDatos.AlternatingRowStyle.BackColor;
    //                            }
    //                            else
    //                            {
    //                                cell.BackColor = gvDatos.RowStyle.BackColor;
    //                            }
    //                            cell.CssClass = "textmode";
    //                        }

    //                        cont++;
    //                    }
    //                }
    //                using ( XLWorkbook wb = new XLWorkbook())
    //                {
    //                    var ws = wb.Worksheets.Add(dt, titulo.ToUpper());
    //                    ws.Tables.Table(0).ShowAutoFilter = false;
    //                    ws.Row(1).InsertRowsAbove(1);
    //                    foreach (IXLRow row in ws.Rows())
    //                    {
    //                        row.Style.Fill.BackgroundColor = XLColor.White;
    //                        foreach (IXLCell cell in row.Cells())
    //                        {
    //                            if (row.RowNumber() > 2)
    //                            {
    //                                if (row.RowNumber() % 2 == 0)
    //                                    cell.Style.Fill.BackgroundColor = XLColor.FromColor(gvDatos.AlternatingRowStyle.BackColor);
    //                                else
    //                                    cell.Style.Fill.BackgroundColor = XLColor.FromColor(gvDatos.RowStyle.BackColor);

    //                                cell.Style.Border.RightBorder = XLBorderStyleValues.Thin;
    //                            }
    //                            else if (row.RowNumber() == 2)
    //                            {
    //                                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
    //                                cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#3AC0F2");
    //                                cell.Style.Border.RightBorder = XLBorderStyleValues.Thin;
    //                            }
    //                            cell.DataType = XLCellValues.Text;
    //                        }
    //                        row.AdjustToContents();
    //                    }

    //                    Response.Clear();
    //                    Response.Buffer = true;
    //                    Response.Charset = "UTF-8";
    //                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    //                    Response.AddHeader("content-disposition", "attachment;filename=" + titulo + ".xlsx");

    //                    using (MemoryStream MyMemoryStream = new MemoryStream())
    //                    {
    //                        wb.SaveAs(MyMemoryStream);
    //                        MyMemoryStream.WriteTo(Response.OutputStream);
    //                        Response.Flush();
    //                        Response.End();
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "load", "<script type=\"text/javascript\">\n" +
    //                "alert('Se ha excedido el número máximo de registros permitidos, no se pueden exportar más de " + numMaxFilasExel.ToString() + " registros.');\n" +
    //                "<" + "/script>", false);
    //            }
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "load", "<script type=\"text/javascript\">\n" +
    //            "alert('No hay datos a exportar.');\n" +
    //            "<" + "/script>", false);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ///// <summary>
    ///// Limpia el grid de controles no necesarios
    ///// </summary>
    ///// <param name="gv"></param>
    ///// <autor>JHERTFELDER</autor>
    ///// <date>09/10/2010</date>
    //public GridView PrepararEstilosExcel(GridView gv)
    //{
    //    try
    //    {
    //        for (int i = 0; i < gv.Columns.Count; i++)
    //        {
    //            if (gv.HeaderRow.Cells[i].Controls.Count > 0)
    //            {
    //                //Borramos la imagen de la cabecera
    //                gv.HeaderRow.Cells[i].Controls.RemoveAt(0);
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        DllUtils.DllLog.Log.WriteLog(DllUtils.DllLog.Log.TipoLog.LogError, DllUtils.DllLog.Log.TipoAplicacion.Generica, "Excepción: " + ex.Message + ex.StackTrace);
    //    }

    //    return gv;
    //}
    /// <summary>
    /// Ejecuta las instrucciones de javascript pasadas por parámetro
    /// </summary>
    /// <param name="instrucciones">Instrucciones a ejecutar.</param>
    public void Javascript(string instrucciones)
    {
        instrucciones = instrucciones.Replace("\n", "\\n").Replace("'", "\"");
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scriptMensaje", instrucciones, true);
    }

    /// <summary>
    /// Ejecuta las instrucciones de javascript pasadas por parámetro
    /// </summary>
    /// <param name="instrucciones">Instrucciones a ejecutar.</param>
    /// <param name="ventana">Nombre de la ventana</param>
    public void Javascript(string instrucciones, string ventana)
    {
        instrucciones = instrucciones.Replace("\n", "\\n").Replace("'", "\"");
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), ventana, instrucciones, true);
    }



    /// <summary>
    /// Ejecuta las instrucciones de javascript pasadas por parámetro si estás dentro de un updatepanel
    /// </summary>
    /// <param name="instrucciones">Instrucciones a ejecutar.</param>
    /// <param name="ventana">Nombre de la ventana</param>
    /// <param name="upEjecutar">Update panel donde se ejecutará el script</param>
    public void Javascript(string instrucciones, string ventana, UpdatePanel upEjecutrar)
    {
        instrucciones = instrucciones.Replace("\n", "\\n").Replace("'", "\"");
        ScriptManager.RegisterStartupScript(upEjecutrar, upEjecutrar.GetType(), ventana, instrucciones, true);
    }



    /// <summary>
    /// Obtiene la ruta relativa de la página respecto a la raiz del sitio web
    /// </summary>
    /// <returns>String con los subdirectorio a subir para llegar a la raiz del sitio web</returns>
    public string RelativePath()
    {
        string iProf = string.Empty;
        int iFSlash = Request.ServerVariables["SCRIPT_NAME"].IndexOf('/');

        for (int iCS = 0; iCS < Request.ServerVariables["SCRIPT_NAME"].Length; iCS++)
            if (Request.ServerVariables["SCRIPT_NAME"][iCS].Equals('/') && iFSlash != iCS)
                iProf += "../";

        return iProf.Substring(0, (iProf.Length >= 3) ? iProf.Length - 3 : iProf.Length);
    }

    /// <summary>
    /// Descarga un tipo de fichero definido por el tipo  
    /// </summary>
    /// <param name="pagina">página desde la que se realiza la descarga del fichero</param>
    /// <param name="strNombreFichero">Nombre del fichero a descargar</param>
    /// <param name="strFichero">Cadena a convertir en fichero</param>
    /// <param name="tipo">Tipo de fichero al se que se va a convertir y que se va a descargar</param>
    public static void DescargarFichero(Page pagina, string strNombreFichero, string strFichero, TipoDatos.TiposMIME tipo)
    {
        string NombreArchivo = "";
        string strTipoMIME = "";

        pagina.Response.Clear();
        pagina.Response.ClearHeaders();
        pagina.Response.ClearContent();

        if (tipo == TipoDatos.TiposMIME.XML)
        {
            NombreArchivo = strNombreFichero + ".xml";
            strTipoMIME = "text/xml";
        }
        else if (tipo == TipoDatos.TiposMIME.XSD)
        {
            NombreArchivo = strNombreFichero + ".xsd";
            strTipoMIME = "text/xsd";
        }
        else if (tipo == TipoDatos.TiposMIME.PDF)
        {
            NombreArchivo = strNombreFichero + ".pdf";
            strTipoMIME = "application/pdf";
        }

        char[] charArray = strFichero.ToCharArray();
        byte[] byteArray = Encoding.Default.GetBytes(charArray);
        pagina.Response.ContentType = strTipoMIME;
        pagina.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + NombreArchivo + "\"");
        pagina.Response.Flush();
        pagina.Response.BinaryWrite(byteArray);
        pagina.Response.End();
    }

 
    public enum tipoAlert
    {
        danger,
        warning,
        success
    }

    /// <summary>
    /// Convierte un List<T> genérico en un DataTable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data">Listado genérico a convertir en datatable</param>
    /// <returns>DataTable con los datos del List<T> </returns>
    public DataTable ToDataTable<T>(IList<T> data)
    {
        PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
        DataTable table = new DataTable();
        List<int> indexColumnsValids = new List<int>();
        for (int i = 0; i < props.Count; i++)
        {
            PropertyDescriptor prop = props[i];
            string name = Normalizar(prop.Name);
            try
            {
                table.Columns.Add(name, prop.PropertyType);
                indexColumnsValids.Add(i);
            }
            catch
            {
                try
                {
                    table.Columns.Add(name, Nullable.GetUnderlyingType(prop.PropertyType));
                    indexColumnsValids.Add(i);
                }
                catch { }
            }
        }
        object[] values = new object[indexColumnsValids.Count];
        foreach (T item in data)
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = props[indexColumnsValids[i]].GetValue(item);
            }
            table.Rows.Add(values);
        }
        return table;
    }

    private string Normalizar(string name)
    {
        string resultado = string.Empty;
        if (!string.IsNullOrEmpty(name))
        {
            resultado = name.Trim();
            for (int i = 0; i < resultado.Length; i++)
            {
                char letra = resultado[i];
                if (i == 0)
                {
                    resultado += char.ToUpper(letra);
                }
                else if (char.IsUpper(letra))
                {
                    resultado += " " + letra;
                }
                else
                {
                    resultado += letra;
                }
            }
        }
        return resultado;
    }

    /// <summary>
    /// Se redirige a la página genérica de error con el mensaje genérico
    /// </summary>
    public void Error()
    {
        MensajeError = string.Empty;
        Response.Redirect("~/UI/Error.aspx", false);
    }

    /// <summary>
    /// Se redirige a la página genérica de error con el mensaje indicado por parámetro
    /// </summary>
    /// <param name="mensaje">Mensaje a mostrar en la página de error</param>
    public void Error(string mensaje)
    {
        MensajeError = mensaje;
        Response.Redirect("~/UI/Error.aspx", false);

    }

}