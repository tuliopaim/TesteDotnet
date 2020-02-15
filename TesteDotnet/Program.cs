using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace TesteDotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Seja bem vindo!!!");
            Console.WriteLine("==================");
            Console.WriteLine("Calculadora DotNet");
            Console.WriteLine("==================");

            IniciarCalculadora();

            Console.ReadLine();
        }

        private static void IniciarCalculadora()
        {
            var calcularNovamente = true;

            while (calcularNovamente)
            {
                try
                {
                    ExibaMenu();

                    var opcaoStr = Console.ReadLine();
                    var operacao = Calculadora.ValidarOperacao(opcaoStr);

                    if (operacao == Operacao.Arquivo)
                    {
                        CalcularPeloArquivo();
                    }
                    else
                    {
                        ExibaInstrucao(operacao);

                        var valoresStr = Console.ReadLine();
                        var valores = Calculadora.ValidarValores(valoresStr);
                        var operador = "";
                        var resultado = Calculadora.Calcula(valores, operacao, (op) => operador = op);

                        ExibaResultado(valores, operador, resultado);
                    }

                    calcularNovamente = DefineSeCalculaNovamente();
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Desculpe, ocorreu um erro!");
                    Console.WriteLine();
                    Console.WriteLine($"Mais detalhes: {e.Message}");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

        }

        private static void CalcularPeloArquivo()
        {
            Console.WriteLine("Digite o caminho para o arquivo");

            var arquivo = Console.ReadLine();

            if (!File.Exists(arquivo))
                throw new ArquivoNaoEncontradoException();

            var linhas = File.ReadAllText(arquivo, System.Text.Encoding.UTF8).Split(Environment.NewLine);

            if (linhas.Length == 0)
                throw new ArquivoVazioException();

            var resultados = new Dictionary<string, decimal>();

            foreach (var linha in linhas)
            {
                if (string.IsNullOrEmpty(linha.Trim())) continue;
                var parametros = linha.Split(";");
                var operacao = Calculadora.ValidarOperacao(parametros[1]);

                var valoresStr = linha.Replace($"{parametros[0]};{parametros[1]};", "");

                var valores = Calculadora.ValidarValores(valoresStr);
                var operador = "";
                var resultado = Calculadora.Calcula(valores, operacao, (op) => operador = op);

                resultados.Add(parametros[0], resultado);
            }

            ExibaResultado(resultados);
        }

        public static void ExibaMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Escolha a operação");
            Console.WriteLine($" 1 - Soma");
            Console.WriteLine($" 2 - Subtração");
            Console.WriteLine($" 3 - Multiplicação");
            Console.WriteLine($" 4 - Divisão");
            Console.WriteLine($" 5 - Média");
            Console.WriteLine($" 6 - Arquivo");
            Console.WriteLine($"(Letras maiusculas ou minusculas, com ou sem acentos)");
            Console.WriteLine("====================");
        }

        public static void ExibaInstrucao(Operacao operacao)
        {
            Console.WriteLine();

            if (operacao == Operacao.Somar || operacao == Operacao.Media)
                Console.WriteLine("Digite os valores separados por ';'. Ex: 5;6;10;15");
            else
                Console.WriteLine("Digite 2 valores separados por ';'. Ex: 5;6 ");

            Console.WriteLine("====================");
        }

        public static void ExibaResultado(Dictionary<string, decimal> resultados)
        {
            Console.WriteLine();
            foreach (var resultado in resultados)
            {
                Console.WriteLine($"{resultado.Key} = {resultado.Value}");
            }
        }

        public static void ExibaResultado(List<decimal> valores, string operador, decimal resultado)
        {
            Console.WriteLine();

            var conta = "";

            if (operador == "media")
            {
                conta = string.Join(", ", valores);
                conta = $"A média de [{conta}] é {resultado}";
            }
            else
            {
                conta = $"{string.Join(operador, valores)} = {resultado.ToString()}";
            }

            Console.WriteLine(conta);

        }

        private static bool DefineSeCalculaNovamente()
        {
            var calcularNovamente = true;

            Console.WriteLine("Pressione qualquer tecla para continuar ou ESC para sair");

            if (Console.ReadKey(true).Key == ConsoleKey.Escape)
            {
                Console.WriteLine("Obrigado por utilizar nossa calculadora");
                calcularNovamente = false;
                Thread.Sleep(2000);
                Environment.Exit(0);
            }

            return calcularNovamente;
        }

    }
}