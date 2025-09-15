using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace McvFirestorePlugin
{
    public class SettingsViewModel : ObservableObject
    {
        #region Commands
        #endregion
        public bool Topmost
        {
            get
            {
                return _topmost;
            }
            set
            {
                if (_topmost == value) return;
                _topmost = value;
                OnPropertyChanged(nameof(Topmost));
            }
        }
        public bool IsEnabled
        {
            get { return _model.IsEnabled; }
            set
            {
                if (_model.IsEnabled != value)
                {
                    _model.IsEnabled = value;
                    OnPropertyChanged(nameof(IsEnabled));
                }
            }
        }
        public string Title
        {
            get { return "Firebase連携用プラグイン"; }
        }
        private bool _topmost;
        public string FirebaseProjectId
        {
            get { return _model.FirebaseProjectId; }
            set
            {
                if (_model.FirebaseProjectId != value)
                {
                    _model.FirebaseProjectId = value;
                    OnPropertyChanged(nameof(FirebaseProjectId));
                }
            }
        }
        public string FirebaseConfigJsonPath
        {
            get { return _model.FirebaseConfigJsonPath; }
            set
            {
                if (_model.FirebaseConfigJsonPath != value)
                {
                    _model.FirebaseConfigJsonPath = value;
                    OnPropertyChanged(nameof(FirebaseConfigJsonPath));
                }
            }
        }
        private RelayCommand _showFilePickerCommand;
        public ICommand ShowFilePickerCommand
        {
            get
            {
                if (_showFilePickerCommand == null)
                {
                    _showFilePickerCommand = new RelayCommand(() =>
                    {
                        _model.ShowFilePicker();
                    });
                }
                return _showFilePickerCommand;
            }
        }
        public string FirestoreYouTubeLiveCommentCollectionPath
        {
            get { return _model.FirestoreYouTubeLiveCommentCollectionPath; }
            set
            {
                if (_model.FirestoreYouTubeLiveCommentCollectionPath != value)
                {
                    _model.FirestoreYouTubeLiveCommentCollectionPath = value;
                    OnPropertyChanged(nameof(FirestoreYouTubeLiveCommentCollectionPath));
                }
            }
        }
        public string FirestoreYouTubeUserCollectionPath
        {
            get { return _model.FirestoreYouTubeUserCollectionPath; }
            set
            {
                if (_model.FirestoreYouTubeUserCollectionPath != value)
                {
                    _model.FirestoreYouTubeUserCollectionPath = value;
                    OnPropertyChanged(nameof(FirestoreYouTubeUserCollectionPath));
                }
            }
        }
        public string FirestoreYouTubeLiveConnectedCollectionPath
        {
            get { return _model.FirestoreYouTubeLiveConnectedCollectionPath; }
            set
            {
                if (_model.FirestoreYouTubeLiveConnectedCollectionPath != value)
                {
                    _model.FirestoreYouTubeLiveConnectedCollectionPath = value;
                    OnPropertyChanged(nameof(FirestoreYouTubeLiveConnectedCollectionPath));
                }
            }
        }
        public string FirestoreYouTubeLiveDisconnectedCollectionPath
        {
            get { return _model.FirestoreYouTubeLiveDisconnectedCollectionPath; }
            set
            {
                if (_model.FirestoreYouTubeLiveDisconnectedCollectionPath != value)
                {
                    _model.FirestoreYouTubeLiveDisconnectedCollectionPath = value;
                    OnPropertyChanged(nameof(FirestoreYouTubeLiveDisconnectedCollectionPath));
                }
            }
        }
        public double DateWidth
        {
            get => _model.DateWidth;
            set
            {
                if (_model.DateWidth != value)
                {
                    _model.DateWidth = value;
                    OnPropertyChanged(nameof(DateWidth));
                }
            }
        }
        public double IdWidth
        {
            get => _model.IdWidth;
            set
            {
                if (_model.IdWidth != value)
                {
                    _model.IdWidth = value;
                    OnPropertyChanged(nameof(IdWidth));
                }
            }
        }
        public double NameWidth
        {
            get => _model.NameWidth;
            set
            {
                if (_model.NameWidth != value)
                {
                    _model.NameWidth = value;
                    OnPropertyChanged(nameof(NameWidth));
                }
            }
        }
        public double CalledWidth
        {
            get => _model.CalledWidth;
            set
            {
                if (_model.CalledWidth != value)
                {
                    _model.CalledWidth = value;
                    OnPropertyChanged(nameof(CalledWidth));
                }
            }
        }
        protected virtual DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }
        private readonly Model _model;
        private readonly Dispatcher _dispatcher;
        public SettingsViewModel()
        {
            if ((bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue))
            {
                var options = new DynamicOptions();
                _model = new Model(options, null);
                IsEnabled = true;
            }
        }
        internal SettingsViewModel(Model model, Dispatcher dispatcher)
        {
            _model = model;
            _dispatcher = dispatcher;
        }

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
