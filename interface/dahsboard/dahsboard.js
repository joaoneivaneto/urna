$(document).ready(function(){
        
            
        function get_branco_nulo(tipo){
            var numero;
            var url=`https://localhost:7064/api/Votos/GetBrancoNulo?status=${tipo}`
            $.ajax({
                url:url,
                type:"get",
                dataType:"json",
                async:false,
                success:function(dados){
                    numero=dados.length;
                }  
            });
            return numero;
        }
       
        function votos(id){
            var quantidade_votos;
            var url =`https://localhost:7064/api/Votos/IdCandidato?id=${id}`
            $.ajax({
                url:url,
                type:"get",
                dataType:"json",
                async:false,
                success:function(dados){
                    quantidade_votos=dados
                }  
            });
            return quantidade_votos;
        }
        function list_legenda(){
            var nome=[];
            var numero_votos=[]; 
            var date_list=[];
            var url ="https://localhost:7064/api/RegistroCandidatos/"
            $.ajax({
                url:url,
                type:"get",
                dataType:"json",
                success:function(dados){ 
                    
                    for(i=0;i<dados.length;i++){
                        date_list[i]={
                            nome:dados[i].nomeCompleto,
                            numero_votos:votos(dados[i].id).length*10000000
                        }
                        nome.push(dados[i].nomeCompleto)
                        numero_votos.push(votos(dados[i].id).length*10000000)
                    }
                    
                    date_list[date_list.length]={
                        nome:"Nulo",
                        numero_votos:get_branco_nulo("Nulo")*80000000000
                    }
                    date_list[date_list.length]={
                        nome:"Branco",
                        numero_votos:get_branco_nulo("Branco")
                    }            
                    date_list.sort(function(a, b){
                        if(a.numero_votos > b.numero_votos){
                            return -1
                        }else{
                            return true
                        }
                       
                    })
                    for(i=0;i<date_list.length;i++){
                        $('.list').append
                            (
                                "<li id='li'>" +
                                        `<p class="index">${i+1}º</p>`+
                                        `<img class="foto_cand" src="https://static.vecteezy.com/system/resources/previews/009/267/454/non_2x/user-icon-design-free-png.png" alt=""></img>`+
                                        `<p class="nome_cand">${date_list[i].nome}</p>`+
                                        `<p class="numero_votos">${date_list[i].numero_votos}</p>`+
                                "</li>"
                                
                                
                               
                            )
                    }                   
                    
                    // for(i=0;i<date_list.length;i++){
                    //    maior=date_list[0].numero_votos
                    //    if(maior<date_list[i].numero){
                         
                    //    }
                    // }
                    
                    numero_votos.push(get_branco_nulo("Nulo")*100000)
                    numero_votos.push(get_branco_nulo("Branco")*100000)
                    nome.push("Nulo")
                    nome.push("Branco")
                    
                    grafico(nome,numero_votos)
                
                }
            });
            // return tot 
        }
        
        function grafico(nome,numero_votos){

            var xValues = nome;
            var yValues = numero_votos;
            var barColors = [
                "#b91d47",
                "#00aba9",
                "#2b5797",
                "#e8c3b9",
                "#1e7145"
            ];

            new Chart("myChart", {
            type: "doughnut",
            data: {
                labels: xValues,
                datasets: [{
                    backgroundColor: barColors,
                    data: yValues
                }]
            },
            options: {
                title: {
                    display: false,
               
                },
                legend: {
                    display: false,
               
                }
            }
            });
        }
        
        list_legenda()
      
            


    
    
});