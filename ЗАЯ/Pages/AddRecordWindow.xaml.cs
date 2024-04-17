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
    
    public partial class AddRecordWindow : Window
    {
        public AddRecordWindow()
        {
            InitializeComponent();
        }
        public RequestData GetData()
        {
            RequestData newData = new RequestData
            {
                RequestNumber = GetNextRequestNumber(),
                DateAdded = DateTime.Now.ToString("yyyy-MM-dd"),
                Equipment = txtEquipment.Text,
                EquipmentType = txtEquipmentType.Text,
                IssueDescription = txtIssueDescription.Text,
                ClientName = txtClientName.Text,
                ClientPhone = txtClientPhone.Text,
                Master = "В ожидании",
                RequestStatus = "В ожидании",

            };

            return newData;
        }

        private int GetNextRequestNumber()
        {
            Random rnd = new Random();
            return rnd.Next(1000, 9999);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
