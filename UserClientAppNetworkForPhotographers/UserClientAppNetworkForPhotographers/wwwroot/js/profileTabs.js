let partProfilePosts = 1;
let partProfileBlogs = 1;

let isUser;
let photographerId;


const btnMoreProfilePosts = document.getElementById("btnMoreProfilePosts");
const btnMoreProfileBlogs = document.getElementById("btnMoreProfileBlogs");

const listProfilePosts = document.getElementById("listProfilePosts");
const listProfileBlogs = document.getElementById("listProfileBlogs");



function initProfileTabs(countPosts, countBlogs, argIsUser, argPhotographerId) {
    isUser = argIsUser;
    photographerId = argPhotographerId;

    if (btnMoreProfilePosts && countPosts < countInPart) {
        btnMoreProfilePosts.style.display = "none";
    }
    if (btnMoreProfileBlogs && countBlogs < countInPart) {
        btnMoreProfileBlogs.style.display = "none";
    }
}


async function moreProfilePosts() {
    partProfilePosts++;

    let res;
    if (isUser) {
        res = await serverGetUserPosts(partProfilePosts);
    }
    else {
        res = await serverGetPhotographerPosts(photographerId, partProfilePosts);
    }
    if (!res) return false;

    addContentsForList(res.data, listProfilePosts, btnMoreProfilePosts);
}

async function moreProfileBlogs() {
    partProfileBlogs++;

    if (isUser) {
        res = await serverGetUserBlogs(partProfileBlogs);
    }
    else {
        res = await serverGetPhotographerBlogs(photographerId, partProfileBlogs);
    }
    if (!res) return false;

    addContentsForList(res.data, listProfileBlogs, btnMoreProfileBlogs);
}