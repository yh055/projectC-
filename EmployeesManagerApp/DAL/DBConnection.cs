using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
namespace DAL
{
    public class DBConnection
    {
        private InterviewsManagerContext context ;
       

            public DBConnection()
            {
                context = new InterviewsManagerContext();
            }

            public List<Employee> GetAllEmployees()
            {
                return context.Employees.ToList();
            }

            public List<Candidate> GetAllCandidates()
            {
                return context.Candidates.ToList();
            }

            public List<Interview> GetAllInterviews()
            {
                return context.Interviews.ToList();
            }


        }
    }
