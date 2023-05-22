using ClosedXML.Excel;
using PracticeAPI.DTO;

namespace PracticeAPI.Services.VersionedServices.V3
{
    public class ExcelFileService : IExcelFileService
    {
        public async Task<XLWorkbook> Get()
        {
            try
            {
                return await Task.Run(() =>
                {
                    var workbook = new XLWorkbook();
                    var worksheet = workbook.Worksheets.Add("Sample Sheet");
                    worksheet.Cell("A1").Value = "Hello World!";
                    worksheet.Cell("A2").FormulaA1 = "=MID(A1, 7, 5)";
                    return workbook;
                });
            }
            catch (Exception){
                throw;
            }
        }
    }
}
