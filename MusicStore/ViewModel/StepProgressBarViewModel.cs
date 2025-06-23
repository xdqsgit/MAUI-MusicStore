using Syncfusion.Maui.ProgressBar;
using System.Collections.ObjectModel;

namespace MusicStore
{
    public class StepProgressBarViewModel
    {
        public ObservableCollection<StepProgressBarItem> StepDetails { get; set; }
        public StepProgressBarViewModel()
        {
            StepDetails = new ObservableCollection<StepProgressBarItem>()
            {
                new StepProgressBarItem { PrimaryText = "Survey information" },
                new StepProgressBarItem { PrimaryText = "Feedback" },
                new StepProgressBarItem { PrimaryText = "Status"},
            };
        }
    }
}