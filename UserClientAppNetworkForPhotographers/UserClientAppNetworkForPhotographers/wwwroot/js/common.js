const clientUrl = "https://localhost:7247";

const typeContent = { post: 'Post', blog: 'Blog' }
const statusContent = { open: 'Open', blocked: 'Blocked' }

const listTypeSorting = {
    typeNew: "New",
    typeTop: "Top",
}

const listPeriodSorting = {
    periodAllTime: "AllTime",
    periodDay: "Day",
    periodWeek: "Week",
    periodMonth: "Month",
    periodYear: "Year",
}

const maxPhotoSize = 20 * 1024 * 1024; //25мб
const countInPart = 5;


function getFormComment(num) {
    return declension(num, "комментарий", "комментария", "комментариев");
}

function getFormLike(num) {
    return declension(num, "лайк", "лайка", "лайков");
}

function getFormSubscriber(num) {
    return declension(num, "подписчик", "подписчика", "подписчиков");
}

function getFormSubscriprion(num) {
    return declension(num, "подписка", "подписки", "подписок");
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