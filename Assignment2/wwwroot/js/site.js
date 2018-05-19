$(document).ready(function () {
    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
        $(this).toggleClass('active');
    });
    $('#popup_from').popup({
        blur: false,
        opacity: 0.3,
        transition: 'all 0.3s'
    });
    $('#popup_from').popup('hide');;
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

var validate = function () {
    $('#ajax_form').val(blockSubmit());
    return false;
}

var blockSubmit = function () {
    alert('blocking');
    return false;
}

AddAntiForgeryToken = function (data) {
    data.__RequestVerificationToken = $('#__AjaxAntiForgeryForm input[name=__RequestVerificationToken]').val();
    return data;
};

var token = $('[name=__RequestVerificationToken]').val();
var headers = {};
headers["__RequestVerificationToken"] = token;
headers["Access-Control-Allow-Headers"] = "application/json";

// get has 2 types, one where he knows the id of the object and the other if he is uncertain
var Get = function (controller, method, id) {
    var url = '';
    if (id == undefined) {
        url = '/' + controller + '/' + method;
    } else {
        url = '/' + controller + '/' + method + '/' + id;
    }

    console.log(url);

    $.ajax({
        type: 'GET',
        url: url,
        cache: false,
        //headers: headers,
        contentType: 'application/json; charset=utf-8',
        data: '',
        success: function (response) {
            console.log(response);
            document.getElementById("user_body").innerHTML = response;
        },
        error: function (error) {
            console.log(error);
        }
    });
};

var GetForm = function (controller, method, var1, var2) {
    var url  = '/' + controller + '/' + method + '?StoreID=' + var1 + '&ProductID=' + var2;
    $.ajax({
        type: 'GET',
        url: url,
        cache: false,
        //headers: headers,
        contentType: 'application/json; charset=utf-8',
        data: '',
        success: function (response) {
            console.log(response);
            document.getElementById("form_body").innerHTML = response;
            $('#popup_form').popup('show');
        },
        error: function (error) {
            console.log(error);
        }
    });
};

var PostCart = function () {
    var data = {
        CustomerID: document.getElementById("CustomerID").value,
        ProductID: document.getElementById("ProductID").value,
        StoreID: document.getElementById("StoreID").value,
        Quantity: document.getElementById("Quantity").value
    };

    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();
    $.ajax({
        url: 'http://localhost:62530/api/cart',
        type: 'POST',
        //headers: headers,
        crossDomain: true,
        xhrFields: { withCredentials: true },
        data: {
            __RequestVerificationToken: token,
            data: data
        },
        success: function (response) {
            document.getElementById("user_body").innerHTML = response;
        },
        error: function (error) {
            console.log(error);
        }
    });
    return false;
};

var DeleteCart = function (CustomerID, StoreID, ProductID) {
    var data = {
        CustomerID: CustomerID,
        ProductID: ProductID,
        StoreID: StoreID
    };

    var url = 'http://localhost:62530/api/cart/' + CustomerID + '/' + StoreID + '/' + ProductID;
    alert(url);

    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();
    $.ajax({
        url: url,
        type: 'DELETE',
        //headers: headers,
        crossDomain: true,
        xhrFields: { withCredentials: true },
        //data: {
        //    __RequestVerificationToken: token,
        //    data: data
        //},
        success: function (response) {
        },
        error: function (error) {
        }
    });
    return false;
};