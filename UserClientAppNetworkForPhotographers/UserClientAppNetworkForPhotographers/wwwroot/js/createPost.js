class FileInfo {
    Width = 0;
    Height = 0;

    Make = '';
    Model = '';
    XResolution = 0;
    YResolution = 0;
    ApertureValue = 0;
    ISOSpeedRatings = 0;
    FocalLength = 0;
    FocalLengthIn35mmFilm = 0;
}


const modalCreatePhotoInfo = document.getElementById('modalCreatePhotoInfo');
const bootstrapModalCreatePhotoInfo = new bootstrap.Modal(modalCreatePhotoInfo);

const inputWidth = document.getElementById('inputWidth');
const inputHeight = document.getElementById('inputHeight');
const inputMake = document.getElementById('inputMake');
const inputModel = document.getElementById('inputModel');
const inputXResolution = document.getElementById('inputXResolution');
const inputYResolution = document.getElementById('inputYResolution');
const inputApertureValue = document.getElementById('inputApertureValue');
const inputISOSpeedRatings = document.getElementById('inputISOSpeedRatings');
const inputFocalLength = document.getElementById('inputFocalLength');
const inputFocalLengthIn35mmFilm = document.getElementById('inputFocalLengthIn35mmFilm');


const arrPhotos = [];
const arrPhotosInfo = [];

let currentIndex = 0;



let photographerId;

function initCreateContent(argPhotographerId) {
    photographerId = argPhotographerId;
}



const inputContentTitle = document.getElementById("inputContentTitle");
const checkboxesCategories = document.querySelectorAll('#categoriesCreateContent input[type="checkbox"]');

const textEmptyCategories = document.getElementById("emptyCategories");
const textEmptyPhotos = document.getElementById("emptyPhotos");

const dataCreatePost = {
    title: '',
    photographerId: 0,
    categoriesIds: []
}


async function createPost() {
    textEmptyCategories.className = '';
    textEmptyPhotos.className = '';

    if (arrPhotos.length == 0) {
        textEmptyPhotos.classList.add('text-danger');
        window.scrollTo({
            top: 0,
            left: 0,
            behavior: 'smooth'
        });
        return false;
    }


    const contentTitle = inputContentTitle.value;

    if (contentTitle.length < 4) {
        inputContentTitle.reportValidity();
        return false;
    }

    dataCreatePost.categoriesIds = [];
    for (let i = 0; i < checkboxesCategories.length; i++) {
        if (checkboxesCategories[i].checked) {
            dataCreatePost.categoriesIds.push(checkboxesCategories[i].value);
        }
    }

    if (dataCreatePost.categoriesIds.length == 0) {
        textEmptyCategories.classList.add('text-danger');
        inputContentTitle.scrollIntoView();
        return false;
    }


    dataCreatePost.title = contentTitle;
    dataCreatePost.photographerId = photographerId;

    const res = await serverCreatePost(dataCreatePost);
    if (!res) return false;

    const content = res.data;

    for (let i = 0; i < 5; i++) {
        if (arrPhotos[i]) {
            await serverCreatePhoto(content.id, arrPhotos[i], arrPhotosInfo[i]);
        }
    }

    window.location.href = '/Profiles';
}




window.onload = function () {
    for (let i = 0; i < 5; i++) {
        const photoInput = document.getElementById(`photos${i}Input`);

        photoInput.addEventListener('change', function () {

            const file = this.files[0];

            if (file.size > maxPhotoSize) {
                photoInput.value = '';
                return false;
            }

            currentIndex = i;

            arrPhotos[i] = file;
            arrPhotosInfo[i] = new FileInfo();


            const image = new Image();
            image.addEventListener('load', () => {
                inputWidth.value = image.naturalWidth;
                inputHeight.value = image.naturalHeight;
            });
            const url = URL.createObjectURL(file);
            image.src = url;

            document.querySelector(`#photos${i}Ready>img`).setAttribute('src', url);
            document.getElementById(`photos${i}`).style.display = "none";
            document.getElementById(`photos${i}Ready`).style.display = "block";

            const reader = new FileReader();
            reader.addEventListener('load', () => {
                const imageData = reader.result;
                const exifData = EXIF.readFromBinaryFile(imageData);

                inputMake.value = exifData?.Make ?? '';
                inputModel.value = exifData?.Model ?? '';
                inputXResolution.value = exifData?.XResolution ?? '';
                inputYResolution.value = exifData?.YResolution ?? '';
                inputApertureValue.value = exifData?.ApertureValue?.toFixed(2) ?? '';
                inputISOSpeedRatings.value = exifData?.ISOSpeedRatings ?? '';
                inputFocalLength.value = exifData?.FocalLength ?? '';
                inputFocalLengthIn35mmFilm.value = exifData?.FocalLengthIn35mmFilm ?? '';
            });
            reader.readAsArrayBuffer(file);


            const modal = bootstrap.Modal.getInstance(modalCreatePhotoInfo);
            modal.show();
        });
    }
};


function createPhotoInfo() {
    arrPhotosInfo[currentIndex].Width = inputWidth.value;
    arrPhotosInfo[currentIndex].Height = inputHeight.value;
    arrPhotosInfo[currentIndex].Make = inputMake.value;
    arrPhotosInfo[currentIndex].Model = inputModel.value;
    arrPhotosInfo[currentIndex].XResolution = inputXResolution.value;
    arrPhotosInfo[currentIndex].YResolution = inputYResolution.value;
    arrPhotosInfo[currentIndex].ApertureValue = inputApertureValue.value;
    arrPhotosInfo[currentIndex].ISOSpeedRatings = inputISOSpeedRatings.value;
    arrPhotosInfo[currentIndex].FocalLength = inputFocalLength.value;
    arrPhotosInfo[currentIndex].FocalLengthIn35mmFilm = inputFocalLengthIn35mmFilm.value;
}