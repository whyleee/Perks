using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Core;
using AutofacContrib.NSubstitute;

namespace Perks.Tests
{
    public abstract class IocFixture : IDisposable
    {
        private AutoSubstitute _fac;

        protected AutoSubstitute fac
        {
            get { return _fac ?? (_fac = new AutoSubstitute()); }
        }

        public void Dispose()
        {
            if (_fac != null)
            {
                _fac.Dispose();
            }
        }
    }

    public static class AutoSubstituteExtensions
    {
        public static T Get<T>(this AutoSubstitute fac, params Parameter[] parameters)
        {
            return fac.Resolve<T>(parameters);
        }
    }
}
