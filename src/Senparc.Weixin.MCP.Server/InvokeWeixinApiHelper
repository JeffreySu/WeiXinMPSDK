public class InvokeWeixinApiHelper
{
    public InvokeWeixinApiHelper()
    {
    }

    public void Invoke(string method, object[] parameters)
    {
        //Use reflection to find and invoke the method
        var methodInfo = GetType().GetMethod(method);
        if (methodInfo != null)
        {
            methodInfo.Invoke(this, parameters);
        }
    }
}
