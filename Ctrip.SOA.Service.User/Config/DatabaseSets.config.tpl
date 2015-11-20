<?xml version="1.0"?>
<dal>
  <databaseSets>
    <databaseSet name="TestDB" provider="sqlProvider">
      <add name="TestDB_M" databaseType="Master" sharding="" connectionString="Test_InsertDB{$DBDataCenter}"/>
      <add name="TestDB_S" databaseType="Slave" sharding="" connectionString="Test_SelectDB{$DBDataCenter}"/>
    </databaseSet>
  </databaseSets>

  <databaseProviders>
    <add name="sqlProvider" type="Arch.Data.DbEngine.Providers.SqlDatabaseProvider,Arch.Data"/>
  </databaseProviders>
  <logListeners>
    <add name="clog" type="Arch.Data.Common.Logging.Listeners.CentralLoggingListener,Arch.Data" level="Information" setting=""/>
  </logListeners>
  <metrics name="centrallogging"/>
</dal>