using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mission06_Trotter.Models;

public class Movie
{
    [Key]
    public int MovieId { get; set; }
    
    [ForeignKey("CategoryId")]
    public int CategoryId { get; set; }
    public Categories? Category { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    [Range(1888, 2026, ErrorMessage = "Year must be between 1888 and 2026." )]
    public int Year { get; set; }
    
    public string? Director { get; set; }
    
    public string? Rating { get; set; }

    [Required]
    public bool Edited { get; set; }

    public string? LentTo { get; set; }
    
    [Required]
    public bool CopiedToPlex { get; set; }

    [StringLength(25, ErrorMessage = "Notes cannot exceed 25 characters.")]
    public string? Notes { get; set; }
}
