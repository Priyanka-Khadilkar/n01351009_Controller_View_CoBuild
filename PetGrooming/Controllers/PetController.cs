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
using PetGrooming.Models.ViewModels;

namespace PetGrooming.Controllers
{
    public class PetController : Controller
    {
        //Coonection for database
        private PetGroomingContext db = new PetGroomingContext();

        // GET: Pet
        /// <summary>
        /// Listing of all Pets on page load
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            //Will write search logic here for search the list 

            //Sql query for listing all pets
            List<Pet> pets = db.Pets.SqlQuery("Select * from Pets").ToList();
            return View(pets);

        }

        // GET: Pet/Details/{id}
        /// <summary>
        /// Display Pet data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Show(int? id)
        {
            //Checking if id is null 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Debug.WriteLine("Displaying pet with id " + Convert.ToString(id));

            //Query for selecting a pet with returing querystring,which holds the PetID of pet
            Pet pet = db.Pets.SqlQuery("select * from pets where petid=@PetID", new SqlParameter("@PetID", id)).FirstOrDefault();
            //If pet data is not found in database
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);
        }

        /// <summary>
        /// Add a new Pet
        /// </summary>
        /// <param name="PetName"></param>
        /// <param name="PetWeight"></param>
        /// <param name="PetColor"></param>
        /// <param name="SpeciesID"></param>
        /// <param name="PetNotes"></param>
        /// <returns>It redirects to list page</returns>
        [HttpPost]
        public ActionResult New(string PetName, Double PetWeight, String PetColor, int SpeciesID, string PetNotes)
        {


            string query = "insert into pets (PetName, PetWeight, PetColor, SpeciesID, PetNotes) values (@PetName,@PetWeight,@PetColor,@SpeciesID,@PetNotes)";
            SqlParameter[] sqlParams = new SqlParameter[5]; //0,1,2,3,4 pieces of information to add
            //each piece of information is a key and value pair
            sqlParams[0] = new SqlParameter("@PetName", PetName);
            sqlParams[1] = new SqlParameter("@PetWeight", PetWeight);
            sqlParams[2] = new SqlParameter("@PetColor", PetColor);
            sqlParams[3] = new SqlParameter("@SpeciesID", SpeciesID);
            sqlParams[4] = new SqlParameter("@PetNotes", PetNotes);

            //db.Database.ExecuteSqlCommand will execute insert, update, delete statements
            db.Database.ExecuteSqlCommand(query, sqlParams);

            Debug.WriteLine("Created a pet with name " + PetName);
            //Redirect to List Page
            return RedirectToAction("List");
        }


        /// <summary>
        /// Create Pet page on load data
        /// </summary>
        /// <returns></returns>
        public ActionResult New()
        {
            //returing the all list of species for loading dropdown of species
            List<Species> species = db.Species.SqlQuery("select * from Species").ToList();
            return View(species);
        }

        /// <summary>
        /// Update pet page on load method to load selected data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Update(int id)
        {
            //need information about a particular pet
            Pet selectedpet = db.Pets.SqlQuery("select * from pets where petid = @id", new SqlParameter("@id", id)).FirstOrDefault();

            //select all species for displaying in dropdown
            string query = "select * from species";
            List<Species> selectedSpecies = db.Species.SqlQuery(query).ToList();

            //Using ViewModel for the listing species in dropdown and display pet data for updation
            PetViewModel viewModel = new PetViewModel();
            viewModel.species = selectedSpecies;
            viewModel.pet = selectedpet;

            //return the viewmodel
            return View(viewModel);
        }

        /// <summary>
        /// Update the pet
        /// </summary>
        /// <param name="id">Pet Id</param>
        /// <param name="PetName"></param>
        /// <param name="PetColor"></param>
        /// <param name="PetWeight"></param>
        /// <param name="SpeciesID"></param>
        /// <param name="PetNotes"></param>
        /// <returns>Return to the list of pets page</returns>
        [HttpPost]
        public ActionResult Update(int id, string PetName, string PetColor, double PetWeight, int SpeciesID, string PetNotes)
        {

            string query = "update pets set PetName=@PetName, PetWeight=@PetWeight, PetColor=@PetColor, SpeciesID=@SpeciesID, PetNotes = @PetNotes where PetID=@id";
            SqlParameter[] sqlParams = new SqlParameter[6];

            sqlParams[0] = new SqlParameter("@PetName", PetName);
            sqlParams[1] = new SqlParameter("@PetWeight", PetWeight);
            sqlParams[2] = new SqlParameter("@PetColor", PetColor);
            sqlParams[3] = new SqlParameter("@SpeciesID", SpeciesID);
            sqlParams[4] = new SqlParameter("@PetNotes", PetNotes);
            sqlParams[5] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, sqlParams);


            //Redirect to List Page
            return RedirectToAction("List");
        }

        /// <summary>
        /// Delete Pet 
        /// </summary>
        /// <param name="id">Id of Pet for deleting pet</param>
        /// <returns>return to the list page</returns>
        public ActionResult Delete(int id)
        {
            //Query to delete pet with the particular id
            string query = "delete from pets where PetID = @id";
            SqlParameter param = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, param);
            return RedirectToAction("List");
        }

        //Space up the garbage memory
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
