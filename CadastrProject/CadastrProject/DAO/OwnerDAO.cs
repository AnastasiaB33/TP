using CadastrProject.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace CadastrProject.DAO
{
    public class OwnerDAO : DAO
    {
        //создаем экземпляр класса сущностей
        private CadastrBDEntities _entities = new CadastrBDEntities();
        public IEnumerable<Owner> GetAllOwner()
        {
            return _entities.Owner.Select(n => n);
        }
       /* public Owner GetAcc(string id)
        {
            return _entities.Owner.Where(n => n.IDUser == id).First();
        }*/
       /* public IEnumerable<Owner> GetOwner(int id)
        {
             return _entities.Owner.Where(n => n.Id == id);
        }*/
       /* public Owner GetOwner(int id)
        {
            return _entities.Owner.Where(n => n.Id == id).First();
        }*/

        public List<Owner> GetAllOwners()
        {
            Connect();
            List<Owner> ownerList = new List<Owner>();
            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Owner", Connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Owner owner = new Owner();
                    owner.Id = Convert.ToInt32(reader["Id"]);
                    owner.Name = Convert.ToString(reader["Name"]);
                    owner.Surname = Convert.ToString(reader["Surname"]);
                    owner.Inn = Convert.ToString(reader["Inn"]);
                    owner.Passport = Convert.ToString(reader["Passport"]);
                    owner.Phone = Convert.ToInt32(reader["Phone"]);
                    owner.Mail = Convert.ToString(reader["Mail"]);
                    ownerList.Add(owner);
                }
                reader.Close();
            }
            catch (Exception) { }
            finally { Disconnect(); }
            return ownerList;
        }
      
      /*  public Cadastre getOwners(int id)
        {
            return (from c in _entities.Cadastre.Include("Group")
                    where c.Id == id
                    select c).FirstOrDefault();
        }*/

        public bool AddOwner (Owner owner)
        {
            bool result = true;
            Connect();
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO  Owner (Name, Surname, Inn, Passport, Phone, Mail) " +
                    "VALUES (@Name, @Surname, @Inn, @Passport, @Phone, @Mail)", Connection);
                cmd.Parameters.AddWithValue("@Name", owner.Name);
                cmd.Parameters.AddWithValue("@Surname", owner.Surname);
                cmd.Parameters.AddWithValue("@Inn", owner.Inn);
                cmd.Parameters.AddWithValue("@Passport", owner.Passport);
                cmd.Parameters.AddWithValue("@Phone", owner.Phone);
                cmd.Parameters.AddWithValue("@Mail", owner.Mail);
                cmd.ExecuteNonQuery();
            }
            catch (Exception) { result = false;
            }
            finally { Disconnect(); }
            return result;
        }

        public void EditOwner(Owner owner)
        {
            try
            {
                Connect();
                string str = "UPDATE Owner SET Name = '" + owner.Name
                    + "', Surname = '" + owner.Surname
                    + "', Inn = '" + owner.Inn
                    + "', Passport = '" + owner.Passport
                    + "', Phone = '" + owner.Phone
                    + "', Mail = '" + owner.Mail
                    + "'WHERE Id = " + owner.Id;
                SqlCommand com = new SqlCommand(str, Connection);
                com.ExecuteNonQuery();
            }
            finally
            {
                Disconnect();
            }
        }

    }
}