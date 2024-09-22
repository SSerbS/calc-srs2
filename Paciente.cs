class Paciente{
    string nome;
    public Paciente(string nome){
        this.nome = nome;
    }
    int[] respostas= new int[65];
    int[] tabelaGabaOD = [0,1,2,3];
    
    int perc = 0; //Percepção Social
    int cogn = 0; //Cognição Social
    int comu = 0; //Comunicação Social
    int moti = 0; //Motivação Social
    int padr = 0; //Padrões Restritivos e Repetitivos
    int[] ordemDireta = [0,1,2,3];
    int[] arOrdIndir = [3,7,11,12,15,17,21,22,26,32,38,40,43,45,48,52,55];
    int[] arPerc = [2,7,25,32,45,52,54,56];
    int[] arCogn = [5,10,15,17,30,40,42,44,48,58,59,62];
    int[] arComu = [12,13,16,18,19,21,22,26,33,35,36,37,38,41,46,47,51,53,55,57,60,61];
    int[] arMoti = [1,3,6,9,11,23,27,34,43,64,65];
    int[] arPadr = [4,8,14,20,24,28,29,31,39,49,50,63];
    public void preencheRespostas(){
        Console.WriteLine($"Preenchendo os dados de {nome}!");
        for(int i = 0; i < respostas.Length; i++){ //lembrar que i sempre estará um abaixo do item de fato
            if(i == 0){
                Console.WriteLine($"Preenchendo a questão {i+1}");
            }
            else{
                Console.WriteLine($"Preenchendo a questão {i+1}! Se quiser corrigir a anterior, digite 'b' em minúsculas!");
            }
            var respAtualString = Console.ReadLine();
            if(respAtualString != "b" || i == 0){ //segue na iteração atual caso digite b ou esteja na primeira iteração
                if(int.TryParse(respAtualString, out int respAtualOK)){ //se for um número, segue
                    if(respAtualOK > 0 && respAtualOK < 5){ //verifica se é 1, 2, 3 ou 4
                        respostas[i] = respAtualOK - 1; //deixa no padrão (0123) para ser lido pela função das áreas
                    }
                    else{
                        //Console.Clear();
                        Console.WriteLine("Digite um número válido (1,2,3,4)! Tente novamente!");
                        i--;
                        continue;
                    }
                }
                else{ //caso alguma letra tenha sido digitada
                    //Console.Clear();
                    Console.WriteLine("Digite apenas números! Tente novamente");
                    i--; //repete a tentativa de preencher o valor atual
                    continue;
                }
            }
            else{// caso tenha digitado "b"
            //Console.Clear();
            i = i-2;
            continue;
            }
        }
        Console.WriteLine("Respostas computadas!");
    }

    public void exibeGabaritoPreenchido(){
        for(int i = 0; i < respostas.Length; i++){
            Console.WriteLine($"Questão {i+1} marcou gabarito {respostas[i]+1}");
        }
    }

    public int resultadoGeralBruto(){
        int soma = perc + cogn + comu + moti + padr;
        return soma;
    }

    public void exibePorAreas(){
        Console.WriteLine($"Percepção: {perc}");
        Console.WriteLine($"Cognição: {cogn}");
        Console.WriteLine($"Comunicação: {comu}");
        Console.WriteLine($"Motivação: {moti}");
        Console.WriteLine($"Padrão: {padr}");
    }
    public void converteParaAreas(){
        for(int i = 0; i < respostas.Length; i++){
            if(!arOrdIndir.Contains(i+1)){//verifica se o item atual é da ordem direta
                if(arPerc.Contains(i+1)){ //verifica se o item atual é de Percepção
                        perc += respostas[i];
                }
                else if(arCogn.Contains(i+1)){
                    cogn += respostas[i];
                }
                else if(arComu.Contains(i+1)){
                    comu += respostas[i];
                }
                else if(arMoti.Contains(i+1)){
                    moti += respostas[i];
                }
                else if(arPadr.Contains(i+1)){
                    padr += respostas[i];
                }
            }
            else{
                if(arPerc.Contains(i+1)){ //verifica se o item atual é de Percepção
                    perc += Math.Abs(respostas[i]-3);
                }
                else if(arCogn.Contains(i+1)){
                    cogn += Math.Abs(respostas[i]-3);
                }
                else if(arComu.Contains(i+1)){
                    comu += Math.Abs(respostas[i]-3);
                }
                else if(arMoti.Contains(i+1)){
                    moti += Math.Abs(respostas[i]-3);
                }
                else if(arPadr.Contains(i+1)){
                    padr += Math.Abs(respostas[i]-3);
                }
            }
        }
    }
    public void SalvarResultados(){
        string local = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string filename = @$"SRS2_{nome}.txt";
        string path = Path.Combine(local, filename);
        int contador = 1;

        while(File.Exists(path)){
            filename = @$"SRS2_{nome}{contador}.txt";
            path = Path.Combine(local, filename);
            contador++;
        }
        if(!File.Exists(path)){
            using(StreamWriter sw = File.CreateText(path)){
                for(int i = 0; i < respostas.Length; i++){
                sw.WriteLine($"Questão {i+1} marcou gabarito {respostas[i]+1}");
                }
                sw.WriteLine();
                sw.WriteLine($"Percepção: {perc}");
                sw.WriteLine($"Cognição: {cogn}");
                sw.WriteLine($"Comunicação: {comu}");
                sw.WriteLine($"Motivação: {moti}");
                sw.WriteLine($"Padrão: {padr}");
                sw.WriteLine();
                sw.WriteLine($"Soma geral: {resultadoGeralBruto()}");
                Console.WriteLine("Arquivo salvo!");
            }
        }
    } 
}