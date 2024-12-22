using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HRApp.Models;
using HRApp.Views;
using System.Collections.ObjectModel;

namespace HRApp.ViewModels;

public partial class EditEmployeeViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Employee> _employeeCollection = 
    [
        new Employee { Id = 1, Name = "Олександр", Surname = "Шевченко", Middlename = "Іванович", Email = "olexandr.shevchenko@gmail.com", Phone = "+380671234567"},
        new Employee { Id = 2, Name = "Олена", Surname = "Коваль", Middlename = "Сергіївна", Email = "olena.koval@gmail.com", Phone = "+380671234568"},
        new Employee { Id = 3, Name = "Марія", Surname = "Бондар", Middlename = "Петрівна", Email = "maria.bondar@gmail.com", Phone = "+380671234569"},
        new Employee { Id = 4, Name = "Дмитро", Surname = "Сидоренко", Middlename = "Володимирович", Email = "dmytro.sydorenko@gmail.com", Phone = "+380671234570"},
        new Employee { Id = 5, Name = "Ірина", Surname = "Гнатюк", Middlename = "Миколаївна", Email = "iryna.hnatiuk@gmail.com", Phone = "+380671234571"},
        new Employee { Id = 6, Name = "Сергій", Surname = "Олійник", Middlename = "Андрійович", Email = "serhiy.oliynyk@gmail.com", Phone = "+380671234572"},
        new Employee { Id = 7, Name = "Наталія", Surname = "Мельник", Middlename = "Василівна", Email = "nataliya.melnyk@gmail.com", Phone = "+380671234573"},
        new Employee { Id = 8, Name = "Віталій", Surname = "Кравченко", Middlename = "Олександрович", Email = "vitaliy.kravchenko@gmail.com", Phone = "+380671234574"},
        new Employee { Id = 9, Name = "Тетяна", Surname = "Лисенко", Middlename = "Юріївна", Email = "tetyana.lysenko@gmail.com", Phone = "+380671234575"},
        new Employee { Id = 10, Name = "Андрій", Surname = "Романенко", Middlename = "Ігорович", Email = "andriy.romanenko@gmail.com", Phone = "+380671234576"}
    ];

    [RelayCommand]
    private static void AddEmployee()
    {
        CreateUpdateEditEmployeeView createUpdateEditEmployeeView = new();
        createUpdateEditEmployeeView.ShowAsync();
        createUpdateEditEmployeeView.InitializeUpdate();
    }
}