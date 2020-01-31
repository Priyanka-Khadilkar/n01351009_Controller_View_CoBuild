using System;
using System.Collections.Generic;
using System.Data;
//required for SqlParameter class
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PetGrooming.Data;
using PetGrooming.Models;
using System.Diagnostics;

namespace PetGrooming.Controllers
{
    public class SpeciesController : Controller
    {
        //Connection to the database
        private PetGroomingContext db = new PetGroomingContext();
        // GET: Species
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// List of all species
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            //Will write search logic here for search the list 

            //select all species 
            List<Species> myspecies = db.Species.SqlQuery("Select * from species").ToList();
            return View(myspecies);
        }

        /// <summary>
        /// Load create new species page
        /// </summary>
        /// <returns></returns>
        public ActionResult New()
        {
            return View();
        }

        /// <summary>
        ///  Create new species
        /// </summary>
        /// <param name="species">Species Object</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult New(Species species)
        {
            string query = "insert into species (Name) values (@SpeciesName)";
            SqlParameter param = new SqlParameter("@SpeciesName", species.Name);
            db.Database.ExecuteSqlCommand(query, param);
            return RedirectToAction("List");
        }

        /// <summary>
        /// Delete Species 
        /// </summary>
        /// <param name="id">Id of species for deleting species</param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            //Query to delete species with the particular id
            string query = "delete from Species where SpeciesID = @id";
            SqlParameter param = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, param);
            return RedirectToAction("List");
        }

        /// <summary>
        /// Update Species page on load
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Update(int id)
        {
            string query = "select * from species where SpeciesID = @id";
            SqlParameter param = new SqlParameter("@id", id);

            Species selctedspecies = db.Species.SqlQuery(query, param).FirstOrDefault();
            return View(selctedspecies);
        }

        /// <summary>
        /// Update Species
        /// </summary>
        /// <param name="id">Id of Species</param>
        /// <param name="species">Updated object of species</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Update(int id, Species species)
        {
            //Update query for a species
            string query = "Update species set Name = @SpeciesName where SpeciesID = @id";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SpeciesName", species.Name);
            param[1] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, param);

            return RedirectToAction("List");
        }

        /// <summary>
        /// Display the details of species
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Show(int id)
        {
            //select species query
            string query = "select * from species where SpeciesID = @id";
            SqlParameter param = new SqlParameter("@id", id);

            Species species = db.Species.SqlQuery(query, param).FirstOrDefault();
            return View(species);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}