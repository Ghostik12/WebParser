using MigraDocCore.Rendering;
using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WebParser
{
    internal class DB()
    {
        public static PdfDocument GetCurrentDBVersion()
        {
            return new PdfDocument();
        }
    }
}
