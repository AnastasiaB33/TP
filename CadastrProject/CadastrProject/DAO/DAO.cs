using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CadastrProject.DAO
{
    public class DAO
    {
        private static string connectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename='C:\Users\barsu\OneDrive\Рабочий стол\Учёба\3 курс\ТП Вершинин В.В\Курсач\TP\CadastrProject\CadastrProject\App_Data\aspnet-CadastrProject-20181111053737.mdf';Initial Catalog=aspnet-CadastrProject-20181111053737;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
        public SqlConnection Connection { get; set; }
        public void Connect()
        {
            Connection = new SqlConnection(connectionString);
            Connection.Open();
        }
        public void Disconnect()
        {
            Connection.Close();
        }
    }
}