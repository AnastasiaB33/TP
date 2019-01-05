using CadastrProject.DAO;
using CadastrProject.Models;
using Microsoft.AspNet.Identity;
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
       // OwnerDAO ownerDAO = new OwnerDAO();
        StatusDAO statusDAO = new StatusDAO();
        
        public ActionResult MyObject()
        {
            string userid = User.Identity.GetUserId();
             var myobject = recordsDAO.GetMyObject(userid);
           // var myobject = userid == null ? recordsDAO.GetMyObject(userid) : recordsDAO.GetMyObject(userid).Where(x => x.AspNetUsers.Id == userid);
            return View(myobject);
        }
        public ActionResult AboutMe()
        {
            return View("AboutMe");
        }
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
        public ActionResult CadastreRequest(int? id)
        {
            ViewData["Group"] = groupDAO.GetAllGroup();
            var records = id == null ? recordsDAO.GetAllCadastrs() : recordsDAO.GetAllCadastrs().Where(x => x.Group.Id == id);
            return View(records);
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
        /*protected bool ViewData_SelectList(int IDStatus)
        {
            var groups = groupDAO.GetAllGroup();
            ViewData["IDStatus"] = new SelectList(groups, "Id", "Name", IDStatus);
            return groups.Count() > 0;
        }*/

        /* работало, но надо прописывать ид 
        //GET:/Home/Create
        [Authorize]
        public ActionResult Create()
        {
            if (!ViewDataSelectList(-1))
                return RedirectToAction("Index");
            return View("Create");
        }
        //POST:/Home/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create(int IDGroup, [Bind(Exclude = "Id")] Cadastre Cadastrs)
        {
            ViewDataSelectList(IDGroup);
        //ViewData_SelectList(IDStatus);
        SelectList status = new SelectList(statusDAO.GetAllStatus(), "id", "name");
        ViewBag.Status = status;
             try
             {
                 if (ModelState.IsValid && recordsDAO.addCadastrs(IDGroup, Cadastrs))
                {
                    return RedirectToAction("Ok");
    }
                 else
                     return View("../Home/Error");
}
             catch
             {
            return View("Index");
            }
        }*/
        
       
        //GET:/Home/Create
        [Authorize]
        public ActionResult Create(string id)
        {
            string userId = User.Identity.GetUserId();
            Cadastre cadastre = new Cadastre();
            cadastre.IDUser = userId;        
            return View(cadastre);
        }
        //POST:/Home/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create(Cadastre model)
        {
             try
             {
                CadastreDAO.addCadastrs(model);
                if (ModelState.IsValid && recordsDAO.addCadastrs(IDGroup, model))
                {
                   }
                 else 
                      return RedirectToAction("Ok");
                      return View("Error");
             }
             catch
             {
            return View("Index");
            }
        }

        //редактирование статуса и даты регистрации, только админ
        //GET:/Home/Edit
        //[Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            Cadastre Records = recordsDAO.getCadastrs(id);
            if (Records != null)
            {
                SelectList status = new SelectList(statusDAO.GetAllStatus(), "id", "name", id);
                ViewBag.Status = status;
                return View(Records);
            }
            /*    if (!ViewDataSelectList(Records.Group.Id))
                    return RedirectToAction("Index");*/
            return View(recordsDAO.getCadastrs(id));
        }
        public ActionResult Ok()
        {

            return View("Ok");
        }
        
        //POST:/Home/Edit
        [HttpPost]
       // [Authorize(Roles = "Admin")]
        public ActionResult Edit(int IDStatus, Cadastre Records)
        {
            ViewDataSelectList(-1);
            try
            {
                if (ModelState.IsValid && recordsDAO.UpdateStatus(Records))
                    return RedirectToAction("Ok");
                else
                    return View("../Home/Error");
            }
            catch
            {
                return View("Index");
            }
        }


        //удаление, только админ
        //GET:/Home/Delete
     //   [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            return View(recordsDAO.getCadastrs(id));
        }

        //POST:/Home/Delete
        [HttpPost]
        [Authorize(Roles = "Admin")]
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
        public ActionResult Contact()
        {
            return View("Contact");
        }
    }
}