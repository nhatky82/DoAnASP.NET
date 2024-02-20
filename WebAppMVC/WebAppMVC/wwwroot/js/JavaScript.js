// Trong tệp JavaScript của bạn (your-script.js)
window.fbAsyncInit = function () {
    FB.init({
        appId: 'your-app-id',
        cookie: true,
        xfbml: true,
        version: 'v15.0'
    });

    FB.getLoginStatus(function (response) {
        statusChangeCallback(response);
    });
};

(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = 'https://connect.facebook.net/en_US/sdk.js';
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));

function statusChangeCallback(response) {
    // Xử lý trạng thái đăng nhập ở đây
    console.log(response);
    if (response.status === 'connected') {
        // Người dùng đã đăng nhập vào ứng dụng của bạn qua Facebook
        // Thực hiện các thao tác khác...
    } else {
        // Người dùng chưa đăng nhập hoặc đã hủy đăng nhập
        // Thực hiện các thao tác khác...
    }
}
