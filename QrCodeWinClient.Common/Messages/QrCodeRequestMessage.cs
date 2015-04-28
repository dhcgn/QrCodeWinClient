namespace QrCodeWinClient.Common
{
    public class QrCodeRequestMessage
    {
        public QrCodeRequestMessage(string value, IQrCodeSettings settings)
        {
            this.Value = value;
            this.Settings = settings;
        }

        public IQrCodeSettings Settings { get; set; }

        public string Value { get; set; }
    }
}