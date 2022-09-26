using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Domain.Entities
{
    public class Inscripcion
    {
        public int Id { get; set; }
        public int CursoId { get; set; }
        public int EstudianteId { get; set; }
        public E_Grado Grado { get; set; }
        public Curso Curso { get; set; }
        public Estudiante Estudiante { get; set; }
    }
}
