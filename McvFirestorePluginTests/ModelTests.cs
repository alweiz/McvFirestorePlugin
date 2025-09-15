using NUnit.Framework;
using McvFirestorePlugin;
using Moq;
using Plugin;
using System;

namespace McvFirestorePluginTests
{
    [TestFixture]
    public class ModelTests
    {
        private Model _model;
        private Mock<IPluginHost> _hostMock;
        private DynamicOptions _options;

        [SetUp]
        public void SetUp()
        {
            _options = new DynamicOptions();
            _hostMock = new Mock<IPluginHost>();
            _model = new Model(_options, _hostMock.Object);
        }

        [Test]
        public void Constructor_ShouldInitializeProperties()
        {
            // Then
            Assert.IsNotNull(_model);
            Assert.AreEqual(_options.FirebaseProjectId, _model.FirebaseProjectId);
            Assert.AreEqual(_options.FirebaseConfigJsonPath, _model.FirebaseConfigJsonPath);
        }

        [Test]
        public void FirebaseProjectId_SetValue_ShouldUpdateOptions()
        {
            // Given
            var testValue = "test-project-123";

            // When
            _model.FirebaseProjectId = testValue;

            // Then
            Assert.AreEqual(testValue, _model.FirebaseProjectId);
            Assert.AreEqual(testValue, _options.FirebaseProjectId);
        }

        [Test]
        public void FirebaseConfigJsonPath_SetValue_ShouldUpdateOptions()
        {
            // Given
            var testPath = @"C:\test\config.json";

            // When
            _model.FirebaseConfigJsonPath = testPath;

            // Then
            Assert.AreEqual(testPath, _model.FirebaseConfigJsonPath);
            Assert.AreEqual(testPath, _options.FirebaseConfigJsonPath);
        }

        [Test]
        public void CollectionPaths_SetValues_ShouldUpdateOptions()
        {
            // Given
            var commentPath = "test-comments";
            var userPath = "test-users";
            var connectedPath = "test-connected";
            var disconnectedPath = "test-disconnected";

            // When
            _model.FirestoreYouTubeLiveCommentCollectionPath = commentPath;
            _model.FirestoreYouTubeUserCollectionPath = userPath;
            _model.FirestoreYouTubeLiveConnectedCollectionPath = connectedPath;
            _model.FirestoreYouTubeLiveDisconnectedCollectionPath = disconnectedPath;

            // Then
            Assert.AreEqual(commentPath, _model.FirestoreYouTubeLiveCommentCollectionPath);
            Assert.AreEqual(userPath, _model.FirestoreYouTubeUserCollectionPath);
            Assert.AreEqual(connectedPath, _model.FirestoreYouTubeLiveConnectedCollectionPath);
            Assert.AreEqual(disconnectedPath, _model.FirestoreYouTubeLiveDisconnectedCollectionPath);
        }

        [Test]
        public void WidthProperties_SetValues_ShouldUpdateOptions()
        {
            // Given
            var dateWidth = 150.0;
            var idWidth = 75.0;
            var nameWidth = 120.0;
            var calledWidth = 90.0;

            // When
            _model.DateWidth = dateWidth;
            _model.IdWidth = idWidth;
            _model.NameWidth = nameWidth;
            _model.CalledWidth = calledWidth;

            // Then
            Assert.AreEqual(dateWidth, _model.DateWidth);
            Assert.AreEqual(idWidth, _model.IdWidth);
            Assert.AreEqual(nameWidth, _model.NameWidth);
            Assert.AreEqual(calledWidth, _model.CalledWidth);
        }

        [Test]
        public void PropertyChanged_WhenValueChanges_ShouldRaiseEvent()
        {
            // Given
            var eventRaised = false;
            _model.PropertyChanged += (sender, e) => {
                if (e.PropertyName == nameof(_model.FirebaseProjectId))
                    eventRaised = true;
            };

            // When
            _model.FirebaseProjectId = "test-project";

            // Then
            Assert.IsTrue(eventRaised);
        }
    }
}