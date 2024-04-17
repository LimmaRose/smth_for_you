using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace ЗАЯ.Pages
{
    public partial class EditRecordWindow : Window
    {
        private RequestData originalData;

        public EditRecordWindow(RequestData requestData)
        {
            InitializeComponent();

            originalData = requestData;

            txtRequestNumber.Text = requestData.RequestNumber.ToString();
            txtDateAdded.Text = requestData.DateAdded;
            txtEquipment.Text = requestData.Equipment;
            txtEquipmentType.Text = requestData.EquipmentType;
            txtIssueDescription.Text = requestData.IssueDescription;
            txtClientName.Text = requestData.ClientName;
            txtClientPhone.Text = requestData.ClientPhone;
            txtIssueType.Text = requestData.IssueType;
            txtComments.Text = requestData.Comments;
            cmbMaster.Text = requestData.Master;
            // Устанавливаем выбранный элемент в комбобоксе
            cmbStatus.SelectedItem = requestData.RequestStatus;
        }

        public RequestData GetEditedData()
        {
            RequestData editedData = new RequestData
            {
                RequestNumber = originalData.RequestNumber,
                DateAdded = originalData.DateAdded,
                Equipment = txtEquipment.Text ?? "",
                EquipmentType = txtEquipmentType.Text ?? "",
                IssueDescription = txtIssueDescription.Text ?? "",
                ClientName = txtClientName.Text ?? "",
                ClientPhone = txtClientPhone.Text ?? "",
                IssueType = txtIssueType.Text ?? "",
                Comments = txtComments.Text ?? "",
                Master = cmbMaster.Text,
                RequestStatus = cmbStatus.Text  // Получаем текст выбранного элемента комбобокса
            };

            return editedData;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            RequestData editedData = GetEditedData();

            if (editedData.RequestStatus == "Выполнено")
            {
                editedData.CompletionDate = DateTime.Now; // Устанавливаем текущую дату при выполнении
            }
            DialogResult = true;
        }
    }
}
