﻿using DAL;
using DAL.Models;

namespace BL
{
    public class InterviewsMBL
    {
        List<Employee> listOfEmployees;
        DBConnection dbConnection;
        List<Candidate> listCandidate;
        List<Interview> listInterview;
        InterviewsManagerContext context = new InterviewsManagerContext();

        public InterviewsMBL()
        {
            dbConnection = new DBConnection();
            listOfEmployees = dbConnection.GetAllEmployees();
            listCandidate = dbConnection.GetAllCandidates();
            listInterview = dbConnection.GetAllInterviews();
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return dbConnection.GetAllEmployees();
        }

        public List<string> GetRoles()
        {
            return listOfEmployees.Select(e => e.RoleInCompany).Distinct().ToList();
        }

        public IEnumerable<Employee> GetEmployeesByRole(string role)
        {
            return listOfEmployees.Where(r => r.RoleInCompany == role).ToList();
        }

        public List<string> GetNameCandidencies()
        {
            return listCandidate.Select(c => c.FirstName + " " + c.LastName).ToList();
        }

        public List<string> GetCity()
        {
            return listOfEmployees.Select(c => c.City).Distinct().ToList();
        }
        public IEnumerable<Employee> GetEmployeesByCity(string city)
        {
            return listOfEmployees.Where(c => c.City == city).ToList();
        }

        public List<int> GetStartWork()
        {
            return listOfEmployees.Select(c => c.StartOfWorkYear).Distinct().ToList();
        }
        public IEnumerable<Employee> GetEmployeesByStartWork(int year)
        {
            return listOfEmployees.Where(c => c.StartOfWorkYear == year).ToList();
        }
        public List<string> GetAgeDecade()
        {
            return listOfEmployees.Select(d => $"{(d.Age / 10) * 10}-{((d.Age / 10) * 10) + 10}").Distinct().ToList();
        }
        public IEnumerable<Employee> GetEmployeesByDecade(string decade)
        {
            var ages = decade.Split('-').Select(int.Parse).ToArray();
            var minAge = ages[0];
            var maxAge = ages[1];

            return listOfEmployees.Where(d => d.Age >= minAge && d.Age < maxAge).ToList();
        }
        public IEnumerable<dynamic> GetInterviewsDetails(string name)
        {
            string[] fullname = name.Split(' ');
            var interviewDetails = from i in context.Interviews
                                   join c in context.Candidates on i.CandidateId equals c.Id
                                   join e in context.Employees on i.InterviewerId equals e.Id
                                   where c.FirstName == fullname[0] && c.LastName == fullname[1]
                                   orderby i.InterviewDate descending
                                   select new
                                   {
                                       InterviewNUmber = i.InterviewNumber,
                                       RoleInCompany = i.RoleInCompany,
                                       InterviewDate = i.InterviewDate,
                                       InterviewerName = e.FirstName + " " + e.LastName,
                                       InterviewerPhone = e.PhoneNumber
                                   };
            return interviewDetails.ToList();
        }
    }
}