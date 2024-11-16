using System.ComponentModel.DataAnnotations;

namespace Products.Domain.DTOS.ProductGroups.Request;

public record GroupDto
{
    [MaxLength(100)] public string Label { get; set; } = string.Empty;
}