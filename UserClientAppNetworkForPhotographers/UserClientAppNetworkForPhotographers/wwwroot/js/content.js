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



// Modal window for photographers who have put a like

const modalAllLikes = document.getElementById('modalAllLikes');

modalAllLikes.addEventListener('show.bs.modal', async function (event) {
    const button = event.relatedTarget;
    contentId = button.getAttribute('data-bs-whatever');

    await getAllContentLikes(contentId);
});

async function getAllContentLikes(contentId) {
    const res = await serverGetAllContentLikes(contentId);

    if (res) {
        const photographers = res.data;

        const parent = document.getElementById("modalAllLikesBody");

        let html = "";
        for (let photographer of photographers) {
            html += createPhotographerInModalAllLikes(photographer);
        }

        parent.innerHTML = html;

        const hModal = document.querySelector("#modalAllLikesHeader>h5");
        const count = photographers.length;
        const form = getFormLike(count);
        hModal.innerHTML = `${count} ${form}`;
    }
}

function createPhotographerInModalAllLikes(photographer) {
    let photo;
    if (photographer.photoProfile == null) {
        photo = `<div class="divForPhoto"><img class="profilePhotoMini" src="${clientUrl}/image/emptyProfile.png"></div>`;
    }
    else {
        photo = `<div class="divForPhoto">
                    <img class="profilePhotoMini" src="${clientUrl}/General/GetPhotographerPhoto?name=${photographer.photoProfile}">
                 </div>`;
    }

    const html =
        `<div class="containerParentFlex pb-3">
            <a href="/Profiles/Photographer/${photographer.id}">${photo}</a>

            <div>
                <a href="/Profiles/Photographer/${photographer.id}">
                    <p><strong>${photographer.username}</strong></p>
                    <p>${photographer.name ?? ""}</p>
                </a>
            </div>
        </div>`;

    return html;
}



// Modal window for deleting content

const modalDeleteContent = document.getElementById('modalDeleteContent');
modalDeleteContent.addEventListener('show.bs.modal', function (event) {
    const button = event.relatedTarget;
    deleteContentId = button.getAttribute('data-bs-whatever');

    var btnDeleteContent = document.getElementById("btnDeleteContent");
    btnDeleteContent.href = `/Contents/Delete/${deleteContentId}`;
});