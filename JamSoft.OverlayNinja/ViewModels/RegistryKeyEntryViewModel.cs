namespace JamSoft.OverlayNinja.ViewModels
{
    using System.Collections.ObjectModel;
    using Utils;

    public class RegistryKeyEntryViewModel : BaseVm
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        private int _numberOfLeadingSpaces;

        public int NumberOfLeadingSpaces
        {
            get { return _numberOfLeadingSpaces; }
            set
            {
                _numberOfLeadingSpaces = value;
                SelectedPriority = value;
                OnPropertyChanged();
            }
        }

        private string _newName;

        public string NewName
        {
            get { return _newName; }
            private set
            {
                _newName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(NewNameUi));
            }
        }

        public string NewNameUi => _newName?.FormatName(SelectedPriority, true);

        private int _selectedPriority;

        public int SelectedPriority
        {
            get { return _selectedPriority; }
            set
            {
                if (value != _selectedPriority)
                {
                    _selectedPriority = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(WillRename));
                }

                SetNewName();
            }
        }

        private void SetNewName()
        {
            NewName = WillRename ? Name.FormatName(SelectedPriority, false) : null;
        }

        public bool WillRename => NumberOfLeadingSpaces != SelectedPriority && Name != NewName;

        public ObservableCollection<int> Priorities { get; set; }
    }
}