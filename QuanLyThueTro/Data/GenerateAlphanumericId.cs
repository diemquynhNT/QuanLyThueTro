using System.Text;

namespace QuanLyThueTro.Data
{
    public class GenerateAlphanumericId
    {
        public string GenerateId(int length)
        {
            const string chars = "0123456789";
            var random = new Random();
            var id = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                id.Append(chars[random.Next(chars.Length)]);
            }

            return id.ToString();
        }
    }
}
