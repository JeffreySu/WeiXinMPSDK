namespace Senparc.Weixin.MP.CoreSample.Controllers.NeuChar
{
    /// <summary>
    /// NeuChar App 处理程序
    /// </summary>
    public class NeuCharAppController : Senparc.NeuChar.App.Controllers.NeuCharAppController
    {
        protected override string Token => "Token";

        /* 基类 NeuCharAppController 已经提供了默认的响应实现，
         * NeuChar 平台的开发者只需要实现这个 Controller 即可完成与 NeuChar 平台 App 的对接，
         * 例如：运行状态监测、接口访问等。
         * Senparc.NeuChar.App 所有行为完全透明开放，开源地址： 
         * https://github.com/Senparc/NeuChar/tree/master/src/Senparc.NeuChar.App
         */
    }
}
