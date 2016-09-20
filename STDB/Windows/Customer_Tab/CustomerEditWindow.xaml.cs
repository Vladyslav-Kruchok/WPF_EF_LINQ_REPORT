using System;
using System.Windows;
using System.Data.Entity;

namespace STDB
{
    /// <summary>
    /// Interaction logic for CustomerEditWindow.xaml
    /// </summary>
    public partial class CustomerEditWindow : Window
    {
        private EF_Model.Customer m_person;
        private EF_Model.STDB_Entities m_context;
        public CustomerEditWindow()
        {
            InitializeComponent();
        }
        public CustomerEditWindow (ref EF_Model.Customer f_person, EF_Model.STDB_Entities f_context)
        {
            InitializeComponent();
            m_person = f_person;
            m_context = f_context;
            Initialize_CustEditBox(ref m_person);
        }
        private void Initialize_CustEditBox(ref EF_Model.Customer f_person)
        {
            Name_Tb.Text = f_person.Name.ToString();
            Address_Tb.Text = f_person.Address_customer.ToString();
            Date_Dp.Text = f_person.Date_in.ToString();
        }
        private void Initialize_Person()
        {
            m_person.Name = Name_Tb.Text;
            m_person.Address_customer = Address_Tb.Text;
            m_person.Date_in = DateTime.Parse(Date_Dp.Text);
        }
        private void Save_Ch_Cust_Bt_Click(object sender, RoutedEventArgs e)
        {
            Initialize_Person();
            string msg = String.Format("Save the changes {0} {1}", m_person.Name, m_person.Address_customer);
            string msg_shure = "Are you sure?";
            if (MessageBox.Show(msg, msg_shure, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                m_context.Entry(m_person).State = EntityState.Modified;
                m_context.SaveChanges();
            }
            this.DialogResult = true;
            this.Close();
        }
    }
}
