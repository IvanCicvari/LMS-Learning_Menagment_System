namespace LMS_Learning_Menagment_System_API.DTO
{
    public class GetUserModel
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? CountryName { get; set; } 

        public string? CityName { get; set; } 

        public string? RoleName { get; set; }

        public string? ClassName { get; set; }

        public List<ExamDto> Exams { get; set; } = new List<ExamDto>();

    }
}
