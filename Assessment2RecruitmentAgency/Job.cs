using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Assessment2RecruitmentAgency
{
    public class Job
    {
        //Defintion of enum
        public enum JobStatus { Unassigend, InProgress, Completed }

        //Properties
        public string JobTitle { get; set; }
        public string JobDate { get; set; }
        public decimal JobCost { get; set; }
        public JobStatus JobCompleted { get; set; }
        public string ContractorName { get; set; }
        public Contractor AssignedContractor { get; set; }

        //Contructors
        public Job() 
        {

        }

        public Job(string jobTitle, DateTime jobDate, decimal jobCost)
        {
            JobTitle = jobTitle;
            JobDate = jobDate.ToString("d");
            JobCost = jobCost;
            JobCompleted = JobStatus.Unassigend;
            ContractorName = "";
        }

        //Methods

    }
}
