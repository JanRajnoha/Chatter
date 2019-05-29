using Chatter.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Chatter.BL.Crypto;

namespace Chatter.Test
{
    public class CryptoTest
    {
        [Fact]
        public void CheckCrypto()
        {
            string pass = "Rajnoha";

            var res = Encrypt(pass);

            Assert.True(Decrypt(res.Salt, res.Hash, pass));

        }
    }
}
