using System.Collections.Generic;
using System.Windows;

namespace STDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        /// <summary>
        /// Customer tab
        /// </summary>
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

        public MainWindow()
        {
            InitializeComponent();
            //Customer Tab
            // entity for use bd
            CTX = new EF_Model.STDB_Entities(MCS.ConnStrEntity);
            //varyable to save some result of searching, Search_Bt_Click(), 
            Search_Cust_Lst = new List<EF_Model.Customer>();


            //Tools Tab

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Upd_CustDV();
            Upd_ToolDV();
        }
    }
}
