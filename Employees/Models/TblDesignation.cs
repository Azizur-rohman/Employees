using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employees.Models
{
    public class TblDesignation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DesignationId { get; set; }
        [StringLength(250)]
        public string? Designation { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<TblEmployee> TblEmployees { get; set; }

    }
}
