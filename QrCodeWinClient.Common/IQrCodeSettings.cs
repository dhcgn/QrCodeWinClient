using System.Drawing;

namespace QrCodeWinClient.Common
{
    public interface IQrCodeSettings
    {
        ErrorCorrectionLevel ErrorCorrectionLevel { get; set; }

        int ModuleSize { get; set; }

        SolidBrush LightBrush { get; set; }

        SolidBrush DarkBrush { get; set; }
    }
}