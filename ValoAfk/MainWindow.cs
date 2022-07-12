using System.Windows;
using System.Windows.Media;
using ValoAfk.controllers;

namespace ValoAfk
{
    public partial class MainWindow
    {
        private readonly Worker _worker = Worker.CreateInstance();


        private void InitOnTopRight(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = SystemParameters.WorkArea;
            Left = desktopWorkingArea.Right - Width;
            Top = desktopWorkingArea.Bottom - Height;
        }


        private void BindActions()
        {
            if (_worker.IsRunning)
                _worker.Stop();
            else
            {
                if (!Windows.FocusOnValorant())
                {
                    MessageBox.Show("Are you sure Valorant is running ?");
                    //return; // if we need to block execution in case valorant ist running 
                }

                _worker.Start(GetCurrentConfig());
            }

            SetRunning(_worker.IsRunning);
        }

        private void SetRunning(bool state)
        {
            RunnerButton.Background = state ? Brushes.LightGreen : Brushes.DeepSkyBlue;
            RunnerButton.Content = state ? "Running" : "Run";
            ProgressBar.Visibility = state ? Visibility.Visible : Visibility.Hidden;
            Pattern.IsReadOnly = state;
            Timeout.IsReadOnly = state;
        }

        private void OnRun(object sender, RoutedEventArgs eventArgs) =>
            Application.Current.Dispatcher.Invoke(BindActions);

        private void OnClose(object sender, RoutedEventArgs eventArgs)
        {
            var config = GetCurrentConfig();
            if (RememberMe.IsChecked.GetValueOrDefault(false))
                ConfigStore.Store(config);

            Close();
        }

        private void SetCurrentConfig()
        {
            var config = ConfigStore.Load();
            if (config == null || !config.RememberMe) return;

            Pattern.Text = config.Pattern;
            Timeout.Text = $"{config.Timeout}";
            RememberMe.IsChecked = config.RememberMe;
        }

        private Config GetCurrentConfig()
        {
            if (Pattern.Text.Length == 0 || Timeout.Text.Length == 0)
            {
                MessageBox.Show("Error: can't run with empty fields");
                return null;
            }

            if (!int.TryParse(Timeout.Text, out var timeout))
            {
                MessageBox.Show("Error: Timeout can only be Integer");
                return null;
            }

            return new Config(Pattern.Text, timeout, RememberMe.IsChecked.GetValueOrDefault(false));
        }
    }
}