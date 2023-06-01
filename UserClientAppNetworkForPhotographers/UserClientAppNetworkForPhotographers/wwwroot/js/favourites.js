let partFavouritesPosts = 1;
let partFavouritesBlogs = 1;


const btnMoreFavouritesPosts = document.getElementById("btnMoreFavouritesPosts");
const btnMoreFavouritesBlogs = document.getElementById("btnMoreFavouritesBlogs");

const listFavouritesPosts = document.getElementById("listFavouritesPosts");
const listFavouritesBlogs = document.getElementById("listFavouritesBlogs");



function initFavourites(countPosts, countBlogs) {
    if (btnMoreFavouritesPosts && countPosts < countInPart) {
        btnMoreFavouritesPosts.style.display = "none";
    }
    if (btnMoreFavouritesBlogs && countBlogs < countInPart) {
        btnMoreFavouritesBlogs.style.display = "none";
    }
}


async function moreFavouritesPosts() {
    partFavouritesPosts++;

    const res = await serverGetFavouritesPosts(partFavouritesPosts);
    if (!res) return false;

    addContentsForList(res.data, listFavouritesPosts, btnMoreFavouritesPosts);
}

async function moreFavouritesBlogs() {
    partFavouritesBlogs++;

    const res = await serverGetFavouritesBlogs(partFavouritesBlogs);
    if (!res) return false;

    addContentsForList(res.data, listFavouritesBlogs, btnMoreFavouritesBlogs);
}