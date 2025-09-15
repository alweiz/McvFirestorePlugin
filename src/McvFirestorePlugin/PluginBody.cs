/*
 * Copyright 2024 alweiz
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Plugin;
using SitePlugin;
using System;
using System.IO;
using System.Windows.Threading;
using System.ComponentModel.Composition;
using YouTubeLiveSitePlugin;

namespace McvFirestorePlugin
{
    [Export(typeof(IPlugin))]
    public class PluginBody : IPlugin
    {
        private IOptions _options;

        public string Name
        {
            get
            {
                return "Firebaseプラグイン";
            }
        }
        public string Description
        {
            get
            {
                return "";
            }
        }
        public IPluginHost Host { get; set; }

        public void OnMessageReceived(ISiteMessage message, IMessageMetadata messageMetadata)
        {
            if (!_options.IsEnabled || messageMetadata.IsNgUser || messageMetadata.IsInitialComment || messageMetadata.Is184)
                return;

            IYouTubeLiveComment comment = message as IYouTubeLiveComment;
            if (null != comment)
            {
                _model.AddYouTubeUser(comment);
                _model.AddYouTubeLiveMessage(comment);
            }
            IYouTubeLiveConnected connected = message as IYouTubeLiveConnected;
            if (null != connected)
            {
                _model.AddYouTubeLiveMessage(connected);
            }
            IYouTubeLiveDisconnected disconnected = message as IYouTubeLiveDisconnected;
            if (null != disconnected)
            {
                _model.AddYouTubeLiveMessage(disconnected);
            }
        }
        SettingsViewModel _vm;
        private Dispatcher _dispatcher;
        protected virtual IOptions LoadOptions()
        {
            var options = new DynamicOptions();
            try
            {
                var s = Host.LoadOptions(GetSettingsFilePath());
                options.Deserialize(s);
            }
            catch (System.IO.FileNotFoundException) { }
            return options;
        }
        public void OnLoaded()
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
            _options = LoadOptions();
            _model = CreateModel();
            _vm = CreateSettingsViewModel();
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", _model.FirebaseConfigJsonPath);
        }

        protected virtual SettingsViewModel CreateSettingsViewModel()
        {
            return new SettingsViewModel(_model, _dispatcher);
        }
        protected virtual Model CreateModel()
        {
            return new Model(_options, Host);
        }

        public void OnClosing()
        {
            var s = _options.Serialize();
            Host.SaveOptions(GetSettingsFilePath(), s);
        }
        public void Run()
        {
        }

        public void ShowSettingView()
        {
            var left = Host.MainViewLeft;
            var top = Host.MainViewTop;
            var view = new SettingsView
            {
                Left = left,
                Top = top,
                DataContext = _vm
            };
            view.Show();
        }

        public string GetSettingsFilePath()
        {
            var dir = Host.SettingsDirPath;
            return Path.Combine(dir, $"{Name}.txt");
        }

        public void OnTopmostChanged(bool isTopmost)
        {
            if (_vm != null)
            {
                _vm.Topmost = isTopmost;
            }
        }
        Model _model;
        public PluginBody()
        {
        }
    }
}
