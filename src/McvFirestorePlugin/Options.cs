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

using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace McvFirestorePlugin
{
    public class DynamicOptions : IOptions, INotifyPropertyChanged
    {
        public string FirebaseProjectId { get; set; } = "Your project ID";
        public string FirebaseConfigJsonPath { get; set; } = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"MultiCommentViewer\McvFirestorePlugin\your-project-id-XXXXXXXXXXXX.json");
        public string FirestoreYouTubeLiveCommentCollectionPath { get; set; } = "youTubeLiveChatMessages";
        public string FirestoreYouTubeUserCollectionPath { get; set; } = "youTubeUsers";
        public string FirestoreYouTubeLiveConnectedCollectionPath { get; set; } = "youTubeLiveConnectionLogs";
        public string FirestoreYouTubeLiveDisconnectedCollectionPath { get; set; } = "youTubeLiveConnectionLogs";
        public bool IsEnabled { get; set; } = false;
        public double DateWidth { get; set; } = 106;
        public double IdWidth { get; set; } = 51;
        public double NameWidth { get; set; } = 95;
        public double CalledWidth { get; set; } = 74;

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public void Deserialize(string json)
        {
            if (string.IsNullOrEmpty(json)) return;
            var options = JsonConvert.DeserializeObject<DynamicOptions>(json);
            if (options == null) return;

            FirebaseProjectId = options.FirebaseProjectId;
            FirebaseConfigJsonPath = options.FirebaseConfigJsonPath;
            FirestoreYouTubeLiveCommentCollectionPath = options.FirestoreYouTubeLiveCommentCollectionPath;
            FirestoreYouTubeUserCollectionPath = options.FirestoreYouTubeUserCollectionPath;
            FirestoreYouTubeLiveConnectedCollectionPath = options.FirestoreYouTubeLiveConnectedCollectionPath;
            FirestoreYouTubeLiveDisconnectedCollectionPath = options.FirestoreYouTubeLiveDisconnectedCollectionPath;
            IsEnabled = options.IsEnabled;
            DateWidth = options.DateWidth;
            IdWidth = options.IdWidth;
            NameWidth = options.NameWidth;
            CalledWidth = options.CalledWidth;
        }

        public void Reset()
        {
            FirebaseProjectId = "Your project ID";
            FirebaseConfigJsonPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"MultiCommentViewer\McvFirestorePlugin\your-project-id-XXXXXXXXXXXX.json");
            FirestoreYouTubeLiveCommentCollectionPath = "youTubeLiveChatMessages";
            FirestoreYouTubeUserCollectionPath = "youTubeUsers";
            FirestoreYouTubeLiveConnectedCollectionPath = "youTubeLiveConnectionLogs";
            FirestoreYouTubeLiveDisconnectedCollectionPath = "youTubeLiveConnectionLogs";
            IsEnabled = false;
            DateWidth = 106;
            IdWidth = 51;
            NameWidth = 95;
            CalledWidth = 74;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}