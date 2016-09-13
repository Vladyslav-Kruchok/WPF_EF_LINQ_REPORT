//SqlConnection, provider name
using System.Data.SqlClient;
//EntityConnection
using System.Data.Entity.Core.EntityClient;
//Config file
using System.Configuration;
//assebly
//path

namespace STDB
{
   static  class MCS
    {
        private static SqlConnectionStringBuilder conn_str_bld_sql
        { set; get; }
        private static Configuration config_file
        { set; get; }
        //create cinfig file
        private static void Config_File_Add(string name_conn_str, string @conn_str)
        {
            //form conn str sql
            var setting = new ConnectionStringSettings
            {
                Name = name_conn_str,
                ConnectionString = conn_str
            };
            //provide an access to config file
            config_file = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //add conn str to config file
            config_file.ConnectionStrings.ConnectionStrings.Add(setting);
            //save config
            config_file.Save(ConfigurationSaveMode.Modified);
            //update info
            ConfigurationManager.RefreshSection(config_file.ConnectionStrings.SectionInformation.Name);
        }
        //initialise connection string
        private static void Conn_Str_Bld_Sql()
        {
            conn_str_bld_sql = new SqlConnectionStringBuilder
            {
                DataSource = @".\SQLEXPRESS",/*Server Name*/
                InitialCatalog = "SilgosptechnikaDB",
                IntegratedSecurity = true,
                Pooling = true
            };
            //add conn str SQL to config file and update
            Config_File_Add("conn_str_bld_sql", conn_str_bld_sql.ConnectionString);
        }
        //initialise entity connect
        private static void Conn_Str_Bld_Entity()
        {
            var conn_str_bld_entity = new EntityConnectionStringBuilder
            {
                Provider = "System.Data.SqlClient",
                ProviderConnectionString = conn_str_bld_sql.ToString(),
                Metadata = "res://*/EF_Model.STDB_Model.csdl |res://*/EF_Model.STDB_Model.ssdl |res://*/EF_Model.STDB_Model.msl"
            };
            Config_File_Add("conn_str_bld_entity", conn_str_bld_entity.ConnectionString);
        }
        //зашифровать
        private static void Code(string name_section)
        {
            //get section
            ConnectionStringsSection section = config_file.GetSection(name_section) as ConnectionStringsSection;
            if(!section.SectionInformation.IsProtected)
            {
                // to code section.
                section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
                //save config
                config_file.Save(ConfigurationSaveMode.Modified);
                //update info
                ConfigurationManager.RefreshSection(config_file.ConnectionStrings.SectionInformation.Name);
            }
        }
        public static void CodeEx()
        {
            Code(config_file.ConnectionStrings.SectionInformation.SectionName);
        }
        //расшифровать
        private static void Uncode(string name_section)
        {
            //get section
            ConnectionStringsSection section = config_file.GetSection(name_section) as ConnectionStringsSection;
            if (section.SectionInformation.IsProtected)
            {
                // uncode section
                section.SectionInformation.UnprotectSection();
                //save config
                config_file.Save(ConfigurationSaveMode.Modified);
                //update info
                ConfigurationManager.RefreshSection(config_file.ConnectionStrings.SectionInformation.Name);
            }
        }
        //form configuration file
        private static void Config_File_Bld()
        {
            Conn_Str_Bld_Sql();
            Conn_Str_Bld_Entity();
            conn_str_bld_sql = null;
            config_file = null;
            //Code(config_file.ConnectionStrings.SectionInformation.SectionName);
        }
        static MCS()
        {
            Config_File_Bld();
        }
        //get connection string
        public static string ConnStrSql
        {
            get
            {
                //Uncode(config_file.ConnectionStrings.SectionInformation.SectionName);
                return ConfigurationManager.ConnectionStrings["conn_str_bld_sql"].ConnectionString;
            }
        }
        public static string ConnStrEntity
        {
            get
            {
                //Uncode(config_file.ConnectionStrings.SectionInformation.SectionName);
                return ConfigurationManager.ConnectionStrings["conn_str_bld_entity"].ConnectionString.ToString();
            }
        }
    }
}
