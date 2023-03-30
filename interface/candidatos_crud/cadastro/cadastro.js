$(document).ready(function(){
    var url ="https://localhost:7064/api/RegistroCandidatos"
    const d = new Date();
    let data = d.toISOString();
    $("a").click(function() {
        var nome_candidato = $('#nome_candidato').val()
        var nome_vice = $('#nome_vice').val();
        var legenda = $('#legenda').val()
        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify({
                "nome_Candidato": nome_candidato,
                "nome_Vice":nome_vice,
                "data_Registro":data,
                "legenda":legenda,
            }),
            contentType: "application/json",
            success:function(){
                
                window.location.href = "http://127.0.0.1:5500/candidatos_crud/list/list.html";
            },
            error: function(XMLHttpRequest){
                
                $.toast({
                    heading: 'Error',
                    hideAfter: false,
                    text: XMLHttpRequest.responseText,
                    showHideTransition: 'fade',
                    icon: 'error'
                })
                
            }
        })
    })
    
})                