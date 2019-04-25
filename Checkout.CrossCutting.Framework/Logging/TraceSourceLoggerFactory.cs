using System.Diagnostics;
using System.Reflection;
using Checkout.CrossCutting.Core.Logging;

namespace Checkout.CrossCutting.Framework.Logging
{
    /// <summary>
    ///     TraceSourceLoggerFactory class used to create TraceSourceLogger
    /// </summary>
    public class TraceSourceLoggerFactory
        : ILoggerFactory
    {
        /// <summary>
        ///     Create a new ILog
        /// </summary>
        /// <returns>
        ///     The ILog created
        /// </returns>
        public ILogger Create()
        {
            return new TraceSourceLogger(new TraceSource(Assembly.GetCallingAssembly().FullName));
        }
    }
}