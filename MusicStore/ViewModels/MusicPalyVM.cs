using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using KWApi;

namespace MusicStore.ViewModels
{
    public partial class MusicPalyVM : AudioBaseVM
    {
        public MusicPalyVM(KWApIHelper kWApIHelper) : base(kWApIHelper)
        {
        }

        [RelayCommand]
        void BangSelected()
        {

        }
    }
}
