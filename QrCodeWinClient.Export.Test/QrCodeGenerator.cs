using System;
using System.Drawing;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QrCodeWinClient.Common;

namespace QrCodeWinClient.Export.Test
{
    [TestClass]
    public class QrCodeGeneratorInputTest
    {
        [TestMethod]
        public void SimpleInput()
        {
            string inputText = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor " +
                               "invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam" +
                               " et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est " +
                               "Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed " +
                               "diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. " +
                               "At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea " +
                               "takimata sanctus est Lorem ipsum dolor sit amet.";

            TestInput(inputText);
        }


        [TestMethod]
        public void UmlauteInput()
        {
            string inputText = "Lörem ipßüm dölör ßit ämet, cönßetetür ßädipßcing elitr, ßed diäm nönümy eirmöd tempör " +
                               "invidünt üt läböre et dölöre mägnä äliqüyäm erät, ßed diäm völüptüä. ät verö eöß et äccüßäm" +
                               " et jüßtö düö dölöreß et eä rebüm. ßtet clitä käßd gübergren, nö ßeä täkimätä ßänctüß eßt " +
                               "Lörem ipßüm dölör ßit ämet. Lörem ipßüm dölör ßit ämet, cönßetetür ßädipßcing elitr, ßed " +
                               "diäm nönümy eirmöd tempör invidünt üt läböre et dölöre mägnä äliqüyäm erät, ßed diäm völüptüä. " +
                               "ät verö eöß et äccüßäm et jüßtö düö dölöreß et eä rebüm. ßtet clitä käßd gübergren, nö ßeä " +
                               "täkimätä ßänctüß eßt Lörem ipßüm dölör ßit ämet.";

            TestInput(inputText);
        }

        [TestMethod]
        public void SpezialInput()
        {
            string inputText = @"öäüÖÄÜß^°!""§$% &/ () =?`{[]}\+#-.,*'_:;~<>|µ@€";

            TestInput(inputText);
        }



        private static void TestInput(string inputText)
        {
            var sb = new StringBuilder();

            foreach (var moduleSize in new[] {1, 2, 3})
            {
                foreach (ErrorCorrectionLevel errorCorrectionLevel in Enum.GetValues(typeof (ErrorCorrectionLevel)).Cast<ErrorCorrectionLevel>())
                {
                    string output = null;

                    try
                    {
                        var bitmapImage = QrCodeExporter.Export(errorCorrectionLevel, inputText, moduleSize, new SolidBrush(Color.Black), new SolidBrush(Color.White));
                        output = QrCodeUtils.GetStringFromQrCode(bitmapImage);
                    }
                    catch (Exception e)
                    {
                        // Assert.Fail("Assert failed, with ModuleSize: {0}, ErrorCorrectionLevel: {1}, Exception: {2}", moduleSize, errorCorrectionLevel, e.Message);
                        sb.AppendLine(String.Format("Assert failed with Exception, with ModuleSize: {0}, ErrorCorrectionLevel: {1}, Exception: {2}", moduleSize, errorCorrectionLevel, e.Message));
                    }

                    //Assert.AreEqual(InputText, output, "Assert failed, with ModuleSize: {0}, ErrorCorrectionLevel: {1}", moduleSize, errorCorrectionLevel);
                    if (inputText != output)
                        sb.AppendLine(String.Format("Assert failed, with ModuleSize: {0}, ErrorCorrectionLevel: {1}", moduleSize, errorCorrectionLevel));
                }
            }

            Assert.AreEqual(String.Empty, sb.ToString());
        }
    }
}
