function addContentsForList(addContentsList, parentAdd, btnAdd) {
    if (addContentsList.length < countInPart) {
        btnAdd.style.display = "none";
    }
    if (addContentsList.length == 0) return false;

    let html = "";
    for (let contentItem of addContentsList) {
        html += `<div class="mb-4">`;
        html += getContentItem(contentItem);
        html += `</div>`;
    }

    parentAdd.innerHTML += html;
}

function getContentItem(contentItem) {
    let photo;
    if (contentItem.photographer.photoProfile == null) {
        photo = `<div class="divForPhoto"><img class="profilePhotoMini" src="${clientUrl}/image/emptyProfile.png"></div>`;
    }
    else {
        photo = `<div class="divForPhoto">
                    <img class="profilePhotoMini" src="${clientUrl}/Common/GetPhotographerPhoto?name=${contentItem.photographer.photoProfile}">
                 </div>`;
    }

    let date = new Date(contentItem.createdAt);
    let dateStringFormatted = date.toLocaleDateString() + ' ' + date.toLocaleTimeString();

    let blocked = "";
    if (contentItem.status == statusContent.blocked) {
        blocked =
            `<div class="divFlexEnd pt-2">
                <button class="btnImg ps-1" data-bs-toggle="modal" data-bs-target="#modalBlocked">
                    <img class="contentImg" src="${clientUrl}/image/content/block.png">
                </button>
            </div>`;
    }

    let html =
        `<div class="border border-3 rounded box-shadow ps-2 pe-2">
            <div class="containerParentFlex p-2">
                <a href="/Profiles/Photographer/${contentItem.photographer.id}">${photo}</a>
                
                <div>
                    <a href="/Profiles/Photographer/${contentItem.photographer.id}">
                        <p><strong>${contentItem.photographer.username}</strong></p>
                    </a>
                    <div class="textDate mt-1">${dateStringFormatted}</div>
                </div>

                ${blocked}
            </div>

            <div class="border-top border-bottom border-2 p-2">`;

    if (contentItem.type === typeContent.post) {
        if (contentItem.photos.count == 1) {
            html +=
                `<div>
                    <img class="photoSliderMini" src="${clientUrl}/Common/GetContentPhoto?contentId=${contentItem.id}&name=${contentItem.photos[0].photoContent}">
                </div>`;
        }
        else {
            html +=
                `<div id="carouselControls${contentItem.id}" class="carousel carousel-dark slide" data-bs-touch="false" data-bs-interval="false">
                    <div class="carousel-inner">

                        <div class="carousel-item active">
                            <img class="photoSliderMini" src="${clientUrl}/Common/GetContentPhoto?contentId=${contentItem.id}&name=${contentItem.photos[0].photoContent}">
                        </div>`;

            for (var i = 1; i < contentItem.photos.length; i++) {
                html +=
                    `   <div class="carousel-item">
                            <img class="photoSliderMini" src="${clientUrl}/Common/GetContentPhoto?contentId=${contentItem.id}&name=${contentItem.photos[i].photoContent}">
                        </div>`;
            }

            html +=
                `   </div>

                    <button class="carousel-control-prev" type="button" data-bs-target="#carouselControls${contentItem.id}" data-bs-slide="prev">
                        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                        <span class="visually-hidden">Предыдущий</span>
                    </button>

                    <button class="carousel-control-next" type="button" data-bs-target="#carouselControls${contentItem.id}" data-bs-slide="next">
                        <span class="carousel-control-next-icon" aria-hidden="true"></span>
                        <span class="visually-hidden">Следующий</span>
                    </button>
                </div>
                
                <div class="mt-4">
                    <pre>${contentItem.title}</pre>
                    <div class="pt-3 pb-1">
                        <a class="link-dark pt-3 pb-1" href="/Contents/Post/${contentItem.id}">Подробнее...</a>
                    </div>
                </div>`;
        }
    } else {
        html +=
            `   <div>
                    <img class="photoSliderMini" src="${clientUrl}/Common/GetBlogMainPhoto?name=${contentItem.blogMainPhoto}">
                </div>

                <div class="mt-4">
                    <pre>${contentItem.title}</pre>
                    <div class="pt-3 pb-1">
                        <a class="link-dark pt-3 pb-1" href="/Contents/Blog/${contentItem.id}">Читать...</a>
                    </div>
                </div>`;
    }

    html +=
        `   </div>

            <div class="p-2">
                ${getContentPanel(contentItem)}
            </div>

        </div>`;

    return html;
}

function getContentPanel(contentItem) {

    let likePanel;
    if (contentItem.isLike == true) {
        likePanel =
            `<button id="btnCreateLike${contentItem.id}" class="btnImg"
                    onclick="createLike(${contentItem.id})" style="display:none">
                <img class="contentImg" src="${clientUrl}/image/content/heart.png">
            </button>

            <button id="btnDeleteLike${contentItem.id}" class="btnImg" onclick="deleteLike(${contentItem.id})">
                <img class="contentImg" src="${clientUrl}/image/content/heartLike.png">
            </button>`;
    }
    else {
        likePanel =
            `<button id="btnCreateLike${contentItem.id}" class="btnImg" onclick="createLike(${contentItem.id})">
                <img class="contentImg" src="${clientUrl}/image/content/heart.png">
            </button>

            <button id="btnDeleteLike${contentItem.id}" class="btnImg"
                    onclick="deleteLike(${contentItem.id})" style="display:none">
                <img class="contentImg" src="${clientUrl}/image/content/heartLike.png">
            </button>`;
    }

    let commentPanel;
    if (contentItem.type == typeContent.post) {
        commentPanel =
            `<a type="button" class="btnImg" href="/Contents/Post/${contentItem.id}">
                <img class="contentImg" src="${clientUrl}/image/content/comment.png">
                <span class="ps-1">${contentItem.countComments}</span>
            </a>`;
    }
    else {
        commentPanel =
            `<a type="button" class="btnImg" href="/Contents/Blog/${contentItem.id}">
                <img class="contentImg" src="${clientUrl}/image/content/comment.png">
                <span class="ps-1">${contentItem.countComments}</span>
            </a>`;
    }

    let favouritePanel;
    if (contentItem.isFavourite == true) {
        favouritePanel =
            `<button id="btnCreateFavourite${contentItem.id}" class="btnImg"
                    onclick="createFavourite(${contentItem.id})" style="display:none">
                <img class="contentImg" src="${clientUrl}/image/content/star.png">
            </button>

            <button id="btnDeleteFavourite${contentItem.id}" class="btnImg" onclick="deleteFavourite(${contentItem.id})">
                <img class="contentImg" src="${clientUrl}/image/content/starFavourite.png">
            </button>`;
    }
    else {
        favouritePanel =
            `<button id="btnCreateFavourite${contentItem.id}" class="btnImg" onclick="createFavourite(${contentItem.id})">
                <img class="contentImg" src="${clientUrl}/image/content/star.png">
            </button>

            <button id="btnDeleteFavourite${contentItem.id}" class="btnImg"
                    onclick="deleteFavourite(${contentItem.id})" style="display:none">
                <img class="contentImg" src="${clientUrl}/image/content/starFavourite.png">
            </button>`;
    }

    let endPanel;
    if (contentItem.photographer.id == contentItem.appUserId) {
        endPanel =
            `<button class="btnImg" data-bs-toggle="modal"
                    data-bs-target="#modalDeleteContent" data-bs-whatever="${contentItem.id}">
                <img class="contentImg" src="${clientUrl}/image/content/deleteWhite.png">
            </button>`;
    }
    else {
        endPanel =
            `<button class="btnImg" data-bs-toggle="modal"
                    data-bs-target="#modalCreateComplaint" data-bs-whatever="${contentItem.id}">
                <img class="contentImg" src="${clientUrl}/image/content/complaint.png">
            </button>`;
    }

    let html =
        `<div class="containerParentFlexMany">
            <div class="pe-3">
                ${likePanel}

                <button id="countLikes${contentItem.id}" class="btnImg ps-1"
                        data-bs-toggle="modal" data-bs-target="#modalAllLikes" data-bs-whatever="${contentItem.id}">
                    ${contentItem.countLikes}
                </button>
            </div>

            <div class="pe-3">${commentPanel}</div>

            <div>
                ${favouritePanel}
                <span id="countFavourites${contentItem.id}" class="ps-1">${contentItem.countFavourites}</span>
            </div>

            <div class="divFlexEnd">${endPanel}</div>
        </div>`

    return html;
}
