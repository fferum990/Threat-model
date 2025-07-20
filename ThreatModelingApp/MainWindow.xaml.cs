using System.Windows;
using ThreatModelingApp.ViewModels;

namespace ThreatModelingApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel(); // Привязка контекста
        }
    }
}