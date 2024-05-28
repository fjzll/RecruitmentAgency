using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assessment2RecruitmentAgency
{
    /// <summary>
    /// Lili Zheng Assessement 2 Recruitment Agency Project
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Properties
        public RecruitmentSystem newRecruitmentSystem { get; set; }

        //Constructors
        public MainWindow()
        {
            InitializeComponent();
            newRecruitmentSystem = new RecruitmentSystem();
            this.DataContext = newRecruitmentSystem;
        }

        //methods

        //Add a contractor to contractor list when ButtonAdd is clicked.
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextboxFirstName.Text) || string.IsNullOrWhiteSpace(TextboxLastName.Text))
            {
                MessageBox.Show("Please enter first name and last name!");
                return;
            }
            decimal hourlyWage = 0;
            if(!decimal.TryParse(TextboxHourlyWage.Text, out hourlyWage) || hourlyWage <= 0)
            {
                MessageBox.Show("Please enter a valid hourly wage!");
                return;
            }

            Contractor newContractor = new Contractor(TextboxFirstName.Text, TextboxLastName.Text, hourlyWage);

            newRecruitmentSystem.AddContractor(newContractor);
            ListContractors.ItemsSource = null;
            ListContractors.ItemsSource = newRecruitmentSystem.Contractors;
            TextboxFirstName.Clear();
            TextboxLastName.Clear();
            TextboxHourlyWage.Clear();
        }

        //Remove a contractor from contractor list when ButtonRemove is clicked.
        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            if(ListContractors.SelectedItem is not Contractor)
            {
                MessageBox.Show("Please select a contractor");
                return;
            }
            Contractor selectedContractor = (Contractor)ListContractors.SelectedItem;

            newRecruitmentSystem.RemoveContractor(selectedContractor);
            ListContractors.ItemsSource = null;
            ListContractors.ItemsSource = newRecruitmentSystem.Contractors;

        }

        //Add a job to job list when ButtonCreateJobs is clicked
        private void ButtonCreateJobs_Click(object sender, RoutedEventArgs e)
        {
            //validation of the inputs of job title, job cost and job date
            if (string.IsNullOrWhiteSpace(TextboxJobTitle.Text))
            {
                MessageBox.Show("Please enter the job title!");
                return;
            }

            decimal jobCost = 0;
            if(!decimal.TryParse(TextboxJobCost.Text, out jobCost) || jobCost <= 0)
            {
                MessageBox.Show("Please enter a valid cost!");
                return;
            }

            DateTime jobDate;
            if (JobDateDatePicker.SelectedDate.HasValue) { jobDate = JobDateDatePicker.SelectedDate.Value; }
            else { jobDate = DateTime.Today; }

            Job newJob = new Job(TextboxJobTitle.Text, jobDate, jobCost);

            newRecruitmentSystem.AddJobs(newJob);
            ListJobs.ItemsSource = null;
            ListJobs.ItemsSource= newRecruitmentSystem.Jobs;
            TextboxJobTitle.Clear();
            TextboxJobCost.Clear();
            JobDateDatePicker.SelectedDate = null;

        }

        //Mark job as complete when ButtonComplete is clicked
        private void ButtonComplete_Click(object sender, RoutedEventArgs e)
        {
            if (ListJobs.SelectedItem is Job selectedJob && selectedJob.JobCompleted != Job.JobStatus.Completed)
            {
                try
                {
                    newRecruitmentSystem.CompleteJob(selectedJob);
                    ListContractors.ItemsSource = null;
                    ListContractors.ItemsSource = newRecruitmentSystem.Contractors;
                    ListJobs.ItemsSource = null;
                    ListJobs.ItemsSource = newRecruitmentSystem.Jobs;
                }
                catch(InvalidOperationException ex) 
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                MessageBox.Show("Please select a job");
            }

        }

        //Assign a contractor to a job when ButtonAssign is clicked
        private void ButtonAssign_Click(object sender, RoutedEventArgs e)
        {
           bool assignmentSuccess;
           if (ListJobs.SelectedItem is Job selectedJob && ListContractors.SelectedItem is Contractor selectedContractor)
            {
                assignmentSuccess = newRecruitmentSystem.AssignJob(selectedContractor, selectedJob);
                if (assignmentSuccess) 
                {
                    MessageBox.Show("Job assigned");
                    ListContractors.ItemsSource = null;
                    ListContractors.ItemsSource = newRecruitmentSystem.Contractors;
                    ListJobs.ItemsSource = null;
                    ListJobs.ItemsSource = newRecruitmentSystem.Jobs;
                }
                else
                {
                    MessageBox.Show("Job assignment failed");
                }
                
            }
            else
            {
                MessageBox.Show("Please select a job and an available contractor!");
            }

        }

        //Show all contractors when ButtonAllContractors is clicked
        private void ButtonAllContractors_Click(object sender, RoutedEventArgs e)
        {
            List<Contractor> allContractorList = newRecruitmentSystem.GetContractors();
            ListContractors.ItemsSource = null;
            ListContractors.ItemsSource = allContractorList;

        }

        //Show available contractors when ButtonAvailableContractors is clicked
        private void ButtonAvailableContractors_Click(object sender, RoutedEventArgs e)
        {
            List<Contractor> availableContractorList = newRecruitmentSystem.GetAvailableContractors();
            ListContractors.ItemsSource = null;
            ListContractors.ItemsSource = availableContractorList;
        }

        //Show all jobs when ButtonAllJobs is clicked
        private void ButtonAllJobs_Click(object sender, RoutedEventArgs e)
        {
            List<Job> allJobList = newRecruitmentSystem.GetJobs();
            ListJobs.ItemsSource = null;
            ListJobs.ItemsSource = allJobList;
        }

        //Show unassigned jobs when ButtonUnassignedJobs is clicked
        private void ButtonUnassignedJobs_Click(object sender, RoutedEventArgs e)
        {
            List<Job> unassignedJobList = newRecruitmentSystem.GetUnassignedJobs();
            ListJobs.ItemsSource = null;
            ListJobs.ItemsSource= unassignedJobList;
        }

        //Function to search completed jobs by cost when Search jobs by cost button is clicked
        private void ButtonSortJobsByCost_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(TextBoxMinCost.Text, out decimal minCost) && decimal.TryParse(TextBoxMaxCost.Text, out decimal maxCost))
            { 
                if (minCost > maxCost)
                {
                    MessageBox.Show("Minimum cost can not be higher than maximum cost!");
                    return;
                }
                else
                {
                    List<Job> jobInRange = newRecruitmentSystem.GetJobByCost(minCost, maxCost);
                    ListJobs.ItemsSource = null;
                    ListJobs.ItemsSource = jobInRange;
                    TextBoxMinCost.Text = "Min Cost";
                    TextBoxMaxCost.Text = "Max Cost";
                }           
            }

            else
            {
                MessageBox.Show("Please enter the minimum and maximum cost!");
                return;
            }
        }

        //Clear text box when user clicks on the text box to enter min cost
        private void TextBoxMinCost_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxMinCost.Clear();
        }

        //Show the default "Min Cost" for TextBoxMinCost when user does not click nor enter anything
        //in the text box.
        private void TextBoxMinCost_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxMinCost.Text))
            {
                TextBoxMinCost.Text = "Min Cost";
            }

        }

        //Clear text box when user clicks on the text box to enter max cost
        private void TextBoxMaxCost_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxMaxCost.Clear();
        }

        //Show the default "Max Cost" for TextBoxMaxCost when user does not click nor enter anything
        //in the text box
        private void TextBoxMaxCost_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxMaxCost.Text))
            {
                TextBoxMaxCost.Text = "Max Cost";
            }
        }

        //Pop up the error messge when invalid date is entered
        private void JobDateDatePicker_DateValidationError(object sender, DatePickerDateValidationErrorEventArgs e)
        {
            MessageBox.Show("Please enter a valid date!");
        }
    }
}
