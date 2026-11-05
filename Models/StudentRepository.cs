using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentCourseRegistration.Models
{
    /// <summary>
    /// In-memory static data repository for student records.
    /// Simulates database persistence during the application runtime.
    /// </summary>
    public static class StudentRepository
    {
        // Static list to persist data in memory across HTTP requests
        private static readonly List<Student> _students = new List<Student>();
        private static readonly object _lock = new object();

        // Static constructor to pre-seed initial sample students
        static StudentRepository()
        {
            SeedInitialData();
        }

        private static void SeedInitialData()
        {
            _students.AddRange(new List<Student>
            {
                new Student
                {
                    StudentId = 101001,
                    StudentName = "Aarav Sharma",
                    Email = "aarav.sharma@marwadi.edu.in",
                    Course = "B.Tech Computer Engineering",
                    Semester = 5,
                    RegistrationDate = DateTime.Now.AddDays(-25),
                    Status = "Active"
                },
                new Student
                {
                    StudentId = 101002,
                    StudentName = "Priya Patel",
                    Email = "priya.patel@marwadi.edu.in",
                    Course = "B.Tech Information Technology",
                    Semester = 3,
                    RegistrationDate = DateTime.Now.AddDays(-18),
                    Status = "Active"
                },
                new Student
                {
                    StudentId = 101003,
                    StudentName = "Rohan Mehta",
                    Email = "rohan.mehta@marwadi.edu.in",
                    Course = "Master of Computer Applications (MCA)",
                    Semester = 2,
                    RegistrationDate = DateTime.Now.AddDays(-10),
                    Status = "Active"
                },
                new Student
                {
                    StudentId = 101004,
                    StudentName = "Ananya Desai",
                    Email = "ananya.desai@marwadi.edu.in",
                    Course = "B.Tech Artificial Intelligence & ML",
                    Semester = 4,
                    RegistrationDate = DateTime.Now.AddDays(-5),
                    Status = "Active"
                },
                new Student
                {
                    StudentId = 101005,
                    StudentName = "Kavya Joshi",
                    Email = "kavya.joshi@marwadi.edu.in",
                    Course = "B.Tech Data Science",
                    Semester = 6,
                    RegistrationDate = DateTime.Now.AddDays(-2),
                    Status = "Active"
                }
            });
        }

        /// <summary>
        /// Retrieves all registered students.
        /// </summary>
        public static List<Student> GetAll()
        {
            lock (_lock)
            {
                // Return a copy to avoid external concurrent modification
                return _students.OrderByDescending(s => s.RegistrationDate).ToList();
            }
        }

        /// <summary>
        /// Retrieves a single student by their unique ID.
        /// </summary>
        public static Student? GetById(int id)
        {
            lock (_lock)
            {
                return _students.FirstOrDefault(s => s.StudentId == id);
            }
        }

        /// <summary>
        /// Checks if a student with the given ID already exists.
        /// </summary>
        public static bool Exists(int id)
        {
            lock (_lock)
            {
                return _students.Any(s => s.StudentId == id);
            }
        }

        /// <summary>
        /// Adds a new student to the in-memory repository.
        /// </summary>
        public static bool Add(Student student)
        {
            lock (_lock)
            {
                if (student.StudentId.HasValue && Exists(student.StudentId.Value))
                {
                    return false; // Duplicate ID found
                }

                if (student.RegistrationDate == default)
                {
                    student.RegistrationDate = DateTime.Now;
                }

                _students.Add(student);
                return true;
            }
        }

        /// <summary>
        /// Removes a student by ID.
        /// </summary>
        public static bool Delete(int id)
        {
            lock (_lock)
            {
                var student = _students.FirstOrDefault(s => s.StudentId == id);
                if (student != null)
                {
                    _students.Remove(student);
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Gets available course options for dropdowns.
        /// </summary>
        public static List<string> GetAvailableCourses()
        {
            return new List<string>
            {
                "B.Tech Computer Engineering",
                "B.Tech Information Technology",
                "B.Tech Artificial Intelligence & ML",
                "B.Tech Data Science",
                "Master of Computer Applications (MCA)",
                "Bachelor of Computer Applications (BCA)",
                "B.Tech Mechanical Engineering",
                "B.Tech Civil Engineering"
            };
        }
    }
}

