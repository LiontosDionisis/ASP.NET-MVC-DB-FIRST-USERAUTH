namespace TeachersMVC.Security
{
    public class EncryptionUtil
    {
        public static string Encrypt(string clearText)
        {
            var encriptedPassword = BCrypt.Net.BCrypt.HashPassword(clearText);
            return encriptedPassword;
        }

        public static bool IsValidPassword(string plainText, string cipherText)
        {
            var isValid = BCrypt.Net.BCrypt.Verify(plainText, cipherText);
            return isValid;
        }
    }
}
