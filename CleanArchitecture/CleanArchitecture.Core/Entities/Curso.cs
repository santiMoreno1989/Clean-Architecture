namespace CleanArchitecture.Domain.Entities
{
    public class Curso
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int Creditos { get; set; }
        public int DepartamentoId { get; set; }
        public Departamento Departamento { get; set; }
        public ICollection<Inscripcion> Inscripciones { get; set; }
        public ICollection<CursoAsignacion> CursoAsignaciones { get; set; }

    }
}
