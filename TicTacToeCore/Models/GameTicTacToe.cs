using System;
using System.Diagnostics.Tracing;
using System.IO;
using System.Xml;
using TicTacToeCore.Properties;

namespace TicTacToeCore.Models
{
    public class GameTicTacToe
    {
        //Esses valores poderiam ser parametrizáveis para possibilidade um jogo com tabuleiro de outras dimensões
        //Mas para simplificação estão definidos aqui.
        private int minPositionValue = 0, maxPositionValue = 2, maxPlayed = 15, positionCurrentPlayer = 3;
        private string _basePathFileGame = System.AppDomain.CurrentDomain.BaseDirectory;

        #region Propriedades

        public Guid Id { get; set; }
        public Players Player;

        #endregion

        #region Métodos Públicos

        public GameTicTacToe()
        { }

        /// <summary>
        /// Inicia um novo jogo:
        ///   1. Gera um novo GUID;
        ///   2. Sorteia o primeiro jogador;
        ///   3. Registra o jogo para futuras movimentações.
        /// </summary>
        /// <returns>Em caso de sucesso, retornará positivo.</returns>
        public ActionResulting StartGame()
        {
            ActionResulting actionResulting = new ActionResulting();
            string localPath;
            try
            {
                Id = Guid.NewGuid();
                Player = SortFirstPlayer();
                localPath = String.Format(@"{0}\{1}.txt", _basePathFileGame, Id);
                RegisterGame(localPath);
                return MovementGameTicTacToe.WriteActionResulting(true, "", null);
            }
            catch (IOException iOEx)
            {
                return MovementGameTicTacToe.WriteActionResulting(false,
                    String.Format("{0} - {1}", Resources.IO_ERROR_MESSAGE, iOEx.Message), null);
            }
            catch (Exception ex)
            {
                return MovementGameTicTacToe.WriteActionResulting(false,
                    String.Format("{0} - {1}", Resources.GENERAL_ERROR_MESSAGE, ex.Message), null);
            }
        }

        #endregion

        #region Métodos Privados

        /// <summary>
        /// Sorteia o primeiro jogador
        /// </summary>
        /// <returns>Players: X / O </returns>
        private Players SortFirstPlayer()
        {
            Random rdm = new Random();
            return (Players)rdm.Next(0, 1);
        }

        /// <summary>
        /// Registra o jogo em arquivo texto.
        /// </summary>
        /// <param name="fileGamePath">Local indicado para gravar o arquivo.</param>
        private void RegisterGame(string fileGamePath)
        {
            if (!File.Exists(fileGamePath))
            {
                string lineGame = ",,|,,|,,|" + Player.ToString();
                System.IO.File.WriteAllText(fileGamePath, lineGame);
            }

        }
        #endregion
    }
}
