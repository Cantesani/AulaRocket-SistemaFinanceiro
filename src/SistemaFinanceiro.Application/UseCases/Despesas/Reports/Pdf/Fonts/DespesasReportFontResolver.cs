using PdfSharp.Fonts;
using System.Reflection;

namespace SistemaFinanceiro.Application.UseCases.Despesas.Reports.Pdf.Fonts
{
    public class DespesasReportFontResolver : IFontResolver
    {
        public byte[]? GetFont(string faceName)
        {
            var stream = ReadFontFile(faceName);

            if (stream == null)
                stream = ReadFontFile(FontHelper.DEFAULT_FONT);

            var length = stream!.Length;
            var data = new byte[length];

            stream.Read(buffer: data, offset: 0, count: (int)length);

            return data;
        }

        public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
        {
            return new FontResolverInfo(familyName);
        }

        private Stream? ReadFontFile(string faceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            return assembly.GetManifestResourceStream($"SistemaFinanceiro.Application.UseCases.Despesas.Reports.Pdf.Fonts.{faceName}.ttf");
        }
    }
}
