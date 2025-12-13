using System.ComponentModel;

namespace Business.ViewModel;

public class INVM_ItemMasterViewModel : BaseViewModel
{
    public string ItemCode { get; set; } = "";
    public string ItemName { get; set; } = "";
    public int DrugFormId { get; set; }
    [DefaultValue("")]
    public string Strength { get; set; } = "";
    public int PackSize { get; set; }
}
