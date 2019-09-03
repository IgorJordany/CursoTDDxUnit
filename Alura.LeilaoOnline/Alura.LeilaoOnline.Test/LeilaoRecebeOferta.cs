using System;
using System.Linq;
using Alura.LeilaoOnline.Core;
using Xunit;

namespace Alura.LeilaoOnline.Test
{
    public class LeilaoRecebeOferta
    {
        [Fact]
        public void NaoAceitaProximoLanceDadeMesmoClienteRealizouUltimoLance()
        {
            //Arranje - cenario
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            leilao.IniciaPregao();
            leilao.RecebeLance(fulano, 800);
            leilao.TerminaPregao();

            //Act - metodo sob teste
            leilao.RecebeLance(fulano,1000);
            
            //Assert esperado
            var quantidadeEsperada = 1;
            var quantidadeObtida = leilao.Lances.Count();
            
            Assert.Equal(quantidadeEsperada, quantidadeObtida);
        }
        [Theory]
        [InlineData(4, new double[]{1000, 1200, 1400, 1300})]
        [InlineData(2, new double[]{800, 900})]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(int quantidadeEsperada, double[] ofertas)
        {
            //Arranje - cenario
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);
            
            leilao.IniciaPregao();
            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];
                if ((i % 2) == 0)
                {
                    leilao.RecebeLance(fulano,valor);
                }
                else
                {
                    leilao.RecebeLance(maria,valor);
                }
            }
            leilao.TerminaPregao();
            
            //Act - metodo sob teste
            leilao.RecebeLance(fulano,1000);
            
            //Assert esperado
            var quantidadeObtida = leilao.Lances.Count();
            
            Assert.Equal(quantidadeEsperada, quantidadeObtida);
        }
    }
}