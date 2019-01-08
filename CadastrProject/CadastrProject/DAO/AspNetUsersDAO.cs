using CadastrProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CadastrProject.DAO
{
    public class AspNetUsersDAO:DAO
    { 
       // private static readonly ApplicationDbContext _appContext = new ApplicationDbContext();
        private static CadastrBDEntities1 _entities = new CadastrBDEntities1();

        public AspNetUsers GetUserById(string id)
        {
            return (from c in _entities.AspNetUsers.Where(u => u.Id == id) select c).FirstOrDefault();
        }            
        public bool UpdateUser(AspNetUsers Records)
        {
            AspNetUsers user = GetUserById(Records.Id);
            try
            {
                //редактирование записи в таблице
                user.Surname = Records.Surname;
                user.Name = Records.Name;
                user.Passport = Records.Passport;
                user.Inn = Records.Inn;
                _entities.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
        /*
         //не работает обновление
         public bool UpdateUser(AspNetUsers Records)
         {
             try
             {
                 var Entity = _entities.AspNetUsers.FirstOrDefault(x => x.Id == Records.Id);
                 Entity.Name = Records.Name;
                 Entity.Surname = Records.Surname;
                 Entity.Passport = Records.Passport;
                 Entity.Inn = Records.Inn;
                 _entities.SaveChanges();
             }
             catch
             {
                 return false;
             }
             return true;
         }
        /*
        public AspNetUsers getUsers(string id)
        {
            return (from c in _entities.AspNetUsers.Include("Group")
                    where c.Id == id
                    select c).FirstOrDefault();
        }
        
                public static IEnumerable<AspNetUsers> GetClients()
                {
                    return _appContext.AspNetUsers.Select(s => s);
                }

                public static AspNetUsers GetClient(/*int string id)
                {
                    return _appContext.AspNetUsers.First(s => s.Id == id);
                }

                public static AspNetUsers GetAcc(string id)
                {
                    return _appContext.AspNetUsers.First(n => n.Id == id);
                }
                /*
                public static bool AddClient(AspNetUsers client)
                {
                    try
                    {
                        _appContext.AspNetUsers.Add(client);
                        _appContext.SaveChanges();
                        //Logger.For(client).Info("Запись успешно добавлена: " + client);

                    }
                    catch (Exception ex)
                    {
                       // Logger.For(client).Error("Ошибка: ", ex);
                        return false;
                    }
                    return true;
                }

                public static bool UpdateClient(AspNetUsers client)
                {
                    try
                    {
                        _appContext.AspNetUsers.First(s => s.Id == client.Id);
                        _appContext.SaveChanges();
                       // Logger.For(client).Info("Запись успешно изменена: " + client);

                    }
                    catch (Exception ex)
                    {
                        //Logger.For(client).Error("Ошибка: ", ex);
                        return false;
                    }

                    return true;
                }*/
    }
}

 