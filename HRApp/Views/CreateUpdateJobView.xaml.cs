using HRApp.Models;
using ModernWpf.Controls.Primitives;
using ModernWpf.Controls;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;

namespace HRApp.Views;

public partial class CreateUpdateJobView : ContentDialog
{
    private bool isOkay = false;
    private int jobId = -1;
    public bool IsUpdating = false;

    public CreateUpdateJobView()
    {
        InitializeComponent();
        SaveButton.IsEnabled = isOkay;
    }

    public void InitializeUpdate(bool isUpdating = false, Job job = null)
    {
        IsUpdating = isUpdating;
        if (!isUpdating)
        {
            TitleTextBlock.Text = "Створення Вакансії";
        }
        else
        {
            TitleTextBlock.Text = "Редагування Вакансії";
            if (job != null)
            {
                jobId = job.Id;
                NameOfPosition.Text = job.Name;
                TimeTextBox.Text = job.TimeOfWork;
                PaycheckTextBox.Text = job.PayInMonth.ToString();
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
        if (string.IsNullOrWhiteSpace(NameOfPosition.Text))
        {
            ControlHelper.SetHeader(NameOfPosition, "Поле не може бути порожнім!");
            return false;
        }
        else
        {
            ControlHelper.SetHeader(NameOfPosition, string.Empty);
        }
        if (!float.TryParse(PaycheckTextBox.Text, out float result))
        {
            ControlHelper.SetHeader(PaycheckTextBox, "Будь ласка, введіть дійсну суму зарплати.");
            return false;
        }
        else
        {
            ControlHelper.SetHeader(PaycheckTextBox, string.Empty);
        }
        if (string.IsNullOrWhiteSpace(TimeTextBox.Text))
        {
            ControlHelper.SetHeader(TimeTextBox, "Поле не може бути порожнім!");
            return false;
        }
        else
        {
            ControlHelper.SetHeader(TimeTextBox, string.Empty);
        }
        return true;
    }

    private void SaveButton_Clicked(object sender, RoutedEventArgs e)
    {
        if (!isOkay)
        {
            SaveButton.IsEnabled = isOkay;
            return;
        }

        if (!IsUpdating)
        {
            Job newJob = new()
            {
                Id = CreateJobView.viewModel.JobCollection.Count + 1,
                Name = NameOfPosition.Text,
                TimeOfWork = TimeTextBox.Text,
                PayInMonth = float.Parse(PaycheckTextBox.Text)
            };
            CreateJobView.viewModel.JobCollection.Add(newJob);
        }
        else
        {
            CreateJobView.viewModel.JobCollection[jobId - 1].Name = NameOfPosition.Text;
            CreateJobView.viewModel.JobCollection[jobId - 1].TimeOfWork = TimeTextBox.Text;
            CreateJobView.viewModel.JobCollection[jobId - 1].PayInMonth = float.Parse(PaycheckTextBox.Text);
        }

        CollectionViewSource.GetDefaultView(CreateJobView.instance.DataGridJob.ItemsSource).Refresh();
        Hide();
    }

    private void CloseButton_Clicked(object sender, RoutedEventArgs e)
    {
        Hide();
    }
}