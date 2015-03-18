namespace Senparc.Weixin.MP.AppStore.Api
{
    /// <summary>
    /// API调用类基类
    /// </summary>
    public class BaseApi
    {
        protected Passport _passport;
        protected ApiConnection ApiConnection { get; set; }

        public BaseApi(Passport passport)
        {
            _passport = passport;
            ApiConnection = new ApiConnection(passport);
        }
    }
}
