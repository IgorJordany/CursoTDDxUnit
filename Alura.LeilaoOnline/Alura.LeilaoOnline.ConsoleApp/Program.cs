using System;
using Alura.LeilaoOnline.Core;

namespace Alura.LeilaoOnline.ConsoleApp
{
    class Program
    {
        private static void Verifica(double esperado, double obtido)
        {
            if (esperado == obtido)
            {
                Console.WriteLine("Teste Passou");
            }
            else
            {
                Console.WriteLine($"Teste Falhou! Esperado: {esperado}, obtido: {obtido}");
            }
        }
        private static void LeilaoComVariosLances()
        {
            //Arranje - cenario
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);
            
            leilao.RecebeLance(fulano, 800);
            leilao.RecebeLance(maria, 900);
            leilao.RecebeLance(fulano, 1000);
            leilao.RecebeLance(maria, 990);

            //Act - metodo sob teste
            leilao.TerminaPregao();
            
            //Assert esperado
            var valorEsperado = 1000;
            var valorObtido = leilao.Ganhador.Valor;
            Verifica(valorEsperado, valorObtido);
        }

        private static void LeilaoComApenasUmLance()
        {
            //Arranje - cenario
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            
            leilao.RecebeLance(fulano, 800);

            //Act - metodo sob teste
            leilao.TerminaPregao();
            
            //Assert esperado
            var valorEsperado = 900;
            var valorObtido = leilao.Ganhador.Valor;
            Verifica(valorEsperado, valorObtido);
        }
        
        static void Main(string[] args)
        {

        }
    }
}