using System.Collections.Generic;

namespace AMPAExt.Comun
{
    /// <summary>
    /// Objeto con los datos del usuario que ha realizado login en el sistema
    /// </summary>
    public class UsuarioConexion
    {
        #region Propiedades privadas
        /// <summary>
        /// Identificador del usuario (privada)
        /// </summary>
        private int _idUsuario;
        /// <summary>
        /// Código de tipo de usuario correspondiente al usuario que ha realizado el login en el sistema (privada)
        /// </summary>
        private TipoDatos.TipoUsuario _codTipoUsuario;
        /// <summary>
        /// Nombre completo correspondiente al usuario que ha realizado el login en el sistema (privada)
        /// </summary>
        private string _nombreCompleto;
        /// <summary>
        /// Número de documento y nombre completo correspondiente al usuario que ha realizado el login en el sistema (privada)
        /// </summary>
        private string _datosUsuario;
        /// <summary>
        /// Nombre correspondiente a la empresa a la que pertenece el usuario que ha realizado el login en el sistema (privada)
        /// </summary>
        private string _empresa;
        /// <summary>
        /// Identificador de la empresa a la que pertenece (privada)
        /// </summary>
        private int _idEmpresa;
        /// <summary>
        /// Lista con los identificadores de Empresa/AMPA sobre los que tiene permiso (privada)
        /// </summary>
        private List<int> _listaIdEmpresa;

        #endregion

        #region Propiedades públicas

        /// <summary>
        /// Identificador del usuario (privada)
        /// </summary>
        public int IdUsuario
        {
            get { return _idUsuario; }
            set { _idUsuario = value; }
        }

        /// <summary>
        /// Código de tipo de usuario correspondiente al usuario que ha realizado el login en el sistema
        /// </summary>
        public TipoDatos.TipoUsuario CodTipoUsuario
        {
            get { return _codTipoUsuario; }
            set { _codTipoUsuario = value; }
        }
         /// <summary>
        /// Nombre completo (nombre y apellidos) correspondiente al usuario que ha realizado el login en el sistema 
        /// </summary>
        public string NombreCompleto
        {
            get { return _nombreCompleto; }
            set { _nombreCompleto = value; }
        }
        /// <summary>
        /// Nombre correspondiente a la empresa a la que pertenece el usuario que ha realizado el login en el sistema
        /// </summary>
        public string Empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }

        /// <summary>
        /// Número de documento y nombre completo correspondiente al usuario que ha realizado el login en el sistema (pública)
        /// </summary>
        public string DatosUsuario
        {
            get { return _datosUsuario; }
            set { _datosUsuario = value; }
        }

        /// <summary>
        /// Identificador de la empresa a la que pertenece (pública)
        /// </summary>
        public int IdEmpresa
        {
            get { return _idEmpresa; }
            set { _idEmpresa = value; }
        }

        /// <summary>
        /// Lista con los identificadores de Empresa/AMPA sobre los que tiene permiso (pública)
        /// </summary>
        public List<int> ListaIdEmpresa
        {
            get { return _listaIdEmpresa; }
            set { _listaIdEmpresa = value; }
        }
        #endregion
    }
}
