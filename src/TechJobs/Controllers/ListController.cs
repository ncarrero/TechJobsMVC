using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TechJobs.Models;

namespace TechJobs.Controllers
{
    public class ListController : Controller
    {
        internal static Dictionary<string, string> columnChoices = new Dictionary<string, string>();

        // This is a "static constructor" which can be used
        // to initialize static members of a class
        //used to populate columnChoices with values to
        //provide a centralized collection of different list and search 
        //options presented through the user interface
        static ListController() 
        {
            
            columnChoices.Add("core competency", "Skill");
            columnChoices.Add("employer", "Employer");
            columnChoices.Add("location", "Location");
            columnChoices.Add("position type", "Position Type");
            columnChoices.Add("all", "All");
        }

        public IActionResult Index()
        {
            //display types of lists user can view
            ViewBag.columns = columnChoices;
            return View();
        }

        public IActionResult Values(string column)
        {
            //displays data obtained from JobData.cs in Models
            //uses query parameter passed in as a column to determine which values to fetch from JobData.cs

            //will fetch all job data, then
            //render the Jobs.cshtml view template rather than default view

            //** this is what needs to be fully implemented in SearchController**
            if (column.Equals("all"))
            {
                List<Dictionary<string, string>> jobs = JobData.FindAll();
                ViewBag.title =  "All Jobs";
                ViewBag.jobs = jobs;
                return View("Jobs");
            }

            //fetch only values for given column and passes them to Values.cshtml view template
            else
            {
                List<string> items = JobData.FindAll(column);
                ViewBag.title =  "All " + columnChoices[column] + " Values";
                ViewBag.column = column;
                ViewBag.items = items;
                return View();
            }
        }

        //displays data obtained from JobData.cs in Models
        //will only display jobs that are in a particular column using the search term (value)
        public IActionResult Jobs(string column, string value)
        {
            
            //take in 2 query parameters, column and value
            //searching for particular value in particular column and then display jobs that match
            //this result will only happen when user clicks on link within one of the views,
            //rather than submitting a form

            //** use this for one of the TODOS **
            List<Dictionary<String, String>> jobs = JobData.FindByColumnAndValue(column, value);
            ViewBag.title = "Jobs with " + columnChoices[column] + ": " + value;
            ViewBag.jobs = jobs;

            return View();
        }
    }
}
