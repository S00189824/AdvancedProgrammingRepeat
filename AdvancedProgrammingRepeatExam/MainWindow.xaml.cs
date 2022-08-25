using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdvancedProgrammingRepeatExam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker backgroundWorker = new BackgroundWorker();

        public string[] wordArray = new string[3] { "sdfsd", "sdfsd", "edfe" };
        public int[] intArray = new int[3];
        const int secondsToWait = 3000; // Thread Timer

        public MainWindow()
        {
            InitializeComponent();

            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;

            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
        }

        private void BackgroundWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            // Displays progress bar progress of background worker
            progressBar.Value = e.ProgressPercentage;
        }

        private void BackgroundWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BackgroundWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < length; i++)
            {
                if (backgroundWorker.CancellationPending)// to check if cancellation is pending and then cancels
                {
                    e.Cancel = true;
                    return;
                }

                MessageBox.Show("");


            }
        }

        private void btn_StartClick(object sender, RoutedEventArgs e)
        {
            //Starts execution of backgroundworker
            backgroundWorker.RunWorkerAsync();
        }
    }
}
