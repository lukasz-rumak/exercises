const width = 11
const diamondSign = '*'
const notDiamondSign = '-'
const isEmptyInside = false;
const diamondCopies = 2

const createDiamond = () => {
    if (width === 1) {
        return ["*"]
    }
    if (!validateInputs()) {
        return ["ERROR"]
    }

    const arraySize = (width + 1) / 2
    const calculatedInsideSpaces = calculateInsideSpaces()
    const calculatedOutsideSpaces = calculateOutsideSpaces()
    let diamond = Array(arraySize).fill().map((_, i) => i === 0 ? createFirstLine(calculatedOutsideSpaces[i])
            : createLine(calculatedOutsideSpaces[i], calculatedInsideSpaces[i]))

    diamond = diamondCopies > 0 ? createDiamondCopies(diamond) : diamond
    return diamond.concat(mirrorFirstHalfDiamond(diamond))
}

const validateInputs = () => width <= 0 || width % 2 === 0 ? false : true

const createDiamondCopies = (diamond) => {
    let newDiamond = []
    Array(diamondCopies).fill().map((_, index) => index === 0 ? newDiamond = diamond.map(line => line.concat(line)) 
        : newDiamond = newDiamond.map((_, index) => newDiamond[index].concat(diamond[index])))
    return newDiamond
}

const mirrorFirstHalfDiamond = (firstHalfDiamond) => {
    let arr = firstHalfDiamond.filter((_, index) => index !== firstHalfDiamond.length - 1);
    return arr.reverse()
}

const calculateInsideSpaces = () => {
    const arraySize = (width + 1) / 2
    let counter = 1
    return Array(arraySize).fill().map((_, index) => index === 0 || index === 1 ? index 
        : counter = counter + 2)
}

const calculateOutsideSpaces = () => {
    const arraySize = (width + 1) / 2
    const outsideSpacesForFirstLine = (width - 1) / 2
    return Array(arraySize).fill().map((_, index) => outsideSpacesForFirstLine - index)
}

const createLine = (spacesOutside, spacesInside) => {
    const outsideSigns = createSigns(notDiamondSign, spacesOutside)
    const insideSigns = isEmptyInside ? createSigns(notDiamondSign, spacesInside)
        : createSigns(diamondSign, spacesInside)
    return [outsideSigns, diamondSign, insideSigns, diamondSign, outsideSigns]
        .reduce((previousValue, currentValue) => previousValue.concat(currentValue))
}

const createFirstLine = (spacesOutside) => {
    const outsideSigns = createSigns(notDiamondSign, spacesOutside)
    return [outsideSigns, diamondSign, outsideSigns]
        .reduce((previousValue, currentValue) => previousValue.concat(currentValue))
}

const createSigns = (sign, spaces) => spaces > 0 ? 
    Array(spaces).fill(sign).reduce((previousValue, currentValue) => previousValue.concat(currentValue)) : ""

const diamond = createDiamond();

diamond.forEach(line => {
    console.log(line)
})

var result = document.getElementById("results");
for (let i = 0; i < diamond.length; i++) {
    result.innerHTML = result.innerHTML + diamond[i] + "<br>"
}
