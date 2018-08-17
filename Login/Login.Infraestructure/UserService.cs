using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Login.Entities;
using System.Data.Entity.Core.Objects;

namespace Login.Infraestructure
{
    public class UserService
    {
        public Tuple<User,int> Login(string username, string password)
        {
            LoginDBEntities context = new LoginDBEntities();
            var result = new ObjectParameter("result", typeof(int));
            context.sp_Login(username, password, result);
            int output = Int32.Parse(result.Value.ToString());
            if (output == 0)
            {
                return new Tuple<User, int>(context.User.FirstOrDefault(x => x.Username == username && x.Password == password),output);
            }
            else {
                return new Tuple<User, int>(null,output);
            }
        }
    }
}
