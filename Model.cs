using Google.Cloud.Firestore;
using McvFirestorePlugin.Entities;
using Plugin;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using YouTubeLiveSitePlugin;
using PluginCommon;

namespace McvFirestorePlugin
{
    public class Model : INotifyPropertyChanged
    {
        private readonly IOptions _options;
        private readonly IPluginHost _host;

        public string FirebaseProjectId
        {
            get => _options.FirebaseProjectId;
            set
            {
                if (_options.FirebaseProjectId != value)
                {
                    _options.FirebaseProjectId = value;
                    RaisePropertyChanged(nameof(FirebaseProjectId));
                }
            }
        }
        public string FirebaseConfigJsonPath
        {
            get => _options.FirebaseConfigJsonPath;
            set
            {
                if (_options.FirebaseConfigJsonPath != value)
                {
                    _options.FirebaseConfigJsonPath = value;
                    RaisePropertyChanged(nameof(FirebaseConfigJsonPath));
                }
            }
        }
        public string FirestoreYouTubeLiveCommentCollectionPath
        {
            get => _options.FirestoreYouTubeLiveCommentCollectionPath;
            set
            {
                if (_options.FirestoreYouTubeLiveCommentCollectionPath != value)
                {
                    _options.FirestoreYouTubeLiveCommentCollectionPath = value;
                    RaisePropertyChanged(nameof(FirestoreYouTubeLiveCommentCollectionPath));
                }
            }
        }
        public string FirestoreYouTubeUserCollectionPath
        {
            get => _options.FirestoreYouTubeUserCollectionPath;
            set
            {
                if (_options.FirestoreYouTubeUserCollectionPath != value)
                {
                    _options.FirestoreYouTubeUserCollectionPath = value;
                    RaisePropertyChanged(nameof(FirestoreYouTubeUserCollectionPath));
                }
            }
        }
        public string FirestoreYouTubeLiveConnectedCollectionPath
        {
            get => _options.FirestoreYouTubeLiveConnectedCollectionPath;
            set
            {
                if (_options.FirestoreYouTubeLiveConnectedCollectionPath != value)
                {
                    _options.FirestoreYouTubeLiveConnectedCollectionPath = value;
                    RaisePropertyChanged(nameof(FirestoreYouTubeLiveConnectedCollectionPath));
                }
            }
        }
        public string FirestoreYouTubeLiveDisconnectedCollectionPath
        {
            get => _options.FirestoreYouTubeLiveDisconnectedCollectionPath;
            set
            {
                if (_options.FirestoreYouTubeLiveDisconnectedCollectionPath != value)
                {
                    _options.FirestoreYouTubeLiveDisconnectedCollectionPath = value;
                    RaisePropertyChanged(nameof(FirestoreYouTubeLiveDisconnectedCollectionPath));
                }
            }
        }
        public double DateWidth
        {
            get => _options.DateWidth;
            set => _options.DateWidth = value;
        }
        public double IdWidth
        {
            get => _options.IdWidth;
            set => _options.IdWidth = value;
        }
        public double NameWidth
        {
            get => _options.NameWidth;
            set => _options.NameWidth = value;
        }
        public double CalledWidth
        {
            get => _options.CalledWidth;
            set => _options.CalledWidth = value;
        }
        public Model(IOptions options, IPluginHost host)
        {
            _options = options;
            _host = host;
        }
        protected virtual DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }
        public void ShowFilePicker()
        {
            var fileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Title = "Firebase 設定 JSON ファイルを選択してください",
                Filter = "JSON ファイル|*.json"
            };
            var result = fileDialog.ShowDialog();
            if (result == true)
            {
                this.FirebaseConfigJsonPath = fileDialog.FileName;
            }
        }
        public async void AddYouTubeLiveMessage(IYouTubeLiveComment youTubeLiveComment)
        {
            if (string.IsNullOrEmpty(_options.FirebaseProjectId)) { throw new ApplicationException("Firebase プロジェクト ID が未指定です。"); }
            FirestoreDb db = FirestoreDb.Create(_options.FirebaseProjectId);
            if (string.IsNullOrEmpty(_options.FirestoreYouTubeLiveCommentCollectionPath)) { throw new ApplicationException("Firestore YouTube Live Comment Collection パスが未指定です。"); }
            CollectionReference collectionRef = db.Collection(_options.FirestoreYouTubeLiveCommentCollectionPath);
            await collectionRef.AddAsync(
                new YouTubeLiveChatMessage() {
                    UserIconUrl = youTubeLiveComment.UserIcon.Url,
                    UserDisplayName = youTubeLiveComment.NameItems.ToText(),
                    MessageType = youTubeLiveComment.YouTubeLiveMessageType.ToString(),
                    MessageId = youTubeLiveComment.Id,
                    Text = youTubeLiveComment.CommentItems.ToText(),
                    PostedAt = Google.Cloud.Firestore.Timestamp.FromDateTime(youTubeLiveComment.PostedAt.ToUniversalTime()),
                });
        }
        public async void AddYouTubeLiveMessage(IYouTubeLiveConnected message)
        {
            if (string.IsNullOrEmpty(_options.FirebaseProjectId)) { throw new ApplicationException("Firebase プロジェクト ID が未指定です。"); }
            FirestoreDb db = FirestoreDb.Create(_options.FirebaseProjectId);
            if (string.IsNullOrEmpty(_options.FirestoreYouTubeLiveConnectedCollectionPath)) { throw new ApplicationException("Firestore YouTube Live Connected Collection パスが未指定です。"); }
            CollectionReference collectionRef = db.Collection(_options.FirestoreYouTubeLiveConnectedCollectionPath);
            await collectionRef.AddAsync(
                new
                {
                    text = message.Text,
                    messageType = message.YouTubeLiveMessageType.ToString(),
                });
        }
        public async void AddYouTubeLiveMessage(IYouTubeLiveDisconnected message)
        {
            if (string.IsNullOrEmpty(_options.FirebaseProjectId)) { throw new ApplicationException("Firebase プロジェクト ID が未指定です。"); }
            FirestoreDb db = FirestoreDb.Create(_options.FirebaseProjectId);
            if (string.IsNullOrEmpty(_options.FirestoreYouTubeLiveDisconnectedCollectionPath)) { throw new ApplicationException("Firestore YouTube Live Disconnected Collection パスが未指定です。"); }
            CollectionReference collectionRef = db.Collection(_options.FirestoreYouTubeLiveDisconnectedCollectionPath);
            await collectionRef.AddAsync(
                new 
                {
                    text = message.Text,
                    messageType = message.YouTubeLiveMessageType.ToString(),
                });
        }
        public async Task<DocumentReference> AddYouTubeUser(IYouTubeLiveComment youTubeLiveComment)
        {
            if (string.IsNullOrEmpty(_options.FirebaseProjectId)) { throw new ApplicationException("Firebase プロジェクト ID が未指定です。"); }
            FirestoreDb db = FirestoreDb.Create(_options.FirebaseProjectId);
            if (string.IsNullOrEmpty(_options.FirestoreYouTubeUserCollectionPath)) { throw new ApplicationException("Firestore YouTube User Collection パスが未指定です。"); }
            CollectionReference collectionRef = db.Collection(_options.FirestoreYouTubeUserCollectionPath);
            var snapshot = await collectionRef.Document(youTubeLiveComment.UserId).GetSnapshotAsync();
            DocumentReference docRef;
            if (snapshot.Exists)
            {
                docRef = snapshot.Reference;
            }
            else
            {
                docRef = collectionRef.Document(youTubeLiveComment.UserId);
            }
            await docRef.SetAsync(
              new YouTubeUser()
              {
                  Id = docRef.Id,
                  IconUrl = youTubeLiveComment.UserIcon.Url,
                  DisplayName = youTubeLiveComment.NameItems.ToText(),
                  PostedAt = Google.Cloud.Firestore.Timestamp.FromDateTime(youTubeLiveComment.PostedAt.ToUniversalTime()),
              }, SetOptions.MergeAll);

            return docRef;
        }
        public bool IsEnabled
        {
            get => _options.IsEnabled;
            set
            {
                if (_options.IsEnabled != value)
                {
                    _options.IsEnabled = value;
                    RaisePropertyChanged(nameof(IsEnabled));
                }
            }
        }
        //private void WriteComment(string comment)
        //{
        //    _host.PostCommentToAll(comment);
        //}

        #region INotifyPropertyChanged
        [NonSerialized]
        private System.ComponentModel.PropertyChangedEventHandler _propertyChanged;

        /// <summary>
        /// 
        /// </summary>
        public virtual event System.ComponentModel.PropertyChangedEventHandler PropertyChanged
        {
            add { _propertyChanged += value; }
            remove { _propertyChanged -= value; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            _propertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
