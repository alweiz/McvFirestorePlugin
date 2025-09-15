using NUnit.Framework;
using McvFirestorePlugin;
using Moq;
using Plugin;

namespace McvFirestorePluginTests
{
    [TestFixture]
    public class OptionsTests
    {
        [Test]
        public void DynamicOptions_DefaultValues_ShouldBeSet()
        {
            // Given & When
            var options = new DynamicOptions();

            // Then
            Assert.AreEqual("Your project ID", options.FirebaseProjectId);
            Assert.AreEqual("youTubeLiveChatMessages", options.FirestoreYouTubeLiveCommentCollectionPath);
            Assert.AreEqual("youTubeUsers", options.FirestoreYouTubeUserCollectionPath);
            Assert.AreEqual("youTubeLiveConnectionLogs", options.FirestoreYouTubeLiveConnectedCollectionPath);
            Assert.AreEqual("youTubeLiveConnectionLogs", options.FirestoreYouTubeLiveDisconnectedCollectionPath);
            Assert.IsFalse(options.IsEnabled);
        }

        [Test]
        public void DynamicOptions_SetValues_ShouldPersist()
        {
            // Given
            var options = new DynamicOptions();

            // When
            options.FirebaseProjectId = "test-project";
            options.IsEnabled = true;
            options.FirestoreYouTubeLiveCommentCollectionPath = "test-comments";

            // Then
            Assert.AreEqual("test-project", options.FirebaseProjectId);
            Assert.IsTrue(options.IsEnabled);
            Assert.AreEqual("test-comments", options.FirestoreYouTubeLiveCommentCollectionPath);
        }

        [Test]
        public void DynamicOptions_Serialization_ShouldWork()
        {
            // Given
            var options = new DynamicOptions();
            options.FirebaseProjectId = "test-project";
            options.IsEnabled = true;

            // When
            var serialized = options.Serialize();
            var newOptions = new DynamicOptions();
            newOptions.Deserialize(serialized);

            // Then
            Assert.AreEqual("test-project", newOptions.FirebaseProjectId);
            Assert.IsTrue(newOptions.IsEnabled);
        }
    }
}