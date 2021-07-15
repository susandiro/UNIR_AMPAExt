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
    #endregion

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
    /// Se redirige a la página genérica de error con el mensaje genérico
    /// </summary>
    public void ErrorGeneral()
    {
        MensajeError = string.Empty;
        Response.Redirect(MasterBase.RelativeURL + "/Error.aspx", false);
    }

    /// <summary>
    /// Se redirige a la página genérica de error con el mensaje indicado por parámetro
    /// </summary>
    /// <param name="mensaje">Mensaje a mostrar en la página de error</param>
    public void ErrorGeneral(string mensaje)
    {
        MensajeError = mensaje;
        Response.Redirect(MasterBase.RelativeURL + "/Error.aspx", false);

    }

}