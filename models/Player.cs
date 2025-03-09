using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TodoApi.Models
{
    public class Player
    {
        [BsonId] // Indica que esta es la clave primaria en MongoDB
        [BsonRepresentation(BsonType.ObjectId)] // Permite tratarlo como string
        public String Id { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public int Puntuacion { get; set; }
    }
}