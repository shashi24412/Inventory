using Data.Entity;

namespace Data.Contract;

public interface I_ItemMasterRepository
{
    public Task<IEnumerable<INVM_ItemMaster>> GetItemMasterByIdAsync(int id);
}