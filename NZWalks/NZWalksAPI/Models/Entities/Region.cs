﻿namespace NZWalksAPI.Models.Entities
{
    public class Region
    {
        public Guid Id { get; set; }

        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string? RegionImageUrl { get; set; }
    }
}
