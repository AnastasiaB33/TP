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
        List<Removal_Request> request;

        public ActionResult RequestViews()
        {

            return View(requestDAO.GetAllRequest());
        }

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
        }   
    }
}
