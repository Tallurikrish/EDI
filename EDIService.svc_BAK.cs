using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;
using System.Reflection;
using System.Configuration;
using EDIIntegration.EDIIntegrationDTO;


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
        public EDIResponse SaveAllegraData(AllegraDataModel xmlMessage)
        {
            
            return _response;
        }

        #endregion       

        #region Delete Allegra Detail
        public string DeleteAllegraData(string value)
        {
            return string.Format("You entered: {0}", value);
        }

        #endregion

        #region Private Methods

        private Tuple<List<string>, List<string>> GetRecursiveChildClaimDetail(object xmlMessage, PropertyInfo[] childProperties, string propertyName)
        {
            List<string> Columns = new List<string>();
            List<string> Values = new List<string>();
            Tuple<List<string>, List<string>> childTuple = null;

            var claimResponse = xmlMessage.GetType().GetProperty(propertyName).GetValue(xmlMessage, null);
            if (claimResponse == null)
                return new Tuple<List<string>, List<string>>(Columns, Values);                    

            foreach (PropertyInfo childProperty in childProperties)
            {
                object val = childProperty.GetValue(claimResponse, null);

                if (childProperty.PropertyType.Name.Contains("DTO"))
                {
                    Type subChildClassType = Type.GetType(childProperty.PropertyType.FullName);
                    PropertyInfo[] subChildProperties = subChildClassType.GetProperties();
                    Tuple<List<string>, List<string>> subChildDetailResult = GetRecursiveChildClaimDetail(claimResponse, subChildProperties, childProperty.Name);
                    Columns.AddRange(subChildDetailResult.Item1);
                    Values.AddRange(subChildDetailResult.Item2);

                }
                else
                {
                    //object val = childProperty.GetValue(xmlMessage, null);
                    if (val == null)
                        continue;

                    Columns.Add(childProperty.Name);
                    Values.Add(val.ToString() != "" ?"'"+ val.ToString()+"'" : "''");
                }
            }

            childTuple = new Tuple<List<string>, List<string>>(Columns, Values);
            return childTuple;
        }
        private Tuple<List<string>, List<string>> GetClaimDetail(AllegraDataModel xmlMessage)
        {
            Tuple<List<string>, List<string>> tuple = null;

            List<string> Columns = new List<string>();
            List<string> Values = new List<string>();

            Type type = typeof(AllegraDataModel);
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                Type childClassType = Type.GetType(property.PropertyType.FullName);
                PropertyInfo[] childProperties = childClassType.GetProperties();

                var childClaimDetail = GetRecursiveChildClaimDetail(xmlMessage, childProperties, property.Name);
                Columns.AddRange(childClaimDetail.Item1);
                Values.AddRange(childClaimDetail.Item2);
            }

           tuple = new Tuple<List<string>, List<string>>(Columns, Values);

            return tuple;
        }

        #endregion
    }
}

