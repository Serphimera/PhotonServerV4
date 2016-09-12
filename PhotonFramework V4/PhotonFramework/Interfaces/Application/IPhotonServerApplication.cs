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
// Name    : PhotonFramework V4 | PhotonFramework | IPhotonServerApplication.cs
// Created : 2016-12-09 12:23
// Modified: 2016-12-09 12:54
// ********************************************************************************

namespace PhotonFramework.Interfaces.Application
{
    /// <summary>
    ///     Interface for all (sub) server application residing in separate modules and
    ///     loaded on runtime by the controlling PhotonApplication
    /// </summary>
    public interface IPhotonServerApplication
    {
        /// <summary>
        ///     Called on startup to run initializing tasks needed to run the server
        ///     module.
        /// </summary>
        void Setup();

        /// <summary>
        ///     Called when the controlling PhotonApplication receives a request to
        ///     stop execution.
        /// </summary>
        void OnStopRequested();

        /// <summary>
        ///     Called when the controlling PhotonApplication tears down after shutdown
        ///     tasks have completed.
        /// </summary>
        void TearDown();

        /// <summary>
        ///     Called whenever a connection to a (sub) server failed for some reason.
        /// </summary>
        /// <param name="errorCode">The code of the error</param>
        /// <param name="errorMessage">Some kind of error message</param>
        /// <param name="state">Some state object</param>
        void OnServerConnectionFailed(int errorCode, string errorMessage, object state);
    }
}