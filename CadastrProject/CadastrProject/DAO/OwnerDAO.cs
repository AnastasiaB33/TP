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
      /*  public Group GetGroup(int id)
        {
            if (id != null) //возращает запись по её Id
                return (from c in _entities.Group
                        where c.Id == id
                        select c).FirstOrDefault();
            else // возвращает первую запись в таблице
                return (from c in _entities.Group
                        select c).FirstOrDefault();
        }
        public Cadastre getOwners(int id)
        {
            return (from c in _entities.Cadastre.Include("Group")
                    where c.Id == id
                    select c).FirstOrDefault();
        }
        public bool addOwners(int GroupId, Owner Owners)
        {
            try
            {
               Owners.Group = GetGroup(GroupId);
                //Добавление записи в таблицу Supply
                _entities.Owner.Add(Owners);
                _entities.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool updateOwners(int GroupId, Owner Owners)
        {
            Cadastre originalCadastrs = getOwners(Owners.Id);
            originalCadastrs.Group = GetGroup(GroupId);
            try
            {
                //редактирование записи в таблице
                originalCadastrs.IDType = Owners.IDType;
                originalCadastrs.Address = Owners.Address;
                originalCadastrs.Value = Owners.Value;
                originalCadastrs.Square = Owners.Square;
                originalCadastrs.Date_application = Owners.Date_application;
                originalCadastrs.IDOwner = Owners.IDOwner;
                originalCadastrs.IDStatus = Owners.IDStatus;
                originalCadastrs.Date_registration = Owners.Date_registration;
                _entities.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool deleteCadastrs(int Id)
        {
            Cadastre originalCadastrs = getOwners(Id);
            try
            {
                //Удаляем запись из таблицы
                _entities.Cadastre.Remove(originalCadastrs);
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