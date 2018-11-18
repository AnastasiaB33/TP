using CadastrProject.DAO;
using CadastrProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CadastrProject.Controllers
{
    public class HomeController : Controller
    {
        GroupDAO groupDAO = new GroupDAO();
        CadastreDAO recordsDAO = new CadastreDAO();
        OwnerDAO ownerDAO = new OwnerDAO();
        //GET:/Home
        public ActionResult Index(int? id)
        {
            ViewData["Group"] = groupDAO.GetAllGroup();
            var records = id == null ? recordsDAO.GetAllCadastrs() : recordsDAO.GetAllCadastrs().Where(x => x.Group.Id == id);
            return View(records);
        }

        public ActionResult CadastreObject(int? id)
        {
            ViewData["Group"] = groupDAO.GetAllGroup();
            var records = id == null ? recordsDAO.GetAllCadastrs() : recordsDAO.GetAllCadastrs().Where(x => x.Group.Id == id);
            return View(records);
        }
        public ActionResult OwnerViews(int? id)
        {
               
            return View(ownerDAO.GetAllOwners());
        }

        //Get:/Home/Details
        public ActionResult Details(int id)
        {
            return View(recordsDAO.getCadastrs(id));
        }
        protected bool ViewDataSelectList(int IDGroup)
        {
            var groups = groupDAO.GetAllGroup();
            ViewData["IDGroup"] = new SelectList(groups, "Id", "Type", IDGroup);
            return groups.Count() > 0;
        }

        //GET:/Home/Create
        public ActionResult Create()
        {
            if (!ViewDataSelectList(-1))
                return RedirectToAction("Index");
            return View("Create");
        }

        //POST:/Home/Create
        [HttpPost]
        public ActionResult Create(int IDGroup, [Bind(Exclude = "Id")] Cadastre Cadastrs)
        {
            ViewDataSelectList(IDGroup);

            try
            {
                if (ModelState.IsValid && recordsDAO.addCadastrs(IDGroup, Cadastrs))
                    return RedirectToAction("Index");
                else
                    return View(Cadastrs);
            }
            catch
            {
                return View(Cadastrs);
            }
        }

        //GET:/Home/Edit
        public ActionResult Edit(int id)
        {
            Cadastre Records = recordsDAO.getCadastrs(id);
            if (!ViewDataSelectList(Records.Group.Id))
                return RedirectToAction("Index");
            return View(recordsDAO.getCadastrs(id));
        }

        //POST:/Home/Edit
        [HttpPost]
        public ActionResult Edit(int IDGroup, Cadastre Cadastrs)
        {
            ViewDataSelectList(-1);
            try
            {
                if (ModelState.IsValid && recordsDAO.updateCadastrs(IDGroup, Cadastrs))
                    return RedirectToAction("Index");
                else
                    return View(Cadastrs);
            }
            catch
            {
                return View(Cadastrs);
            }
        }

        //GET:/Home/Delete
        public ActionResult Delete(int id)
        {
            return View(recordsDAO.getCadastrs(id));
        }

        //POST:/Home/Delete
        [HttpPost]
        public ActionResult Delete(int id, Cadastre Cadastrss)
        {
            try
            {
                if (recordsDAO.deleteCadastrs(id))
                    return RedirectToAction("Index");
                else
                    return View(recordsDAO.getCadastrs(id));
            }
            catch
            {
                return View(recordsDAO.getCadastrs(id));
            }
        }
    }
}