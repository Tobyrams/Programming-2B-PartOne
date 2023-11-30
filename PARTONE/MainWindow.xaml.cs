using System;
using System.Collections.Generic;
using System.Linq;
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
using PARTONE;
using PARTONE_CLASSLIBRARY;

namespace PARTONE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //This creates an instance of the listOfModules window so that we can access its elements
        static ListOfModules listofmodulesInstance = new ListOfModules();
        //Here I am able to access the listbox from the other window here
        ListBox listModules = listofmodulesInstance.ListBox;
        //Instance of the ModuleManager so that we can access its class
        ModuleManager ModuleManager = new ModuleManager();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddModule_ButtonClick(object sender, RoutedEventArgs e)
        {
            //Variables
            string code = txtCode.Text;
            string name = txtName.Text;
            int numOfCredits = int.Parse(txtCreditSlider.Text);
            int classHoursPerWeek = int.Parse(txtClassHoursSliderTextBox.Text);
            int weeksInSemester = int.Parse(txtNumWeeksSemesterSliderTextBox.Text);
            var semesterStart = SemesterStartDate.SelectedDate;
            var certainDate = CertainDatePicker.SelectedDate;
            int certainDateValue;
            int id = ModuleManager.GetAllModules().Count() + 1;

            //Test if user has NOT typed the incorrect detailes else(if they have) an error message will appear
            if (!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(CertainDateTextBox.Text) && numOfCredits != 0 && classHoursPerWeek != 0 && weeksInSemester != 0 && semesterStart != null && certainDate != null)
            {
                //This test if the certain date enterd is an int else it will send an error
                if (int.TryParse(CertainDateTextBox.Text, out certainDateValue)) { }
                else { MessageBox.Show("Make sure the Certain Date is an int"); return; }

                //This Creates a new module of type Module
                Module newModule = new Module(id, code, name, numOfCredits, weeksInSemester, classHoursPerWeek, (DateTime)semesterStart, (DateTime)certainDate, certainDateValue,PARTONE_CLASSLIBRARY.Calculations.self_Study_Hours(numOfCredits, weeksInSemester, classHoursPerWeek), PARTONE_CLASSLIBRARY.Calculations.remainingStudyHours(PARTONE_CLASSLIBRARY.Calculations.self_Study_Hours(numOfCredits, weeksInSemester, classHoursPerWeek), certainDateValue));
               
                //Adds the module to a list call modules where it will then be able to be iterated over
                //as it is an IEnumerabl.
                ModuleManager.AddModule(newModule);

                //Clear all the input fields
                txtCode.Clear();
                txtName.Clear();
                txtCreditSlider.Clear();
                txtClassHoursSliderTextBox.Clear();
                txtNumWeeksSemesterSliderTextBox.Clear();
                SemesterStartDate.SelectedDate = null;
                CertainDatePicker.SelectedDate = null;
                CertainDateTextBox.Clear();

                //This updates the list box with the modules
                UpdateModuleListBox();


            }
            else
            {
                MessageBox.Show("Please make sure all details are entered!");
            }

            
            
            
            
        }

        private void UpdateModuleListBox()
        {
            listModules.Items.Clear();

            // Get and display all modules
            IEnumerable<Module> allModules = ModuleManager.GetAllModules();
            foreach (var module in allModules)
            {

                listModules.Items.Add(  $"Code: {module.CODE}\n" +
                                        $"Name: {module.NAME}\n" +
                                        $"Credits: {module.NUMOFCREDITS}\n" +
                                        $"Weeks in Semester: {module.NUMWEEKINSEMESTER}\n" +
                                        $"Class Hours Per Week: {module.CLASSHOURSPERWEEK}hrs\n" +
                                        $"Start Date: {module.STARTDATE}\n" +
                                        $"On {module.STUDYDATE} Studied for: {module.HOURSONSTUDYDATE}hrs\n" +
                                        $"Self Study Hours: {module.SELFSTUDYHOURS}\n" +
                                        $"Remaining Study Hours: {module.REMAININGSTUDYHOURS}");
            }
        }

        private void ViewModules_ButtonClick(object sender, RoutedEventArgs e)
        {
            
            listofmodulesInstance.Show();
        }

        /*When user clikc the text box the example text will disaper and the text
        foreground will turn to black.*/
        private void txtCode_GotFocus(object sender, RoutedEventArgs e)
        {
            txtCode.Clear();
            txtCode.Foreground = Brushes.Black;
        }
        private void txtName_GotFocus(object sender, RoutedEventArgs e)
        {
            txtName.Clear();
            txtName.Foreground = Brushes.Black;
        }
        private void CertainDateTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            CertainDateTextBox.Clear();
            CertainDateTextBox.Foreground = Brushes.Black;
        }
    }
}
