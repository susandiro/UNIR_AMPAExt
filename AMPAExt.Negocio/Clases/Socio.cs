using System;
using System.Collections.Generic;
using AMPA.Modelo;
using AMPAExt.Comun;
using System.Data.Entity;

namespace AMPAExt.Negocio
{
    public class Socio : Comun 
    {
        /// <summary>
        /// Obtiene el listado de descuentos posibles
        /// </summary>
        /// <returns>Listado de <see cref="CURSO"/> con los cursos existentes</returns>
        public List<CURSO> GetCursos()
        {
            return SocioDat.GetCursos();        
        }

        /// <summary>
        /// Obtiene el listado de clases para un curso
        /// </summary>
        /// <param name="idCurso">Curso al que pertenecen las clases</param>
        /// <param name="idAMPA">IDentificador del AMPA al que pertenecen</param>
        /// <returns>Listado de <see cref="CURSO_CLASE"/> con las clases existentes</returns>
        public List<CURSO_CLASE> GetClases(int idCurso, int idAMPA)
        {
            return SocioDat.GetClases(idCurso, idAMPA);
        }

        /// <summary>
        /// Da de alta un nuevo socio en la AMPA
        /// </summary>
        /// <param name="socio">Datos del tutor</param>
        /// <param name="alumnos">Listado de alumnos</param>
        /// <returns>Identificador del socio</returns>
        public int AltaSocio(TUTOR socio, List<ALUMNO> alumnos)
        {
            int numSocio = -1;
            if (socio != null && alumnos != null && alumnos.Count >0)
            {
                using (AMPAEXTBD conn = new AMPAEXTBD())
                {
                    using (DbContextTransaction transaccion = conn.Database.BeginTransaction())
                    {
                        try
                        {
                            //Se da de alta los tutores
                            numSocio = SocioDat.AltaSocio(socio, conn);
                            if (numSocio < 1)
                                throw new Exception("El tutor no ha podido ser dado de alta");
                            
                            //Se da de alta los alumnos
                            int idAlumno;
                            ALUMNO nuevoAlumno ;
                            foreach (ALUMNO acth in alumnos)
                            {
                                acth.ID_TUTOR = numSocio;
                                nuevoAlumno = MapearAlumno(acth);
                                idAlumno= SocioDat.AltaAlumno(nuevoAlumno, conn);
                                if (idAlumno < 0)
                                    throw new Exception("Se ha producido un error al dar de alta al alumno.");
                            }
                            transaccion.Commit();
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
                throw new Exception("No se han recuperado correctamente los datos de los tutores y/o alumnos");
            return numSocio;
        }

        /// <summary>
        /// Modifica un nuevo socio en la AMPA
        /// </summary>
        /// <param name="socio">Datos del tutor</param>
        /// <param name="alumnos">Listado de alumnos</param>
        /// <returns>Booleano con el resultado: true si ha ido todo bien; false en caso contrario</returns>
        public bool ModificarSocio(TUTOR socio, List<ALUMNO> alumnos)
        {
            bool resultado = false;
            if (socio != null && alumnos != null && alumnos.Count > 0)
            {
                using (AMPAEXTBD conn = new AMPAEXTBD())
                {
                    using (DbContextTransaction transaccion = conn.Database.BeginTransaction())
                    {
                        try
                        {
                            //Se modifican los tutores
                            TUTOR nuevoSocio = MapearTutor(socio);
                            if(!SocioDat.ModificarSocio(nuevoSocio, conn))
                                 throw new Exception("El tutor no se ha actualizado en el sistema");

                            //Se eliminan los alumnos actuales del socio                            
                            ALUMNO alumnoActual;
                            List<ALUMNO_ACTIVIDAD> actividades = new List<ALUMNO_ACTIVIDAD>();
                           
                            //Se obtiene el listado actual de alumnos que tiene el socio
                            List<ALUMNO> listadoActual = SocioDat.GetAlumnosBySocio(socio.ID_TUTOR);
                            //Para cada uno de los existentes, se comprueba que esté o no en el listado nuevo
                            foreach (ALUMNO actAlum in listadoActual)
                            {
                                alumnoActual = alumnos.Find(c => c.NOMBRE.ToUpper() == actAlum.NOMBRE.ToUpper() && c.APELLIDO1.ToUpper() == actAlum.APELLIDO1.ToUpper() && c.APELLIDO2.ToUpper() == actAlum.APELLIDO2.ToUpper() && c.FECHA_NACIMIENTO == actAlum.FECHA_NACIMIENTO);
                                if (alumnoActual != null)
                                    alumnos.Remove(alumnoActual);
                                else //Si no se encuentra entre los nuevos, se eliminan
                                {
                                    //Primero se elimina los horarios y luego los alumnos
                                    //Se recuperan los alumnos vinculados al horario
                                    actividades = ActividadDat.GetAlumnnosByActividad(actAlum.ID_ALUMNO);
                                    //Se elimina el vínculo entre los usuarios encontrados y el horario
                                    foreach (ALUMNO_ACTIVIDAD alumAct in actividades)
                                        if (!ActividadDat.BajaAlumnoHorario(alumAct.ID_ALUM_ACT, conn))
                                            throw new Exception("Error al eliminar la relación entre el horario y el alumno existente: " + alumAct.ID_ALUM_ACT.ToString());

                                    //Se elimina el alumno
                                    if (!SocioDat.BajaAlumno(actAlum.ID_ALUMNO, conn))
                                        throw new Exception("Error al eliminar el alumno existente: " + actAlum.ID_ALUMNO.ToString());
                                }
                            }

                            //Con los alumnos de nueva inserción, se insertan
                            foreach (ALUMNO actAlumno in alumnos)
                            {
                                actAlumno.ID_TUTOR = socio.ID_TUTOR;
                                //Se inserta el nuevo alumno
                                alumnoActual = MapearAlumno(actAlumno);
                                if (SocioDat.AltaAlumno(alumnoActual, conn) < 1)
                                    throw new Exception("Error al insertar el nuevo alumno");
                            }
                            transaccion.Commit();
                            resultado = true;
                        }
                        catch (Exception ex)
                        {
                            transaccion.Rollback();
                            Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".ModificarSocio().", ex);
                            throw;
                        }
                    }
                }
            }
            else
                throw new Exception("No se han recuperado correctamente los datos de los tutores y/o alumnos");
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
            return SocioDat.GetSociosByAMPA(idAMPA, filtro);
        }

        /// <summary>
        /// Obtiene un determinado socio de una AMPA
        /// </summary>
        /// <param name="idUsuario">Identificador del socio a recuperar</param>
        /// <param name="idAMPA">Identificador de la AMPA</param>
        /// <returns>Datos del socio en <see cref="TUTOR"/></returns>
        public TUTOR GetSocioById(int idSocio, int idAMPA)
        {
            return SocioDat.GetSocioById(idSocio, idAMPA);
        }
        /// <summary>
        /// Obtiene el listado de alumnos de un socio
        /// </summary>
        /// <param name="idSocio">Identificador del socio</param>
        /// <returns>Listado de <see cref="ALUMNO"/> con los alumnos</returns>
        public List<ALUMNO> GetAlumnosBySocio(int idSocio)
        {
            return SocioDat.GetAlumnosBySocio(idSocio);
        }

        /// <summary>
        /// Obtiene el listado de alumnos que pertenecen a una AMPA
        /// </summary>
        /// <param name="idAMPA">Identificador de la AMPA</param>
        /// <param name="filtro">Campos de búsqueda</param>
        /// <returns>Listado de <see cref="ALUMNO"/> con los alumnos</returns>
        public List<ALUMNO> GetAlumnosByAMPA(int idAMPA, FiltroAlumno filtro)
        {
            return SocioDat.GetAlumnosByAMPA(idAMPA, filtro);
        }

        /// <summary>
        /// Obtiene el listado de alumnos que pertenecen a una empresa extraescolar
        /// </summary>
        /// <param name="idEmpresa">Identificador de la empresa</param>
        /// <param name="filtro">Campos de búsqueda</param>
        /// <returns>Listado de <see cref="ALUMNO"/> con los alumnos</returns>
        public List<ALUMNO> GetAlumnosByEmpresa(int idEmpresa, FiltroAlumno filtro)
        {
            return SocioDat.GetAlumnosByEmpresa(idEmpresa, filtro);
        }

        /// <summary>
        /// Da de baja un socio en el AMPA
        /// </summary>
        /// <param name="idTutor">Identificador del socio a dar de baja</param>
        /// <returns>Booleano con el resultado: true si ha ido todo bien; false en caso contrario</returns>
        public bool BajaSocio(int idTutor)
        {
            bool resultado = false;
            List<ALUMNO> alumnos = SocioDat.GetAlumnosBySocio(idTutor);
            using (AMPAEXTBD conn = new AMPAEXTBD())
            {
                using (DbContextTransaction transaccion = conn.Database.BeginTransaction())
                {
                    try
                    {
                        //Se borran las actividades extraescolares para los alumnos del socio
                        foreach (ALUMNO alumno in alumnos)
                        {
                            foreach(ALUMNO_ACTIVIDAD actividad in alumno.ALUMNO_ACTIVIDAD)
                                if (!ActividadDat.BajaAlumnoHorario(actividad.ID_ALUM_ACT, conn))
                                    throw new Exception("No se ha podido borrar la relación con el alumno: " + actividad.ID_ALUM_ACT.ToString());
                        }
                        //Se borran los alumnos del socio
                        if (!SocioDat.BajaAlumnosBySocio(idTutor, conn))
                            throw new Exception("No se han podido borrar los alumnos del socio " + idTutor.ToString());

                        //Se borra el socio
                        if(!SocioDat.BajaSocio(idTutor, conn))
                            throw new Exception("No se han podido borrar el socio " + idTutor.ToString());
                        resultado = true;
                        transaccion.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaccion.Rollback();
                        Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".BajaSocio(). idTutor: " + idTutor.ToString(), ex);
                        throw;
                    }
                }
            }
            return resultado;
        }

        /// <summary>
        /// Mapea un alumno en actividad
        /// </summary>
        /// <param name="objeto">Datos a Mapear</param>
        /// <returns>Objeto <see cref="ALUMNO"/> con los datos</returns>
        private ALUMNO MapearAlumno(ALUMNO objeto)
        {
            ALUMNO resultado = new ALUMNO();
            resultado.ID_ALUMNO = objeto.ID_ALUMNO;
            resultado.ID_CURSO_CLASE = objeto.ID_CURSO_CLASE;
            resultado.ID_TUTOR = objeto.ID_TUTOR;
            resultado.APELLIDO1 = objeto.APELLIDO1;
            resultado.APELLIDO2 = objeto.APELLIDO2;
            resultado.FECHA = objeto.FECHA;
            resultado.FECHA_MOD = objeto.FECHA_MOD;
            resultado.FECHA_NACIMIENTO = objeto.FECHA_NACIMIENTO;
            resultado.NOMBRE = objeto.NOMBRE;
            resultado.USUARIO = objeto.USUARIO;
            return resultado;
        }

        /// <summary>
        /// Mapea un socio
        /// </summary>
        /// <param name="objeto">Datos a Mapear</param>
        /// <returns>Objeto <see cref="TUTOR"/> con los datos</returns>
        private TUTOR MapearTutor(TUTOR objeto)
        {
            TUTOR resultado = new TUTOR();
            resultado.ID_TUTOR = objeto.ID_TUTOR;
            resultado.FECHA = objeto.FECHA;
            resultado.T1_NOMBRE = objeto.T1_NOMBRE;
            resultado.T1_APELLIDO1 = objeto.T1_APELLIDO1;
            resultado.T1_APELLIDO2 = objeto.T1_APELLIDO2;
            resultado.T1_EMAIL = objeto.T1_EMAIL;
            resultado.T1_ID_TIPO_DOCUMENTO = objeto.T1_ID_TIPO_DOCUMENTO;
            resultado.T1_NUMERO_DOCUMENTO = objeto.T1_NUMERO_DOCUMENTO;
            resultado.T1_TELEFONO = objeto.T1_TELEFONO;
            resultado.T2_NOMBRE = objeto.T2_NOMBRE;
            resultado.T2_APELLIDO1 = objeto.T2_APELLIDO1;
            resultado.T2_APELLIDO2 = objeto.T2_APELLIDO2;
            resultado.T2_EMAIL = objeto.T2_EMAIL;
            resultado.T2_ID_TIPO_DOCUMENTO = objeto.T2_ID_TIPO_DOCUMENTO;
            resultado.T2_NUMERO_DOCUMENTO = objeto.T2_NUMERO_DOCUMENTO;
            resultado.T2_TELEFONO = objeto.T2_TELEFONO;
            resultado.USUARIO = objeto.USUARIO;
            resultado.INDIVIDUAL = objeto.INDIVIDUAL;
            resultado.ID_AMPA = objeto.ID_AMPA;

            resultado.FECHA_MOD = objeto.FECHA_MOD;
            resultado.USUARIO = objeto.USUARIO;
            return resultado;
        }
    }
}
