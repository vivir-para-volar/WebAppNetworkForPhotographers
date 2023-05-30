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
    if (!res) return false;

    const comments = res.data;

    for (let comment of comments) {
        createCommentInHtml(comment, false);

        countComments++;
        setCountComments(countComments);
    }
}


async function createUserComment() {
    const inputText = document.getElementById("textCreateComment");
    const text = inputText.value;

    if (text.length < 1) {
        return false;
    }

    const res = await serverCreateComment(text, contentId);
    if (!res) return false;

    const comment = res.data;

    inputText.value = '';

    createCommentInHtml(comment, true);

    countComments++;
    setCountComments(countComments);
}


function createCommentInHtml(comment, isUser) {
    const parent = document.getElementById("divComments");

    let photo;
    if (comment.photographer.photoProfile == null) {
        photo = `<div class="divForPhoto"><img class="profilePhotoMini" src="${clientUrl}/image/emptyProfile.png"></div>`;
    }
    else {
        photo = `<div class="divForPhoto">
                    <img class="profilePhotoMini" src="${clientUrl}/Common/GetPhotographerPhoto?name=${comment.photographer.photoProfile}">
                 </div>`;
    }

    let date = new Date(comment.createdAt);
    let dateStringFormatted = date.toLocaleDateString() + ' ' + date.toLocaleTimeString();

    let divDeleteComment = "";
    if (isUser) {
        divDeleteComment =
            `<div class="divFlexEnd pt-2">
                <button class="btnImg" data-bs-toggle="modal"
                        data-bs-target="#modalDeleteComment" data-bs-whatever="${comment.id}">
                    <img class="deleteCommentImg" src="${clientUrl}/image/content/delete.png">
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

                    <pre class="textComment mt-1">${comment.text}</pre>
                    <div class="textDate mt-1">${dateStringFormatted}</div>
                </div>

                ${divDeleteComment}
            </div>`;

    parent.innerHTML += html;

    if (isUser) downPage();
}




// Modal window for deleting comment

let deleteCommentId;

const modalDeleteComment = document.getElementById('modalDeleteComment');
modalDeleteComment.addEventListener('show.bs.modal', function (event) {
    const button = event.relatedTarget;
    deleteCommentId = button.getAttribute('data-bs-whatever');
});

async function deleteUserComment() {
    const res = await serverDeleteComment(deleteCommentId);
    if (!res) return false;

    var divCommen = document.getElementById(`divComment${deleteCommentId}`);

    if (divCommen.parentNode) {
        divCommen.parentNode.removeChild(divCommen);
    }

    countComments--;
    setCountComments(countComments);
}