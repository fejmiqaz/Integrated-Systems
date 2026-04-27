namespace CoursesApplication.Web.Response;

public record ExamSlotResponse(Guid Id, DateTime ScheduledAt, string Location, int Capacity, Guid CourseId, Guid SemesterId);
