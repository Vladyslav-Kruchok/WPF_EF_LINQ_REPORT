using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace STDB.EF_Model
{
    public partial class Customer
    {
        public Customer(string f_name = "", string f_address = "", string f_date = "") 
        {
            if(f_name != "")
            {
                Name = f_name;
            }
            if (f_address != "")
            {
                Address_customer = f_address;
            }
            if (f_date != "" && IsRight_Date(f_date))
            {
                Date_in = DateTime.Parse(f_date);
            }
            else
            {
                Date_in = DateTime.Today;
            }
        }
        private bool IsRight_Date(string f_date = "")
        {
            DateTime date_from_tb = DateTime.Parse(f_date);
            if (date_from_tb < new DateTime(1900, 01, 01) | date_from_tb > DateTime.Now)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
