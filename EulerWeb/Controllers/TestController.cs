using EulerWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EulerWeb
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index(int id = 0)
        {
            return View(new TestModel(id));
        }

        // GET: Test/Details/5
        public ActionResult Details(int id)
        {
            return View(new TestModel(id));
        }

        // GET: api/Test
        [HttpGet]
        public IEnumerable<TestModel> Get()
        {
            return new TestModel[] { new TestModel(1), new TestModel(2) };
        }

        // GET: Test/Create
        public ActionResult Create()
        {
            return View(new TestModel());
        }

        // POST: Test/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View(new TestModel());
            }
        }

        // GET: Test/Edit/5
        public ActionResult Edit(int id)
        {
            return View(new TestModel(id));
        }

        // POST: Test/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View(new TestModel(id));
            }
        }

        // GET: Test/Delete/5
        public ActionResult Delete(int id)
        {
            return View(new TestModel(id));
        }

        // POST: Test/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View(new TestModel(id));
            }
        }
    }

    //public class TestdApiController : Controller
    //{
    //    // GET: api/Test
    //    [AcceptVerbs("Get")]
    //    public IEnumerable<TestModel> GetModels()
    //    {
    //        return new TestModel[] { new TestModel(1), new TestModel(2) };
    //    }

    //    // GET: api/Test
    //    [AcceptVerbs("Get")]
    //    public TestModel GetModel(int index)
    //    {
    //        return new TestModel(1);
    //    }
    //}
}
