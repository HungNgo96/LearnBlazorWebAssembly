﻿namespace ApplicationClient.Responses
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Supplier { get; set; }
        public double Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}