using Business.Contract;
using Business.ViewModel;
using Data.Contract;
using Data.Entity;
using AutoMapper;

namespace Business.Implementation;

public class ItemMasterManager : I_ItemMasterManager
{
    private readonly I_ItemMasterRepository _itemMasterRepository;
    private readonly IMapper _mapper;

    public ItemMasterManager(I_ItemMasterRepository itemMasterRepository, IMapper mapper)
    {
        _itemMasterRepository = itemMasterRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<INVM_ItemMasterViewModel>> GetItemMasterAsync(int id)
    {
        var itemMaster = await _itemMasterRepository.GetItemMasterByIdAsync(id);
        var vModule = _mapper.Map<IEnumerable<INVM_ItemMasterViewModel>>(itemMaster);
        return vModule;
    }
}

