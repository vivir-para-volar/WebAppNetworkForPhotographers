function addPhotographersForModal(addPhotographersList, modalAdd) {
    if (addPhotographersList.length == 0) return false;

    let html = "";

    html += `<div class="p-2">`;
    html += getPhotographerItem(addPhotographersList[0]);
    html += `</div>`;

    for (let i = 1; i < addPhotographersList.length; i++) {
        html += `<div class="border-top p-2">`;
        html += getPhotographerItem(addPhotographersList[i]);
        html += `</div>`;
    }

    modalAdd.innerHTML = html;
}


function addPhotographersForList(addPhotographersList, parentAdd, btnAdd) {
    if (addPhotographersList.length < countInPart) {
        btnAdd.style.display = "none";
    }
    if (addPhotographersList.length == 0) return false;

    let html = "";
    for (let photographerItem of addPhotographersList) {
        html += `<div class="border-bottom p-3">`;
        html += getPhotographerItem(photographerItem);
        html += `</div>`;
    }

    parentAdd.innerHTML += html;
}

function getPhotographerItem(photographerItem) {
    let photo;
    if (photographerItem.photoProfile == null) {
        photo = `<div class="divForPhoto"><img class="profilePhotoMini" src="${clientUrl}/image/emptyProfile.png"></div>`;
    }
    else {
        photo = `<div class="divForPhoto">
                    <img class="profilePhotoMini" src="${clientUrl}/Common/GetPhotographerPhoto?name=${photographerItem.photoProfile}">
                 </div>`;
    }

    const html =
        `<div class="containerParentFlex">
            <a href="/Profiles/Photographer/${photographerItem.id}">${photo}</a>

            <div>
                <a href="/Profiles/Photographer/${photographerItem.id}">
                    <p><strong>${photographerItem.username}</strong></p>
                    <p>${photographerItem.name ?? ""}</p>
                </a>
            </div>
        </div>`;

    return html;
}
