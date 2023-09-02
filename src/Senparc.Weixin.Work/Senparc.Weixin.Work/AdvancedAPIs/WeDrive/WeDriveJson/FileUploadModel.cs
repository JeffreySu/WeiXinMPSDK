using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive.WeDriveJson
{
    /// <summary>
    /// 指定位置上传文件入口参数
    /// </summary>
    public class FileUploadModel
    {
        /// <summary>
        /// 空间spaceid
        /// </summary>
        public string spaceid { get; set; }
        /// <summary>
        /// 父目录fileid, 在根目录时为空间spaceid
        /// </summary>
        public string fatherid { get; set; }
        /// <summary>
        /// 	微盘和文件选择器jsapi返回的selectedTicket。若填此参数，则不需要填spaceid/fatherid。
        /// </summary>
        public string selected_ticket { get; set; }
        /// <summary>
        /// 文件名字（注意：文件名最多填255个字符, 英文算1个, 汉字算2个）
        /// </summary>
        public string file_name { get; set; }
        /// <summary>
        /// 	文件内容base64（注意：只需要填入文件内容的Base64，不需要添加任何如："data:application/x-javascript;base64" 的数据类型描述信息），文件大小上限为10M。大于10M文件，可使用文件分块上传接口
        /// </summary>
        public string file_base64_content { get; set; }
    }
}
