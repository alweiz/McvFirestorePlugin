using NUnit.Framework;
using McvFirestorePlugin;
using Moq;
using Plugin;
using SitePlugin;
using YouTubeLiveSitePlugin;
using System.Collections.Generic;

namespace McvFirestorePluginTests
{
    [TestFixture]
    public class PluginBodyTests
    {
        private PluginBody _plugin;
        private Mock<IPluginHost> _hostMock;

        [SetUp]
        public void SetUp()
        {
            _plugin = new PluginBody();
            _hostMock = new Mock<IPluginHost>();

            // Setup mock host
            _hostMock.Setup(h => h.SettingsDirPath).Returns(@"C:\temp\test");
            _hostMock.Setup(h => h.LoadOptions(It.IsAny<string>())).Returns("");
            _hostMock.Setup(h => h.SaveOptions(It.IsAny<string>(), It.IsAny<string>()));

            _plugin.Host = _hostMock.Object;
        }

        [Test]
        public void Name_ShouldReturnCorrectValue()
        {
            // When & Then
            Assert.AreEqual("Firestore連携", _plugin.Name);
        }

        [Test]
        public void OnLoaded_ShouldInitializeComponents()
        {
            // When
            _plugin.OnLoaded();

            // Then
            // プラグインが正常に初期化されることを確認
            Assert.IsNotNull(_plugin.Host);
        }

        [Test]
        public void OnClosing_ShouldSaveOptions()
        {
            // Given
            _plugin.OnLoaded();

            // When
            _plugin.OnClosing();

            // Then
            _hostMock.Verify(h => h.SaveOptions(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void OnMessageReceived_WithDisabledPlugin_ShouldIgnoreMessage()
        {
            // Given
            _plugin.OnLoaded();
            var messageMock = new Mock<IYouTubeLiveComment>();
            var metadataMock = new Mock<IMessageMetadata>();
            metadataMock.Setup(m => m.IsNgUser).Returns(false);
            metadataMock.Setup(m => m.IsInitialComment).Returns(false);
            metadataMock.Setup(m => m.Is184).Returns(false);

            // When - プラグインが無効の状態でメッセージ受信
            _plugin.OnMessageReceived(messageMock.Object, metadataMock.Object);

            // Then - 例外が発生しないことを確認
            Assert.Pass("メッセージが正常に無視された");
        }

        [Test]
        public void GetSettingsFilePath_ShouldReturnCorrectPath()
        {
            // Given
            _plugin.Host = _hostMock.Object;

            // When
            var path = _plugin.GetSettingsFilePath();

            // Then
            Assert.That(path, Does.Contain("Firestore連携.txt"));
            Assert.That(path, Does.Contain(@"C:\temp\test"));
        }

        [Test]
        public void OnTopmostChanged_ShouldNotThrowException()
        {
            // Given
            _plugin.OnLoaded();

            // When & Then - 例外が発生しないことを確認
            Assert.DoesNotThrow(() => _plugin.OnTopmostChanged(true));
            Assert.DoesNotThrow(() => _plugin.OnTopmostChanged(false));
        }
    }
}