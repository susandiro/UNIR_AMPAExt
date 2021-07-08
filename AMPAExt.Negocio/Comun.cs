using AMPAExt.Modelo;

namespace AMPAExt.Negocio
{
    /// <summary>
    /// Clase común a todas las clases de negocio
    /// </summary>
    public class Comun
    {
        #region Instancias de las clases del Modelo

        #region privadas
        /// <summary>
        /// Instancia de la clase del modelo <see cref="ClsAdministracion"/>
        /// </summary>
        private AMPAExt.Modelo.ClsAdministracion _clsUsuarioDat;
        /// <summary>
        /// Instancia de la clase del modelo <see cref="ClsExtraescolar/>
        /// </summary>
        private AMPAExt.Modelo.ClsExtraescolar _clsExtraescolarDat;
        /// <summary>
        /// Instancia de la clase del modelo <see cref="clsActividad"/>
        /// </summary>
        private AMPAExt.Modelo.clsActividad _clsActividadDat;
        /// <summary>
        /// Instancia de la clase del modelo <see cref="clsSocio"/>
        /// </summary>
        private AMPAExt.Modelo.clsSocio _clsSocioDat;
        /// <summary>
        /// Instancia de la clase del modelo <see cref="clsTablasMaestras"/>
        /// </summary>
        private AMPAExt.Modelo.clsTablasMaestras _clsTablasMaestrasDat;
        #endregion

        #region públicas
        /// <summary>
        /// Instancia de la clase del modelo <see cref="ClsAdministracion"/>. Si no existe la instancia, se crea una nueva. Si ya existe, es la que se usará
        /// </summary>
        public ClsAdministracion UsuarioDat {
            get
            {
                if (_clsUsuarioDat == null)
                    _clsUsuarioDat = new ClsAdministracion();
                return _clsUsuarioDat;
            }
        }

        /// <summary>
        /// Instancia de la clase del modelo <see cref="ClsExtraescolar"/>. Si no existe la instancia, se crea una nueva. Si ya existe, es la que se usará
        /// </summary>
        public ClsExtraescolar ExtraescolarDat
        {
            get
            {
                if (_clsExtraescolarDat == null)
                    _clsExtraescolarDat = new ClsExtraescolar();
                return _clsExtraescolarDat;
            }
        }

        /// <summary>
        /// Instancia de la clase del modelo <see cref="clsActividad"/>. Si no existe la instancia, se crea una nueva. Si ya existe, es la que se usará
        /// </summary>
        public clsActividad ActividadDat
        {
            get
            {
                if (_clsActividadDat == null)
                    _clsActividadDat = new clsActividad();
                return _clsActividadDat;
            }
        }
        /// <summary>
        /// Instancia de la clase del modelo <see cref="clsSocio"/>. Si no existe la instancia, se crea una nueva. Si ya existe, es la que se usará
        /// </summary>
        public clsSocio SocioDat
        {
            get
            {
                if (_clsSocioDat == null)
                    _clsSocioDat = new clsSocio();
                return _clsSocioDat;
            }
        }

        /// <summary>
        /// Instancia de la clase del modelo <see cref="clsTablasMaestras"/>. Si no existe la instancia, se crea una nueva. Si ya existe, es la que se usará
        /// </summary>
        public clsTablasMaestras TablasMaestrasDat
        {
            get
            {
                if (_clsTablasMaestrasDat == null)
                    _clsTablasMaestrasDat = new clsTablasMaestras();
                return _clsTablasMaestrasDat;
            }
        }
        #endregion

        #endregion
    }
}
