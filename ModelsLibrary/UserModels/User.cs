using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.UserModels
{
    public class User : AUser
    {
        public string Token {  get; set; }
        #region Constructors
        public User() : base()
        {
            Token= GenerateToken();
        }
        public User(string name, string password) : base(name,password)
        {
            Token = GenerateToken();
        }
        public User(User user) :
            base(user.Name, user.Password)
        {
            Token=user.Token;
        }

        public User(UserAuthorizationArg user): this(user.Name,user.Password)
        {
            Token = GenerateToken();
        }
        public User(UserAuthorizationArg user, string token) : this(user)
        {
            this.Token = token;
        }
        #endregion
        #region Overrided methods
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            User? ob =obj as User;
            if(ob is null)
                return false;

            return ob.Name==Name;
        }
        #endregion


        public static bool operator==(User left, User right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(User left, User right)
        {
            return !left.Equals(right);
        }
        
        private static string GenerateToken()
        {
            Random random = new Random();
            int times = 64 + random.Next(64);
            return GenerateRandomString(times);
        }
        static string GenerateRandomString(int length)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0;i<length;++i)
                sb.Append(GetRandomChar());

            return sb.ToString();
        }
        static char GetRandomChar()
        {
            Random r = new Random();
            
            char result=(char)(r.Next(65,123)%255);
            return result;
        }
        public void RegenerateToken()
        {
            string oldToken=Token;
            do Token = GenerateToken();
            while (oldToken == Token);
        }
    }
}
