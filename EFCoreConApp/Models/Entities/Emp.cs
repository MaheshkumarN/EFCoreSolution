using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreConApp.Models.Entities
{
  public class Emp
  {
    [Key]
    public int EmpNo { get; set; }
    public string EName { get; set; }
    public string Job { get; set; }
    public int? Mgr { get; set; }
    public DateTime HireDate { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public decimal Sal { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal? Comm { get; set; }
    public int DeptNo { get; set; }

    public override string ToString()
    {
      return $"EmpNo: {EmpNo}, EName: {EName}, JOb: {Job}, Mgr: {Mgr}, HireDate: {HireDate}, Sal: {Sal}, Comm: {Comm}, DeptNo: {DeptNo}";
    }

  }
}
