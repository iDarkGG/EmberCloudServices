namespace EmberAPI.Dtos;

public class CreatedUserPOSTDto
{
    public string userNameHash { get; set; }
    public string userPasswordHash { get; set; }
    public int? DBRolesId { get; set; }
    public int? InstanceID { get; set; }
}