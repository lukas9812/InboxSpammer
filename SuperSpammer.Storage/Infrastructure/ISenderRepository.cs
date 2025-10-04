using SuperSpammer.Common;

namespace SuperSpammer.Storage.Infrastructure;

public interface ISenderRepository
{
    Task<IEnumerable<SenderDto>> GetAll();
    Task Create(SenderDto sender);
}