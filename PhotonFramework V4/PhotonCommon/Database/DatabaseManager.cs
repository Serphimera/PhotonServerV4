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
// Name    : PhotonFramework V4 | PhotonCommon | DatabaseManager.cs
// Created : 2016-12-09 12:45
// Modified: 2016-12-09 12:54
// ********************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Util;

namespace PhotonCommon.Database
{
    /// <summary>
    ///     ToDo: Is this thread safe?
    /// </summary>
    public static class DatabaseManager
    {
        static DatabaseManager()
        {
            IEnumerable<Assembly> mappingAssemblies =
                AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith("PhotonCommon"));

            SessionFactory = Fluently.Configure()
                .Database(MySQLConfiguration.Standard
                    .ConnectionString(
                        c =>
                            c.Server(DBSettings.Default.DBHost)
                                .Database(DBSettings.Default.DBName)
                                .Username(DBSettings.Default.DBUser)
                                .Password(DBSettings.Default.DBPass)))
                .Mappings(m => mappingAssemblies.ForEach(a => m.FluentMappings.AddFromAssembly(a)))
                .Diagnostics(m => m.Enable())
                .BuildSessionFactory();
        }

        #region Class Property Declarations

        /// <summary>Gets the session factory created from the initialized configuration. The returned factory is thread safe.</summary>
        private static ISessionFactory SessionFactory { get; }

        #endregion

        /// <summary>Opens a new session on the existing session factory</summary>
        /// <returns>ready to use ISession instance</returns>
        /// <remarks>
        ///     Dispose this instance after you're done with the instance, so after lazy loading has occurred. The returned
        ///     ISession instance is <b>not</b> thread safe.
        /// </remarks>
        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        #region Class Member Declarations

        #endregion
    }
}