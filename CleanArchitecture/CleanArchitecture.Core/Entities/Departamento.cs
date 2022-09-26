namespace CleanArchitecture.Domain.Entities
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Presupuesto { get; set; }
        public DateTime FechaInicio { get; set; }
        public int? InstructorId { get; set; }
        public Instructor Instructor { get; set; }
        public ICollection<Curso> Curso { get; set; }
    }
}
