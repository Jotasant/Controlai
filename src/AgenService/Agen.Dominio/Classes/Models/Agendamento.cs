using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Agendamento
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("demandaId")]
    public string DemandaId { get; set; } = null!;

    public Cliente Cliente { get; set; } = null!;
    public Servico Servico { get; set; } = null!;
    public string TecnicoResponsavel { get; set; } = null!;
    public DateTime DataAgendamento { get; set; }
    public string Status { get; set; } = null!;
    public List<Notificacao> Notificacoes { get; set; } = new();
    public string Observacoes { get; set; } = null!;
}






