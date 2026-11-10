namespace RynekIT.Domain.Users;

public class Cv
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public string FileName { get; set; } = null!;
    public DateTime UploadedAt { get; set; }

    public ICollection<CvSkill> CvSkills { get; set; } = new List<CvSkill>();
}
