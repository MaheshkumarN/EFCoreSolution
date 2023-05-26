using System.ComponentModel.DataAnnotations;

namespace EFCoreConApp.Models.Entities
{
  public class Dept
  {
    [Key]
    public int DeptNo { get; set; }
    public string DName { get; set; }
    public string Loc { get; set; }

    public override string ToString()
    {
      return $"DeptNo: {DeptNo}, DName: {DName}, Loc: {Loc}";
    }
  }
}
