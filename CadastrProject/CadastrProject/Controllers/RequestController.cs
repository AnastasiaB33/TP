using CadastrProject.DAO;
using CadastrProject.Models;
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

        public ActionResult RequestViews(int? id)
        {

            return View(requestDAO.GetAllRequest());
        }

        // GET: Request
        public ActionResult Index()
        {
            return View(requestDAO.GetAllRequest());
        }
        
        // GET: Request/Details/5
        /*public ActionResult Details(int id)
        {
            return View();
        }*/

        // GET: Request/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Request/Create
        [HttpPost]
        public ActionResult Create(Removal_Request request)
        {
            try
            {
                if (requestDAO.AddRequest(request))
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
