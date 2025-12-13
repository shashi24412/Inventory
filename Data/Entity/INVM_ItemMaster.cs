using System;
namespace Data.Entity;

public class INVM_ItemMaster : BaseEntity
{
    public string ItemCode { get; set; } = "";
    public string ItemName { get; set; } = "";
    public int DrugFormId { get; set; }
    public string Strength { get; set; } = "";
    public int PackSize { get; set; }
}
