using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StudentCourseRegistration.Models;

namespace StudentCourseRegistration.Controllers
{
    /// <summary>
    /// Home controller providing a modern university dashboard with live registration metrics.
    /// </summary>
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var allStudents = StudentRepository.GetAll();
            var availableCourses = StudentRepository.GetAvailableCourses();

            var viewModel = new DashboardViewModel
            {
                TotalStudents = allStudents.Count,
                TotalCoursesAvailable = availableCourses.Count,
                ActiveSemestersCount = allStudents.Select(s => s.Semester).Distinct().Count(),
                RecentStudents = allStudents.Take(5).ToList(),
                StudentsByCourse = allStudents
                    .GroupBy(s => s.Course)
                    .ToDictionary(g => g.Key, g => g.Count())
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
