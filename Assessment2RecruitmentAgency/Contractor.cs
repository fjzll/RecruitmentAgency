using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Assessment2RecruitmentAgency
{
    /// <summary>
    /// 
    /// </summary>
    public class Contractor
    {
        //Definition of enum
        public enum ContractorStatus
        {
            Available,
            OnJob
        }

        //Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal HourlyWage { get; set; }
        public ContractorStatus Status { get; set; }

        
        
        //Constructors
        public Contractor(string firstName, string lastName, decimal hourlyWage)
        {
            FirstName = firstName;
            LastName = lastName;
            HourlyWage = hourlyWage;
            Status = ContractorStatus.Available;
        }
        public Contractor() { }

    }
}
