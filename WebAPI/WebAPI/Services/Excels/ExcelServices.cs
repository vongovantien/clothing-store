using OfficeOpenXml;
using System.Dynamic;

namespace WebAPI.Services.Excels
{
    public enum EnumExcelTemplate
    {
        MyHrInOutLogs,
        HrInOutLogs,
        OT,
        AbsentRequests,
        LeaveDaysController
    }
    public class cell
    {
        public cell()
        {
            ROW = 0;
            COL = 0;
        }

        public cell(int X, int Y)
        {
            ROW = X;
            COL = Y;
        }
        public int ROW { get; set; }
        public int COL { get; set; }
    }

    public class ExcelServices
    {
        public static byte[] Export<T>(byte[] file, List<T> dataImport)
        {
            byte[] finalOutput = new byte[100];
            List<string> propOfMap = new List<string>();
            var mapObj = new ExpandoObject() as IDictionary<string, object>;
            cell header = new cell(), body = new cell(), mapIndex = new cell();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (MemoryStream stream = new MemoryStream(file))
            {
                using (ExcelPackage excelPackage = new ExcelPackage(stream))
                {

                    //loop all worksheets
                    var MapSheet = excelPackage.Workbook.Worksheets["Mapping"];
                    var DataSheet = excelPackage.Workbook.Worksheets["Data"];
                    for (int i = MapSheet.Dimension.Start.Row; i <= MapSheet.Dimension.End.Row; i++)
                    {
                        //loop all columns in a row
                        for (int j = MapSheet.Dimension.Start.Column; j <= MapSheet.Dimension.End.Column; j++)
                        {
                            //add the cell data to the List
                            switch (MapSheet.Cells[i, j].Value as string)
                            {
                                case "Header":
                                    header = new cell(i, j);
                                    break;
                                case "Body":
                                    body = new cell(i, j);
                                    break;
                                case "Mapping":
                                    mapIndex = new cell(i, j);
                                    break;
                            }

                        }
                    }

                    for (int i = mapIndex.ROW + 1; i <= MapSheet.Dimension.End.Row; i++)
                    {
                        //loop all columns in a row
                        if (MapSheet.Cells[i, mapIndex.COL].Value as string != null && MapSheet.Cells[i, mapIndex.COL + 1].Value != null)
                        {
                            mapObj.Add(MapSheet.Cells[i, mapIndex.COL].Value as string, MapSheet.Cells[i, mapIndex.COL + 1].Value);
                        }
                    }

                    for (int i = 0; i < dataImport.Count; i++)
                    {
                        //loop all columns in a row
                        DataSheet.Cells[Convert.ToInt32(MapSheet.Cells[body.ROW, 2].Value) + 1 + i, 1].Value = i + 1;
                        foreach (var item in mapObj)
                        {
                            var Address = (item.Value as string) + (Convert.ToInt32(MapSheet.Cells[body.ROW, 2].Value) + 1 + i).ToString();
                            var propValue = dataImport[i].GetType().GetProperty(item.Key.ToString()).GetValue(dataImport[i]);
                            if (propValue is DateTime)
                            {
                                propValue = (propValue as DateTime?).Value.ToString("dd/MM/yyyy hh:mm:ss");
                            }
                            DataSheet.Cells[Address].Value = propValue;
                        }
                    }
                    excelPackage.Workbook.Worksheets.Delete(MapSheet);
                    finalOutput = excelPackage.GetAsByteArray();
                }
            }
            return finalOutput;
        }

    }
}
