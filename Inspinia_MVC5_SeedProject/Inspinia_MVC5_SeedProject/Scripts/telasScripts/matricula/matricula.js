
$("#EnviarFormularioAjax").click(function () {


    var checkeds = $('#serial input:checkbox:checked').serialize()
    var array = new Array();
    var dataInicio = $('#datetimepicker').val();
    var id = $('#Aluno_AlunoId').val();

    array = checkeds;
    var horario = $('input:radio[name=horario]:checked').val();

    var obj = {


        'horario': horario,
        'cursos': array,
        'idAluno': id,
        'dataInicio': dataInicio

    }



    $.ajax({
        url: "/Matriculas/Create",
        dataType: 'json',
        data: obj,
        type: 'POST',
        success: function (result) {
            if (result.success) {
                
                var urlRelativa = '/Matriculas/Index';
                window.location = urlRelativa;
            }

        },
        error: function (xhr) {
            var mensagensErro = [];
            mensagensErro.push({ mensagem: "A requisição falhou. A aplicação está em execução?", icone: 'exclamation-sign' });
            mostraNotificacao("danger", mensagensErro, $('#info'));
            mensagensErro = [];
        }
    });
    event.preventDefault();

});
