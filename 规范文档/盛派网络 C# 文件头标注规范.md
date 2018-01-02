## 文件头格式

### 1、文件头顶格（没有空行或空格），放置协议或关键说明

如：

``` C#
#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2017 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
```

### 2、紧跟协议或关键说明，为文件说明及版本更新内容
如：

``` C#
/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc

    文件名：ModifyDomainApi.cs
    文件功能描述：修改域名接口


    创建标识：Senparc - 20170601


    修改标识：Senparc - 20171201
    修改描述：v1.7.3 修复ModifyDomainApi.ModifyDomain()方法判断问题
        
    修改标识：Senparc - 20171231
    修改描述：v1.7.4 更新API地址
        
----------------------------------------------------------------*/
```
#### 创建

其中“文件名”“文件功能描述”及“创建标识”必须在文件创建时时填写。

“创建标识”上下各空 2 行。

#### 修改

每次更新文件必须填写“修改标识”及对应的“修改描述”。“修改标识”下，需要填写“修改人”和“时间”，使用“ - ”分隔。

每次修改填写的一组“修改标识”和“修改描述”中间不空行，每组修改信息上下各空 1 行（第一次修改是，上方空 2 行，配合“创建标识”标准）。
