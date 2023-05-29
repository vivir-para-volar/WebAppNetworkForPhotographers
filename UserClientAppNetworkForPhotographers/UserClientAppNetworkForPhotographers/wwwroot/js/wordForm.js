function getFormComment(num) {
    return declension(num, "комментарий", "комментария", "комментариев");
}

function declension(num, nominative, dative, dativeMany) {
    num = Math.abs(num) % 100;
    if (num > 10 && num < 20) {
        return dativeMany;
    }
    if (num % 10 >= 2 && num % 10 <= 4) {
        return dative;
    }
    if (num % 10 == 1) {
        return nominative;
    }
    return dativeMany;
}