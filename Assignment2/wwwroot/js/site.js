// Write your JavaScript code.
//$(document).ready(function () {
//    //$("#sidebar").mCustomScrollbar({
//    //    theme: "minimal"
//    //});

//    $('#dismiss, .overlay').on('click', function () {
//        $('#sidebar').removeClass('active');
//        $('.overlay').fadeOut();
//    });

//    $('#sidebarCollapse').on('click', function () {
//        $('#sidebar').addClass('active');
//        $('.overlay').fadeIn();
//        $('.collapse.in').toggleClass('in');
//        $('a[aria-expanded=true]').attr('aria-expanded', 'false');
//    });
//});

$(document).ready(function () {
    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
        $(this).toggleClass('active');
    });
});

var GetSidebar = function (role) {
    $.ajax({
        type: 'GET',
        url: '/' + role + '/Sidebar',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: '',
        success: function (response) {
            document.getElementById("sidebar-content").innerHTML = response;
        },
        error: function (error) {
            console.log(error);
        }
    });
}
