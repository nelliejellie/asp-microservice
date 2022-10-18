using System;
using System.ComponentModel.DataAnnotations;

namespace Play.Catalog.Service.Dtos
{
    public record ItemDto(Guid id, [Required]string name, string description, [Range(1,1000)]decimal price, DateTimeOffset CreatedAt);

    public record CreateItemDto([Required]string name, string description, [Range(1,1000)]decimal price);

    public record UpdateItemDto([Required]string name, string Description, [Range(1,1000)]decimal price);
}