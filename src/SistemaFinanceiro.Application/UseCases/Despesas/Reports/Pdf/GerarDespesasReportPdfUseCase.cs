using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using SistemaFinanceiro.Application.UseCases.Despesas.Reports.Pdf.Colors;
using SistemaFinanceiro.Application.UseCases.Despesas.Reports.Pdf.Fonts;
using SistemaFinanceiro.Domain.Entities;
using SistemaFinanceiro.Domain.Extensions;
using SistemaFinanceiro.Domain.Reports;
using SistemaFinanceiro.Domain.Repositories.Despesas;
using System.Reflection;

namespace SistemaFinanceiro.Application.UseCases.Despesas.Reports.Pdf
{
    public class GerarDespesasReportPdfUseCase : IGerarDespesasReportPdfUseCase
    {
        private readonly IDespesasReadOnlyRepository _repository;
        private const string CURRENCY_SYMBOL = "R$";
        private const int HEIHT_LINHA_TABELA = 25;

        public GerarDespesasReportPdfUseCase(IDespesasReadOnlyRepository repository)
        {
            _repository = repository;
            GlobalFontSettings.FontResolver = new DespesasReportFontResolver();
        }

        public async Task<Byte[]> Execute(DateOnly mes)
        {
            var despesas = await _repository.GetByMes(mes);

            if (despesas.Count == 0)
            {
                return [];
            }

            var documento = CreateDocument(mes);
            var page = CreatePage(documento);

            CreateHeaderComFotoENome(page);

            var totalDespesas = despesas.Sum(x => x.Valor);
            CreateTotalGasto(page, mes, totalDespesas);

            foreach (var despesa in despesas)
            {
                var tabela = CreateDespesasTable(page);

                var row = tabela.AddRow();
                row.Height = HEIHT_LINHA_TABELA;

                //adicionando primeira linha (cabeçalho) na tabela (noma da despesa e valor.)
                AddHeaderTituloTablesDespesa(row.Cells[0], despesa.Titulo);
                AddHeaderValorTablesDespesa(row.Cells[3]);

                //adiciona nova linha com tamanho padrao
                row = tabela.AddRow();
                row.Height = HEIHT_LINHA_TABELA;

                //preenche data da despesa
                row.Cells[0].AddParagraph(despesa.Data.ToString("D"));
                row.Cells[0].Format.LeftIndent = 20;
                StyleBaseDespesas(row.Cells[0]);

                //preenche hora da despesa
                row.Cells[1].AddParagraph(despesa.Data.ToString("t"));
                StyleBaseDespesas(row.Cells[1]);

                //preenche Tipo Pagto da despesa
                row.Cells[2].AddParagraph(despesa.TipoPagto.TipoPagtoToString());
                StyleBaseDespesas(row.Cells[2]);

                //preenche valor da despesa
                AddBodyValorTablesDespesa(row.Cells[3], despesa.Valor);

                //preenche descricao da despesa caso houver

                if (!string.IsNullOrWhiteSpace(despesa.Descricao))
                {
                    var rowDescricao = tabela.AddRow();
                    rowDescricao.Cells[0].AddParagraph(despesa.Descricao);
                    rowDescricao.Cells[0].Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 10, Color = ColorsHelper.BLACK };
                    rowDescricao.Cells[0].Format.LeftIndent = 20;
                    rowDescricao.Cells[0].Shading.Color = ColorsHelper.GREEN_LIGHT;
                    rowDescricao.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                    rowDescricao.Cells[0].MergeRight = 2;
                    row.Cells[3].MergeDown = 1;
                }

                //Adiciona espaço em branco entre as tabelas
                AddEspacoBranco(tabela);
            }



            return RenderizarDocumento(documento);
        }

        private Document CreateDocument(DateOnly mes)
        {
            var documento = new Document();
            documento.Info.Title = $"{ResourceReportGenerationMessages.DESPESA_PARA} {mes.ToString("Y")}";
            documento.Info.Author = "Gabriel Cantesani";

            var style = documento.Styles["Normal"];
            style!.Font.Name = FontHelper.RALEWAY_REGULAR;

            return documento;
        }

        private Section CreatePage(Document documento)
        {
            var section = documento.AddSection();
            section.PageSetup = documento.DefaultPageSetup.Clone();

            section.PageSetup.PageFormat = PageFormat.A4;
            section.PageSetup.LeftMargin = 40;
            section.PageSetup.RightMargin = 40;
            section.PageSetup.BottomMargin = 80;
            section.PageSetup.TopMargin = 80;

            return section;
        }

        private void CreateHeaderComFotoENome(Section page)
        {
            var tabela = page.AddTable();
            tabela.AddColumn();
            tabela.AddColumn("300");

            var linha = tabela.AddRow();

            var assembly = Assembly.GetExecutingAssembly();
            var diretorio = Path.GetDirectoryName(assembly.Location);
            var PathFile = Path.Combine(diretorio!, "Img", "Capture.PNG");

            linha.Cells[0].AddImage(PathFile);

            var saudacao = string.Format(ResourceReportGenerationMessages.SAUDACAO, "Gabriel Cantesani");
            linha.Cells[1].AddParagraph().AddFormattedText(saudacao, new Font { Name = FontHelper.RALEWAY_BLACK, Size = 16 });
            linha.Cells[1].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
        }

        private void CreateTotalGasto(Section page, DateOnly mes, decimal totalDespesas)
        {
            var paragrafo = page.AddParagraph();
            paragrafo.Format.SpaceBefore = "40";
            paragrafo.Format.SpaceAfter = "40";

            var titulo = string.Format(ResourceReportGenerationMessages.TOTAL_GASTO_EM, mes.ToString("Y"));

            paragrafo.AddFormattedText(titulo, new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 15 });
            paragrafo.AddLineBreak();


            paragrafo.AddFormattedText($"{CURRENCY_SYMBOL} {totalDespesas}", new Font { Name = FontHelper.WORKSANS_BLACK, Size = 50 });
        }

        private Table CreateDespesasTable(Section page)
        {
            var tabela = page.AddTable();

            tabela.AddColumn("195").Format.Alignment = ParagraphAlignment.Left;
            tabela.AddColumn("80").Format.Alignment = ParagraphAlignment.Center;
            tabela.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
            tabela.AddColumn("120").Format.Alignment = ParagraphAlignment.Right;

            return tabela;
        }

        private void AddHeaderTituloTablesDespesa(Cell cell, string tituloDespesa)
        {
            cell.AddParagraph(tituloDespesa);
            cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorsHelper.BLACK };
            cell.Format.LeftIndent = 20;
            cell.Shading.Color = ColorsHelper.RED_LIGHT;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.MergeRight = 2;
        }

        private void AddHeaderValorTablesDespesa(Cell cell)
        {
            cell.AddParagraph(ResourceReportGenerationMessages.VALOR);
            cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorsHelper.WHITE };
            cell.Shading.Color = ColorsHelper.RED_DARK;
            cell.VerticalAlignment = VerticalAlignment.Center;
        }

        private void AddBodyValorTablesDespesa(Cell cell, decimal valorDespesa)
        {
            cell.AddParagraph($"-{valorDespesa.ToString("C")}");
            cell.Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 14, Color = ColorsHelper.BLACK };
            cell.Shading.Color = ColorsHelper.WHITE;
            cell.VerticalAlignment = VerticalAlignment.Center;
        }

        private void StyleBaseDespesas(Cell cell)
        {
            cell.Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 12, Color = ColorsHelper.BLACK };
            cell.Shading.Color = ColorsHelper.GREEN_DARK;
            cell.VerticalAlignment = VerticalAlignment.Center;
        }

        private void AddEspacoBranco(Table tabela)
        {
            var row = tabela.AddRow();
            row.Height = 30;
            row.Borders.Visible = false;
        }

        private byte[] RenderizarDocumento(Document documento)
        {
            var renderizar = new PdfDocumentRenderer
            {
                Document = documento
            };

            renderizar.RenderDocument();

            using var file = new MemoryStream();
            renderizar.PdfDocument.Save(file);

            return file.ToArray();
        }
    }
}
