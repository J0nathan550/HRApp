using HRApp.Models;
using HRApp.ViewModels;
using System.Windows.Controls;

namespace HRApp.Views;

public partial class CreateJobView : UserControl
{
    public static CreateJobViewModel viewModel = new();
    public static CreateJobView instance;

    public CreateJobView()
    {
        DataContext = viewModel;
        InitializeComponent();
        instance = this;
        foreach (Job job in viewModel.JobCollection)
        {
            job.Employee = EditEmployeeView.viewModel.EmployeeCollection[job.Id - 1];
        }
    }
}