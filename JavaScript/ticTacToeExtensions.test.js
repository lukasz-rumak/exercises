import * as ticTacToeExtentions from './ticTacToeExtensions.js'

test('Create Console Output To Console - Empty Board', () => {
    const board = Array(3).fill().map(() => Array(3).fill('-'))
    const expectArray = [ '|-|-|-|', '|-|-|-|', '|-|-|-|' ]
    expect(ticTacToeExtentions.createBoardOutputToConsole(board)).toStrictEqual(expectArray)
})

test('Create Console Output To Console - Board During The Game', () => {
    const board = Array(3).fill().map(() => Array(3).fill('-'))
    board[0][0] = "X"
    board[1][1] = "O"
    board[2][2] = "X"
    board[0][2] = "O"
    board[2][0] = "X"
    const expectArray = [ '|X|-|X|', '|-|O|-|', '|O|-|X|' ]
    expect(ticTacToeExtentions.createBoardOutputToConsole(board)).toStrictEqual(expectArray)
})

test('Create Board Visualisation As Two Arrays Based On Game Model - Empty Board', () => {
    const gameModel = ticTacToeExtentions.createGameModel(3)
    const expectArray = [ [ '-', '-', '-' ], [ '-', '-', '-' ], [ '-', '-', '-' ] ]
    expect(ticTacToeExtentions.createBoardVisualisationAsTwoArraysBasedOnGameModel(gameModel)).toStrictEqual(expectArray)
})

test('Create Board Visualisation As Two Arrays Based On Game Model - Board During The Game', () => {
    let gameModel = ticTacToeExtentions.createGameModel(3)
    gameModel[[0, 0]].player = "X"
    gameModel[[1, 1]].player = "O"
    gameModel[[2, 2]].player = "X"
    gameModel[[0, 2]].player = "O"
    gameModel[[2, 0]].player = "X"
    const expectArray = [ [ 'X', '-', 'X' ], [ '-', 'O', '-' ], [ 'O', '-', 'X' ] ]
    expect(ticTacToeExtentions.createBoardVisualisationAsTwoArraysBasedOnGameModel(gameModel)).toStrictEqual(expectArray)
})

test('Create Console Output To Console Using Game Model - Empty Board', () => {
    const gameModel = ticTacToeExtentions.createGameModel(3)
    const expectArray = [ '|-|-|-|', '|-|-|-|', '|-|-|-|' ]
    expect(ticTacToeExtentions.createBoardOutputToConsoleUsingGameModel(gameModel)).toStrictEqual(expectArray)
})

test('Create Console Output To Console Using Game Model - Board During The Game', () => {
    let gameModel = ticTacToeExtentions.createGameModel(3)
    gameModel[[0, 0]].player = "X"
    gameModel[[1, 1]].player = "O"
    gameModel[[2, 2]].player = "X"
    gameModel[[0, 2]].player = "O"
    gameModel[[2, 0]].player = "X"
    const expectArray = [ '|X|-|X|', '|-|O|-|', '|O|-|X|' ]
    expect(ticTacToeExtentions.createBoardOutputToConsoleUsingGameModel(gameModel)).toStrictEqual(expectArray)
})

test('Create All Paths To End Game - Board Size 3x3', () => {
    const expectArray = [
        [ [ 0, 0 ], [ 1, 0 ], [ 2, 0 ] ],
        [ [ 0, 0 ], [ 0, 1 ], [ 0, 2 ] ],
        [ [ 0, 1 ], [ 1, 1 ], [ 2, 1 ] ],
        [ [ 1, 0 ], [ 1, 1 ], [ 1, 2 ] ],
        [ [ 0, 2 ], [ 1, 2 ], [ 2, 2 ] ],
        [ [ 2, 0 ], [ 2, 1 ], [ 2, 2 ] ],
        [ [ 0, 0 ], [ 1, 1 ], [ 2, 2 ] ],
        [ [ 0, 2 ], [ 1, 1 ], [ 2, 0 ] ]
      ]
    const board = Array(3).fill().map(() => Array(3).fill('-'))
    expect(ticTacToeExtentions.createAllPathsToEndGame(board)).toStrictEqual(expectArray)
})

test('Validate Input - True', () => {
  const input = ['1', '1']
  const boardFromGameModel = [ [ '-', '-', '-' ], [ '-', '-', '-' ], [ '-', '-', '-' ] ]
  const boardSize = 3
  const errorMsg = ticTacToeExtentions.errorMsg()
  const actualResult = ticTacToeExtentions.validateInput(input, boardFromGameModel, boardSize, errorMsg)
  const expectResult = { isValid: true }
  expect(actualResult).toStrictEqual(expectResult)
})

test('Validate Input - Against Empty String Is False', () => {
  const input = ['1', '']
  const boardFromGameModel = [ [ '-', '-', '-' ], [ '-', '-', '-' ], [ '-', '-', '-' ] ]
  const boardSize = 3
  const errorMsg = ticTacToeExtentions.errorMsg()
  const actualResult = ticTacToeExtentions.validateInput(input, boardFromGameModel, boardSize, errorMsg)
  const expectResult = { isValid: false, msg: errorMsg.incorrectInputEmpty }
  expect(actualResult).toStrictEqual(expectResult)
})

test('Validate Input - Against Numbers Is False', () => {
  const input = ['1x', '1']
  const boardFromGameModel = [ [ '-', '-', '-' ], [ '-', '-', '-' ], [ '-', '-', '-' ] ]
  const boardSize = 3
  const errorMsg = ticTacToeExtentions.errorMsg()
  const actualResult = ticTacToeExtentions.validateInput(input, boardFromGameModel, boardSize, errorMsg)
  const expectResult = { isValid: false, msg: errorMsg.incorrectInputNumbers }
  expect(actualResult).toStrictEqual(expectResult)
})

test('Validate Input - Board Size Is False', () => {
  const input = ['11', '12']
  const boardFromGameModel = [ [ '-', '-', '-' ], [ '-', '-', '-' ], [ '-', '-', '-' ] ]
  const boardSize = 3
  const errorMsg = ticTacToeExtentions.errorMsg()
  const actualResult = ticTacToeExtentions.validateInput(input, boardFromGameModel, boardSize, errorMsg)
  const expectResult = { isValid: false, msg: errorMsg.outsideTheBoard }
  expect(actualResult).toStrictEqual(expectResult)
})

test('Validate Input - Empty Field Is False', () => {
  const input = ['1', '1']
  const boardFromGameModel = [ [ '-', '-', '-' ], [ '-', 'X', '-' ], [ '-', '-', '-' ] ]
  const boardSize = 3
  const errorMsg = ticTacToeExtentions.errorMsg()
  const actualResult = ticTacToeExtentions.validateInput(input, boardFromGameModel, boardSize, errorMsg)
  const expectResult = { isValid: false, msg: errorMsg.fieldTaken }
  expect(actualResult).toStrictEqual(expectResult)
})

test('Validate Input Againt Empty String - True', () => {
  expect(ticTacToeExtentions.validateInputAgaintEmptyString(['0', '1'])).toBeTruthy()
  expect(ticTacToeExtentions.validateInputAgaintEmptyString(['1', '22'])).toBeTruthy()
  expect(ticTacToeExtentions.validateInputAgaintEmptyString(['1x23', '23x'])).toBeTruthy()
  expect(ticTacToeExtentions.validateInputAgaintEmptyString(['xxx', 'yyy'])).toBeTruthy()
})

test('Validate Input Againt Empty String - False', () => {
  expect(ticTacToeExtentions.validateInputAgaintEmptyString(['', ''])).toBeFalsy()
  expect(ticTacToeExtentions.validateInputAgaintEmptyString(['', '11'])).toBeFalsy()
  expect(ticTacToeExtentions.validateInputAgaintEmptyString(['11', ''])).toBeFalsy()
})

test('Validate Input Against Numbers - True', () => {
    expect(ticTacToeExtentions.validateInputAgainstNumbers(['0', '0'])).toBeTruthy()
    expect(ticTacToeExtentions.validateInputAgainstNumbers(['0', '1'])).toBeTruthy()
    expect(ticTacToeExtentions.validateInputAgainstNumbers(['1', '0'])).toBeTruthy()
    expect(ticTacToeExtentions.validateInputAgainstNumbers(['1', '2'])).toBeTruthy()
    expect(ticTacToeExtentions.validateInputAgainstNumbers(['123', '23'])).toBeTruthy()
    expect(ticTacToeExtentions.validateInputAgainstNumbers(['555', '9999'])).toBeTruthy()
    expect(ticTacToeExtentions.validateInputAgainstNumbers(['-11', '11'])).toBeTruthy() 
    expect(ticTacToeExtentions.validateInputAgainstNumbers(['11', '-11'])).toBeTruthy()
})

test('Validate Input Against Numbers- False', () => {
    expect(ticTacToeExtentions.validateInputAgainstNumbers(['02', '11'])).toBeFalsy() 
    expect(ticTacToeExtentions.validateInputAgainstNumbers(['11', '00'])).toBeFalsy()
    expect(ticTacToeExtentions.validateInputAgainstNumbers(['11', '000002'])).toBeFalsy()
    expect(ticTacToeExtentions.validateInputAgainstNumbers(['xxx', '111'])).toBeFalsy()
    expect(ticTacToeExtentions.validateInputAgainstNumbers(['111', 'xxx'])).toBeFalsy()
    expect(ticTacToeExtentions.validateInputAgainstNumbers(['xxx', 'yyy'])).toBeFalsy()
    expect(ticTacToeExtentions.validateInputAgainstNumbers(['5x5x5', '5xx1'])).toBeFalsy()
    expect(ticTacToeExtentions.validateInputAgainstNumbers(['x5x5x555', 'yyy55yy'])).toBeFalsy()
    expect(ticTacToeExtentions.validateInputAgainstNumbers(['555x5x5x', 'x1x1x1x'])).toBeFalsy()
    expect(ticTacToeExtentions.validateInputAgainstNumbers(['!@#!@#', '#@!123!@#'])).toBeFalsy()
})

test('Validate Input Against Board Size - True', () => {
    expect(ticTacToeExtentions.validateInputAgainstBoardSize([0, 0], 3)).toBeTruthy()
    expect(ticTacToeExtentions.validateInputAgainstBoardSize([2, 1], 3)).toBeTruthy()
    expect(ticTacToeExtentions.validateInputAgainstBoardSize([2, 2], 3)).toBeTruthy()
})

test('Validate Input Against Board Size - False', () => {
    expect(ticTacToeExtentions.validateInputAgainstBoardSize([-1, -1], 3)).toBeFalsy()
    expect(ticTacToeExtentions.validateInputAgainstBoardSize([5, 5], 3)).toBeFalsy()
    expect(ticTacToeExtentions.validateInputAgainstBoardSize([1, 9], 3)).toBeFalsy()
    expect(ticTacToeExtentions.validateInputAgainstBoardSize([11, 2], 3)).toBeFalsy()
})

test('Validate Input Against Empty Field - True', () => {
    const board = Array(3).fill().map(() => Array(3).fill('-'))
    expect(ticTacToeExtentions.validateInputAgainstEmptyField(board, [1, 1])).toBeTruthy()
})

test('Validate Input Against Empty Field - Fasle', () => {
    const board = Array(3).fill().map(() => Array(3).fill('-'))
    board[1][1] = "X"
    expect(ticTacToeExtentions.validateInputAgainstEmptyField(board, [1, 1])).toBeFalsy()
})

test('Is Player Won - True', () => {
    const board = Array(3).fill().map(() => Array(3).fill('-'))
    board[0][0] = "O"
    board[1][1] = "O"
    board[2][2] = "O"
    board[1][2] = "X"
    board[2][1] = "X"
    const pathsToEndGame = ticTacToeExtentions.createDictionaryWithPathsToEndGameForAllPositions(board)
    expect(ticTacToeExtentions.isPlayerWon(board, pathsToEndGame[[1, 1]], "O")).toBeTruthy()
})

test('Is Player Won - False', () => {
    const board = Array(3).fill().map(() => Array(3).fill('-'))
    board[0][0] = "O"
    board[1][1] = "O"
    board[2][2] = "O"
    board[2][0] = "0"
    board[1][2] = "X"
    board[2][1] = "X"
    const pathsToEndGame = ticTacToeExtentions.createDictionaryWithPathsToEndGameForAllPositions(board)
    expect(ticTacToeExtentions.isPlayerWon(board, pathsToEndGame[[2, 0]], "O")).toBeFalsy()
})

test('Is Player Won - False', () => {
    const board = Array(3).fill().map(() => Array(3).fill('-'))
    board[0][0] = "O"
    board[1][1] = "O"
    board[2][2] = "O"
    board[1][2] = "X"
    board[2][1] = "X"
    const pathsToEndGame = ticTacToeExtentions.createDictionaryWithPathsToEndGameForAllPositions(board)
    expect(ticTacToeExtentions.isPlayerWon(board, pathsToEndGame[[2, 1]], "X")).toBeFalsy()
})

test('Is Player Won - False', () => {
    const board = Array(3).fill().map(() => Array(3).fill('-'))
    board[0][0] = "O"
    board[0][1] = "O"
    board[0][2] = "X"
    board[1][1] = "X"
    board[2][1] = "X"
    const pathsToEndGame = ticTacToeExtentions.createDictionaryWithPathsToEndGameForAllPositions(board)
    expect(ticTacToeExtentions.isPlayerWon(board, pathsToEndGame[[0, 1]], "O")).toBeFalsy()
})

test('Is Not All Fields Taken - True', () => {
    const board = Array(3).fill().map(() => Array(3).fill('-'))
    board[0][0] = "O"
    board[0][1] = "O"
    board[0][2] = "X"
    board[1][1] = "X"
    board[2][1] = "X"
    expect(ticTacToeExtentions.isNotAllFieldsTaken(board)).toBeTruthy()
})

test('Is Not All Fields Taken - False', () => {
    const board = Array(3).fill().map(() => Array(3).fill('-'))
    board[0][0] = "O"
    board[0][1] = "O"
    board[0][2] = "X"
    board[1][0] = "O"
    board[1][1] = "O"
    board[1][2] = "X"
    board[2][0] = "O"
    board[2][1] = "O"
    board[2][2] = "X"
    expect(ticTacToeExtentions.isNotAllFieldsTaken(board)).toBeFalsy()
})

test('Create Array Collection With Paths To End Game For Given Position', () => {
    const allPathsToEndGame = [
        [ [ 0, 0 ], [ 1, 0 ], [ 2, 0 ] ],
        [ [ 0, 0 ], [ 0, 1 ], [ 0, 2 ] ],
        [ [ 0, 1 ], [ 1, 1 ], [ 2, 1 ] ],
        [ [ 1, 0 ], [ 1, 1 ], [ 1, 2 ] ],
        [ [ 0, 2 ], [ 1, 2 ], [ 2, 2 ] ],
        [ [ 2, 0 ], [ 2, 1 ], [ 2, 2 ] ],
        [ [ 0, 0 ], [ 1, 1 ], [ 2, 2 ] ],
        [ [ 0, 2 ], [ 1, 1 ], [ 2, 0 ] ]
      ]

    const actualResult = ticTacToeExtentions.createArrayCollectionWithPathsToEndGameForGivenPosition([1, 1], allPathsToEndGame)
    const expectResult = [
        [ [ 0, 1 ], [ 1, 1 ], [ 2, 1 ] ],
        [ [ 1, 0 ], [ 1, 1 ], [ 1, 2 ] ],
        [ [ 0, 0 ], [ 1, 1 ], [ 2, 2 ] ],
        [ [ 0, 2 ], [ 1, 1 ], [ 2, 0 ] ]
      ]
    expect(actualResult).toStrictEqual(expectResult)
})

test('Create Dictionary With Paths To End Game For All Positions', () => {
    const board = Array(3).fill().map(() => Array(3).fill('-'))
    const actualResult = ticTacToeExtentions.createDictionaryWithPathsToEndGameForAllPositions(board)
    const expectResult = {
        '0,0': [ 
            [ [ 0, 0 ], [ 1, 0 ], [ 2, 0 ] ],
            [ [ 0, 0 ], [ 0, 1 ], [ 0, 2 ] ],
            [ [ 0, 0 ], [ 1, 1 ], [ 2, 2 ] ]
          ],
        '0,1': [ 
            [ [ 0, 0 ], [ 0, 1 ], [ 0, 2 ] ], 
            [ [ 0, 1 ], [ 1, 1 ], [ 2, 1 ] ] 
          ],
        '0,2': [
            [ [ 0, 0 ], [ 0, 1 ], [ 0, 2 ] ],
            [ [ 0, 2 ], [ 1, 2 ], [ 2, 2 ] ],
            [ [ 0, 2 ], [ 1, 1 ], [ 2, 0 ] ]
          ],
        '1,0': [ 
            [ [ 0, 0 ], [ 1, 0 ], [ 2, 0 ] ], 
            [ [ 1, 0 ], [ 1, 1 ], [ 1, 2 ] ] 
          ],
        '1,1': [
            [ [ 0, 1 ], [ 1, 1 ], [ 2, 1 ] ],
            [ [ 1, 0 ], [ 1, 1 ], [ 1, 2 ] ],
            [ [ 0, 0 ], [ 1, 1 ], [ 2, 2 ] ],
            [ [ 0, 2 ], [ 1, 1 ], [ 2, 0 ] ]
          ],
        '1,2': [ 
            [ [ 1, 0 ], [ 1, 1 ], [ 1, 2 ] ], 
            [ [ 0, 2 ], [ 1, 2 ], [ 2, 2 ] ] 
          ],
        '2,0': [
            [ [ 0, 0 ], [ 1, 0 ], [ 2, 0 ] ],
            [ [ 2, 0 ], [ 2, 1 ], [ 2, 2 ] ],
            [ [ 0, 2 ], [ 1, 1 ], [ 2, 0 ] ]
          ],
        '2,1': [ 
            [ [ 0, 1 ], [ 1, 1 ], [ 2, 1 ] ], 
            [ [ 2, 0 ], [ 2, 1 ], [ 2, 2 ] ] 
          ],
        '2,2': [
            [ [ 0, 2 ], [ 1, 2 ], [ 2, 2 ] ],
            [ [ 2, 0 ], [ 2, 1 ], [ 2, 2 ] ],
            [ [ 0, 0 ], [ 1, 1 ], [ 2, 2 ] ]
          ]
      }

    expect(actualResult).toStrictEqual(expectResult)
})

test('Create Game Model', () => {
    const actualResult = ticTacToeExtentions.createGameModel(3)
    const expectResult = {
        'boardSize': 3,
        '0,0': { x: 0, y: 0, player: '-', pathsToEndGame: [
            [ [ 0, 0 ], [ 1, 0 ], [ 2, 0 ] ],
            [ [ 0, 0 ], [ 0, 1 ], [ 0, 2 ] ],
            [ [ 0, 0 ], [ 1, 1 ], [ 2, 2 ] ]
          ] },
        '0,1': { x: 0, y: 1, player: '-', pathsToEndGame: [ 
            [ [ 0, 0 ], [ 0, 1 ], [ 0, 2 ] ], 
            [ [ 0, 1 ], [ 1, 1 ], [ 2, 1 ] ] 
          ] },
        '0,2': { x: 0, y: 2, player: '-', pathsToEndGame: [
            [ [ 0, 0 ], [ 0, 1 ], [ 0, 2 ] ],
            [ [ 0, 2 ], [ 1, 2 ], [ 2, 2 ] ],
            [ [ 0, 2 ], [ 1, 1 ], [ 2, 0 ] ]
          ] },
        '1,0': { x: 1, y: 0, player: '-', pathsToEndGame: [ 
            [ [ 0, 0 ], [ 1, 0 ], [ 2, 0 ] ], 
            [ [ 1, 0 ], [ 1, 1 ], [ 1, 2 ] ] 
          ] },
        '1,1': { x: 1, y: 1, player: '-', pathsToEndGame: [
            [ [ 0, 1 ], [ 1, 1 ], [ 2, 1 ] ],
            [ [ 1, 0 ], [ 1, 1 ], [ 1, 2 ] ],
            [ [ 0, 0 ], [ 1, 1 ], [ 2, 2 ] ],
            [ [ 0, 2 ], [ 1, 1 ], [ 2, 0 ] ]
          ] },
        '1,2': { x: 1, y: 2, player: '-', pathsToEndGame: [ 
            [ [ 1, 0 ], [ 1, 1 ], [ 1, 2 ] ], 
            [ [ 0, 2 ], [ 1, 2 ], [ 2, 2 ] ] 
          ] },
        '2,0': { x: 2, y: 0, player: '-', pathsToEndGame: [
            [ [ 0, 0 ], [ 1, 0 ], [ 2, 0 ] ],
            [ [ 2, 0 ], [ 2, 1 ], [ 2, 2 ] ],
            [ [ 0, 2 ], [ 1, 1 ], [ 2, 0 ] ]
          ] },
        '2,1': { x: 2, y: 1, player: '-', pathsToEndGame: [ 
            [ [ 0, 1 ], [ 1, 1 ], [ 2, 1 ] ], 
            [ [ 2, 0 ], [ 2, 1 ], [ 2, 2 ] ] 
          ] },
        '2,2': { x: 2, y: 2, player: '-', pathsToEndGame: [
            [ [ 0, 2 ], [ 1, 2 ], [ 2, 2 ] ],
            [ [ 2, 0 ], [ 2, 1 ], [ 2, 2 ] ],
            [ [ 0, 0 ], [ 1, 1 ], [ 2, 2 ] ]
          ] }
      }
    expect(actualResult).toStrictEqual(expectResult)
})