using Syncfusion.Maui.DataSource;
namespace MusicStore;
///<summary>
///ListViewFeatures class
///</summary>
public partial class ListViewFeatures : ContentPage
{
	///<summary>
    ///ListViewFeatures constructor
    ///</summary>
	public ListViewFeatures()
	{
		InitializeComponent();
		 listView.DataSource.GroupDescriptors.Add(new GroupDescriptor()
		{
			PropertyName = "ContactName",
			KeySelector = (object obj1) =>
			{
				var item = (obj1 as ListViewContactsInfo);
				return item.ContactName[0].ToString();
			},
		});
	}
}
