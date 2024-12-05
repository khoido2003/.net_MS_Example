using System.ComponentModel.DataAnnotations;
using CommandsService.Models;

namespace CommandsService.Models
{
  public class Command
  {

    [Required]
    [Key]
    public int Id { get; set; }

    [Required]
    public string HowTo { get; set; }

    [Required]
    public string CommandLine { get; set; }

    [Required]
    public int PlatformId { get; set; }

    public Platform Platform { get; set; }
  }
}
