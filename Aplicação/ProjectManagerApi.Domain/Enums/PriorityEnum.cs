using System.ComponentModel;

namespace ProjectManagerApi.Domain.Enums;
public enum PriorityEnum
{
    [Description("Alta")]
    High = 1,
    [Description("Média")]
    Medium = 2,
    [Description("Baixa")]
    Low = 3
}