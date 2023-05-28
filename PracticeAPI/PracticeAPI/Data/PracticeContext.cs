using Microsoft.EntityFrameworkCore;
using PracticeAPI.Models;

namespace PracticeAPI.Data
{
    public class PracticeContext : DbContext
    {
        public DbSet<GameAccount> Accounts { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Quest> Quests { get; set; }
        public DbSet<GameAccountCharacter> GameAccountCharacters { get; set; }
        public DbSet<GameAccountQuest> GameAccountQuests { get; set; }

        private readonly IConfiguration _configuration;

        public PracticeContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("sqlConStr"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameAccountCharacter>()
                .HasKey(gac => new { gac.GameAccountId, gac.CharacterId });

            modelBuilder.Entity<GameAccountCharacter>()
                .HasOne(gac => gac.GameAccount)
                .WithMany(ga => ga.Characters)
                .HasForeignKey(gac => gac.GameAccountId);

            modelBuilder.Entity<GameAccountCharacter>()
                .HasOne(gac => gac.Character)
                .WithMany(c => c.GameAccounts)
                .HasForeignKey(gac => gac.CharacterId);

            modelBuilder.Entity<GameAccountQuest>()
                .HasKey(gaq => new { gaq.GameAccountId, gaq.QuestId });

            modelBuilder.Entity<GameAccountQuest>()
                .HasOne(gaq => gaq.GameAccount)
                .WithMany(ga => ga.Quests)
                .HasForeignKey(gaq => gaq.GameAccountId);

            modelBuilder.Entity<GameAccountQuest>()
                .HasOne(gaq => gaq.Quest)
                .WithMany(q => q.GameAccounts)
                .HasForeignKey(gaq => gaq.QuestId);
        }
    }
}
