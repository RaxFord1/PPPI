using ClosedXML.Excel;

namespace PracticeAPI.Services.VersionedServices.V3
{
    public interface IExcelFileService
    {
        Task<XLWorkbook> Get();
    }
}
