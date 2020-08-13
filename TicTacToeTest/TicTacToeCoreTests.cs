using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToeCore.Models;

namespace TicTacToeTest
{
    [TestClass]
    public class TicTacToeCoreTests
    {
        [TestMethod]
        public void StartsGameWithFirstPlayer()
        {
            //Arrange
            GameTicTacToe game = new GameTicTacToe();
            ActionResulting methodReturn;

            //Act
            methodReturn = game.StartGame();

            //Assert
            Assert.IsTrue(methodReturn.StatusAction);
        }

        [TestMethod]
        public void CheckCreatedGuid()
        {
            //Arrange
            GameTicTacToe game = new GameTicTacToe();
            ActionResulting methodReturn;

            //Act
            methodReturn = game.StartGame();

            //Assert
            if (methodReturn.StatusAction)
                Assert.IsNotNull(game.Id);
        }

        [TestMethod]
        public void CheckCreatedFirstPlayer()
        {
            //Arrange
            GameTicTacToe game = new GameTicTacToe();
            ActionResulting methodReturn;

            //Act
            methodReturn = game.StartGame();

            //Assert
            if (methodReturn.StatusAction)
                Assert.IsNotNull(game.Player);
        }

        [TestMethod]
        public void TestGameTurn()
        {
            //Arrange
            GameTicTacToe game = new GameTicTacToe();
            ActionResulting methodReturn;


            //Act
            game.StartGame();
            methodReturn = MovementGameTicTacToe.MovimentGame(new Position { x = 0, y = 2 }
                                                                , game.Player, game.Id.ToString());

            //Assert
            Assert.IsTrue(methodReturn.StatusAction);
        }

        [TestMethod]
        public void TurnErrorTest()
        {
            //Arrange
            GameTicTacToe game = new GameTicTacToe();
            ActionResulting methodReturn = new ActionResulting();
            Position positionTurn = new Position();
            int[,] turns = { { 0, 0 }, { 1, 0 } };
            int numbersTurns = 2;

            //Act
            game.StartGame();
            for (int i = 0; i < numbersTurns; i++)
            {
                positionTurn.x = turns[i, 0];
                positionTurn.y = turns[i, 1];
                methodReturn = MovementGameTicTacToe.MovimentGame(positionTurn, game.Player, game.Id.ToString());
            }

            //Assert
            Assert.IsFalse(methodReturn.StatusAction);
            Assert.AreEqual("Não é turno do jogador", methodReturn.MessageAction);

        }

        [TestMethod]
        public void GameNumberErrorTest()
        {
            //Arrange
            GameTicTacToe game = new GameTicTacToe();
            ActionResulting methodReturn = new ActionResulting();
            Position positionTurn = new Position();

            //Act
            game.StartGame();
            positionTurn.x = 1;
            positionTurn.y = 0;
            methodReturn = MovementGameTicTacToe.MovimentGame(positionTurn, game.Player, "guid-invalido-de-jogo");


            //Assert
            Assert.IsFalse(methodReturn.StatusAction);
            Assert.AreEqual("Partida não encontrada", methodReturn.MessageAction);

        }
        [TestMethod]
        public void FullGameLineWinTest()
        {
            //Arrange
            GameTicTacToe game = new GameTicTacToe();
            ActionResulting methodReturn = new ActionResulting();
            Position positionTurn = new Position();
            Players winnerPlayer = game.Player;
            int[,] turns = { { 0, 0 }, { 1, 0 }, { 0, 1 }, { 1, 1 }, { 0, 2 } };
            int numbersTurns = 5;

            //Act
            game.StartGame();
            for (int i = 0; i < numbersTurns; i++)
            {
                positionTurn.x = turns[i, 0];
                positionTurn.y = turns[i, 1];
                methodReturn = MovementGameTicTacToe.MovimentGame(positionTurn, game.Player, game.Id.ToString());
                game.Player = MovementGameTicTacToe.GetNextPlayer(game.Player);
            }

            //Assert
            Assert.IsTrue(methodReturn.StatusAction);
            Assert.AreEqual("Partida finalizada!", methodReturn.MessageAction);
            Assert.AreEqual(winnerPlayer.ToString(), methodReturn.Winner);

        }

        [TestMethod]
        public void FullGameColumnWinTest()
        {
            //Arrange
            GameTicTacToe game = new GameTicTacToe();
            ActionResulting methodReturn = new ActionResulting();
            Position positionTurn = new Position();
            int[,] turns = { { 0, 0 }, { 0, 1 }, { 0, 2 }, { 1, 1 }, { 1, 0 }, { 2, 1 } };
            int numbersTurns = 6;

            //Act
            game.StartGame();
            for (int i = 0; i < numbersTurns; i++)
            {
                positionTurn.x = turns[i, 0];
                positionTurn.y = turns[i, 1];
                methodReturn = MovementGameTicTacToe.MovimentGame(positionTurn, game.Player, game.Id.ToString());
                game.Player = MovementGameTicTacToe.GetNextPlayer(game.Player);
            }

            //Assert
            Assert.IsTrue(methodReturn.StatusAction);
            Assert.AreEqual("Partida finalizada!", methodReturn.MessageAction);
        }

        [TestMethod]
        public void FullGameDiagonal1WinTest()
        {
            //Arrange
            GameTicTacToe game = new GameTicTacToe();
            ActionResulting methodReturn = new ActionResulting();
            Position positionTurn = new Position();
            int[,] turns = { { 0, 0 }, { 0, 1 }, { 1, 1 }, { 0, 2 }, { 2, 2 } };
            int numbersTurns = 5;

            //Act
            game.StartGame();
            for (int i = 0; i < numbersTurns; i++)
            {
                positionTurn.x = turns[i, 0];
                positionTurn.y = turns[i, 1];
                methodReturn = MovementGameTicTacToe.MovimentGame(positionTurn, game.Player, game.Id.ToString());
                game.Player = MovementGameTicTacToe.GetNextPlayer(game.Player);
            }

            //Assert
            Assert.IsTrue(methodReturn.StatusAction);
            Assert.AreEqual("Partida finalizada!", methodReturn.MessageAction);
        }

        [TestMethod]
        public void FullGameDiagonal2WinTest()
        {
            //Arrange
            GameTicTacToe game = new GameTicTacToe();
            ActionResulting methodReturn = new ActionResulting();
            Position positionTurn = new Position();
            int[,] turns = { { 1, 1 }, { 1, 0 }, { 2, 0 }, { 2, 1 }, { 0, 2 } };
            int numbersTurns = 5;

            //Act
            game.StartGame();
            for (int i = 0; i < numbersTurns; i++)
            {
                positionTurn.x = turns[i, 0];
                positionTurn.y = turns[i, 1];
                methodReturn = MovementGameTicTacToe.MovimentGame(positionTurn, game.Player, game.Id.ToString());
                game.Player = MovementGameTicTacToe.GetNextPlayer(game.Player);
            }

            //Assert
            Assert.IsTrue(methodReturn.StatusAction);
            Assert.AreEqual<string>("Partida finalizada!", methodReturn.MessageAction);
        }

        [TestMethod]
        public void FullGameDrawWinTest()
        {
            //Arrange
            GameTicTacToe game = new GameTicTacToe();
            ActionResulting methodReturn = new ActionResulting();
            Position positionTurn = new Position();
            int[,] turns = { { 0, 0 }, { 1, 1 }, { 1, 0 }, { 2, 0 }, { 0, 2 }, { 0, 1 }, { 2, 1 }, { 1, 2 }, { 2, 2 } };
            int numbersTurns = 9;

            //Act
            game.StartGame();
            for (int i = 0; i < numbersTurns; i++)
            {
                positionTurn.x = turns[i, 0];
                positionTurn.y = turns[i, 1];
                methodReturn = MovementGameTicTacToe.MovimentGame(positionTurn, game.Player, game.Id.ToString());
                game.Player = MovementGameTicTacToe.GetNextPlayer(game.Player);
            }

            //Assert
            Assert.IsTrue(methodReturn.StatusAction);
            Assert.AreEqual("Partida finalizada!", methodReturn.MessageAction);
            Assert.AreEqual("Draw", methodReturn.Winner);
        }
    }
}
