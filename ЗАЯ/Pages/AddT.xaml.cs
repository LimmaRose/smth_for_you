using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ЗАЯ.Pages
{
    public partial class AddT : Page
    {
        public ObservableCollection<RequestData> Requests { get; set; }
        public bool IsAdmin { get; set; }
        public AddT()
        {
            {
                InitializeComponent();
                Requests = new ObservableCollection<RequestData>();
                Requests.Add(new RequestData
                {
                    RequestNumber = 1,
                    DateAdded = "11.04.2024",
                    Equipment = "Printer",
                    EquipmentType = "Canon",
                    IssueType = "Paper jam",
                    IssueDescription = "Printer is not picking up paper",
                    ClientName = "Петр",
                    ClientPhone = "88773456234",
                    Master = "В ожидании",
                    RequestStatus = "В ожидании"
                });

                Requests.Add(new RequestData
                {
                    RequestNumber = 2,
                    DateAdded = "01.04.2024",
                    Equipment = "Computer",
                    EquipmentType = "Lenovo",
                    IssueType = "Slow performance",
                    IssueDescription = "Computer is running slow",
                    ClientName = "Иванов",
                    ClientPhone = "88773457823",
                    Master = "В ожидании",
                    RequestStatus = "В ожидании"
                });

                Requests.Add(new RequestData
                {
                    RequestNumber = 3,
                    DateAdded = "12.03.2024",
                    Equipment = "Scanner",
                    EquipmentType = "HP",
                    IssueType = "Connection problem",
                    IssueDescription = "Scanner is not connecting to the computer",
                    ClientName = "Сидоров",
                    ClientPhone = "88767856234",
                    Master = "Иванов Максим Александрович",
                    RequestStatus = "В работе"
                });

                Requests.Add(new RequestData
                {
                    RequestNumber = 4,
                    DateAdded = "16.04.2024",
                    Equipment = "Printer",
                    EquipmentType = "Canon",
                    IssueType = "Paper jam",
                    IssueDescription = "Printer is not picking up paper",
                    ClientName = "Козлов",
                    ClientPhone = "88773412344",
                    Master = "Иванов Максим Александрович",
                    RequestStatus = "В работе"
                });

                Requests.Add(new RequestData
                {
                    RequestNumber = 5,
                    DateAdded = "11.04.2024",
                    Equipment = "Printer",
                    EquipmentType = "HP",
                    IssueType = "Paper jam",
                    IssueDescription = "Printer is not picking up paper",
                    ClientName = "Николаев",
                    ClientPhone = "88263412044",
                    Master = "Иванов Максим Александрович",
                    RequestStatus = "Выполнено",
                    CompletionDate = new DateTime(2024, 3, 20, 12, 33, 0)
                });

                Requests.Add(new RequestData
                {
                    RequestNumber = 6,
                    DateAdded = DateTime.Now.ToShortDateString(),
                    Equipment = "Computer",
                    EquipmentType = "Asus",
                    IssueType = "Software issue",
                    IssueDescription = "Software is not working properly",
                    ClientName = "Петренко",
                    ClientPhone = "89263452200",
                    Master = "Иванов Максим Александрович",
                    RequestStatus = "Выполнено",
                    CompletionDate = DateTime.Now
                });
                dataGrid.ItemsSource = Requests;
            }
        }
        private void btnAddNewRecord_Click(object sender, RoutedEventArgs e)
        {
            var AddRecordWindow = new AddRecordWindow();

            bool? result = AddRecordWindow.ShowDialog();

            if (result == true)
            {
                RequestData newData = AddRecordWindow.GetData();
                Requests.Add(newData);
            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            RequestData selectedRequest = (RequestData)dataGrid.SelectedItem;

            if (selectedRequest != null)
            {
                Requests.Remove(selectedRequest);
            }
            else
            {
                MessageBox.Show("Выберите элемент для удаления.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            RequestData selectedRequest = (RequestData)dataGrid.SelectedItem;
            if (selectedRequest != null)
            {
                EditRecordWindow editRecordWindow = new EditRecordWindow(selectedRequest);
                bool? result = editRecordWindow.ShowDialog();
                if (result == true)
                {
                    RequestData editedData = editRecordWindow.GetEditedData();
                    int index = Requests.IndexOf(selectedRequest);

                    if (editedData.RequestStatus == "Выполнено")
                    {
                        SetRequestStatusToCompleted(editedData); // Вызываем метод для установки статуса "Выполнено" и даты выполнения
                    }

                    Requests[index] = editedData;
                }
            }
            else
            {
                MessageBox.Show("Выберите элемент для редактирования.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void SetRequestStatusToCompleted(RequestData request)
        {
            request.RequestStatus = "Выполнено";
            request.CompletionDate = DateTime.Now; // Устанавливаем текущую дату и время
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            string selectedStatus = ((ComboBoxItem)cmbFilter.SelectedItem).Content.ToString();
            if (selectedStatus == "Показать все")
            {
                dataGrid.ItemsSource = Requests;
            }
            else
            {
                var filteredRequests = new ObservableCollection<RequestData>(Requests.Where(r => r.RequestStatus == selectedStatus));
                dataGrid.ItemsSource = filteredRequests;
            }
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtSearchNumber.Text))
            {
                int searchNumber = int.Parse(txtSearchNumber.Text);
                var foundRequest = Requests.FirstOrDefault(r => r.RequestNumber == searchNumber);

                if (foundRequest != null)
                {
                    dataGrid.SelectedItem = foundRequest;
                    dataGrid.ScrollIntoView(foundRequest);
                }
                else
                {
                    MessageBox.Show("Заявка с указанным номером не найдена.", "Поиск заявки", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Введите номер заявки для поиска.", "Поиск заявки", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new Pages.Regis());
        }
        private void btnCalculateStatistics_Click(object sender, RoutedEventArgs e)
        {
            if (!IsAdmin)
            {
                MessageBox.Show("Только администратор может просматривать статистику.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            int completedRequests = Requests.Count(r => r.RequestStatus == "Выполнено");
            if (completedRequests == 0)
            {
                MessageBox.Show("Нет выполненных заявок.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            double totalCompletionTime = 0;
            foreach (RequestData request in Requests)
            {
                if (request.RequestStatus == "Выполнено")
                {
                    TimeSpan completionTime = request.CompletionDate.Value - DateTime.Parse(request.DateAdded);
                    totalCompletionTime += completionTime.TotalHours;
                }
            }

            double averageCompletionTime = totalCompletionTime / completedRequests;

            Dictionary<string, int> issueTypeStatistics = new Dictionary<string, int>();
            foreach (RequestData request in Requests)
            {
                if (request.RequestStatus == "Выполнено")
                {
                    if (issueTypeStatistics.ContainsKey(request.IssueType))
                    {
                        issueTypeStatistics[request.IssueType]++;
                    }
                    else
                    {
                        issueTypeStatistics.Add(request.IssueType, 1);
                    }
                }
            }

            StringBuilder statistics = new StringBuilder();
            statistics.AppendLine("Статистика работы отдела обслуживания:");
            statistics.AppendLine("Количество выполненных заявок: " + completedRequests);
            statistics.AppendLine("Среднее время выполнения заявки: " + averageCompletionTime.ToString("F2") + " ч.");
            statistics.AppendLine("Статистика по типам неисправностей:");

            foreach (var stat in issueTypeStatistics)
            {
                statistics.AppendLine($"Тип неисправности: {stat.Key}");
            }

            MessageBox.Show(statistics.ToString(), "Статистика", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }


    public class RequestData
    {
        public int RequestNumber { get; set; }
        public string DateAdded { get; set; }
        public string Equipment { get; set; }
        public string EquipmentType { get; set; }
        public string IssueDescription { get; set; }
        public string ClientName { get; set; }
        public string ClientPhone { get; set; }
        public string Master { get; set; }
        public string IssueType { get; set; }
        public string RequestStatus { get; set; }
        public string Comments { get; set; }
        public DateTime? CompletionDate { get; set; }
    }
}