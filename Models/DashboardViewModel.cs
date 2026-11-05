using System.Collections.Generic;

namespace StudentCourseRegistration.Models
{
    /// <summary>
    /// ViewModel for presenting dashboard summary metrics and recent activity on the Home Page.
    /// </summary>
    public class DashboardViewModel
    {
        public int TotalStudents { get; set; }
        public int TotalCoursesAvailable { get; set; }
        public int ActiveSemestersCount { get; set; }
        public List<Student> RecentStudents { get; set; } = new List<Student>();
        public Dictionary<string, int> StudentsByCourse { get; set; } = new Dictionary<string, int>();
    }
}

