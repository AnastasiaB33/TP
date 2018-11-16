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

    //GET:/Home
    public ActionResult Index(int? id)
    {
        ViewData["Groups"] = groupDAO.GetAllGroups();
        var records = id == null ? recordsDAO.GetAllCadastrs() : recordsDAO.GetAllCadastrs().Where(x => x.Group.Id == id);
        return View(records);
    }

    //Get:/Home/Details
    public ActionResult Details(int id)
    {
        return View(recordsDAO.getCadastrs(id));
    }
    protected bool ViewDataSelectList(int GroupId)
    {
        var groups = groupDAO.GetAllGroups();
        ViewData["GroupId"] = new SelectList(groups, "Id", "Type", GroupId);
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
    public ActionResult Create(int GroupId, [Bind(Exclude = "Id, AddDate")] Cadastre Cadastrs)
    {
        ViewDataSelectList(GroupId);

        try
        {
            if (ModelState.IsValid && recordsDAO.addCadastrs(GroupId, Cadastrs))
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
    public ActionResult Edit(int GroupId, Cadastre Cadastrs)
    {
        ViewDataSelectList(-1);
        try
        {
            if (ModelState.IsValid && recordsDAO.updateCadastrs(GroupId, Cadastrs))
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
    public ActionResult Delete(int id,Cadastre Cadastrss)
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

