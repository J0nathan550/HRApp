using CommunityToolkit.Mvvm.Input;
using HRApp.Views;
using ModernWpf.Controls;
using System.Windows.Data;

namespace HRApp.Models;

public partial class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Middlename { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public Job Position { get; set; }
    
    [RelayCommand]
    private void EditEmployee()
    {
        CreateUpdateEditEmployeeView createUpdateEditEmployeeView = new();
        createUpdateEditEmployeeView.ShowAsync();
        createUpdateEditEmployeeView.InitializeUpdate(true, this);
    }

    [RelayCommand]
    private async Task DeleteEmployee()
    {
        ContentDialog contentDialog = new()
        {
            Title = "Видалення Робітника",
            Content = "Ви впевнені, що хочете видалити цього робітника?",
            PrimaryButtonText = "Так",
            CloseButtonText = "Ні"
        };
        ContentDialogResult result = await contentDialog.ShowAsync();
        if (result == ContentDialogResult.Primary)
        {
            EditEmployeeView.viewModel.EmployeeCollection.RemoveAt(Id - 1);
            if (Position.Id > -1)
            {
                CreateJobView.viewModel.JobCollection[Position.Id - 1].Employee = null;
            }
            CollectionViewSource.GetDefaultView(EditEmployeeView.instance.DataGrid_Employees.ItemsSource).Refresh();
            CollectionViewSource.GetDefaultView(CreateJobView.instance.DataGridJob.ItemsSource).Refresh();
        }
    }
}