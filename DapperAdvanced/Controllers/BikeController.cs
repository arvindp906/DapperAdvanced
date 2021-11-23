using DapperAdvanced.Models;
using Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using Glimpse.AspNet.Tab;
using NPOI.HSSF.UserModel;

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
                    IsDescending = false,


                };
            }
          
            ViewBag.Request = request;
            return View(repository.GetAll(request));
        }




        public ActionResult Details(int id)
        {
            return View(repository.Get(id));
        }



        // GET: Bike/Details/5


        // GET: Bike/Create
        public ActionResult Create()
        {
            var Biketypes = new List<string>() {  "Normal",  "Stunt", "Sport" };
            ViewBag.Biketypes = Biketypes;
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


            var Biketypes = new List<string>() { "Normal", "Stunt", "Sport" };
            ViewBag.Biketypes = Biketypes;

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



        //public ActionResult ExportToExcel()
        //{

        //    BikesRepository repository = new BikesRepository();
        //    RequestModel request = new RequestModel();
        //    var model = repository.GetAll(request);

        //    HSSFWorkbook templateWorkbook = new HSSFWorkbook();
        //    HSSFSheet sheet = (HSSFSheet)templateWorkbook.CreateSheet("Index");
        //    List<Bike> _Bike = model.ToList();
        //    HSSFRow dataRow = (HSSFRow)sheet.CreateRow(0);
        //    HSSFCellStyle style = (HSSFCellStyle)templateWorkbook.CreateCellStyle();

        //    HSSFFont font = (HSSFFont)templateWorkbook.CreateFont();
        //    font.Color = NPOI.HSSF.Util.HSSFColor.White.Index;
        //    HSSFCell cell;
        //    style.SetFont(font);

        //    cell = (HSSFCell)dataRow.CreateCell(0);
        //    cell.CellStyle = style;
        //    cell.SetCellValue("No.");

        //    cell = (HSSFCell)dataRow.CreateCell(1);
        //    cell.CellStyle = style;
        //    cell.SetCellValue("Name");
        //    cell = (HSSFCell)dataRow.CreateCell(2);
        //    cell.CellStyle = style;
        //    cell.SetCellValue("Type");
        //    cell = (HSSFCell)dataRow.CreateCell(3);
        //    cell.CellStyle = style;
        //    cell.SetCellValue("Price");
        //    cell = (HSSFCell)dataRow.CreateCell(4);
        //    cell.CellStyle = style;
        //    cell.SetCellValue("Company");

        //    for (int i = 0; i < _Bike.Count; i++)
        //    {
        //        dataRow = (HSSFRow)sheet.CreateRow(i + 1);
        //        dataRow.CreateCell(0).SetCellValue(i + 1);
        //        dataRow.CreateCell(1).SetCellValue(_Bike[i].Name);
        //        dataRow.CreateCell(2).SetCellValue(_Bike[i].Type);
        //        dataRow.CreateCell(3).SetCellValue(_Bike[i].Price);
        //        dataRow.CreateCell(3).SetCellValue(_Bike[i].Company);
        //    }
        //    MemoryStream ms = new MemoryStream();
        //    templateWorkbook.Write(ms);
        //    return File(ms.ToArray(), "application/vnd.ms-excel", "Bikes.xls");


        //}
    }
}

