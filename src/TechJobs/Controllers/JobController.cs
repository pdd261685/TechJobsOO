using Microsoft.AspNetCore.Mvc;
using TechJobs.Data;
using TechJobs.Models;
using TechJobs.ViewModels;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static JobData jobData;

        static JobController()
        {
            jobData = JobData.GetInstance();
        }

        // The detail display for a given Job at URLs like /Job?id=17
        /*[Route("/Job/{id}")]*/
        public IActionResult Index(int id)
        {
            // TODO #1 - get the Job with the given ID and pass it into the view

            return View(jobData.Find(id));
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
            // TODO #6 - Validate the ViewModel and if valid, create a 
            // new Job and add it to the JobData data store. Then
            // redirect to the Job detail (Index) action/view for the new Job.
            if (ModelState.IsValid)
            {
                jobData = JobData.GetInstance();// need this to access the methods in there like Find()


                Job newJob = new Job {
                    Name = newJobViewModel.Name,
                    // employer id => newJobViewModel.EmployerID
                    //JobData.Employers.Find() to find the employer with the employer ID

                    //jobData.Find(newJobViewModel.EmployerID) returns a job object & hence .Employer
                    Employer = jobData.Employers.Find(newJobViewModel.EmployerID),

                    Location =jobData.Locations.Find(newJobViewModel.LocationID),

                    CoreCompetency =jobData.CoreCompetencies.Find(newJobViewModel.SkillID),

                    PositionType=jobData.PositionTypes.Find(newJobViewModel.PositionID)

                };
                jobData.Jobs.Add(newJob);

                return Redirect("/Job?id=" + newJob.ID);
            }
            return View(newJobViewModel);
        }
    }
}
