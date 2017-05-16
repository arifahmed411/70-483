using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AttributeDemo
{
    class DataContractDemo
    {


    }

    [ServiceContract]
    public interface ISampleInterface
    {
        // No data contract is required since both the parameter 
        // and return types are primitive types.
        [OperationContract]
        double SquareRoot(int root);

        // No Data Contract required because both parameter and return 
        // types are marked with the SerializableAttribute attribute.
        [OperationContract]
        System.Drawing.Bitmap GetPicture(System.Uri pictureUri);

        // The MyTypes.PurchaseOrder is a complex type, and thus 
        // requires a data contract.
        [OperationContract]
        bool ApprovePurchaseOrder(MyTypes.PurchaseOrder po);
    }
}

namespace MyTypes
{
    [DataContract]
    public class PurchaseOrder
    {
        private int poId_value;

        // Apply the DataMemberAttribute to the property.
        [DataMember]
        public int PurchaseOrderId
        {

            get { return poId_value; }
            set { poId_value = value; }
        }
    }
}
