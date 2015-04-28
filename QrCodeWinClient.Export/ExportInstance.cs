using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using QrCodeWinClient.Common;

namespace QrCodeWinClient.Export
{
    public sealed class ExportInstance
    {
        private static volatile ExportInstance instance;
        private static object syncRoot = new Object();

        private ExportInstance()
        {
            Messenger.Default.Register<QrCodeRequestMessage>(this, this.StartQrCodeGeneration);
        }

        private void StartQrCodeGeneration(QrCodeRequestMessage qrCodeRequestMessage)
        {
            var qrCodeImage = QrCodeExporter.Export(qrCodeRequestMessage.Settings.ErrorCorrectionLevel, qrCodeRequestMessage.Value, qrCodeRequestMessage.Settings.ModuleSize, qrCodeRequestMessage.Settings.DarkBrush, qrCodeRequestMessage.Settings.LightBrush);
            Messenger.Default.Send<QrCodeResponseMessage>(new QrCodeResponseMessage() {QrCodeImage = qrCodeImage});
        }

        public static ExportInstance Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ExportInstance();
                    }
                }

                return instance;
            }
        }

        //public void StartQrCodeGeneration(string value, IQrCodeSettings settings, Action<QrCodeResponseMessage> reveiceQRCode)
        //{
        //    reveiceQRCode.Invoke(new QrCodeResponseMessage()
        //    {
        //        QrCodeImage = QrCodeExporter.Export(settings.ErrorCorrectionLevel, value, settings.ModuleSize, settings.DarkBrush, settings.LightBrush)
        //    });
        //}
        public void Init()
        {
            
        }
    }
}