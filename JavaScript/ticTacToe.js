import * as ticTacToeExtentions from './ticTacToeExtensions.js';
import promptSync from 'prompt-sync';
const prompt = promptSync();

const playTicTacToe = (boardSize) => {
    const errorMsg = ticTacToeExtentions.errorMsg()
    if (boardSize < 3) {
        return console.log(errorMsg.error)
    }
    let gameModel = ticTacToeExtentions.createGameModel(boardSize)
    let player = "X"
    let boardFromGameModel = ticTacToeExtentions.createBoardVisualisationAsTwoArraysBasedOnGameModel(gameModel)
    ticTacToeExtentions.createBoardOutputToConsole(boardFromGameModel).forEach((value) => console.log(value))
    while (ticTacToeExtentions.isNotAllFieldsTaken(boardFromGameModel)) {
        player = player === "O" ? "X" : "O"
        const playerMove = getPlayerMove(player)
        const validationResult = ticTacToeExtentions.validateInput(playerMove, boardFromGameModel, gameModel.boardSize, errorMsg)
        if (!validationResult.isValid) {
            console.log(validationResult.msg)
            continue
        }
        boardFromGameModel[playerMove[0]][playerMove[1]] = player
        ticTacToeExtentions.createBoardOutputToConsole(boardFromGameModel).forEach((value) => console.log(value))
        let isGameOver = ticTacToeExtentions.isPlayerWon(boardFromGameModel, gameModel[playerMove].pathsToEndGame, player)
        isGameOver ? console.log(`Player ${player} won!`) : console.log("Next round!")
        if (isGameOver) {
            break
        }
    }
}

const getPlayerMove = (player) => {
    let arr = ['X', 'Y']
    return arr.map(value => {
        return prompt(`${player} round. Please provide ${value}: `)
    })
}

const playGame = playTicTacToe(3)