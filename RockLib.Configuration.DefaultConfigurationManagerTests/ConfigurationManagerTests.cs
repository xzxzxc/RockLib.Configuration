using System;
using Xunit;

namespace RockLib.Configuration.DefaultConfigurationManagerTests
{
    public class ConfigurationManagerTests
    {
        static ConfigurationManagerTests()
        {
            // Set the environment variable before ConfigurationManager is used.
            Environment.SetEnvironmentVariable("AppSettings:Environment", "Prod");
        }

        [Fact(DisplayName = "DefaultConfigurationManagerTests: IsDefault is true by default.")]
        public void IsDefaultIsTrueByDefault()
        {
            Assert.True(ConfigurationManager.IsDefault);
        }

        [Fact(DisplayName = "DefaultConfigurationManagerTests: 'rocklib.config.json' is used by default.")]
        public void FileConfigIsUsedByDefault()
        {
            Assert.Equal("201740", ConfigurationManager.AppSettings["ApplicationId"]);
        }

        [Fact(DisplayName = "DefaultConfigurationManagerTests: Environment variables override values from 'rocklib.config.json'.")]
        public void EnvironmentVariablesOverrideFileConfig()
        {
            // Note that the "AppSettings:Environment" environment variable was
            // set in the static constructor with a value of "Prod".
            Assert.Equal("Prod", ConfigurationManager.AppSettings["Environment"]);
        }

        [Fact(DisplayName = "DefaultConfigurationManagerTests: IsLocked is true after the ConfigurationRoot property has been accessed.")]
        public void IsLockedIsTrueAfterConfigurationRootHasBeenAccessed()
        {
            // Accessing ConfigurationRoot causes IsLocked to be true.
            var root = ConfigurationManager.ConfigurationRoot;

            Assert.True(ConfigurationManager.IsLocked);
        }

        [Fact(DisplayName = "DefaultConfigurationManagerTests: Changing environment variable values when IsLocked is true has no effect.")]
        public void EnvironmentVariablesDoNotOverrideAfterLocked()
        {
            // Accessing ConfigurationRoot causes IsLocked to be true.
            var root = ConfigurationManager.ConfigurationRoot;

            // This is the same environment variable that we successfully changed in the static constructor.
            // Setting it this time, however, is too late.
            Environment.SetEnvironmentVariable("AppSettings:Environment", "Beta");

            // No effect.
            Assert.Equal("Prod", ConfigurationManager.AppSettings["Environment"]);
        }
    }
}
