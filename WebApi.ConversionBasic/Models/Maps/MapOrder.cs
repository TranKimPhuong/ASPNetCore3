namespace WebApi.ConversionBasic.Models.Maps
{
    public class MapOrder
    {
        /// <summary>
        /// Field can sap xep
        /// </summary>
        public string Field { get; set; }

        public bool Descending { get; set; }

        public MapOrder(string field, bool descending = false)
        {
            this.Field = field;
            this.Descending = descending;
        }
    }
}

