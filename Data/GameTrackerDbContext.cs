using GameTracker.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static GameTracker.Data.DataConstants.UserConstants;

namespace GameTracker.Data
{
    public class GameTrackerDbContext : IdentityDbContext<User>
    {
        public GameTrackerDbContext(DbContextOptions<GameTrackerDbContext> options)
            : base(options)
        {
            base.Database.Migrate();
        }
        public DbSet<BoardGame> boardGames { get; set; }
        public DbSet<BoardGameType> boardGameTypes { get; set; }
        public DbSet<BookGame> bookGames { get; set; }
        public DbSet<BookGameType> bookGameTypes { get; set; }
        public DbSet<ComputerGame> computerGames { get; set; }
        public DbSet<ComputerGameType> computerGameTypes { get; set; }

        public DbSet<UserFavoriteBookGames> userFavoriteBookGames{ get; set; }
        public DbSet<UserFavoriteComputerGames> userFavoriteComputerGames { get; set; }
        public DbSet<UserFavoriteBoardGames> userFavoriteBoardGames { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {


            builder.Entity<UserFavoriteBoardGames>()
                  .HasKey(k => new { k.UserId, k.BoardGameId});
            builder.Entity<UserFavoriteBookGames>()
                  .HasKey(k => new { k.UserId, k.BookGameId });
            builder.Entity<UserFavoriteComputerGames>()
                 .HasKey(k => new { k.UserId, k.ComputerGameId });

            builder
            .Entity<BoardGameType>()
               .HasData(new BoardGameType()
               {
                   Id = 1,
                   Name = "Engine Building"
               },
               new BoardGameType()
               {
                   Id = 2,
                   Name = "Deck Building"
               },
               new BoardGameType()
               {
                   Id = 3,
                   Name = "Worker Placement"
               },
               new BoardGameType()
               {
                   Id = 4,
                   Name = "Co-Op"
               },
               new BoardGameType()
               {
                   Id = 5,
                   Name = "RPG"
               }, 
               new BoardGameType()
               {
                   Id = 6,
                   Name = "Comfort"
               }, 
               new BoardGameType()
               {
                   Id = 7,
                   Name = "Social Deduction"
               });



            builder
            .Entity<BookGameType>()
               .HasData(new BookGameType()
               {
                   Id = 1,
                   Name = "Mainstream fiction"
               },
               new BookGameType()
               {
                   Id = 2,
                   Name = "Education"
               },
               new BookGameType()
               {
                   Id = 3,
                   Name = "Erotica"
               },
               new BookGameType()
               {
                   Id = 4,
                   Name = "Role-playing solitaire adventures"
               },
               new BookGameType()
               {
                   Id = 5,
                   Name = "Adventures"
               });



            builder
           .Entity<ComputerGameType>()
              .HasData(new ComputerGameType()
              {
                  Id = 1,
                  Name = "Sandbox"
              },
              new ComputerGameType()
              {
                  Id = 2,
                  Name = "Real-time strategy"
              },
              new ComputerGameType()
              {
                  Id = 3,
                  Name = "Shooters"
              },
              new ComputerGameType()
              {
                  Id = 4,
                  Name = "Multiplayer online battle arena"
              },
              new ComputerGameType()
              {
                  Id = 5,
                  Name = "Role-playing"
              });




            builder.Entity<User>()
       .Property(p => p.UserName)
       .HasMaxLength(UsernameMaxLength)
       .IsRequired();

            builder.Entity<User>()
             .Property(p => p.Email)
             .HasMaxLength(EmailMaxLength)
             .IsRequired();

            base.OnModelCreating(builder);
        }
    }
}