using Bogus;
using Castle.Core.Configuration;
using CleanArchitecture.Application.Common.Dtos;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Runtime.Serialization;

namespace CleanArchTests
{
    [Trait("Pruebas", "EstudiantesService")]
    public class EstudiantesServiceTest
    {
        private readonly EstudiantesService _studiantesService;
        private readonly Mock<IEstudiantesRepository> _studiantesRepositoryMock = new Mock<IEstudiantesRepository>();

        public EstudiantesServiceTest()
        {
            _studiantesService = new EstudiantesService(_studiantesRepositoryMock.Object);
            Randomizer.Seed = new Random(123);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetByIdAsync_ShouldReturnStudent_WhenStudentExist(int id)
        {
            //Arrage
            var estudianteEsperado = new Estudiante() 
            {
                Id= 1,
                Nombre="Santiago",
                Apellido="Moreno",
                FechaInscripcion=DateTime.Now
            };

            _studiantesRepositoryMock.Setup(e => e.GetById(estudianteEsperado.Id)).ReturnsAsync(estudianteEsperado);

            //Act
            var estudianteActual = await _studiantesService.GetStudent(id);

            //Assert
            Assert.NotNull(estudianteActual);
            Assert.Equal(estudianteEsperado.Id, estudianteActual.Id);
        }

        [Fact]
        public async Task GetAllStudents_ShouldReturnsAllStudents()
        {
            //Arrange
            var estudiantes = GetEstudiantes();

            _studiantesRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(estudiantes);

            //Act
            var estudianteResultado = await _studiantesService.GetAllStudents();

            //Assert
            GetAllStudentsAsserts(estudiantes, estudianteResultado);
        }

        [Fact]
        public async Task GetAllStudents_WhenStudentDoestExist_ThrowKeyNotFoundException() 
        {
            //Arragen
            //var students = new List<Estudiante>();
            //_studiantesRepositoryMock.Setup(x => x.GetAll()).Throws<KeyNotFoundException>();


            //Act, Assert
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(async () => await _studiantesService.GetAllStudents()).Result;
            Assert.Equal("No existen estudiantes en la base de datos.", exception.Message);

        }

        [Fact]
        public async Task AddStudent_ShouldCreateANewStudent()
        {
            //Arrange
            var estudianteRequest = new EstudianteRequest
            {
                Nombre = "Santiago",
                Apellido = "Moreno"
            };

            var estudiante = new Estudiante
            {
                Id = 1,
                Nombre = "Santiago",
                Apellido = "Moreno",
                FechaInscripcion = DateTime.Now
            };

            _studiantesRepositoryMock.Setup(x => x.Add(estudiante)).ReturnsAsync(estudiante);

            //Act
            
            var estudianteResultado = await _studiantesService.CreateStudent(estudianteRequest);
            
            //Assert
            Assert.NotNull(estudianteResultado);
            _studiantesRepositoryMock.Verify(x => x.Add(estudianteResultado));

        }

        [Fact]
        public async Task DeleteStudent_WhenCalled_ShoulDeleteTheStudent()
        {
            //Arrange
            int id = 1;
            _studiantesRepositoryMock.Setup(x => x.Delete(1)).Verifiable();

            //Act
            await _studiantesService.DeleteStudent(id);

            //Assert
            Assert.Equal(1, id);
            _studiantesRepositoryMock.Verify(x=> x.Delete(id));
        }

        private static IEnumerable<Estudiante> GetEstudiantes()
        {
            var fakesEstudiantes = new Faker<Estudiante>()
                .RuleFor(e => e.Id, f => f.Random.Int(1, 100))
                .RuleFor(e => e.Nombre, f => f.Person.FirstName)
                .RuleFor(e => e.Apellido, f => f.Person.LastName)
                .RuleFor(e => e.FechaInscripcion, f => f.Date.Between(DateTime.Now.AddMonths(-3), DateTime.Now.AddMonths(3)));

            return fakesEstudiantes.Generate(5);
        }

        private static void GetAllStudentsAsserts(IEnumerable<Estudiante> estudiantes, IEnumerable<Estudiante> estudianteResultado)
        {
            Assert.NotNull(estudianteResultado);
            Assert.Equal(estudiantes.Count(), estudianteResultado.Count());
            Assert.All(estudianteResultado, r => Assert.NotEmpty(r.Nombre));
            Assert.Equal(estudiantes, estudianteResultado);
            Assert.True(estudiantes.Equals(estudianteResultado));
            Assert.IsAssignableFrom<IEnumerable<Estudiante>>(estudianteResultado);
        }

    }
}
