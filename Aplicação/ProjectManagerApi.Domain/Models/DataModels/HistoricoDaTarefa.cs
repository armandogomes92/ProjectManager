
namespace ProjectManagerApi.Domain.Models.DataModels;

public class
    HistoricoDaTarefa
{
    public int Id { get; set; }
    public string ItemModificado { get; set; }
    public string ValoresAntigos { get; set; }
    public string ValoresNovos { get; set; }
    public DateTime DataModificacao { get; set; }
    public int TaskId { get; set; }
    public virtual Tarefa Task { get; set; }
}