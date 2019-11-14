using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TechJobs.Models;
using System;

namespace TechJobs.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.columns = ListController.columnChoices;
            ViewBag.title = "Search";
            return View();
        }

        // TODO #1 - Create a Results action method to process 
        // search request and display results (by column and by all jobs)

        
        //use form field names to name parameters appropriately
        //see below: public IActionResult Results(type of search, search term)
        public IActionResult Results(string searchType, string searchTerm = "")
        {
            //use some static methods provided by JobData.cs Model
            List<Dictionary<string, string>> jobs;
            if (searchType.Equals("all"))
            {
                jobs = JobData.FindByValue(searchTerm);
                //use FindByValue passing search term and store values in the list
            }
            else
            {
                //use search term and value (search type) and FindByColumnAndValue method to add/store values in list
                jobs = JobData.FindByColumnAndValue(searchType, searchTerm);
            }
            ViewBag.jobs = jobs;
            //pass ListController.columnChoices to the view similar to Index method
            ViewBag.columns = ListController.columnChoices;
            //title of search results
            //ViewBag.title = $"Search Results for {searchType} containing {searchTerm}.";
            // pass results into "Views/Search/Index.cshtml" view
            //(this is not default view for this action)
            return View("Index");
        }
    }
}
