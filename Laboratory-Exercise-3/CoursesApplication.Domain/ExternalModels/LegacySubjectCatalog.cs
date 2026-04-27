namespace CoursesApplication.Domain.ExternalModels;

public class LegacySubjectCatalog
{
    public int SubjectCode { get; set; }
    public string SubjectTitle { get; set; }
    public string SubjectSummary { get; set; }
    public int CreditUnits { get; set; }
    public string Discipline { get; set; }
    public int PeriodId { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateLastModified { get; set; }
}