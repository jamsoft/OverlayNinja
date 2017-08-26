namespace JamSoft.OverlayNinja.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class AppGroupViewModel : BaseVm
    {
        public string AppName { get; set; }

        // ReSharper disable once MemberCanBePrivate.Global
        public ObservableCollection<RegistryKeyEntryViewModel> Keys { get; }

        public ObservableCollection<int> Priorities { get; set; }

        public AppGroupViewModel(string appName, IEnumerable<RegistryKeyEntryViewModel> keys, ObservableCollection<int> priorities)
        {
            Priorities = priorities;
            AppName = appName;
            Keys = new ObservableCollection<RegistryKeyEntryViewModel>(keys);

            var groupPriorities = Keys.GroupBy(key => key.NumberOfLeadingSpaces);
            if (groupPriorities.Count() == 1)
            {
                _selectedPriority = Keys[0].SelectedPriority;
            }
        }

        private int _selectedPriority;

        public int SelectedPriority
        {
            get { return _selectedPriority; }
            set
            {
                _selectedPriority = value;
                SetPriority(value);
            }
        }

        private void SetPriority(int p)
        {
            foreach (var registryKeyEntryViewModel in Keys)
            {
                registryKeyEntryViewModel.SelectedPriority = p;
            }
        }
    }
}