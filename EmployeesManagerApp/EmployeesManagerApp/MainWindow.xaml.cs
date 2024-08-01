using BL;
using DAL.Models;
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


namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AddEmployee addEmployeeWin;
        InterviewsMBL interviewsMBL;
        List<string> categories;
        private bool isUpdating = false;

        public MainWindow()
        {
            addEmployeeWin = new AddEmployee();
            interviewsMBL = new InterviewsMBL();

            InitializeComponent();

            var employees = interviewsMBL.GetEmployees();
            CandidateDataGrid.ItemsSource = employees;

            //var filter = interviewsMBL.GetRoles();
            //ComboBoxFilterCategory.ItemsSource = filter;

            categories = new List<string>() { "Role", "City", "Starting working year", "Decade" };
            ComboBoxFilterOptions.ItemsSource = categories;
            ComboBoxFilterOptions.SelectionChanged += ComboBoxFilterOptions_SelectionChanged;
        }


        public bool Refresh(Employee employee)
        {
            var employees = interviewsMBL.GetEmployees();
            CandidateDataGrid.ItemsSource = employees;
           
            return true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            addEmployeeWin.EventAddEmployee += Refresh; // שימוש בארוע הוספת עובד
            addEmployeeWin.ShowDialog();
            //Refresh();
        }

        private void CandidateDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBoxFilterCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isUpdating || ComboBoxFilterOptions.SelectedItem == null || ComboBoxFilterCategory.SelectedItem == null)
                return;
            if (ComboBoxFilterOptions.SelectedItem.ToString() == categories[0])
            {
                var employeesByRole = interviewsMBL.GetEmployeesByRole(ComboBoxFilterCategory.SelectedItem.ToString());
                CandidateDataGrid.ItemsSource = employeesByRole;
            }
            else
            {
                if (ComboBoxFilterOptions.SelectedItem.ToString() == categories[1])
                {
                    var employeesByCity = interviewsMBL.GetEmployeesByCity(ComboBoxFilterCategory.SelectedItem.ToString());
                    CandidateDataGrid.ItemsSource = employeesByCity;
                }
                else
                {
                    if (ComboBoxFilterOptions.SelectedItem.ToString() == categories[2])
                    {
                        var employeesByStartWork = interviewsMBL.GetEmployeesByStartWork(int.Parse(ComboBoxFilterCategory.SelectedItem.ToString()));
                        CandidateDataGrid.ItemsSource = employeesByStartWork;
                    }
                    else
                    {
                        if (ComboBoxFilterOptions.SelectedItem.ToString() == categories[3])
                        {
                            var employeesByDecade = interviewsMBL.GetEmployeesByDecade(ComboBoxFilterCategory.SelectedItem.ToString());
                            CandidateDataGrid.ItemsSource = employeesByDecade;
                        }
                    }
                }
            }
        }

        private void ComboBoxFilterOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            isUpdating = true;
            ComboBoxFilterCategory.ItemsSource = null;
            if (ComboBoxFilterOptions.SelectedItem.ToString() == categories[0])
            {
                var filter = interviewsMBL.GetRoles();
                ComboBoxFilterCategory.ItemsSource = filter;
            }
            else
            {
                if (ComboBoxFilterOptions.SelectedItem.ToString() == categories[1])
                {
                    var filter = interviewsMBL.GetCity();
                    ComboBoxFilterCategory.ItemsSource = filter;
                }
                else
                {
                    if (ComboBoxFilterOptions.SelectedItem.ToString() == categories[2])
                    {
                        var filter = interviewsMBL.GetStartWork();
                        ComboBoxFilterCategory.ItemsSource = filter;
                    }
                    else
                    {
                        if (ComboBoxFilterOptions.SelectedItem.ToString() == categories[3])
                        {
                            var filter = interviewsMBL.GetAgeDecade();
                            ComboBoxFilterCategory.ItemsSource = filter;
                        }
                    }
                }
            }
            var employees = interviewsMBL.GetEmployees();
            CandidateDataGrid.ItemsSource = employees;
            isUpdating = false;
        }
    }
}
