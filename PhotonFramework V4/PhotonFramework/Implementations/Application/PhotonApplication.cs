// ********************************************************************************
// This file is part of PhotonFramework V4.
// 
// PhotonFramework V4is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// PhotonFramework V4 is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with PhotonFramework V4.  If not, see <http://www.gnu.org/licenses/>.
// ********************************************************************************
// Copyright (C) 2016 Sebastian Kenter. All rights reserved.
//   
// Name    : PhotonFramework V4 | PhotonFramework | PhotonApplication.cs
// Created : 2016-12-09 12:56
// Modified: 2016-12-09 12:57
// ********************************************************************************

using System.IO;
using Autofac;
using Autofac.Configuration;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net;
using log4net.Config;
using Microsoft.Extensions.Configuration;
using Photon.SocketServer;
using PhotonFramework.Interfaces.Application;
using PhotonFramework.Interfaces.Client;
using PhotonFramework.Interfaces.Configuration;
using PhotonFramework.Interfaces.Server;
using LogManager = ExitGames.Logging.LogManager;

namespace PhotonFramework.Implementations.Application
{
    public class PhotonApplication : ApplicationBase
    {
        #region public properties

        #endregion

        #region protected properties

        #endregion

        #region private properties

        private IPhotonServerApplication PhotonServerModule { get; set; }
        private IPhotonServerConfiguration PhotonServerConfiguration { get; set; }
        private IPhotonServerPeerFactory PhotonServerPeerFactory { get; set; }
        private IPhotonClientPeerFactory PhotonClientPeerFactory { get; set; }
        private ILogger Logger { get; set; }

        #endregion

        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            return null;
        }

        protected override void Setup()
        {
            #region Initialize Logging
            LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
            GlobalContext.Properties["LogName"] = ApplicationName;
            GlobalContext.Properties["LogBasePath"] = BinaryPath;
            GlobalContext.Properties["LogPath"] = ApplicationName;
            XmlConfigurator.ConfigureAndWatch(new FileInfo(Path.Combine(BinaryPath, "log4net.Config")));
            Logger = LogManager.GetLogger(ApplicationName);
            #endregion

            #region Initialize Configuration

            

            #endregion

            #region Initialize Autofac

            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterInstance(this).As<PhotonApplication>().SingleInstance();
            builder.RegisterInstance(Logger).As<ILogger>();
            builder.RegisterType<IPhotonServerPeerFactory>();
            builder.RegisterType<IPhotonClientPeerFactory>();
            builder.RegisterType<IPhotonServerConfiguration>().SingleInstance();
            builder.RegisterType<IPhotonServerApplication>();

            ConfigurationBuilder moduleConfig = new ConfigurationBuilder();
            moduleConfig.AddJsonFile(Path.Combine(BinaryPath, "Modules", ApplicationName + ".config.json"));
            builder.RegisterModule(new ConfigurationModule(moduleConfig.Build()));

            IContainer container = builder.Build();

            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                PhotonServerConfiguration = scope.Resolve<IPhotonServerConfiguration>();
                PhotonClientPeerFactory = scope.Resolve<IPhotonClientPeerFactory>();
                PhotonServerPeerFactory = scope.Resolve<IPhotonServerPeerFactory>();
                Logger = scope.Resolve<ILogger>();
                PhotonServerModule = scope.Resolve<IPhotonServerApplication>();
            }

                #endregion

            PhotonServerModule.Setup();
        }

        protected override void OnStopRequested()
        {
            PhotonServerModule.OnStopRequested();
            base.OnStopRequested();
        }

        protected override void TearDown()
        {
            PhotonServerModule.TearDown();
        }

        protected override void OnServerConnectionFailed(int errorCode, string errorMessage, object state)
        {
            PhotonServerModule.OnServerConnectionFailed(errorCode, errorMessage, state);
        }
    }
}