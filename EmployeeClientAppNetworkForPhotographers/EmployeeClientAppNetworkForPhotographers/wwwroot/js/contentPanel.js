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

    const res = await sendReqWithoutBody('get', `/Common/GetPhotoInfo?photoId=${photoId}`);
    if (!res) return false;

    const photoInfo = res.data;

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

async function sendReqWithoutBody(method, url) {
    try {
        return await axios({
            method,
            url,
        });
    } catch (error) {
        console.error(error);
        return false;
    }
}



// Modal window for big photo

const modalBigPhotoImg = document.getElementById('modalBigPhotoImg');

const modalBigPhoto = document.getElementById('modalBigPhoto');
modalBigPhoto.addEventListener('show.bs.modal', function (event) {
    const button = event.relatedTarget;

    const photoSrc = button.getAttribute('data-bs-whatever');
    modalBigPhotoImg.src = photoSrc;
});