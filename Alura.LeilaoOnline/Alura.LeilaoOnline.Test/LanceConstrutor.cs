using System;
using Alura.LeilaoOnline.Core;
using Xunit;

namespace Alura.LeilaoOnline.Test
{
    public class LanceConstrutor
    {
        [Fact]
        public void LancaArgumentExceptionDadoValorNegativo()
        {
            //Arranje
            var valorNegativo = -100;
            
            //Assert
            Assert.Throws<ArgumentException>(
                //Act
                () => new Lance(null, valorNegativo)
            );
        }
        
    }
}