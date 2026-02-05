using Business.ViewModel;

namespace Business.Contract;

public interface I_ItemMasterManager
{
    Task<IEnumerable<INVM_ItemMasterViewModel>> GetItemMasterAsync(int id);
    Task<bool> InsertDataAsync(INVM_ItemMasterViewModel item);
}