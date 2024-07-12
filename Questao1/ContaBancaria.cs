using System;
using System.Globalization;

namespace Questao1
{
    class ContaBancaria
    {
        public int Numero { get; }
        public string Titular { get; set; }
        public decimal Saldo { get; private set; }

        public ContaBancaria(int numero, string titular, decimal depositoInicial)
        {
            Numero = numero;
            Titular = titular;
            Saldo = depositoInicial;
        }

        public ContaBancaria(int numero, string titular)
        {
            Numero = numero;
            Titular = titular;
        }

        public void Deposito(decimal quantia)
        {
            if(quantia > 0)
               Saldo += quantia;
        }

        public void Saque(decimal quantia)
        {
            Saldo -= (quantia + Instituicao.TaxaTransacao);
        }

        public override string ToString()
        {
            return $"Conta {Numero}, Titular: {Titular}, Saldo: $ {Saldo:F2}";
        }
    }
}
