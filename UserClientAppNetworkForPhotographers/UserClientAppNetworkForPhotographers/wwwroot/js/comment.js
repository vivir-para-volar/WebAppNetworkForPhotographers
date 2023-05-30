const clientUrl = "https://localhost:7247";

let checkDate;
let contentId;
let countComments;



function initComments(id, count) {
    checkDate = new Date();

    contentId = id;

    countComments = count;
    setCountComments(countComments);
}

function setCountComments(count) {
    const hCountComments = document.getElementById("countComments");
    const form = getFormComment(count);
    hCountComments.innerHTML = `${count} ${form}`;
}

function downPage() {
    const divCreateComment = document.getElementById("divCreateComment");
    divCreateComment.scrollIntoView();
}


setInterval(checkNewComments, 3000);
async function checkNewComments() {
    const dateStringFormatted = `${checkDate.toLocaleDateString()} ${checkDate.toLocaleTimeString()}.${checkDate.getMilliseconds()}`;
    checkDate = new Date();

    const res = await serverGetNewContentComments(contentId, dateStringFormatted);

    if (res) {
        const comments = res.data;

        for (let comment of comments) {
            createCommentInHtml(comment, false);

            countComments++;
            setCountComments(countComments);
        }
    }
}



async function createUserComment() {
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

        createCommentInHtml(comment, true);

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


function createCommentInHtml(comment, isUser) {
    const parent = document.getElementById("divComments");

    let photo;
    if (comment.photographer.photoProfile == null) {
        photo = `<div class="divForPhoto"><img class="profilePhotoMini" src="${clientUrl}/image/emptyProfile.png"></div>`;
    }
    else {
        photo = `<div class="divForPhoto">
                    <img class="profilePhotoMini" src="${clientUrl}/General/GetPhotographerPhoto?name=${comment.photographer.photoProfile}">
                 </div>`;
    }

    let date = new Date(comment.createdAt);
    let dateStringFormatted = date.toLocaleDateString() + ' ' + date.toLocaleTimeString();

    let divDeleteComment = "";
    if (isUser) {
        divDeleteComment = 
            `<div class="divDeleteComment pt-2">
                <button class="btnImg" data-bs-toggle="modal"
                        data-bs-target="#modalDeleteComment" data-bs-whatever="${comment.id}">
                    <img class="deleteCommentImg" src="${clientUrl}/image/delete.png">
                </button>
            </div>`
    }

    const html =
        `<div id="divComment${comment.id}" class="divComment containerParentFlex pt-3 pb-3">
                <a href="/Profiles/Photographer/${comment.photographer.id}">
                    ${photo}
                </a>

                <div>
                    <a href="/Profiles/Photographer/${comment.photographer.id}">
                        <p><strong>${comment.photographer.username}</strong></p>
                    </a>

                    <p class="textComment mt-1">${comment.text}</p>
                    <div class="textDate mt-1">${dateStringFormatted}</div>
                </div>

                ${divDeleteComment}
            </div>`;

    parent.innerHTML += html;

    if (isUser) downPage();
}




// Модальное окно удаления комментария

let deleteCommentId;

const modalDeleteComment = document.getElementById('modalDeleteComment');
modalDeleteComment.addEventListener('show.bs.modal', function (event) {
    const button = event.relatedTarget;
    deleteCommentId = button.getAttribute('data-bs-whatever');
});

const btnDeleteComment = document.getElementById('btnDeleteComment');
btnDeleteComment.addEventListener("click", async function (event) {
    await deleteUserComment(deleteCommentId);
});