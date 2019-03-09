#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

using System.Collections.Generic;

namespace Senparc.Weixin.MP.AdvancedAPIs.MerChant
{
    public class MBase
    {
        public int eid { get; protected set; }//控件ID
    }

    public class M1 : MBase
    {
        public M1_GroupInfo group_info { get; set; }//分组信息

        /// <summary>
        /// 控件1数据初始化
        /// </summary>
        /// <param name="group_info_filter_count">group_info/filter/count</param>
        /// <param name="group_info_group_id">group_info/group_id</param>
        public M1(int group_info_filter_count, int group_info_group_id)
        {
            base.eid = 1;
            group_info = new M1_GroupInfo()
            {
                filter = new Filter()
                {
                    count = group_info_filter_count
                },
                group_id = group_info_group_id
            };
        }
    }

    public class M1_GroupInfo
    {
        public Filter filter { get; set; }
        public int group_id { get; set; }//分组ID
    }

    public class Filter
    {
        public int count { get; set; }//该控件展示商品个数
    }

    public class M2 : MBase
    {
        public M2_GroupInfos group_infos { get; set; }//分组数组

        /// <summary>
        /// 控件2数据初始化
        /// </summary>
        /// <param name="groupIds">groups/[group_id]</param>
        public M2(int[] groupIds)
        {
            group_infos = new M2_GroupInfos()
                {
                    groups = new List<Group>()
                };

            for (int i = 0; i < groupIds.Length; i++)
            {
                group_infos.groups.Add(new Group() { group_id = groupIds[i] });
            }
            base.eid = 2;
        }
    }

    public class M2_GroupInfos
    {
        public List<Group> groups { get; set; }//分组ID
    }

    public class Group
    {
        public int group_id { get; set; }//分组ID
    }

    public class M3 : MBase
    {
        public GroupInfo group_info { get; set; }//分组信息

        /// <summary>
        /// 控件3数据初始化
        /// </summary>
        /// <param name="group_info_group_id"></param>
        /// <param name="group_info_image"></param>
        public M3(int group_info_group_id, string group_info_image)
        {
            base.eid = 3;
            group_info = new GroupInfo()
            {
                group_id = group_info_group_id,
                img = group_info_image
            };
        }
    }

    public class GroupInfo
    {
        public int group_id { get; set; }//分组ID
        public string img { get; set; }//分组照片(图片需调用图片上传接口获得图片Url填写至此，否则添加货架失败，建议分辨率600*208)
    }

    public class M4 : MBase
    {
        public M4_GroupInfos group_infos { get; set; }

        /// <summary>
        /// 控件4数据初始化
        /// </summary>
        /// <param name="groupIds">groups/[group_id]</param>
        /// <param name="imgs">groups/[img]</param>
        /// 注意groupIds和imgs要对应
        public M4(int[] groupIds,string[] imgs)
        {
            group_infos = new M4_GroupInfos()
                {
                    groups = new List<GroupInfo>()
                };

            for (int i = 0; i < groupIds.Length; i++)
            {
                group_infos.groups.Add(new GroupInfo() { group_id = groupIds[i], img = imgs[i] });
            }
            base.eid = 4;
        }
    }

    public class M4_GroupInfos
    {
        public List<GroupInfo> groups { get; set; }
    }

    public class M5 : MBase
    {
        public M5_GroupInfos group_infos { get; set; }

        /// <summary>
        /// 控件5数据初始化
        /// </summary>
        /// <param name="groupIds">groups/[group_id]</param>
        /// <param name="imgBackground">groups/img_background</param>
        public M5(int[] groupIds, string imgBackground)
        {
            group_infos = new M5_GroupInfos()
            {
                groups = new List<Group>(),
                img_background = imgBackground
            };

            for (int i = 0; i < groupIds.Length; i++)
            {
                group_infos.groups.Add(new Group() { group_id = groupIds[i] });
            }
            base.eid = 4;
        }
    }

    public class M5_GroupInfos
    {
        public List<Group> groups { get; set; }
        public string img_background { get; set; }
    }
}

