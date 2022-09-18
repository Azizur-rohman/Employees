using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employees.Models
{
    public class TblEmployee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(150)]
        public string? Name { get; set; }
        [StringLength(250)]
        public string? Email { get; set; }
        public int Age { get; set; }
        public DateTime Doj { get; set; }
        [StringLength(50)]
        public string? Gender { get; set; }
        public bool IsActive { get; set; }
        public int DesignationId { get; set; }
        [ForeignKey("DesignationId")]
        public virtual TblDesignation TblDesignation { get; set; }
    }
}
