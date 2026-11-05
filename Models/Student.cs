using System;
using System.ComponentModel.DataAnnotations;

namespace StudentCourseRegistration.Models
{
    /// <summary>
    /// Represents a student enrolled in a course.
    /// Uses Data Annotations for validation rules and UI display labels.
    /// </summary>
    public class Student
    {
        [Required(ErrorMessage = "Student ID is required.")]
        [Range(1000, 999999, ErrorMessage = "Student ID must be a valid number between 1000 and 999999.")]
        [Display(Name = "Student ID")]
        public int? StudentId { get; set; }

        [Required(ErrorMessage = "Student Name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Student Name must be between 3 and 50 characters.")]
        [Display(Name = "Student Name")]
        public string StudentName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address (e.g. student@marwadi.edu.in).")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a Course.")]
        [StringLength(50, ErrorMessage = "Course name cannot exceed 50 characters.")]
        [Display(Name = "Course / Program")]
        public string Course { get; set; } = string.Empty;

        [Required(ErrorMessage = "Semester is required.")]
        [Range(1, 8, ErrorMessage = "Semester must be between 1 and 8.")]
        [Display(Name = "Semester")]
        public int? Semester { get; set; }

        [Display(Name = "Registration Date")]
        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        [Display(Name = "Academic Status")]
        public string Status { get; set; } = "Active";
    }
}

