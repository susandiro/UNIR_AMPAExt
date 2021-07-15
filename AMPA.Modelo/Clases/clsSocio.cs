using System;
using System.Linq;
using System.Data.Entity;
using AMPAExt.Comun;
using System.Collections.Generic;
using AMPA.Modelo;

namespace AMPAExt.Modelo
{
    public class clsSocio
    {
        #region Altas
        /// <summary>
        /// Da de alta un socio en el AMPA
        /// </summary>
        /// <param name="socio">Datos del socio</param>
        /// <param name="conn">Conexión abierta para realizar la acción</param>
        /// <returns>Identificador del socio dado de alta en el sistema.</returns>
        public int AltaSocio(TUTOR socio, AMPAEXTBD conn)
        {
            int idTutor;
            try
            {
                conn.TUTOR.Add(socio);
                conn.SaveChanges();
                idTutor = socio.ID_TUTOR;
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error al dar de alta el socio en el AMPA", ex);
                throw;
            }
            return idTutor;
        }

        /// <summary>
        /// Da de alta un alumno
        /// </summary>
        /// <param name="alumno">Datos del alumno</param>
        /// <param name="conn">Conexión abierta para realizar la acción</param>
        /// <returns>Identificador del alumno dado de alta en el sistema.</returns>
        public int AltaAlumno(ALUMNO alumno, AMPAEXTBD conn)
        {
            int idAlumno;
            try
            {
                conn.ALUMNO.Add(alumno);
                conn.SaveChanges();
                idAlumno = alumno.ID_TUTOR;
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error al dar de alta el alumno", ex);
                throw;
            }
            return idAlumno;
        }
        #endregion

        #region Consultas
        /// <summary>
        /// Obtiene el listado de cursos
        /// </summary>
        /// <returns>Listado de <see cref="CURSO"/> con los cursos existentes</returns>
        public List<CURSO> GetCursos()
        {
            List<CURSO> resultado = new List<CURSO>();
            try
            {
                using (AMPAEXTBD db = new AMPAEXTBD())
                {
                    resultado = db.CURSO
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".GetCursos()", ex);
                throw;
            }
            return resultado;
        }
        /// <summary>
        /// Obtiene el listado de clases para un curso
        /// </summary>
        /// <param name="idCurso">Curso al que pertenecen las clases</param>
        /// <param name="idAMPA">IDentificador del AMPA al que pertenecen</param>
        /// <returns>Listado de <see cref="CURSO_CLASE"/> con las clases existentes</returns>
        public List<CURSO_CLASE> GetClases(int idCurso, int idAMPA)
        {
            List<CURSO_CLASE> resultado = new List<CURSO_CLASE>();
            try
            {
                using (AMPAEXTBD db = new AMPAEXTBD())
                {
                    resultado = db.CURSO_CLASE
                        .Where(c=>c.ID_AMPA== idAMPA && c.ID_CURSO==idCurso)
                        .Include(c=>c.CLASE)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".GetClases(). idCurso: " + idCurso.ToString() + ", idAMPA: " + idAMPA.ToString(), ex);
                throw;
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene el listado de socios que forman parte de una AMPA
        /// </summary>
        /// <param name="idAMPA">Identificador de la AMPA</param>
        /// <param name="filtro">Campos de búsqueda</param>
        /// <returns>Listado de <see cref="TUTOR"/> con los socios de la AMPA</returns>
        public List<TUTOR> GetSociosByAMPA(int idAMPA, FiltroUsuario filtro)
        {
            List<TUTOR> socios = new List<TUTOR>();
            try
            {
                using (AMPAEXTBD db = new AMPAEXTBD())
                {
                    var query = db.TUTOR
                        .Where(c => c.ID_AMPA == idAMPA)
                        .Include(c => c.TIPO_DOCUMENTO)
                        .Include(c=> c.ALUMNO);

                    if (!filtro.Vacio)
                    {
                        if (filtro.IdUsuario > 0)
                            query = query.Where(c => c.ID_TUTOR == filtro.IdUsuario);

                        if (filtro.IdTipoDocumento > 0)
                            query = query.Where(c => c.T1_ID_TIPO_DOCUMENTO == filtro.IdTipoDocumento || c.T2_ID_TIPO_DOCUMENTO == filtro.IdTipoDocumento);

                        if (!string.IsNullOrEmpty(filtro.NumeroDocumento))
                            query = query.Where(c => c.T1_NUMERO_DOCUMENTO.ToUpper().Contains(filtro.NumeroDocumento.ToUpper()) || c.T2_NUMERO_DOCUMENTO.ToUpper().Contains(filtro.NumeroDocumento.ToUpper()));
                        if (!string.IsNullOrEmpty(filtro.Telefono))
                            query = query.Where(c => c.T1_TELEFONO.ToUpper().Contains(filtro.Telefono.ToUpper()) || c.T2_TELEFONO.ToUpper().Contains(filtro.Telefono.ToUpper()));

                        if (!string.IsNullOrEmpty(filtro.Nombre))
                        {
                            string nombre = filtro.Nombre.ToUpper();
                            query = query.Where(c => c.T1_NOMBRE.ToUpper().Contains(nombre) ||
                                                c.T1_APELLIDO1.ToUpper().Contains(nombre) ||
                                                c.T1_APELLIDO2.ToUpper().Contains(nombre) ||
                                                c.T2_NOMBRE.ToUpper().Contains(nombre) ||
                                                c.T2_APELLIDO1.ToUpper().Contains(nombre) ||
                                                c.T2_APELLIDO2.ToUpper().Contains(nombre)
                                                );
                        }
                    }
                    query = query.OrderBy(c => c.ID_TUTOR);
                    socios = query.ToList();
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".GetSociosByAMPA(). idAMPA: " + idAMPA.ToString(), ex);
            }
            return socios;
        }

        /// <summary>
        /// Obtiene un determinado socio de una AMPA
        /// </summary>
        /// <param name="idUsuario">Identificador del socio a recuperar</param>
        /// <param name="idAMPA">Identificador de la AMPA</param>
        /// <returns>Datos del socio en <see cref="TUTOR"/></returns>
        public TUTOR GetSocioById(int idSocio, int idAMPA)
        {
            TUTOR socio = new TUTOR();
            try
            {
                using (AMPAEXTBD db = new AMPAEXTBD())
                {
                    socio = db.TUTOR
                      .Where(c => c.ID_TUTOR == idSocio && c.ID_AMPA == idAMPA)
                      .Include(c => c.TIPO_DOCUMENTO)
                      .Include(c => c.ALUMNO).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".GetSocioById(). idSocio: " + idSocio.ToString(), ex);
            }
            return socio;
        }

        /// <summary>
        /// Obtiene el listado de alumnos de un socio
        /// </summary>
        /// <param name="idSocio">Identificador del socio</param>
        /// <returns>Listado de <see cref="ALUMNO"/> con los alumnos</returns>
        public List<ALUMNO> GetAlumnosBySocio(int idSocio)
        {
            List<ALUMNO> resultado = new List<ALUMNO>();
            try
            {
                using (AMPAEXTBD db = new AMPAEXTBD())
                {
                    resultado = db.ALUMNO
                        .Where(c => c.ID_TUTOR == idSocio)
                        .Include(c=>c.ALUMNO_ACTIVIDAD)
                        .Include(c => c.CURSO_CLASE)
                        .Include(c => c.CURSO_CLASE.CURSO)
                        .Include(c => c.CURSO_CLASE.CLASE)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".GetAlumnosBySocio(). idSocio: " + idSocio.ToString(), ex);
                throw;
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene el listado de alumnos que pertenecen a una AMPA
        /// </summary>
        /// <param name="idAMPA">Identificador de la AMPA</param>
        /// <param name="filtro">Campos de búsqueda</param>
        /// <returns>Listado de <see cref="ALUMNO"/> con los alumnos</returns>
        public List<ALUMNO> GetAlumnosByAMPA(int idAMPA, FiltroAlumno filtro)
        {
            List<ALUMNO> resultado = new List<ALUMNO>();
            try
            {
                using (AMPAEXTBD db = new AMPAEXTBD())
                {
                    var query = db.ALUMNO
                        .Where(c => c.TUTOR.ID_AMPA == idAMPA)
                        .Include(c => c.CURSO_CLASE.CURSO)
                        .Include(c => c.CURSO_CLASE.CLASE);

                    if (!filtro.Vacio)
                    {
                        if (filtro.IdCurso > 0)
                            query = query.Where(c => c.CURSO_CLASE.ID_CURSO == filtro.IdCurso);

                        if (filtro.IdClase > 0)
                            query = query.Where(c => c.CURSO_CLASE.ID_CLASE == filtro.IdClase);

                        if (!string.IsNullOrEmpty(filtro.NumDocumentoTutor))
                            query = query.Where(c => c.TUTOR.T1_NUMERO_DOCUMENTO.ToUpper().Contains(filtro.NumDocumentoTutor.ToUpper()) || c.TUTOR.T2_NUMERO_DOCUMENTO.ToUpper().Contains(filtro.NumDocumentoTutor.ToUpper()));

                        if (!string.IsNullOrEmpty(filtro.Nombre))
                        {
                            string nombre = filtro.Nombre.ToUpper();
                            query = query.Where(c => c.NOMBRE.ToUpper().Contains(nombre) ||
                                                c.APELLIDO1.ToUpper().Contains(nombre) ||
                                                c.APELLIDO2.ToUpper().Contains(nombre));
                        }
                    }
                    resultado = query.ToList();
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".GetAlumnosByAMPA(). idAMPA: " + idAMPA.ToString(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene el listado de alumnos que pertenecen a una empresa
        /// </summary>
        /// <param name="idEmpresa">Identificador de la empresa</param>
        /// <param name="filtro">Campos de búsqueda</param>
        /// <returns>Listado de <see cref="TUTOR"/> con los alumnos</returns>
        public List<ALUMNO> GetAlumnosByEmpresa(int idEmpresa, FiltroAlumno filtro)
        {
            List<ALUMNO> resultado = new List<ALUMNO>();
            try
            {
                using (AMPAEXTBD db = new AMPAEXTBD())
                {
                    resultado = db.ALUMNO_ACTIVIDAD
                        .Where(c => c.ACTIVIDAD_HORARIO.ACTIVIDAD.ID_EMPRESA == idEmpresa)
                        .Select(c=>c.ALUMNO).Distinct()
                        .Include(c=>c.CURSO_CLASE.CURSO)
                        .Include(c=>c.CURSO_CLASE.CLASE)
                        .OrderBy(c=> c.CURSO_CLASE.CURSO.NOMBRE)
                        .ToList();

                    if (!filtro.Vacio)
                    {
                        if (filtro.IdCurso > 0)
                            resultado = resultado.Where(c => c.CURSO_CLASE.ID_CURSO == filtro.IdCurso).ToList();

                        if (filtro.IdClase > 0)
                            resultado = resultado.Where(c => c.CURSO_CLASE.ID_CLASE == filtro.IdClase).ToList();

                        if (!string.IsNullOrEmpty(filtro.NumDocumentoTutor))
                            resultado = resultado.Where(c => c.TUTOR.T1_NUMERO_DOCUMENTO.ToUpper().Contains(filtro.NumDocumentoTutor.ToUpper()) || c.TUTOR.T2_NUMERO_DOCUMENTO.ToUpper().Contains(filtro.NumDocumentoTutor.ToUpper())).ToList();

                        if (!string.IsNullOrEmpty(filtro.Nombre))
                        {
                            string nombre = filtro.Nombre.ToUpper();
                            resultado = resultado.Where(c => c.NOMBRE.ToUpper().Contains(nombre) ||
                                                c.APELLIDO1.ToUpper().Contains(nombre) ||
                                                c.APELLIDO2.ToUpper().Contains(nombre)).ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".GetAlumnosByEmpresa(). idEmpresa: " + idEmpresa.ToString(), ex);
            }
            return resultado;
        }
        #endregion

        #region Bajas
        /// <summary>
        /// Realiza la eliminación de los alumnos de un socio
        /// </summary>
        /// <param name="idSocio">Identificador del socio</param>
        /// <param name="conn">Conexión abierta para realizar la acción</param>
        /// <returns>Booleano con el resultado: true si ha ido todo bien; false en caso contrario</returns>
        public bool BajaAlumnosBySocio(int idSocio, AMPAEXTBD conn)
        {
            bool resultado = false;
            try
            {
                List<ALUMNO> alumnos = conn.ALUMNO
                   .Where(c => c.ID_TUTOR == idSocio)
                 .ToList();

                foreach (ALUMNO alumno in alumnos)
                {
                    conn.ALUMNO.Remove(alumno);
                    conn.SaveChanges();
                }
                resultado = true;
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".BajaAlumnosBySocio(). idSocio: " + idSocio.ToString(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Realiza la eliminación de un socio
        /// </summary>
        /// <param name="idSocio">Identificador del socio</param>
        /// <param name="conn">Conexión abierta para realizar la acción</param>
        /// <returns>Booleano con el resultado: true si ha ido todo bien; false en caso contrario</returns>
        public bool BajaSocio(int idSocio, AMPAEXTBD conn)
        {
            bool resultado = false;
            try
            {
                TUTOR resultadoAct = conn.TUTOR
                   .Where(c => c.ID_TUTOR == idSocio)
                 .FirstOrDefault();

                if (resultadoAct == null)
                    throw new Exception("El socio " + idSocio.ToString() + " no se encuentra");

                conn.TUTOR.Remove(resultadoAct);
                conn.SaveChanges();
                resultado = true;
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".BajaSocio(). idSocio: " + idSocio.ToString(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Realiza la eliminación de un alumno
        /// </summary>
        /// <param name="idAlumno">Identificador del alumno</param>
        /// <param name="conn">Conexión abierta para realizar la acción</param>
        /// <returns>Booleano con el resultado: true si ha ido todo bien; false en caso contrario</returns>
        public bool BajaAlumno(int idAlumno, AMPAEXTBD conn)
        {
            bool resultado = false;
            try
            {
                ALUMNO resultadoAct = conn.ALUMNO
                   .Where(c => c.ID_ALUMNO == idAlumno)
                 .FirstOrDefault();

                if (resultadoAct == null)
                    throw new Exception("El alumno " + idAlumno.ToString() + " no se encuentra");

                conn.ALUMNO.Remove(resultadoAct);
                conn.SaveChanges();
                resultado = true;
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".BajaAlumno(). idSocio: " + idAlumno.ToString(), ex);
            }
            return resultado;
        }
        #endregion

        #region Modificaciones
        /// <summary>
        /// Modifica un socio
        /// </summary>
        /// <param name="socio">Datos del socio en <see cref="TUTOR"/></param>
        /// <param name="conn">Conexión abierta para realizar la acción</param>
        /// <returns>Booleano con el resultado: true si ha ido todo bien; false en caso contrario</returns>
        public bool ModificarSocio(TUTOR socio, AMPAEXTBD conn)
        {
            bool resultado;
            try
            {
                conn.Entry(socio).State = EntityState.Modified;
                conn.SaveChanges();
                resultado = true;
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error al modificar el socio. Id_empresa: " + socio.ID_TUTOR.ToString(), ex);
                throw;
            }
            return resultado;
        }

        #endregion
    }
}
