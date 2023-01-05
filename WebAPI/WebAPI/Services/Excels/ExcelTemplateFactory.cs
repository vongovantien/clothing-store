using WebAPI.Services.Excels.Templates;

namespace WebAPI.Services.Excels
{
    public class ExcelTemplateFactory
    {
        public static ExcelUsersTemplate ExcelUsersTemplate(List<object> data)
        {
            return new ExcelUsersTemplate(data);
        }
    }
}
