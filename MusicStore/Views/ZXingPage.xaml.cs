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
            Multiple = true
        };
    }


    private async void CameraBarcodeReaderView_BarcodesDetectedAsync(object? sender, BarcodeDetectionEventArgs? e)
    {
        await DisplayAlert("ɨ����", e?.Results?.FirstOrDefault()?.Value, "ȷ��");
    }
}