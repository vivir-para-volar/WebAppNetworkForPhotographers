const method = { get: 'get', post: 'post', delete: 'delete' }
const url = {
    checkSubscription: '/Subscriptions/Check',
    createSubscription: '/Subscriptions/Create',
    deleteSubscription: '/Subscriptions/Delete',

    getCountSubscribers: '/Subscriptions/GetCountSubscribers',
    getCountSubscriptions: '/Subscriptions/GetCountSubscriptions',
    getSubscribers: '/Subscriptions/GetSubscribers',
    getSubscriptions: '/Subscriptions/GetSubscriptions',

    getUserPosts: '/Profiles/GetUserPosts',
    getUserBlogs: '/Profiles/GetUserBlogs',
    getPhotographerPosts: '/Profiles/GetPhotographerPosts',
    getPhotographerBlogs: '/Profiles/GetPhotographerBlogs',

    searchPhotographers: '/Search/Photographers',
    searchPosts: '/Search/Posts',
    searchBlogs: '/Search/Blogs',

    getFavouritesPosts: '/Favourites/GetPosts',
    getFavouritesBlogs: '/Favourites/GetBlogs',

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




async function serverCheckSubscription(photographerId) {
    const currentUrl = `${url.checkSubscription}?photographerId=${photographerId}`;
    return await sendReqWithoutBody(method.get, currentUrl);
}

async function serverCreateSubscription(photographerId) {
    var formData = new FormData();
    formData.append("photographerId", photographerId);

    return await sendReq(method.post, url.createSubscription, formData);
}

async function serverDeleteSubscription(photographerId) {
    var formData = new FormData();
    formData.append("photographerId", photographerId);

    return await sendReq(method.delete, url.deleteSubscription, formData);
}



async function serverGetCountSubscribers(photographerId) {
    const currentUrl = `${url.getCountSubscribers}?photographerId=${photographerId}`;
    return await sendReqWithoutBody(method.get, currentUrl);
}

async function serverGetCountSubscriptions(photographerId) {
    const currentUrl = `${url.getCountSubscriptions}?photographerId=${photographerId}`;
    return await sendReqWithoutBody(method.get, currentUrl);
}

async function serverGetSubscribers(photographerId) {
    const currentUrl = `${url.getSubscribers}?photographerId=${photographerId}`;
    return await sendReqWithoutBody(method.get, currentUrl);
}

async function serverGetSubscriptions(photographerId) {
    const currentUrl = `${url.getSubscriptions}?photographerId=${photographerId}`;
    return await sendReqWithoutBody(method.get, currentUrl);
}



async function serverGetUserPosts(part) {
    const currentUrl = `${url.getUserPosts}?part=${part}`;
    return await sendReqWithoutBody(method.get, currentUrl);
}

async function serverGetUserBlogs(part) {
    const currentUrl = `${url.getUserBlogs}?part=${part}`;
    return await sendReqWithoutBody(method.get, currentUrl);
}

async function serverGetPhotographerPosts(id, part) {
    const currentUrl = `${url.getPhotographerPosts}?id=${id}&part=${part}`;
    return await sendReqWithoutBody(method.get, currentUrl);
}

async function serverGetPhotographerBlogs(id, part) {
    const currentUrl = `${url.getPhotographerBlogs}?id=${id}&part=${part}`;
    return await sendReqWithoutBody(method.get, currentUrl);
}



async function serverSearchPhotographers(data, part) {
    const currentUrl = `${url.searchPhotographers}?data=${data}&part=${part}`;
    return await sendReqWithoutBody(method.get, currentUrl);
}

async function serverSearchPosts(data, part) {
    const currentUrl = `${url.searchPosts}?data=${data}&part=${part}`;
    return await sendReqWithoutBody(method.get, currentUrl);
}

async function serverSearchBlogs(data, part) {
    const currentUrl = `${url.searchBlogs}?data=${data}&part=${part}`;
    return await sendReqWithoutBody(method.get, currentUrl);
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
