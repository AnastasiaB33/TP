using CadastrProject.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CadastrProject.DAO
{
    public class CadastreDAO:DAO
    {
        //создаем экземпляр класса сущностей
        private CadastrBDEntities1 _entities = new CadastrBDEntities1();

        public IEnumerable<Cadastre> GetAllCadastrs()
        {
            //Простая выборка через класс сущностей
            return (from c in _entities.Cadastre.Include("Group") select c);
        }
        public IEnumerable<Cadastre> GetAllStatus()
        {
            //Простая выборка через класс сущностей
            return (from c in _entities.Cadastre.Include("Status") select c);
        }

        public Group GetGroup(int? id)
        {
            if (id != null) //возращает запись по её Id
                return (from c in _entities.Group
                        where c.Id == id
                        select c).FirstOrDefault();
            else // возвращает первую запись в таблице
                return (from c in _entities.Group
                        select c).FirstOrDefault();
        }
        public Status GetStatus(int? id)
        {
            if (id != null) //возращает запись по её Id
                return (from c in _entities.Status
                        where c.Id == id
                        select c).FirstOrDefault();
            else // возвращает первую запись в таблице
                return (from c in _entities.Status
                        select c).FirstOrDefault();
        }
        public Cadastre getCadastrs(int id)
        {
            return (from c in _entities.Cadastre.Include("Group")
                    where c.Id == id
                    select c).FirstOrDefault();
        }

        public Cadastre getStatus(int id)
        {
            return (from c in _entities.Cadastre.Include("Status")
                    where c.Id == id
                    select c).FirstOrDefault();
        }

        public bool addCadastrs(int IDGroup, Cadastre Cadastrs)
        {
            try
            {
                Cadastrs.Group = GetGroup(IDGroup);
                //Добавление записи в таблицу Supply
                _entities.Cadastre.Add(Cadastrs);
                _entities.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool UpdateStatus(Cadastre Records)
        {
            try
            {
                var Entity = _entities.Cadastre.FirstOrDefault(x => x.Id == Records.Id);
                Entity.IDStatus = Records.IDStatus;
                Entity.Date_registration = Records.Date_registration;
                _entities.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        /* public bool updateCadastrs(int GroupId, Cadastre Cadastrs)
         {
             Cadastre originalCadastrs = getCadastrs(Cadastrs.Id);
             originalCadastrs.Group = GetGroup(GroupId);
             try
             {
                 //редактирование записи в таблице
                 originalCadastrs.Address = Cadastrs.Address;
                 originalCadastrs.Value = Cadastrs.Value;
                 originalCadastrs.Square = Cadastrs.Square;
                 originalCadastrs.Date_application = Cadastrs.Date_application;
                 originalCadastrs.IDOwner = Cadastrs.IDOwner;
                 originalCadastrs.IDStatus = Cadastrs.IDStatus;
                 originalCadastrs.Date_registration = Cadastrs.Date_registration;
                 _entities.SaveChanges();
             }
             catch
             {
                 return false;
             }
             return true;
         }*/

        /*
        public bool deleteCadastrs(int Id)
        {
            Cadastre originalCadastrs = getCadastrs(Id);
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

