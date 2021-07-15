using System;
using System.Collections.Generic;
using AMPA.Modelo;
using AMPAExt.Comun;
using System.Data.Entity;

namespace AMPAExt.Negocio
{
    /// <summary>
    /// clase de negocio para el tratamiento de la gestión de empresas extraescolares
    /// </summary>
    public class Extraescolar : Comun
    {
        #region Altas
        /// <summary>
        /// Realiza un alta de usuario de tipo extraescolar
        /// </summary>
        /// <param name="datosUsuario">Datos del usuario que se va a dar de alta</param>
        /// <returns>Identificador del nuevo usuario<</returns>
        public int AltaUsuarioExtraescolar(USUARIO_EMPRESA datosUsuario)
        {
            int resultado;
            try
            {
                //Se asigna la contraseña y login al usuario
                datosUsuario.LOGIN = datosUsuario.NUMERO_DOCUMENTO;
                datosUsuario.PASSWORD = Utilidades.GetSHA256(datosUsuario.NUMERO_DOCUMENTO);

                resultado = ExtraescolarDat.AltaUsuarioEmpresa(datosUsuario);
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".AltaUsuarioExtraescolar() Para la empresa: " + datosUsuario.ID_EMPRESA.ToString(), ex);
                throw;
            }
            return resultado;
        }

        /// <summary>
        /// Realiza el alta de la empresa extraescolar junto con un usuario asociado a ella
        /// 1º_Se comprueba si la empresa ya existe (por NIF). Si existe, devuelve el control indicando que la empresa ya existe
        /// 2º Se comprueba que el usuario exista. Si existe, recupera su Id de usuario
        /// 3º Se da de alta la empresa
        /// 4º Se da de alta al usuario si no existe
        /// 5º Se vincula la empresa con el usuario
        /// </summary>
        /// <param name="nuevaEmpresa">Datos de la empresa extraescolar junto con los datos del usuario que se va a dar de alta</param>
        /// <param name="idAMPA">Identificador del AMPA al que se asocia la empresa extraescolar</param>
        /// <param name="usuarioGestor">Datos del usuario que da de alta la empresa y su usuario</param>
        /// <returns>Identificador de la empresa dada de alta. Si devuelve -10, la empresa ya existe</returns>
        public int AltaEmpresaExtUsuario(USUARIO_EMPRESA nuevaEmpresa, int idAMPA, string usuarioGestor)
        {
            int resultado = -1, idEmpresa;
            if (nuevaEmpresa != null && nuevaEmpresa.USUARIO != null && nuevaEmpresa.EMPRESA != null)
            {
                EMPRESA datosEmpresa = UsuarioDat.GetEmpresabyNIF(nuevaEmpresa.EMPRESA.NIF);
                if (datosEmpresa != null)
                    resultado = -10; //La empresa ya existe
                else
                {
                    using (AMPAEXTBD conn = new AMPAEXTBD())
                    {
                        using (DbContextTransaction transaccion = conn.Database.BeginTransaction())
                        {
                            try
                            {
                                //Se da de alta la empresa
                                idEmpresa = UsuarioDat.AltaEmpresa(nuevaEmpresa.EMPRESA, idAMPA, usuarioGestor, conn);
                                if (idEmpresa < 1)
                                    throw new Exception("La empresa no ha podido ser creada");
                                nuevaEmpresa.ID_EMPRESA = idEmpresa;
                                //Se da de alta al usuario si no existe
                                //Se asigna la contraseña y login al usuario por si no existe
                                nuevaEmpresa.LOGIN = nuevaEmpresa.NUMERO_DOCUMENTO;
                                nuevaEmpresa.PASSWORD = Utilidades.GetSHA256(nuevaEmpresa.NUMERO_DOCUMENTO);

                                resultado = ExtraescolarDat.AltaUsuarioEmpresa(nuevaEmpresa, conn);
                                transaccion.Commit();
                            }
                            catch (Exception ex)
                            {
                                transaccion.Rollback();
                                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".AltaEmpresaExtUsuario().", ex);
                                throw;
                            }
                        }
                    }
                }
            }
            else
                throw new Exception("No se han introducido datos de la empresa y usuario a dar de alta");
            return resultado;
        }

        /// <summary>
        /// Realiza un alta de monitor
        /// </summary>
        /// <param name="datosMonitor">Datos del monitor que se va a dar de alta</param>
        /// <returns>Identificador del nuevo monitor<</returns>
        public int AltaMonitor(MONITOR datosMonitor)
        {
            int resultado;
            try
            {
                //Se asigna la contraseña y login al usuario
                datosMonitor.LOGIN = datosMonitor.NUMERO_DOCUMENTO;
                datosMonitor.PASSWORD = Utilidades.GetSHA256(datosMonitor.NUMERO_DOCUMENTO);

                resultado = ExtraescolarDat.AltaMonitor(datosMonitor);
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".AltaMonitor() Para la empresa: " + datosMonitor.ID_EMPRESA.ToString(), ex);
                throw;
            }
            return resultado;
        }
        #endregion

        #region Consultas
        /// <summary>
        /// Obtiene un determinado usuario de una empresa
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario a recuperar</param>
        /// <param name="idEmpresa">Identificador de la empresa</param>
        /// <returns>Datos del usuario en <see cref="USUARIO_EMPRESA"/></returns>
        public USUARIO_EMPRESA GetUsuarioByIdEmpresa(int idUsuario, int idEmpresa)
        {
            return ExtraescolarDat.GetUsuarioByIdEmpresa(idUsuario, idEmpresa);
        }

        /// <summary>
        /// Obtiene un determinado monitor de una empresa
        /// </summary>
        /// <param name="idMonitor">Identificador del monitor a recuperar</param>
        /// <param name="idEmpresa">Identificador de la empresa</param>
        /// <returns>Datos del usuario en <see cref="MONITOR"/></returns>
        public MONITOR GetMonitorById(int idMonitor, int idEmpresa)
        {
            return ExtraescolarDat.GetMonitorById(idMonitor, idEmpresa);
        }

        /// <summary>
        /// Obtiene el listado de usuario de tipo empresa extraescolar
        /// </summary>
        /// <param name="filtro">Campos de búsqueda</param>
        /// <returns>Listado de <see cref="USUARIO_EMPRESA"/> con los usuarios</returns>
        public List<USUARIO_EMPRESA> GetUsuarios( FiltroUsuario filtro)
        {
            return ExtraescolarDat.GetUsuarios(filtro);
        }

        /// <summary>
        /// Obtiene un determinado usuario 
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario a recuperar</param>
        /// <returns>Datos del usuario en <see cref="USUARIO_EMPRESA"/></returns>
        public USUARIO_EMPRESA GetUsuarioById(int idUsuario)
        {
            return ExtraescolarDat.GetUsuarioById(idUsuario);
        }

        /// <summary>
        /// Recupera los datos de una empresa
        /// </summary>
        /// <param name="idEmpresa">Identificador de la empresa a recuperar</param>
        /// <returns>Datos de la empresa en <see cref="EMPRESA"></see> </returns>
        public EMPRESA GetEmpresabyId(int idEmpresa)
        {
            return ExtraescolarDat.GetEmpresabyId(idEmpresa);
        }

        /// <summary>
        /// Recupera las empresas que están asociadas a una AMPA
        /// </summary>
        /// <param name="idAMPA">Identificador de la AMPA a la que pertenecen las empresas</param>
        /// <param name="filtro">Campos de búsqueda</param>
        /// <returns>Listado de las empresas en <see cref="EMPRESA_AMPA"></see> </returns>
        public List<EMPRESA_AMPA> GetEmpresasbyIdAMPA(int idAMPA, FiltroUsuario filtro)
        {
            return ExtraescolarDat.GetEmpresasbyIdAMPA(idAMPA, filtro);
        }

        /// <summary>
        /// Recupera las empresas que están asociadas a una AMPA
        /// </summary>
        /// <param name="idAMPA">Identificador de la AMPA a la que pertenecen las empresas</param>
        /// <param name="soloActivas">Si sólo para las activas. True: solo activas; False: todas</param>
        /// <returns>Listado de las empresas en <see cref="EMPRESA_AMPA"></see> </returns>
        public List<EMPRESA_AMPA> GetEmpresasbyIdAMPA(int idAMPA, bool soloActivas)
        {
            FiltroUsuario filtro = new FiltroUsuario();
            if (soloActivas) filtro.Activo = "S";
            return ExtraescolarDat.GetEmpresasbyIdAMPA(idAMPA, filtro);
        }

        /// <summary>
        /// Obtiene el listado de monitores de una empresa
        /// </summary>
        /// <param name="idEmpresa">Identificador de la empresa a la que pertenecen los monitores</param>
        /// <returns>Listado de <see cref="MONITOR"/> con los monitores</returns>
        public List<MONITOR> GetMonitores(int idEmpresa)
        {
            return ExtraescolarDat.GetMonitores(idEmpresa);
        }

        #endregion

        #region Modificaciones
        /// <summary>
        /// Realiza la modificación de un usuario de tipo extraescolar
        /// </summary>
        /// <param name="usuario">Datos del usuario a modificar</param>
        /// <returns>Booleano con el resultado: true si ha ido todo bien; false en caso contrario</returns>
        public bool ModificarUsuarioExtraescolar(USUARIO_EMPRESA usuario)
        {
            USUARIO_EMPRESA datosUsuario = GetUsuarioByIdEmpresa(usuario.ID_USUARIO_EMP, usuario.ID_EMPRESA);
            usuario.LOGIN = datosUsuario.LOGIN;
            usuario.PASSWORD = datosUsuario.PASSWORD;
            usuario.FECHA = datosUsuario.FECHA;
            return ExtraescolarDat.ModificarUsuarioExtr(usuario);
        }

        /// <summary>
        /// Realiza la modificación de un monitor
        /// </summary>
        /// <param name="datos">Datos del monitor a modificar</param>
        /// <returns>Booleano con el resultado: true si ha ido todo bien; false en caso contrario</returns>
        public bool ModificarMonitor(MONITOR datos)
        {
            MONITOR resultado = GetMonitorById(datos.ID_MONITOR, datos.ID_EMPRESA);
            datos.LOGIN = resultado.LOGIN;
            datos.PASSWORD = resultado.PASSWORD;
            datos.FECHA = resultado.FECHA;
            return ExtraescolarDat.ModificarMonitor(datos);
        }
        /// <summary>
        /// Realiza la modificación de una empresa
        /// </summary>
        /// <param name="empresa">Datos de la empresa</param>
        /// <returns>Booleano con el resultado: true si ha ido todo bien; false en caso contrario</returns>
        public bool ModificarEmpresa(EMPRESA empresa)
        {
            EMPRESA datosEmpresa = GetEmpresabyId(empresa.ID_EMPRESA);
            empresa.FECHA = datosEmpresa.FECHA;
            return ExtraescolarDat.ModificarEmpresa(empresa);
        }
        #endregion

        #region Borrados
        /// <summary>
        /// Realiza la eliminación de un usuario de una empresa
        /// </summary>
        /// <param name="idUsuario">Identificador de usuario</param>
        /// <param name="idEmpresa">Identificador de la empresa</param>
        /// <returns>Booleano con el resultado: true si ha ido todo bien; false en caso contrario</returns>
        public bool BajaUsuario(int idUsuario, int idEmpresa)
        {
            return ExtraescolarDat.BajaUsuario(idUsuario, idEmpresa);
        }
        /// <summary>
        /// Realiza la eliminación de un monitor de una empresa
        /// </summary>
        /// <param name="idMonitor">Identificador de monitor</param>
        /// <param name="idEmpresa">Identificador de la empresa</param>
        /// <returns>Booleano con el resultado: true si ha ido todo bien; false en caso contrario</returns>
        public bool BajaMonitor(int idMonitor, int idEmpresa)
        {
            return ExtraescolarDat.BajaMonitor(idMonitor, idEmpresa);
        }
        /// <summary>
        /// Realiza la baja de una empresa para una AMPA poniendo activo = N
        /// </summary>
        /// <param name="idEmpresa">Identificador de la empresa</param>
        /// <param name="idAMPA">Identificador de la AMPA a la que pertenece la empresa</param>
        /// <param name="usuarioAccion">Usuario que realiza la baja</param>
        /// <returns>Booleano con el resultado: true si ha ido todo bien; false en caso contrario</returns>
        public bool BajaEmpresa(int idEmpresa, int idAMPA, string usuarioAccion)
        {
            
            FiltroActividad filtro = new FiltroActividad();
            filtro.IdEmpresa = idEmpresa;
            filtro.IdAMPA = idAMPA;
            bool resultado = false;
            using (AMPAEXTBD conn = new AMPAEXTBD())
            {
                using (DbContextTransaction transaccion = conn.Database.BeginTransaction())
                {
                    try
                    {
                        List<ACTIVIDAD> actividades = ActividadDat.GetActividades(filtro);
                        List<ALUMNO_ACTIVIDAD> alumnos;
                        //Comprueba si hay alumnos en las actividades extrescolares y si los hay, los borra
                        foreach (ACTIVIDAD act in actividades)
                        {
                            alumnos = ActividadDat.GetAlumnnosByActividad(act.ID_ACTIVIDAD, conn);
                            foreach (ALUMNO_ACTIVIDAD alumno in alumnos)
                                if (!ActividadDat.BajaAlumnoHorario(alumno.ID_ALUM_ACT, conn))
                                    throw new Exception("No se ha podido dar de baja el alumno " + alumno.ID_ALUMNO.ToString() + " en la actividad horario " + alumno.ID_ACT_HORARIO.ToString());
                        }
                        //Da de baja a la empresa para la AMPA
                        if (!ExtraescolarDat.BajaEmpresa(idEmpresa, idAMPA, usuarioAccion, conn))
                            throw new Exception("No se ha podido dar de baja la empresa " + idEmpresa.ToString() + ", para la AMPA " + idAMPA.ToString() + ", con el usuario " + usuarioAccion);
                        resultado = true;
                        transaccion.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaccion.Rollback();
                        Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".BajaEmpresa(). idEmpresa: " + idEmpresa.ToString() + ", idAMPA: " + idAMPA.ToString(), ex);
                        throw;
                    }
                }
            }
            return resultado;

        }
        #endregion

    }
}
