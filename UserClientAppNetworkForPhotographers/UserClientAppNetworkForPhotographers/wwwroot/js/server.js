const method = { get: 'get', post: 'post', delete: 'delete' }
const url = {
    createPost: '/Contents/CreatePost',
    createBlog: '/Contents/CreateBlog',
    updateBlogMainPhoto: '/Contents/UpdateBlogMainPhoto',
    createPhoto: '/Contents/CreatePhoto',

    getNews: '/Home/GetNews',
    getOthers: '/Home/GetOthers',

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

    getPhotoInfo: '/ContentActions/GetPhotoInfo',
}



async function serverCreatePost(data) {
    const formData = new FormData();
    formData.append("title", data.title);
    formData.append("photographerId", data.photographerId);

    for (let categoryId of data.categoriesIds) {
        formData.append("categoriesIds", categoryId);
    }

    return await sendReq(method.post, url.createPost, formData);
}

async function serverCreateBlog(data) {
    let formData = new FormData();
    formData.append("title", data.title);
    formData.append("blogBody", data.body);
    formData.append("photographerId", data.photographerId);

    for (let categoryId of data.categoriesIds) {
        formData.append("categoriesIds", categoryId);
    }

    const res = await sendReq(method.post, url.createBlog, formData);
    const createdBlog = res.data;

    formData = new FormData();
    formData.append("contentId", createdBlog.id);
    formData.append("photo", data.mainPhoto);

    return await sendReq(method.post, url.updateBlogMainPhoto, formData);
}

async function serverCreatePhoto(contentId, photo, photosInfo) {
    const formData = new FormData();
    formData.append("contentId", contentId);
    formData.append("photo", photo);

    formData.append("Width", photosInfo.Width);
    formData.append("Height", photosInfo.Height);
    formData.append("Make", photosInfo.Make);
    formData.append("Model", photosInfo.Model);
    formData.append("XResolution", photosInfo.XResolution);
    formData.append("YResolution", photosInfo.YResolution);
    formData.append("ApertureValue", photosInfo.ApertureValue.replaceAll('.', ','));
    formData.append("ISOSpeedRatings", photosInfo.ISOSpeedRatings);
    formData.append("FocalLength", photosInfo.FocalLength);
    formData.append("FocalLengthIn35mmFilm", photosInfo.FocalLengthIn35mmFilm);

    return await sendReq(method.post, url.createPhoto, formData);
}



async function serverGetNews(data, part) {
    const formData = new FormData();
    formData.append("photographerId", 0);
    formData.append("typeContent", data.typeContent);

    for (let categoryId of data.categoriesIds) {
        formData.append("categoriesIds", categoryId);
    }

    const currentUrl = `${url.getNews}?part=${part}`;
    return await sendReq(method.post, currentUrl, formData);
}

async function serverGetOthers(data, part) {
    const formData = new FormData();
    formData.append("typeSorting", data.typeSorting);
    formData.append("periodSorting", data.periodSorting);
    formData.append("countLikeSorting", data.countLikeSorting);
    formData.append("typeContent", data.typeContent);

    for (let categoryId of data.categoriesIds) {
        formData.append("categoriesIds", categoryId);
    }

    const currentUrl = `${url.getOthers}?part=${part}`;
    return await sendReq(method.post, currentUrl, formData);
}


async function serverCheckSubscription(photographerId) {
    const currentUrl = `${url.checkSubscription}?photographerId=${photographerId}`;
    return await sendReqWithoutBody(method.get, currentUrl);
}

async function serverCreateSubscription(photographerId) {
    const formData = new FormData();
    formData.append("photographerId", photographerId);

    return await sendReq(method.post, url.createSubscription, formData);
}

async function serverDeleteSubscription(photographerId) {
    const formData = new FormData();
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
    const formData = new FormData();
    formData.append("contentId", contentId);

    return await sendReq(method.post, url.createLike, formData);
}

async function serverDeleteLike(contentId) {
    const formData = new FormData();
    formData.append("contentId", contentId);

    return await sendReq(method.delete, url.deleteLike, formData);
}




async function serverCreateFavourite(contentId) {
    const formData = new FormData();
    formData.append("contentId", contentId);

    return await sendReq(method.post, url.createFavourite, formData);
}

async function serverDeleteFavourite(contentId) {
    const formData = new FormData();
    formData.append("contentId", contentId);

    return await sendReq(method.delete, url.deleteFavourite, formData);
}




async function serverGetNewContentComments(contentId, startTime) {
    const currentUrl = `${url.getNewContentComments}?contentId=${contentId}&startTime=${startTime}`;
    return await sendReqWithoutBody(method.get, currentUrl);
}

async function serverCreateComment(text, contentId) {
    const formData = new FormData();
    formData.append("text", text);
    formData.append("contentId", contentId);

    return await sendReq(method.post, url.createComment, formData);
}

async function serverDeleteComment(id) {
    const formData = new FormData();
    formData.append("id", id);

    return await sendReq(method.delete, url.deleteComment, formData);
}



async function serverGetAllComplaintsBase() {
    return await sendReqWithoutBody(method.get, url.getAllComplaintsBase);
}

async function serverCreateComplaint(text, complaintBaseId, contentId) {
    const formData = new FormData();
    formData.append("text", text);
    formData.append("complaintBaseId", complaintBaseId);
    formData.append("contentId", contentId);

    return await sendReq(method.post, url.createComplaint, formData);
}



async function serverGetPhotoInfo(photoId) {
    const currentUrl = `${url.getPhotoInfo}?photoId=${photoId}`;
    return await sendReqWithoutBody(method.get, currentUrl);
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
