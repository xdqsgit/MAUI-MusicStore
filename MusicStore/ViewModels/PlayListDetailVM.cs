using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MusicStore.ViewModels
{
    public partial class PlayListDetailVM:ObservableObject
    {
        [ObservableProperty]
        string name;
    }
}
