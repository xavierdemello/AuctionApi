﻿using System.Text.Json.Serialization;

namespace AuctionApi.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string DocumentNumber { get; set; }

        public string Phone { get; set; }

        public UserStatus StatusId { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }
}
