using FluentAssertions;
using Gymate.App.Abstract;
using Gymate.App.Concrete;
using Gymate.App.Managers;
using Gymate.Domain.Entity;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Gymate.Tests
{
    public class ExerciseManagerTests
    {
        [Fact]
        public void Should_ServiceAddItemBeCalled_When_AddItemCalled()
        {
            //Arrange
            var exercise = new Exercise(1, "dummyExercise", 1);
            var exerciseServiceMock = new Mock<IService<Exercise>>();
            var informationProviderMock = new Mock<InformationProvider>();

            exerciseServiceMock.Setup(s => s.GetItem(1)).Returns(exercise);
            informationProviderMock.Setup(e => e.GetNumericInputKey()).Returns(1);
            informationProviderMock.Setup(e => e.GetInputString()).Returns("dummyExercise");

            var objectUnderTest = new ExerciseManager(new MenuActionService(), exerciseServiceMock.Object, informationProviderMock.Object);

            //Act

            var result = objectUnderTest.AddNewExercise();

            //Assert
            result.Should().Be(exercise.Id);
            exerciseServiceMock.Verify(m => m.GetLastId(), Times.Once);
            exerciseServiceMock.Verify(m => m.AddItem(It.IsAny<Exercise>()), Times.Once);
        }

        [Fact]
        public void Should_ServiceRemoveItemBeCalledOnce_When_RemoveExerciseCalled()
        {
            //Arrange

            Exercise item = new Exercise(1, "dummyExercise", 1);

            var exerciseServiceMock = new Mock<IService<Exercise>>();

            exerciseServiceMock.SetupGet(m => m.Items).Returns(new List<Exercise>() { item });

            var informationProviderMock = new Mock<InformationProvider>();

            informationProviderMock.Setup(m => m.GetNumericInputKey()).Returns(item.Id);

            var objectUnderTest = new ExerciseManager(new MenuActionService(), exerciseServiceMock.Object, informationProviderMock.Object);

            //Act
            objectUnderTest.RemoveExercise();

            //Assert
            exerciseServiceMock.Verify(m => m.RemoveItem(It.IsAny<Exercise>()), Times.Once);
        }

        [Fact]
        public void Should_RemoveItemNotBeCalled_When_NoExerciseInService()
        {
            //Arrange
            var exerciseList = new List<Exercise>();
            var exerciseServiceMock = new Mock<IService<Exercise>>();

            exerciseServiceMock.SetupGet(m => m.Items).Returns(exerciseList);

            var informationProviderMock = new Mock<InformationProvider>();

            var objectUnderTest = new ExerciseManager(new MenuActionService(), exerciseServiceMock.Object, informationProviderMock.Object);

            //Act
            objectUnderTest.RemoveExercise();

            //Assert
            exerciseServiceMock.Verify(m => m.RemoveItem(It.IsAny<Exercise>()), Times.Never);
        }

        [Fact]
        public void Should_ServiceGetItemBeCalled_When_ViewExerciseDetailsCalled()
        {
            //Arrange
            var exercise = new Exercise(1, "dummyExercise", 1);
            var exerciseServiceMock = new Mock<IService<Exercise>>();

            exerciseServiceMock.SetupGet(m => m.Items).Returns(new List<Exercise>() { exercise });

            var informationProviderMock = new Mock<InformationProvider>();

            informationProviderMock.Setup(m => m.GetNumericValue()).Returns(1);

            var objectUnderTest = new ExerciseManager(new MenuActionService(), exerciseServiceMock.Object, informationProviderMock.Object);

            //Act
            objectUnderTest.ViewExerciseDetails();

            //Assert
            exerciseServiceMock.Verify(m => m.GetItem(exercise.Id), Times.Once);
        }

        [Fact]
        public void Should_ShowMultipleInformationBeCalled_When_ItemNotFound()
        {
            var exerciseServiceMock = new Mock<IService<Exercise>>();

            var exercise = new Exercise(1, "dummyExercise", 1);

            exerciseServiceMock.SetupGet(m => m.Items).Returns(new List<Exercise>() { exercise});

            exerciseServiceMock.Setup(m => m.GetItem(It.IsAny<int>())).Returns(exercise);

            var informationProviderMock = new Mock<InformationProvider>();

            var objectUnderTest = new ExerciseManager(new MenuActionService(), exerciseServiceMock.Object, informationProviderMock.Object);

            var expectedInformationList = new List<string>() { "Exercise id: 1", "Exercise name: dummyExercise", "Exercise type id: 1" };

            //Act
            objectUnderTest.ViewExerciseDetails();

            //Assert
            informationProviderMock.Verify(m => m.ShowMultipleInformation(expectedInformationList), Times.Once);
        }

        [Fact]
        public void Should_ShowSingleMessageBeCalles_When_ItemNotFound()
        {
            var exerciseServiceMock = new Mock<IService<Exercise>>();
            exerciseServiceMock.SetupGet(m => m.Items).Returns(new List<Exercise>());

            var informationProviderMock = new Mock<InformationProvider>();

            var objectUnderTest = new ExerciseManager(new MenuActionService(), exerciseServiceMock.Object, informationProviderMock.Object);

            //Act
            objectUnderTest.ViewExerciseDetails();

            //Assert
            informationProviderMock.Verify(m => m.ShowSingleMessage("No exercise to show."), Times.Once);
        }

        [Fact]
        public void Shoud_ShowExercise_When_ShowAllExerciseCalledAndItemsNotEmpty()
        {
            //Arrange
            var exercise = new Exercise(1, "dummyExercise", 1);

            var items = new List<Exercise>() { exercise };

            var exerciseServiceMock = new Mock<IService<Exercise>>();

            exerciseServiceMock.SetupGet(m => m.Items).Returns(items);

            var informationProviderMock = new Mock<InformationProvider>();

            var objectUnderTest = new ExerciseManager(new MenuActionService(), exerciseServiceMock.Object, informationProviderMock.Object);

            var expectedMessage = new List<string>() { $"{exercise.Id} - {exercise.Name}" };

            //Act
            objectUnderTest.ShowAllExercises();

            //Assert
            informationProviderMock.Verify(m => m.ShowMultipleInformation(expectedMessage), Times.Once);
        }

        [Fact]
        public void Shoud_ShowSingleMessage_When_ShowAllExerciseCalledAndItemsEmpty()
        {
            var exerciseServiceMock = new Mock<IService<Exercise>>();
            exerciseServiceMock.SetupGet(m => m.Items).Returns(new List<Exercise>());

            var informationProviderMock = new Mock<InformationProvider>();

            var objectUnderTest = new ExerciseManager(new MenuActionService(), exerciseServiceMock.Object, informationProviderMock.Object);

            //Act
            objectUnderTest.ShowAllExercises();

            //Assert
            informationProviderMock.Verify(m => m.ShowSingleMessage("No exercises added."), Times.Once);
        }

        [Fact]
        public void Should_ShowExercise_When_ViewExerciseByTypeIdCalledAndExerciseExists()
        {
            //Arrange
            var exercise = new Exercise(1, "dummyExercise", 1);

            var informationProviderMock = new Mock<InformationProvider>();

            informationProviderMock.Setup(m => m.GetNumericInputKey()).Returns(exercise.TypeId);

            var exerciseServiceMock = new Mock<IService<Exercise>>();

            exerciseServiceMock.Setup(m => m.GetAllItems()).Returns(new List<Exercise>() { exercise });

            var objectUnderTest = new ExerciseManager(new MenuActionService(), exerciseServiceMock.Object, informationProviderMock.Object);

            //Act
            objectUnderTest.ViewExercisesByTypeId();

            //Assert
            informationProviderMock.Verify(m => m.ShowMultipleInformation(new List<string>() { $"\n{exercise.Id} - {exercise.Name}" }));
        }

        [Fact]
        public void Shoud_ShowSingleMessage_When_ViewExerciseByTypeIdCalledAndNoExercise()
        {
            //Arrange
            var informationProviderMock = new Mock<InformationProvider>();
            var exerciseServiceMock = new Mock<IService<Exercise>>();

            exerciseServiceMock.Setup(m => m.GetAllItems()).Returns(new List<Exercise>());

            var objectUnderTest = new ExerciseManager(new MenuActionService(), exerciseServiceMock.Object, informationProviderMock.Object);

            //Act
            objectUnderTest.ViewExercisesByTypeId();

            //Assert
            informationProviderMock.Verify(m => m.ShowSingleMessage("No exercise to show."), Times.Once);
        }

        [Fact]
        public void Should_SetExerciseVolume_When_ExerciseFound()
        {
            var exercise = new Exercise(1, "dummyExercise", 1);

            var informationProviderMock = new Mock<InformationProvider>();
            var exerciseServiceMock = new Mock<IService<Exercise>>();

            exerciseServiceMock.SetupGet(m => m.Items).Returns(new List<Exercise>() { exercise });

            exerciseServiceMock.Setup(m => m.GetItem(exercise.Id)).Returns(exercise);

            informationProviderMock.Setup(m => m.GetNumericInputKey()).Returns(exercise.Id);
            informationProviderMock.Setup(m => m.GetNumericValue()).Returns(11);

            var objectUnderTest = new ExerciseManager(new MenuActionService(), exerciseServiceMock.Object, informationProviderMock.Object);

            objectUnderTest.UpdateVolumeInExercise();

            Assert.Equal(11, exercise.Sets);
            Assert.Equal(11, exercise.Reps);
            Assert.Equal(11, exercise.Load);
        }

        [Fact]
        public void Should_ShowSingleMessage_When_SetExerciseVolumeAndExerciseNotFound()
        {
            var exercise = new Exercise(99, "dummyExercise", 40);
            var informationProviderMock = new Mock<InformationProvider>();
            var exerciseServiceMock = new Mock<IService<Exercise>>();

            exerciseServiceMock.SetupGet(m => m.Items).Returns(new List<Exercise>());

            exerciseServiceMock.Setup(m => m.GetItem(exercise.Id)).Returns((Exercise)null);

            informationProviderMock.Setup(m => m.GetNumericInputKey()).Returns(exercise.Id);
            informationProviderMock.Setup(m => m.GetNumericValue()).Returns(11);

            var objectUnderTest = new ExerciseManager(new MenuActionService(), exerciseServiceMock.Object, informationProviderMock.Object);

            objectUnderTest.UpdateVolumeInExercise();

            informationProviderMock.Verify(m => m.ShowSingleMessage("Exercise not found"), Times.Once);
        }
    }
}
