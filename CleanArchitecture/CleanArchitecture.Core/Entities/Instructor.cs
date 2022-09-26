namespace CleanArchitecture.Domain.Entities
{
    public class Instructor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaContratacion { get; set; }
        public ICollection<CursoAsignacion> CursoAsignaciones { get; set; }
        public OficinaAsignacion OficinaAsignacion { get; set; }
    }
}
