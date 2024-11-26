using ProjectManagerApi.Domain.Enums;

namespace ProjectManagerApi.Domain.Models.DataModels;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public PositionEnum Cargo { get; set; }
    public virtual ICollection<Projeto> Projetos { get; set; }
    public virtual ICollection<Comentario> Comentarios { get; set; }
}