namespace API.Entities;

//Modela nuestra entidad de usuario en la base de datos
public class AppUser
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string DisplayName { get; set; }
    public required string Email { get; set; }
    public string? ImageUrl { get; set; }
    //Entender los principios de autenticacion
    public required byte[] PasswordHash { get; set; } //Contrasenia que guarde el usuario
    public required byte[] PasswordSalt { get; set; } //Variacion de la contrasenia y se encripta de manera diferente

    // Navigation properties
    public Member Member { get; set; } = null!;
}
