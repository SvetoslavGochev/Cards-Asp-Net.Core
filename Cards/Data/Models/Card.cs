﻿namespace Cards.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class Card
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Keyword { get; set; }

        [Range(1, 1000)]
        public int Attack { get; set; }

        [Range(0, 1000)]
        public int Health { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public ICollection<UserCard> Users { get; init; } = new HashSet<UserCard>();
    }
}
