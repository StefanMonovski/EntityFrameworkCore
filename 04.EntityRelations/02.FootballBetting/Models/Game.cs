﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P02_FootballBetting.Data.Models
{
    public class Game
    {
        public Game()
        {
            PlayerStatistics = new HashSet<PlayerStatistic>();
            Bets = new HashSet<Bet>();
        }
         
        [Key]
        public int GameId { get; set; }

        public int HomeTeamId { get; set; }

        public Team HomeTeam { get; set; }

        public int AwayTeamId { get; set; }

        public Team AwayTeam { get; set; }

        public int HomeTeamGoals { get; set; }

        public int AwayTeamGoals { get; set; }

        public DateTime DateTime { get; set; }

        [Column(TypeName = "DECIMAL(4,2)")]
        public decimal HomeTeamBetRate { get; set; }

        [Column(TypeName = "DECIMAL(4,2)")]
        public decimal AwayTeamBetRate { get; set; }

        [Column(TypeName = "DECIMAL(4,2)")]
        public decimal DrawBetRate { get; set; }

        [Required]
        [MaxLength(30)]
        public string Result { get; set; }

        public ICollection<PlayerStatistic> PlayerStatistics { get; set; }

        public ICollection<Bet> Bets { get; set; }
    }
}