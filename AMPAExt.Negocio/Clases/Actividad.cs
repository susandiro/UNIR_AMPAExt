using System;
using System.Collections.Generic;
using AMPA.Modelo;
using AMPAExt.Comun;
using System.Data.Entity;

namespace AMPAExt.Negocio
{
    public class Actividad : Comun
    {
        /// <summary>
        /// Da de alta una actividad extraescolar
        /// </summary>
        /// <param name="actividad">Datos de la actividad</param>
        /// <param name="descuento">Listado de descuentos</param>
        /// <param name="horario">Listado de horarios</param>
        /// <returns>Identificador de la actividad dada de alta en el sistema.</returns>
        public int AltaActividad(ACTIVIDAD actividad, List<ACTIVIDAD_HORARIO> horario, List<ACTIVIDAD_DESCUENTO> descuento)
        {
            int resultado = -1, idActividad;
            if (actividad != null && horario != null)
            {
                using (AMPAEXTBD conn = new AMPAEXTBD())
                {
                    using (DbContextTransaction transaccion = conn.Database.BeginTransaction())
                    {
                        try
                        {
                            //Se da de alta la actividad
                            idActividad = ActividadDat.AltaActividad(actividad, conn);
                            if (idActividad < 1)
                                throw new Exception("La actividad no ha podido ser creada");
                            actividad.ID_ACTIVIDAD = idActividad;
                            //Se da de alta los horarios
                            int idHorario;
                            foreach (ACTIVIDAD_HORARIO acth in horario)
                            {
                                acth.ID_ACTIVIDAD = idActividad;
                                idHorario = ActividadDat.AltaHorario(acth, conn);
                                if (idHorario < 0)
                                    throw new Exception("Se ha producido un error al insertar el horario en la actividad. IdActividad: " + idActividad.ToString());
                            }
                            //Se da de alta los descuentos
                            int idDescuento;
                            foreach (ACTIVIDAD_DESCUENTO actd in descuento)
                            {
                                actd.ID_ACTIVIDAD = idActividad;
                                idDescuento = ActividadDat.AltaDescuento(actd, conn);
                                if (idDescuento < 0)
                                    throw new Exception("Se ha producido un error al insertar el descuento en la actividad. IdActividad: " + idActividad.ToString());
                            }
                            transaccion.Commit();
                            resultado = idActividad;
                        }
                        catch (Exception ex)
                        {
                            transaccion.Rollback();
                            Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".AltaActividad().", ex);
                            throw;
                        }
                    }
                }
            }
            else
                throw new Exception("No se han introducido datos de la actividad y de horario");
            return resultado;
        }
        /// <summary>
        /// Vincula un horario a un alumno
        /// </summary>
        /// <param name="horario">Datos del horario</param>
        /// <returns>Identificador de la actividad dada de alta en el sistema.</returns>
        public int AltaAlumnoHorario(ALUMNO_ACTIVIDAD horario)
        {
            ALUMNO_ACTIVIDAD resultado = MapearAlumno(horario);
            return ActividadDat.AltaAlumnoHorario(resultado);
        }

        /// <summary>
        /// Obtiene la lista de actividades
        /// </summary>
        /// <param name="filtro">Campos de búsqueda</param>
        /// <returns><see cref="USUARIO_AMPA"/> con los datos de la AMPA</returns>
        public List<ACTIVIDAD> GetActividades(FiltroActividad filtro)
        {
            return ActividadDat.GetActividades(filtro);
        }

        /// <summary>
        /// Obtiene una determinada actividad
        /// </summary>
        /// <param name="idActividad">Identificador de la actividad</param>
        /// <returns><see cref="ACTIVIDAD"/> con los datos de la actividad</returns>
        public ACTIVIDAD GetActividadById(int idActividad)
        {
            return ActividadDat.GetActividadById(idActividad);
        }

        /// <summary>
        /// Obtiene la lista de alumnos para una actividad
        /// </summary>
        /// <param name="idActividad">Identificador de la actividad/param>
        /// <returns>Listado de <see cref="ALUMNO_ACTIVIDAD"/> con los datos de la AMPA</returns>
        public List<ALUMNO_ACTIVIDAD> GetAlumnnosByActividad(int idActividad)
        {
            return ActividadDat.GetAlumnnosByActividad(idActividad);
        }

        /// <summary>
        /// Obtiene el listado de horarios para una actividad
        /// </summary>
        /// <param name="idActividad">Identificador de la actividad</param>
        /// <returns><see cref="ACTIVIDAD"/> con los datos de la actividad</returns>
        public List<ACTIVIDAD_HORARIO> GetHorarioByActividad(int idActividad)
        {
            return ActividadDat.GetHorarioByActividad(idActividad);
        }

        /// <summary>
        /// Obtiene el alumno con sus actividades
        /// </summary>
        /// <param name="idAlumno">Identificador del alumno/param>
        /// <returns>Datos del alumno en <see cref="ALUMNO"/></returns>
        public ALUMNO GetAlumnnoById(int idAlumno)
        {
            return ActividadDat.GetAlumnnoById(idAlumno);
        }

        /// <summary>
        /// Obtiene las actividades de un alumno
        /// </summary>
        /// <param name="idAlumno">Identificador del alumno/param>
        /// <returns>Listado de actividades en <see cref="ALUMNO_ACTIVIDAD"/></returns>
        public List<ALUMNO_ACTIVIDAD> GetActividadesByAlumnno(int idAlumno)
        {
            return ActividadDat.GetActividadesByAlumnno(idAlumno);
        }

        /// <summary>
        /// Obtiene la AMPA de un alumno
        /// </summary>
        /// <param name="idAlumno">Identificador del alumno</param>
        /// <returns>Datos de las AMPA <see cref="ALUMNO"/></returns>
        public ALUMNO GetAMPAByAlumno(int idAlumno)
        {
            return ActividadDat.GetAMPAByAlumno(idAlumno);
        }

        /// <summary>
        /// Da de baja una actividad extraescolar
        /// </summary>
        /// <param name="actividad">Datos de la actividad en <see cref="ACTIVIDAD"/></param>
        /// <returns>Booleano con el resultado: true si ha ido todo bien; false en caso contrario</returns>
        public bool BajaActividad(ACTIVIDAD actividad)
        {
            bool resultado = false;
            List<ALUMNO_ACTIVIDAD> alumnos = ActividadDat.GetAlumnnosByActividad(actividad.ID_ACTIVIDAD);
            using (AMPAEXTBD conn = new AMPAEXTBD())
            {
                using (DbContextTransaction transaccion = conn.Database.BeginTransaction())
                {
                    try
                    {
                        //Se borra la relación entre alumnos y actividad
                        foreach (ALUMNO_ACTIVIDAD alumno in alumnos)
                        {
                            if (!ActividadDat.BajaAlumnoHorario(alumno.ID_ALUM_ACT, conn))
                                throw new Exception("No se ha podido borrar la relación con el alumno: " + alumno.ID_ALUM_ACT.ToString());
                        }
                        //Se actualiza la actividad con el usuario de acción, fecha y activo = 'N'
                        actividad.ACTIVO = "N";
                        if (!ActividadDat.ModificarActividad(actividad, conn))
                            throw new Exception("No se ha podido actualizar la actividad con activo N: " + actividad.ID_ACTIVIDAD.ToString());
                        resultado = true;
                        transaccion.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaccion.Rollback();
                        Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".BajaActividad(). IdActividad: " + actividad.ID_ACTIVIDAD.ToString(), ex);
                        throw;
                    }
                }
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
            return ActividadDat.BajaAlumnoActividad(idAlumno, idActividadHorario);
        }

        /// <summary>
        /// Modifica los datos de una actividad extraescolar: tanto la actividad, como los horarios y posible descuento
        /// </summary>
        /// <param name="actividad">Datos de la actividad</param>
        /// <param name="horario">Listado de horarios en <see cref="ACTIVIDAD_HORARIO"/></param>
        /// <param name="descuento">Listado de descuentos en <see cref="ACTIVIDAD_DESCUENTO"/></param>
        /// <returns>Booleano con el resultado: true si ha ido todo bien; false en caso contrario</returns>
        public bool ModificarActividad(ACTIVIDAD actividad, List<ACTIVIDAD_HORARIO> horario, List<ACTIVIDAD_DESCUENTO> descuento)
        {
            bool resultado = false;
            if (actividad != null && horario != null)
            {
                using (AMPAEXTBD conn = new AMPAEXTBD())
                {
                    using (DbContextTransaction transaccion = conn.Database.BeginTransaction())
                    {
                        try
                        {
                            bool resultadoAct = false;
                            //Se actualiza la actividad
                            ACTIVIDAD actividadAux = MapearActividad(actividad);
                            resultadoAct = ActividadDat.ModificarActividad(actividadAux, conn);
                            if (!resultadoAct)
                                throw new Exception("La actividad no ha podido ser modificada");

                            //Se eliminan los descuentos actuales de la actividad                            
                            if (!ActividadDat.BajaDescuentos(actividad.ID_ACTIVIDAD, conn))
                                throw new Exception("Error al eliminar los descuentos");

                            //Se insertan los nuevos descuentos
                            ACTIVIDAD_DESCUENTO descuentoAuxiliar;
                            foreach (ACTIVIDAD_DESCUENTO actDesc in descuento)
                            {
                                actDesc.ID_ACTIVIDAD = actividad.ID_ACTIVIDAD;
                                descuentoAuxiliar = MapearDescuento(actDesc);
                                if (ActividadDat.AltaDescuento(descuentoAuxiliar, conn) < 1)
                                    throw new Exception("Error al insertar el descuento");
                            }

                            ACTIVIDAD_HORARIO horarioActual;
                            List<ALUMNO_ACTIVIDAD> alumnos = new List<ALUMNO_ACTIVIDAD>();
                            int idNuevoHorario;
                            //Obtenemos el listado actual de horarios que tiene la actividad
                            List<ACTIVIDAD_HORARIO> listadoHActual = ActividadDat.GetHorarioByActividad(actividad.ID_ACTIVIDAD);
                            //Para cada uno de los existentes, se comprueba que esté o no en el listado nuevo
                            foreach (ACTIVIDAD_HORARIO actHora in listadoHActual)
                            {
                                horarioActual = horario.Find(c=>c.DIAS == actHora.DIAS && c.HORA_INI == actHora.HORA_INI && c.HORA_FIN == actHora.HORA_FIN && c.ID_MONITOR == actHora.ID_MONITOR);
                                if (horarioActual != null)
                                    horario.Remove(horarioActual);
                                else //Si no se encuentra entre los nuevos, se eliminan
                                {
                                    //Primero se elimina los alumnos y luego los horarios
                                    //Se recuperan los alumnos vinculados al horario
                                    alumnos = ActividadDat.GetAlumnnosByIdActividad(actHora.ID_ACT_HORARIO);

                                    //Se elimina el vínculo entre los usuarios encontrados y el horario
                                    foreach (ALUMNO_ACTIVIDAD alumAct in alumnos)
                                        if (!ActividadDat.BajaAlumnoHorario(alumAct.ID_ALUM_ACT, conn))
                                            throw new Exception("Error al eliminar la relación entre el horario y el alumno existente: " + alumAct.ID_ALUM_ACT.ToString());
                                    //Se elimina el horario
                                    if (!ActividadDat.BajaHorario(actHora.ID_ACT_HORARIO, conn))
                                        throw new Exception("Error al eliminar el horario existente: " + horarioActual.ID_ACT_HORARIO.ToString());
                                }
                            }

                            //Con los horarios de nueva inserción, se insertan
                            foreach (ACTIVIDAD_HORARIO actHorario in horario)
                            {
                                actHorario.ID_ACTIVIDAD = actividad.ID_ACTIVIDAD;
                                //Se inserta el nuevo horario
                                horarioActual = MapearHorario(actHorario);
                                idNuevoHorario = ActividadDat.AltaHorario(horarioActual, conn);
                            }
                            transaccion.Commit();
                            resultado = true;
                        }
                        catch (Exception ex)
                        {
                            transaccion.Rollback();
                            Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".ModificarActividad().", ex);
                            throw;
                        }
                    }
                }
            }
            else
                throw new Exception("No se han introducido datos de la actividad y/o de horario");
            return resultado;
        }

        /// <summary>
        /// Modifica la empresa para una actividad
        /// </summary>
        /// <param name="actividad">Datos de la actividad</param>
        /// <returns>Booleano con el resultado: true si ha ido todo bien; false en caso contrario</returns>
        public bool CambiarActividadEmpresa(ACTIVIDAD actividad)
        {
            ACTIVIDAD resultado = MapearActividad(actividad);
            return ActividadDat.CambiarActividadEmpresa(resultado);
        }

        /// <summary>
        /// Intercambia los alumnos de una actividad extraescolar (actividad y horario) a otra actividad extraescolar
        /// </summary>
        /// <param name="idActividadHorarioOrigen">Identificador de la actividad y horario de origen</param>
        /// <param name="idActividadHorarioDestino">Identificador de la actividad y horario de destino</param>
        /// <param name="usuarioAccion">Usuario que realiza la acción</param>
        /// <returns>Booleano con el resultado: true si ha ido todo bien; false en caso contrario</returns>
        public bool IntercambiarEmpresa(int idActividadHorarioOrigen, int idActividadHorarioDestino, string usuarioAccion)
        {
            bool resultado = false;
            using (AMPAEXTBD conn = new AMPAEXTBD())
            {
                using (DbContextTransaction transaccion = conn.Database.BeginTransaction())
                {
                    try
                    {
                        //Se guardan los alumnos que hay en origen
                        List<ALUMNO_ACTIVIDAD> alumnosOrigen = ActividadDat.GetAlumnosByActividadHorario(idActividadHorarioOrigen, conn);
                        //Se actualizan los alumnos de origen en actividad y horario de destino
                        foreach (ALUMNO_ACTIVIDAD alumno in alumnosOrigen)
                        {
                            alumno.ID_ACT_HORARIO = idActividadHorarioDestino;
                            alumno.USUARIO = usuarioAccion;
                            alumno.FECHA_MOD = DateTime.Now;
                        }
                        
                        conn.SaveChanges();
                        transaccion.Commit();
                        resultado = true;
                    }
                    catch (Exception ex)
                    {
                        transaccion.Rollback();
                        Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".IntercambiarEmpresa(). idActividadHorarioOrigen: " + idActividadHorarioOrigen.ToString() + ", idActividadHorarioDestino: " + idActividadHorarioDestino.ToString() + ", usuarioAccion: " + usuarioAccion, ex);
                        throw;
                    }
                    return resultado;
                }
            }
        }
        /// <summary>
        /// Mapea una actividad extraescolar
        /// </summary>
        /// <param name="actividad">Datos a Mapear</param>
        /// <returns>Objeto <see cref="ACTIVIDAD"/> con los datos de la actividad</returns>
        private ACTIVIDAD MapearActividad(ACTIVIDAD actividad)
            {
            ACTIVIDAD resultado = new ACTIVIDAD();
            resultado.ACTIVO = actividad.ACTIVO;
            resultado.DESCRIPCION = actividad.DESCRIPCION;
            resultado.FECHA = actividad.FECHA;
            resultado.FECHA_MOD = actividad.FECHA;
            resultado.ID_ACTIVIDAD = actividad.ID_ACTIVIDAD;
            resultado.ID_AMPA = actividad.ID_AMPA;
            resultado.ID_EMPRESA = actividad.ID_EMPRESA;
            resultado.NOMBRE = actividad.NOMBRE;
            resultado.OBSERVACIONES = actividad.OBSERVACIONES;
            resultado.USUARIO = actividad.USUARIO;
            return resultado;
        }
        /// <summary>
        /// Mapea un horario
        /// </summary>
        /// <param name="objeto">Datos a Mapear</param>
        /// <returns>Objeto <see cref="ACTIVIDAD_HORARIO"/> con los datos</returns>
        private ACTIVIDAD_HORARIO MapearHorario(ACTIVIDAD_HORARIO objeto)
        {
            ACTIVIDAD_HORARIO resultado = new ACTIVIDAD_HORARIO();
            resultado.CUOTA = objeto.CUOTA;
            resultado.DIAS = objeto.DIAS;
            resultado.FECHA = objeto.FECHA;
            resultado.FECHA_MOD = objeto.FECHA_MOD;
            resultado.HORA_FIN = objeto.HORA_FIN;
            resultado.HORA_INI = objeto.HORA_INI;
            resultado.ID_ACTIVIDAD = objeto.ID_ACTIVIDAD;
            resultado.ID_ACT_HORARIO = objeto.ID_ACT_HORARIO;
            resultado.ID_MONITOR = objeto.ID_MONITOR;
            resultado.USUARIO = objeto.USUARIO;
            return resultado;
        }

        /// <summary>
        /// Mapea un descuento
        /// </summary>
        /// <param name="objeto">Datos a Mapear</param>
        /// <returns>Objeto <see cref="ACTIVIDAD_DESCUENTO"/> con los datos</returns>
        private ACTIVIDAD_DESCUENTO MapearDescuento(ACTIVIDAD_DESCUENTO objeto)
        {
            ACTIVIDAD_DESCUENTO resultado = new ACTIVIDAD_DESCUENTO();
            resultado.FECHA = objeto.FECHA;
            resultado.FECHA_MOD = objeto.FECHA_MOD;
            resultado.ID_ACTIVIDAD = objeto.ID_ACTIVIDAD;
            resultado.ID_ACT_DESCUENTO = objeto.ID_ACT_DESCUENTO;
            resultado.ID_DESCUENTO = objeto.ID_DESCUENTO;
            resultado.USUARIO = objeto.USUARIO;
            resultado.VALOR = objeto.VALOR;
            return resultado;
        }

        /// <summary>
        /// Mapea un alumno en actividad
        /// </summary>
        /// <param name="objeto">Datos a Mapear</param>
        /// <returns>Objeto <see cref="ALUMNO_ACTIVIDAD"/> con los datos</returns>
        private ALUMNO_ACTIVIDAD MapearAlumno(ALUMNO_ACTIVIDAD objeto)
        {
            ALUMNO_ACTIVIDAD resultado = new ALUMNO_ACTIVIDAD();
            resultado.ID_ACT_HORARIO = objeto.ID_ACT_HORARIO;
            resultado.ID_ALUMNO = objeto.ID_ALUMNO;
            resultado.ID_ALUM_ACT = objeto.ID_ALUM_ACT;
            resultado.FECHA = objeto.FECHA;
            resultado.FECHA_MOD = objeto.FECHA_MOD;
            resultado.USUARIO = objeto.USUARIO;
            return resultado;
        }
        
    }
}
