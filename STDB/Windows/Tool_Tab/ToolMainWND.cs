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
        /// Tool View
        /// </summary>
        private void Upd_ToolDV()
        {
            CollectionViewSource toolsViewSource = ((CollectionViewSource)(this.FindResource("toolsViewSource")));//toolsViewSource
            CTX.Tools.Load();
            toolsViewSource.Source = m_context.Tools.Local;
        }
        /// <summary>
        /// Tool Additional func
        /// </summary>
        private bool IsEmpty_TtoolBox()
        {
            if (Name_Tool_Tb.Text == "" & Amount_Tool_Tb.Text == "")
            {
                return true;
            }
            else return false;
        }
        private bool IsEmpty_ToolVaryable(ref EF_Model.Tools f_tool)
        {
            if (f_tool.Name == null && f_tool.Amount == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void Clear_ToolFields()
        {
            Name_Tool_Tb.Text = "";
            Amount_Tool_Tb.Text = "";
        }
        private void Search_Tool(EF_Model.Tools f_set_tool_search)// linq does not eссept ref
        {
            Search_Tool_Lst = CTX.Tools.Where(p => (p.Name.StartsWith(f_set_tool_search.Name))).ToList();     
        }
        /// <summary>
        /// Tool Button
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
            Clear_ToolFields();
            CTX.Tools.Load();
            var item = CTX.Tools.Find(((EF_Model.Tools)toolsDataGrid.SelectedItem).Id);//toolsDataGrid
            string msg = String.Format("Delete {0} {1}", item.Name, item.Amount);//item.Name, item.Amount
            string msg_shure = "Are you sure?";
            if (MessageBox.Show(msg, msg_shure, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                //modification status of row
                CTX.Entry(item).State = EntityState.Deleted;
                CTX.SaveChanges();
                Upd_ToolDV();
                Search_Cust_Lst.Clear();
            }    
        }
        private void Edit_Tool_Bt_Click(object sender, RoutedEventArgs e)
        {
            //select, find, get id of tool
            object selected_item = toolsDataGrid.SelectedItem;
            int tool_id = ((EF_Model.Tools)selected_item).Id;
            CTX.Tools.Load();
            var item = CTX.Tools.Find(tool_id);
            //to send data to edit form
            ToolEditWindow tool_edit_wnd = new ToolEditWindow(ref item, ref m_context);
            tool_edit_wnd.ShowDialog();
            if (tool_edit_wnd.DialogResult == true)
            {
                Search_Tool(item);
                Upd_TableDV<EF_Model.Tools>(Search_Tool_Lst, "toolsViewSource");
            }            
            Clear_ToolFields();
        }
        private void Search_Tool_Bt_Click(object sender, RoutedEventArgs e)
        {
            var set_tool_search = new EF_Model.Tools(Name_Tool_Tb.Text, Amount_Tool_Tb.Text);
            if (IsEmpty_TtoolBox() | set_tool_search.Amount != null)
            {
                Upd_ToolDV();
                Search_Tool_Lst.Clear();
                return;
            }
            else
            {
                //to load a dta to context
                CTX.Tools.Load();
                //search
                Search_Tool(set_tool_search);
                Upd_TableDV<EF_Model.Tools>(Search_Tool_Lst, "toolsViewSource");
            }
        }
        private void Print_Tool_Bt_Click(object sender, RoutedEventArgs e)
        {
            if (Search_Tool_Lst.Count != 0)
            {
                var tool_rep_wnd = new ToolReportWindow(Search_Tool_Lst);
                tool_rep_wnd.ShowDialog();
            }
            else
            {
                var tool_rep_wnd = new ToolReportWindow(CTX.Tools);
                tool_rep_wnd.ShowDialog();
            }
        }

    }
}
