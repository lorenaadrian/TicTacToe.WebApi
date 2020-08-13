using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TicTacToeCore.Properties;

namespace TicTacToeCore.Models
{
    public static class MovementGameTicTacToe
    {
        //Esses valores poderiam ser parametrizáveis para possibilidade um jogo com tabuleiro de outras dimensões
        //Mas para simplificação estão definidos aqui.
        private static int minPositionValue = 0, maxPositionValue = 2, maxPlayed = 15, positionCurrentPlayer = 3;
        private static readonly string _basePathFileGame = AppDomain.CurrentDomain.BaseDirectory;

        #region Public Methods
        /// <summary>
        /// Registra o movimento do jogador no tabuleiro
        /// </summary>
        /// <param name="boardPosition">Representa uma posição para movimentação no tabuleiro</param>
        /// <param name="player">Jogador que realizará a movimentação no jogo</param>
        /// <returns>Quando é uma movimentação válida, retorna positivo</returns>
        public static ActionResulting MovimentGame(Position boardPosition, Players player, string Id)
        {
            //Flag para sinalizar que se trata da última jogada da partida
            bool isLastMove = false, statusAction = false;

            string filePath = String.Format(@"{0}\{1}.txt", _basePathFileGame, Id);
            string messageReturn = "", gameBoard = "", newLineBoard = "";
            string[] lineXGame, columnYGame;

            try
            {
                using (StreamReader streamFileGame = new StreamReader(filePath))
                {
                    gameBoard = streamFileGame.ReadLine();
                }

                /* Layout do Tabuleiro:
                 * __,__,__|__,__,__|__,__,__|(X/O)
                 * Onde: 
                 * __,__,__| => linha do tabuleiro
                 * __        => posição (x,y) do tabuleiro
                 * X/O       => jogador do próximo turno
                 */
                lineXGame = gameBoard.Split("|");

                // Verifico se é o turno do jogador
                if (player.ToString() == lineXGame[positionCurrentPlayer])
                {
                    //Verifico se a movimentação informada é válida
                    if (!(boardPosition.x < minPositionValue || boardPosition.x > maxPositionValue
                        || boardPosition.y < minPositionValue || boardPosition.y > maxPositionValue))
                    {
                        //Recupero a linha do tabuleiro para incluir a movimentação do turno
                        columnYGame = lineXGame[boardPosition.x].Split(",");

                        //Verifico se a posição está disponível para a movimentação
                        if (String.IsNullOrEmpty(columnYGame[boardPosition.y]))
                        {
                            //Incluo a movimentação
                            columnYGame[boardPosition.y] = player.ToString();

                            //Remonto a linha modificada
                            newLineBoard = ReassembleGame(columnYGame, ",");

                            //Remover última vírgula
                            lineXGame[boardPosition.x] = newLineBoard.Substring(0, newLineBoard.Length - 1);

                            //Remonto o tabuleiro para gravar no arquivo
                            gameBoard = ReassembleGame(lineXGame, "|");

                            //Verifico se é a última jogada
                            int sumLineLength = 0;
                            for (int i = 0; i < lineXGame.Length - 1; i++)
                                sumLineLength += lineXGame[i].Length;

                            //Defino flag para sinalizar se é a última movimentação
                            isLastMove = (sumLineLength == maxPlayed);

                            //Se não é a última jogada, salvo o turno atual
                            if (!isLastMove)
                                gameBoard += GetNextPlayer(player).ToString();
                            else
                                messageReturn = Resources.FINISHED_GAMER_MESSAGE;
                            //Gravo o novo estado do tabuleiro
                            WriteFileGame(filePath, gameBoard);

                            //Removo o proximo jogador para verificar se há vencedor no turno atual
                            gameBoard = gameBoard.Substring(0, gameBoard.Length - 2);

                            //Verifico o tabuleiro em busca de um vencendor
                            if (CheckBoard(gameBoard))
                                return WriteActionResulting(true, Resources.FINISHED_GAMER_MESSAGE, player.ToString());
                            else
                                statusAction = true;
                        }
                        else //A posição não está disponível para a movimentação
                            messageReturn = Resources.INVALID_PLAY_MESSAGE;
                    }
                    else //Movimentação inválida
                        messageReturn = Resources.INVALID_POSITION_MOVEMENT;
                }
                else //Não é o turno do jogador
                    messageReturn = Resources.PLAYER_TURN_VALIDATION_MESSAGE;
            }
            catch (FileNotFoundException)
            {
                return WriteActionResulting(false, Resources.IO_NOT_FOUNT_FILE_MESSAGE, null);
            }
            catch (Exception ex)
            {
                return WriteActionResulting(false,
                    String.Format("{0} - {1}", Resources.GENERAL_ERROR_MESSAGE, ex.Message), null);
            }
            return WriteActionResulting(statusAction, messageReturn, null);
        }

        /// <summary>
        /// Escreve uma situação na partida.
        /// </summary>
        /// <param name="statusACtion">Quando a situação é posivita para o que se espera, retornará verdadeiro</param>
        /// <param name="messageAction">Espaço para comunicação entre sistema e interface sobre algo na situação corrente</param>
        /// <returns>Retorna a situação atual da ação do jogo</returns>
        public static ActionResulting WriteActionResulting(bool statusACtion, string messageAction, string winner)
        {
            return new ActionResulting
            {
                StatusAction = statusACtion,
                MessageAction = messageAction,
                Winner = winner == null ? "Draw" : winner
            };
        }

        /// <summary>
        /// Define jogador do próximo turno
        /// </summary>
        /// <param name="actualPlayer">Jogador do turno atual</param>
        /// <returns>Próximo jogador</returns>
        public static Players GetNextPlayer(Players actualPlayer)
        {
            if ((int)actualPlayer == 0)
                return Players.O;
            else
                return Players.X;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Verifica se há linha vencedora
        /// </summary>
        /// <param name="lineBoard">Linha verificada</param>
        /// <returns>Caso haja vitória pela linha retorna positivo</returns>
        private static bool CheckWinningrBoard(string lineBoard)
        {
            try
            {
                string[] columnBoard = lineBoard.Split(",");
                string lastSeenMove = null;
                int cont = 0;
                for (int i = minPositionValue; i <= maxPositionValue; i++)
                {
                    if (String.IsNullOrEmpty(lastSeenMove))
                        lastSeenMove = columnBoard[i];
                    if (!String.IsNullOrEmpty(lastSeenMove) && lastSeenMove != "" && columnBoard[i] == lastSeenMove)
                        cont++;
                }
                return (cont == 3) ? true : false;
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }

        /// <summary>
        /// Verifica tabuleiro em busca de um vencedor
        /// </summary>
        /// <param name="lineXGame"></param>
        /// <returns></returns>
        private static bool CheckBoard(string gameBoard)
        {
            string[] lineXGame, auxLine;
            string column1Aux = "", column2Aux = "", column3Aux = "", diag1Aux = "", diag2Aux = "";
            lineXGame = gameBoard.Split("|");

            //Verifico se há linha vencedora
            //Monto colunas e verifico se há vencendor
            //Monto as diagonais
            for (int i = minPositionValue; i <= maxPositionValue; i++)
            {
                auxLine = lineXGame[i].Split(",");
                if (CheckWinningrBoard(lineXGame[i]))
                    return true;
                column1Aux += auxLine[0] + ","; //Número mágicos, precisamos mudar isso...
                column2Aux += auxLine[1] + ",";
                column3Aux += auxLine[2] + ",";
                diag1Aux += auxLine[i] + ",";
                diag2Aux += auxLine[maxPositionValue - i] + ",";
            }

            if (CheckWinningrBoard(column1Aux.Substring(0, column1Aux.Length - 1)))
                return true;

            if (CheckWinningrBoard(column2Aux.Substring(0, column2Aux.Length - 1)))
                return true;

            if (CheckWinningrBoard(column3Aux.Substring(0, column3Aux.Length - 1)))
                return true;
            if (CheckWinningrBoard(diag1Aux.Substring(0, diag1Aux.Length - 1)))
                return true;

            if (CheckWinningrBoard(diag2Aux.Substring(0, diag2Aux.Length - 1)))
                return true;

            return false;
        }

        /// <summary>
        /// Cria um novo arquivo de jogo para salvar as informações do tabuleiro
        /// e próximo jogador válido para movimentação.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="lineGame"></param>  
        private static void WriteFileGame(string filePath, string lineGame)
        {
            try
            {
                System.IO.File.WriteAllText(filePath, lineGame);
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Responsável por montar novamente a estrutura do tabuleiro
        /// </summary>
        /// <param name="vectorGame">Vetor a ser percorrido</param>
        /// <param name="separatorSymbol">Símbolo separador a ser utilizado</param>
        /// <returns></returns>
        private static string ReassembleGame(string[] vectorGame, string separatorSymbol)
        {
            string newLineBoard = "";
            //Remonto a linha modificada
            for (int i = minPositionValue; i <= maxPositionValue; i++)
            {
                if (i == minPositionValue)
                    newLineBoard = String.Format("{0}{1}", vectorGame[i], separatorSymbol);
                else
                    newLineBoard += String.Format("{0}{1}", vectorGame[i], separatorSymbol);
            }

            return newLineBoard;

        }

        #endregion

    }
}
