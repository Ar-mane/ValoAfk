using System.Windows;

namespace ValoAfk
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(InitOnTopRight);
            SetCurrentConfig();
        }
    }
}