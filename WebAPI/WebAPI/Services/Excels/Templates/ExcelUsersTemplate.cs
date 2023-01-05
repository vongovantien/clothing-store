namespace WebAPI.Services.Excels.Templates
{
    public class ExcelUsersTemplate : Excel
    {
        List<object> dataExport = new List<object>();
        public ExcelUsersTemplate(List<object> data)
        {
            this.ExcelTemplatePath += "/UsersTemplate.xlsx";
            this.dataExport = data;
        }

        public byte[] Export()
        {
            var file = System.IO.File.ReadAllBytes(this.ExcelTemplatePath);
            return ExcelServices.Export(file, this.dataExport);
        }
    }
}
