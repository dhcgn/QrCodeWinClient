using System.Drawing;

namespace QrCodeWinClient.Common
{
    public interface IQrCodeSettings
    {
        ErrorCorrectionLevel ErrorCorrectionLevel { get; set; }

        int ModuleSize { get; set; }

        Brush LightBrush { get; set; }

        Brush DarkBrush { get; set; }
    }
}