using System.Windows;
using AI_Lab1.ViewModel;

namespace AI_Lab1;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MyViewModel();
    }
}