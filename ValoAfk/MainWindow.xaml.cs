namespace ValoAfk
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += InitOnTopRight;
            SetCurrentConfig();
        }
    }
}