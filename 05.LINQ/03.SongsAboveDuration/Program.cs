using Microsoft.EntityFrameworkCore;
using MusicHub.Data;
using System;
using System.Linq;
using System.Text;

namespace MusicHub
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var context = new MusicHubDbContext();
            MusicHubDbInitializer.ResetDatabase(context);

            Console.WriteLine(ExportSongsAboveDuration(context, int.Parse(Console.ReadLine())));
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            var songs = context.Songs
                .Include(x => x.SongPerformers)
                .ThenInclude(x => x.Performer)
                .Include(x => x.Writer)
                .Include(x => x.Album)
                .ThenInclude(x => x.Producer)
                .Select(x => new
                {
                    SongName = x.Name,
                    SongPerformer = x.SongPerformers
                        .Select(x => x.Performer.FirstName + " " + x.Performer.LastName)
                        .FirstOrDefault(),
                    WriterName = x.Writer.Name,
                    AlbumProducer = x.Album.Producer.Name,
                    SongDuration = x.Duration,
                })
                .Where(x => x.SongDuration > new TimeSpan(0, 0, duration))
                .OrderBy(x => x.SongName)
                .ThenBy(x => x.WriterName)
                .ThenBy(x => x.SongPerformer)
                .ToList();

            StringBuilder sb = new StringBuilder();
            int i = 1;
            foreach (var song in songs)
            {
                sb.AppendLine($"-Song #{i}");
                sb.AppendLine($"---SongName: {song.SongName}");
                sb.AppendLine($"---Writer: {song.WriterName}");
                sb.AppendLine($"---Performer: {song.SongPerformer}");
                sb.AppendLine($"---AlbumProducer: {song.AlbumProducer}");
                sb.AppendLine($"---Duration: {song.SongDuration:c}");
                i++;
            }
            return sb.ToString().Trim();
        }
    }
}
