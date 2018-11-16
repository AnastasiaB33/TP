using CadastrProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace CadastrProject.DAO
{
    public class GroupDAO
    {
        private CadastrBDEntities1 _entities = new CadastrBDEntities1();
        public IEnumerable<Models.Group> GetAllGroups()
        {
            return (from c in _entities.Group select c);
        }
    }
} 