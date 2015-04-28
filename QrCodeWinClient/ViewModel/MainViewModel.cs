using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace QrCodeWinClient.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        

        #region

        public RelayCommand CopyQRCodeToClipboardCommand { get; set; }
        public RelayCommand SaveQRCodeToLibraryCommand { get; set; }
        public RelayCommand SaveQRCodeDialogCommand { get; set; }

        #endregion

        #region Properties

        private string inputText;
        public string InputText
        {
            get { return this.inputText; }
            set { this.Set(ref this.inputText, value); }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }


    }
}