using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Assessment2RecruitmentAgency;
using System.Collections.Generic;

namespace RecruitmentSystemUnitTests
{
    [TestClass]
    public class RecruitmentAgencyUnitTest
    {
        /// <summary>
        /// This is the TestClass for RecruitmentSystem Class.
        /// </summary>
        RecruitmentSystem recruitmentSystem;

        [TestInitialize]

        public void SetUp()
        {
            recruitmentSystem = new RecruitmentSystem();

        }

        [TestMethod]
        public void ContractorList_ListNotNull()
        {
           //Assert
            Assert.IsNotNull(recruitmentSystem.Contractors);
        }

        [TestMethod]
        public void JobList_ListNotNull() 
        {
            //Assert
            Assert.IsNotNull (recruitmentSystem.Jobs); 
        }

        [TestMethod]
        public void AddContractor_AddToContractorList()
        {
            //Arrange
            Contractor contractor = new Contractor("Bob", "Ross", 32);
            int expectedCount = 1;

            //Act
            recruitmentSystem.AddContractor(contractor);

            //Assert
            Assert.IsTrue(recruitmentSystem.Contractors.Contains(contractor));
            Assert.AreEqual(expectedCount, recruitmentSystem.Contractors.Count);

        }

        [TestMethod]
        public void AddJobs_AddToJobList()
        {
            //Arrange
            Job job = new Job("Perth Garden", DateTime.Today , 200);
            int expectedCount = 1;

            //Act
            recruitmentSystem.AddJobs(job);

            //Assert
            Assert.IsTrue(recruitmentSystem.Jobs.Contains(job));
            Assert.AreEqual(expectedCount, recruitmentSystem.Jobs.Count);

        }

        [TestMethod]

        public void RemoveContractor_RemoveFromList()
        {
            //Arrange
            Contractor contractor = new Contractor("Bob", "Ross", 32);

            //Act
            recruitmentSystem.AddContractor (contractor);
            recruitmentSystem.RemoveContractor(contractor);
            int expectedCount = 0;

            //Assert
            Assert.AreEqual(expectedCount, recruitmentSystem.Contractors.Count);

        }

        [TestMethod]

        public void AssignJob_Success_ReturnTrue()
        {
            //Arrange
            Contractor contractor = new Contractor();
            contractor.Status = Contractor.ContractorStatus.Available;
            Job job = new Job();
            job.JobCompleted = Job.JobStatus.Unassigend;
            job.ContractorName = "";

            //Act
            bool result = recruitmentSystem.AssignJob(contractor, job);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]

        public void AssignJob_ContractorOnJob_ReturnFalse()
        {
            //Arrange
            Contractor contractor = new Contractor();
            contractor.Status = Contractor.ContractorStatus.OnJob;
            Job job = new Job();
            job.JobCompleted = Job.JobStatus.Unassigend;
            job.ContractorName = "";

            //Act
            bool result = recruitmentSystem.AssignJob(contractor, job);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]

        public void AssignJob_JobAssigned_ReturnFalse()
        {
            //Arrange
            Contractor contractor = new Contractor();
            contractor.Status = Contractor.ContractorStatus.Available;
            Job job = new Job();
            job.JobCompleted = Job.JobStatus.InProgress;

            //Act
            bool result = recruitmentSystem.AssignJob(contractor, job);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CompleteJob_ContractorReturnToAvailable()
        {
            //Arrange
            Job job = new Job();
            job.JobCompleted = Job.JobStatus.InProgress;
            Contractor contractor = new Contractor("Boss", "Ross", 32);
            contractor.Status = Contractor.ContractorStatus.OnJob;
            job.AssignedContractor = contractor;

            //Act

            recruitmentSystem.CompleteJob(job);

            //Assert
            Assert.AreEqual(job.JobCompleted, Job.JobStatus.Completed);
            Assert.AreEqual(contractor.Status, Contractor.ContractorStatus.Available);

        }

        [TestMethod]
        public void CompleteJob_TryToCompleteUnassignedJob_ThrowException()
        {
            //Arrange
            Job job = new Job();
            job.JobCompleted = Job.JobStatus.Unassigend;

            //Act and Assert
            Assert.ThrowsException<InvalidOperationException>(() => recruitmentSystem.CompleteJob(job));

        }

        [TestMethod]
        public void GetContractors_ListNotNull()
        {

            //Act
            List<Contractor> contractor = recruitmentSystem.GetContractors();

            //Assert
            Assert.IsNotNull(contractor);

        }

        [TestMethod]
        public void GetJobs_ListNotNull()
        {

            //Act
            List<Job> job = recruitmentSystem.GetJobs();

            //Assert
            Assert.IsNotNull(job);

        }

        [TestMethod]
        public void GetAvailableContractors_ReturnListWithAvailableContractors()
        {

            //Arrange
            Contractor contractorOne = new Contractor("Bob", "Ross", 35);
            Contractor contractorTwo = new Contractor("Betty", "White", 32);
            contractorOne.Status = Contractor.ContractorStatus.Available;
            contractorTwo.Status = Contractor.ContractorStatus.OnJob;

            //Act
            recruitmentSystem.AddContractor(contractorOne);
            recruitmentSystem.AddContractor(contractorTwo);
            List<Contractor> availableContractorList = recruitmentSystem.GetAvailableContractors();
            int numberOfAvailableContractors = 1;

            //Assert
            Assert.AreEqual(numberOfAvailableContractors, availableContractorList.Count);


        }

        [TestMethod]
        public void GetAvailableContractors_ReturnEmptyList()
        {

            //Arrange
            Contractor contractorOne = new Contractor("Bob", "Ross", 35);
            Contractor contractorTwo = new Contractor("Betty", "White", 32);
            contractorOne.Status = Contractor.ContractorStatus.OnJob;
            contractorTwo.Status = Contractor.ContractorStatus.OnJob;

            //Act
            recruitmentSystem.AddContractor(contractorOne);
            recruitmentSystem.AddContractor(contractorTwo);
            List<Contractor> availableContractorList = recruitmentSystem.GetAvailableContractors();
            int numberOfAvailableContractors = 0;

            //Assert
            Assert.AreEqual(numberOfAvailableContractors, availableContractorList.Count);


        }

        [TestMethod]
        public void GetUnassignedJobs_ReturnListWithUnassignedJobs()
        {
            //Arrange
            Job jobOne = new Job();
            Job jobTwo = new Job();
            jobOne.JobCompleted = Job.JobStatus.Unassigend;
            jobTwo.JobCompleted = Job.JobStatus.InProgress;


            //Act
            recruitmentSystem.AddJobs(jobOne);
            recruitmentSystem.AddJobs(jobTwo);
            List<Job> availableJobList = recruitmentSystem.GetUnassignedJobs();
            int numberofAvailableJobs = 1;


            //Assert
            Assert.AreEqual(numberofAvailableJobs, availableJobList.Count);

        }

        [TestMethod]
        public void GetUnassignedJobs_ReturnEmptyList()
        {
            //Arrange
            Job jobOne = new Job();
            Job jobTwo = new Job();
            jobOne.JobCompleted = Job.JobStatus.Completed;
            jobTwo.JobCompleted = Job.JobStatus.InProgress;


            //Act
            recruitmentSystem.AddJobs(jobOne);
            recruitmentSystem.AddJobs(jobTwo);
            List<Job> availableJobList = recruitmentSystem.GetUnassignedJobs();


            //Assert
            Assert.AreEqual(0, availableJobList.Count);

        }

        [DataTestMethod]
        [DataRow("300", "400", 1)]
        [DataRow("100", "199", 0)]
        [DataRow("100", "200", 1)]
        [DataRow("200", "400", 2)]
        public void GetJobByCost_InRange_VerifyNumberOfJobInRange(string minCostString, string maxCostString, int expectedNumber)
        {
            //Arrange
            Job jobOne = new Job("One", DateTime.Today, 200m);
            Job jobTwo = new Job("Two", DateTime.Today, 400m);
            jobOne.JobCompleted = Job.JobStatus.Completed;
            jobTwo.JobCompleted=Job.JobStatus.Completed;
            decimal minCost = decimal.Parse(minCostString);
            decimal maxCost = decimal.Parse(maxCostString);

            //Act
            recruitmentSystem.AddJobs(jobOne);
            recruitmentSystem.AddJobs(jobTwo);
            List<Job> jobInRange = recruitmentSystem.GetJobByCost(minCost, maxCost);


            //Assert
            Assert.AreEqual(expectedNumber, jobInRange.Count);

        }

    }
}
