namespace Tests
{
    using GameTracker.Controllers;
    using GameTracker.Data;
    using GameTracker.Models.Games.BoardGameModels;
    using GameTracker.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework.Internal;

    public class ControllerTests
    {
        [Test]
        public void AllBoardGames()
        {
            BoardGamesController boardGamesController = new BoardGamesController(null);
            var result = boardGamesController.AllBoardGames;
            Assert.NotNull(result);

        }
        [Test]
        public void AddBoardGamePost()
        {
            BoardGamesController boardGamesController = new BoardGamesController(null);
            var result  = boardGamesController.AddBoardGame(null);
            Assert.NotNull(result);
        }
        [Test]
        public void AddBoardGamePostInvalidModelState()
        {
            BoardGameFormModel boardGameFormModel = new BoardGameFormModel();
            BoardGamesController boardGamesController = new BoardGamesController(null);
            var result = boardGamesController.AddBoardGame(boardGameFormModel);
            Assert.NotNull(result);
        }
        [Test]
        public void AddBoardGameGet()
        {
            BoardGamesController boardGamesController = new BoardGamesController(null);
            var result = boardGamesController.AddBoardGame();
            Assert.NotNull(result);
        }
        [Test]
        public void AddToCollectionBoard()
        {
            BoardGamesController boardGamesController = new BoardGamesController(null);
            var result = boardGamesController.AddToCollection(0);
            Assert.NotNull(result);
        }
        [Test]
        public void FavoriteBoardGames()
        {
            BoardGamesController boardGamesController = new BoardGamesController(null);
            var result = boardGamesController.FavoriteBoardGames;
            Assert.NotNull(result);
        }
        [Test]
        public void RemoveFromCollectionBoard()
        {
            BoardGamesController boardGamesController = new BoardGamesController(null);
            var result = boardGamesController.RemoveFromCollection;
            Assert.NotNull(result); 
        }






        [Test]
        public void AllBookGames()
        {
            BookGamesController bo��GamesController = new BookGamesController(null);
            var result = bo��GamesController.AllBookGames;
            Assert.NotNull(result);
        }
        [Test]
        public void AddBookGameGet()
        {
            BookGamesController bo��GamesController = new BookGamesController(null);
            var result = bo��GamesController.AddBookGame();
            Assert.NotNull(result);
        }
        [Test]
        public void AddBookGamePost()
        {
            BookGamesController bo��GamesController = new BookGamesController(null);
            var result = bo��GamesController.AddBookGame(null);
            Assert.NotNull(result);
        }
        [Test]
        public void AddToCollectionBook()
        {
            BookGamesController bo��GamesController = new BookGamesController(null);
            var result = bo��GamesController.AddToCollection;
            Assert.NotNull(result);
        }
        [Test]
        public void FavoriteBookGames()
        {
            BookGamesController bo��GamesController = new BookGamesController(null);
            var result = bo��GamesController.FavoriteBookGames;
            Assert.NotNull(result);
        }
        [Test]
        public void RemoveFromCollectionBook()
        {
            BookGamesController bo��GamesController = new BookGamesController(null);
            var result = bo��GamesController.RemoveFromCollection(0);
            Assert.NotNull(result);
        }




        [Test]
        public void AllComputerGames()
        {
            ComputerGamesController computerGamesController = new ComputerGamesController(null);
            var result = computerGamesController.AllComputerGames;
            Assert.NotNull(result);
        }
        [Test]
        public void AddComputerGamePost()
        {
            ComputerGamesController computerGamesController = new ComputerGamesController(null);
            var result = computerGamesController.AddComputerGame(null);
            Assert.NotNull(result);
        }
        [Test]
        public void AddComputerGameGet()
        {
            ComputerGamesController computerGamesController = new ComputerGamesController(null);
            var result = computerGamesController.AddComputerGame();
            Assert.NotNull(result);
        }
        [Test]
        public void AddToCollectionComputer()
        {
            ComputerGamesController computerGamesController = new ComputerGamesController(null);
            var result = computerGamesController.AddToCollection(0);
            Assert.NotNull(result);
        }
        [Test]
        public void FavoriteComputerGames()
        {
            ComputerGamesController computerGamesController = new ComputerGamesController(null);
            var result = computerGamesController.FavoriteComputerGames;
            Assert.NotNull(result);
        }
        [Test]
        public void RemoveFromCollectionComputer()
        {
            ComputerGamesController computerGamesController = new ComputerGamesController(null);
            var result = computerGamesController.RemoveFromCollection(0);
            Assert.NotNull(result);
        }
    }
}