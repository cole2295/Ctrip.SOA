<?xml version="1.0"?>
<configuration>
  <configSections>
    <section name="unity" type="Microsoft.Practices.Unity.Configuration.UnityConfigurationSection, Microsoft.Practices.Unity.Configuration"/>
    <!--<section name="databaseMappings" type="Microsoft.Practices.EnterpriseLibrary.-->
    <section name="dal" type="Arch.Data.DbEngine.Configuration.DbEngineConfigurationSection, Arch.Data" />
  </configSections>
  <unity configSource="Config\\Unity.config" />
  <!--<connectionStrings configSource="Config\\Database.Config" />-->
  <appSettings configSource="Config\\AppSetting.config" />
  <!--<databaseMappings configSource="Config\\DatabaseMap.Config" />-->
  <dal configSource="Config\DatabaseSets.config" />
  
  <system.web>
    <httpRuntime requestValidationMode="2.0" />
    <compilation debug="true" targetFramework="4.0" />
   
  </system.web>
  <system.diagnostics>
    <sources>
      <source name="System.ServiceModel.MessageLogging">
        <listeners>
          <add name="messages"
          type="System.Diagnostics.XmlWriterTraceListener"
          initializeData="c:\logs\messages.svclog" />
        </listeners>
      </source>
    </sources>
  </system.diagnostics>
  <system.serviceModel>
    <diagnostics>
      <messageLogging
           logEntireMessage="true"
           logMalformedMessages="false"
           logMessagesAtServiceLevel="true"
           logMessagesAtTransportLevel="false"
           maxMessagesToLog="3000"
           maxSizeOfMessageToLog="2000"/>
    </diagnostics>
    <bindings configSource="Config\ServiceModel.Bindings.config" />
    <behaviors>
      <serviceBehaviors>
        <behavior>
          <!-- To avoid disclosing metadata information, set the value below to false and remove the metadata endpoint above before deployment -->
          <serviceMetadata httpGetEnabled="true"/>
          <!-- To receive exception details in faults for debugging purposes, set the value below to true.  Set to false before deployment to avoid disclosing exception information -->
          <serviceDebug includeExceptionDetailInFaults="true"/>
        </behavior>
      </serviceBehaviors>
      <endpointBehaviors>
        <behavior name="ServiceViewEventBehavior">
          <dataContractSerializer maxItemsInObjectGraph="2147483647"/>
          <!--序列化配置-->
        </behavior>
      </endpointBehaviors>
    </behaviors>
    <serviceHostingEnvironment multipleSiteBindingsEnabled="true" minFreeMemoryPercentageToActivateService="1"  aspNetCompatibilityEnabled="true" />

    <services configSource="Config\ServiceModel.Services.config" />
  </system.serviceModel>
  <system.webServer>
    <modules runAllManagedModulesForAllRequests="true"/>

  </system.webServer>

</configuration>
