using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Sync_Async_Demo
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        public MainWindow()
        {
            InitializeComponent();
        }
        //Separar el código en Model y Command, esto es solo un EJEMPLO

        private void executeSync_Click(object sender, RoutedEventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var results = Methods.RunDownloadSync();
            PrintResults(results);

            watch.Stop();

            setElapsedMilisecond(watch.ElapsedMilliseconds);

        }

        private void executeParallelSync_Click(object sender, RoutedEventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var results = Methods.RunDownloadParallelSync();
            PrintResults(results);

            watch.Stop();

            setElapsedMilisecond(watch.ElapsedMilliseconds);
        }

        private async void executeAsync_Click(object sender, RoutedEventArgs e)
        {
            cancellationTokenSource = new CancellationTokenSource();
            Progress<ProgressReportModel> progress = new Progress<ProgressReportModel>();
            progress.ProgressChanged += ReportProgress;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                var results = await Methods.RunDownloadAsync(progress, cancellationTokenSource.Token);
                //PrintResults(results);
            }
            catch (OperationCanceledException)
            {
                resultsWindow.Text += $"The async download was cancelled. {Environment.NewLine}";
                cancellationTokenSource = new CancellationTokenSource();
            }

            watch.Stop();

            setElapsedMilisecond(watch.ElapsedMilliseconds);

        }

        private async void executeParallelAsync_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mess = MessageBox.Show("¿Run Parallel Async V2?", "Dooffys", MessageBoxButton.YesNo, MessageBoxImage.Question);

            var watch = System.Diagnostics.Stopwatch.StartNew();
            if (mess == MessageBoxResult.Yes)
            {
                Progress<ProgressReportModel> progress = new Progress<ProgressReportModel>();
                progress.ProgressChanged += ReportProgress;
                var results = await Methods.RunDownloadParallelAsyncV2(progress);//, cancellationTokenSource.Token);
            }
            else
            {
                var results = await Methods.RunDownloadParallelAsync();
                PrintResults(results);
            }

            watch.Stop();

            setElapsedMilisecond(watch.ElapsedMilliseconds);
        }

        private void cancelOperation_Click(object sender, RoutedEventArgs e)
        {
            cancellationTokenSource.Cancel();
        }

        private void setElapsedMilisecond(long elapsedMs)
        {
            resultsWindow.Text += $"Total execution time: { elapsedMs } {Environment.NewLine}";
        }

        private void PrintResults(List<WebsiteDataModel> results)
        {
            resultsWindow.Text = "";
            results.ForEach(data =>
            {
                resultsWindow.Text += $"{ data.WebSiteUrl } downloaded: {data.WebSiteData.Length} characters long.{ Environment.NewLine }";
            });
        }

        private void ReportProgress(object sender, ProgressReportModel e)
        {
            dashboardProgress.Value = e.PercentageComplete;
            PrintResults(e.SitesDownloaded);
        }

        private void changeButtonsVisibility(object sender, RoutedEventArgs e)
        {
            if ((bool)rbParallelExecution.IsChecked)
            {
                executeSync.Visibility = Visibility.Collapsed;
                executeParallelSync.Visibility = Visibility.Visible;
                executeAsync.Visibility = Visibility.Collapsed;
                executeParallelAsync.Visibility = Visibility.Visible;
            }
            else
            {
                executeSync.Visibility = Visibility.Visible;
                executeParallelSync.Visibility = Visibility.Collapsed;
                executeAsync.Visibility = Visibility.Visible;
                executeParallelAsync.Visibility = Visibility.Collapsed;
            }
        }
    }
}
