﻿namespace Products.Domain.DTOS;

public record Result
{
    public bool Success { get; set; }
    public List<string> Errors { get; set; } = [];
}