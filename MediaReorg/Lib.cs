using System.Security.Cryptography;
using System.Text;

namespace MediaReorg;


public static class Lib {
    public static string CalculateMD5(string filename)
    {
        using var md5 = MD5.Create();
        using var stream = File.OpenRead(filename);

        var hash = md5.ComputeHash(stream);

        return Convert.ToHexStringLower(hash);
    }
}