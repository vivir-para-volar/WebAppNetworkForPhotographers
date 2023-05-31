const method = { get: 'get', post: 'post', delete: 'delete' }
const url = {
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




async function serverGetAllContentLikes(contentId) {
    try {
        return await axios({
            method: method.get,
            url: `${url.getAllContentLikes}?contentId=${contentId}`,
        });
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




async function serverGetNewContentComments(contentId, startTime) {
    try {
        return await axios({
            method: method.get,
            url: `${url.getNewContentComments}?contentId=${contentId}&startTime=${startTime}`,
        });
    } catch (error) {
        console.error(error);
        return false;
    }
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



async function serverGetAllComplaintsBase() {
    try {
        return await axios({
            method: method.get,
            url: url.getAllComplaintsBase,
        });
    } catch (error) {
        console.error(error);
        return false;
    }
}

async function serverCreateComplaint(text, complaintBaseId, contentId) {
    var formData = new FormData();
    formData.append("text", text);
    formData.append("complaintBaseId", complaintBaseId);
    formData.append("contentId", contentId);

    return await sendReqWithRes(method.post, url.createComplaint, formData);
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