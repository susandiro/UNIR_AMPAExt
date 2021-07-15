using System;
using System.Collections.Generic;
using AMPA.Modelo;
using AMPAExt.Comun;
using System.Data.Entity;

namespace AMPAExt.Negocio
{
    public class Administracion : Comun
    {
        /// <summary>
        /// Realiza el login de un usuario al sistema. Si el login y passoword introducidos son correctos, devuelve los permisos de <see cref="USUARIO_EMPRESA"/>
        /// </summary>
        /// <param name="usuario">usuario de login para el acceso</param>
        /// <param name="password">password para el acceso</param>
        /// <param name="tipoUsuario">Tipo de usuario que realiza el login</param>
        /// <returns>Datos con el permiso del usuario <see cref="UsuarioConexion"/></returns>
        public UsuarioConexion Login(string usuario, string password, TipoDatos.TipoUsuario tipoUsuario)
        {
            UsuarioConexion datosUsuario = new UsuarioConexion();
            try
            {
                switch (tipoUsuario)
                {
                    case TipoDatos.TipoUsuario.AMPA:
                        USUARIO_AMPA usuarioSistema = UsuarioDat.GetUsuarioAMPA(usuario, password);
                        //Si el usuario existe y es correcto, se obtiene los datos del AMPA
                        if (usuarioSistema != null && usuarioSistema.ID_USUARIO > 0)
                        {
                            datosUsuario.IdUsuario = usuarioSistema.ID_USUARIO;
                            datosUsuario.CodTipoUsuario = TipoDatos.TipoUsuario.AMPA;
                            datosUsuario.NombreCompleto = usuarioSistema.NOMBRE + " " + usuarioSistema.APELLIDO1 + " " + usuarioSistema.APELLIDO2;
                            datosUsuario.DatosUsuario = usuarioSistema.NUMERO_DOCUMENTO + ": " + datosUsuario.NombreCompleto;
                            //Se obtienen los datos de la AMPA
                            USUARIO_AMPA ampa = UsuarioDat.GetAMPAbyIdUsuario(usuarioSistema.ID_USUARIO);
                            if (ampa != null && ampa.AMPA != null)
                            {
                                datosUsuario.Empresa = ampa.AMPA.NOMBRE;
                                datosUsuario.IdEmpresa = ampa.AMPA.ID_AMPA;
                                datosUsuario.ListaIdEmpresa = new List<int>();
                                foreach (EMPRESA_AMPA empAmp in ampa.AMPA.EMPRESA_AMPA)
                                    datosUsuario.ListaIdEmpresa.Add(empAmp.ID_AMPA);
                            }
                            else
                                throw new Exception("Error al recuperar los permisos del usuario AMPA. No se han encontrado datos de empresa");
                        }
                        break;
                    case TipoDatos.TipoUsuario.EXTR:
                        USUARIO_EMPRESA usuarioExtr = UsuarioDat.GetUsuarioExtraescolar(usuario, password);
                        //Si el usuario existe y es correcto, se obtiene los datos del AMPA
                        if (usuarioExtr != null && usuarioExtr.ID_USUARIO_EMP > 0)
                        {

                            datosUsuario.IdUsuario = usuarioExtr.ID_USUARIO_EMP;
                            datosUsuario.CodTipoUsuario = TipoDatos.TipoUsuario.EXTR;
                            datosUsuario.NombreCompleto = usuarioExtr.NOMBRE + " " + usuarioExtr.APELLIDO1 + " " + usuarioExtr.APELLIDO2;
                            datosUsuario.DatosUsuario = usuarioExtr.NUMERO_DOCUMENTO + ": " + datosUsuario.NombreCompleto;
                            //Se obtienen los datos de la empresa
                            USUARIO_EMPRESA empresa = UsuarioDat.GetEmpresabyIdUsuario(usuarioExtr.ID_USUARIO_EMP);
                            if (empresa != null && empresa.EMPRESA != null && empresa.EMPRESA.EMPRESA_AMPA != null)
                            {
                                datosUsuario.Empresa = empresa.EMPRESA.NOMBRE;
                                datosUsuario.IdEmpresa = empresa.EMPRESA.ID_EMPRESA;
                                datosUsuario.ListaIdEmpresa = new List<int>();
                                foreach (EMPRESA_AMPA empAmp in empresa.EMPRESA.EMPRESA_AMPA)
                                    datosUsuario.ListaIdEmpresa.Add(empAmp.ID_AMPA);
                            }
                            else
                                throw new Exception("Error al recuperar los permisos del usuario extraescolar. No se han encontrado datos de AMPA");
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".Login(). Usuario: " + usuario + ", tipoUsuario: " + tipoUsuario.ToString(), ex);
                throw;
            }
            return datosUsuario;
        }
        /// <summary>
        /// Realiza un alta de usuario de tipo AMPA
        /// </summary>
        /// <param name="datosUsuario">Datos del usuario que se va a dar de alta</param>
        /// <returns>Identificador del nuevo usuario</returns>
        public int AltaUsuarioAMPA(USUARIO_AMPA datosUsuario)
        {
            int resultado;
            try
            {
                //Se asigna la contraseña y login al usuario
                datosUsuario.LOGIN = datosUsuario.NUMERO_DOCUMENTO;
                datosUsuario.PASSWORD = Utilidades.GetSHA256(datosUsuario.NUMERO_DOCUMENTO);
                resultado = UsuarioDat.AltaUsuarioAMPA(datosUsuario);
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".AltaUsuarioAMPA() Para la AMPA: " + datosUsuario.ID_AMPA.ToString(), ex);
                throw;
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene el listado de empresas que pertenecen a una AMPA
        /// </summary>
        /// <param name="idAMPA">Identificador de la AMPA</param>
        /// <param name="soloActivas">True si sólo las que se encuentren activas. False para todas</param>
        /// <returns>Listado de <see cref="EMPRESA_AMPA"/> con las empresas que pertenecen a la AMPA</returns>
        public List<EMPRESA_AMPA> GetEmpresasByAMPA(int idAMPA, bool soloActivas = false)
        {
            return UsuarioDat.GetEmpresasByAMPA(idAMPA, soloActivas);
        }

        /// <summary>
        /// Obtiene el listado de AMPAS asociadas a una empresa. Sólo las activas
        /// </summary>
        /// <param name="idEmpresa">Identificador de la empresa extraescolar</param>
        /// <returns>Listado de <see cref="EMPRESA_AMPA"/> con las AMPAS asociadas a una empresa</returns>
        public List<EMPRESA_AMPA> GetAMPASByEmpresa(int idEmpresa)
        {
            return UsuarioDat.GetAMPASByEmpresa(idEmpresa);
        }

        /// <summary>
        /// Obtiene un determinado usuario de una AMPA
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario a recuperar</param>
        /// <param name="idAMPA">Identificador de la AMPA</param>
        /// <returns>Datos del usuario en <see cref="USUARIO_AMPA"/></returns>
        public USUARIO_AMPA GetUsuarioByIdAMPA(int idUsuario, int idAMPA)
        {
            return UsuarioDat.GetUsuarioByIdAMPA(idUsuario, idAMPA);
        }

        /// <summary>
        /// Obtiene el listado de usuario que forman parte de una AMPA
        /// </summary>
        /// <param name="idAMPA">Identificador de la AMPA</param>
        /// <param name="filtro">Campos de búsqueda</param>
        /// <returns>Listado de <see cref="USUARIO_AMPA"/> con los usuario de la AMPA</returns>
        public List<USUARIO_AMPA> GetUsuariosByAMPA(int idAMPA, FiltroUsuario filtro)
        {
            return UsuarioDat.GetUsuariosByAMPA(idAMPA, filtro);
        }

        /// <summary>
        /// Realiza la eliminación de un usuario de tipo AMPA
        /// </summary>
        /// <param name="idUsuario">Identificador de usuario</param>
        /// <param name="idAMPA">Identificador de la AMPA</param>
        /// <returns>Booleano con el resultado: true si ha ido todo bien; false en caso contrario</returns>
        public bool BajaUsuarioAMPA(int idUsuario, int idAMPA)
        {
            return UsuarioDat.BajaUsuarioAMPA(idUsuario, idAMPA);
        }

        /// <summary>
        /// Realiza la modificación de un usuario de tipo AMPA
        /// </summary>
        /// <param name="usuario">Datos del usuario a modificar</param>
        /// <returns>Booleano con el resultado: true si ha ido todo bien; false en caso contrario</returns>
        public bool ModificarUsuarioAMPA(USUARIO_AMPA usuario)
        {
            USUARIO_AMPA datosUsuario = GetUsuarioByIdAMPA(usuario.ID_USUARIO, usuario.ID_AMPA);
            usuario.LOGIN = datosUsuario.LOGIN;
            usuario.PASSWORD = datosUsuario.PASSWORD;
            usuario.FECHA = datosUsuario.FECHA;
            return UsuarioDat.ModificarUsuarioAMPA(usuario);
        }

    }
}
