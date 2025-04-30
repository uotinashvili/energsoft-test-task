using System.Text;

namespace EnergsoftInterview.Api.Common.CosmosDb.Helpers
{
    public static class ContinuationTokenHelper
    {
        public static string? EncodeToken(string? token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            try
            {
                var bytes = Encoding.UTF8.GetBytes(token);
                return Convert.ToBase64String(bytes);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string? DecodeToken(string? token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            try
            {
                var bytes = Convert.FromBase64String(token);
                return Encoding.UTF8.GetString(bytes);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
} 