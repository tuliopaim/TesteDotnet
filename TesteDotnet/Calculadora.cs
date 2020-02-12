using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TesteDotNet
{
    public enum Operacao
    {
        Somar = 1, Subtrair, Multiplicar, Dividir, Media, Arquivo
    }

    public class Calculadora
    {
        public static decimal Somar(decimal num1, decimal num2) => num1 + num2;

        public static decimal Somar(List<decimal> valores) => valores.Sum();

        public static decimal Subtrair(decimal num1, decimal num2) => num1 - num2;

        public static decimal Multiplicar(decimal num1, decimal num2) => num1 * num2;

        public static decimal Dividir(decimal num1, decimal num2)
        {
            if (num2 == 0)
                throw new DivisaoPorZeroException();
            return num1 / num2;
        }

        public static decimal Media(List<decimal> valores) => decimal.Divide(valores.Sum(), valores.Count());

        public static Operacao ValidarOperacao(string operacao)
        {
            if (string.IsNullOrEmpty(operacao))
                throw new OperacaoInvalidaException($"{operacao}");

            switch (operacao.ToLower())
            {
                case "1":
                case "soma":
                case "somar":
                    return Operacao.Somar;

                case "2":
                case "subtrai":
                case "subtrair":
                case "subtracao":
                case "subtração":
                    return Operacao.Subtrair;

                case "3":
                case "multiplica":
                case "multiplicar":
                case "multiplicacao":
                case "multiplicação":
                    return Operacao.Multiplicar;

                case "4":
                case "divide":
                case "dividir":
                case "divisao":
                case "divisão":
                    return Operacao.Dividir;

                case "5":
                case "media":
                case "média":
                    return Operacao.Media;

                case "6":
                case "arquivo":
                case "input.txt":
                    return Operacao.Arquivo;

                default:
                    throw new OperacaoInvalidaException(operacao);
            }
        }

        internal static List<decimal> ValidarValores(string valoresStr)
        {
            if (!valoresStr.Contains(";"))
                throw new EntradaInvalidaException(valoresStr);

            valoresStr = valoresStr.Trim();

            var valoresArr = valoresStr.Split(";");

            if (valoresArr.Length < 2 || string.IsNullOrEmpty(valoresArr[0])
                                      || string.IsNullOrEmpty(valoresArr[1]))
                throw new EntradaInvalidaException(valoresStr);

            var valoresList = new List<decimal>();

            foreach (var valorStr in valoresArr)
            {
                var valor = 0m;

                if (!decimal.TryParse(valorStr, out valor))
                    throw new ValorInvalidoException($"{valorStr}");

                valoresList.Add(valor);
            }

            return valoresList;
        }

        internal static decimal Calcula(List<decimal> valores, Operacao operacao, Action<string> defineOperador)
        {
            var resultado = 0m;
            switch (operacao)
            {
                case Operacao.Somar:
                    defineOperador("+");
                    if (valores.Count > 2)
                        resultado = Somar(valores);
                    else
                        resultado = Somar(valores[0], valores[1]);
                    break;
                case Operacao.Subtrair:
                    defineOperador("-");
                    resultado = Subtrair(valores[0], valores[1]);
                    break;

                case Operacao.Multiplicar:
                    defineOperador("*");
                    resultado = Multiplicar(valores[0], valores[1]);
                    break;

                case Operacao.Dividir:
                    defineOperador("/");
                    resultado = Dividir(valores[0], valores[1]);
                    break;

                case Operacao.Media:
                    defineOperador("media");
                    resultado = decimal.Round(Media(valores), 3);
                    break;
            }

            resultado = decimal.Round(resultado, 3);
            return resultado;
        }

    }
}