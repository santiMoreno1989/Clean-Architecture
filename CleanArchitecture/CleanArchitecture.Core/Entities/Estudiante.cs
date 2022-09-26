﻿namespace CleanArchitecture.Domain.Entities
{
    public class Estudiante
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public ICollection<Inscripcion> Inscripciones { get; set; }
    }
}
