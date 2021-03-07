using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P02_FootballBetting.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P02_FootballBetting.Data.Configurations
{
    class PlayerStatisticConfiguration : IEntityTypeConfiguration<PlayerStatistic>
    {
        public void Configure(EntityTypeBuilder<PlayerStatistic> builder)
        {
            builder.HasKey(x => new { x.GameId, x.PlayerId });
        }
    }
}
