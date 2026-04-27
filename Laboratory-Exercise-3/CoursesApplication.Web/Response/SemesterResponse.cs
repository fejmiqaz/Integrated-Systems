namespace CoursesApplication.Web.Response;

public record SemesterResponse(Guid Id, string Name, DateTime StartDate, DateTime EndDate);
