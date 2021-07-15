using System;
using System.Linq;
using System.Data.Entity;
using AMPAExt.Comun;
using System.Collections.Generic;
using AMPA.Modelo;

namespace AMPAExt.Modelo
{
    /// <summary>
    /// Realiza las llamadas relacionadas con las consultas y gestión de usuarios en base de datos
    /// </summary>
    public class ClsAdministracion
    {
        #region Métodos Usuario
        /// <summary>
        /// Realiza el alta de un nuevo usuario en AMPA. En el caso de que el usuario exista ya en el sistema, se devolverá su identificador
        /// </summary>
        /// <param name="datosUsuario">Datos del usuario que se va a dar de alta</param>
        /// <returns>Identificador del usuario dado de alta en el sistema.</returns>
        public int AltaUsuarioAMPA(USUARIO_AMPA datosUsuario)
        {
            int idUsuario;
            try
            {
                using (AMPAEXTBD conn = new AMPAEXTBD())
                {
                    USUARIO_AMPA nuevoUsuario = conn.USUARIO_AMPA.Where(d => d.NUMERO_DOCUMENTO == datosUsuario.NUMERO_DOCUMENTO &&
                        d.ID_TIPO_DOCUMENTO == datosUsuario.ID_TIPO_DOCUMENTO).FirstOrDefault();
                    //Si existe el usuario, se devuelve el identificador del usuario
                    if (nuevoUsuario != null)
                        idUsuario = nuevoUsuario.ID_USUARIO;
                    else
                    {
                        conn.USUARIO_AMPA.Add(datosUsuario);
                        conn.SaveChanges();
                        idUsuario = datosUsuario.ID_USUARIO;
                    }
                }
                return idUsuario;
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error al insertar el nuevo usuario.", ex);
                throw;
            }
        }

        /// <summary>
        /// Devuelve el usuario de AMPA correspondiente al login y password introducidos
        /// </summary>
        /// <param name="usuario">Login del usuario para el acceso a la aplicación</param>
        /// <param name="password">Contraseña del usuario para el acceso a la aplicación</param>
        /// <returns>Objeto <see cref="USUARIO_AMPA"/> con los datos del usuario o, null si no se ha encontrado
        public USUARIO_AMPA GetUsuarioAMPA(string usuario, string password)
        {
            USUARIO_AMPA usuarioBuscado = new USUARIO_AMPA();
            try
            {
                string pass = Utilidades.GetSHA256(password);
                using (AMPAEXTBD db = new AMPAEXTBD())
                {
                    usuarioBuscado = db.USUARIO_AMPA
                        .Where(c => c.LOGIN.ToUpper() == usuario.ToUpper() &&
                        c.PASSWORD == pass).FirstOrDefault();
                }
            }
            catch(Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".GetUsuarioAMPA(). Usuario: " + usuario, ex);
                throw;
            }
            return usuarioBuscado;
        }

        /// <summary>
        /// Devuelve el usuario de empresa extraescolar correspondiente al login y password introducidos
        /// </summary>
        /// <param name="usuario">Login del usuario para el acceso a la aplicación</param>
        /// <param name="password">Contraseña del usuario para el acceso a la aplicación</param>
        /// <returns>Objeto <see cref="USUARIO_EMPRESA"/> con los datos del usuario o, null si no se ha encontrado
        public USUARIO_EMPRESA GetUsuarioExtraescolar(string usuario, string password)
        {
            USUARIO_EMPRESA usuarioBuscado = new USUARIO_EMPRESA();
            try
            {
                string pass = Utilidades.GetSHA256(password);
                using (AMPAEXTBD db = new AMPAEXTBD())
                {
                    usuarioBuscado = db.USUARIO_EMPRESA
                        .Where(c => c.LOGIN.ToUpper() == usuario.ToUpper() &&
                        c.PASSWORD == pass).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".GetUsuarioExtraescolar(). Usuario: " + usuario, ex);
                throw;
            }
            return usuarioBuscado;
        }
        /// <summary>
        /// Obtiene la AMPA sobre la que tiene permisos el usuario
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario perteneciente a la AMPA</param>
        /// <returns><see cref="USUARIO_AMPA"/> con los datos de la AMPA</returns>
        public USUARIO_AMPA GetAMPAbyIdUsuario(int idUsuario)
        {
            USUARIO_AMPA empresa = new USUARIO_AMPA();
            try
            {
                using (AMPAEXTBD db = new AMPAEXTBD())
                {
                    empresa = db.USUARIO_AMPA
                        .Where(c => c.ID_USUARIO == idUsuario)
                        .Include(c => c.AMPA)
                        .Include(c => c.AMPA.EMPRESA_AMPA)
                       .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".GetAMPAbyIdUsuario(). IdUsuario: " + idUsuario.ToString(), ex);
            }
            return empresa;
        }

        /// <summary>
        /// Obtiene la empresa sobre la que tiene permisos el usuario
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario del que se van a obtener empresa sobre la que tiene permisos</param>
        /// <returns><see cref="USUARIO_EMPRESA"/> con la empresa sobre la que tiene permiso</returns>
        public USUARIO_EMPRESA GetEmpresabyIdUsuario(int idUsuario)
        {

            USUARIO_EMPRESA empresas = new USUARIO_EMPRESA(); 
            try
            {
                using (AMPAEXTBD db = new AMPAEXTBD())
                {
                    empresas = db.USUARIO_EMPRESA
                        .Where(c => c.ID_USUARIO_EMP == idUsuario)
                        .Include(c => c.EMPRESA)
                        .Include(c => c.EMPRESA.EMPRESA_AMPA)
                       .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".GetEmpresabyIdUsuario(). IdUsuario: " + idUsuario.ToString(), ex);
            }
            return empresas;
        }

        /// <summary>
        /// Obtiene el listado de empresas que pertenecen a una AMPA
        /// </summary>
        /// <param name="idAMPA">Identificador de la AMPA</param>
        /// <param name="soloActivas">True si sólo las que se encuentren activas. False para todas</param>
        /// <returns>Listado de <see cref="EMPRESA_AMPA"/> con las empresas que pertenecen a la AMPA</returns>
        public List<EMPRESA_AMPA> GetEmpresasByAMPA(int idAMPA, bool soloActivas)
        {
            List<EMPRESA_AMPA> empresas = new List<EMPRESA_AMPA>();
            try
            {
                using (AMPAEXTBD db = new AMPAEXTBD())
                {
                    if (soloActivas)
                        empresas = db.EMPRESA_AMPA
                            .Where(c => c.ID_AMPA == idAMPA && c.ACTIVO == "S")
                            .Include(c => c.EMPRESA)
                            .OrderBy(c => c.EMPRESA.NOMBRE)
                            .ToList();
                    else
                        empresas = db.EMPRESA_AMPA
                            .Where(c => c.ID_AMPA == idAMPA)
                            .Include(c => c.EMPRESA)
                            .OrderBy(c => c.EMPRESA.NOMBRE)
                            .ToList();
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".GetEmpresasByAMPA(). idAMPA: " + idAMPA.ToString() + ", soloActivas:" + soloActivas.ToString(), ex);
            }
            return empresas;
        }

        /// <summary>
        /// Obtiene el listado de AMPAS asociadas a una empresa. Sólo las activas
        /// </summary>
        /// <param name="idEmpresa">Identificador de la empresa extraescolar</param>
        /// <returns>Listado de <see cref="EMPRESA_AMPA"/> con las AMPAS asociadas a una empresa</returns>
        public List<EMPRESA_AMPA> GetAMPASByEmpresa(int idEmpresa)
        {
            List<EMPRESA_AMPA> empresas = new List<EMPRESA_AMPA>();
            try
            {
                using (AMPAEXTBD db = new AMPAEXTBD())
                {
                    empresas = db.EMPRESA_AMPA
                        .Where(c => c.ID_EMPRESA == idEmpresa && c.ACTIVO == "S")
                        .Include(c => c.AMPA)
                        .OrderBy(c => c.AMPA.NOMBRE)
               .ToList();
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".GetAMPASByEmpresa(). idEmpresa: " + idEmpresa.ToString(), ex);
            }
            return empresas;
        }

        /// <summary>
        /// Obtiene un determinado usuario de una AMPA
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario a recuperar</param>
        /// <param name="idAMPA">Identificador de la AMPA</param>
        /// <returns>Datos del usuario en <see cref="USUARIO_AMPA"/></returns>
        public USUARIO_AMPA GetUsuarioByIdAMPA(int idUsuario, int idAMPA)
        {
            USUARIO_AMPA usuario = new USUARIO_AMPA();
            try
            {
                using (AMPAEXTBD db = new AMPAEXTBD())
                {
                    usuario = db.USUARIO_AMPA
                        .Where(c => c.ID_USUARIO == idUsuario && c.ID_AMPA == idAMPA)
                        .Include(c => c.TIPO_DOCUMENTO)
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".GetUsuarioByIdAMPA(). idUsuario: " + idUsuario.ToString() + ", idAMPA: " + idAMPA.ToString(), ex);
                throw;
            }
            return usuario;
        }

        /// <summary>
        /// Obtiene el listado de usuario que forman parte de una AMPA
        /// </summary>
        /// <param name="idAMPA">Identificador de la AMPA</param>
        /// <param name="filtro">Campos de búsqueda</param>
        /// <returns>Listado de <see cref="USUARIO_AMPA"/> con los usuario de la AMPA</returns>
        public List<USUARIO_AMPA> GetUsuariosByAMPA(int idAMPA, FiltroUsuario filtro)
        {
            List<USUARIO_AMPA> usuarios = new List<USUARIO_AMPA>();
            try
            {
                using (AMPAEXTBD db = new AMPAEXTBD())
                {
                    var query = db.USUARIO_AMPA
                        .Where(c => c.ID_AMPA == idAMPA)
                        .Include(c => c.TIPO_DOCUMENTO);

                    if (!filtro.Vacio)
                    {
                        if (filtro.IdTipoDocumento > 0)
                            query = query.Where(c => c.ID_TIPO_DOCUMENTO == filtro.IdTipoDocumento);

                        if (!string.IsNullOrEmpty(filtro.NumeroDocumento))
                            query = query.Where(c => c.NUMERO_DOCUMENTO.ToUpper().Contains(filtro.NumeroDocumento.ToUpper()));
                        if (!string.IsNullOrEmpty(filtro.Telefono))
                            query = query.Where(c => c.TELEFONO.ToUpper().Contains(filtro.Telefono.ToUpper()));

                        if (!string.IsNullOrEmpty(filtro.Nombre))
                        {
                            string nombre = filtro.Nombre.ToUpper();
                            query = query.Where(c => c.NOMBRE.ToUpper().Contains(nombre) ||
                                                c.APELLIDO1.ToUpper().Contains(nombre) ||
                                                c.APELLIDO2.ToUpper().Contains(nombre));
                        }                            
                    }
                    query = query.OrderBy(c => c.NOMBRE);
                    usuarios = query.ToList();
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".GetUsuariosByAMPA(). idAMPA: " + idAMPA.ToString(), ex);
            }
            return usuarios;
        }

        /// <summary>
        /// Realiza la eliminación de un usuario de tipo AMPA
        /// </summary>
        /// <param name="idUsuario">Identificador de usuario</param>
        /// <param name="idAMPA">Identificador de la AMPA</param>
        /// <returns>Booleano con el resultado: true si ha ido todo bien; false en caso contrario</returns>
        public bool BajaUsuarioAMPA(int idUsuario, int idAMPA)
        {
            bool resultado = false;
            try
            {
                using (AMPAEXTBD conn = new AMPAEXTBD())
                {
                    USUARIO_AMPA usuario = conn.USUARIO_AMPA
                       .Where(c => c.ID_USUARIO == idUsuario && c.ID_AMPA == idAMPA)
                     .FirstOrDefault();

                    if (usuario == null)
                        throw new Exception("El usuario " + idUsuario.ToString() + " no se encuentra como usuario de AMPA " + idAMPA.ToString());

                    conn.USUARIO_AMPA.Remove(usuario);
                    conn.SaveChanges();
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".BajaUsuarioAMPA(). idUsuario: " + idUsuario.ToString() + ", idAMPA: " + idAMPA.ToString(), ex);
                throw;
            }
            return resultado;
        }

        /// <summary>
        /// Realiza la modificación de un usuario de tipo AMPA
        /// </summary>
        /// <param name="usuario">Datos del usuario a modificar</param>
        /// <returns>Booleano con el resultado: true si ha ido todo bien; false en caso contrario</returns>
        public bool ModificarUsuarioAMPA(USUARIO_AMPA usuario)
        {
            bool resultado = false;
            try
            {
                using (AMPAEXTBD conn = new AMPAEXTBD())
                {
                    conn.Entry(usuario).State = EntityState.Modified;
                    conn.SaveChanges();
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".ModificarUsuarioAMPA()", ex);
                throw;
            }
            return resultado;
        }

        #endregion

        #region Empresa

        /// <summary>
        /// Recupera los datos de la empresa por el NIF indicado
        /// </summary>
        /// <param name="NIF">NIF de la empresa a buscar</param>
        /// <returns>Datos de la empresa en la clase <see cref="EMPRESA"/></returns>
        public EMPRESA GetEmpresabyNIF(string NIF)
        {
            EMPRESA empresaBuscada = new EMPRESA();
            try
            {
                using (AMPAEXTBD db = new AMPAEXTBD())
                {
                    empresaBuscada = db.EMPRESA
                        .Where(c => c.NIF.ToUpper() == NIF.ToUpper()).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".GetEmpresabyNIF(). NIF: " + NIF, ex);
                throw;
            }
            return empresaBuscada;
        }

        /// <summary>
        /// Realiza el alta de una nueva empresa.
        /// </summary>
        /// <param name="datosEmpresa">Datos de la empresa que se va a dar de alta</param>
        /// <param name="idAMPA">Identificador de la AMPA para la que se dará de alta</param>
        /// <param name="usuarioGestor">Datos del usuario que está realizando el alta</param>
        /// <param name="conn">Conexión abierta para realizar la acción</param>
        /// <returns>Identificador de la empresa dada de alta en el sistema.</returns>
        public int AltaEmpresa(EMPRESA datosEmpresa, int idAMPA, string usuarioGestor,  AMPAEXTBD conn)
        {
            int idEmpresa;
            try
            {
                conn.EMPRESA.Add(datosEmpresa);
                conn.SaveChanges();
                idEmpresa = datosEmpresa.ID_EMPRESA;
                if (idEmpresa > 0)
                { 
                    EMPRESA_AMPA datosRelacion = new EMPRESA_AMPA();
                    datosRelacion.ACTIVO = "S";
                    datosRelacion.USUARIO = usuarioGestor;
                    datosRelacion.ID_AMPA = idAMPA;
                    datosRelacion.ID_EMPRESA = idEmpresa;
                    conn.EMPRESA_AMPA.Add(datosRelacion);
                    conn.SaveChanges();
                }
                else
                    throw new Exception("No se ha dado de alta correctamente la empresa");
              return idEmpresa;
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error al insertar la nueva empresa.", ex);
                throw;
            }
        }
        #endregion
    }
}
