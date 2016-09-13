using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace STDB
{
    public static class AddTools
    {
        public enum SearchedFields
        {
            All = -2, None, Name, NameDate, NameAddress, Address, AddressDate, Date
        }
    }
}
