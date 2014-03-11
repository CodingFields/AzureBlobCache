namespace AzureBlobCache
{
    public enum Caches
    {
        User,
        Page,
        Misc
    }

    public enum Enviornments
    {
        Staging,
        Production
    }

    public static class EnumExtensions
    {
        public static string EnumToPage(this Caches cache)
        {
            return cache.ToString();
        }
    }
}
