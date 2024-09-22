class Program{
    static void Main(){
        Console.WriteLine("Digite o nome do paciente!");
        Paciente paciente = new Paciente(Console.ReadLine());
        paciente.preencheRespostas();
        paciente.converteParaAreas();
        Console.WriteLine(paciente.resultadoGeralBruto());
        paciente.exibePorAreas();
        paciente.exibeGabaritoPreenchido();
    }
}