namespace Client.TenPayHttpClient.Signer
{
    public interface ISigner
    {
        string GetAlgorithm(); // 返回签名算法, 用于生成Authorization
        string Sign(string message, string privateKey = null); // 对信息进行签名
    }
}
