using Newtonsoft.Json;

namespace Mashinin.Entities
{
    public class Make : BaseEntity
    {
        //markalarimiz
        public string Name { get; set; }
        public int TurboAzId { get; set; }


        public List<Model> Models { get; set; }
    }
}
