using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SoapBroadProvider
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private const string ConnectionString = "Server=tcp:mhl.database.windows.net,1433;Initial Catalog=MhlDatabase;Persist Security Info=False;User ID=mick0433;Password=Espltdkh20258;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }


        public int InsertMeasurements(int light, int temp, int potent, int analog)
        {
            const string insertTime = "insert into Broadcast_Time (light, temp, potent, analog) values (@Light, @Temp, @Potent, @Analog)";

            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();

                using (SqlCommand insertCommand = new SqlCommand(insertTime, databaseConnection))
                {
                    insertCommand.Parameters.AddWithValue("@Light", light);
                    insertCommand.Parameters.AddWithValue("@Temp", temp);
                    insertCommand.Parameters.AddWithValue("@Potent", potent);
                    insertCommand.Parameters.AddWithValue("@Analog", analog);
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }
    }
}
