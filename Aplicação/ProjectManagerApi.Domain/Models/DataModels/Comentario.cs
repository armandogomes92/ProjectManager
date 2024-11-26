namespace ProjectManagerApi.Domain.Models.DataModels;

public class Comentario
{
    public int Id { get; set; }
    public string TextoComentario { get; set; }
    public DateTime DataCriacao { get; set; }
    public int TarefaId { get; set; }
    public virtual Tarefa Task { get; set; }
    public int UsuarioId { get; set; }
    public virtual Usuario Usuario { get; set; }
}