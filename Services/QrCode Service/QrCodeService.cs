using QRCoder;

namespace Cafe_Management_System.Services.QrCode_Service;

public class QrCodeService
{
    public string GenerateQrCode(string data)
    {
        var qrGenerator = new QRCodeGenerator();
        var qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
        using (var qrCode = new PngByteQRCode(qrCodeData))
        {
            var qrCodeImage = qrCode.GetGraphic(20);
            using (MemoryStream ms = new MemoryStream())
            {
                var base64QrCode = Convert.ToBase64String(ms.ToArray());
                Console.WriteLine("Base64 QR Code: " + base64QrCode);
                return base64QrCode;
            }
        }
    }
}