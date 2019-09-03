using System;
using Alura.LeilaoOnline.Core;
using Xunit;

namespace Alura.LeilaoOnline.Test
{
    public class LeilaoTerminaPregao
    {
        [Theory]
        [InlineData(1200, 1250, new double[]{800, 1150, 1400, 1250})]
        public void RetornaValorSuperiorMaisProximoDadoLeilaoNessaModalidade(double valorDestino, double valorEsperado, double[] ofertas)
        {
            //Arranje - cenario
            var modalidade = new OfertaSuperiorMaisProxima(valorDestino);
            //IModalidadeAvaliacao modalidade = new OfertaSuperiorMaisProxima(valorDestino);
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

            //Act - metodo sob teste
            leilao.TerminaPregao();
            
            //Assert esperado
            var valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);
        }
            
        [Fact]
        public void LancaInvalidOperationExceptionDadoPregaoNaoIniciado()
        {
            //Arranje - cenario
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);

            //Assert
            var excecaoObtida = Assert.Throws<InvalidOperationException>(
                //Act - metodo sob teste
                () => leilao.TerminaPregao()
            );
            var mensagemEsperada =
                "Nao e possivel terminar o pregao sem que ele tenha comecado. Para isso, Utilize o metodo IniciaPregao.";
            Assert.Equal(mensagemEsperada, excecaoObtida.Message);
        }
        
        [Fact]
        public void RetornaZeroDadoLeilaoSemLances()
        {
            //Arranje - cenario
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            leilao.IniciaPregao();
            
            //Act - metodo sob teste
            leilao.TerminaPregao();
            
            //Assert esperado
            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;
            
            Assert.Equal(valorEsperado, valorObtido);
        }

        [Theory]
        [InlineData(1200, new double[]{800,900,1000,1200})]
        [InlineData(1000, new double[]{800,900,1000,990})]
        [InlineData(800, new double[]{800})]
        public void RetornaMaiorValorDadoLeilaoComPeloMenosUmLance(double valorEsperado, double[] ofertas)
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

            //Act - metodo sob teste
            leilao.TerminaPregao();
            
            //Assert esperado
            var valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);
        }
    }
}