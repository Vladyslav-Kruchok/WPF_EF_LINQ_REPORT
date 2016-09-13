using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STDB.EF_Model
{
    public partial class STDB_Entities
    {
        //my conn str to entities
        public STDB_Entities(string conn_to_entity):base(conn_to_entity)
        { }
    }
}
