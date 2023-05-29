async function createLike(contentId) {
    const res = await serverCreateLike(contentId);

    if (res) {
        document.getElementById("btnCreateLike" + contentId).style.display = "none";
        document.getElementById("btnDeleteLike" + contentId).style.display = "inline-block";

        const aCountLikes = document.getElementById("countLikes" + contentId);
        aCountLikes.innerHTML = +aCountLikes.innerHTML + 1;
    }
}

async function deleteLike(contentId) {
    const res = await serverDeleteLike(contentId);

    if (res) {
        document.getElementById("btnCreateLike" + contentId).style.display = "inline-block";
        document.getElementById("btnDeleteLike" + contentId).style.display = "none";

        const aCountLikes = document.getElementById("countLikes" + contentId);
        aCountLikes.innerHTML = +aCountLikes.innerHTML - 1;
    }
}


async function createFavourite(contentId) {
    const res = await serverCreateFavourite(contentId);

    if (res) {
        document.getElementById("btnCreateFavourite" + contentId).style.display = "none";
        document.getElementById("btnDeleteFavourite" + contentId).style.display = "inline-block";

        const aCountFavourites = document.getElementById("countFavourites" + contentId);
        aCountFavourites.innerHTML = +aCountFavourites.innerHTML + 1;
    }
}

async function deleteFavourite(contentId) {
    const res = await serverDeleteFavourite(contentId);

    if (res) {
        document.getElementById("btnCreateFavourite" + contentId).style.display = "inline-block";
        document.getElementById("btnDeleteFavourite" + contentId).style.display = "none";

        const aCountFavourites = document.getElementById("countFavourites" + contentId);
        aCountFavourites.innerHTML = +aCountFavourites.innerHTML - 1;
    }
}
