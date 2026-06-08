using TaskFlow.Domain.Entities;

namespace TaskFlow.Domain.Interfaces
{
    public interface ITokenService
    {
        string Generate(User user);
    }
}
