var i=0;
function Condition(Num,Num2){
   
    if(document.getElementById('valor1').value == ''){
        document.getElementById('valor1').value = Num;
    }
    else if(document.getElementById('valor2').value == ''){
        document.getElementById('valor2').value = Num2;
    }
}
function Limpar(){
    document.getElementById('valor1').value = '';
    document.getElementById('valor2').value = '';
    document.getElementById('nom_candidato').innerHTML='';
    document.getElementById('nom_vice').innerHTML='';
    document.getElementById('value_nom_candidato').value=''
    document.getElementById('value_nom_vice').value=''
    i=0
}


function show(){
    var result_busca
    var digito1=$('#valor1').val()
    var digito2 = $('#valor2').val()
    var legenda = digito1 + digito2
    var url ="https://localhost:7064/api/RegistroCandidatos/Search?legenda="+legenda+""
    $.ajax({
        url:url,
        type:"get",
        dataType:"json",
        async: false,
        success:function(dados){
           result_busca=dados[0];
        }
    });
    if(digito1!=0 && digito2 != 0){
            
        if(!result_busca){
            $('#nom_candidato').text("Nulo");
            $('#nom_vice').text("Nulo");

        }else{
            $('#nom_candidato').text(result_busca.nomeCompleto);
            $('#nom_vice').text(result_busca.nomeVice);
        }
        
    }
   return result_busca;
}
function inserir_voto(id,data,status){
    
        var url ="https://localhost:7064/api/Votos"
        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify({
                "id_Candidato": id,
                "data_Voto":data,
                "status":status,
            }),
            contentType: "application/json",
            success:function(){
                $("#info_presi").hide();
                $("#fim").show();
                setTimeout(function () {
                    $("#fim").hide();
                    $("#info_presi").show();

                    Limpar();
                }, 2500);
              
            }
        });   
     
}
function som_confirma(){
    var confirma = document.getElementById('som_confirma');
    confirma.play();
}
function som_click(){
    var click = document.getElementById('som_click');
    click.play();
}     
$(document).ready(function(){
    $("#fim").hide();
    $('#listaddecanditato').eq(0).attr('title', 'Lista de Candidatos');
    $('#dashBoard').eq(0).attr('title', 'DashBoard');
    $(".num_click").click(function() {
        
        show();
        
    });
    $("#valor2").keyup(function(){
        show();
    });
    $("#valor1").keyup(function(){
        show();
    });
    $("#confirma").click(function() {
        const d = new Date();
        let data = d.toISOString();
        if( $('#valor1').val() != 0  && $('#valor2').val() != 0){
            if(!show()){
                inserir_voto("00000000-0000-0000-0000-000000000000",data,"Nulo")
            }else{
                inserir_voto(show().id,data,"Concedido")
            }
            
        }else if($('#value_nom_candidato').val() == "Branco" && $('#value_nom_vice').val() == "Branco"){

           inserir_voto("00000000-0000-0000-0000-000000000000",data,"Branco")
        }
        som_confirma();
        
    });
    $("#branco").click(function(){
        if(!$('#valor1').val() &&  !$('#Valor2').val()){
            $('#nom_candidato').text("Branco");
            $('#nom_vice').text("Branco");
            $('#value_nom_candidato').val("Branco");
            $('#value_nom_vice').val("Branco"); 
        }

       
    })
    $('.num_click').click(function(){
        i++
        if(i<3){
            som_click();
        }
    })
    
});

