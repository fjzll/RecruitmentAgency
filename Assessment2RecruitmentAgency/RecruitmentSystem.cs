using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Assessment2RecruitmentAgency
{
    public class RecruitmentSystem
    {
        /// <summary>
        /// This is a class that manages all aspects of the Recruiment Agency,
        /// including add/remove a contractor, assign a job to a contractor,
        /// view available contractors, view unassigned jbos, and search a job
        /// within a cost range.
        /// </summary>
        
        //Properties

        public List<Contractor> Contractors { get; set; }
        public List<Job> Jobs { get; set; }

        //Constructors
        public RecruitmentSystem()
        {
            Contractors = new List<Contractor>();
            Jobs = new List<Job>();
        }

        //Methods and business logic
        
        //Add contractor function. When a contractor's details are entered and click event is trigger,
        //contractor's details will be added and displayed in the Listview.
        public void AddContractor(Contractor contractor)
        {
            Contractors.Add(contractor);
            
        }

        //Remove contractor function. When a contractor's details are selected and click event is trigger,
        //contractor's details will be removed in the Listview.
        public void RemoveContractor(Contractor contractor)
        {
            Contractors.Remove(contractor);
        }
        
        //Add new job to the jobs collection and dispay in the ListView
        public void AddJobs(Job job)
        {
            Jobs.Add(job);
        }

        //Assign a contractor to a job
        public bool AssignJob(Contractor contractor,Job job)
        {
           if (contractor.Status is Contractor.ContractorStatus.Available && job.ContractorName == "")
           {
              job.AssignedContractor = contractor;
              contractor.Status = Contractor.ContractorStatus.OnJob;
              job.ContractorName = $"{contractor.FirstName} {contractor.LastName}";
              job.JobCompleted = Job.JobStatus.InProgress;
              return true;
           }
           else
           {
              return false;
           }
        }

        //Mark job as complete and return the assigned contractor back to "available"
        public void CompleteJob(Job job)
        {
            job.JobCompleted = Job.JobStatus.Completed;
            if (job.AssignedContractor != null)
            {
                job.AssignedContractor.Status = Contractor.ContractorStatus.Available;
            }        
        }

        //Get the list of all contractors
        public List<Contractor> GetContractors()
        {
            return Contractors;
        }

        //Get the list of all jobs
        public List<Job> GetJobs()
        {
            return Jobs;
        }

        //Get the list of available contractors
        public List<Contractor> GetAvailableContractors()
        {
            List<Contractor> availableContractors = new List<Contractor>();
            foreach (Contractor contractor in Contractors)
            {
                if (contractor.Status == Contractor.ContractorStatus.Available)
                {
                    availableContractors.Add(contractor);
                }
            }
            return availableContractors;
       
        }

        //Get the list of unassigned jobs
        public List<Job> GetUnassignedJobs() 
        {
            List<Job> unassignedJobs = new List<Job>();
            foreach (Job job in Jobs)
            {
                if (job.JobCompleted == Job.JobStatus.Unassigend)
                {
                    unassignedJobs.Add(job);
                }
            }
            return unassignedJobs;
        }

        //Get the completed job list, and find jobs within the cost range, add them
        //to the jobsInRange list, finally return a list of jobs within the cost range.
        public List<Job> GetJobByCost(decimal mincost, decimal maxcost)
        {
            List<Job> completedJobs = new List<Job>();
            List<Job> jobsInRange = new List<Job>();
            foreach (Job job in Jobs)
            {
                if (job.JobCompleted == Job.JobStatus.Completed)
                {
                    completedJobs.Add(job);
                }
            }
            foreach (Job job in completedJobs)
            {
                if (job.JobCost >= mincost && job.JobCost <= maxcost)
                {
                    jobsInRange.Add(job);
                }
            }
            return jobsInRange;
        }
    }
}
