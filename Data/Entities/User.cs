using GameTracker.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace GameTracker.Data.Entities
{
    public class User : IdentityUser
    {
        public List<UserFavoriteBoardGames> UsersFavoriteBoardGames { get; set; } = new List<UserFavoriteBoardGames>();
        public List<UserFavoriteBookGames> UsersFavoriteBookGames { get; set; } = new List<UserFavoriteBookGames>();

        public List<UserFavoriteComputerGames> UsersFavoriteComputerGames { get; set; } = new List<UserFavoriteComputerGames>();


    }
}
