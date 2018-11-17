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
        private CadastrBDEntities _entities = new CadastrBDEntities();
        public IEnumerable<Models.Group> GetAllGroup()
        {
            return (from c in _entities.Group select c);
        }
    }
} 