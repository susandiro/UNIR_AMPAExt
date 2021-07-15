using System;
using System.Linq;
using System.Data.Entity;
using AMPAExt.Comun;
using System.Collections.Generic;
using AMPA.Modelo;

namespace AMPAExt.Modelo
{
    /// <summary>
    /// Realiza las llamadas relacionadas con las actividades extraescolares
    /// </summary>
    public class clsActividad
    {
        #region Consultas
        /// <summary>
        /// Obtiene la lista de actividades
        /// </summary>
        /// <param name="filtro">Campos de búsqueda</param>
        /// <returns><see cref="USUARIO_AMPA"/> con los datos de la AMPA</returns>
        public List<ACTIVIDAD> GetActividades(FiltroActividad filtro)
        {
            List<ACTIVIDAD> resultado = new List<ACTIVIDAD>();
            try
            {
                using (AMPAEXTBD db = new AMPAEXTBD())
                {
                    var query = db.ACTIVIDAD
                        .Include(c => c.ACTIVIDAD_HORARIO)
                        .Include(c => c.ACTIVIDAD_DESCUENTO)
                        .Include(c => c.AMPA)
                        .Include(c => c.EMPRESA);

                    if (!filtro.Vacio)
                    {
                        if (!string.IsNullOrEmpty(filtro.Nombre))
                            query = query.Where(c => c.NOMBRE.ToUpper().Contains(filtro.Nombre.ToUpper()));

                        if ((filtro.IdEmpresa) > 0)
                            query = query.Where(c => c.ID_EMPRESA == filtro.IdEmpresa);

                        if ((filtro.IdAMPA) > 0)
                            query = query.Where(c => c.ID_AMPA == filtro.IdAMPA);

                        if (!string.IsNullOrEmpty(filtro.Activo))
                            query = query.Where(c => c.ACTIVO == filtro.Activo);
                    }
                    query = query.OrderBy(c => c.NOMBRE);
                    resultado = query.ToList();
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".GetActividades()", ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene una determinada actividad
        /// </summary>
        /// <param name="idActividad">Identificador de la actividad</param>
        /// <returns><see cref="ACTIVIDAD"/> con los datos de la actividad</returns>
        public ACTIVIDAD GetActividadById(int idActividad)
        {
            ACTIVIDAD resultado = new ACTIVIDAD();
            try
            {
                using (AMPAEXTBD db = new AMPAEXTBD())
                {
                    resultado = db.ACTIVIDAD
                        .Where(c => c.ID_ACTIVIDAD == idActividad)
                        .Include(c => c.EMPRESA)
                        .Include(c => c.AMPA)
                        .Include(c => c.ACTIVIDAD_DESCUENTO)
                        .Include(c => c.ACTIVIDAD_HORARIO).FirstOrDefault();

                    List<ACTIVIDAD_DESCUENTO> descuento = db.ACTIVIDAD_DESCUENTO
                        .Where(c => c.ID_ACTIVIDAD == idActividad)
                        .Include(c => c.DESCUENTO).ToList();
                    resultado.ACTIVIDAD_DESCUENTO = descuento;

                    List<ACTIVIDAD_HORARIO> horario = db.ACTIVIDAD_HORARIO
                        .Where(c => c.ID_ACTIVIDAD == idActividad)
                        .Include(c => c.MONITOR).ToList();

                    resultado.ACTIVIDAD_HORARIO = horario;
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".GetActividades()", ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene la AMPA de un alumno
        /// </summary>
        /// <param name="idAlumno">Identificador del alumno</param>
        /// <returns>Datos de las AMPA <see cref="ALUMNO"/></returns>
        public ALUMNO GetAMPAByAlumno(int idAlumno)
        {
            ALUMNO resultado = new ALUMNO();
            try
            {
                using (AMPAEXTBD db = new AMPAEXTBD())
                {
                    resultado = db.ALUMNO
                        .Where(c => c.ID_ALUMNO == idAlumno)
                        .Include (c=>c.TUTOR)
                        .Include(c=>c.TUTOR.AMPA).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".GetAMPAByAlumno()", ex);
            }
            return resultado;
        }
        /// <summary>
        /// Obtiene el listado de horarios para una actividad
        /// </summary>
        /// <param name="idActividad">Identificador de la actividad</param>
        /// <returns><see cref="ACTIVIDAD"/> con los datos de la actividad</returns>
        public List<ACTIVIDAD_HORARIO> GetHorarioByActividad(int idActividad)
        {
            List<ACTIVIDAD_HORARIO> resultado = new List<ACTIVIDAD_HORARIO>();
            try
            {
                using (AMPAEXTBD db = new AMPAEXTBD())
                {
                    resultado = db.ACTIVIDAD_HORARIO
                        .Where(c => c.ID_ACTIVIDAD == idActividad)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".GetHorarioByActividad(). idActividad: " + idActividad.ToString(), ex);
            }
            return resultado;
        }
        /// <summary>
        /// Obtiene la lista de alumnos para una actividad
        /// </summary>
        /// <param name="idActividad">Identificador de la actividad/param>
        /// <returns>Listado de <see cref="ALUMNO_ACTIVIDAD"/> con los datos de la AMPA</returns>
        public List<ALUMNO_ACTIVIDAD> GetAlumnnosByActividad(int idActividad)
        {
            List<ALUMNO_ACTIVIDAD> resultado = new List<ALUMNO_ACTIVIDAD>();
            try
            {
                using (AMPAEXTBD db = new AMPAEXTBD())
                {
                    resultado =  GetAlumnnosByActividad(idActividad, db);
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".GetAlumnnosByActividad(). idActividad: " + idActividad.ToString(), ex);
            }
            return resultado;
        }
        /// <summary>
        /// Obtiene la lista de alumnos para una actividad
        /// </summary>
        /// <param name="idActividad">Identificador de la actividad/param>
        /// <param name="conn">Conexión abierta para realizar la acción</param>
        /// <returns>Listado de <see cref="ALUMNO_ACTIVIDAD"/> con los datos de la AMPA</returns>
        public List<ALUMNO_ACTIVIDAD> GetAlumnnosByActividad(int idActividad, AMPAEXTBD conn)
        {
            List<ALUMNO_ACTIVIDAD> resultado = new List<ALUMNO_ACTIVIDAD>();
            try
            {
                resultado = conn.ALUMNO_ACTIVIDAD
                    .Where(c => c.ACTIVIDAD_HORARIO.ID_ACTIVIDAD == idActividad)
                    .Include(c => c.ALUMNO)
                    .Include(c => c.ACTIVIDAD_HORARIO.ACTIVIDAD)
                    .Include(c => c.ACTIVIDAD_HORARIO).ToList();
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".GetAlumnnosByActividad(). idActividad: " + idActividad.ToString(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene la lista de de alumnos para una actividad y horario
        /// </summary>
        /// <param name="idActividadHorario">Identificador de la actividad y horario/param>
        /// <param name="conn">Conexión abierta para realizar la acción</param>
        /// <returns>Listado de <see cref="ALUMNO_ACTIVIDAD"/> apuntados a una actividad y horario</returns>
        public List<ALUMNO_ACTIVIDAD> GetAlumnosByActividadHorario(int idActividadHorario, AMPAEXTBD conn)
        {
            List<ALUMNO_ACTIVIDAD> resultado = new List<ALUMNO_ACTIVIDAD>();
            try
            {
                resultado = conn.ALUMNO_ACTIVIDAD
                    .Where(c => c.ID_ACT_HORARIO == idActividadHorario)
                    .ToList();
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".GetAlumnosByActividadHorario(). idActividad: " + idActividadHorario.ToString(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene la lista de alumnos para una actividad
        /// </summary>
        /// <param name="idActividad">Identificador de la actividad/param>
        /// <returns>Listado de <see cref="ALUMNO_ACTIVIDAD"/> con los datos de la AMPA</returns>
        public List<ALUMNO_ACTIVIDAD> GetAlumnnosByIdActividad(int idActividad)
        {
            List<ALUMNO_ACTIVIDAD> resultado = new List<ALUMNO_ACTIVIDAD>();
            try
            {
                using (AMPAEXTBD db = new AMPAEXTBD())
                {
                    resultado = db.ALUMNO_ACTIVIDAD
                        .Where(c => c.ID_ALUM_ACT == idActividad)
                        .Include(c => c.ALUMNO)
                        .Include(c => c.ACTIVIDAD_HORARIO).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".GetAlumnnosByIdActividad(). idActividad: " + idActividad.ToString(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene los datos del alumno
        /// </summary>
        /// <param name="idAlumno">Identificador del alumno/param>
        /// <returns>Datos del alumno en <see cref="ALUMNO"/></returns>
        public ALUMNO GetAlumnnoById(int idAlumno)
        {
            ALUMNO resultado = new ALUMNO();
            try
            {
                using (AMPAEXTBD db = new AMPAEXTBD())
                {
                    resultado = db.ALUMNO
                        .Where(c => c.ID_ALUMNO == idAlumno)
                        .Include(c=> c.TUTOR)
                        .Include(c => c.CURSO_CLASE.CURSO)
                        .Include(c => c.CURSO_CLASE.CLASE)
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".GetAlumnnoById(). idAlumno: " + idAlumno.ToString(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene las actividades de un alumno
        /// </summary>
        /// <param name="idAlumno">Identificador del alumno/param>
        /// <returns>Listado de actividades en <see cref="ALUMNO_ACTIVIDAD"/></returns>
        public List<ALUMNO_ACTIVIDAD> GetActividadesByAlumnno(int idAlumno)
        {
            List<ALUMNO_ACTIVIDAD> resultado = new List<ALUMNO_ACTIVIDAD>();
            try
            {
                using (AMPAEXTBD db = new AMPAEXTBD())
                {
                    resultado = db.ALUMNO_ACTIVIDAD
                        .Where(c => c.ID_ALUMNO == idAlumno)
                        .Include(c => c.ACTIVIDAD_HORARIO)
                        .Include(c => c.ACTIVIDAD_HORARIO.ACTIVIDAD)
                        .Include(c => c.ACTIVIDAD_HORARIO.ACTIVIDAD.EMPRESA)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".GetActividadesByAlumnno(). idAlumno: " + idAlumno.ToString(), ex);
                throw;
            }
            return resultado;
        }
        #endregion

        #region Altas
        /// <summary>
        /// Da de alta una actividad extraescolar
        /// </summary>
        /// <param name="actividad">Datos de la actividad</param>
        /// <param name="conn">Conexión abierta para realizar la acción</param>
        /// <returns>Identificador de la actividad dada de alta en el sistema.</returns>
        public int AltaActividad(ACTIVIDAD actividad, AMPAEXTBD conn)
        {
            int idActividad;
            try
            {
                conn.ACTIVIDAD.Add(actividad);
                conn.SaveChanges();
                idActividad = actividad.ID_ACTIVIDAD;
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error al dar de alta la actividad extraescolar. Id_empresa: " + actividad.ID_EMPRESA.ToString(), ex);
                throw;
            }
            return idActividad;
        }

        /// <summary>
        /// Da de alta horarios para la empresa extraescolar
        /// </summary>
        /// <param name="horario">Datos del horario</param>
        /// <param name="conn">Conexión abierta para realizar la acción</param>
        /// <returns>Identificador de la actividad dada de alta en el sistema.</returns>
        public int AltaHorario(ACTIVIDAD_HORARIO horario, AMPAEXTBD conn)
        {
            int idHorario;
            try
            {
                conn.ACTIVIDAD_HORARIO.Add(horario);
                conn.SaveChanges();
                idHorario = horario.ID_ACT_HORARIO;
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error al dar de alta el horario de la actividad extraescolar. Id_actividad: " + horario.ID_ACTIVIDAD.ToString(), ex);
                throw;
            }
            return idHorario;
        }

        /// <summary>
        /// Vincula un horario a un alumno
        /// </summary>
        /// <param name="horario">Datos del horario</param>
        /// <returns>Identificador de la actividad dada de alta en el sistema.</returns>
        public int AltaAlumnoHorario(ALUMNO_ACTIVIDAD horario)
        {
            using (AMPAEXTBD conn = new AMPAEXTBD())
            {
                return AltaAlumnoHorario(horario, conn);
            }
        }

        /// <summary>
        /// Vincula un horario a un alumno
        /// </summary>
        /// <param name="horario">Datos del horario</param>
        /// <param name="conn">Conexión abierta para realizar la acción</param>
        /// <returns>Identificador de la actividad dada de alta en el sistema.</returns>
        public int AltaAlumnoHorario(ALUMNO_ACTIVIDAD horario, AMPAEXTBD conn)
        {
            int idHorario;
            try
            {
                conn.ALUMNO_ACTIVIDAD.Add(horario);
                conn.SaveChanges();
                idHorario = horario.ID_ALUM_ACT;
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error al vincular un horario a un alumno", ex);
                throw;
            }
            return idHorario;
        }

        /// <summary>
        /// Da de alta descuentos para la empresa extraescolar
        /// </summary>
        /// <param name="horario">Datos del descuento</param>
        /// <param name="conn">Conexión abierta para realizar la acción</param>
        /// <returns>Identificador de la actividad dada de alta en el sistema.</returns>
        public int AltaDescuento(ACTIVIDAD_DESCUENTO descuento, AMPAEXTBD conn)
        {
            int idDescuento;
            try
            {
                conn.ACTIVIDAD_DESCUENTO.Add(descuento);
                conn.SaveChanges();
                idDescuento = descuento.ID_ACT_DESCUENTO;
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error al dar de alta el descuento de la actividad extraescolar. Id_actividad: " + descuento.ID_ACTIVIDAD.ToString(), ex);
                throw;
            }
            return idDescuento;
        }
        #endregion

        #region Modificaciones
        /// <summary>
        /// Modifica una actividad extraescolar
        /// </summary>
        /// <param name="actividad">Datos de la actividad</param>
        /// <param name="conn">Conexión abierta para realizar la acción</param>
        /// <returns>Booleano con el resultado: true si ha ido todo bien; false en caso contrario</returns>
        public bool ModificarActividad(ACTIVIDAD actividad, AMPAEXTBD conn)
        {
            bool resultado;
            try
            {
                conn.Entry(actividad).State = EntityState.Modified;
                conn.SaveChanges();
                resultado = true;
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error al modificar la actividad extraescolar. Id_actividad: " + actividad.ID_EMPRESA.ToString(), ex);
                throw;
            }
            return resultado;
        }

        /// <summary>
        /// Modifica la empresa para una actividad
        /// </summary>
        /// <param name="actividad">Datos de la actividad</param>
        /// <returns>Booleano con el resultado: true si ha ido todo bien; false en caso contrario</returns>
        public bool CambiarActividadEmpresa(ACTIVIDAD actividad)
        {
            bool resultado;
            try
            {
                using (AMPAEXTBD conn = new AMPAEXTBD())
                {
                    conn.Entry(actividad).State = EntityState.Modified;
                    conn.SaveChanges();
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error al modificar la actividad extraescolar. idActividad: " + actividad.ID_ACTIVIDAD.ToString(), ex);
                throw;
            }
            return resultado;
        }
        #endregion

        #region Bajas

        /// <summary>
        /// Realiza la eliminación de un horario de actividad
        /// </summary>
        /// <param name="idHorarioAct">Identificador del horario_actividad</param>
        /// <param name="idActividad">Identificador de la actividad</param>
        /// <param name="conn">Conexión abierta para realizar la acción</param>
        /// <returns>Booleano con el resultado: true si ha ido todo bien; false en caso contrario</returns>
        public bool BajaHorario(int idHorarioAct, AMPAEXTBD conn)
        {
            bool resultado = false;
            try
            {
                ACTIVIDAD_HORARIO horario = conn.ACTIVIDAD_HORARIO
                   .Where(c => c.ID_ACT_HORARIO == idHorarioAct)
                 .FirstOrDefault();

                if (horario == null)
                    throw new Exception("El horario " + idHorarioAct.ToString() + " no se encuentra");

                conn.ACTIVIDAD_HORARIO.Remove(horario);
                conn.SaveChanges();
                resultado = true;
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".BajaHorario(). idMonitor: " + idHorarioAct.ToString(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Realiza la eliminación de descuentos de una actividad
        /// </summary>
        /// <param name="idActividad">Identificador de la actividad</param>
        /// <param name="conn">Conexión abierta para realizar la acción</param>
        /// <returns>Booleano con el resultado: true si ha ido todo bien; false en caso contrario</returns>
        public bool BajaDescuentos(int idActividad, AMPAEXTBD conn)
        {
            bool resultado = false;
            try
            {
                List<ACTIVIDAD_DESCUENTO> descuento = conn.ACTIVIDAD_DESCUENTO
                   .Where(c => c.ID_ACTIVIDAD == idActividad)
                 .ToList();

                foreach (ACTIVIDAD_DESCUENTO desc in descuento)
                {
                    conn.ACTIVIDAD_DESCUENTO.Remove(desc);
                    conn.SaveChanges();
                }
                resultado = true;
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".BajaDescuentos(). idActividad: " + idActividad.ToString(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Realiza la eliminación de un alumno de un horario de actividad
        /// </summary>
        /// <param name="idAlumnoHorario">Identificador del horario de actividad para el alumno</param>
        /// <param name="conn">Conexión abierta para realizar la acción</param>
        /// <returns>Booleano con el resultado: true si ha ido todo bien; false en caso contrario</returns>
        public bool BajaAlumnoHorario(int idAlumnoHorario, AMPAEXTBD conn)
        {
            bool resultado = false;
            try
            {
                ALUMNO_ACTIVIDAD resultadoAct = conn.ALUMNO_ACTIVIDAD
                   .Where(c => c.ID_ALUM_ACT == idAlumnoHorario)
                 .FirstOrDefault();

                if (resultadoAct == null)
                    throw new Exception("El horario para el alumno " + idAlumnoHorario.ToString() + " no se encuentra");

                conn.ALUMNO_ACTIVIDAD.Remove(resultadoAct);
                conn.SaveChanges();
                resultado = true;
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".BajaAlumnoHorario(). idAlumnoHorario: " + idAlumnoHorario.ToString(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Da de baja un alumno de una actividad extraescolar
        /// </summary>
        /// <param name="idAlumno">Identificador del alumno</param>
        /// <param name="idActividadHorario">Identificador de la actividad</param>
        /// <returns>Booleano con el resultado: true si ha ido todo bien; false en caso contrario</returns>
        public bool BajaAlumnoActividad(int idAlumno, int idActividadHorario)
        {
            bool resultado = false;
            try
            {
                using (AMPAEXTBD conn = new AMPAEXTBD())
                {
                    ALUMNO_ACTIVIDAD alumno = conn.ALUMNO_ACTIVIDAD
                        .Where(c => c.ID_ALUMNO == idAlumno && c.ID_ACT_HORARIO == idActividadHorario)
                        .FirstOrDefault();

                    conn.ALUMNO_ACTIVIDAD.Remove(alumno);
                    conn.SaveChanges();
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".BajaAlumnoActividad(). idAlumno: " + idAlumno.ToString() + ", idActividadHorario:" + idActividadHorario.ToString(), ex);
                throw;
            }
            return resultado;
        }

        #endregion
    }
}
