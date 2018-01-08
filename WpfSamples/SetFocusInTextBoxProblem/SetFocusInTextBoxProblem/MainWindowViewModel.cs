using System;
using System.Timers;

namespace SetFocusInTextBoxProblem
{
    /// <summary>
    /// This is the data context for the MainWindow.
    /// </summary>
    class MainWindowViewModel : ViewModelBase
    {
        #region BackingFields
        private bool _readyForDataEntry;
        private Timer _timer = new Timer(3000);
        #endregion

        #region Properties
        public RelayCommand DataEntryFocusLostCmd { get; set; }
        public RelayCommand SetFocusCmd { get; set; }

        public bool ReadyForDataEntry
        {
            get { return _readyForDataEntry; }
            set { SetProperty(ref _readyForDataEntry, value); }
        }

        #endregion

        /// <summary>
        /// Constructor.  Finishes construction and initialization of objects needed by the 
        /// view model.  
        /// </summary>
        public MainWindowViewModel()
        {
            DataEntryFocusLostCmd = new RelayCommand(DataEntryFocusLost);
            SetFocusCmd = new RelayCommand(SetFocus);
        }

        private void DataEntryFocusLost()
        {
            ReadyForDataEntry = false;
        }

        private void SetFocus()
        {
            _timer.Elapsed += OnElapsed;
            _timer.Enabled = true;
        }

        private void OnElapsed(Object source, System.Timers.ElapsedEventArgs e)
        {
            ReadyForDataEntry = true;
            _timer.Enabled = false;
        }
    }
}
