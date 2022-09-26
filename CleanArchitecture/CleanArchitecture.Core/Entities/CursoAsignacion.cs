namespace CleanArchitecture.Domain.Entities
{
    public class CursoAsignacion
    {
        public int InstructorId { get; set; }
        public int CursoId { get; set; }
        public Instructor Instructor { get; set; }
        public Curso Curso { get; set; }
    }
}
