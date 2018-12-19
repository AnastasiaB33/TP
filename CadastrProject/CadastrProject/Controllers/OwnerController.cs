using CadastrProject.DAO;
using CadastrProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CadastrProject.Controllers
{
    public class OwnerController : Controller
    {
        OwnerDAO ownerDAO = new OwnerDAO();
        List<Owner> owner;
       
        // GET: Owner
        public ActionResult Index()
        {
            return View(ownerDAO.GetAllOwner());
        }

        // GET: Owner/Details/5
        public ActionResult Details(int id)
        {
            owner = ownerDAO.GetAllOwners();
            int trueid = 0;
            for (int i = 0; i < owner.Count; i++) if (owner[i].Id == id) trueid = i;
            return View(owner[trueid]);
        }

        // GET: Owner/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Owner/Create
        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")]Owner owner)
        {
            try
            {
                if (ownerDAO.AddOwner(owner))
                {
                    return RedirectToAction("Ok");
                }
                else
                {

                    return View("Error");
                }
            }
            catch
            {
                return View("Create");
            }

        }

        // GET: Owner/Edit/5
        public ActionResult Edit(int id)
        {
            owner = ownerDAO.GetAllOwners();
            int trueid = 0;
            for (int i = 0; i < owner.Count; i++) if (owner[i].Id == id) trueid = i;
            return View(owner[trueid]);
        }

        // POST: Owner/Edit/5
        [HttpPost]
        public ActionResult Edit(Owner owner)
        {
            if (ModelState.IsValid)
            {
                ownerDAO.EditOwner(owner);
                return View("Ok");
            }
            else
            {
                return View("Error");
            }

        }

        public ActionResult OwnerViews(int? id)
        {

            return View(ownerDAO.GetAllOwners());
        }

        public ActionResult Ok()
        {

            return View("Ok");
        }
        /*
         // GET: Owner/Delete/5
         public ActionResult Delete(int id)
         {
             try
             {
                 owner = ownersDAO.GetAllOwners();
                 int trueid = 0;
                 for (int i = 0; i < owner.Count; i++) if (owner[i].Id == id) trueid = i;
                 return View(owner[trueid]);
             }
             catch
             {
                 return View("Ошибка");
             }
         }

         // POST: Owner/Delete/5
         [HttpPost]
         public ActionResult Delete(int id, FormCollection collection)
         {
             try
             {
                 int rec_id = Convert.ToInt32(id);
                 ownersDAO.DeleteOwner(rec_id);
                 return View("OK");
             }
             catch
             {
                 return View("Ошибка");
             }
         }*/

    }
}
