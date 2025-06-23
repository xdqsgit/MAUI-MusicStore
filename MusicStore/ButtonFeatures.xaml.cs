using MusicStore.VM;

namespace MusicStore;
///<summary>
///ButtonFeatures class
///</summary>
public partial class ButtonFeatures : ContentPage
{
    ///<summary>
    ///ButtonFeatures constructor
    ///</summary>
    public ButtonFeatures()
    {
        InitializeComponent();
        this.BindingContext = new ButtonVM();
    }
}
