﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Types;
using Dolittle.Utils;

namespace Dolittle.Web.Configuration
{
    public class NamespaceMapper
    {
        readonly StringMapper _clientToServerMapper = new StringMapper();
        readonly StringMapper _serverToClientMapper = new StringMapper();
        readonly ITypeFinder _typeFinder;

        public NamespaceMapper(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
            _clientToServerMapper = new StringMapper();
            _serverToClientMapper = new StringMapper();
        }

        public void Add(string clientNamespace, string serverNamespace)
        {
            _clientToServerMapper.AddMapping(clientNamespace,serverNamespace);
            _serverToClientMapper.AddMapping(serverNamespace,clientNamespace);
        }

        public bool CanResolveToClient(string serverNamespace)
        {
            return _serverToClientMapper.HasMappingFor(serverNamespace);
        }

        public bool CanResolveToServer(string clientNamespace)
        {
            return _clientToServerMapper.HasMappingFor(clientNamespace);
        }

        public virtual Type GetServerTypeFrom(string fullyQualifiedClientName)
        {
            var mappers = _clientToServerMapper.GetAllMatchingMappingsFor(fullyQualifiedClientName);
            foreach (var mapper in mappers)
            {
                var fullyQualifiedName = mapper.Resolve(fullyQualifiedClientName);
                var matchingType = _typeFinder.FindTypeByFullName(fullyQualifiedName);
                if (matchingType != null)
                    return matchingType;
            }
            return null;
        }

        public virtual string GetClientFullyQualifiedNameFrom(Type type)
        {
            return _serverToClientMapper.Resolve(type.FullName);
        }

        public virtual string GetClientNamespaceFrom(string serverNamespace)
        {
            return _serverToClientMapper.Resolve(serverNamespace);
        }
    }
}
