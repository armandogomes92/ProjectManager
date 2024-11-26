namespace ProjectManagerApi.Domain.Models.DataModels;

public class Projeto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public bool Active { get; set; }
    public int UserId { get; set; }
    public Usuario User { get; set; }
    public virtual ICollection<Tarefa> Tasks { get; set; }
}