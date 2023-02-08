using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Dtos
{
    public class EstudianteResponse
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaInscripcion { get; set; }

        public static explicit operator EstudianteResponse(Estudiante v)
        {
            return new EstudianteResponse()
            {
                Nombre=v.Nombre,
                Apellido=v.Apellido,
                FechaInscripcion=v.FechaInscripcion
            };
        }
    }
}
