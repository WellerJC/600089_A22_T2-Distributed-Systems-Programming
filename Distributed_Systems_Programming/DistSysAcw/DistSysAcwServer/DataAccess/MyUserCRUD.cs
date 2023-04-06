using DistSysAcwServer.Models;

namespace DistSysAcwServer.DataAccess
{
    public class MyUserCRUD : UserContext
    {

        public string Create(string username)
        {
            string api;

            using (var ctx = new UserContext())
            {
                User user = new User()
                {
                    UserName = username,
                    ApiKey = Guid.NewGuid().ToString(),
                    Role = "Admin"
                };

                ctx.Users.Add(user);
                ctx.SaveChanges();
                api = user.ApiKey;
            };

            return api;
        }
        public bool CheckApi(string ApiKey)
        {
            using (var ctx = new UserContext())
            {
                var user = ctx.Users.FirstOrDefault(u => u.ApiKey == ApiKey);

                if (user != null) { return true; }
                else { return false; }
            }
        }
        public bool CheckUser(string UserName)
        {
            using (var ctx = new UserContext())
            {
                var user = ctx.Users.FirstOrDefault(u => u.UserName == UserName);

                if (user != null) { return true; }
                else { return false; }
            }
        }
        public bool CheckApiUser(string ApiKey, string Username) 
        {
            using (var ctx = new UserContext())
            {
                var user = ctx.Users.FirstOrDefault(u => u.ApiKey == ApiKey && u.UserName == Username);

                if (user != null) { return true; }
                else { return false; }
            }
        }
        public User ReturnUser(string ApiKey)
        {
            using (var ctx = new UserContext())
            {
                var user = ctx.Users.FirstOrDefault(u => u.ApiKey == ApiKey);

                if (user != null) { return user; }
                else 
                {
                    User NoUser = new User()
                    {
                        UserName = "No User",
                        
                    }; 
                    return NoUser; 
                }
            }

        }
        public string ReturnUserName(string ApiKey)
        {
            using (var ctx = new UserContext())
            {
                var user = ctx.Users.FirstOrDefault(u => u.ApiKey == ApiKey);

                return user.UserName;
            }
        }
        public void DeleteUser(string ApiKey)
        {
            using (var ctx = new UserContext())
            {
                var user = ctx.Users.FirstOrDefault(u => u.ApiKey == ApiKey);
              
                if (user != null)
                {     
                    ctx.Users.Remove(user);
                    ctx.SaveChanges();
                }
            }
        }

        public void ChangeRole(string userName, string role)
        {
            using (var ctx = new UserContext())
            {
                var user = ctx.Users.FirstOrDefault(u => u.UserName == userName);

                user.Role = role;

                    ctx.SaveChanges();
            }
        }
    }
}
