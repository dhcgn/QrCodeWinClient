﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight.Messaging;
using QrCodeWinClient.Common;

namespace QrCodeWinClient.Export
{
    public sealed class ExportInstance
    {
        private static volatile ExportInstance instance;
        private static object syncRoot = new Object();

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

        private ExportInstance()
        {
            Messenger.Default.Register<QrCodeRequestMessage>(this, this.StartQrCodeGeneration);
        }

        private void StartQrCodeGeneration(QrCodeRequestMessage message)
        {
            BitmapImage qrCodeImage = null;

            if (!String.IsNullOrEmpty(message.Value))
            {
                qrCodeImage = QrCodeExporter.Export(
                    message.Settings.ErrorCorrectionLevel,
                    message.Value,
                    message.Settings.ModuleSize,
                    message.Settings.DarkBrush,
                    message.Settings.LightBrush);
            }

            Messenger.Default.Send<QrCodeResponseMessage>(new QrCodeResponseMessage() {QrCodeImage = qrCodeImage});
        }

        public void Init()
        {
        }
    }
}