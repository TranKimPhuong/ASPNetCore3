
using WebApi.Conversion4.Models.Maps;

namespace WebApi.Conversion4.Services.CustomerA
{
    public class CustomerASupplierMap : Map
    {
        public override void SetMapHeader()
        {
            //throw new NotImplementedException();
            DataFromLine = 2;
            AddressPreFixs.Add("VendorAddress");
            MapHeader.AddField("VendorId", 1);
            MapHeader.AddField("VendorName", 2);
            MapHeader.AddField("VendorAddress1", 3);
            MapHeader.AddField("VendorAddress2", 4);
            MapHeader.AddField("VendorAddress3", 5);
            MapHeader.AddField("VendorCity", 6);
            MapHeader.AddField("VendorState", 7);
            MapHeader.AddField("VendorZip", 8);
            MapHeader.AddField("VendorPhone", 9);
        }

        public override void SetMapDetail()
        {
            return;
        }

        public override void SetMapRemittance()
        {
            base.SetMapRemittance();
        }
    }
}