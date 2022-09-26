using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Domain.Entities
{
    public class OficinaAsignacion
    {
        public int InstructorID { get; set; }
        public string Ubicacion { get; set; }
        public Instructor Instructor { get; set; }
    }
}
