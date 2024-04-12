using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Cavaleiro
{
    [JsonPropertyName("pk")]
    public string Pk { get; set; }

    [JsonPropertyName("nome")]
    public string Nome { get; set; }

    [JsonPropertyName("local_treinamento")]
    public string LocalDeTreinamento { get; set; }

    [JsonPropertyName("armadura")]
    public string Armadura { get; set; }

    [JsonPropertyName("constelacao")]
    public string Constelacao { get; set; }

    [JsonPropertyName("golpe_principal")]
    public string GolpePrincipal { get; set; }

    [JsonPropertyName("divindade")]
    public string Divindade { get => Armadura != "Sapuris" ? "Atena" : "Hades"; }

    [JsonPropertyName("referencia_imagem")]
    public string ReferenciaImagem { get; set; }
}