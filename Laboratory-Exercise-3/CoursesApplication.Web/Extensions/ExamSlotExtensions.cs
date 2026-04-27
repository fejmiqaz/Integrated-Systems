using CoursesApplication.Domain.Models;
using CoursesApplication.Web.Response;

namespace CoursesApplication.Web.Extensions;

public static class ExamSlotExtensions
{
    public static ExamSlotResponse ToResponse(this ExamSlot examSlot) =>
        new(examSlot.Id, examSlot.ScheduledAt, examSlot.Location, examSlot.Capacity, examSlot.CourseId, examSlot.SemesterId);
}
