using Business.ViewModel;

namespace Business.Contract;

public interface I_ItemMasterManager
{
    public Task<IEnumerable<INVM_ItemMasterViewModel>> GetItemMasterAsync(int id);
}