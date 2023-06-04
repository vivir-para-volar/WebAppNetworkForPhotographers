const typeTabs = { news: 'newsTab', others: 'othersTab' }


let partHomeNews = 1;
let partHomeOthers = 1;


const dataFilterNews = {
    typeContent: '',
    categoriesIds: []
}

const dataFilterOthers = {
    typeSorting: '',
    countLikeSorting: 0,
    periodSorting: '',

    typeContent: '',
    categoriesIds: []
}


const btnMoreHomeNews = document.getElementById("btnMoreHomeNews");
const btnMoreHomeOthers = document.getElementById("btnMoreHomeOthers");

const listHomeNews = document.getElementById("listHomeNews");
const listHomeOthers = document.getElementById("listHomeOthers");

const emptyHomeNews = document.getElementById("emptyHomeNews");
const emptyHomeOthers = document.getElementById("emptyHomeOthers");



function initHome(countNews, countOthers, chooseCategory) {
    dataFilterOthers.typeSorting = listTypeSorting.typeNew;
    dataFilterOthers.periodSorting = listPeriodSorting.periodAllTime;

    if (chooseCategory != null) {
        dataFilterOthers.categoriesIds.push(chooseCategory);

        document.querySelectorAll('.nav-tabs button')[1].click();
    }

    if (countNews === 0) {
        emptyHomeNews.style.display = "block";
    }
    else if (countNews >= countInPart) {
        btnMoreHomeNews.style.display = "block";
    }

    if (countOthers === 0) {
        emptyHomeOthers.style.display = "block";
    }
    else if (countOthers >= countInPart) {
        btnMoreHomeOthers.style.display = "block";
    }
}



async function moreHomeNews() {
    partHomeNews++;

    const res = await serverGetNews(dataFilterNews, partHomeNews);
    if (!res) return false;

    addContentsForList(res.data, listHomeNews, btnMoreHomeNews);
}

async function moreHomeOthers() {
    partHomeOthers++;

    const res = await serverGetOthers(dataFilterOthers, partHomeOthers);
    if (!res) return false;

    addContentsForList(res.data, listHomeOthers, btnMoreHomeOthers);
}




const selectTypeContentNews = document.getElementById("selectTypeContentNews");
const checkboxesCategoriesNews = document.querySelectorAll('#categoriesHomeNews input[type="checkbox"]')

async function filterHomeNews() {
    partHomeNews = 1;
    listHomeNews.innerHTML = "";

    dataFilterNews.typeContent = selectTypeContentNews.value;
    dataFilterNews.categoriesIds = selectTypeContentNews.value;

    dataFilterNews.categoriesIds = [];
    for (let i = 0; i < checkboxesCategoriesNews.length; i++) {
        if (checkboxesCategoriesNews[i].checked) {
            dataFilterNews.categoriesIds.push(checkboxesCategoriesNews[i].value);
        }
    }

    const res = await serverGetNews(dataFilterNews, partHomeNews);
    if (!res) return false;

    if (res.data.length === 0) {
        btnMoreHomeNews.style.display = "none";
        emptyHomeNews.style.display = "block";
        return false;
    }
    else {
        emptyHomeNews.style.display = "none";
    }

    addContentsForList(res.data, listHomeNews, btnMoreHomeNews);
}



const selectTypeSorting = document.getElementById("selectTypeSorting");
const selectCountLikeSorting = document.getElementById("selectCountLikeSorting");
const selectPeriodSorting = document.getElementById("selectPeriodSorting");

const selectTypeContentOthers = document.getElementById("selectTypeContentOthers");
const checkboxesCategoriesOthers = document.querySelectorAll('#categoriesHomeOthers input[type="checkbox"]')

async function filterHomeOthers() {
    partHomeOthers = 1;
    listHomeOthers.innerHTML = "";

    dataFilterOthers.typeSorting = selectTypeSorting.value;
    dataFilterOthers.countLikeSorting = selectCountLikeSorting.value;
    dataFilterOthers.periodSorting = selectPeriodSorting.value;

    dataFilterOthers.typeContent = selectTypeContentOthers.value;

    dataFilterOthers.categoriesIds = [];
    for (let i = 0; i < checkboxesCategoriesOthers.length; i++) {
        if (checkboxesCategoriesOthers[i].checked) {
            dataFilterOthers.categoriesIds.push(checkboxesCategoriesOthers[i].value);
        }
    }

    const res = await serverGetOthers(dataFilterOthers, partHomeOthers);
    if (!res) return false;

    if (res.data.length === 0) {
        btnMoreHomeOthers.style.display = "none";
        emptyHomeOthers.style.display = "block";
        return false;
    }
    else {
        emptyHomeOthers.style.display = "none";
    }

    addContentsForList(res.data, listHomeOthers, btnMoreHomeOthers);
}






const btnFilterHomeNews = document.getElementById("btnFilterHomeNews");
const btnFilterHomeOthers = document.getElementById("btnFilterHomeOthers");


const tabBtns = document.querySelectorAll('.nav-tabs li button');
tabBtns.forEach(btn => {
    btn.addEventListener('click', () => {
        activeTab = btn.id;

        switch (activeTab) {
            case typeTabs.news:
                btnFilterHomeNews.style.display = "block";
                btnFilterHomeOthers.style.display = "none";
                break;
            case typeTabs.others:
                btnFilterHomeNews.style.display = "none";
                btnFilterHomeOthers.style.display = "block";
                break;
        }
    });
});