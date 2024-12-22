using CommunityToolkit.Mvvm.ComponentModel;
using HRApp.Models;
using HRApp.Views;
using System.Collections.ObjectModel;

namespace HRApp.ViewModels;

public partial class CreateUpdateEditEmployeeViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Job> _jobs = [];
    [ObservableProperty]
    private Job _selectedJob;

    public CreateUpdateEditEmployeeViewModel()
    {
        Jobs.Add(new Job { Id = -1, Name = "Без посади", PayInMonth = 0, TimeOfWork = "00:00 - 00:00" });
        foreach (Job job in CreateJobView.viewModel.JobCollection)
        {
            Jobs.Add(job);
        }
    }
}