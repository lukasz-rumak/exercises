﻿using System;
using System.ComponentModel.DataAnnotations;
using BoardGameApi.DataAnnotations;

namespace BoardGameApi.Models
{
    public class AddPlayer
    {
        [Required]
        public Guid SessionId { get; set; }
        [Required]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "The {0} value must be exactly {1} character")]
        [PlayerType(ErrorMessage = "The PlayerType must be 'P' or 'K'")]
        public string PlayerType { get; set; }
    }
}