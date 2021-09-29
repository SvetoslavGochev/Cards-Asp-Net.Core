﻿namespace Cards.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserCard
    {

        public string UserId { get; set; }
        public User User { get; set; }


        public int CardId { get; set; }
        public Card Card { get; set; }

    }
}
