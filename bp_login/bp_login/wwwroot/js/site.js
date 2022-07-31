function pathSelector() {
    var fullPath = window.location.pathname;
    var slash = new Object;

    for (var i = 0; i < fullPath.length; i++) {
        if (fullPath[i] == '/') {
            if (slash.first == undefined || slash.first == null) {
                slash.first = i;
            } else if (slash.second == undefined || slash.second == null) {
                slash.second = i;
            } else if (slash.third == undefined || slash.third == null) {
                slash.third = i;
            }
        }
    }
    var pathObject = new Object;
    pathObject.app = "";
    pathObject.controller = "";
    pathObject.action = "";

    if (slash.third == undefined || slash.third == null) {
        for (var i = slash.first + 1; i < slash.second; i++) {
            pathObject.controller += fullPath[i];
        }
        for (var i = slash.second + 1; i < fullPath.length; i++) {
            pathObject.action += fullPath[i];
        }
    } else {

        for (var i = slash.first + 1; i < slash.second; i++) {
            pathObject.app += fullPath[i];
        }

        for (var i = slash.second + 1; i < slash.third; i++) {
            pathObject.controller += fullPath[i];
        }

        for (var i = slash.third + 1; i < fullPath.length; i++) {
            pathObject.action += fullPath[i];
        }

    }
    return pathObject;
}

function requestPath(controller, action) {
    var ajaxUrl;

    if (pathSelector().app == '' || pathSelector().app == undefined) {
        ajaxUrl = window.location.origin + "/" + controller + "/" + action;
    } else {
        ajaxUrl = window.location.origin + "/" + pathSelector().app + "/" + controller + "/" + action
    }
    return ajaxUrl;
}

function logaUsuario() {
    var request = new Object;
    request.login = document.getElementById('loginUsuario').value;
    request.senha = document.getElementById('senhaUsuario').value;

    $.ajax({
        type: 'POST',
        async: false,
        url: requestPath("Home", "logaUsuario"),
        data: JSON.stringify(request),
        contentType: "application/json; charset=utf-8",
        success: function (_data) {            
            var data = JSON.parse(_data);
            if (data.valid) {
                window.location.reload();
            }
            alert(data.message);
        },
        error: function (data) {            
            console.log(data)
            alert("algo deu errado, contatar o suporte técnico");
        }
    });
}