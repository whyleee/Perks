using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Hosting;

namespace Perks.Web.Wrappers
{
    /// <summary>
    /// The wrapper for .NET web host API.
    /// </summary>
    public class HostWrapper
    {
        /// <summary>
        /// Maps the virtual path to the physical location of the host.
        /// </summary>
        /// <param name="virtualPath">The virtual path.</param>
        /// <returns>The absolute physical path to the resource.</returns>
        public virtual string MapPath(string virtualPath)
        {
            return HostingEnvironment.MapPath(virtualPath);
        }
    }
}
