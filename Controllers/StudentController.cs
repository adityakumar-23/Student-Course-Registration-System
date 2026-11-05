using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StudentCourseRegistration.Models;

namespace StudentCourseRegistration.Controllers
{
    /// <summary>
    /// Controller responsible for student registration, listing, and details.
    /// Demonstrates MVC principles, Model Binding, and ModelState validation.
    /// </summary>
    public class StudentController : Controller
    {
        // GET: /Student/Index
        // Displays the list of all registered students with optional search/filter
        public IActionResult Index(string? searchString, string? selectedCourse)
        {
            var students = StudentRepository.GetAll();

            // Filter by search string (matches Name, Email, or Student ID)
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                var query = searchString.Trim().ToLower();
                students = students.Where(s =>
                    (s.StudentName != null && s.StudentName.ToLower().Contains(query)) ||
                    (s.Email != null && s.Email.ToLower().Contains(query)) ||
                    (s.StudentId.HasValue && s.StudentId.Value.ToString().Contains(query))
                ).ToList();

                ViewBag.CurrentSearch = searchString;
            }

            // Filter by course dropdown if selected
            if (!string.IsNullOrWhiteSpace(selectedCourse))
            {
                students = students.Where(s => s.Course == selectedCourse).ToList();
                ViewBag.SelectedCourse = selectedCourse;
            }

            ViewBag.Courses = StudentRepository.GetAvailableCourses();
            return View(students);
        }

        // GET: /Student/Create
        // Renders the registration form
        public IActionResult Create()
        {
            ViewBag.Courses = StudentRepository.GetAvailableCourses();
            var model = new Student
            {
                RegistrationDate = System.DateTime.Now
            };
            return View(model);
        }

        // POST: /Student/Create
        // Handles form submission, performs Model Binding and ModelState validation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            // Custom business logic validation: Check for duplicate Student ID
            if (student.StudentId.HasValue && StudentRepository.Exists(student.StudentId.Value))
            {
                ModelState.AddModelError("StudentId", "A student with this Student ID is already registered.");
            }

            // Verify if model state complies with all Data Annotation rules
            if (!ModelState.IsValid)
            {
                // Validation failed: re-populate view bag data and redisplay form with error messages
                ViewBag.Courses = StudentRepository.GetAvailableCourses();
                return View(student);
            }

            // Validation succeeded: save the student to the in-memory repository
            bool added = StudentRepository.Add(student);
            if (!added)
            {
                ModelState.AddModelError(string.Empty, "Unable to save student record. Please try again.");
                ViewBag.Courses = StudentRepository.GetAvailableCourses();
                return View(student);
            }

            // Set success notification using TempData (survives redirection)
            TempData["SuccessMessage"] = $"Student '{student.StudentName}' (ID: {student.StudentId}) registered successfully!";

            // Redirect to the Student List view
            return RedirectToAction(nameof(Index));
        }

        // GET: /Student/Details/{id}
        // Shows complete details for an individual student
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "Student ID was not specified.";
                return RedirectToAction(nameof(Index));
            }

            var student = StudentRepository.GetById(id.Value);
            if (student == null)
            {
                TempData["ErrorMessage"] = $"Student with ID {id.Value} was not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(student);
        }

        // POST: /Student/Delete/{id}
        // Removes a student record (bonus practical feature for student management)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var student = StudentRepository.GetById(id);
            if (student != null)
            {
                StudentRepository.Delete(id);
                TempData["SuccessMessage"] = $"Student '{student.StudentName}' (ID: {id}) has been removed.";
            }
            else
            {
                TempData["ErrorMessage"] = $"Student with ID {id} was not found.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

