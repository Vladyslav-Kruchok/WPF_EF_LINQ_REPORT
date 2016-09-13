using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Data.Entity;

namespace STDB
{
    public partial class MainWindow
    {
        /// <summary>
        /// View
        /// </summary>
        private void Upd_ToolDV()
        {
            CollectionViewSource toolsViewSource = ((CollectionViewSource)(this.FindResource("toolsViewSource")));//toolsViewSource
            CTX.Tools.Load();
            toolsViewSource.Source = m_context.Tools.Local;
        }


        /// <summary>
        /// Additional func
        /// </summary>
        private bool IsEmpty_TtoolBox()
        {
            if (Name_Tool_Tb.Text == "" | Amount_Tool_Tb.Text == "")
            {
                return true;
            }
            else return false;
        }
        private void Clear_ToolFields()
        {
            Name_Tool_Tb.Text = "";
            Amount_Tool_Tb.Text = "";
        }
        /// <summary>
        /// Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Tool_Bt_Click(object sender, RoutedEventArgs e)
        {
            if (!IsEmpty_TtoolBox())
            {
                var add_tool = new EF_Model.Tools(Name_Tool_Tb.Text, Amount_Tool_Tb.Text);
                CTX.Tools.Add(add_tool);
                CTX.SaveChanges();
                Upd_ToolDV();
                Clear_ToolFields();
            }
            else
            {
                MessageBox.Show("Fields are empty", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void Delete_Tool_Bt_Click(object sender, RoutedEventArgs e)
        {
            CTX.Tools.Load();
            var item = CTX.Tools.Find(((EF_Model.Tools)toolsDataGrid.SelectedItem).Id);//toolsDataGrid
            string msg = String.Format("Delete {0} {1}", item.Name, item.Amount);//item.Name, item.Amount
            string msg_shure = "Are you sure?";
            if(MessageBox.Show(msg, msg_shure, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                //modification status of row
                CTX.Entry(item).State = EntityState.Deleted;
                CTX.SaveChanges();
                Upd_ToolDV();
                Search_Cust_Lst.Clear();
            }
        }

    }
}
