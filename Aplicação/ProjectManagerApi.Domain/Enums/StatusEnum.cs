using System.ComponentModel;

namespace ProjectManagerApi.Domain.Enums;
public enum StatusEnum
{
    [Description("Pendente")]
    Pendente,
    [Description("Em andamento")]
    EmAndamento,
    [Description("Concluída")]
    Concluída
}