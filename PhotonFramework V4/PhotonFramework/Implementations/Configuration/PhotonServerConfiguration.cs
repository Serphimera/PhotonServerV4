using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ExitGames.Logging;
using NHibernate;
using PhotonCommon.Database;
using PhotonCommon.Database.Domains;
using PhotonFramework.Interfaces.Configuration;

namespace PhotonFramework.Implementations.Configuration
{
    public class PhotonServerConfiguration : IPhotonServerConfiguration
    {
        public IPAddress MasterServerAddress { get; set; }
        public int InternalMasterServerPort { get; set; }
        public int ExternalMasterServerPort { get; set; }
        public bool ConnectsToMasterServer { get; set; }
        public int MaxConnectRetries { get; set; }
        public int ConnectRetryDelay { get; set; }

        private ILogger Logger { get; }

        public PhotonServerConfiguration(ILogger logger, string photonApplication)
        {
            Logger = logger;
            Logger.DebugFormat($"Trying to resolve configuration for {photonApplication} from database.");

            using (ISession session = DatabaseManager.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        IList<ParameterT> result = session.QueryOver<ParameterT>()
                            .Where(p => p.ParCompGroup == "Server")
                            .And(p => p.ParComp == photonApplication)
                            .List<ParameterT>();

                        if (result != null)
                        {
                            MasterServerAddress = IPAddress.Parse(result.FirstOrDefault(l => l.ParKey == "MasterServerAddress").ParValue);
                        }

                    }
                    catch (Exception ex)
                    {
                        Logger.ErrorFormat("Could not execute database operation: {0}", ex.Message);
                        transaction.Rollback();
                    }
                }
            }

        }
    }
}
