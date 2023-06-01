const method = { get: 'get', post: 'post', delete: 'delete' }
const url = {
    getFavouritesPosts: '/Favourites/GetFavouritesPosts',
    getFavouritesBlogs: '/Favourites/GetFavouritesBlogs',

    getAllContentLikes: '/ContentActions/GetAllContentLikes',
    createLike: '/ContentActions/CreateLike',
    deleteLike: '/ContentActions/DeleteLike',

    createFavourite: '/ContentActions/CreateFavourite',
    deleteFavourite: '/ContentActions/DeleteFavourite',

    getAllComplaintsBase: '/ContentActions/GetAllComplaintsBase',
    createComplaint: '/ContentActions/CreateComplaint',

    getNewContentComments: '/ContentActions/GetNewContentComments',
    createComment: '/ContentActions/CreateComment',
    deleteComment: '/ContentActions/DeleteComment',
}



async function serverGetFavouritesPosts(part) {
    const currentUrl = `${url.getFavouritesPosts}?part=${part}`;
    return await sendReqWithoutBody(method.get, currentUrl);
}

async function serverGetFavouritesBlogs(part) {
    const currentUrl = `${url.getFavouritesBlogs}?part=${part}`;
    return await sendReqWithoutBody(method.get, currentUrl);
}



async function serverGetAllContentLikes(contentId) {
    const currentUrl = `${url.getAllContentLikes}?contentId=${contentId}`;
    return await sendReqWithoutBody(method.get, currentUrl);
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




async function serverGetNewContentComments(contentId, startTime) {
    const currentUrl = `${url.getNewContentComments}?contentId=${contentId}&startTime=${startTime}`;
    return await sendReqWithoutBody(method.get, currentUrl);
}

async function serverCreateComment(text, contentId) {
    var formData = new FormData();
    formData.append("text", text);
    formData.append("contentId", contentId);

    return await sendReq(method.post, url.createComment, formData);
}

async function serverDeleteComment(id) {
    var formData = new FormData();
    formData.append("id", id);

    return await sendReq(method.delete, url.deleteComment, formData);
}



async function serverGetAllComplaintsBase() {
    return await sendReqWithoutBody(method.get, url.getAllComplaintsBase);
}

async function serverCreateComplaint(text, complaintBaseId, contentId) {
    var formData = new FormData();
    formData.append("text", text);
    formData.append("complaintBaseId", complaintBaseId);
    formData.append("contentId", contentId);

    return await sendReq(method.post, url.createComplaint, formData);
}



async function sendReq(method, url, data) {
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

async function sendReqWithoutBody(method, url) {
    try {
        return await axios({
            method,
            url,
        });
    } catch (error) {
        console.error(error);
        return false;
    }
}
