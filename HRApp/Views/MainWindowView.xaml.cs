using ModernWpf.Controls;
using System.Windows;

namespace HRApp.Views;

public partial class MainWindowView : Window
{
    public static MainWindowView instance;

    private readonly EditEmployeeView editEmployeeView = new();
    private readonly CreateJobView createJobView = new();

    public MainWindowView()
    {
        InitializeComponent();
        instance = this;
        NavigationView.SelectedItem = NavigationView.MenuItems[0];
        MainFrame.Navigate(editEmployeeView);
    }

    private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.SelectedItem is NavigationViewItem selectedItem)
        {
            switch (selectedItem.Tag)
            {
                case "CreateJob":
                    MainFrame.Navigate(createJobView);
                    break;
                case "EditEmployee":
                    MainFrame.Navigate(editEmployeeView);
                    break;
            }
        }
    }
}