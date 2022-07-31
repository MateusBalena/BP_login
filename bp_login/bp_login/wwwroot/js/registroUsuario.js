function registraUsuario() {
    var request = new Object;
    var login = document.getElementById('login');
    var nome = document.getElementById('nome');
    var dtAniversario = document.getElementById('dtAniversario');
    var senha = document.getElementById('senha');
    var confirmaSenha = document.getElementById('confirmaSenha');

    if (!login.value || login.value.length > 10) {
        alert("login inválido");
        return;
    }

    if (!nome.value || nome.value.length > 255) {
        alert("Nome inválido");
        return;
    }

    if (!dtAniversario.value) {
        alert("Data de aniversário não inserida");
        return;
    }

    if (!senha.value || !confirmaSenha.value || senha.value != confirmaSenha.value) {
        alert("Senha inválida ou incompatível com confirmação da senha");
        return;
    }

    request.login = login.value;
    request.nome = nome.value;
    request.senha = senha.value;
    request.data_aniversario = dtAniversario.value;

    $.ajax({
        type: 'POST',
        async: false,
        url: requestPath("Home", "insereUsuario"),
        data: JSON.stringify(request),
        contentType: "application/json; charset=utf-8",
        success: function (_data) {
            var data = JSON.parse(_data);

            alert(data.message);

            if (data.valid) {                
                window.location.href = requestPath("Home", "Index");
            }
        },
        error: function (data) {
            console.log(data)
            alert("algo deu errado, contatar o suporte técnico");
        }
    });
}