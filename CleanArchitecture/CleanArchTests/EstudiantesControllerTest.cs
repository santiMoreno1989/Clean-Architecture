using CleanArchitecture.API.Controllers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CleanArchTests
{
    [Trait("Pruebas", "EstudiantesControllerTest")]
    public class EstudiantesControllerTest
    {
        private readonly EstudiantesController _estudiantesController;
        private readonly Mock<IEstudiantesService> _estudiantesService = new Mock<IEstudiantesService>();
		private readonly Mock<IBackgroundJobClient> _backgroundJobClient = new();
		private readonly Mock<IRecurringJobManager> _recurringJobManager = new();

		public EstudiantesControllerTest()
        {
            _estudiantesController = new EstudiantesController(_estudiantesService.Object,_backgroundJobClient.Object,_recurringJobManager.Object);
        }

        [Fact]
        public async Task GetAllStudents_ShouldReturnAllStudents() 
        {
            //Arrange
            var students = GetStudents();
            _estudiantesService.Setup(x => x.GetAllStudents()).ReturnsAsync(students);
            
            //Act
            var studentResult = await _estudiantesController.GetAllStudents();

            //Assert
            _estudiantesService.Verify(x => x.GetAllStudents(), Times.Exactly(1)); // verifica que se llame una vez al servicio dentro del metodo
            Assert.NotNull(studentResult);
            Assert.IsType<OkObjectResult>(studentResult);
        }

        [Fact]
        public async Task DeleteStudent_ShouldDeleteAExpecificStudent() 
        {
            //Arrange
            int id = 1;
            _estudiantesService.Setup(x => x.DeleteStudent(1)).Verifiable();

            //Act
            var result = await _estudiantesController.DeleteStudent(id);

            //Assert
            Assert.IsType<OkObjectResult>(result);
            _estudiantesService.Verify();
        }

        private static IEnumerable<Estudiante> GetStudents()
        {
            return new List<Estudiante>
            {
                new Estudiante()
                {
                    Id = 1,
                    Nombre="Pascal",
                    Apellido="Sky",
                    FechaInscripcion=new DateTime(2023,1,23)
                },
                new Estudiante()
                {
                    Id = 2,
                    Nombre="Walter",
                    Apellido="Encina",
                    FechaInscripcion=new DateTime(2023,1,12)
                },
                new Estudiante()
                {
                    Id = 3,
                    Nombre="Cesar",
                    Apellido="Fiorda",
                    FechaInscripcion=new DateTime(2023,2,5)
                },
            };
        }
    }
}
