using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MusicStore.ViewModel
{
    public partial class FlyoutHeaderVM:ObservableObject
    {
        [RelayCommand]
        void ChangeTheme()
        {
            App.Current.UserAppTheme = App.Current.UserAppTheme == AppTheme.Light ? AppTheme.Dark : AppTheme.Light;
            
        }
    }
}
