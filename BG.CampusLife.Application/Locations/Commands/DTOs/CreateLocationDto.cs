﻿using System;

namespace BG.CampusLife.Application.Locations.Commands.DTOs
{
    public class CreateLocationDto
    {
        public Guid Id { get; set; }
        
        public string FullAddress { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }
    }
}
