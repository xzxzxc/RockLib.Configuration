﻿
using Microsoft.Extensions.Configuration;
using System;

namespace RockLib.Configuration
{

    public class AppSettings
    {
        private readonly Func<IConfigurationRoot> _getConfigurationRoot;

        public AppSettings(Func<IConfigurationRoot> getConfigurationRoot)
        {
            _getConfigurationRoot = getConfigurationRoot;
        }

        public string this[string key]
        {
            get
            {
                var value = _getConfigurationRoot()[key];

                if (string.IsNullOrEmpty(value))
                {
                    throw new NullReferenceException($"The provided key ({key}) was not found in the configuration file.  Ensure you are requesting a valid key");
                }

                return value;
            }
        }
    }

    public class ConnectionStings
    {
        
    }
}
