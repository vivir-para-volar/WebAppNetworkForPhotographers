const createType = {
    header: 'h',
    text: 't',
    photo: 'p',
    carousel: 'c',
}

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



const dataCreateBlog = {
    title: '',
    photographerId: 0,
    mainPhoto: '',
    body: [],
    categoriesIds: []
}

const arrPhotos = [];
const arrPhotosInfo = [];
const arrPhotosURL = [];


let endIndex = -1;
let currentIndex = 0;




function initCreateContent(argPhotographerId) {
    dataCreateBlog.photographerId = argPhotographerId;
}


const inputContentTitle = document.getElementById("inputContentTitle");
const checkboxesCategories = document.querySelectorAll('#categoriesCreateContent input[type="checkbox"]');

const textEmptyCategories = document.getElementById("emptyCategories");
const textEmptyMainPhoto = document.getElementById("emptyMainPhoto");

function createFirstPart() {
    textEmptyCategories.className = '';
    textEmptyMainPhoto.className = '';

    if (!dataCreateBlog.mainPhoto) {
        textEmptyMainPhoto.classList.add('text-danger');
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

    dataCreateBlog.title = contentTitle;


    dataCreateBlog.categoriesIds = [];
    for (let i = 0; i < checkboxesCategories.length; i++) {
        if (checkboxesCategories[i].checked) {
            dataCreateBlog.categoriesIds.push(checkboxesCategories[i].value);
        }
    }

    if (dataCreateBlog.categoriesIds.length == 0) {
        textEmptyCategories.classList.add('text-danger');
        inputContentTitle.scrollIntoView();
        return false;
    }

    document.getElementById("divFirstPart").style.display = "none";
    document.getElementById("divSecondPart").style.display = "block";

    document.getElementById("blogTitle").innerHTML = contentTitle;
}


const emptyBlogBody = document.getElementById("emptyBlogBody");

async function createBlog() {
    if (dataCreateBlog.body.length === 0) {
        emptyBlogBody.style.display = "block";
        return false;
    }

    let res = await serverCreateBlog(dataCreateBlog);
    if (!res) return false;

    const content = res.data;

    let arrIndex = -1;
    for (let item of dataCreateBlog.body) {
        if (item.t === createType.photo) {
            arrIndex++;

            res = await serverCreatePhoto(content.id, arrPhotos[arrIndex], arrPhotosInfo[arrIndex]);
            if (!res) return false;

            const photo = res.data;
            item.b = photo.id;
        }
        else if (item.t === createType.carousel) {
            for (let i = 0; i < 5; i++) {
                arrIndex++;

                if (arrPhotos[arrIndex]) {
                    res = await serverCreatePhoto(content.id, arrPhotos[arrIndex], arrPhotosInfo[arrIndex]);
                    if (!res) return false;

                    const photo = res.data;
                    item.b += photo.id + " ";
                }
            }
        }
    }

    res = await serverUpdateBlog({ id: content.id, body: JSON.stringify(dataCreateBlog.body) });
    if (!res) return false;

    window.location.href = '/Profiles';
}



window.onload = function () {
    const mainPhotoInput = document.getElementById('mainPhotoInput');

    mainPhotoInput.addEventListener('change', function () {

        const file = this.files[0];

        if (file.size > maxPhotoSize) {
            mainPhotoInput.value = '';
            return false;
        }

        dataCreateBlog.mainPhoto = file;

        const url = URL.createObjectURL(file);

        document.querySelector(`#mainPhotoReady>img`).setAttribute('src', url);
        document.getElementById("blogMainPhoto").setAttribute('src', url);

        document.getElementById(`mainPhoto`).style.display = "none";
        document.getElementById(`mainPhotoReady`).style.display = "block";
    });



    const modalCreatePhotoInfo = document.getElementById('modalCreatePhotoInfo');
    new bootstrap.Modal(modalCreatePhotoInfo);

    for (let i = 0; i < 6; i++) {
        const photoInput = document.getElementById(`photos${i}Input`);

        photoInput.addEventListener('change', function () {

            const file = this.files[0];

            if (file.size > maxPhotoSize) {
                photoInput.value = '';
                return false;
            }

            if (i == 0) currentIndex = endIndex;
            else currentIndex = endIndex - 5 + i;


            arrPhotos[currentIndex] = file;
            arrPhotosInfo[currentIndex] = new FileInfo();


            const image = new Image();
            image.addEventListener('load', () => {
                inputWidth.value = image.naturalWidth;
                inputHeight.value = image.naturalHeight;
            });
            const url = URL.createObjectURL(file);
            image.src = url;

            arrPhotosURL[currentIndex] = url;

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



const mainCreateButtons = document.getElementById('mainCreateButtons');
const blogBody = document.getElementById('blogBody');


const divCreateHeader = document.getElementById('divCreateHeader');
const inputCreateHeader = document.getElementById('inputCreateHeader');
function showCreateHeader() {
    emptyBlogBody.style.display = "none";

    divCreateHeader.style.display = "block";
    mainCreateButtons.style.display = "none";

    divCreateHeader.querySelector("button:last-child").scrollIntoView();
}
function createHeader() {
    const valueHeader = inputCreateHeader.value;

    if (valueHeader.length < 4) {
        inputCreateHeader.reportValidity();
        return false;
    }

    dataCreateBlog.body.push({ t: createType.header, b: valueHeader });

    blogBody.innerHTML += `<h5 class="mt-4">${valueHeader}</h5>`;

    // Clear
    divCreateHeader.style.display = "none";
    mainCreateButtons.style.display = "block";
    inputCreateHeader.value = '';
}


const divCreateText = document.getElementById('divCreateText');
const inputCreateText = document.getElementById('inputCreateText');
function showCreateText() {
    emptyBlogBody.style.display = "none";

    divCreateText.style.display = "block";
    mainCreateButtons.style.display = "none";

    divCreateText.querySelector("button:last-child").scrollIntoView();
}
function createText() {
    const valueText = inputCreateText.value;

    if (valueText.length < 4) {
        inputCreateText.reportValidity();
        return false;
    }

    dataCreateBlog.body.push({ t: createType.text, b: valueText })

    blogBody.innerHTML += `<div class="mt-3"><pre>${valueText}</pre></div`;

    // Clear
    divCreateText.style.display = "none";
    mainCreateButtons.style.display = "block";
    inputCreateText.value = '';
}


const divCreatePhoto = document.getElementById('divCreatePhoto');
const emptyCreatePhoto = document.getElementById('emptyCreatePhoto');
function showCreatePhoto() {
    emptyBlogBody.style.display = "none";

    divCreatePhoto.style.display = "block";
    mainCreateButtons.style.display = "none";

    endIndex++;
    divCreatePhoto.querySelector("button:last-child").scrollIntoView();
}
function createPhoto() {
    if (!arrPhotos[currentIndex]) {
        emptyCreatePhoto.classList.add('text-danger');
        return false;
    }

    dataCreateBlog.body.push({ t: createType.photo, b: '' })

    blogBody.innerHTML += `<div class="mt-4 pb-2"><img class="photoSlider" src="${arrPhotosURL[currentIndex]}"></div`;

    // Clear
    divCreatePhoto.style.display = "none";
    mainCreateButtons.style.display = "block";

    document.querySelector(`#photos0Ready>img`).setAttribute('src', '#');
    document.getElementById(`photos0`).style.display = "block";
    document.getElementById(`photos0Ready`).style.display = "none";
    document.getElementById(`photos0Input`).value = '';
    emptyCreatePhoto.className = '';
}


const divCreateCarousel = document.getElementById('divCreateCarousel');
const emptyCreateCarousel = document.getElementById('emptyCreateCarousel');
function showCreateCarousel() {
    emptyBlogBody.style.display = "none";

    divCreateCarousel.style.display = "block";
    mainCreateButtons.style.display = "none";

    endIndex += 5;
    divCreateCarousel.querySelector("button:last-child").scrollIntoView();
}
function createCarousel() {
    let countEmpty = 0;
    for (let i = endIndex - 4; i <= endIndex; i++) {
        if (!arrPhotos[i]) countEmpty++;
    }
    if (countEmpty > 3) {
        emptyCreateCarousel.classList.add('text-danger');
        return false;
    }

    dataCreateBlog.body.push({ t: createType.carousel, b: '' })



    let index = endIndex - 4;
    while (!arrPhotos[index]) index++;

    let html =
        `<div id="carouselControls${endIndex}" class="mt-4 pb-2 carousel carousel-dark slide" data-bs-touch="false" data-bs-interval="false">
            <div class="carousel-inner">

                <div class="carousel-item active">
                    <img class="photoSliderMini" src="${arrPhotosURL[index]}">
                </div>`;

    index++;
    for (; index <= endIndex; index++) {
        if (arrPhotos[index]) {
            html +=
                `<div class="carousel-item">
                    <img class="photoSliderMini" src="${arrPhotosURL[index]}">
                </div>`;
        }
    }

    html +=
        `   </div>

            <button class="carousel-control-prev" type="button" data-bs-target="#carouselControls${endIndex}" data-bs-slide="prev">
                <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                <span class="visually-hidden">Предыдущий</span>
            </button>

            <button class="carousel-control-next" type="button" data-bs-target="#carouselControls${endIndex}" data-bs-slide="next">
                <span class="carousel-control-next-icon" aria-hidden="true"></span>
                <span class="visually-hidden">Следующий</span>
            </button>
        </div>`;


    blogBody.innerHTML += html;


    // Clear
    divCreateCarousel.style.display = "none";
    mainCreateButtons.style.display = "block";

    for (let i = 1; i < 6; i++) {
        document.querySelector(`#photos${i}Ready>img`).setAttribute('src', '#');
        document.getElementById(`photos${i}`).style.display = "block";
        document.getElementById(`photos${i}Ready`).style.display = "none";
        document.getElementById(`photos${i}Input`).value = '';
        emptyCreateCarousel.className = '';
    }

    const carouselItems = document.getElementById("divCreateCarousel").querySelectorAll(".carousel-item");
    carouselItems[0].className = 'carousel-item active';
    for (let i = 1; i < carouselItems.length; i++) {
        carouselItems[i].className = 'carousel-item';
    }
}
