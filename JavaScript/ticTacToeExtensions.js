export const createBoardOutputToConsole = (board) => {
    let arr = []
    Array(board.length).fill().forEach((_, i) => {
        let str = "|"
        Array(board.length).fill().forEach((_, j) => {
            str += `${board[j][i]}|`
        })
        arr.push(str)
    })
    return arr
}

export const createBoardOutputToConsoleUsingGameModel = (gameModel) => {
    let arr = []
    Array(gameModel['boardSize']).fill().forEach((_, i) => {
        let str = "|"
        Array(gameModel['boardSize']).fill().forEach((_, j) => {
            str += `${gameModel[[j,i]].player}|`
        })
        arr.push(str)
    })
    return arr
}

export const createAllPathsToEndGame = (board) => {
    let arr = []
    Array(board.length).fill().forEach((_, i) => {
        const arrHorizontal = Array(board.length).fill(i).map((value, index) => [index, value])
        arr.push(arrHorizontal)
        const arrVertical = Array(board.length).fill(i).map((value, index) => [value, index])
        arr.push(arrVertical)
    })
    const arrFromTopLeftToBottomRight = Array(board.length).fill().map((_, index) => [index, index])
    arr.push(arrFromTopLeftToBottomRight)
    const arrFromBottomLeftToTopRight = Array(board.length).fill(board.length - 1).map((value, index) => [index, value - index])
    arr.push(arrFromBottomLeftToTopRight)
    return arr
}

export const validateInput = (input, boardFromGameModel, boardSize, errorMsg) => {
    return validateInputAgaintEmptyString(input) ?
        validateInputAgainstNumbers(input) ?
            validateInputAgainstBoardSize(input, boardSize) ?
                validateInputAgainstEmptyField(boardFromGameModel, input) ?
                    { isValid: true } 
                    : { isValid: false, msg: errorMsg.fieldTaken }
                : { isValid: false, msg: errorMsg.outsideTheBoard }
            : { isValid: false, msg: errorMsg.incorrectInputNumbers }
        : {isValid: false, msg: errorMsg.incorrectInputEmpty}
}

export const validateInputAgaintEmptyString = (input) => {
    return Array.from(input).map(String).every(value => {
        if (value.length > 0) {
            return true
        }
    })
}

export const validateInputAgainstNumbers = (input) => {
    return validateInputAgainstNumbersPerPosition(input[0])
        && validateInputAgainstNumbersPerPosition(input[1])
}

const validateInputAgainstNumbersPerPosition = (input) => {
    return validateInputAgainstLeadingZeros(input)
        && Number.isInteger(Number(input))
}

const validateInputAgainstLeadingZeros = (input) => {
    return input.length === String(Number(input)).length
}

export const validateInputAgainstBoardSize = (input, boardLength) => {
    return checkTheBoundaries(input[0], boardLength) 
        && checkTheBoundaries(input[1], boardLength) 
        ? true : false
}

const checkTheBoundaries = (position, boardLength) => {
    return position >= 0 && position < boardLength
}

export const validateInputAgainstEmptyField = (board, input) => {
    return board[input[0]][input[1]] === '-'
        ? true : false
}

export const isPlayerWon = (board, pathsToEndGame, player) => {
    return pathsToEndGame.some((array) => {
        if (array.every((position) => player === board[position[0]][position[1]])) {
            return true
        }
    })
}

export const createDictionaryWithPathsToEndGameForAllPositions = (board) => {
    const allPathsToEndGame = createAllPathsToEndGame(board)
    let dict = {}
    for (let i = 0; i < board.length; i++) {
        for (let j = 0; j < board.length; j++) {
            const key = [i, j]
            dict[key] = createArrayCollectionWithPathsToEndGameForGivenPosition([i, j], allPathsToEndGame)
        }
    }
    return dict
}

export const createArrayCollectionWithPathsToEndGameForGivenPosition = (position, allPathsToEndGame) => {
    const arr = []
    allPathsToEndGame.forEach(array => {
        if (array.some(value => value[0] === position[0] && value[1] === position[1])) {
            arr.push(array)
        }
    })
    return arr
}

export const isNotAllFieldsTaken = (board) => {
    return board.some((row) => {
        if (row.some((field) => field === '-')) {
            return true
        }
    })
}

export const isNotAllFieldsTakenUsingGameModel = (board) => {
    return board.some((row) => {
        if (row.some((field) => field === '-')) {
            return true
        }
    })
}

export const createGameModel = (boardSize) => {
    const board = Array(boardSize).fill().map(() => Array(boardSize).fill('-'))
    const allPathsToEndGame = createAllPathsToEndGame(board)
    let model = {}
    model['boardSize'] = boardSize
    for (let i = 0; i < boardSize; i++) {
        for (let j = 0; j < boardSize; j++) {
            const key = [i, j]
            const pathsToWin = createArrayCollectionWithPathsToEndGameForGivenPosition([i, j], allPathsToEndGame)
            model[key] = createField(i, j, pathsToWin)
        }
    }
    return model
}

const createField = (xX, yY, pathsToWin) => {
    return { x: xX, y: yY, player: '-', pathsToEndGame: pathsToWin }
}

export const createBoardVisualisationAsTwoArraysBasedOnGameModel = (gameModel) => {
    let arr = []
    Array(gameModel['boardSize']).fill().forEach((_, i) => {
        let arrTmp = []
        Array(gameModel['boardSize']).fill().forEach((_, j) => {
            arrTmp.push(gameModel[[j,i]].player)
        })
        arr.push(arrTmp)
    })
    return arr
}

export const errorMsg = (msg) => {
    return {
        error: 'ERROR',
        fieldTaken: 'Field taken! Skipping round!',
        outsideTheBoard: 'Move outside of the board! Skipping round!',
        incorrectInputEmpty: 'Incorrect input. It cannot be empty! Skipping round!',
        incorrectInputNumbers: 'Incorrect input. Only numbers are allowed! Skipping round!'
    }
}

// export const getNumberFromConsoleInput = (input) => {
//     let str = ''
//     Array.from(input).map(String).forEach(value => {
//         if (Number.isInteger(Number(value))) {
//             str += value
//         }
//     })
//     return str === '' ? NaN : Number(str)
// }
