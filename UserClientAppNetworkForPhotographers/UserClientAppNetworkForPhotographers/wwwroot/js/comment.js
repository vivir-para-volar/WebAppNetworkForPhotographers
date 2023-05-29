const clientUrl = "https://localhost:7247";
let countComments;

function initCountComments(count) {
    countComments = count;
    setCountComments(countComments);
}

function setCountComments(count) {
    const hCountComments = document.getElementById("countComments");
    const form = getFormComment(count);
    hCountComments.innerHTML = `${count} ${form}`;
}

async function createUserComment(contentId) {
    const inputComment = document.getElementById("textCreateComment");

    const text = inputComment.value;
    if (text.length < 1) {
        alert("Комментарий должен быть не менее 1 символа");
        return false;
    }

    const res = await serverCreateComment(text, contentId);

    if (res) {
        const comment = res.data;

        inputComment.value = '';

        const parent = document.getElementById("divComments");

        let photo;
        if (comment.photographer.photoProfile == null) {
            photo = `<div class="divForPhoto"><img class="profilePhotoMini" src="~/image/emptyProfile.png"></div>`;
        }
        else {
            photo = `<div class="divForPhoto">
                         <img class="profilePhotoMini" src="${clientUrl}/General/GetPhotographerPhoto?name=${comment.photographer.photoProfile}">
                     </div>`;
        }

        let date = new Date(comment.createdAt);
        var dateStringFormatted = date.toLocaleDateString() + ' ' + date.toLocaleTimeString();

        const html =
            `<div id="divComment${comment.id}" class="divComment containerParentFlex pt-3 pb-3">
                <a href="/Profiles/Photographer/${comment.photographer.id}">
                    ${photo}
                </a>

                <div class="divAfterPhoto">
                    <a href="/Profiles/Photographer/${comment.photographer.id}">
                        <p><strong>${comment.photographer.username}</strong></p>
                    </a>

                    <p class="textComment mt-1">${comment.text}</p>
                    <div class="textDate mt-1">${dateStringFormatted}</div>
                </div>

                <div class="divDeleteComment pt-2">
                    <button class="btnImg" data-bs-toggle="modal"
                            data-bs-target="#modalDeleteComment" data-bs-whatever="${comment.id}">
                        <img class="deleteCommentImg" src="${clientUrl}/image/delete.png">
                    </button>
                </div>
            </div>`;

        parent.innerHTML = parent.innerHTML + html;

        const divCreateComment = document.getElementById("divCreateComment");
        divCreateComment.scrollIntoView();

        countComments++;
        setCountComments(countComments);
    }
}

async function deleteUserComment(id) {
    const res = await serverDeleteComment(id);

    if (res) {
        var divCommen = document.getElementById(`divComment${id}`);

        if (divCommen.parentNode) {
            divCommen.parentNode.removeChild(divCommen);
        }

        countComments--;
        setCountComments(countComments);
    }
}