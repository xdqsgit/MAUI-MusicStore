using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStore.Model;

namespace MusicStore.DataTemplates
{
    public class LyriclistDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ValidTemplate { get; set; }
        public DataTemplate InvalidTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((Lyric)item).Time == 0f ? ValidTemplate : InvalidTemplate;
        }
    }
}
