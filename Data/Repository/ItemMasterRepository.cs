namespace Data.Repository;

using Data.Contract;
using Data.Entity;
using System.Threading.Tasks;
public class ItemMasterRepository : I_ItemMasterRepository
{
    public async Task<IEnumerable<INVM_ItemMaster>> GetItemMasterByIdAsync(int id)
    {
        // Implementation to retrieve ItemMaster by id from the data source
        // This is a placeholder for actual data access code
        List<INVM_ItemMaster> items = new List<INVM_ItemMaster>();
        items.Add(new INVM_ItemMaster
        {
            Id = 1,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
            CreatedBy = "Admin",
            ModifiedBy = "Admin",
            Active = true,
            ItemCode = "ITM001",
            ItemName = "Sample Item",
            DrugFormId = 2,
            Strength = "500mg",
            PackSize = 30
        });
        return items;
    }
}