using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Windows.Controls.Primitives;

namespace STDB
{
    public partial class MainWindow
    {
        /// <summary>
        /// View
        /// </summary>
        /// <param name="context"></param>
        private void Upd_CustDV()
        {
            CollectionViewSource customerViewSource = ((CollectionViewSource)(this.FindResource("customerViewSource")));//customerViewSource
            CTX.Customer.Load();
            customerViewSource.Source = m_context.Customer.Local;
        }
        private void Upd_CustDV<Ttype>(List<Ttype> view)
        {
            CollectionViewSource customerViewSource = ((CollectionViewSource)(this.FindResource("customerViewSource")));
            customerViewSource.Source = view;
        }
        /// <summary>
        /// Additional func
        /// </summary>
        private void Clear_CustFields()
        {
            Name_Tb.Text = "";
            Address_Tb.Text = "";
            Date_Dp.Text = "";
            Name_ChBox.IsChecked = false;
            Address_ChBox.IsChecked = false;
            Date_ChBox.IsChecked = false;
        }
        private bool IsEmpty_CustBox()
        {
            if (Name_Tb.Text == "" | Address_Tb.Text == "" | Date_Dp.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool IsEmpty_CustVaryable(ref EF_Model.Customer f_customer)
        {
            if (f_customer.Name == null & f_customer.Address_customer == null & f_customer.Date_in == new DateTime(0001, 01, 01, 0, 00, 00))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void Checked_Cust_Fields(bool f_name = true, bool f_address = true, bool f_date = true)
        {
            Name_ChBox.IsChecked = f_name;
            Address_ChBox.IsChecked = f_address;
            Date_ChBox.IsChecked = f_date;
        }
        private AddTools.SearchedFields Searched_Choice()
        {
            if (Name_ChBox.IsChecked == true && Address_ChBox.IsChecked == true && Date_ChBox.IsChecked == true)
            {
                return AddTools.SearchedFields.All;
            }

            if (Name_ChBox.IsChecked == true)
            {
                if (Date_ChBox.IsChecked == true)
                {
                    return AddTools.SearchedFields.NameDate;
                }
                if (Address_ChBox.IsChecked == true)
                {
                    return AddTools.SearchedFields.NameAddress;
                }
                return AddTools.SearchedFields.Name;
            }
            
            if (Address_ChBox.IsChecked == true)
            {
                if (Date_ChBox.IsChecked == true)
                {
                    return AddTools.SearchedFields.AddressDate;
                }
                return AddTools.SearchedFields.Address;
            }
           
            if (Date_ChBox.IsChecked == true)
            {
                return AddTools.SearchedFields.Date;
            }
            else
            {
                return AddTools.SearchedFields.None;
            }
        }
        private void Search_Cust(EF_Model.Customer f_set_cust_search)// linq does not eссept ref
        {
            var choice = Searched_Choice();
            switch (choice)
            {
                case AddTools.SearchedFields.Name:
                    Search_Cust_Lst = CTX.Customer.Where(p => (p.Name == f_set_cust_search.Name)).ToList();
                    break;
                case AddTools.SearchedFields.NameDate:
                    Search_Cust_Lst = CTX.Customer.Where(p => (p.Name == f_set_cust_search.Name && p.Date_in == f_set_cust_search.Date_in)).ToList();
                    break;
                case AddTools.SearchedFields.NameAddress:
                    Search_Cust_Lst = CTX.Customer.Where(p => (p.Name == f_set_cust_search.Name && p.Address_customer == f_set_cust_search.Address_customer)).ToList();
                    break;
                case AddTools.SearchedFields.Address:
                    Search_Cust_Lst = CTX.Customer.Where(p => (p.Address_customer == f_set_cust_search.Address_customer)).ToList();
                    break;
                case AddTools.SearchedFields.AddressDate:
                    Search_Cust_Lst = CTX.Customer.Where(p => (p.Address_customer == f_set_cust_search.Address_customer && p.Date_in == f_set_cust_search.Date_in)).ToList();
                    break;
                case AddTools.SearchedFields.Date:
                    Search_Cust_Lst = CTX.Customer.Where(p => (p.Date_in == f_set_cust_search.Date_in)).ToList();
                    break;
                case AddTools.SearchedFields.All:
                    {
                        Search_Cust_Lst = CTX.Customer.Where(
                       p => (p.Name.StartsWith(f_set_cust_search.Name)  &&
                             p.Address_customer == f_set_cust_search.Address_customer &&
                             p.Date_in == f_set_cust_search.Date_in)).ToList();
                    }
                    break;
                default://default and none
                    {
                        //linq
                        Search_Cust_Lst = CTX.Customer.Where(
                        p => (p.Name.StartsWith(f_set_cust_search.Name) ||
                              p.Address_customer.StartsWith(f_set_cust_search.Address_customer) ||
                              p.Date_in == f_set_cust_search.Date_in)).ToList();
                        //linq
                    }
                    break;
            }
        }
        /// <summary>
        /// Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Test_Conn_Bt_Click(object sender, RoutedEventArgs e)
        {
            //MCS conn_entity = new MCS();
            using (var connect_to_entity = new EntityConnection(MCS.ConnStrEntity))
            {
                try
                {
                    connect_to_entity.Open();
                    if (connect_to_entity.State == System.Data.ConnectionState.Open)
                    {
                        Test_Conn_Bt.Background = new SolidColorBrush(Colors.Green);
                        Test_Conn_Bt.Content = connect_to_entity.State.ToString();
                    }
                    else
                    {
                        MessageBox.Show(connect_to_entity.State.ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(connect_to_entity.State.ToString());
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void Print_Cust_Bt_Click(object sender, RoutedEventArgs e)
        {
            if (Search_Cust_Lst.Count != 0)
            {
                var cust_rep_wnd = new CustomerReportWindow(Search_Cust_Lst);
                cust_rep_wnd.ShowDialog();
            }
            else
            {
                var cust_rep_wnd = new CustomerReportWindow(CTX.Customer);
                cust_rep_wnd.ShowDialog();
            }
        }
        private void Delete_Cust_Bt_Click(object sender, RoutedEventArgs e)
        {

            CTX.Customer.Load();
            //to transmit "id" of the chosen row to varyable person
            var person = CTX.Customer.Find(((EF_Model.Customer)customerDataGrid.SelectedItem).Id);//customerDataGrid
            //shure message
            string msg = String.Format("Delete {0} {1}", person.Name, person.Address_customer);//person.Name, person.Address_customer
            string msg_shure = "Are you sure?";
            if (MessageBox.Show(msg, msg_shure, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                //modification status of row
                CTX.Entry(person).State = EntityState.Deleted;//person
                CTX.SaveChanges();
                Upd_CustDV();
                Search_Cust_Lst.Clear();
            }
        }  
        private void Search_Cust_Bt_Click(object sender, RoutedEventArgs e)
        {
            var set_cust_search = new EF_Model.Customer(Name_Tb.Text, Address_Tb.Text, Date_Dp.Text);
            if (IsEmpty_CustVaryable(ref set_cust_search))
            {
                Upd_CustDV();
                Search_Cust_Lst.Clear();
                return;
            }
            else
            {
                //to load a data to context
                CTX.Customer.Load();
                //search
                Search_Cust(set_cust_search);
                //view
                Upd_CustDV<EF_Model.Customer>(Search_Cust_Lst);
                Clear_CustFields();
            }
        }
        private void Add_Cust_Bt_Click(object sender, RoutedEventArgs e)
        {
            if (!IsEmpty_CustBox())
            {
                var add_cust = new EF_Model.Customer(Name_Tb.Text, Address_Tb.Text, Date_Dp.Text);
                CTX.Customer.Add(add_cust);
                CTX.SaveChanges();
                Upd_CustDV();
                Clear_CustFields();
            }
            else
            {
                MessageBox.Show("Fields are empty", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void Edit_Cust_Bt_Click(object sender, RoutedEventArgs e)
        {
            //select customer
            object selected_item = customerDataGrid.SelectedItem;
            int searched_id = ((EF_Model.Customer)selected_item).Id;
            CTX.Customer.Load();
            var person = CTX.Customer.Find(searched_id);
            //to send data into edit form
            CustomerEditWindow cust_edit_wnd = new CustomerEditWindow(ref person, ref m_context);
            cust_edit_wnd.ShowDialog();//return true if row was changed or false if cancel 
            if (cust_edit_wnd.DialogResult == true)
            {
                //select all(default)
                Checked_Cust_Fields();
                //linq
                Search_Cust(person);
                //linq
                Upd_CustDV<EF_Model.Customer>(Search_Cust_Lst);
            }      
            Clear_CustFields();
        }
    }
}
