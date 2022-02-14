
$(document).ready(function () {



     var datas = {
        IdAluno: $('#AlunoId').val()
       
    }
    if (datas.IdAluno != "") {

        $.ajax({
            dataType: "json",
            type: "GET",
            url: "../../Alunos/obterDiaSemana",
            data: datas,
            success: function (result) {

                if (result != "") {

                    $('#PrimeiroDia').val(result.PrimeiroDia);
                    $('#SegundoDia').val(result.SegundoDia);
                    $('#periodo').val(result.periodo + " Mês");
                    $('#descricao').val(result.descricao);
                    $('#valor').val(result.valor);


                }




            }
        });
    }
    






   
    if ($('#Identificador').val() != "") {

        $('#divBancoNome').css('display', 'block');
        $('#Produto, #SubDominio').prop('disabled', true)
    }

    // Seleciona e Carrega Curso

    $('#listaCursos').on('change', function () {
        var dados = {
            idCurso: $('#listaCursos').val()
        }



        if (dados.idCurso != "") {


            $('.listaCurso').css('display', 'block');

            $.ajax({
                dataType: "json",
                type: "GET",
                url: "../../Alunos/obterCurso",
                data: dados,
                success: function (result) {

                    if (result != "") {

                        $('#nome').val(result.nome);
                        $('#periodo').val(result.periodo + " Mês");
                        $('#descricao').val(result.descricao);
                        $('#valor').val(result.valor);


                    }




                }
            });
        }
        else {
            $('.listaCurso').css('display', 'none');

        }

    });







    $('a[href$="finish"]').on('click', function () {




        var data = {
            IdAluno: $('#AlunoId').val(),
            CursoId: $('#listaCursos').val(),
            HoraInicio: $('#HorarioInicio').val(),
            HorarioFim: $('#HorarioFim').val(),
            DataInicio: $('#DataInicio').val(),
            DataFim: $('#DataFim').val(),
            PrimeiroDia: $('#PrimeiroDia').val(),
            SegundoDia: $('#SegundoDia').val()


        }



        if (data.DataInicio == "" || data.HorarioFim == "" || data.CursoId == "" || data.DataFim == "") {


            if(data.DataInicio == "")
            {
                toastr.error('Preencha o Campo Data Início');
                $('#DataInicio').addClass('danger');
            }
            if(data.DataFim == "")
            {
                toastr.error('Preencha o Campo Data Fim');
            }
            if (data.CursoId == "")
            {
                toastr.error('Preencha o Campo Curso');
            }



        }
        else {



            $.ajax({
                type: "POST",
                data: JSON.stringify(data),
                url: "../../Alunos/salvarMatricula",
                contentType: 'application/json; charset=utf-8',
                success: function (result) {
                    if (result.success) {
                        var IdAluno = $('#AlunoId').val();
                        window.location = '/Alunos/Details?id=' + IdAluno;
                        location.reload();

                    }


                }
            })
        }

    });




});

