const method = { post: 'post', delete: 'delete' }
const url = {
    createLike: '/ContentActions/CreateLike',
    deleteLike: '/ContentActions/DeleteLike',

    createFavourite: '/ContentActions/CreateFavourite',
    deleteFavourite: '/ContentActions/DeleteFavourite',

    createComment: '/ContentActions/CreateComment',
    deleteComment: '/ContentActions/DeleteComment',
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


async function serverCreateComment(text, contentId) {
    var formData = new FormData();
    formData.append("text", text);
    formData.append("contentId", contentId);

    return await sendReqWithRes(method.post, url.createComment, formData);
}

async function serverDeleteComment(id) {
    var formData = new FormData();
    formData.append("id", id);

    return await sendReq(method.delete, url.deleteComment, formData);
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


async function sendReqWithRes(method, url, data) {
    try {
        return await axios({
            method,
            url,
            data,
            headers: { "Content-Type": "multipart/form-data" },
        });
    } catch (error) {
        console.error(error);
        return false;
    }
}
