using MigraDocCore.DocumentObjectModel;
using MigraDocCore.Rendering;
using PdfSharpCore.Pdf;

namespace WebParser
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var result = new WebpageData().CountriesAsync().GetAwaiter().GetResult();

            var pdf = result.ToPDF();

            var pdfIsTheSameAsLatestDBVersion = pdf.IsTheSameAsLatestDBVersion();

            if (pdfIsTheSameAsLatestDBVersion)
                return;

            var todaysDate = DateTime.Now.Date.ToString("yyyy-MM-dd");
            var filename = $"CountryData_{todaysDate}.pdf";
            pdf.Save(filename);
        }

        public static PdfDocument ToPDF(this List<Country> input)
        {
            Document document = new();

            Section section = document.AddSection();

            foreach (var item in input)
            {
                section.AddParagraph($"Day: {item.Day}");
                //section.AddParagraph($"Time: {item.Time}");
                //section.AddParagraph($"Population: {item.Population}");
                //section.AddParagraph($"Area KM Squared: {item.Area_KM_Squared}");
                section.AddParagraph();
            }

            PdfDocumentRenderer pdfRenderer = new() { Document = document };

            pdfRenderer.RenderDocument();

            return pdfRenderer.PdfDocument;
        }

        public static bool IsTheSameAsLatestDBVersion(this PdfDocument input)
        {
            var current = DB.GetCurrentDBVersion();

            if (input == current) { return true; }

            return false;
        }
    }
}
