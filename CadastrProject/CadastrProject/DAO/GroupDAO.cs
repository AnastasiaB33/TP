using CadastrProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace CadastrProject.DAO
{
    public class GroupDAO : DAO
    {
        private CadastrBDEntities1 _entities = new CadastrBDEntities1();
        public IEnumerable<Models.Group> GetAllGroup()
        {
            //return (from c in _entities.Group select c);
            return _entities.Group.Select(s => s);
        }

       
    }
}