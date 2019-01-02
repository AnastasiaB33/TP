using CadastrProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CadastrProject.DAO
{
    public class AspNetUsersDAO:DAO
    { /*
        private static readonly ApplicationDbContext _appContext = new ApplicationDbContext();

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

 