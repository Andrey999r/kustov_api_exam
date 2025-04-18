namespace Backend.Core.DTOs;
using System.ComponentModel.DataAnnotations;

public class TaskDto
{
    [Required(ErrorMessage = "Поле Title обязательно")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Поле Description обязательно")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Поле Status обязательно")]
    public string Status { get; set; } = "Pending";
}