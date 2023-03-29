$(document).ready(function(){
    url="https://localhost:7064/api/RegistroCandidatos";
    $.ajax({
        url:url,
        type:"get",
        dataType:"json",
        success:function(dados){
            console.log(dados)
            // console.log(dados.length)
            for(i=0; i<dados.length; i++){
                $('#show').append
                (
                    "<tr>" +
                        `<td>${dados[i].nomeCompleto}</td>`+
                        `<td>${dados[i].nomeVice}</td>`+
                        `<td>${dados[i].dataRegistro}</td>`+
                        `<td>${dados[i].legenda}</td>`+
                    "</tr>"    

                
                 )
            }
           
        }
    })
});