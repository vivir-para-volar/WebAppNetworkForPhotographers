const typeTabs = { photographers: 'photographersTab', posts: 'postsTab', blogs: 'blogsTab' }

let activeTab = typeTabs.photographers;
let searchData = null;

let partSearchPhotographers = 0;
let partSearchPosts = 0;
let partSearchBlogs = 0;


const btnMoreSearchPhotographers = document.getElementById("btnMoreSearchPhotographers");
const btnMoreSearchPosts = document.getElementById("btnMoreSearchPosts");
const btnMoreSearchBlogs = document.getElementById("btnMoreSearchBlogs");

const listSearchPhotographers = document.getElementById("listSearchPhotographers");
const listSearchPosts = document.getElementById("listSearchPosts");
const listSearchBlogs = document.getElementById("listSearchBlogs");

const emptySearchPhotographers = document.getElementById("emptySearchPhotographers");
const emptySearchPosts = document.getElementById("emptySearchPosts");
const emptySearchBlogs = document.getElementById("emptySearchBlogs");



const tabBtns = document.querySelectorAll('.nav-tabs li button');
tabBtns.forEach(btn => {
    btn.addEventListener('click', () => {
        activeTab = btn.id;

        switch (activeTab) {
            case typeTabs.photographers:
                if (!listSearchPhotographers.innerHTML && searchData) {
                    startTypeSearch();
                }
                break;
            case typeTabs.posts:
                if (!listSearchPosts.innerHTML && searchData) {
                    startTypeSearch();
                }
                break;
            case typeTabs.blogs:
                if (!listSearchBlogs.innerHTML && searchData) {
                    startTypeSearch();
                }
                break;
        }
    });
});


function startSearch() {
    const inputSearchData = document.getElementById("searchData");
    const text = inputSearchData.value;

    if (text.length < 2) {
        inputSearchData.reportValidity();
        return false;
    }

    searchData = text;

    partSearchPhotographers = 0;
    partSearchPosts = 0;
    partSearchBlogs = 0;

    listSearchPhotographers.innerHTML = '';
    listSearchPosts.innerHTML = '';
    listSearchBlogs.innerHTML = '';

    startTypeSearch();
}


async function startTypeSearch() {
    switch (activeTab) {
        case typeTabs.photographers:
            emptySearchPhotographers.style.display = "none";
            btnMoreSearchPhotographers.style.display = "block";

            await moreSearchPhotographers();

            if (!listSearchPhotographers.innerHTML) {
                emptySearchPhotographers.style.display = "block";
                btnMoreSearchPhotographers.style.display = "none";
            }

            break;

        case typeTabs.posts:
            emptySearchPosts.style.display = "none";
            btnMoreSearchPosts.style.display = "block";

            await moreSearchPosts();

            if (!listSearchPosts.innerHTML) {
                emptySearchPosts.style.display = "block";
                btnMoreSearchPosts.style.display = "none";
            }

            break;

        case typeTabs.blogs:
            emptySearchBlogs.style.display = "none";
            btnMoreSearchBlogs.style.display = "block";

            await moreSearchBlogs();

            if (!listSearchBlogs.innerHTML) {
                emptySearchBlogs.style.display = "block";
                btnMoreSearchBlogs.style.display = "none";
            } 

            break;
    }
}


async function moreSearchPhotographers() {
    partSearchPhotographers++;

    const res = await serverSearchPhotographers(searchData, partSearchPhotographers);
    if (!res) return false;

    addPhotographersForList(res.data, listSearchPhotographers, btnMoreSearchPhotographers);
}

async function moreSearchPosts() {
    partSearchPosts++;

    const res = await serverSearchPosts(searchData, partSearchPosts);
    if (!res) return false;

    addContentsForList(res.data, listSearchPosts, btnMoreSearchPosts);
}

async function moreSearchBlogs() {
    partSearchBlogs++;

    const res = await serverSearchBlogs(searchData, partSearchBlogs);
    if (!res) return false;

    addContentsForList(res.data, listSearchBlogs, btnMoreSearchBlogs);
}