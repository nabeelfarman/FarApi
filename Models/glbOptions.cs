public class glbOptions
{
    public static string glbFirst;

    public string getGlbFirst()
    {
        glbFirst = "Server=tcp:10.1.1.1,1433;Initial Catalog=FAR;Persist Security Info=False;User ID=far;Password=telephone@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
        return glbFirst;
    }
}