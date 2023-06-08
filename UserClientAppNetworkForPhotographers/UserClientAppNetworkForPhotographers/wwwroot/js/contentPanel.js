async function createLike(contentId) {
    const res = await serverCreateLike(contentId);
    if (!res) return false;

    document.getElementById("btnCreateLike" + contentId).style.display = "none";
    document.getElementById("btnDeleteLike" + contentId).style.display = "inline-block";

    const aCountLikes = document.getElementById("countLikes" + contentId);
    aCountLikes.innerHTML = +aCountLikes.innerHTML + 1;
}

async function deleteLike(contentId) {
    const res = await serverDeleteLike(contentId);
    if (!res) return false;

    document.getElementById("btnCreateLike" + contentId).style.display = "inline-block";
    document.getElementById("btnDeleteLike" + contentId).style.display = "none";

    const aCountLikes = document.getElementById("countLikes" + contentId);
    aCountLikes.innerHTML = +aCountLikes.innerHTML - 1;
}



async function createFavourite(contentId) {
    const res = await serverCreateFavourite(contentId);
    if (!res) return false;

    document.getElementById("btnCreateFavourite" + contentId).style.display = "none";
    document.getElementById("btnDeleteFavourite" + contentId).style.display = "inline-block";

    const aCountFavourites = document.getElementById("countFavourites" + contentId);
    aCountFavourites.innerHTML = +aCountFavourites.innerHTML + 1;
}

async function deleteFavourite(contentId) {
    const res = await serverDeleteFavourite(contentId);
    if (!res) return false;

    document.getElementById("btnCreateFavourite" + contentId).style.display = "inline-block";
    document.getElementById("btnDeleteFavourite" + contentId).style.display = "none";

    const aCountFavourites = document.getElementById("countFavourites" + contentId);
    aCountFavourites.innerHTML = +aCountFavourites.innerHTML - 1;
}



// Modal window for photographers who have put a like

const modalAllLikes = document.getElementById('modalAllLikes');
modalAllLikes.addEventListener('show.bs.modal', async function (event) {
    const button = event.relatedTarget;
    const contentId = button.getAttribute('data-bs-whatever');

    const res = await serverGetAllContentLikes(contentId);
    if (!res) return false;

    const photographers = res.data;

    const parent = document.getElementById("modalAllLikesBody");
    addPhotographersForModal(photographers, parent);


    const hModal = document.querySelector("#modalAllLikesHeader>h5");
    const count = photographers.length;
    const form = getFormLike(count);
    hModal.innerHTML = `${count} ${form}`;
});



// Modal window for deleting content

const modalDeleteContent = document.getElementById('modalDeleteContent');
modalDeleteContent.addEventListener('show.bs.modal', function (event) {
    const button = event.relatedTarget;
    const deleteContentId = button.getAttribute('data-bs-whatever');

    const btnDeleteContent = document.getElementById("btnDeleteContent");
    btnDeleteContent.href = `/Contents/Delete/${deleteContentId}`;
});



// Modal window for creating complaint

let complaintContentId;

const modalCreateComplaint = document.getElementById('modalCreateComplaint');
modalCreateComplaint.addEventListener('show.bs.modal', async function (event) {
    const button = event.relatedTarget;
    complaintContentId = button.getAttribute('data-bs-whatever');

    const res = await serverGetAllComplaintsBase();
    if (!res) return false;

    const complaintsBase = res.data;

    const parent = document.getElementById("divComplaints");

    let html =
        `<div class="form-check mt-1">
            <input type="radio" id="complaintBase${complaintsBase[0].id}" name="complaintBaseId" 
                   value="${complaintsBase[0].id}" class="form-check-input" checked>
            <label for="complaintBase${complaintsBase[0].id}" class="form-check-label">${complaintsBase[0].name}</label>
        </div>`;
    for (let i = 1; i < complaintsBase.length; i++) {
        html +=
            `<div class="form-check mt-1">
                <input type="radio" id="complaintBase${complaintsBase[i].id}" name="complaintBaseId" 
                       value="${complaintsBase[i].id}" class="form-check-input">
                <label for="complaintBase${complaintsBase[i].id}" class="form-check-label">${complaintsBase[i].name}</label>
            </div>`;
    }

    parent.innerHTML = html;
});

async function createComplaint() {
    const inputText = document.getElementById("textComplaint");
    const text = inputText.value;

    if (text && text.length < 16) {
        inputText.reportValidity();
        return false;
    }

    let complaintBaseId;
    const radioButtons = document.getElementsByName("complaintBaseId");
    for (let radioButton of radioButtons) {
        if (radioButton.checked) {
            complaintBaseId = radioButton.value;
            break;
        }
    }

    const res = await serverCreateComplaint(text, complaintBaseId, complaintContentId);
    if (!res) return false;

    const myModal = bootstrap.Modal.getInstance(modalCreateComplaint);
    myModal.hide();

    inputText.value = "";
}



// Modal window for photo info

const spanWidth = document.getElementById('spanWidth');
const spanHeight = document.getElementById('spanHeight');
const spanMake = document.getElementById('spanMake');
const spanModel = document.getElementById('spanModel');
const spanXResolution = document.getElementById('spanXResolution');
const spanYResolution = document.getElementById('spanYResolution');
const spanApertureValue = document.getElementById('spanApertureValue');
const spanISOSpeedRatings = document.getElementById('spanISOSpeedRatings');
const spanFocalLength = document.getElementById('spanFocalLength');
const spanFocalLengthIn35mmFilm = document.getElementById('spanFocalLengthIn35mmFilm');

const modalPhotoInfo = document.getElementById('modalPhotoInfo');
modalPhotoInfo.addEventListener('show.bs.modal', async function (event) {
    const button = event.relatedTarget;
    const photoId = button.getAttribute('data-bs-whatever');

    const res = await serverGetPhotoInfo(photoId);
    if (!res) return false;

    const photoInfo = res.data;

    console.log(photoInfo);

    spanWidth.innerHTML = photoInfo.width + " px";
    spanHeight.innerHTML = photoInfo.height + " px";
    spanMake.innerHTML = photoInfo.make ?? "-";
    spanModel.innerHTML = photoInfo.model ?? "-";
    spanXResolution.innerHTML = photoInfo.xResolution ? `${photoInfo.xResolution} точек на дюйм` : "-";
    spanYResolution.innerHTML = photoInfo.yResolution ? `${photoInfo.yResolution} точек на дюйм` : "-";
    spanApertureValue.innerHTML = photoInfo.apertureValue ? `f/${photoInfo.apertureValue }` : "-";
    spanISOSpeedRatings.innerHTML = photoInfo.isoSpeedRatings ? `ISO-${photoInfo.isoSpeedRatings}` : "-";
    spanFocalLength.innerHTML = photoInfo.focalLength ? `${photoInfo.xResolution} мм` : "-";
    spanFocalLengthIn35mmFilm.innerHTML = photoInfo.focalLengthIn35mmFilm ?? "-";
});




// Modal window for big photo

const modalBigPhotoImg = document.getElementById('modalBigPhotoImg');

const modalBigPhoto = document.getElementById('modalBigPhoto');
modalBigPhoto.addEventListener('show.bs.modal', function (event) {
    const button = event.relatedTarget;

    const photoSrc = button.getAttribute('data-bs-whatever');
    modalBigPhotoImg.src = photoSrc;
});