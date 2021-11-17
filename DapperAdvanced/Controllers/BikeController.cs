using DapperAdvanced.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DapperAdvanced.Controllers
{
    public class BikeController : Controller
    {
        // GET: Bike
        private BikesRepository repository;

        public BikeController()
        {
            repository = new BikesRepository();
        }
        public ActionResult Index(RequestModel request)
        {
            if (request.OrderBy == null)
            {
                request = new RequestModel
                {
                    Search = request.Search,
                    OrderBy = "name",
                    IsDescending = false
                };
            }
            ViewBag.Request = request;
            return View(repository.GetAll(request));
        }

        // GET: Bike/Details/5
        public ActionResult Details(int id)
        {
            return View(repository.Get(id));
        }

        // GET: Bike/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bike/Create
        [HttpPost]
        public ActionResult Create(Bike bike, bool editAfterSaving = false)
        {
            if (ModelState.IsValid)
            {
                var lastInsertedId = repository.Create(bike);
                if (lastInsertedId > 0)
                {
                    TempData["Message"] = "Record added successfully";
                }
                else
                {
                    TempData["Error"] = "Failed to add record";
                }
                if (editAfterSaving)
                {
                    return RedirectToAction("Edit", new { Id = lastInsertedId });
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        // GET: Bike/Edit/5
        public ActionResult Edit(int id)
        {
            return View(repository.Get(id));

        }

        // POST: Bike/Edit/5
        [HttpPost]
        public ActionResult Edit(Bike bike)
        {
            if (ModelState.IsValid)
            {
                var recordAffected = repository.Update(bike);
                if (recordAffected > 0)
                {
                    TempData["Message"] = "Record updated successfully";
                }
                else
                {
                    TempData["Error"] = "Failed to update record";
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Bike/Delete/5
        public ActionResult Delete(int id)
        {
            return View(repository.Get(id));
        }

        // POST: Bike/Delete/5
        [HttpPost]
        public ActionResult Delete(Bike bike)
        {
            var recordAffected = repository.Delete(bike.Id);
            if (recordAffected > 0)
            {
                TempData["Message"] = "Record deleted successfully";
            }
            else
            {
                TempData["Error"] = "Failed to delete record";
            }
            return RedirectToAction("Index");
        }
    }
}
