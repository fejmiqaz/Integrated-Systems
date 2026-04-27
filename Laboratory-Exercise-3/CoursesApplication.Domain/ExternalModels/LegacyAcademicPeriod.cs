namespace CoursesApplication.Domain.ExternalModels;

public class LegacyAcademicPeriod
{
    public int PeriodId { get; set; }
    public string PeriodLabel { get; set; }
    public DateTime StartingDate { get; set; }
    public DateTime ClosingDate { get; set; }
    public bool IsActive { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateLastModified { get; set; }
    
}