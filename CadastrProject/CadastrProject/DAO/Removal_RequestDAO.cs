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
        private CadastrBDEntities1 _entities = new CadastrBDEntities1();

        public IEnumerable<Removal_Request> GetAllRequest()
        {
            return (from c in _entities.Removal_Request.Include("Group") select c);
        }
        public Removal_Request getRequest(int id)
        {
            return (from c in _entities.Removal_Request.Include("Group")
                    where c.Id == id
                    select c).FirstOrDefault();
        }
        public List<Removal_Request> GetMyRequest(string id)
        {
            List<Removal_Request> myrequest = new List<Removal_Request>();
            try
            {
                {
                    myrequest = _entities.Removal_Request
                        .Include("Group")
                        .Include("Status")
                        .Where(c => c.IDUser == id).ToList();                 
                }
            }
            catch (Exception)
            { }
            return myrequest;
        }
              
        public bool UpdateStatus(Removal_Request Records)
        {
            try
            {
                var Entity = _entities.Removal_Request.FirstOrDefault(x => x.Id == Records.Id);
                Entity.IDStatus = Records.IDStatus;
                Entity.DateDelete = Records.DateDelete;
                _entities.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        /*
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
                    request.IDCadastr = Convert.ToInt32(reader["IDCadastr"]);
                    request.Date_application = Convert.ToDateTime(reader["Date_application"]);
                    request.Address = Convert.ToString(reader["Address"]);
                    request.Value = Convert.ToInt32(reader["Value"]);
                    request.Square = Convert.ToInt32(reader["Square"]);
                    request.Date_registration = Convert.ToDateTime(reader["Date_registration"]);
                    request.IDUser = Convert.ToString(reader["IDUser"]);
                    request.DateDelete = Convert.ToDateTime(reader["DateDelete"]);
                    request.Date_request = Convert.ToDateTime(reader["Date_request"]);
                    request.IDStatus = Convert.ToInt32(reader["IDStatus"]);
                    request.Cause = Convert.ToString(reader["Cause"]);
                    requestList.Add(request);
                }
                reader.Close();
            }
            catch (Exception) { }
            finally { Disconnect(); }
            return requestList;
        }*/
        
        public bool AddRequest(Removal_Request request)
        {
            bool result = true;
            Connect();
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO  Removal_Request ( IDUser,IDCadastr, IDGroup,IDStatus, Address,Date_application, Value, Square, Date_registration, Cause,Date_request) " +
                    "VALUES ( @IDUser,@IDCadastr,@IDGroup, @IDStatus,@Address,@Date_application, @Value, @Square, @Date_registration, @Cause,@Date_request)", Connection);
                cmd.Parameters.AddWithValue("@IDUser", request.IDUser);
                cmd.Parameters.AddWithValue("@IDCadastr", request.IDCadastr);
                cmd.Parameters.AddWithValue("@IDStatus", request.IDStatus);
                cmd.Parameters.AddWithValue("@IDGroup", request.IDGroup);
                cmd.Parameters.AddWithValue("@Address", request.Address);
                cmd.Parameters.AddWithValue("@Date_application", request.Date_application);
                cmd.Parameters.AddWithValue("@Value", request.Value);
                cmd.Parameters.AddWithValue("@Square", request.Square);
                cmd.Parameters.AddWithValue("@Date_registration", request.Date_registration);
                cmd.Parameters.AddWithValue("@Cause", request.Cause);
                cmd.Parameters.AddWithValue("@Date_request", request.Date_request);
                cmd.ExecuteNonQuery();
                
            }
            catch (Exception)
            {
                result = false;
            }
            finally { Disconnect(); }
            return result;
        }
        public bool UpdateRequest(Removal_Request records)
        {
            try
            {
                var Entity = _entities.Removal_Request.FirstOrDefault(x => x.Id == records.Id);
                Entity.Address = records.Address;
                Entity.Value = records.Value;
                Entity.Square = records.Square;
                Entity.IDGroup = records.IDGroup;
                Entity.IDCadastr = records.IDCadastr;
                Entity.Date_registration = records.Date_registration;
                Entity.Date_application = records.Date_application;
                Entity.Date_request = records.Date_request;
                Entity.Cause = records.Cause;
                _entities.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
        /*
        public void EditRequest(Removal_Request request)
        {
            try
            {
                Connect();
                string str = "UPDATE Removal_Request SET IDCadastr = '" + request.IDCadastr

                    + "', DateDelete = '" + request.DateDelete
                    + "', IDStatus = '" + request.IDStatus
                    + "', Address = '" + request.Address
                    + "', Value = '" + request.Value
                    + "', Square = '" + request.Square
                    + "', Date_registration = '" + request.Date_registration
                    + "', Cause = '" + request.Cause
                    + "'WHERE Id = " + request.Id;
                SqlCommand com = new SqlCommand(str, Connection);
                com.ExecuteNonQuery();
            }
            finally
            {
                Disconnect();
            }
        }*/
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