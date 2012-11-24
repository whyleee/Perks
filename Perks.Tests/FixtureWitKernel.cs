using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Ninject;
using Ninject.MockingKernel;
using Ninject.MockingKernel.NSubstitute;

namespace Perks.Tests
{
    public abstract class FixtureWithKernel
    {
        protected IKernel kernel = new NSubstituteMockingKernel();

        [TearDown]
        public void TearDown()
        {
            (kernel as MockingKernel).IfNotNull(x => x.Reset());
        }

        [TestFixtureTearDown]
        public void FixtureTearDown()
        {
            kernel.Dispose();
        }
    }
}
