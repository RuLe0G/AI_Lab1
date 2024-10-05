using System.Windows;
using AI_Lab1.ViewModel;

namespace AI_Lab1;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MyViewModel();
    }
}