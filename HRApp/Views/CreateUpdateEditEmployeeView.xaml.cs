using HRApp.Models;
using HRApp.ViewModels;
using ModernWpf.Controls;
using ModernWpf.Controls.Primitives;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HRApp.Views;

public partial class CreateUpdateEditEmployeeView : ContentDialog
{
    private bool isOkay = false;
    private readonly CreateUpdateEditEmployeeViewModel viewModel = new();
    public bool IsUpdating = false;
    private int employeeId = -1;
    public CreateUpdateEditEmployeeView()
    {
        DataContext = viewModel;
        InitializeComponent();
        SaveButton.IsEnabled = isOkay;
    }

    public void InitializeUpdate(bool isUpdating = false, Employee employee = null)
    {
        IsUpdating = isUpdating;
        if (!isUpdating)
        {
            TitleTextBlock.Text = "Створення Робітника";
        }
        else
        {
            TitleTextBlock.Text = "Редагування Робітника";
            if (employee != null)
            {
                employeeId = employee.Id;
                FirstNameTextBox.Text = employee.Name;
                LastNameTextBox.Text = employee.Surname;
                MiddleNameTextBox.Text = employee.Middlename;
                EmailTextBox.Text = employee.Email;
                PhoneNumberTextBox.Text = employee.Phone;

                Job matchingJob = ((ObservableCollection<Job>)PositionComboBox.ItemsSource)
                    .FirstOrDefault(job => job.Id == employee.Position.Id);
                if (matchingJob != null)
                {
                    PositionComboBox.SelectedIndex = PositionComboBox.Items.IndexOf(matchingJob);
                }
            }
        }
    }

    private void OnTextBoxTextChanged(object sender, TextChangedEventArgs e)
    {
        isOkay = ValidateAllFields();
        SaveButton.IsEnabled = isOkay;
    }

    private bool ValidateAllFields()
    {
        if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text))
        {
            ControlHelper.SetHeader(FirstNameTextBox, "Поле не може бути порожнім!");
            return false;
        }
        else
        {
            ControlHelper.SetHeader(FirstNameTextBox, string.Empty);
        }
        if (string.IsNullOrWhiteSpace(LastNameTextBox.Text))
        {
            ControlHelper.SetHeader(LastNameTextBox, "Поле не може бути порожнім!");
            return false;
        }
        else
        {
            ControlHelper.SetHeader(LastNameTextBox, string.Empty);
        }
        if (string.IsNullOrWhiteSpace(MiddleNameTextBox.Text))
        {
            ControlHelper.SetHeader(MiddleNameTextBox, "Поле не може бути порожнім!");
            return false;
        }
        else
        {
            ControlHelper.SetHeader(MiddleNameTextBox, string.Empty);
        }
        if (string.IsNullOrWhiteSpace(EmailTextBox.Text) || !IsValidEmail(EmailTextBox.Text))
        {
            ControlHelper.SetHeader(EmailTextBox, "Введіть дійсну пошту!");
            return false;
        }
        else
        {
            ControlHelper.SetHeader(EmailTextBox, string.Empty);
        }
        if (string.IsNullOrWhiteSpace(PhoneNumberTextBox.Text) || !IsValidPhoneNumber(PhoneNumberTextBox.Text))
        {
            ControlHelper.SetHeader(PhoneNumberTextBox, "Введіть дійсний номер телефону!");
            return false;
        }
        else
        {
            ControlHelper.SetHeader(PhoneNumberTextBox, string.Empty);
        }
        if (PositionComboBox.SelectedItem == null)
        {
            ControlHelper.SetHeader(PositionComboBox, "Будь ласка, оберіть посаду!");
            return false;
        }
        else
        {
            ControlHelper.SetHeader(PositionComboBox, string.Empty);
        }
        return true;
    }

    private static bool IsValidEmail(string email)
    {
        Regex emailRegex = new(@"^[^\s@]+@[^\s@]+\.[^\s@]+$");
        return emailRegex.IsMatch(email);
    }

    private static bool IsValidPhoneNumber(string phoneNumber)
    {
        Regex phoneRegex = new(@"^\+[\d]{10,12}$");
        return phoneRegex.IsMatch(phoneNumber);
    }

    private void SaveButton_Clicked(object sender, RoutedEventArgs e)
    {
        if (!isOkay)
        {
            SaveButton.IsEnabled = isOkay;
            return;
        }

        Employee employee = IsUpdating
            ? EditEmployeeView.viewModel.EmployeeCollection[employeeId - 1]
            : new Employee
            {
                Id = EditEmployeeView.viewModel.EmployeeCollection.Count + 1,
                Name = FirstNameTextBox.Text,
                Surname = LastNameTextBox.Text,
                Middlename = MiddleNameTextBox.Text,
                Email = EmailTextBox.Text,
                Phone = PhoneNumberTextBox.Text,
                Position = viewModel.SelectedJob
            };

        if (!IsUpdating)
        {
            EditEmployeeView.viewModel.EmployeeCollection.Add(employee);
        }
        else
        {
            employee.Name = FirstNameTextBox.Text;
            employee.Surname = LastNameTextBox.Text;
            employee.Middlename = MiddleNameTextBox.Text;
            employee.Email = EmailTextBox.Text;
            employee.Phone = PhoneNumberTextBox.Text;
            if (viewModel.SelectedJob.Id == -1)
            {
                CreateJobView.viewModel.JobCollection[employee.Position.Id - 1].Employee = null;
            }
            employee.Position = viewModel.SelectedJob;
        }

        if (viewModel.SelectedJob != null)
        {
            Job job = CreateJobView.viewModel.JobCollection.FirstOrDefault(j => j.Id == viewModel.SelectedJob.Id);
            if (job != null)
            {
                job.Employee = employee;
                employee.Position = job;
            }
        }
        else
        {
            employee.Position = null;
        }

        // Refresh both grids
        CollectionViewSource.GetDefaultView(EditEmployeeView.instance.DataGrid_Employees.ItemsSource).Refresh();
        CollectionViewSource.GetDefaultView(CreateJobView.instance.DataGridJob.ItemsSource).Refresh();

        Hide();
    }


    private void CloseButton_Clicked(object sender, RoutedEventArgs e)
    {
        Hide();
    }

    private void PositionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (PositionComboBox.SelectedItem == null)
        {
            isOkay = false;
            string errorMessage = "Будь ласка, оберіть посаду.";
            ControlHelper.SetHeader(PositionComboBox, errorMessage);
        }
        else
        {
            ControlHelper.SetHeader(PositionComboBox, string.Empty);
            OnTextBoxTextChanged(FirstNameTextBox, null);
        }
    }
}