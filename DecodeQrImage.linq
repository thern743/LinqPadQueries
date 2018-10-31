<Query Kind="Program">
  <NuGetReference>ZXing.Net</NuGetReference>
  <Namespace>ZXing</Namespace>
  <Namespace>System.Drawing</Namespace>
</Query>

void Main()
{
	var barcodeReader = new BarcodeReader();
	var barcodeBitmap = (Bitmap)Bitmap.FromFile("C:\\Users\\bw6y\\Documents\\LINQPad Queries\\sample.jpg");
	var barcodeResult = barcodeReader.Decode(barcodeBitmap);
	Console.WriteLine($"Decoded barcode text: {barcodeResult?.Text}");
	Console.WriteLine($"Barcode format: {barcodeResult?.BarcodeFormat}");
}

