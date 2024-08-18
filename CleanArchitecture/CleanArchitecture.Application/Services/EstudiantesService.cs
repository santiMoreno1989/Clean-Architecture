using CleanArchitecture.Application.Common.Dtos;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Entities;
using Microsoft.FeatureManagement;

namespace CleanArchitecture.Application.Services
{
    public class EstudiantesService : IEstudiantesService
	{
        private readonly IEstudiantesRepository _repository;
        private readonly IFeatureManager _featureManager;
		public EstudiantesService(IEstudiantesRepository repository, IFeatureManager featureManager)
		{
			_repository = repository;
			_featureManager = featureManager;
		}

		/// <summary>
		/// Obtiene todos los estudiantes registrados en la base de datos
		/// </summary>
		/// <returns></returns>
		/// <exception cref="KeyNotFoundException"></exception>
		public async Task<IEnumerable<Estudiante>> GetAllStudents()
        {
            if (await _featureManager.IsEnabledAsync(FeatureFlag.StudentFeature))
            {
                var estudiantes = await _repository.GetAll();

                if (!estudiantes.Any())
                    throw new KeyNotFoundException("No existen estudiantes en la base de datos.");

                return estudiantes;
            }
            else 
            {
                throw new BadRequestException("Característica no habilitada");
            }

        }

        /// <summary>
        /// Registra un nuevo estudiante
        /// </summary>
        /// <param name="estudiante"></param>
        /// <returns></returns>
        /// <exception cref="BadRequestException"></exception>
        public async Task<Estudiante> CreateStudent(EstudianteRequest estudiante)
        {
            var estudianteExiste = await _repository.GetAll();
            var nombre = estudianteExiste.Select(e => e.Nombre.ToLower());
            var apellido = estudianteExiste.Select(e => e.Apellido.ToLower());

            var student = new Estudiante()
            {
                Apellido = estudiante.Apellido,
                FechaInscripcion = DateTime.Now,
                Nombre = estudiante.Nombre,
                Activo = estudiante.Activo
            };

            if (nombre.Contains(student.Nombre.ToLower()) && apellido.Contains(student.Apellido.ToLower()))
                throw new BadRequestException($"El estudiante {student.Nombre} {student.Apellido} ya existe en la base de datos.");

            await _repository.Add(student);
            return student;
        }

        /// <summary>
        /// Obtiene un estudiante en base a su ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="BadRequestException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<Estudiante> GetStudent(int id)
        {
            if (id == 0)
                throw new BadRequestException();

            var student = await _repository.GetById(id);

            if (student == null)
                throw new KeyNotFoundException($"El estudiante con ID : {id} no existe.");

            return student;
        }

        /// <summary>
        /// Borra un estudiante de la base de datos a traves de su ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteStudent(int id) 
        {
             await _repository.Delete(id);
        }

		public async Task CreateStudentJob()
		{
			var student = new Estudiante()
			{
				Apellido = "Borrar",
				FechaInscripcion = DateTime.Now,
				Nombre = "Para",
                Activo = true
			};

			await _repository.Add(student);
		}

		public async Task DeleteStudentJob()
		{
            var estudianteRepetido = (await _repository.GetWhere(e => !e.Activo));

            if (estudianteRepetido.Any())
               await _repository.DeleteRange(estudianteRepetido);


		}
	}
}
