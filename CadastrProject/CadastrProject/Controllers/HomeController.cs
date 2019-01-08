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
        StatusDAO statusDAO = new StatusDAO();
        AspNetUsersDAO userDAO = new AspNetUsersDAO();

        public ActionResult MyObject()
        {
            string userid = User.Identity.GetUserId();
             var myobject = recordsDAO.GetMyObject(userid);
            return View(myobject);
        }
        public ActionResult AboutMe()
        {
            AspNetUsersDAO dao = new AspNetUsersDAO();
            var user = dao.GetUserById(User.Identity.GetUserId());
            return View(user);
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
            ViewData["Status"] = statusDAO.GetAllStatus();
            var records = id == null ? recordsDAO.GetAllCadastrs() : recordsDAO.GetAllCadastrs().Where(x => x.Status.Id == id);
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
            /*
        // работало, но надо прописывать ид 
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
            var group = new SelectList(groupDAO.GetAllGroup(), "Id", "Type");
            ViewData["IDGroup"] = group;
            string userId = User.Identity.GetUserId();
            Cadastre cadastre = new Cadastre();
            cadastre.IDUser = userId; 
            return View("Create", cadastre);
        }

        //POST:/Home/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create(Cadastre model)
        {
            var group = new SelectList(groupDAO.GetAllGroup(), "Id", "Type");           
            ViewData["IDGroup"] = group;
            string userId = User.Identity.GetUserId();           
            model.IDUser = userId;
            try
            {
                if (recordsDAO.addCadastrs(model))
                {
                    return RedirectToAction("Ok");
                }

                return View("CadastreObject");
            }
            catch (Exception)
            {
                return View("Error");
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

        //редактирование данных пользователя
        //GET:/Home/Edit
        public ActionResult EditUser()
        {
            AspNetUsers Records = userDAO.GetUserById(User.Identity.GetUserId());
           
            if (Records != null)
            { 
                return View(Records);
            }
            return RedirectToAction("Index"); 
        }

        //редактирование данных пользователя
        //POST:/Home/Edit
        [HttpPost]
        public ActionResult EditUser(string id, AspNetUsers Records)
        {
            if (id !=null && Records != null && ModelState.IsValid)
            {
               userDAO.UpdateUser(Records);
               return RedirectToAction("../Home/AboutMe");
            }
            return View("EditUser", userDAO.GetUserById(id));

        }
        /*
        //POST:/Home/Edit
        [HttpPost]
        public ActionResult EditUser(AspNetUsers Records)
        {
            ViewDataSelectList(-1);
            try
            {
                if (ModelState.IsValid && userDAO.UpdateUser(Records))
                    return RedirectToAction("Ok");
                else
                    return View("Error");
            }
            catch
            {
                return View("Index");
            }
        }*/


        //редактирование заявок
        //GET:/Home/Edit
        //[Authorize]
        public ActionResult EditObject(int id)
        {
            Cadastre Records = recordsDAO.getCadastrs(id);
            if (Records != null)
            {
                SelectList group = new SelectList(groupDAO.GetAllGroup(), "id", "type", id);
                ViewBag.Status = group;
                return View(Records);
            }
            /*    if (!ViewDataSelectList(Records.Group.Id))
                    return RedirectToAction("Index");*/
            return View(recordsDAO.getCadastrs(id));
        }
        //POST:/Home/Edit
        [HttpPost]
        //[Authorize]
        public ActionResult EditObject(int IDGroup, Cadastre Records)
        {
            ViewDataSelectList(-1);
            try
            {
                if (ModelState.IsValid && recordsDAO.UpdateObject(Records))
                    return RedirectToAction("Ok");
                else
                    return View("../Home/Error");
            }
            catch
            {
                return View("Index");
            }
        }

        [Authorize]
        public ActionResult Sent(int id)
        {
            Cadastre Records = recordsDAO.getCadastrs(id);
            Records.IDStatus = 2;
            recordsDAO.UpdateStatus(Records);
            return RedirectToAction("../Home/MyObject");
        }


    }
}