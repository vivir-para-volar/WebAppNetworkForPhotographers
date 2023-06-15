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

    displayInfo() {
        console.log(`Ширина: ${this.Width} px`);
        console.log(`Высота: ${this.Height} px`);

        console.log("Make: " + this.Make)
        console.log("Model: " + this.Model)
        console.log("XResolution: " + this.XResolution)
        console.log("YResolution: " + this.YResolution)
        console.log("ApertureValue: " + this.ApertureValue)
        console.log("ISOSpeedRatings: " + this.ISOSpeedRatings)
        console.log("FocalLength: " + this.FocalLength)
        console.log("FocalLengthIn35mmFilm: " + this.FocalLengthIn35mmFilm)
    }
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



const dataCreateBlog = {
    title: '',
    photographerId: 0,
    mainPhoto: '',
    body: '',
    categoriesIds: []
}

const arrPhotos = [];
const arrPhotosInfo = [];

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


async function createBlog() {
    dataCreateBlog.body = `<pre>Практический опыт показывает, что постоянное информационно-техническое обеспечение нашей деятельности напрямую зависит от ключевых компонентов планируемого обновления! Разнообразный и богатый опыт консультация с профессионалами из IT влечет за собой процесс внедрения и модернизации системы обучения кадров, соответствующей насущным потребностям. Соображения высшего порядка, а также выбранный нами инновационный путь создаёт предпосылки качественно новых шагов для экономической целесообразности принимаемых решений.
Практический опыт показывает, что консультация с профессионалами из IT играет важную роль в формировании новых предложений.Задача организации, в особенности же выбранный нами инновационный путь требует от нас анализа направлений прогрессивного развития.Практический опыт показывает, что реализация намеченного плана развития влечет за собой процесс внедрения и модернизации форм воздействия.Не следует, однако, забывать о том, что новая модель организационной деятельности создаёт предпосылки качественно новых шагов для позиций, занимаемых участниками в отношении поставленных задач.
Соображения высшего порядка, а также дальнейшее развитие различных форм деятельности обеспечивает актуальность позиций, занимаемых участниками в отношении поставленных задач!
Соображения высшего порядка, а также постоянное информационно - техническое обеспечение нашей деятельности способствует повышению актуальности ключевых компонентов планируемого обновления! Повседневная практика показывает, что курс на социально - ориентированный национальный проект обеспечивает широкому кругу специалистов участие в формировании соответствующих условий активизации.Соображения высшего порядка, а также консультация с профессионалами из IT играет важную роль в формировании...</pre>`;

    const res = await serverCreateBlog(dataCreateBlog);
    if (!res) return false;

    //const content = res.data;

    //for (let i = 0; i < 5; i++) {
    //    if (arrPhotos[i]) {
    //        await serverCreatePhoto(content.id, arrPhotos[i], arrPhotosInfo[i]);
    //    }
    //}

    window.location.href = '/Profiles';
}




window.onload = function () {
    const mainPhotoInput = document.getElementById('mainPhotoInput');

    mainPhotoInput.addEventListener('change', function () {

        const file = this.files[0];

        if (file.size > maxPhotoSize) {
            photoInput.value = '';
            return false;
        }

        dataCreateBlog.mainPhoto = file;

        const url = URL.createObjectURL(file);

        document.querySelector(`#mainPhotoReady>img`).setAttribute('src', url);
        document.getElementById("blogMainPhoto").setAttribute('src', url);

        document.getElementById(`mainPhoto`).style.display = "none";
        document.getElementById(`mainPhotoReady`).style.display = "block";
    });

    //for (let i = 0; i < 5; i++) {
    //    const photoInput = document.getElementById(`photos${i}Input`);

    //    photoInput.addEventListener('change', function () {

    //        const file = this.files[0];

    //        if (file.size > maxPhotoSize) {
    //            photoInput.value = '';
    //            return false;
    //        }

    //        currentIndex = i;

    //        arrPhotos[i] = file;
    //        arrPhotosInfo[i] = new FileInfo();


    //        const image = new Image();
    //        image.addEventListener('load', () => {
    //            inputWidth.value = image.naturalWidth;
    //            inputHeight.value = image.naturalHeight;
    //        });
    //        const url = URL.createObjectURL(file);
    //        image.src = url;

    //        document.querySelector(`#photos${i}Ready>img`).setAttribute('src', url);
    //        document.getElementById(`photos${i}`).style.display = "none";
    //        document.getElementById(`photos${i}Ready`).style.display = "block";

    //        const reader = new FileReader();
    //        reader.addEventListener('load', () => {
    //            const imageData = reader.result;
    //            const exifData = EXIF.readFromBinaryFile(imageData);

    //            inputMake.value = exifData?.Make ?? '';
    //            inputModel.value = exifData?.Model ?? '';
    //            inputXResolution.value = exifData?.XResolution ?? '';
    //            inputYResolution.value = exifData?.YResolution ?? '';
    //            inputApertureValue.value = exifData?.ApertureValue?.toFixed(2) ?? '';
    //            inputISOSpeedRatings.value = exifData?.ISOSpeedRatings ?? '';
    //            inputFocalLength.value = exifData?.FocalLength ?? '';
    //            inputFocalLengthIn35mmFilm.value = exifData?.FocalLengthIn35mmFilm ?? '';
    //        });
    //        reader.readAsArrayBuffer(file);


    //        const modal = bootstrap.Modal.getInstance(modalCreatePhotoInfo);
    //        modal.show();
    //    });
    //}
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