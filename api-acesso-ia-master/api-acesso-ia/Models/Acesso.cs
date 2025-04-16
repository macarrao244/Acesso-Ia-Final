using api_acesso_ia.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Acesso
{
    public int Id { get; set; }

    [ForeignKey("Usuario")]
    public int IdUsuario { get; set; }

    public DateTime DataHoraAcesso { get; set; }

    [JsonIgnore]
    public virtual Usuario? Usuario { get; set; }
}
