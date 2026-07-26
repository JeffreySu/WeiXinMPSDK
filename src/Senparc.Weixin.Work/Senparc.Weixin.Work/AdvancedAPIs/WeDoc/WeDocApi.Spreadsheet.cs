/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：WeDocApi.Spreadsheet.cs
    文件功能描述：企业微信电子表格接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐电子表格批量更新、属性和范围数据接口

----------------------------------------------------------------*/

using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDoc
{
    public static partial class WeDocApi
    {
        private const string SpreadsheetBatchUpdatePath = "/cgi-bin/wedoc/spreadsheet/batch_update";
        private const string SpreadsheetPropertiesPath = "/cgi-bin/wedoc/spreadsheet/get_sheet_properties";
        private const string SpreadsheetRangeDataPath = "/cgi-bin/wedoc/spreadsheet/get_sheet_range_data";

        /// <summary>按顺序批量更新电子表格，单次最多提交 5 个操作。</summary>
        public static WeDocSpreadsheetBatchUpdateResult BatchUpdateSpreadsheet(string accessTokenOrAppKey,
            WeDocSpreadsheetBatchUpdateRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDocSpreadsheetBatchUpdateResult>(accessTokenOrAppKey, SpreadsheetBatchUpdatePath,
                request, timeOut);

        /// <summary>异步按顺序批量更新电子表格，单次最多提交 5 个操作。</summary>
        public static Task<WeDocSpreadsheetBatchUpdateResult> BatchUpdateSpreadsheetAsync(
            string accessTokenOrAppKey, WeDocSpreadsheetBatchUpdateRequest request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocSpreadsheetBatchUpdateResult>(accessTokenOrAppKey, SpreadsheetBatchUpdatePath,
                request, timeOut);

        /// <summary>获取电子表格的工作表、行数和列数。</summary>
        public static WeDocSpreadsheetPropertiesResult GetSpreadsheetProperties(string accessTokenOrAppKey,
            WeDocIdRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDocSpreadsheetPropertiesResult>(accessTokenOrAppKey, SpreadsheetPropertiesPath,
                request, timeOut);

        /// <summary>异步获取电子表格的工作表、行数和列数。</summary>
        public static Task<WeDocSpreadsheetPropertiesResult> GetSpreadsheetPropertiesAsync(
            string accessTokenOrAppKey, WeDocIdRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocSpreadsheetPropertiesResult>(accessTokenOrAppKey, SpreadsheetPropertiesPath,
                request, timeOut);

        /// <summary>读取指定工作表 A1 范围内的单元格数据和格式。</summary>
        public static WeDocSpreadsheetDataResult GetSpreadsheetRangeData(string accessTokenOrAppKey,
            WeDocSpreadsheetRangeRequest request, int timeOut = Config.TIME_OUT)
            => Post<WeDocSpreadsheetDataResult>(accessTokenOrAppKey, SpreadsheetRangeDataPath,
                request, timeOut);

        /// <summary>异步读取指定工作表 A1 范围内的单元格数据和格式。</summary>
        public static Task<WeDocSpreadsheetDataResult> GetSpreadsheetRangeDataAsync(
            string accessTokenOrAppKey, WeDocSpreadsheetRangeRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WeDocSpreadsheetDataResult>(accessTokenOrAppKey, SpreadsheetRangeDataPath,
                request, timeOut);
    }
}
