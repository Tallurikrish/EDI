using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using EDIIntegration.EDIIntegrationDTO;
using System.Xml;
//using Oracle.ManagedDataAccess.Client;

namespace EDIIntegration
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]

    public interface IEDIService
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
               UriTemplate = "GetAllegraData", ResponseFormat = WebMessageFormat.Xml)]
        string GetAllegraData();

        [OperationContract]
        [WebInvoke(Method = "POST",
               UriTemplate = "SaveData", ResponseFormat = WebMessageFormat.Xml, RequestFormat = WebMessageFormat.Xml)]
        XmlElement SaveAllegraData(AllegraDataModel xmlMessage);       

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "DeleteAllegraData", ResponseFormat = WebMessageFormat.Xml)]
        string DeleteAllegraData(string cdata);
    }
}



