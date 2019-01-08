using CadastrProject.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace CadastrProject.DAO
{
    public class CadastreDAO:DAO
    {
        private static readonly ApplicationDbContext _appContext = new ApplicationDbContext();
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

        public List<Cadastre> GetMyObject (string id)
        {
            List<Cadastre> myobject = new List<Cadastre>();
            try
            {
              //  using (var ctx = new CadastrBDEntities1())
                {
                    myobject = _entities.Cadastre
                        .Include("Group")
                        .Include("Status")
                        .Where(c => c.IDUser==id).ToList();
                    //string query = "SELECT * FROM Cadastre WHERE IDUser=@P0";
                    //myobject.AddRange(_entities.Database.SqlQuery<Cadastre>(query, id).ToList());
                }
            }
            catch (Exception)
            { }
            return myobject;
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
        
        public bool addCadastrs( Cadastre cadastre)
        {
            try
            {
               //  cadastre.Group = GetGroup(IDGroup);
                _entities.Cadastre.Add(cadastre);
                _entities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
           
        }
        /* не работало
        public void addCadastrs(Cadastre model)
        {
            try
            {
                
                    string query = "INSERT INTO Cadastre (Address, Value, Square, Date_application, IDUser, IDGroup, IDStatus) VALUES (@P0, @P1, @P2, @P3, @P4, @P5, @P6)";
                    List<object> parametrList = new List<object>
                  {
                    model.Address,
                    model.Value,
                    model.Square,
                    model.Date_application,
                    model.IDUser,
                    model.IDGroup,
                    model.IDStatus
                  };
                    object[] parametrs = parametrList.ToArray();
                    int result = _entities.Database.ExecuteSqlCommand(query, parametrs);
                
            }
            catch (Exception ex) { }

        }*/
        
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
   
        /*21.12.2018
            public static IEnumerable<AspNetUsers> GetCadastreForClient(string id)
        {
            return _appContext.AspNetUsers.Where(s => s.Id == id);
        }

*/

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
        }

        public bool UpdateObject(Cadastre records)
        {
            try
            {
                var Entity = _entities.Cadastre.FirstOrDefault(x => x.Id == records.Id);
                Entity.Address = records.Address;
                Entity.Value = records.Value;
                Entity.Square = records.Square;
                Entity.IDGroup = records.IDGroup;
                Entity.Date_application = records.Date_application;
                _entities.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}

