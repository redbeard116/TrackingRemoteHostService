using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingRemoteHostService.Models
{
    public abstract class BaseModel
    {
        /// <summary>
        /// Идентификатор объекта
        /// </summary>
        [Key, Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
        }
    }
}
