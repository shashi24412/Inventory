using Data.Entity;

namespace Data.Contract;

public interface I_ItemMasterRepository
{
    Task<IEnumerable<INVM_ItemMaster>> GetItemMasterByIdAsync(int id);
    Task<bool> InsertDataAsync(INVM_ItemMaster item);
}