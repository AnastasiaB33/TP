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
    public class RequestController : Controller
    {
        Removal_RequestDAO requestDAO = new Removal_RequestDAO();
        GroupDAO groupDAO = new GroupDAO();
        StatusDAO statusDAO = new StatusDAO();
        AspNetUsersDAO userDAO = new AspNetUsersDAO();

        public ActionResult RequestViews(int? id)
        {
            ViewData["Group"] = groupDAO.GetAllGroup();
            var records = id == null ? requestDAO.GetAllRequest() : requestDAO.GetAllRequest().Where(x => x.Group.Id == id);
            return View(records);
        }
        public ActionResult MyRequest()
        {
            string userid = User.Identity.GetUserId();
            var myrequest = requestDAO.GetMyRequest(userid);
            return View(myrequest);
        }
        //GET:/Request/Create
        [Authorize]
        public ActionResult Create(string id)
        {
            var group = new SelectList(groupDAO.GetAllGroup(), "Id", "Type");
            ViewData["IDGroup"] = group;
            string userId = User.Identity.GetUserId();
            Removal_Request request = new Removal_Request();
            request.IDUser = userId;
            return View("Create", request);
        }

        //POST:/Request/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create(Removal_Request model)
        {
            var group = new SelectList(groupDAO.GetAllGroup(), "Id", "Type");
            ViewData["IDGroup"] = group;
            string userId = User.Identity.GetUserId();
            model.IDUser = userId;
            try
            {
                if (requestDAO.AddRequest(model))
                {
                    return RedirectToAction("Ok");
                }

                return View("RequestViews");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        //Get:/Request/Details
        public ActionResult Details(int id)
        {
            return View(requestDAO.getRequest(id));
        }
        //редактирование статуса и даты регистрации, только админ
        //GET:/Request/Edit
        //[Authorize(Roles = "Admin")]
        public ActionResult EditStatus(int id)
        {
            Removal_Request Records = requestDAO.getRequest(id);
            if (Records != null)
            {
                SelectList status = new SelectList(statusDAO.GetAllStatus(), "id", "name", id);
                ViewBag.Status = status;
                return View(Records);
            }
            return View(requestDAO.getRequest(id));
        }
        //POST:/Request/Edit
        [HttpPost]
        // [Authorize(Roles = "Admin")]
        public ActionResult EditStatus(int IDStatus, Removal_Request Records)
        {
            ViewDataSelectList(-1);
            try
            {
                if (ModelState.IsValid && requestDAO.UpdateStatus(Records))
                    return RedirectToAction("Ok");
                else
                    return View("Error");
            }
            catch
            {
                return View("../Home/Index");
            }
        }
        protected bool ViewDataSelectList(int IDGroup)
        {
            var groups = groupDAO.GetAllGroup();
            ViewData["IDGroup"] = new SelectList(groups, "Id", "Type", IDGroup);
            return groups.Count() > 0;
        }
        [Authorize]
        public ActionResult Sent(int id)
        {
            Removal_Request request = requestDAO.getRequest(id);
            request.IDStatus = 2;
            requestDAO.UpdateStatus(request);
            return RedirectToAction("../Request/MyRequest");
        }

        //редактирование заявок
        //GET:/Request/Edit
        //[Authorize]
        public ActionResult EditRequest(int id)
        {
            Removal_Request Records = requestDAO.getRequest(id);
            if (Records != null)
            {
                SelectList group = new SelectList(groupDAO.GetAllGroup(), "id", "type", id);
                ViewBag.Status = group;
                return View(Records);
            }
            return View(requestDAO.getRequest(id));
        }
        //POST:/Request/Edit
        [HttpPost]
        //[Authorize]
        public ActionResult EditRequest(int IDGroup, Removal_Request Records)
        {
            ViewDataSelectList(-1);
            try
            {
                if (ModelState.IsValid && requestDAO.UpdateRequest(Records))
                    return RedirectToAction("../Home/Ok");
                else
                    return View("Error");
            }
            catch
            {
                return View("../Home/Index");
            }
        }
        /*
        // GET: Request
        public ActionResult Index()
        {
            return View(requestDAO.GetAllRequest());
        }
        // GET: Request/Details/5
        public ActionResult Details(int id)
        {
            request = requestDAO.GetAllRequest();
            int trueid = 0;
            for (int i = 0; i < request.Count; i++) if (request[i].Id == id) trueid = i;
            return View(request[trueid]);

        }

        //GET:/Home/Create
        [Authorize]
        public ActionResult Create(string id)
        {
            var group = new SelectList(requestDAO.GetAllRequest(), "Id", "Type");
            ViewData["IDGroup"] = group;
            string userId = User.Identity.GetUserId();
            Removal_Request request = new Removal_Request();
            request.IDUser = userId;
            return View("Create", request);
        }

        //POST:/Home/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create(Removal_Request model)
        {
            var group = new SelectList(requestDAO.GetAllRequest(), "Id", "Type");
            ViewData["IDGroup"] = group;
            string userId = User.Identity.GetUserId();
            model.IDUser = userId;
            try
            {
                if (requestDAO.addRequest(model))
                {
                    return RedirectToAction("Ok");
                }

                return View("RequestView");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        
        // GET: Request/Edit/5
        public ActionResult Edit(int id)
        {
            request = requestDAO.GetAllRequest();
            int trueid = 0;
            for (int i = 0; i < request.Count; i++) if (request[i].Id == id) trueid = i;
            return View(request[trueid]);
        }

        // POST: Request/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Removal_Request request)
        {
            if (ModelState.IsValid)
            {
                requestDAO.EditRequest(request);
                return View("Ok");
            }
            else
            {
                return View("Error");
            }
        }   */

    }
}
