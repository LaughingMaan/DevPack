﻿using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace LcDevPack_TeamDamonA.Tools
{
    public class NpcList
    {
        public static System.Collections.Generic.List<tNpc> List = new System.Collections.Generic.List<tNpc>();
        public static string LoadFromDatabaseSQL = "SELECT a_index, a_name_ger, a_name_usa, a_name_frc, a_name_pld, a_name_brz, a_name_rus, a_name_mex, a_name_spn, a_name_thai, a_name_ita, a_file_smc  FROM t_npc ORDER BY a_index";
        public static Connection connection = new Connection();
        public static string Host = NpcList.connection.ReadSettings("Host");
        public static string User = NpcList.connection.ReadSettings("User");
        public static string Password = NpcList.connection.ReadSettings("Password");
        public static string Database = NpcList.connection.ReadSettings("Database");
        public static string language = NpcList.connection.ReadSettings("Language");//dethunter12 language selection
        public static MySqlConnection mysqlCon;
        public static string ConnectionString;
        public static string namee; //dethunter12 stringfrom lang modify
        public static string StringFromLanguage() //dethunter12 5/10/2018
        {

            if (language == "GER")
            {
                return "a_name_ger";
            }
            else if (language == "POL")
            {
                return "a_name_pld";
            }
            else if (language == "BRA")
            {
                return "a_name_brz";
            }
            else if (language == "RUS")
            {
                return "a_name_rus";
            }
            else if (language == "FRA")
            {
                return "a_name_frc";
            }
            else if (language == "ESP")
            {
                return "a_name_spn";
            }
            else if (language == "MEX")
            {
                return "a_name_mex";
            }
            else if (language == "THA")
            {
                return "a_name_thai";
            }
            else if (language == "ITA")
            {
                return "a_name_ita";
            }
            else if (language == "USA")
            {
                return "a_name_usa";
            }
            else
            {
                return null;
            }
        }

        public static bool SetConnection()
        {
            NpcList.ConnectionString = string.Format("Data Source={0};Database={1};User ID={2};Password={3};", NpcList.Host, NpcList.Database, NpcList.User, NpcList.Password);
            return true;
        }

        public static DataTable GetFromQuery(string query)
        {
            DataTable dataTable = new DataTable();
            using (NpcList.mysqlCon = new MySqlConnection(NpcList.ConnectionString))
            {
                NpcList.mysqlCon.Open();
                MySqlDataReader mySqlDataReader = MySqlHelper.ExecuteReader(NpcList.mysqlCon, query);
                dataTable.Load((IDataReader)mySqlDataReader);
                NpcList.mysqlCon.Close();
            }
            return dataTable;
        }

        public static void Import()
        {
            namee = StringFromLanguage(); //dethunter12 test
            foreach (DataRow row in (InternalDataCollectionBase)NpcList.GetFromQuery(NpcList.LoadFromDatabaseSQL).Rows)

                NpcList.List.Add(new tNpc()
                {

                    ItemID = Convert.ToInt32(row["a_index"]),
                    Name = Convert.ToString(row["" + namee + ""]), //dethunter12 test
                    SMCPath = Convert.ToString(row["a_file_smc"])
                });
        }
    }
}
