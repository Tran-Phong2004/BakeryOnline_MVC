//Admin - khi click vào navbar của admin thì mũi tên quay xuống
document.addEventListener("DOMContentLoaded", () => {
    const arrowContainer = document.querySelector(".arrowContainer");
    if (arrowContainer != undefined && arrowContainer != null) {
        arrowContainer.addEventListener("click", (event) => {
            document.querySelector(".arrow-down").classList.toggle("arrow-active");
        });
    }
});


//Login,Signup - ẩn hiện con mắt password , confirm password
const inputPassword = document.querySelector(".password");
if (inputPassword) {
    const containerIconEye = inputPassword.closest("div").querySelector(".icon-eye");
    const iconEye = containerIconEye.querySelector("i");

    // Hàm kiểm tra xem có nên hiển thị hoặc ẩn icon eye
    const toggleIconVisibility = () => {
        if (inputPassword.value.length > 0) {
            containerIconEye.style.display = "block";
        } else {
            containerIconEye.style.display = "none";
        }
    };

    // Hiển thị icon khi có văn bản nhập vào
    inputPassword.addEventListener("input", toggleIconVisibility);
    // Xử lý sự kiện click trên icon eye để hiện hoặc ẩn mật khẩu
    containerIconEye.addEventListener("click", () => {
        if (iconEye.classList.contains("bi-eye-slash")) {
            iconEye.classList.remove("bi-eye-slash");
            iconEye.classList.add("bi-eye");
            inputPassword.type = "text";
        } else {
            iconEye.classList.add("bi-eye-slash");
            iconEye.classList.remove("bi-eye");
            inputPassword.type = "password";
        }
    });
}


//Y chang trên nhưng cho ô confirm password trong trang sign up
const inputPasswordConfirm = document.querySelector(".passwordConfirm");
if (inputPasswordConfirm) {
    const containerIconEye = inputPasswordConfirm.closest("div").querySelector(".icon-eye");
    const iconEye = containerIconEye.querySelector("i");

    const toggleIconVisibility = () => {
        if (inputPasswordConfirm.value.length > 0) {
            containerIconEye.style.display = "block";
        } else {
            containerIconEye.style.display = "none";
        }
    };

    inputPasswordConfirm.addEventListener("input", toggleIconVisibility);

    containerIconEye.addEventListener("click", () => {
        if (iconEye.classList.contains("bi-eye-slash")) {
            iconEye.classList.remove("bi-eye-slash");
            iconEye.classList.add("bi-eye");
            inputPasswordConfirm.type = "text";
        } else {
            iconEye.classList.add("bi-eye-slash");
            iconEye.classList.remove("bi-eye");
            inputPasswordConfirm.type = "password";
        }
    });
}