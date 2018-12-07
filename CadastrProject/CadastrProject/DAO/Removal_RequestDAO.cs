using CadastrProject.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CadastrProject.DAO
{
    public class Removal_RequestDAO :DAO
    {
        //создаем экземпляр класса сущностей
        private CadastrBDEntities _entities = new CadastrBDEntities();

        public List<Removal_Request> GetAllRequest()
        {
            Connect();
            List<Removal_Request> requestList = new List<Removal_Request>();
            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Removal_Request", Connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Removal_Request request = new Removal_Request();
                    request.Id = Convert.ToInt32(reader["Id"]);
                    request.IDCadastre = Convert.ToInt32(reader["IDCadastre"]);
                    request.IDOwner = Convert.ToInt32(reader["IDOwner"]);
                    request.IDStatus = Convert.ToInt32(reader["IDStatus"]);
                    request.Cause = Convert.ToString(reader["Cause"]);
                    requestList.Add(request);
                }
                reader.Close();
            }
            catch (Exception) { }
            finally { Disconnect(); }
            return requestList;
        }
        
        public bool AddRequest(Removal_Request request)
        {
            bool result = true;
            Connect();
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO  Removal_Request (IDCadastre, IDOwner, IDStatus, Cause) " +
                    "VALUES (@IDCadastre, @IDOwner, @IDStatus, @Cause)", Connection);
                cmd.Parameters.AddWithValue("@IDCadastre", request.IDCadastre);
                cmd.Parameters.AddWithValue("@IDOwner", request.IDOwner);
                cmd.Parameters.AddWithValue("@IDStatus", request.IDStatus);
                cmd.Parameters.AddWithValue("@Cause", request.Cause);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                result = false;
            }
            finally { Disconnect(); }
            return result;
        }

        public void EditRequest(Removal_Request request)
        {
            try
            {
                Connect();
                string str = "UPDATE Removal_Request SET IDCadastre = '" + request.IDCadastre
                    + "', IDOwner = '" + request.IDOwner
                    + "', IDStatu = '" + request.IDStatus
                    + "', Cause = '" + request.Cause
                    + "'WHERE Id = " + request.Id;
                SqlCommand com = new SqlCommand(str, Connection);
                com.ExecuteNonQuery();
            }
            finally
            {
                Disconnect();
            }
        }
        /*
public Removal_Request getRequest(int id)
{
   return (from c in _entities.Removal_Request.Include("Group")
           where c.Id == id
           select c).FirstOrDefault();
}

   public bool DeleteRequest(int Id)
{
   Removal_Request originalCadastrs = getRequest(Id);
   try
   {
       //Удаляем запись из таблицы
       _entities.Removal_Request.Remove(originalCadastrs);
       _entities.SaveChanges();
   }
   catch
   {
       return false;
   }
   return true;
}*/
    }
}