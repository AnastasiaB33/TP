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
    public class ClientController : Controller
    { /*
        // GET: Client
        public ActionResult ClientView()
        {
            return View();
        }

        [HttpGet]
       // [Authorize(Roles = "Client")]
        public ActionResult CreateClient(string id)
        {
            AspNetUsers client = new AspNetUsers {Id = id };
            return View(client);
        }

        [HttpPost]
       // [Authorize(Roles = "Client")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateClient(AspNetUsers client)
        {
            string id = User.Identity.GetUserId();
            client.Id = id;

            if (AspNetUsersDAO.AddClient(client))
            {
                return RedirectToAction("Index", "Home");
            }

            return View("CreateClient");
        }

        [HttpGet]
      //  [Authorize(Roles = "Client")]
        public ActionResult EditClient(string id)
        {
            AspNetUsers client = AspNetUsersDAO.GetClient(id);

            if (client != null)
            {
                return View(client);
            }
            return RedirectToAction("Index", "Manage");
        }

        [HttpPost]
       // [Authorize(Roles = "Client")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditClient(string id, AspNetUsers client)
        {
            if (client != null && ModelState.IsValid)
            {
                AspNetUsersDAO.UpdateClient(client);
                return RedirectToAction("Index", "Manage");
            }
            return View("EditClient", AspNetUsersDAO.GetClient(id));
        }*/
    }
}
