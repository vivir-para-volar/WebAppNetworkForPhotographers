const method = { post: 'post', delete: 'delete' }
const url = {
    createLike: '/Likes/CreateLike',
    deleteLike: '/Likes/DeleteLike',
    createFavourite: '/Favourites/CreateFavourite',
    deleteFavourite: '/Favourites/DeleteFavourite',
}

async function sendReq(method, url, data) {
    try {
        await axios({
            method,
            url,
            data,
            headers: { "Content-Type": "multipart/form-data" },
        });

        return true;
    } catch (error) {
        console.error(error);
        return false;
    }
}

async function serverCreateLike(contentId) {
    var formData = new FormData();
    formData.append("contentId", contentId);

    return await sendReq(method.post, url.createLike, formData);
}

async function serverDeleteLike(contentId) {
    var formData = new FormData();
    formData.append("contentId", contentId);

    return await sendReq(method.delete, url.deleteLike, formData);
}


async function serverCreateFavourite(contentId) {
    var formData = new FormData();
    formData.append("contentId", contentId);

    return await sendReq(method.post, url.createFavourite, formData);
}

async function serverDeleteFavourite(contentId) {
    var formData = new FormData();
    formData.append("contentId", contentId);

    return await sendReq(method.delete, url.deleteFavourite, formData);
}
