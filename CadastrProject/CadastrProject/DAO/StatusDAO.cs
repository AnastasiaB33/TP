using CadastrProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CadastrProject.DAO
{
    public class StatusDAO:DAO
    {
        private CadastrBDEntities _entities = new CadastrBDEntities();
        public IEnumerable<Models.Status> GetAllStatus()
        {
            return (from c in _entities.Status select c);
        }
    }
}