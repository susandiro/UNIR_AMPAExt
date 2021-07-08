using System;
using System.Linq;
using System.Data.Entity;
using AMPAExt.Comun;
using System.Collections.Generic;
using AMPA.Modelo;

namespace AMPAExt.Modelo
{
    public class ClsExtraescolar
    {
        #region Altas
        /// <summary>
        /// Da de alta un usuario para una empresa extraescolar
        /// </summary>
        /// <param name="usuario">Datos del usuario</param>
        /// <returns>Identificador del usuario dado de alta en el sistema.</returns>
        public int AltaUsuarioEmpresa(USUARIO_EMPRESA usuario)
        {
            using (AMPAEXTBD conn = new AMPAEXTBD())
            {
                return AltaUsuarioEmpresa(usuario, conn);
            }
        }

        /// <summary>
        /// Da de alta un usuario para una empresa extraescolar
        /// </summary>
        /// <param name="usuario">Datos del usuario</param>
        /// <param name="conn">Conexión abierta para realizar la acción</param>
        /// <returns>Identificador del usuario dado de alta en el sistema.</returns>
        public int AltaUsuarioEmpresa(USUARIO_EMPRESA usuario, AMPAEXTBD conn)
        {
            int idUsuario;
            try
            {
                USUARIO_EMPRESA nuevoUsuario = conn.USUARIO_EMPRESA.Where(d => d.NUMERO_DOCUMENTO == usuario.NUMERO_DOCUMENTO &&
                d.ID_EMPRESA == usuario.ID_EMPRESA).FirstOrDefault();
                //Si existe el usuario, se devuelve el identificador del usuario
                if (nuevoUsuario != null)
                    idUsuario = nuevoUsuario.ID_USUARIO_EMP;
                else
                {
                    conn.USUARIO_EMPRESA.Add(usuario);
                    conn.SaveChanges();
                    idUsuario = usuario.ID_USUARIO_EMP;
                }
                return idUsuario;
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error al dar de alta al usuario con la empresa. Id_empresa: " + usuario.ID_EMPRESA.ToString(), ex);
                throw;
            }
        }

        /// <summary>
        /// Da de alta un monitor para una empresa extraescolar
        /// </summary>
        /// <param name="datosMonitor">Datos del monitor</param>
        /// <returns>Identificador del monitor dado de alta en el sistema.</returns>
        public int AltaMonitor(MONITOR datosMonitor)
        {
            int idMonitor;
            try
            {
                using (AMPAEXTBD conn = new AMPAEXTBD())
                {
                    MONITOR nuevo = conn.MONITOR.Where(d => d.NUMERO_DOCUMENTO == datosMonitor.NUMERO_DOCUMENTO &&
                    d.ID_EMPRESA == datosMonitor.ID_EMPRESA).FirstOrDefault();
                    //Si existe el monitor, se devuelve el identificador del usuario
                    if (nuevo != null)
                        idMonitor = nuevo.ID_MONITOR;
                    else
                    {
                        conn.MONITOR.Add(datosMonitor);
                        conn.SaveChanges();
                        idMonitor = datosMonitor.ID_MONITOR;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error al dar de alta al monitor. Id_empresa: " + datosMonitor.ID_EMPRESA.ToString(), ex);
                throw;
            }
            return idMonitor;
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
            USUARIO_EMPRESA usuario = new USUARIO_EMPRESA();
            try
            {
                using (AMPAEXTBD db = new AMPAEXTBD())
                {
                    usuario = db.USUARIO_EMPRESA
                        .Where(c => c.ID_USUARIO_EMP == idUsuario && c.ID_EMPRESA == idEmpresa)
                        .Include(c => c.TIPO_DOCUMENTO)
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".GetUsuarioByIdEmpresa(). idUsuario: " + idUsuario.ToString() + ", idEmpresa: " + idEmpresa.ToString(), ex);
                throw;
            }
            return usuario;
        }

        /// <summary>
        /// Obtiene un determinado monitor de una empresa
        /// </summary>
        /// <param name="idMonitor">Identificador del monitor a recuperar</param>
        /// <param name="idEmpresa">Identificador de la empresa</param>
        /// <returns>Datos del usuario en <see cref="MONITOR"/></returns>
        public MONITOR GetMonitorById(int idMonitor, int idEmpresa)
        {
            MONITOR resultado = new MONITOR();
            try
            {
                using (AMPAEXTBD db = new AMPAEXTBD())
                {
                    resultado = db.MONITOR
                        .Where(c => c.ID_MONITOR == idMonitor && c.ID_EMPRESA == idEmpresa)
                        .Include(c => c.TIPO_DOCUMENTO)
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".GetMonitorById(). idMonitor: " + idMonitor.ToString() + ", idEmpresa: " + idEmpresa.ToString(), ex);
                throw;
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene el listado de usuarios de tipo empresa extraescolar
        /// </summary>
        /// <param name="filtro">Campos de búsqueda</param>
        /// <returns>Listado de <see cref="USUARIO_EMPRESA"/> con los usuarios</returns>
        public List<USUARIO_EMPRESA> GetUsuarios(FiltroUsuario filtro)
        {
            List<USUARIO_EMPRESA> usuarios = new List<USUARIO_EMPRESA>();
            try
            {
                using (AMPAEXTBD db = new AMPAEXTBD())
                {
                    var query = db.USUARIO_EMPRESA
                        .Include(c => c.TIPO_DOCUMENTO)
                        .Include(c => c.EMPRESA);

                    if (!filtro.Vacio)
                    {
                        if (filtro.IdTipoDocumento > 0)
                            query = query.Where(c => c.ID_TIPO_DOCUMENTO == filtro.IdTipoDocumento);

                        if (!string.IsNullOrEmpty(filtro.NumeroDocumento))
                            query = query.Where(c => c.NUMERO_DOCUMENTO.ToUpper().Contains(filtro.NumeroDocumento.ToUpper()));
                        if (!string.IsNullOrEmpty(filtro.Telefono))
                            query = query.Where(c => c.TELEFONO.ToUpper().Contains(filtro.Telefono.ToUpper()));
                        if (filtro.IdEmpresa > 0)
                            query = query.Where(c => c.ID_EMPRESA == filtro.IdEmpresa);
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
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".GetUsuarios()", ex);
            }
            return usuarios;
        }

        /// <summary>
        /// Obtiene un determinado usuario 
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario a recuperar</param>
        /// <returns>Datos del usuario en <see cref="USUARIO_EMPRESA"/></returns>
        public USUARIO_EMPRESA GetUsuarioById(int idUsuario)
        {
            USUARIO_EMPRESA usuario = new USUARIO_EMPRESA();
            try
            {
                using (AMPAEXTBD db = new AMPAEXTBD())
                {
                    usuario = db.USUARIO_EMPRESA
                        .Where(c => c.ID_USUARIO_EMP == idUsuario)
                        .Include(c => c.TIPO_DOCUMENTO)
                        .Include(c => c.EMPRESA)
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".GetUsuarioById(). idUsuario: " + idUsuario.ToString(), ex);
                throw;
            }
            return usuario;
        }

        /// <summary>
        /// Recupera los datos de una empresa
        /// </summary>
        /// <param name="idEmpresa">Identificador de la empresa a recuperar</param>
        /// <returns>Datos de la empresa en <see cref="EMPRESA"></see> </returns>
        public EMPRESA GetEmpresabyId(int idEmpresa)
        {
            EMPRESA empresa = new EMPRESA();
            try
            {
                using (AMPAEXTBD db = new AMPAEXTBD())
                {
                    empresa = db.EMPRESA
                        .Where(c => c.ID_EMPRESA == idEmpresa)
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".GetEmpresabyId(). idEmpresa: " + idEmpresa.ToString(), ex);
                throw;
            }
            return empresa;
        }

        /// <summary>
        /// Recupera las empresas que están asociadas a una AMPA
        /// </summary>
        /// <param name="idAMPA">Identificador de la AMPA a la que pertenecen las empresas</param>
        /// <param name="filtro">Campos de búsqueda</param>
        /// <returns>Listado de las empresas en <see cref="EMPRESA_AMPA"></see> </returns>
        public List<EMPRESA_AMPA> GetEmpresasbyIdAMPA(int idAMPA, FiltroUsuario filtro)
        {
            List<EMPRESA_AMPA> empresas = new List<EMPRESA_AMPA>();
            try
            {
                using (AMPAEXTBD db = new AMPAEXTBD())
                {
                    var query = db.EMPRESA_AMPA
                        .Where(c => c.ID_AMPA == idAMPA)
                        .Include(c => c.EMPRESA);

                    if (!filtro.Vacio)
                    {
                        if (!string.IsNullOrEmpty(filtro.NumeroDocumento))
                            query = query.Where(c => c.EMPRESA.NIF.ToUpper().Contains(filtro.NumeroDocumento.ToUpper()));
                        if (!string.IsNullOrEmpty(filtro.Nombre))
                            query = query.Where(c => c.EMPRESA.NOMBRE.ToUpper().Contains(filtro.Nombre.ToUpper()));

                        if (!string.IsNullOrEmpty(filtro.Activo))
                        {
                            query = query.Where(c => c.ACTIVO == filtro.Activo);
                        }
                    }
                    query = query.OrderBy(c => c.EMPRESA.NOMBRE);
                    empresas = query.ToList();
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".GetEmpresasbyIdAMPA()", ex);
            }
            return empresas;
        }

        /// <summary>
        /// Obtiene el listado de monitores de una empresa
        /// </summary>
        /// <param name="idEmpresa">Identificador de la empresa a la que pertenecen los monitores</param>
        /// <returns>Listado de <see cref="MONITOR"/> con los monitores</returns>
        public List<MONITOR> GetMonitores(int idEmpresa)
        {
            List<MONITOR> resultado = new List<MONITOR>();
            try
            {
                using (AMPAEXTBD db = new AMPAEXTBD())
                {
                    resultado = db.MONITOR
                        .Where(c => c.ID_EMPRESA == idEmpresa)
                        .Include(c=> c.TIPO_DOCUMENTO)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".GetMonitores(). idEmpresa: " + idEmpresa.ToString(), ex);
                throw;
            }
            return resultado;
        }
        #endregion

        #region Modificaciones
        /// <summary>
        /// Realiza la modificación de un usuario de tipo extraescolar
        /// </summary>
        /// <param name="usuario">Datos del usuario a modificar</param>
        /// <returns>Booleano con el resultado: true si ha ido todo bien; false en caso contrario</returns>
        public bool ModificarUsuarioExtr(USUARIO_EMPRESA usuario)
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
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".ModificarUsuarioExtr()", ex);
                throw;
            }
            return resultado;
        }

        /// <summary>
        /// Realiza la modificación de un monitor
        /// </summary>
        /// <param name="datos">Datos del monitor a modificar</param>
        /// <returns>Booleano con el resultado: true si ha ido todo bien; false en caso contrario</returns>
        public bool ModificarMonitor(MONITOR datos)
        {
            bool resultado = false;
            try
            {
                using (AMPAEXTBD conn = new AMPAEXTBD())
                {
                    conn.Entry(datos).State = EntityState.Modified;
                    conn.SaveChanges();
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".ModificarMonitor()", ex);
                throw;
            }
            return resultado;
        }

        /// <summary>
        /// Realiza la modificación de una empresa
        /// </summary>
        /// <param name="empresa">Datos de la empresa</param>
        /// <returns>Booleano con el resultado: true si ha ido todo bien; false en caso contrario</returns>
        public bool ModificarEmpresa(EMPRESA empresa)
        {
            bool resultado = false;
            try
            {
                using (AMPAEXTBD conn = new AMPAEXTBD())
                {
                    conn.Entry(empresa).State = EntityState.Modified;
                    conn.SaveChanges();
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".ModificarEmpresa()", ex);
                throw;
            }
            return resultado;
        }

        #endregion

        #region Bajas
        /// <summary>
        /// Realiza la eliminación de un usuario de una empresa
        /// </summary>
        /// <param name="idUsuario">Identificador de usuario</param>
        /// <param name="idEmpresa">Identificador de la empresa</param>
        /// <returns>Booleano con el resultado: true si ha ido todo bien; false en caso contrario</returns>
        public bool BajaUsuario(int idUsuario, int idEmpresa)
        {
            bool resultado = false;
            try
            {
                using (AMPAEXTBD conn = new AMPAEXTBD())
                {
                    USUARIO_EMPRESA usuario = conn.USUARIO_EMPRESA
                       .Where(c => c.ID_USUARIO_EMP == idUsuario && c.ID_EMPRESA == idEmpresa)
                     .FirstOrDefault();

                    if (usuario == null)
                        throw new Exception("El usuario " + idUsuario.ToString() + " no se encuentra como usuario de la empresa " + idEmpresa.ToString());

                    conn.USUARIO_EMPRESA.Remove(usuario);
                    conn.SaveChanges();
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".BajaUsuario(). idUsuario: " + idUsuario.ToString() + ", idEmpresa: " + idEmpresa.ToString(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Realiza la eliminación de un monitor de una empresa
        /// </summary>
        /// <param name="idMonitor">Identificador de monitor</param>
        /// <param name="idEmpresa">Identificador de la empresa</param>
        /// <returns>Booleano con el resultado: true si ha ido todo bien; false en caso contrario</returns>
        public bool BajaMonitor(int idMonitor, int idEmpresa)
        {
            bool resultado = false;
            try
            {
                using (AMPAEXTBD conn = new AMPAEXTBD())
                {
                    MONITOR monitor = conn.MONITOR
                       .Where(c => c.ID_MONITOR == idMonitor && c.ID_EMPRESA == idEmpresa)
                     .FirstOrDefault();

                    if (monitor == null)
                        throw new Exception("El usuario " + idMonitor.ToString() + " no se encuentra como monitor de la empresa " + idEmpresa.ToString());

                    conn.MONITOR.Remove(monitor);
                    conn.SaveChanges();
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".BajaMonitor(). idMonitor: " + idMonitor.ToString() + ", idEmpresa: " + idEmpresa.ToString(), ex);
            }
            return resultado;
        }
        #endregion
    }
}
