using CMCS.Web.Models;

namespace CMCS.Web.Services
{
    public interface IUserStore
    {
        IEnumerable<User> All();
        User? Get(Guid id);
        User? GetByEmail(string email);
        User Add(User user);
        void Update(User user);
        void Delete(Guid id);
    }
}
