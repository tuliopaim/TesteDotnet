using System;
using System.Collections.Generic;
using System.Text;

namespace TesteDotNet
{
    public class OperacaoInvalidaException : Exception
    {
        public OperacaoInvalidaException(string operacaoStr)
            : base($"Operação [{operacaoStr}] inválida!") { }
    }

    public class EntradaInvalidaException : Exception
    {
        public EntradaInvalidaException(string entradaStr)
            : base($"Entrada [{entradaStr}] inválida!") { }
    }

    public class ValorInvalidoException : Exception
    {
        public ValorInvalidoException(string valorStr) 
            : base($"Valor [{valorStr}] inválido!") { }
    }

    public class DivisaoPorZeroException : Exception
    {
        public DivisaoPorZeroException() 
            : base("Não é possivel dividir por 0!") { }
    }

    public class ArquivoNaoEncontradoException : Exception
    {
        public ArquivoNaoEncontradoException() 
            : base("Arquivo não encontrado!") { }
    }

    public class ArquivoVazioException : Exception
    {
        public ArquivoVazioException() 
            : base("Arquivo vazio!") { }
    }
}