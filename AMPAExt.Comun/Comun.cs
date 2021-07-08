using System;
using System.Text;

namespace AMPAExt.Comun
{
    public class Excepciones
    {
        [Serializable]
        public class ErrorDeConexionException : Exception
        {

        }

        //[Serializable]
        //public class ErrorNoRecuperableException : Exception
        //{

        //}

        //[Serializable]
        //public class ErrorEjecutarProcedimientoException : Exception
        //{
        //    #region CODIGOSERRORES

        //    /// <summary>
        //    /// Códigos de error en las diferentes funciones
        //    /// -13 La contraseña esta ya registrada
        //    /// -12 El fabricante y/o marca no existen en RFFR
        //    /// -11 Argumento inválido. El agumento no cumple los requisitos
        //    /// -10 error concurrencia base de datos
        //    /// -8 cuando falla al enviar la sol. al PAC
        //    /// -7 cuando falla el apunte en el registro electrónico
        //    /// -6 cuando falla al insertar las categorías
        //    /// -5 cuando falla al borrar las categorías
        //    /// -4 cuando falla al insetar un documento
        //    /// -3 cuando falla el borrado de un documento
        //    /// -2 cuando falla la insercción/borrado de documentos
        //    /// -1 error de base de datos
        //    /// </summary>
        //    public const int eContraFR = -13;
        //    public const int eRFFR = -12;
        //    public const int eConcurrencia = -10;
        //    public const int eArginvalid = -11;
        //    public const int eInsertarcat = -6;
        //    public const int eBorrarcat = -5;
        //    public const int eInsertardoc = -4;
        //    public const int eBorrardoc = -3;
        //    public const int eTotaldoc = -2;
        //    public const int eBD = -1;

        //    #endregion

        //    public int CodError { get; set; }

        //    public ErrorEjecutarProcedimientoException()
        //    {

        //    }

        //    public ErrorEjecutarProcedimientoException(string mensaje)
        //        : base(mensaje)
        //    {

        //    }

        //    public ErrorEjecutarProcedimientoException(string mensaje, int Error)
        //        : base(mensaje)
        //    {
        //        CodError = Error;
        //    }
        //}

        ///// <summary>
        ///// Excepción lanzada si el un agumento no cumple los requisitos necesarios
        ///// </summary>
        //[Serializable]
        //public class ErrorArgumentoInvalidoException : Exception
        //{

        //    #region CODIGOSERRORES

        //    /// <summary>
        //    /// -3 error el tamaño del argumento
        //    /// -2 error en el formato del argumento
        //    /// -1 error argumento vacio
        //    /// </summary>
        //    public const int eSize = -3;
        //    public const int eFormato = -2;
        //    public const int eVacio = -1;

        //    #endregion

        //    /// <summary>
        //    /// Código de error asociado a la excepción
        //    /// </summary>
        //    public int CodError { get; set; }

        //    /// <summary>
        //    /// Constructor de la excepción que recibe un único parámetro el mensaje de la misma
        //    /// </summary>
        //    /// <param name="mensaje">Mensaje que arroja la excepción</param>
        //    public ErrorArgumentoInvalidoException(string mensaje)
        //        : base(mensaje)
        //    {

        //    }

        //    /// <summary>
        //    /// constructor de la excpción que recibe dos parámetros, el mensaje de la misma y el codigo de error
        //    /// </summary>
        //    /// <param name="mensaje">mensaje que arroja la excepción</param>
        //    /// <param name="Error">código de error de la excépción</param>
        //    public ErrorArgumentoInvalidoException(string mensaje, int Error)
        //        : base(mensaje)
        //    {
        //        CodError = Error;
        //    }
        //}

        ///// <summary>
        ///// Excepción lanzada si se realiza una operación no permita o falla una validación
        ///// </summary>
        //[Serializable]
        //public class ErrorOperacionInvalidaException : Exception
        //{

        //    #region CODIGOSERRORES

        //    /// <summary>
        //    /// -11 error al intentar eliminar una solicitud que previamente ha sido eliminada por otro usuario
        //    /// -10 error al eliminar debido al estado de la solicitud
        //    /// -9 error al procesar la solicitud por falta de documentación en tarjeta preimpresa
        //    /// -8 error al procesar la solicitud de tipo a debido a organismo dado de baja
        //    /// -7 error al procesar la cantidad de tarjetas
        //    /// -6 error al procesar aceptar una solicitud sin sus homologaciones procesadas (Tarjetas ITV)
        //    /// -5 error al salirnos de un rango de números
        //    /// -4 error por no aceptación de las cláusulas de la solicitud (tarjetas ITV)
        //    /// -3 error el estado de la solicitud ha sido cambiado por otro usuario
        //    /// -2 error número de documento identidad incorrecto
        //    /// -1 error objeto vacío
        //    /// </summary>
        //    public const int eEstadoEliminada = -11;
        //    public const int eEstadoEliminar = -10;
        //    public const int eDocPreimpresa = -9;
        //    public const int eOrganismoBaja = -8;
        //    public const int eCantidadVacia = -7;
        //    public const int eProcesada = -6;
        //    public const int eRango = -5;
        //    public const int eAceptar = -4;
        //    public const int eEstado = -3;
        //    public const int eNumDocumento = -2;
        //    public const int eVacio = -1;


        //    #endregion

        //    /// <summary>
        //    /// Código de error asociado a la excepción
        //    /// </summary>
        //    public int CodError { get; set; }

        //    /// <summary>
        //    /// Constructor de la excpción que recibe un único parámetro el mensaje de la misma
        //    /// </summary>
        //    /// <param name="mensaje">mensaje que arroja la excepción</param>
        //    public ErrorOperacionInvalidaException(string mensaje)
        //        : base(mensaje)
        //    {

        //    }

        //    /// <summary>
        //    /// Constructor de la excpción que recibe dos parámetros, el mensaje de la misma y el codigo de error
        //    /// </summary>
        //    /// <param name="mensaje">mensaje que arroja la excepción</param>
        //    /// <param name="Error">código de error de la excépción</param>
        //    public ErrorOperacionInvalidaException(string mensaje, int Error)
        //        : base(mensaje)
        //    {
        //        CodError = Error;
        //    }
        //}

        ///// <summary>
        ///// Clase que recoge una excepcion si un datatable es nulo
        ///// </summary>
        //[Serializable]
        //public class ErrorDatatableNuloException : Exception
        //{
        //    /// <summary>
        //    /// Código de error asociado a la excepción
        //    /// </summary>
        //    public int CodError { get; set; }

        //    public ErrorDatatableNuloException()
        //    {

        //    }

        //    public ErrorDatatableNuloException(string mensaje)
        //        : base(mensaje)
        //    {

        //    }

        //    public ErrorDatatableNuloException(string mensaje, int Error)
        //        : base(mensaje)
        //    {
        //        CodError = Error;
        //    }
        //}

        //[Serializable]
        //public class ErrorRegistroElectronicoSinConexionException : Exception
        //{
        //    public ErrorRegistroElectronicoSinConexionException()
        //    {

        //    }

        //    public ErrorRegistroElectronicoSinConexionException(string mensaje)
        //        : base(mensaje)
        //    {

        //    }
        //}

        //[Serializable]
        //public class ErrorRecuperableRegistroElectronicoException : Exception
        //{
        //    public ErrorRecuperableRegistroElectronicoException()
        //    {

        //    }

        //    public ErrorRecuperableRegistroElectronicoException(string mensaje)
        //        : base(mensaje)
        //    {

        //    }
        //}

        //[Serializable]
        //public class ErrorSqlException : Exception
        //{
        //    public ErrorSqlException()
        //    {

        //    }

        //    public ErrorSqlException(string mensaje)
        //        : base(mensaje)
        //    {

        //    }
        //}

        //[Serializable]
        //public class ErrorObtenerDatosException : Exception
        //{
        //    public ErrorObtenerDatosException()
        //    {

        //    }

        //    public ErrorObtenerDatosException(string mensaje)
        //        : base(mensaje)
        //    {

        //    }
        //}

        //[Serializable]
        //public class ErrorEnviarCorreoException : Exception
        //{
        //    public ErrorEnviarCorreoException()
        //    {

        //    }

        //    public ErrorEnviarCorreoException(string mensaje)
        //        : base(mensaje)
        //    {

        //    }
        //}

        //[Serializable]
        //public class ErrorObtenerEstadoException : Exception
        //{
        //    public ErrorObtenerEstadoException() { }

        //    public ErrorObtenerEstadoException(string mensaje) : base(mensaje) { }
        //}

    }
    /// <summary>
    /// Clase encargada del tratamiento de logs generados por la aplicación
    /// </summary>
    public static class Log
    {
        /// <summary>
        /// Genera trazas de log en la aplicación
        /// </summary>
        public static readonly log4net.ILog TrazaLog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }

    /// <summary>
    /// Contenedor de utilidades para la aplicación
    /// </summary>
    public partial class Utilidades
    {
        /// <summary>
        /// Encripta con el algoritmo de encriptación SHA-256 una cadena
        /// </summary>
        /// <param name="cadena">Cadena a encriptar</param>
        /// <returns>Cadena encriptada con el algoritmo SHA-256</returns>
        public static string GetSHA256(String cadena)
        {
            System.Security.Cryptography.SHA256 encr = System.Security.Cryptography.SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            int i;
            StringBuilder resultado = new StringBuilder();
            stream = encr.ComputeHash(encoding.GetBytes(cadena));
            for (i = 0; i < stream.Length; i++)
                resultado.AppendFormat("{0:x2}", stream[i]);
            return resultado.ToString();
        }
    }

    /// <summary>
    /// Contiene la información del usuario que ha realizado login en el sistema
    /// </summary>
    [Serializable]
    public partial class conexion
    {
        #region Propiedades
        /// <summary>
        /// Indicador de que el usuario no está logado
        /// </summary>
        private const string _strNologado = "NO_LOGADO";

        /// <summary>
        /// Datos del usuario que ha realizado el login (privada)
        /// </summary>
        private UsuarioConexion _usuarioConectado;

        /// <summary>
        /// Datos del usuario que ha realizado el login
        /// </summary>
        public UsuarioConexion UsuarioConectado
        {
            get { return _usuarioConectado; }
            set { _usuarioConectado = value; }
        }
        /// <summary>
        /// Listado de tipo de usuario para el tratamiento en el sitemap. Si no tiene ningún perfil devuelve usuario no logado "NO_LOGADO"
        /// </summary>
        public string PermisosConexion
        {
            get
            {
                if (this.UsuarioConectado == null)
                    return _strNologado;
                string listaPermisos = string.Empty;
                listaPermisos = this.UsuarioConectado.CodTipoUsuario.ToString();
                return listaPermisos;
            }
        }
        #endregion


    }
}