using HRApp.Models;
using HRApp.ViewModels;
using ModernWpf.Controls;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace HRApp.Views;

public partial class EditEmployeeView : UserControl
{
    public static EditEmployeeViewModel viewModel = new();
    public static EditEmployeeView instance;

    public EditEmployeeView()
    {
        DataContext = viewModel;
        InitializeComponent();
        instance = this;
        foreach (Employee employee in viewModel.EmployeeCollection)
        {
            employee.Position = CreateJobView.viewModel.JobCollection[employee.Id - 1];
        }
    }
}