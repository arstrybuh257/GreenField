using System;
using System.Text;
using GreenField.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace GreenField.BLL.Authentication
{
    public class PasswordManager
    {
        const string Digits = "0123456789";
        const string Alphabet = "abcdefghijklmnopqrstuvwxyz";
        const string Symbols = " ~`@#$%^&*()_+-=[]{};'\\:\"|,./<>?";

        private IPasswordHasher<User> _passwordHasher;

        public PasswordManager(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public enum CharSet
        {
            Digits,
            Letters,
            DigitsAndLetters,
            SpecChar,
            DigitsAndSpecChar,
            LettersAndSpecChar,
            All
        }
        
        [Flags]
        public enum PasswordChars
        {
            Digits = 0b0001, 
            Alphabet = 0b0010,
            Symbols = 0b0100
        }
        
        public static string GeneratePassword(CharSet charSet, int length)
        {
            PasswordChars passwordChars = (PasswordChars) charSet;
            var random = new Random();
            var resultPassword = new StringBuilder(length);
            var passwordCharSet = string.Empty;
            if (passwordChars.HasFlag(PasswordChars.Alphabet))
            {
                passwordCharSet += Alphabet + Alphabet.ToUpper();
            }
            if (passwordChars.HasFlag(PasswordChars.Digits))
            {
                passwordCharSet += Digits;
            }
            if (passwordChars.HasFlag(PasswordChars.Symbols))
            {
                passwordCharSet += Symbols;
            }
            for (var i = 0; i < length; i++)
            {
                resultPassword.Append(passwordCharSet[random.Next(0, passwordCharSet.Length)]);
            }
            return resultPassword.ToString();
        }
        
        public void SetHashedPassword(User user, string password)
        {
            user.PasswordHash =  _passwordHasher.HashPassword(user, password);
        }

        public bool ValidatePassword(User user, string password)
        {
            return _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password) != 
                PasswordVerificationResult.Failed;
        }
    }
}