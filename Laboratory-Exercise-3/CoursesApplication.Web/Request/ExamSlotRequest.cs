namespace CoursesApplication.Web.Request;

public record ExamSlotRequest(DateTime ScheduledAt, string Location, int Capacity, Guid CourseId, Guid SemesterId);
