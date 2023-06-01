for (let i = 0; i < 3; i++) {
    const photoInput = document.getElementById(`photos${i}Input`);

    photoInput.addEventListener('change', function () {
        if (this.files[0].size > maxPhotoSize) {
            photoInput.value = '';
            return false;
        }

        var reader = new FileReader();
        reader.onload = function (e) {
            document.querySelector(`#photos${i}Ready>img`).setAttribute('src', e.target.result);

            document.getElementById(`photos${i}`).style.display = "none";
            document.getElementById(`photos${i}Ready`).style.display = "block";
        }
        reader.readAsDataURL(this.files[0]);
    });
}
