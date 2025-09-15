using NUnit.Framework;
using McvFirestorePlugin;

namespace McvFirestorePluginTests
{
    [TestFixture]
    public class SimpleOptionsTests
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

        [Test]
        public void DynamicOptions_InvalidValues_ShouldHandleGracefully()
        {
            // Given
            var options = new DynamicOptions();

            // When & Then - 空文字列や無効値の処理
            Assert.DoesNotThrow(() => options.FirebaseProjectId = "");
            Assert.DoesNotThrow(() => options.FirebaseConfigJsonPath = "");
            Assert.DoesNotThrow(() => options.FirestoreYouTubeLiveCommentCollectionPath = "");
        }

        [Test]
        public void DynamicOptions_WidthProperties_ShouldAcceptValidValues()
        {
            // Given
            var options = new DynamicOptions();

            // When
            options.DateWidth = 150.0;
            options.IdWidth = 75.0;
            options.NameWidth = 120.0;
            options.CalledWidth = 90.0;

            // Then
            Assert.AreEqual(150.0, options.DateWidth);
            Assert.AreEqual(75.0, options.IdWidth);
            Assert.AreEqual(120.0, options.NameWidth);
            Assert.AreEqual(90.0, options.CalledWidth);
        }
    }
}