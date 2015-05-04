using System.Drawing;
using System.Xml.Serialization;

namespace QrCodeWinClient.Common
{
    public interface IQrCodeSettings
    {
        ErrorCorrectionLevel ErrorCorrectionLevel { get; set; }

        int ModuleSize { get; set; }

        SolidBrush LightBrush { get; set; }

        SolidBrush DarkBrush { get; set; }
    }

    public class PersistQrCodeSettings : IQrCodeSettings
    {
        public ErrorCorrectionLevel ErrorCorrectionLevel { get; set; }

        public int ModuleSize { get; set; }

        [XmlIgnore]
        public SolidBrush LightBrush
        {
            get { return new SolidBrush(ColorTranslator.FromHtml(this.LightBrushColor)); }
            set { this.LightBrushColor = ColorTranslator.ToHtml(value.Color); }
        }

        public string LightBrushColor { get; set; }

        [XmlIgnore]
        public SolidBrush DarkBrush
        {
            get { return new SolidBrush(ColorTranslator.FromHtml(this.DarkBrushColor)); }
            set { this.DarkBrushColor = ColorTranslator.ToHtml(value.Color); }
        }

        public string DarkBrushColor { get; set; }
    }
}