namespace BlazorAppSolution.UI.Model;

public class Course
{
    public int? CourseId { get; set; } // Optional if you're not posting this when creating
    public string? Name { get; set; }
    public string? ImageName { get; set; }
    public int? Duration { get; set; }
    public string? Description { get; set; }
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }

    // Add a method to calculate total enrollments for a course
    public int GetTotalEnrollments(List<Enrollment> enrollments)
    {
        return enrollments.Count(e => e.courseId == this.CourseId);
    }
}
