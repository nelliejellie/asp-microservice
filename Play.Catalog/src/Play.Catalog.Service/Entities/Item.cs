using System;


namespace Play.Catalog.Service.Entities
{
    public class Item : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
        public DateTimeOffset CreatedDate {get; set;}
    }
}