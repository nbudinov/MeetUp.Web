﻿namespace MeetUp.Data.Models
{
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Deleted { get; set; } = 0;
    }
}
