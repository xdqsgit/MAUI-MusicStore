using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MusicStore.VM
{
    public partial class ButtonVM: ObservableObject
    {
        [ObservableProperty]
        int count;

        [RelayCommand]
        void Increment()
        {
            Count++;
        }
    }
}
