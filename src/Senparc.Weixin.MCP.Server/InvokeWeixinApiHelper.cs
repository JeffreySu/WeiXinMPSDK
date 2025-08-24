using System;
using System.Reflection;
using System.Threading.Tasks;

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

    /// <summary>
    /// 调用完整方法字符串，自动解析类型和方法名
    /// </summary>
    /// <param name="fullMethodPath">完整方法路径，如 "Senparc.Weixin.MP.AdvancedAPIs.CustomApi.SendTextAsync"</param>
    /// <param name="parameters">方法参数</param>
    /// <returns>方法执行结果</returns>
    public object InvokeFullMethod(string fullMethodPath, object[] parameters)
    {
        var (typeName, methodName) = ParseFullMethodPath(fullMethodPath);
        return InvokeStaticMethod(typeName, methodName, parameters);
    }

    /// <summary>
    /// 异步调用完整方法字符串，自动解析类型和方法名
    /// </summary>
    /// <param name="fullMethodPath">完整方法路径，如 "Senparc.Weixin.MP.AdvancedAPIs.CustomApi.SendTextAsync"</param>
    /// <param name="parameters">方法参数</param>
    /// <returns>方法执行结果</returns>
    public async Task<object> InvokeFullMethodAsync(string fullMethodPath, object[] parameters)
    {
        var (typeName, methodName) = ParseFullMethodPath(fullMethodPath);
        return await InvokeStaticMethodAsync(typeName, methodName, parameters);
    }

    /// <summary>
    /// 解析完整方法路径，分离类型名和方法名
    /// </summary>
    /// <param name="fullMethodPath">完整方法路径</param>
    /// <returns>类型名和方法名的元组</returns>
    private (string typeName, string methodName) ParseFullMethodPath(string fullMethodPath)
    {
        if (string.IsNullOrEmpty(fullMethodPath))
            throw new ArgumentException("方法路径不能为空", nameof(fullMethodPath));

        var lastDotIndex = fullMethodPath.LastIndexOf('.');
        if (lastDotIndex == -1)
            throw new ArgumentException("无效的方法路径格式", nameof(fullMethodPath));

        var typeName = fullMethodPath.Substring(0, lastDotIndex);
        var methodName = fullMethodPath.Substring(lastDotIndex + 1);

        return (typeName, methodName);
    }

    /// <summary>
    /// 调用静态方法
    /// </summary>
    /// <param name="typeName">类型名称</param>
    /// <param name="methodName">方法名称</param>
    /// <param name="parameters">参数</param>
    /// <returns>方法执行结果</returns>
    public object InvokeStaticMethod(string typeName, string methodName, object[] parameters)
    {
        try
        {
            var type = Type.GetType(typeName);
            if (type == null)
            {
                // 尝试从当前加载的程序集中查找类型
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    type = assembly.GetType(typeName);
                    if (type != null) break;
                }
            }

            if (type == null)
                throw new InvalidOperationException($"无法找到类型: {typeName}");

            var method = type.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public);
            if (method == null)
                throw new InvalidOperationException($"无法找到静态方法: {typeName}.{methodName}");

            return method.Invoke(null, parameters);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"调用方法 {typeName}.{methodName} 时发生错误", ex);
        }
    }

    /// <summary>
    /// 异步调用静态方法
    /// </summary>
    /// <param name="typeName">类型名称</param>
    /// <param name="methodName">方法名称</param>
    /// <param name="parameters">参数</param>
    /// <returns>方法执行结果</returns>
    public async Task<object> InvokeStaticMethodAsync(string typeName, string methodName, object[] parameters)
    {
        try
        {
            var type = Type.GetType(typeName);
            if (type == null)
            {
                // 尝试从当前加载的程序集中查找类型
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    type = assembly.GetType(typeName);
                    if (type != null) break;
                }
            }

            if (type == null)
                throw new InvalidOperationException($"无法找到类型: {typeName}");

            var method = type.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public);
            if (method == null)
                throw new InvalidOperationException($"无法找到静态方法: {typeName}.{methodName}");

            var result = method.Invoke(null, parameters);

            // 检查返回值是否是 Task 或 Task<T>
            if (result is Task task)
            {
                await task;
                
                // 如果是 Task<T>，获取结果
                if (task.GetType().IsGenericType)
                {
                    var property = task.GetType().GetProperty("Result");
                    return property?.GetValue(task);
                }
                
                return null; // Task 没有返回值
            }

            return result;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"调用方法 {typeName}.{methodName} 时发生错误", ex);
        }
    }
}
