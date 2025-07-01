using ZXing;
using ZXing.Net.Maui;

namespace MusicStore.Views;

public partial class ZXingPage : ContentPage
{
	public ZXingPage()
	{
		InitializeComponent();
		barcodeScannerView.IsTorchOn = true;
		barcodeScannerView.IsDetecting = true;        
        barcodeScannerView.BarcodesDetected += CameraBarcodeReaderView_BarcodesDetectedAsync;
        barcodeScannerView.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormats.All,
            AutoRotate = true,
            Multiple = true,            
        };
    }


    private async void CameraBarcodeReaderView_BarcodesDetectedAsync(object? sender, BarcodeDetectionEventArgs? e)
    {
        await DisplayAlert("扫码结果", e?.Results?.FirstOrDefault()?.Value, "确定");
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PopToRootAsync();
    }

    private void ChangeCamera_Clicked(object sender, EventArgs e)
    {
        barcodeScannerView.CameraLocation = barcodeScannerView.CameraLocation == CameraLocation.Rear ? CameraLocation.Front : CameraLocation.Rear;
    }
}