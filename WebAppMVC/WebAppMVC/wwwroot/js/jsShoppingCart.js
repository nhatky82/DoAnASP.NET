$(document).ready(function () {
    $('body').on('click', '.addToCart', function (e) {
        if (document.getElementById("Login") == null) {
            alert("Vui lòng đăng nhập!");
        }
        else
        {
            alert("Thêm sản phẩm vào vỏ hàng thành công");
        }
    });


    $('body').on('click', '.addToFavourites', function (e) {
        if (document.getElementById("Login") == null) {
            alert("Vui lòng đăng nhập!");
        }
        else {
            alert("Thêm sản phẩm vào mục yêu thích thành công");
        }
    });
});