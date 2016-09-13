using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STDB.EF_Model
{
    public partial class Tools
    {
        public Tools(string f_name_tool = "", string f_amount_tool = "")
        {
            if (f_name_tool != "")
            {
                this.Name = f_name_tool;
            }
            if (f_amount_tool != "")
            {
                this.Amount = int.Parse(f_amount_tool);
            }            
        }
    }
}
