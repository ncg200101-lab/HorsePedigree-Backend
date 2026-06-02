namespace HorsePedigree_2026.Entities;

public class Rol
{
    public long RolId { get; set; }
    public string Descripcion { get; set; } = string.Empty;

    public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
