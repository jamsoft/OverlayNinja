namespace JamSoft.OverlayNinja.ViewModels
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class BaseVm : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}