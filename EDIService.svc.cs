using EDIIntegration.EDIIntegrationDTO;
using System.Runtime.Serialization;
using System.Xml;

namespace EDIIntegration
{
   
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class EDIService : IEDIService
    {
        #region Get Allegra Detail

        public string GetAllegraData()
        {
            return string.Format("You entered: {0}", "Test");
        }

        #endregion

        // Added by Krishna Talluri on 19/05/2018

        #region Insert Allegra Detail

        public XmlElement SaveAllegraData(AllegraDataModel xmlMessage)
        {
            IClaimRepository repo = new ClaimRepository();
            XmlElement response = repo.SaveAllegraDetail(xmlMessage);

            return response;
        }

        #endregion       

        #region Delete Allegra Detail
        public string DeleteAllegraData(string value)
        {
            return string.Format("You entered: {0}", value);
        }

        #endregion

       
    }
}

