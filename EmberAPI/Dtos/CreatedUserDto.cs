namespace EmberAPI.Dtos;

public class CreatedUserDto
{
    public int CreatedUserId { get; set; }
    public string userName { get; set; }
    public string userPassword { get; set; }
    public int? DBRolesId { get; set; }
}