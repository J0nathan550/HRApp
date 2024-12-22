using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HRApp.Models;
using HRApp.Views;
using System.Collections.ObjectModel;

namespace HRApp.ViewModels;

public partial class CreateJobViewModel : ObservableObject
{
    public ObservableCollection<Job> JobCollection { get; set; } = 
    [
        new Job { Id = 1, Name = "Junior Developer", PayInMonth = 5000, TimeOfWork = "09:00 - 18:00" },
        new Job { Id = 2, Name = "Middle Developer", PayInMonth = 8000, TimeOfWork = "10:00 - 19:00" },
        new Job { Id = 3, Name = "Senior Developer", PayInMonth = 12000, TimeOfWork = "10:00 - 18:00" },
        new Job { Id = 4, Name = "DevOps Engineer", PayInMonth = 11000, TimeOfWork = "09:30 - 18:30" },
        new Job { Id = 5, Name = "QA Engineer", PayInMonth = 7000, TimeOfWork = "10:00 - 18:00" },
        new Job { Id = 6, Name = "UI/UX Designer", PayInMonth = 7500, TimeOfWork = "11:00 - 19:00" },
        new Job { Id = 7, Name = "Product Manager", PayInMonth = 10000, TimeOfWork = "09:00 - 17:00" },
        new Job { Id = 8, Name = "System Administrator", PayInMonth = 6000, TimeOfWork = "08:00 - 16:00" },
        new Job { Id = 9, Name = "Data Scientist", PayInMonth = 13000, TimeOfWork = "10:00 - 18:00" },
        new Job { Id = 10, Name = "Tech Support Specialist", PayInMonth = 4000, TimeOfWork = "09:00 - 17:00" },
    ];

    [RelayCommand]
    private static void AddJob()
    {
        CreateUpdateJobView createUpdateJobView = new();
        createUpdateJobView.ShowAsync();
        createUpdateJobView.InitializeUpdate();
    }
}