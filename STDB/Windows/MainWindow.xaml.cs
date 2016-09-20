using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;

namespace STDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private EF_Model.STDB_Entities m_context;
        public EF_Model.STDB_Entities CTX
        {
            set
            {
                m_context = value;
            }
            get
            {
                if (m_context == null)
                {
                    m_context = new EF_Model.STDB_Entities(MCS.ConnStrEntity);
                }
                return m_context;
            }
        }
        /// <summary>
        /// Customer tab
        /// </summary>
        private List<EF_Model.Customer> m_search_customer_list;
        public List<EF_Model.Customer> Search_Cust_Lst
        {
            set
            {
                m_search_customer_list = value;
            }
            get
            {
                if (m_search_customer_list == null)
                {
                    m_search_customer_list = new List<EF_Model.Customer>();
                }
                return m_search_customer_list;
            }
        }
        /// <summary>
        /// Tool tab
        /// </summary>
        List<EF_Model.Tools> m_search_tool_list;
        public List<EF_Model.Tools> Search_Tool_Lst
        {
            set
            {
                m_search_tool_list = value;   
            }
            get
            {
                if(m_search_tool_list == null)
                {
                    m_search_tool_list = new List<EF_Model.Tools>();
                }
                return m_search_tool_list;
            }
        }
        /// <summary>
        /// Order tab
        /// </summary>
        private List<EF_Model.O_rder> m_search_order_list;
        public List<EF_Model.O_rder> Search_Order_Lst
        {
            set
            {
                m_search_order_list = value;
            }
            get
            {
                if (m_search_order_list == null)
                {
                    m_search_order_list = new List<EF_Model.O_rder>();
                }
                return m_search_order_list;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            //Customer Tab
            // entity for use bd
            CTX = new EF_Model.STDB_Entities(MCS.ConnStrEntity);
            //varyable to save some result of searching, Search_Cust(), 
            Search_Cust_Lst = new List<EF_Model.Customer>();
            //Tools Tab
            //varyable to save some result of searching, Search_Tool(),
            Search_Tool_Lst = new List<EF_Model.Tools>();
            //Order Tab
            Search_Order_Lst = new List<EF_Model.O_rder>();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Upd_CustDV();
            Upd_ToolDV();
            Upd_OrderDV();
        }
        private void Upd_TableDV<T>(List<T> view, string f_view_source)
        {
            CollectionViewSource customerViewSource = ((CollectionViewSource)(this.FindResource(f_view_source)));
            customerViewSource.Source = view;
        }
    }
}
