using ProjectManagerApi.Domain.Enums;

namespace ProjectManagerApi.Domain.Models.DataModels;

public class Tarefa
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public DateTime PrazoConclusao { get; set; }
    public DateTime UltimaAtualizacao { get; set; }
    public StatusEnum Status { get; set; }
    public PriorityEnum Prioridade { get; set; }
    public int ProjetoId { get; set; }
    public virtual Projeto Projeto { get; set; }
    public virtual ICollection<Comentario>? comentarios { get; set; }
    public virtual ICollection<HistoricoDaTarefa>? HistoricoDaTarefa { get; set; }
}