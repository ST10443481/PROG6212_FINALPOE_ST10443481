using System.Collections.Concurrent;
using CMCS.Web.Models;

namespace CMCS.Web.Services
{
    public class InMemoryUserStore : IUserStore
    {
        private readonly ConcurrentDictionary<Guid, User> _db = new();

        public InMemoryUserStore()
        {
            // seed sample user(s)
            var u = new User
            {
                UserId = Guid.NewGuid(),
                FullName = "Lecturer A",
                Email = "lecturer.a@example.com",
                Role = "Lecturer",
                HourlyRate = 250m
            };
            _db[u.UserId] = u;

            var hr = new User
            {
                UserId = Guid.NewGuid(),
                FullName = "HR Admin",
                Email = "hr@example.com",
                Role = "HR",
                HourlyRate = 0m
            };
            _db[hr.UserId] = hr;
        }

        public IEnumerable<User> All() => _db.Values.OrderBy(u => u.FullName);

        public User? Get(Guid id) => _db.TryGetValue(id, out var u) ? u : null;

        public User? GetByEmail(string email) =>
            _db.Values.FirstOrDefault(u => string.Equals(u.Email, email, StringComparison.OrdinalIgnoreCase));

        public User Add(User user)
        {
            user.UserId = user.UserId == Guid.Empty ? Guid.NewGuid() : user.UserId;
            _db[user.UserId] = user;
            return user;
        }

        public void Update(User user)
        {
            if (user == null) return;
            _db[user.UserId] = user;
        }

        public void Delete(Guid id)
        {
            _db.TryRemove(id, out _);
        }
    }
}

