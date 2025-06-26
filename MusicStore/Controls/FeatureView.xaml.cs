namespace MusicStore.Controls;

public partial class FeatureView : ContentView
{
	public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(FeatureView), null);
    public string Title
    {
        get => (string)GetValue(FeatureView.TitleProperty);
        set => SetValue(FeatureView.TitleProperty, value);
    }

    
	public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource), typeof(string), typeof(FeatureView), null);
    public string ImageSource
    {
        get => (string)GetValue(FeatureView.ImageSourceProperty);
        set => SetValue(FeatureView.ImageSourceProperty, value);
    }
    
	public static readonly BindableProperty TextOnImageProperty = BindableProperty.Create(nameof(TextOnImage), typeof(string), typeof(FeatureView), null);
    public string TextOnImage
    {
        get => (string)GetValue(FeatureView.TextOnImageProperty);
        set => SetValue(FeatureView.TextOnImageProperty, value);
    }


    public FeatureView()
	{
		InitializeComponent();
	}



}