using CommunityToolkit.Mvvm.Input;
using HRApp.Views;
using ModernWpf.Controls;
using System.Windows.Data;

namespace HRApp.Models;

public partial class Job
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string TimeOfWork { get; set; }
    public float PayInMonth { get; set; }
    public string PayInMonthString 
    { 
        get => PayInMonth.ToString("F2") + "$";
        set
        {
            if (float.TryParse(value, out float result))
            {
                PayInMonth = result;
            }
        }
    }
    public Employee Employee { get; set; }

    [RelayCommand]
    private void EditJob()
    {
        CreateUpdateJobView createUpdateJobView = new();
        createUpdateJobView.ShowAsync();
        createUpdateJobView.InitializeUpdate(true, this);
    }

    [RelayCommand]
    private async Task DeleteJob()
    {
        ContentDialog contentDialog = new()
        {
            Title = "Видалення Вакансії",
            Content = "Ви впевнені, що хочете видалити цю вакансію?",
            PrimaryButtonText = "Так",
            CloseButtonText = "Ні"
        };
        ContentDialogResult result = await contentDialog.ShowAsync();
        if (result == ContentDialogResult.Primary)
        {
            if (Employee != null)
            {
                EditEmployeeView.viewModel.EmployeeCollection[Employee.Id - 1].Position = null;
                CreateJobView.viewModel.JobCollection[Id - 1].Employee = null;
            }
            CreateJobView.viewModel.JobCollection.RemoveAt(Id - 1);
            CollectionViewSource.GetDefaultView(EditEmployeeView.instance.DataGrid_Employees.ItemsSource).Refresh();
            CollectionViewSource.GetDefaultView(CreateJobView.instance.DataGridJob.ItemsSource).Refresh();
        }
    }
}