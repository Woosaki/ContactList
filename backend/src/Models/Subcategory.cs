using System.Text.Json.Serialization;

namespace ContactListAPI.Models;

public class Subcategory
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CategoryId { get; set; }

    [JsonIgnore]
    public Category Category { get; set; } = null!;
}
