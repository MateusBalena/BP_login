var currentProp = null;



function alteraNome() {
    var prop = document.getElementById("mudaNome");

    if (currentProp == prop) {
        $(prop).toggleClass("d-none");
        currentProp = null;
        return;
    }

    if (currentProp != null) {
        $(currentProp).toggleClass("d-none");        
    }    

    $(prop).toggleClass("d-none");
    currentProp = prop;
}

function alteraData() {
    var prop = document.getElementById("mudaAniversario");

    if (currentProp == prop) {
        $(prop).toggleClass("d-none");
        currentProp = null;
        return;
    }

    if (currentProp != null) {
        $(currentProp).toggleClass("d-none");        
    }

    $(prop).toggleClass("d-none");
    currentProp = prop;
}

function salvaNome() {
    var request = new Object;
    var prop = document.getElementById("nomeUsuario");

    request.nome = prop.value;
    request.data = null;
    request.action = "Nome";

    console.log(request);

    $.ajax({
        type: 'POST',
        async: false,
        url: requestPath("Home", "alteracao"),
        data: JSON.stringify(request),
        contentType: "application/json; charset=utf-8",
        success: function (_data) {
            var data = JSON.parse(_data);
            if (data.valid) {
                renovaSessao();
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

function salvaData() {
    var request = new Object;
    var prop = document.getElementById("aniversarioUsuario");

    request.nome = "";
    request.data = prop.value;
    request.action = "Aniversario";

    $.ajax({
        type: 'POST',
        async: false,
        url: requestPath("Home", "alteracao"),
        data: JSON.stringify(request),
        contentType: "application/json; charset=utf-8",
        success: function (_data) {
            var data = JSON.parse(_data);
            if (data.valid) {
                renovaSessao();
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

function renovaSessao() {
    $.ajax({
        type: 'POST',
        async: false,
        url: requestPath("Home", "renovaSessao"),        
        contentType: "application/json; charset=utf-8",
        success: function (_data) {   
            var data = JSON.parse(_data);
            if (!data.valid) {
                alert(data.message);
            }
        },
        error: function (data) {
            console.log(data)
            alert("algo deu errado, contatar o suporte técnico");
        }
    });
}