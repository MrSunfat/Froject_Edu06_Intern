namespace MISA_Demo.Model
{
    public class Teacher
    {
        public Guid TeacherID { get; set; }
        public string TeacherCode { get; set; }
        public string FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? SubjectGroup { get; set; }
        public string? SubjectName { get; set; }
        public Boolean? IsProfessionalQualifications { get; set; }
        public Boolean? IsWorking { get; set; }
    }
}
