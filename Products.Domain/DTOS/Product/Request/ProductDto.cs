﻿using System.ComponentModel.DataAnnotations;

namespace Products.Domain.DTOS.Product.Request;

public record ProductDto
{
    [Required, StringLength(maximumLength: 8, MinimumLength = 2)]
    public string? Code { get; set; }
    [Required, StringLength(maximumLength: 100, MinimumLength = 2)]
    public string? Name { get; set; }
    [Required, Range(0.01, float.MaxValue)]
    public double Price { get; set; }
    public int Quantity { get; set; }
    [StringLength(maximumLength: 350)]
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}