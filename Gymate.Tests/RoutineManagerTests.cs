using Gymate.App.Abstract;
using Gymate.App.Managers;
using Gymate.Domain.Entity;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Gymate.Tests
{
    public class RoutineManagerTests
    {
        [Fact]
        public void Should_GetRoutineIdReturnCorrectId_When_Called()
        {
            //Arrange
            var routine = new Routine(1, "dummyRoutine");

            var informationProviderMock = new Mock<InformationProvider>();

            var routineServiceMock = new Mock<IService<Routine>>();

            routineServiceMock.SetupGet(m => m.Items).Returns(new List<Routine>() { routine });

            informationProviderMock.Setup(m => m.GetNumericInputKey()).Returns(1);

            var objectUnderTest = new RoutineManager(routineServiceMock.Object, informationProviderMock.Object);

            //Act
            var result = objectUnderTest.GetRoutineId();

            //Assert
            Assert.Equal(routine.Id, result);
        }

        [Fact]
        public void Should_AddExerciseToRoutineDay_When_ExerciseExists()
        {
            //Arrange
            var routine = new Routine(1, "dummyRoutine");

            var exercise = new Exercise(1, "dummyExercise", 1);

            var informationProviderMock = new Mock<InformationProvider>();
            var routineServiceMock = new Mock<IService<Routine>>();
            routineServiceMock.SetupGet(m => m.Items).Returns(new List<Routine>() { routine });
            routineServiceMock.Setup(m => m.GetItem(routine.Id)).Returns(routine);

            var objectUnderTest = new RoutineManager(routineServiceMock.Object, informationProviderMock.Object);

            //Act
            objectUnderTest.AddSelectedExerciseToRoutineDay(routine.Id, exercise);

            //Assert
            Assert.Equal(exercise.Id, routine.ExercisesOfTheDay[0].Id);
            Assert.Equal(exercise.Name, routine.ExercisesOfTheDay[0].Name);
        }

        [Fact]
        public void Should_ShowExercisesInRoutine_When_ExerciseAddedToRoutineDay()
        {
            //Arrange
            var routine = new Routine(1, "dummyRoutine");

            var exercise = new Exercise(1, "dummyExercise", 1);

            routine.ExercisesOfTheDay.Add(exercise);

            var routineServiceMock = new Mock<IService<Routine>>();

            routineServiceMock.Setup(m => m.GetAllItems()).Returns(new List<Routine>() { routine });

            var informationProviderMock = new Mock<InformationProvider>();

            var objectUnderTest = new RoutineManager(routineServiceMock.Object, informationProviderMock.Object);

            //Act
            objectUnderTest.ShowWholeRoutine();

            //Assert
            informationProviderMock.Verify(m => m.ShowMultipleInformation(new List<string>() { "1. dummyExercise - 0 x 0 x 0" }), Times.Once);
        }

        [Fact]
        public void Should_ShowSingleMessage_When_ExerciseNotAddedToRoutineDay()
        {
            //Arrange
            var routine = new Routine(1, "dummyRoutine");

            routine.ExercisesOfTheDay = new List<Exercise>();

            var routineServiceMock = new Mock<IService<Routine>>();

            routineServiceMock.Setup(m => m.GetAllItems()).Returns(new List<Routine>() { routine });

            var informationProviderMock = new Mock<InformationProvider>();

            var objectUnderTest = new RoutineManager(routineServiceMock.Object, informationProviderMock.Object);

            //Act
            objectUnderTest.ShowWholeRoutine();

            //Assert
            informationProviderMock.Verify(m => m.ShowSingleMessage("No exercises added."), Times.Once);
        }
    }
}
