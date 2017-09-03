namespace JamSoft.OverlayNinja.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Security;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Input;
    using Microsoft.Win32;
    using Utils;

    public class MainWindowViewModel : BaseVm
    {
        private RegistryKey _overlayRegistryKey;

        private string _keyName;

        // ReSharper disable once MemberCanBePrivate.Global
        public string KeyName
        {
            // ReSharper disable once UnusedMember.Global
            get { return _keyName; }
            set { _keyName = value; OnPropertyChanged(); }
        }

        private int _keyCount;

        public int KeyCount
        {
            get { return _keyCount; }
            set { _keyCount = value; OnPropertyChanged(); }
        }

        public string KeyTitle
        {
            get { return $"Reading Key (found {KeyCount} keys):"; }
        }

        private ObservableCollection<RegistryKeyEntryViewModel> _keys;

        // ReSharper disable once MemberCanBePrivate.Global
        public ObservableCollection<RegistryKeyEntryViewModel> Keys
        {
            get { return _keys; }
            set { _keys = value; OnPropertyChanged(); }
        }

        private ObservableCollection<AppGroupViewModel> _appGroups;

        // ReSharper disable once MemberCanBePrivate.Global
        public ObservableCollection<AppGroupViewModel> AppGroups
        {
            // ReSharper disable once UnusedMember.Global
            get { return _appGroups; }
            set { _appGroups = value; OnPropertyChanged(); }
        }

        private ObservableCollection<int> _priorities;

        public MainWindowViewModel Init()
        {
            _priorities = new ObservableCollection<int>(Enumerable.Range(0, 15).ToArray());
            Keys = new ObservableCollection<RegistryKeyEntryViewModel>();
            GetIconOverlayKeys();
            return this;
        }

        // ReSharper disable once UnusedMember.Global
        public ICommand RenameKeysCommand => new DelegateCommand(RenameKeys);

        // ReSharper disable once UnusedMember.Global
        public ICommand ReloadKeysCommand => new DelegateCommand(ReloadKeys);

        private void ReloadKeys()
        {
            Init();
        }

        private void RenameKeys()
        {
            foreach (var registryKeyEntryViewModel in Keys)
            {
                if (registryKeyEntryViewModel.WillRename && 
                    !string.IsNullOrEmpty(registryKeyEntryViewModel.Name) &&
                    !string.IsNullOrWhiteSpace(registryKeyEntryViewModel.Name) &&
                    !string.IsNullOrEmpty(registryKeyEntryViewModel.NewName) &&
                    !string.IsNullOrWhiteSpace(registryKeyEntryViewModel.NewName))
                {
                    try
                    {
                        bool success = RegUtils.RenameSubKey(_overlayRegistryKey, registryKeyEntryViewModel.Name, registryKeyEntryViewModel.NewName);
                        if (!success)
                        {
                            MessageBox.Show(Application.Current.MainWindow, "Rename failed", App.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        }
                    }
                    catch (Exception ex) when (ex is UnauthorizedAccessException || ex is SecurityException)
                    {
                        MessageBox.Show(Application.Current.MainWindow, "Unauthorised access, try running as administrator", App.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(Application.Current.MainWindow, ex.Message, App.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }

            Init();
        }

        private void GetIconOverlayKeys()
        {
            try
            {
                _overlayRegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\ShellIconOverlayIdentifiers", true);
            }
            catch (SecurityException ex)
            {
                MessageBox.Show(Application.Current.MainWindow, ex.Message, App.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_overlayRegistryKey != null)
            {
                KeyName = _overlayRegistryKey.Name;

                foreach (var subKeyName in _overlayRegistryKey.GetSubKeyNames())
                {
                    Keys.Add(new RegistryKeyEntryViewModel { Name = subKeyName, Priorities = _priorities });
                    KeyCount++;
                }
            }

            AppGroups = new ObservableCollection<AppGroupViewModel>(Keys.GroupBy(key => Regex.Replace(key.Name.Trim(), @"[\d-]", " ").Split(' ')[0]).Select(x => new AppGroupViewModel(x.Key, x, _priorities)));
        }
    }
}
